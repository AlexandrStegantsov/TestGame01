public interface ISaveService
{
    SaveData Data { get; }

    void Save();

    void Load();
}