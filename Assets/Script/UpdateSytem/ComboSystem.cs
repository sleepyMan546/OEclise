using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Rendering.LookDev;

public class ComboSystem : MonoBehaviour
{
    public WeaponSwitchDop weaponSwitchDop;
    public PlayerMovement playerMovement;
    public Hp hp;
    public float hakiRange = 100f;
    public GameObject hakiEffectPrefab;
    public EnemyHp enemyHp;
    [SerializeField] private float shakeDuration = 0.5f;       
    [SerializeField] private float shakeMagnitude = 0.3f;

    // UI Elements
    public Image[] comboImages; // Array ของ Image สำหรับแสดงรูปปืน (2-3 ช่อง)
    public TextMeshProUGUI buffText; // Text สำหรับแสดงสถานะบัฟ
    public Camera mainCamera;

    // Combo Data
    private List<string> currentCombo = new List<string>(); // เก็บลำดับปืนในคอมโบ
    private string[] weapons = { "pistol", "shotgun", "machineGun" }; // รายการปืน
    public Sprite pistolSprite; // Sprite ของปืนพก
    public Sprite shotgunSprite; // Sprite ของลูกซอง
    public Sprite machineGunSprite; // Sprite ของปืนกล

    // Buff Settings
    private float buffDuration = 20f; // ระยะเวลาของบัฟ
    private bool isBuffActive = false; // ตัวแปรสถานะบัฟ
    private float originalMoveSpeed; // ความเร็วเริ่มต้นของผู้เล่น
    private Vector3 originalScale; // ขนาดเริ่มต้นของตัวละคร
    private float originalDashCooldown; // Cooldown เริ่มต้นของ Dash
    private float originalAirJumpCooldown; // Cooldown เริ่มต้นของ Air Jump
    private float originalBarrierCooldown; // Cooldown เริ่มต้นของ Barrier

    // Clone Buff Settings
    public GameObject playerPrefab; // Prefab ของ Player สำหรับ Clone Buff

    // Combo Timer
    public float comboTimeLimit = 10f; // เวลาที่ให้ทำคอมโบ
    private float comboTimer = 0f; // ตัวจับเวลา
    private bool isComboActive = false; // ตัวแปรสถานะคอมโบ

    // ตัวแปรสำหรับตรวจสอบการใช้สกิล
    private bool wasDashing = false; // ตรวจสอบว่า Dash ถูกใช้หรือไม่
    private int lastAirJumpCount = 0; // เก็บค่า airJumpCount ก่อนหน้า

    void Start()
    {
        // ตรวจสอบว่า References ไม่เป็น null
        if (weaponSwitchDop == null || playerMovement == null || hp == null)
        {
            Debug.LogError("ComboSystem: Missing references to WeaponSwitchDop, PlayerMovement, or Hp!");
            return;
        }

        if (comboImages.Length < 2)
        {
            Debug.LogError("ComboSystem: Need at least 2 combo images in the array!");
            return;
        }

        if (buffText == null)
        {
            Debug.LogError("ComboSystem: Buff Text (TextMeshProUGUI) is not assigned!");
            return;
        }

        // ตรวจสอบว่า Sprites ไม่เป็น null
        if (pistolSprite == null || shotgunSprite == null || machineGunSprite == null)
        {
            Debug.LogError("ComboSystem: One or more weapon sprites are not assigned!");
            return;
        }

        // ตรวจสอบว่า Player Prefab ไม่เป็น null
        if (playerPrefab == null)
        {
            Debug.LogError("ComboSystem: Player Prefab is not assigned for Clone Buff!");
            return;
        }

        // เก็บค่าเริ่มต้น
        originalMoveSpeed = playerMovement.moveSpeed;
        originalScale = playerMovement.transform.localScale; // เก็บขนาดเริ่มต้นของตัวละคร

        // เก็บ Cooldown เริ่มต้น (สมมติว่ามีตัวแปรเหล่านี้ใน PlayerMovement และ Hp)
        originalDashCooldown = playerMovement.dashCooldown; // ต้องเพิ่มตัวแปรใน PlayerMovement
        originalAirJumpCooldown = playerMovement.airJumpCount; // ต้องเพิ่มตัวแปรใน PlayerMovement
        originalBarrierCooldown = hp.barrierCooldown; // ต้องเพิ่มตัวแปรใน Hp

        // เริ่มคอมโบแรก
        GenerateNewCombo();
    }

