using System;
using System.Runtime.CompilerServices;
using UnityEngine;
public class SoundEvents
{
    public event Action<SoundType> onTriggerSound;

    public void TriggerSound(SoundType sound)
    {
        if(onTriggerSound != null)
        {
            onTriggerSound(sound);
        }
    }

    public event Action<AudioClip> onSendAudioClip;

    public void SendAudioClip(AudioClip clip)
    {
        if(onSendAudioClip != null)
        {
            onSendAudioClip(clip);
        }
    }
}

public enum SoundType
{
    PLACE_PRODUCT,
    PLACE_BOTTLE,
    PLACE_CRATE,
    DRINK,
    POUR,
    MOP,
    WATER,
    BOTTLES_THROW,
    BOTTLE_CRASH,
    PICKUP,
    CASH_SCAN,
    CASH_SPAWN,
    CASH_BUTTONS,
    CASH_CHANGE,
    CASH_OPEN,
    SAFE,
    DOOR_OPEN,
    DOOR_LOCKED,
    BAT_SWOOSH,
    BAT_HIT,
    GUN_SHOOT,
    GUN_LOAD,
    AMMO,
    PLAYER_HURT,
    NPC_DEATH,
    FLASHLIGHT,
    MARKET_DOOR_CLOSED,
    MARKET_DOOR_OPEN,
}