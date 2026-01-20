using System;
using UnityEngine;

public class NPC_Events
{
    public event Action<GameObject, Vector3> onFacePlayer;

    public void FacePlayer(GameObject agent, Vector3 playerPosition)
    {
        if(onFacePlayer != null)
        {
            onFacePlayer(agent, playerPosition);
        }
    }

    public event Action<GameObject> onPingPlayerPosition;

    public void PingPlayerPosition(GameObject agent)
    {
        if(onPingPlayerPosition != null)
        {
            onPingPlayerPosition(agent);
        }
    }

    public event Action<GameObject, Quaternion> onFaceDirection;

    public void FaceDirection(GameObject agent, Quaternion faceDirection)
    {
        if(onFaceDirection != null)
        {
            onFaceDirection(agent, faceDirection);
        }
    }

    public event Action<GameObject> onResetFace;

    public void ResetFace(GameObject agent)
    {
        if(onResetFace != null)
        {
            onResetFace(agent);
        }
    }
}