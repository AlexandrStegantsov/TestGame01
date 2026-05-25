using UnityEngine;

[CreateAssetMenu(
    menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Stats")]
    public float damage = 10f;
    public float fireRate = 0.2f;
    public float reloadTime = 1.5f;
    public bool automatic;
    [Header("Ammo")]
    public int magazineSize = 30;

    [Header("Projectile")]
    public Projectile projectilePrefab;

    public float projectileSpeed = 30f;
}