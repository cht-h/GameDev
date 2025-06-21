public interface IInteractable
{
    string InteractionPrompt { get; }
    void OnInteract(PlayerController player);
    void ShowPrompt();
    void HidePrompt();
}