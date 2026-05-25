using System;
using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform shootPoint;

    [SerializeField] private ProjectilePool projectilePool;

    [SerializeField] private WeaponData currentWeapon;

    [SerializeField] private VFXPool impactPool;

    private IInputService inputService;

    private float nextShootTime;

    private int currentAmmo;

    private bool isReloading;

    public int CurrentAmmo =>
        currentAmmo;

    public int MaxAmmo =>
        currentWeapon.magazineSize;

    public event Action<int, int>
        OnAmmoChanged;

    public event Action<float>
        OnReloadProgress;

    public event Action OnReloadStarted;
    public event Action OnReloadFinished;

    private void Awake()
    {
        inputService =
            ServiceLocator.Get<IInputService>();

        currentAmmo =
            currentWeapon.magazineSize;
    }

    private void OnEnable()
    {
        if (inputService != null)
        {
            inputService.OnShoot += Shoot;

            inputService.OnReload += Reload;
        }
    }

    private void OnDisable()
    {
        if (inputService != null)
        {
            inputService.OnShoot -= Shoot;

            inputService.OnReload -= Reload;
        }
    }

    private void Start()
    {
        OnAmmoChanged?.Invoke(
            currentAmmo,
            MaxAmmo);
    }

    private void Update()
    {
        if (currentWeapon.automatic &&
            inputService.ShootHeld)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (currentWeapon.automatic == false &&
            inputService.ShootHeld == false)
        {
            return;
        }

        if (isReloading)
            return;

        if (Time.time < nextShootTime)
            return;

        if (currentAmmo <= 0)
        {
            Debug.LogWarning("No Ammo");

            return;
        }

        nextShootTime =
            Time.time + currentWeapon.fireRate;

        currentAmmo--;

        OnAmmoChanged?.Invoke(
            currentAmmo,
            MaxAmmo);

        Projectile projectile =
            projectilePool.GetProjectile();

        projectile.OnHit +=
            HandleProjectileHit;

        projectile.transform
            .SetPositionAndRotation(
                shootPoint.position,
                shootPoint.rotation);

        projectile.Initialize(
            currentWeapon.projectileSpeed,
            projectilePool);

        
    }

    private void Reload()
    {
        if (isReloading)
            return;

        if (currentAmmo ==
            currentWeapon.magazineSize)
        {
            return;
        }

        StartCoroutine(
            ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        isReloading = true;

        OnReloadStarted?.Invoke();

        float elapsed = 0f;

        while (elapsed <
               currentWeapon.reloadTime)
        {
            elapsed += Time.deltaTime;

            float progress =
                elapsed /
                currentWeapon.reloadTime;

            OnReloadProgress?.Invoke(
                progress);

            yield return null;
        }

        currentAmmo =
            currentWeapon.magazineSize;

        OnAmmoChanged?.Invoke(
            currentAmmo,
            MaxAmmo);

        isReloading = false;

        OnReloadProgress?.Invoke(1f);

        OnReloadFinished?.Invoke();

        Debug.Log("Reload Complete");
    }

    private void HandleProjectileHit(
        Vector3 hitPosition)
    {
        GameObject impact =
            impactPool.Get();

        impact.transform.position =
            hitPosition;

        impact.transform.rotation =
            Quaternion.identity;

        VFXAutoReturn autoReturn =
            impact.GetComponent<VFXAutoReturn>();

        if (autoReturn != null)
        {
            autoReturn.Initialize(
                impactPool);
        }
    }
}