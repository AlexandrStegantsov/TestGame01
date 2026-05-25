using UnityEngine;
using UnityEngine.UI;

public class ReloadHUD : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;

    [SerializeField] private Image reloadCircle;

    private WeaponController currentWeapon;

    private void Start()
    {
        reloadCircle.fillAmount = 0f;

        reloadCircle.gameObject.SetActive(false);

        weaponManager.OnWeaponChanged +=
            HandleWeaponChanged;

        HandleWeaponChanged(
            weaponManager.CurrentWeapon);
    }

    private void OnDestroy()
    {
        if (weaponManager != null)
        {
            weaponManager.OnWeaponChanged -=
                HandleWeaponChanged;
        }

        if (currentWeapon != null)
        {
            currentWeapon.OnReloadStarted -=
                HandleReloadStarted;

            currentWeapon.OnReloadProgress -=
                HandleReloadProgress;

            currentWeapon.OnReloadFinished -=
                HandleReloadFinished;
        }
    }

    private void HandleWeaponChanged(
        WeaponController weapon)
    {
        if (currentWeapon != null)
        {
            currentWeapon.OnReloadStarted -=
                HandleReloadStarted;

            currentWeapon.OnReloadProgress -=
                HandleReloadProgress;

            currentWeapon.OnReloadFinished -=
                HandleReloadFinished;
        }

        currentWeapon = weapon;

        if (currentWeapon == null)
            return;

        currentWeapon.OnReloadStarted +=
            HandleReloadStarted;

        currentWeapon.OnReloadProgress +=
            HandleReloadProgress;

        currentWeapon.OnReloadFinished +=
            HandleReloadFinished;
    }

    private void HandleReloadStarted()
    {
        reloadCircle.gameObject.SetActive(true);

        reloadCircle.fillAmount = 0f;
    }

    private void HandleReloadProgress(
        float progress)
    {
        reloadCircle.fillAmount =
            progress;
    }

    private void HandleReloadFinished()
    {
        reloadCircle.fillAmount = 0f;

        reloadCircle.gameObject.SetActive(false);
    }
}