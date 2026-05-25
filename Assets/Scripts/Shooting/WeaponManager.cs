using System;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponController[] weapons;

    private IInputService inputService;

    private int currentIndex;

    public event Action<WeaponController>
        OnWeaponChanged;

    public WeaponController CurrentWeapon =>
        weapons[currentIndex];

    private void Awake()
    {
        inputService =
            ServiceLocator.Get<IInputService>();
    }

    private void OnEnable()
    {
        if (inputService != null)
        {
            inputService.OnNextWeapon +=
                NextWeapon;

            inputService.OnPreviousWeapon +=
                PreviousWeapon;
        }
    }

    private void OnDisable()
    {
        if (inputService != null)
        {
            inputService.OnNextWeapon -=
                NextWeapon;

            inputService.OnPreviousWeapon -=
                PreviousWeapon;
        }
    }

    private void Start()
    {
        Equip(0);
    }

    public void Equip(int index)
    {
        if (index < 0 || index >= weapons.Length)
            return;

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(
                i == index);
        }

        currentIndex = index;

        OnWeaponChanged?.Invoke(
            CurrentWeapon);
    }

    private void NextWeapon()
    {
        int nextIndex =
            currentIndex + 1;

        if (nextIndex >= weapons.Length)
        {
            nextIndex = 0;
        }

        Equip(nextIndex);
    }

    private void PreviousWeapon()
    {
        int previousIndex =
            currentIndex - 1;

        if (previousIndex < 0)
        {
            previousIndex =
                weapons.Length - 1;
        }

        Equip(previousIndex);
    }
}