public interface ISaveBackend
{
    void Save(string data);

    string Load();

    bool Exists();
}