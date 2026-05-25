using System.Collections.Generic;
using UnityEngine;

public class AchievementService :
    IAchievementService
{
    private readonly Dictionary<string,
        AchievementData> achievements =
            new();

    private readonly ISaveService
        saveService;

    private int currentKills;

    public AchievementService()
    {
        saveService =
            ServiceLocator.Get<ISaveService>();

        CreateAchievements();

        LoadUnlockedAchievements();
    }

    private void CreateAchievements()
    {
        AddAchievement(
            "FIRST_BLOOD",
            "First Blood");

        AddAchievement(
            "AREA_CLEARED",
            "Area Cleared");

        AddAchievement(
            "WAR_MACHINE",
            "War Machine");

        AddAchievement(
            "UNSTOPPABLE",
            "Unstoppable");
    }

    private void AddAchievement(
        string id,
        string title)
    {
        achievements[id] =
            new AchievementData
            {
                id = id,
                title = title,
                unlocked = false
            };
    }

    private void LoadUnlockedAchievements()
    {
        foreach (string id in
                 saveService.Data
                     .unlockedAchievements)
        {
            if (achievements.ContainsKey(id))
            {
                achievements[id].unlocked =
                    true;
            }
        }
    }

    public void RegisterKill()
    {
        currentKills++;

        if (currentKills >= 1)
        {
            Unlock(
                "FIRST_BLOOD");
        }

        if (currentKills >= 5)
        {
            Unlock(
                "AREA_CLEARED");
        }

        if (currentKills >= 10)
        {
            Unlock(
                "WAR_MACHINE");
        }

        if (currentKills >= 20)
        {
            Unlock(
                "UNSTOPPABLE");
        }
    }

    private void Unlock(string id)
    {
        if (!achievements.ContainsKey(id))
            return;

        AchievementData achievement =
            achievements[id];

        if (achievement.unlocked)
            return;

        achievement.unlocked = true;

        if (!saveService.Data
                .unlockedAchievements
                .Contains(id))
        {
            saveService.Data
                .unlockedAchievements
                .Add(id);

            saveService.Save();
        }

        Debug.Log(
            $"Achievement Unlocked: {achievement.title}");

        IPlatformService platformService =
            ServiceLocator.Get<IPlatformService>();

        platformService.UnlockAchievement(id);
    }

    public bool IsUnlocked(string id)
    {
        if (!achievements.ContainsKey(id))
            return false;

        return achievements[id].unlocked;
    }
}