using UnityEngine;

public abstract class MiniGameBaseState
{
    public abstract void StartQuest(MiniGameStateManager MiniGameManager, int questVariant);

    public abstract void UpdateQuest();

    public abstract void EndQuest();
}
