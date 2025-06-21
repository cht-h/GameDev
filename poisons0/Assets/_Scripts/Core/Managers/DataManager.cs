using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private string savePath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Path.Combine(Application.persistentDataPath, "save.json");
    }

    public void SaveGame(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    public GameData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<GameData>(json);
        }
        return new GameData();
    }

    private List<string> GetUnlockedIngredients()
    {
        return new List<string>();
    }
}

[System.Serializable]
public class GameData
{
    public List<string> unlockedIngredients = new List<string>();
    public int playerGold;
    public int reputation;
}