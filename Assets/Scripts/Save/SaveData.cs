using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public float sensitivity = 1f;

    public int killedEnemies;

    public List<string> unlockedAchievements =
        new();
}