using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameData CurrentGameData { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        CurrentGameData = DataManager.Instance.LoadGame();
    }

    public void AddMoney(int amount)
    {
        CurrentGameData.playerGold += amount;
        UIManager.Instance.UpdateMoneyUI(CurrentGameData.playerGold);
    }

    public void AddReputation(int amount)
    {
        CurrentGameData.reputation += amount;
        UIManager.Instance.UpdateReputationUI(CurrentGameData.reputation);
    }

    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveGame(CurrentGameData);
    }
}