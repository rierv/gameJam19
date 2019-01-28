public interface IInteractable
{
    void Interact();
    InteractionMode InteractionMode { get; }
}