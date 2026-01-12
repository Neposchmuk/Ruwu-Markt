using Cinemachine.Utility;
using System;
using UnityEngine;

public class PlayerEvents
{
    public InputEventContext inputContext { get; private set; } = InputEventContext.DEFAULT;

    public void ChangeInputEventContext (InputEventContext newContext)
    {
        inputContext = newContext;
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
}
