using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Health playerHealth;

    [SerializeField] private TMP_Text hpText;

    [Header("Weapon")]
    [SerializeField] private WeaponManager weaponManager;

    [SerializeField] private TMP_Text ammoText;

    private WeaponController currentWeapon;

    private void Start()
    {
        playerHealth.OnHealthChanged +=
            UpdateHealth;

        weaponManager.OnWeaponChanged +=
            HandleWeaponChanged;

        HandleWeaponChanged(
            weaponManager.CurrentWeapon);

        UpdateHealth(
            playerHealth.CurrentHealth,
            playerHealth.MaxHealth);
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -=
                UpdateHealth;
        }

        if (weaponManager != null)
        {
            weaponManager.OnWeaponChanged -=
                HandleWeaponChanged;
        }

        UnsubscribeWeapon();
    }

    private void HandleWeaponChanged(
        WeaponController weapon)
    {
        UnsubscribeWeapon();

        currentWeapon = weapon;

        if (currentWeapon == null)
            return;

        currentWeapon.OnAmmoChanged +=
            UpdateAmmo;

        UpdateAmmo(
            currentWeapon.CurrentAmmo,
            currentWeapon.MaxAmmo);
    }

    private void UnsubscribeWeapon()
    {
        if (currentWeapon == null)
            return;

        currentWeapon.OnAmmoChanged -=
            UpdateAmmo;
    }

    private void UpdateHealth(
        float current,
        float max)
    {
        hpText.text =
            $"HP: {Mathf.CeilToInt(current)}";
    }

    private void UpdateAmmo(
        int current,
        int max)
    {
        ammoText.text =
            $"{current} / {max}";
    }
}