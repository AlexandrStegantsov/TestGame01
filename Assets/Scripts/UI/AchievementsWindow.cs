using UnityEngine;

public class AchievementsWindow : UIWindow
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