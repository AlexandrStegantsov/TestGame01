using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapInstaller : MonoBehaviour
{
    [Header("Services")]
    [SerializeField] private InputSystemService inputSystemService;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        RegisterServices();

        SceneManager.LoadSceneAsync(1);
    }

    private void RegisterServices()
    {
        ServiceLocator.Register<IInputService>(
            inputSystemService);

        ServiceLocator.Register<ICursorService>(
            new CursorService());

        ServiceLocator.Register<IWindowService>(
            new WindowService());

        ServiceLocator.Register<IPlatformService>(
            new DummyPlatformService());

        ISaveBackend saveBackend =
            new JsonFileBackend();

        ServiceLocator.Register<ISaveService>(
            new SaveService(saveBackend));

        ServiceLocator.Register<IAchievementService>(
            new AchievementService());
    }
}