using UnityEngine;

public class SaveService :
    ISaveService
{
    public SaveData Data { get; private set; }

    private readonly ISaveBackend
        saveBackend;

    public SaveService(
        ISaveBackend backend)
    {
        saveBackend = backend;

        Load();
    }

    public void Save()
    {
        string json =
            JsonUtility.ToJson(
                Data,
                true);

        saveBackend.Save(json);

        Debug.Log("Game Saved");
    }

    public void Load()
    {
        if (saveBackend.Exists())
        {
            string json =
                saveBackend.Load();

            Data =
                JsonUtility.FromJson<SaveData>(
                    json);

            Debug.Log("Save Loaded");
        }
        else
        {
            Data = new SaveData();

            Save();

            Debug.Log(
                "New Save Created");
        }
    }
}