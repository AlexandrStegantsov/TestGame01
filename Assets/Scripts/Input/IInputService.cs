using System;
using UnityEngine;

public interface IInputService
{
    Vector2 Move { get; }

    Vector2 Look { get; }

    bool IsGamepad { get; }

    bool IsAiming { get; }

    bool ShootHeld { get; }

    event Action OnJump;
    event Action OnShoot;
    event Action OnPause;
    event Action OnReload;

    event Action OnNextWeapon;
    event Action OnPreviousWeapon;

    event Action<bool> OnAimChanged;

    event Action<bool> OnInputSchemeChanged;

    void EnableGameplayInput();

    void EnableUIInput();
}