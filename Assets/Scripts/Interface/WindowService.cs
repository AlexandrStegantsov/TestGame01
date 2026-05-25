

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowService : IWindowService
{
    private readonly Stack<UIWindow>
        windowStack = new();

    private readonly ICursorService
        cursorService;

    private readonly IInputService
        inputService;

    public UIWindow CurrentWindow =>
        windowStack.Count > 0
            ? windowStack.Peek()
            : null;

    public WindowService()
    {
        cursorService =
            ServiceLocator.Get<ICursorService>();

        inputService =
            ServiceLocator.Get<IInputService>();
    }

    public void Open(UIWindow window)
    {
        if (windowStack.Count > 0)
        {
            UIWindow current =
                windowStack.Peek();

            current.Hide();
        }

        windowStack.Push(window);

        window.Show();

        cursorService.SetUIMode(
            inputService.IsGamepad);

        SelectTopWindow();

        Debug.Log(
            $"Open {window.name}");
    }

    public void CloseTop()
    {
        if (windowStack.Count == 0)
            return;

        UIWindow closedWindow =
            windowStack.Pop();

        closedWindow.Hide();

        Debug.Log(
            $"Close {closedWindow.name}");

        if (windowStack.Count > 0)
        {
            UIWindow topWindow =
                windowStack.Peek();

            topWindow.Show();

            cursorService.SetUIMode(
                inputService.IsGamepad);

            SelectTopWindow();

            Debug.Log(
                $"Return to {topWindow.name}");
        }
        else
        {
            cursorService.SetGameplayMode();
        }
    }

    public void SelectTopWindow()
    {
        if (CurrentWindow == null)
            return;

        Selectable selectable =
            CurrentWindow.GetDefaultSelectable();

        if (selectable == null)
            return;

        EventSystem.current.SetSelectedGameObject(
            selectable.gameObject);
    }
}