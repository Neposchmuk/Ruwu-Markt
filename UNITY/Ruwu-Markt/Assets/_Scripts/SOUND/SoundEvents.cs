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

    public event Action onStopSound;

    public void StopSound()
    {
        if(onStopSound != null)
        {
            onStopSound();
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
    BOTTLE_THROW,
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
    MATRESS,
    PC_CLICK,
    BAT_SWOOSH,
    BAT_HIT,
    GUN_SHOOT,
    GUN_LOAD,
    PLAYER_HURT,
    NPC_DEATH,
    FLASHLIGHT,
    MARKET_DOOR_CLOSED,
    MARKET_DOOR_OPEN,

    UI_CLICK,
    UI_OPEN
}