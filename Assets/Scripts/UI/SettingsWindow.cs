using UnityEngine;

public class SettingsWindow : UIWindow
{
    private IWindowService windowService;

    private void Awake()
    {
        Hide();
    }
    private void Start()
    {
        windowService =
            ServiceLocator.Get<IWindowService>();
    }

    public void OnBackPressed()
    {
        windowService.CloseTop();
    }
}