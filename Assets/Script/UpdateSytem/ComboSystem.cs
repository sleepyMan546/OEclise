using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboSystem : MonoBehaviour
{
    public WeaponSwitchDop weaponSwitchDop;
    public PlayerMovement playerMovement;
    public Hp hp;

    // UI Elements
    public Image[] comboImages; // Array ของ Image สำหรับแสดงรูปปืน (2-3 ช่อง)
    public TextMeshProUGUI buffText;// Text สำหรับแสดงสถานะบัฟ (เพิ่มเข้ามา)

    // Combo Data
    private List<string> currentCombo = new List<string>(); // เก็บลำดับปืนในคอมโบ
    private string[] weapons = { "pistol", "shotgun", "machineGun" }; // รายการปืน
    public Sprite pistolSprite; // Sprite ของปืนพก
    public Sprite shotgunSprite; // Sprite ของลูกซอง
    public Sprite machineGunSprite; // Sprite ของปืนกล

    // Buff Settings
    private float buffDuration = 5f; // ระยะเวลาของบัฟ
    private bool isBuffActive = false; // ตัวแปรสถานะบัฟ
    private float originalMoveSpeed;
    public Transform clonespaw;// ความเร็วเริ่มต้นของผู้เล่น

    // Clone Buff Settings
    public GameObject playerPrefab; // Prefab ของ Player สำหรับ Clone Buff

    // Combo Timer
    public float comboTimeLimit = 10f; // เวลาที่ให้ทำคอมโบ
    private float comboTimer = 0f; // ตัวจับเวลา
    private bool isComboActive = false; // ตัวแปรสถานะคอมโบ

    // ตัวแปรสำหรับตรวจสอบการใช้สกิล
    private bool wasDashing = false; // ตรวจสอบว่า Dash ถูกใช้หรือไม่
    private int lastAirJumpCount = 0; // เก็บค่า airJumpCount ก่อนหน้า
    private bool wasBarrierActive = false; // ตรวจสอบว่า Barrier ถูกใช้หรือไม่
    private bool barrierActivatedThisFrame = false; // เพิ่มตัวแปรเพื่อตรวจสอบการเปิด Barrier ในเฟรมนี้

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
            Debug.LogError("ComboSystem: Buff Text is not assigned!");
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

        // เก็บความเร็วเริ่มต้นของผู้เล่น
        originalMoveSpeed = playerMovement.moveSpeed;

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

        // รีเซ็ตตัวแปรตรวจสอบ Barrier ในแต่ละเฟรม
        barrierActivatedThisFrame = false;

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
                    if (Input.GetKeyDown(KeyCode.Space) )
                    {
                        Debug.Log("ComboSystem: Shotgun Air Jump detected!");
                        CompleteComboStep();
                        lastAirJumpCount = playerMovement.airJumpCount; // อัพเดตค่า airJumpCount
                    }
                }
                else if (requiredWeapon == "machineGun")
                {
                    // ตรวจสอบ Barrier (Mouse Right Click)
                    bool isBarrierActive = hp.IsBarrierActive();
                    if (!wasBarrierActive && isBarrierActive)
                    {
                        Debug.Log("ComboSystem: Machine Gun Barrier detected!");
                        CompleteComboStep();
                        barrierActivatedThisFrame = true; // ระบุว่า Barrier ถูกเปิดในเฟรมนี้
                    }
                    else if (Input.GetKeyDown(KeyCode.Mouse1) && !isBarrierActive && hp.GetBarrierCooldown() <= 0)
                    {
                        // ถ้ากด Mouse Right Click และ Barrier พร้อมใช้งาน
                        Debug.Log("ComboSystem: Attempting to activate Barrier for Machine Gun!");
                        if (hp.ActivateBarrier())
                        {
                            Debug.Log("ComboSystem: Machine Gun Barrier activated successfully!");
                            CompleteComboStep();
                            barrierActivatedThisFrame = true;
                        }
                        else
                        {
                            Debug.LogWarning("ComboSystem: Failed to activate Barrier! Cooldown: " + hp.GetBarrierCooldown());
                        }
                    }
                    wasBarrierActive = isBarrierActive; // อัพเดตสถานะ Barrier
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

        // สุ่มจำนวนปืนในคอมโบ (2 หรือ 3)
        int comboLength = Random.Range(2, 4); // Random.Range(2, 4) จะได้ 2 หรือ 3

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

        // สุ่มบัฟ (0-3)
        int buffIndex = Random.Range(0, 5);
        switch (buffIndex)
        {
            case 0: // ฟื้นฟู HP
                int healAmount = 500;
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
                float speedMultiplier = 2f;
                playerMovement.moveSpeed *= speedMultiplier;
                StartCoroutine(SpeedBuff(speedMultiplier));
                buffText.text = "Buff: Speed x" + speedMultiplier;
                Debug.Log("ComboSystem: Speed Buff applied! x" + speedMultiplier);
                break;

            case 3: // ได้โล่
                int healAmountA = 500;
                hp.TakeDamage(-healAmountA);
                buffText.text = "Buff: Heal +" + healAmountA;
                Debug.Log("ComboSystem:heal 500");
                break;
            case 4: // ได้โล่
                float jumpde = 20f;
                playerMovement.jumpForce = jumpde;
                StartCoroutine(JumpBuff(jumpde));
                buffText.text = "Buff: Jump +" + jumpde;
                Debug.Log("ComboSystem: jumpboot");
                break;
        }
    }

    IEnumerator CloneBuff()
    {
        isBuffActive = true;
        // สร้าง Clone จาก Player Prefab
        GameObject clone = Instantiate(playerPrefab,playerMovement.transform.position, Quaternion.identity);
        Debug.Log("ComboSystem: Clone created at position: " + clone.transform.position);

        // รอตามระยะเวลา buffDuration แล้วทำลาย Clone
        yield return new WaitForSeconds(50f);

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
    IEnumerator JumpBuff(float speedMultiplier)
    {
        isBuffActive = true;
        yield return new WaitForSeconds(buffDuration);
        playerMovement.moveSpeed = originalMoveSpeed;
        isBuffActive = false;
        playerMovement.jumpForce = 10f;
        buffText.text = "";
    }
}
