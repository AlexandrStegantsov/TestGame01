using UnityEngine;
using UnityEngine.EventSystems;

public class UISelectionService
{
    public void Select(GameObject target)
    {
        EventSystem.current.SetSelectedGameObject(
            target);
    }

    public void ClearSelection()
    {
        EventSystem.current.SetSelectedGameObject(
            null);
    }

    public void RestoreSelection()
    {
        if (UIFocusHandler.LastHoveredSelectable == null)
            return;

        EventSystem.current.SetSelectedGameObject(
            UIFocusHandler
                .LastHoveredSelectable
                .gameObject);
    }
}