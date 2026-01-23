using Cinemachine.Utility;
using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerEvents
{
    public InputEventContext inputContext { get; private set; } = InputEventContext.DEFAULT;

    public void ChangeInputEventContext (InputEventContext newContext)
    {
        inputContext = newContext;
    }

    public QuestContext questContext { get; private set; } = QuestContext.NONE;

    public void ChangeQuestContext (QuestContext newContext)
    {
        questContext = newContext;
    }


    public event Action<bool> onMovementLock;

    public void LockPlayerMovement(bool toggle)
    {
        if(onMovementLock != null)
        {
            onMovementLock(toggle);
        }
    }


    public event Action<bool> onLockCamera;

    public void LockCamera(bool toggle)
    {
        if(onLockCamera != null)
        {
            onLockCamera(toggle);
        }
    }



    public event Action<InputEventContext> onPressedInteract;

    public void PressedInteract()
    {
        if(onPressedInteract != null)
        {
            onPressedInteract(inputContext);
        }
    }

    public event Action<bool> onToggleJump;

    public void ToggleJump(bool toggle)
    {
        if(onToggleJump != null)
        {
            onToggleJump(toggle);
        }
    }

    public event Action<InputEventContext> onPressedEscape;

    public void PressedEscape()
    {
        if(onPressedEscape != null)
        {
            onPressedEscape(inputContext);
        }
    }

    public event Action<InputEventContext> onPressedSpecialPrimary;

    public void PressedSpecialPrimary()
    {
        if(onPressedSpecialPrimary != null)
        {
            onPressedSpecialPrimary(inputContext);
        }
    }

    public event Action<InputEventContext> onPressedAttack;

    public void PressedAttack()
    {
        if(onPressedAttack != null)
        {
            onPressedAttack(inputContext);
        }
    }

    public event Action<InputEventContext, QuestContext> onPressedAttackQuest;

    public void PressedAttackQuest()
    {
        if(onPressedAttackQuest != null)
        {
            onPressedAttackQuest(inputContext, questContext);
        }
    }

    public event Action<InputEventContext> onReleasedAttack;

    public void ReleasedAttack()
    {
        if(onReleasedAttack != null)
        {
            onReleasedAttack(inputContext);
        }
    }

    public event  Action<InputEventContext> onPressedSpecialSecondary;

    public void PressedSpecialSecondary()
    {
        if(onPressedSpecialSecondary != null)
        {
            onPressedSpecialSecondary(inputContext);
        }
    }
}
