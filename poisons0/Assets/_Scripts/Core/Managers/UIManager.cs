using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")]
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text reputationText;
    [SerializeField] private GameObject interactionPrompt;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateMoneyUI(int amount)
    {
        if (moneyText != null)
            moneyText.text = $"Gold: {amount}";
    }

    public void UpdateReputationUI(int amount)
    {
        if (reputationText != null)
            reputationText.text = $"Rep: {amount}";
    }

    public void ShowInteractionPrompt(bool show)
    {
        if (interactionPrompt != null)
            interactionPrompt.SetActive(show);
    }
}