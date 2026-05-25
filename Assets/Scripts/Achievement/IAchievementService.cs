public interface IAchievementService
{
    void RegisterKill();

    bool IsUnlocked(string id);
}