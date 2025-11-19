using UnityEngine;

public abstract class MiniGameBaseState
{
    public abstract void StartQuest(MiniGame_Caller MiniGame_Caller, int questVariant);

    public abstract void InitiateQuest();

    public abstract void UpdateQuest();

    public abstract void EndQuest();

    public abstract void Interact();

    public abstract void HoldingAttack(bool buttonIsPressed);
}
