// UIInputModeHandler.cs

using UnityEngine;

public class UIInputModeHandler : MonoBehaviour
{
    private IInputService inputService;

    private IWindowService windowService;

    private ICursorService cursorService;

    private void Start()
    {
        inputService =
            ServiceLocator.Get<IInputService>();

        windowService =
            ServiceLocator.Get<IWindowService>();

        cursorService =
            ServiceLocator.Get<ICursorService>();

        InputSystemService input =
            inputService as InputSystemService;

        input.OnInputSchemeChanged +=
            OnInputSchemeChanged;
    }

    private void OnDestroy()
    {
        InputSystemService input =
            inputService as InputSystemService;

        if (input != null)
        {
            input.OnInputSchemeChanged -=
                OnInputSchemeChanged;
        }
    }

    private void OnInputSchemeChanged(
        bool isGamepad)
    {
        if (windowService.CurrentWindow == null)
            return;

        cursorService.SetUIMode(
            isGamepad);

        if (isGamepad)
        {
            windowService.SelectTopWindow();
        }
    }
}