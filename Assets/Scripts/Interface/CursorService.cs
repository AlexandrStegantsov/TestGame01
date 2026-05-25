using UnityEngine;

public class CursorService : ICursorService
{
    public void SetGameplayMode()
    {
        Cursor.visible = false;

        Cursor.lockState =
            CursorLockMode.Locked;
    }

    public void SetUIMode(bool isGamepad)
    {
        Cursor.visible = !isGamepad;

        Cursor.lockState =
            isGamepad
                ? CursorLockMode.Locked
                : CursorLockMode.None;
    }
}