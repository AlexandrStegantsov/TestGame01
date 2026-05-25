using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider sensitivitySlider;

    [Header("References")]
    [SerializeField] private PlayerLook playerLook;

    private ISaveService saveService;

   

    private void Start()
    {
        saveService =
            ServiceLocator.Get<ISaveService>();

        saveService.Load();

        sensitivitySlider.value =
            saveService.Data.sensitivity;
    
    }

    
    private void OnSensitivityChanged(
        float value)
    {
        saveService.Data.sensitivity = value;

        if (playerLook != null)
        {
            playerLook.SetSensitivity(
                value);
        }

    }

    public void OnSavePressed()
    {
        saveService.Data.sensitivity =
            sensitivitySlider.value ;

        saveService.Save();

      
    }
}