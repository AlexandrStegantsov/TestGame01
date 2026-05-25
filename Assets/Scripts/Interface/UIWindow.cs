using UnityEngine;
using UnityEngine.UI;

public class UIWindow : MonoBehaviour
{
    [SerializeField] private GameObject root;

    [SerializeField] private Selectable defaultSelectable;

    public virtual void Hide()
    {
     

        root.SetActive(false);
    }

    public virtual void Show()
    {
       

        root.SetActive(true);
    }

    public Selectable GetDefaultSelectable()
    {
        return defaultSelectable;
    }
}