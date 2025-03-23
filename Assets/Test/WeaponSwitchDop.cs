using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponSwitchDop : MonoBehaviour
{
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject machineGun;
    public GameObject shotgunEffectPrefab;
    public GameObject pistolSwitchEffectPrefab;
    public GameObject machinegunSwitchEffectPrefab;
    [SerializeField] private AudioSource pistolSwitchSoundSource;
    [SerializeField] private AudioSource shotgunSwitchSoundSource;
    [SerializeField] private AudioSource machineGunSwitchSoundSource;

    private Dictionary<string, GameObject> weapons;
    private string[] weaponOrder = { "pistol", "shotgun", "machineGun" };
    private int currentWeaponIndex = 0;
    public Sprite pistolIcon;
    public Sprite shotgunIcon;
    public Sprite machineGunIcon;

    
    public Image weaponIconUI;

    void Start()
    {
        weapons = new Dictionary<string, GameObject>
        {
            { "pistol", pistol },
            { "shotgun", shotgun },
            { "machineGun", machineGun }
        };

        UpdateWeaponVisibility();
        UpdateWeaponIconUI();
        if (pistolSwitchSoundSource == null || shotgunSwitchSoundSource == null || machineGunSwitchSoundSource == null)
        {
            Debug.LogError("Missing audio source for one or more weapons!");
        }
      
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon("pistol");
            

        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            SwitchWeapon("shotgun");
          TriggerWeaponSwitchEffectShotGun();

        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchWeapon("machineGun");
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon("pistol");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon("shotgun");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon("machineGun");
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            SwitchToNextWeapon();
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            SwitchToPreviousWeapon();
        }
    }

    void SwitchWeapon(string weaponName)
    {
        if (weapons.ContainsKey(weaponName) && weaponOrder[currentWeaponIndex] != weaponName)
        {
            currentWeaponIndex = System.Array.IndexOf(weaponOrder, weaponName);
            UpdateWeaponVisibility();
            PlaySwitchSound(weaponName);
            UpdateWeaponIconUI();

        }
    }

    void SwitchToNextWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % weaponOrder.Length;
        UpdateWeaponVisibility();
        PlaySwitchSound(weaponOrder[currentWeaponIndex]);
        UpdateWeaponIconUI();

    }

    void SwitchToPreviousWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex - 1 + weaponOrder.Length) % weaponOrder.Length;
        UpdateWeaponVisibility();
        PlaySwitchSound(weaponOrder[currentWeaponIndex]);
        UpdateWeaponIconUI();
    }

    void UpdateWeaponVisibility()
    {
        foreach (var weapon in weapons.Values)
        {
            weapon.SetActive(false);
        }
        weapons[weaponOrder[currentWeaponIndex]].SetActive(true);
    }

    void PlaySwitchSound(string weaponName)
    {
        if (weaponName == "pistol" && pistolSwitchSoundSource != null)
        {
            pistolSwitchSoundSource.Play();
            TriggerWeaponSwitchEffectPistol();
        }
        else if (weaponName == "shotgun" && shotgunSwitchSoundSource != null)
        {
            shotgunSwitchSoundSource.Play();
            TriggerWeaponSwitchEffectShotGun();
        }
        else if (weaponName == "machineGun" && machineGunSwitchSoundSource != null)
        {
            machineGunSwitchSoundSource.Play();
            TriggerWeaponSwitchEffectMachinegun();
        }
    }

    public string GetCurrentWeapon()
    {
        return weaponOrder[currentWeaponIndex];
    }

    
    void TriggerWeaponSwitchEffectShotGun()
    {
        if (shotgunEffectPrefab != null)
        {
            GameObject effectInstance = Instantiate(shotgunEffectPrefab, transform.position, Quaternion.identity); 

            
            StartCoroutine(AnimateScaleBurst(effectInstance));
        }
    }
    void TriggerWeaponSwitchEffectPistol()
    {
        if (pistolSwitchEffectPrefab != null)
        {
            GameObject effectInstance = Instantiate(pistolSwitchEffectPrefab, transform.position, Quaternion.identity);


            StartCoroutine(AnimateScaleBurst(effectInstance));
        }
    }

    void TriggerWeaponSwitchEffectMachinegun()
    {
        if (pistolSwitchEffectPrefab != null)
        {
            GameObject effectInstance = Instantiate(machinegunSwitchEffectPrefab, transform.position, Quaternion.identity);


            StartCoroutine(AnimateScaleBurst(effectInstance));
        }
    }

    IEnumerator AnimateScaleBurst(GameObject effect)
    {
        SpriteRenderer spriteRenderer = effect.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) yield break; 

        Vector3 initialScale = Vector3.zero;
        Vector3 targetScale = Vector3.one * 3f; 
        float duration = 0.5f;
        float time = 0f;

        effect.transform.localScale = initialScale; 

        while (time < duration)
        {
            time += Time.deltaTime;
            float normalizedTime = time / duration;
            
            effect.transform.localScale = Vector3.Lerp(initialScale, targetScale, normalizedTime);
            spriteRenderer.color = Color.Lerp(Color.white, new Color(1f, 1f, 1f, 0f), normalizedTime);
            yield return null;
        }

        Destroy(effect); 
    }
    void UpdateWeaponIconUI()
    {
        if (weaponIconUI != null)
        {
            string currentWeaponName = GetCurrentWeapon();

           
            switch (currentWeaponName)
            {
                case "pistol":
                    weaponIconUI.sprite = pistolIcon;
                    break;
                case "shotgun":
                    weaponIconUI.sprite = shotgunIcon;
                    break;
                case "machineGun":
                    weaponIconUI.sprite = machineGunIcon;
                    break;
                default:
                    weaponIconUI.sprite = null; 
                    Debug.LogWarning("Unknown weapon name: " + currentWeaponName);
                    break;
            }
        }
    }
}
