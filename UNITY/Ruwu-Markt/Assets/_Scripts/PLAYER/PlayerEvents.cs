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


    public event Action<bool> onCameraLock;

    public void CameraLock(bool toggle)
    {
        if(onCameraLock != null)
        {
            onCameraLock(toggle);
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
}
