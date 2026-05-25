using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIFocusHandler :
    MonoBehaviour,
    IPointerEnterHandler
{
    private Selectable selectable;

    public static Selectable LastHoveredSelectable;

    private void Awake()
    {
        selectable = GetComponent<Selectable>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LastHoveredSelectable = selectable;
    }
}