    void Update()
    {
        // อัพเดตตัวจับเวลาคอมโบ
        if (isComboActive)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                Debug.Log("ComboSystem: Time's up! Resetting combo.");
                GenerateNewCombo();
            }
        }

        // ตรวจสอบการใช้สกิล ถ้ามีคอมโบให้ทำ
        if (currentCombo.Count > 0)
        {
            string requiredWeapon = currentCombo[0]; // ปืนที่ต้องใช้
            string currentWeapon = weaponSwitchDop.GetCurrentWeapon(); // ปืนปัจจุบัน

            // Debug: แสดงปืนที่ต้องการและปืนปัจจุบัน
            Debug.Log($"ComboSystem: Required Weapon = {requiredWeapon}, Current Weapon = {currentWeapon}");

            // ตรวจสอบว่าปืนปัจจุบันตรงกับปืนที่ต้องการหรือไม่
            if (currentWeapon == requiredWeapon)
            {
                // ตรวจสอบการใช้สกิลตามปืน
                if (requiredWeapon == "pistol")
                {
                    // ตรวจสอบ Dash (Mouse Right Click)
                    if (!wasDashing && playerMovement.isDashing)
                    {
                        Debug.Log("ComboSystem: Pistol Dash detected!");
                        CompleteComboStep();
                    }
                    wasDashing = playerMovement.isDashing; // อัพเดตสถานะ Dash
                }
                else if (requiredWeapon == "shotgun")
                {
                    // ตรวจสอบ Air Jump (Space)
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Debug.Log("ComboSystem: Shotgun Air Jump detected!");
                        CompleteComboStep();
                        lastAirJumpCount = playerMovement.airJumpCount; // อัพเดตค่า airJumpCount
                    }
                }
                else if (requiredWeapon == "machineGun")
                {
                    // ตรวจสอบการยิง (Mouse Left Click)
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        Debug.Log("ComboSystem: Machine Gun Fire detected!");
                        CompleteComboStep();
                    }
                }
            }
            else
            {
                Debug.LogWarning($"ComboSystem: Weapon mismatch! Required: {requiredWeapon}, but Current: {currentWeapon}");
            }
        }
    }

    void GenerateNewCombo()
    {
        // รีเซ็ตคอมโบ
        currentCombo.Clear();
        isComboActive = false;

        
        int comboLength = Random.Range(4, 6); 

        // สุ่มปืนและเพิ่มในคอมโบ
        for (int i = 0; i < comboLength; i++)
        {
            int weaponIndex = Random.Range(0, weapons.Length); // สุ่มดัชนีปืน
            currentCombo.Add(weapons[weaponIndex]);
        }

        // อัพเดต UI
        UpdateComboUI();

        // เริ่มตัวจับเวลาคอมโบ
        comboTimer = comboTimeLimit;
        isComboActive = true;

        Debug.Log("ComboSystem: New combo generated - " + string.Join(" -> ", currentCombo));
    }

    void UpdateComboUI()
    {
        // อัพเดต UI ตาม currentCombo
        for (int i = 0; i < comboImages.Length; i++)
        {
            if (i < currentCombo.Count)
            {
                // แสดงรูปปืนตามลำดับ
                string weapon = currentCombo[i];
                switch (weapon)
                {
                    case "pistol":
                        comboImages[i].sprite = pistolSprite;
                        comboImages[i].enabled = true; // เปิดการแสดงผล
                        break;
                    case "shotgun":
                        comboImages[i].sprite = shotgunSprite;
                        comboImages[i].enabled = true;
                        break;
                    case "machineGun":
                        comboImages[i].sprite = machineGunSprite;
                        comboImages[i].enabled = true;
                        break;
                    default:
                        Debug.LogWarning("ComboSystem: Unknown weapon in combo - " + weapon);
                        comboImages[i].enabled = false;
                        break;
                }
            }
            else
            {
                // ปิด Image ถ้าไม่มีปืนในช่องนี้
                comboImages[i].enabled = false;
            }
        }

        Debug.Log("ComboSystem: UI Updated - " + string.Join(" -> ", currentCombo));
    }

    void CompleteComboStep()
    {
        // ลบปืนแรกออกจากคอมโบ
        currentCombo.RemoveAt(0);
        UpdateComboUI();

        Debug.Log("ComboSystem: Combo step completed! Remaining steps: " + currentCombo.Count);

        // ถ้าคอมโบครบแล้ว
        if (currentCombo.Count == 0)
        {
            ApplyRandomBuff();
            GenerateNewCombo();
        }
    }

    void ApplyRandomBuff()
    {
        //if (isBuffActive)
        //{
        //    Debug.Log("ComboSystem: Buff already active, skipping new buff.");
        //    return;
        //}

        // สุ่มบัฟ (0-5) เพิ่มจาก 4 เป็น 6 ตัวเลือก
        int buffIndex = Random.Range(0, 7);
        switch (buffIndex)
        {
            case 0: // ฟื้นฟู HP
                int healAmount = 100;
                hp.TakeDamage(-healAmount); // ใช้ TakeDamage ด้วยค่าลบเพื่อเพิ่ม HP
                buffText.text = "Buff: Heal +" + healAmount;
                Debug.Log("ComboSystem: Heal Buff applied! +" + healAmount + " HP");
                break;

            case 1: // แยกร่าง
                StartCoroutine(CloneBuff());
                buffText.text = "Buff: Clone";
                Debug.Log("ComboSystem: Clone Buff applied!");
                break;

            case 2: // วิ่งเร็ว
                float speedMultiplier = 1.5f;
                playerMovement.moveSpeed *= speedMultiplier;
                StartCoroutine(SpeedBuff(speedMultiplier));
                buffText.text = "Buff: Speed x" + speedMultiplier;
                Debug.Log("ComboSystem: Speed Buff applied! x" + speedMultiplier);
                break;

            case 3: // ได้โล่ (เพิ่ม HP)
                int healAmountA = 500;
                hp.TakeDamage(-healAmountA);
                buffText.text = "Buff: Shield (HP +" + healAmountA + ")";
                Debug.Log("ComboSystem: Shield Buff applied! HP +" + healAmountA);
                break;

            case 4: // ตัวใหญ่ (Super Form)
                StartCoroutine(SuperFormBuff());
                buffText.text = "Buff: Super Form";
                Debug.Log("ComboSystem: Super Form Buff applied!");
                break;

            case 5: // ตัวเล็ก (Mini Form)
                StartCoroutine(MiniFormBuff());
                buffText.text = "Buff: Mini Form";
                Debug.Log("ComboSystem: Mini Form Buff applied!");
                break;
            case 6: // ฮาคิราชันย์
                StartCoroutine(ConquerorsHakiBuff());
                buffText.text = "Buff: Conqueror's Haki";
                Debug.Log("ComboSystem: Conqueror's Haki Buff applied!");
                break;
        }
    }

    IEnumerator CloneBuff()
    {
        isBuffActive = true;
        // สร้าง Clone จาก Player Prefab
        GameObject clone = Instantiate(playerPrefab, playerMovement.transform.position, Quaternion.identity);
        Debug.Log("ComboSystem: Clone created at position: " + clone.transform.position);

        // รอตามระยะเวลา buffDuration แล้วทำลาย Clone
        yield return new WaitForSeconds(buffDuration);

        Destroy(clone);
        Debug.Log("ComboSystem: Clone destroyed after " + buffDuration + " seconds");
        isBuffActive = false;
        buffText.text = "";
    }

    IEnumerator SpeedBuff(float speedMultiplier)
    {
        isBuffActive = true;
        yield return new WaitForSeconds(buffDuration);
        playerMovement.moveSpeed = originalMoveSpeed;
        isBuffActive = false;
        buffText.text = "";
    }

    IEnumerator SuperFormBuff()
    {
        isBuffActive = true;

        // เก็บค่าเดิมก่อนปรับ
        float speedMultiplier = 2f;
        
        int healAmount = 100;

        // ปรับขนาดตัวละคร (ใหญ่ขึ้น)
        playerMovement.transform.localScale = originalScale * 1.5f;

        // เพิ่ม HP
        hp.TakeDamage(-healAmount);

        // เพิ่มความเร็ว
        playerMovement.moveSpeed *= speedMultiplier;

        // ลด Cooldown สกิล
        playerMovement.dashCooldown = originalDashCooldown;
        playerMovement.airJumpsAllowed += 11;
        hp.barrierCooldown = 0.2f;

        // รอตามระยะเวลา
        yield return new WaitForSeconds(20f);

        // รีเซ็ตทุกอย่างกลับสู่ค่าเริ่มต้น
        playerMovement.transform.localScale = originalScale;
        playerMovement.moveSpeed = originalMoveSpeed;
        playerMovement.dashCooldown = originalDashCooldown;
        playerMovement.airJumpsAllowed = 1;
        hp.barrierCooldown = originalBarrierCooldown;

        isBuffActive = false;
        buffText.text = "";
    }

    IEnumerator MiniFormBuff()
    {
        isBuffActive = true;

        // เก็บค่าเดิมก่อนปรับ
        float speedMultiplier = 2f;
        float cooldownReduction = 0.7f; // ลด Cooldown 50%
        int healAmount = 100;

        // ปรับขนาดตัวละคร (เล็กลง)
        playerMovement.transform.localScale = originalScale * 0.5f;

        // เพิ่ม HP
        hp.TakeDamage(-healAmount);

        // เพิ่มความเร็ว
        playerMovement.moveSpeed *= speedMultiplier;

        // ลด Cooldown สกิล
        playerMovement.dashCooldown *= cooldownReduction;
        playerMovement.airJumpCount += 3;
        hp.barrierCooldown *= cooldownReduction;

        // รอตามระยะเวลา
        yield return new WaitForSeconds(20f);

        // รีเซ็ตทุกอย่างกลับสู่ค่าเริ่มต้น
        playerMovement.transform.localScale = originalScale;
        playerMovement.moveSpeed = originalMoveSpeed;
        playerMovement.dashCooldown = originalDashCooldown;
        playerMovement.airJumpCount += 3;
        hp.barrierCooldown = originalBarrierCooldown;

        isBuffActive = false;
        buffText.text = "";
    }
    IEnumerator ConquerorsHakiBuff()
    {
        isBuffActive = true;

        // สร้าง Visual Effect รอบตัวผู้เล่น
        GameObject hakiEffect = Instantiate(hakiEffectPrefab, playerMovement.transform.position, Quaternion.identity);
        hakiEffect.transform.SetParent(playerMovement.transform); // ทำให้ Effect ติดตามผู้เล่น

        // ปล่อยออร่า Conqueror's Haki
        // ตรวจจับศัตรูในระยะรอบตัวผู้เล่น
        
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(playerMovement.transform.position, hakiRange);
        foreach (Collider2D enemy in enemiesInRange)
        {
            if (enemy.CompareTag("Enemy"))
            {
                StartCoroutine(ShakeCamera());
                Debug.Log($"ComboSystem: Enemy {enemy.name} defeated by Conqueror's Haki!");
                Destroy(enemy.gameObject);
            }
        }

        // รอตามระยะเวลา
        yield return new WaitForSeconds(1f);

        // ทำลาย Visual Effect
        Destroy(hakiEffect);

        isBuffActive = false;
        buffText.text = "";
    }
    private IEnumerator ShakeCamera()
    {
        Vector3 originalCameraPosition = mainCamera.transform.position;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            elapsed += Time.deltaTime;
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            mainCamera.transform.position = new Vector3(originalCameraPosition.x + x, originalCameraPosition.y + y, originalCameraPosition.z);
            yield return null;
        }

        mainCamera.transform.position = originalCameraPosition;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerMovement.transform.position, hakiRange);
    }
}
