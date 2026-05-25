using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuWindow : UIWindow
{
    [Header("Windows")]
    [SerializeField] private UIWindow settingsWindow;
    [SerializeField] private UIWindow achievementsWindow;

    private IWindowService windowService;

    private void Start()
    {
        windowService =
            ServiceLocator.Get<IWindowService>();
        windowService.Open(this);
        
    }

    public void OnNewGamePressed()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void OnContinuePressed()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void OnSettingsPressed()
    {
        windowService.Open(settingsWindow);
    }

    public void OnAchievementsPressed()
    {
        windowService.Open(achievementsWindow);
    }
}