using System.IO;
using UnityEngine;

public class JsonFileBackend :
    ISaveBackend
{
    private readonly string savePath;

    public JsonFileBackend()
    {
        savePath =
            Path.Combine(
                Application.persistentDataPath,
                "save.json");
    }

    public void Save(string data)
    {
        File.WriteAllText(
            savePath,
            data);

        Debug.Log(
            $"Save Write: {savePath}");
    }

    public string Load()
    {
        return File.ReadAllText(
            savePath);
    }

    public bool Exists()
    {
        return File.Exists(
            savePath);
    }
}