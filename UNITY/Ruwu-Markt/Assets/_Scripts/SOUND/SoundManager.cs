using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundLibrary library;

    private void OnEnable()
    {
        GameEventsManager.instance.soundEvents.onTriggerSound += SendAudioClip;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.soundEvents.onTriggerSound -= SendAudioClip;
    }


    private void SendAudioClip(SoundType sound)
    {
        switch (sound)
        {
            case SoundType.PLACE_PRODUCT:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Place_Product);
                break;
            case SoundType.PLACE_BOTTLE:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Place_Bottle);
                break;
            case SoundType.PLACE_CRATE:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Place_Crate);
                break;
            case SoundType.DRINK:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Drink);
                break;
            case SoundType.POUR:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Pour);
                break;
            case SoundType.MOP:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Mop);
                break;
            case SoundType.WATER:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Water);
                break;
            case SoundType.BOTTLE_THROW:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Bottle_Throw);
                break;
            case SoundType.BOTTLE_CRASH:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Bottle_Crash);
                break;
            case SoundType.PICKUP:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.PickUp);
                break;
            case SoundType.CASH_SCAN:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Cash_Scan);
                break;
            case SoundType.CASH_SPAWN:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Cash_Spawn);
                break;
            case SoundType.CASH_BUTTONS:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Cash_Buttons);
                break;
            case SoundType.CASH_CHANGE:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Cash_Change);
                break;
            case SoundType.CASH_OPEN:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Cash_Open);
                break;
            case SoundType.SAFE:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Safe);
                break;
            case SoundType.DOOR_OPEN:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Door_Open);
                break;
            case SoundType.DOOR_LOCKED:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Door_Locked);
                break;
            case SoundType.MATRESS:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Matress);
                break;
            case SoundType.PC_CLICK:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.PC_Click);
                break;
            case SoundType.BAT_SWOOSH:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Bat_Swoosh);
                break;
            case SoundType.BAT_HIT:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Bat_Hit);
                break;
            case SoundType.GUN_SHOOT:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Gun_Shoot);
                break;
            case SoundType.GUN_LOAD:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Gun_Load);
                break;
            case SoundType.PLAYER_HURT:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Player_Hurt);
                break;
            case SoundType.NPC_DEATH:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.NPC_Death);
                break;
            case SoundType.FLASHLIGHT:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Flashlight);
                break;
            case SoundType.MARKET_DOOR_CLOSED:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Market_Door_Closed);
                break;
            case SoundType.MARKET_DOOR_OPEN:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.Market_Door_Open);
                break;
            case SoundType.UI_CLICK:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.UI_Click);
                break;
            case SoundType.UI_OPEN:
                GameEventsManager.instance.soundEvents.SendAudioClip(library.UI_Open);
                break;
        }
    }
}
