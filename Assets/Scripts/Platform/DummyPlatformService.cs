using UnityEngine;

public class DummyPlatformService :
    IPlatformService
{
    public void UnlockAchievement(
        string id)
    {
        Debug.Log(
            $"Platform Unlock Achievement: {id}");
    }
}