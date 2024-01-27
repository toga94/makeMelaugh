

public interface IInteractable 
{
    bool IsInteractionAllowed();
    void BeginInteract();
    void Interact();
    void EndInteract();
}
