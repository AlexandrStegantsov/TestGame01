using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    [Header("Achievement")]
    [SerializeField] private string achievementId;

    [Header("UI")]
    [SerializeField] private Image icon;

    [SerializeField] private GameObject lockImage;

    [SerializeField] private TMP_Text nameText;

    [SerializeField] private TMP_Text descriptionText;

    private IAchievementService
        achievementService;

    private void Start()
    {
        achievementService =
            ServiceLocator.Get<IAchievementService>();

        Refresh();
    }

    public void Refresh()
    {
        bool unlocked =
            achievementService
                .IsUnlocked(
                    achievementId);

        icon.gameObject.SetActive(
            unlocked);

        lockImage.SetActive(
            !unlocked);
    }
}