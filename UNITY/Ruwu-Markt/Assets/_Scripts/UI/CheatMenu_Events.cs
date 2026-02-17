using System;
using UnityEngine;

public class CheatMenu_Events
{
    public event Action<bool> onToggleGamePause;

    public void ToggleGamePause(bool toggle)
    {
        if(onToggleGamePause != null)
        {
            onToggleGamePause(toggle);
        }
    }

    public event Action onSkipDay;

    public void SkipDay()
    {
        if(onSkipDay != null)
        {
            onSkipDay();
        }
    }

    public event Action<int, int> onChangeDayNight;

    public void ChangeDayNight(int day, int night)
    {
        if(onChangeDayNight != null)
        {
            onChangeDayNight(day, night);
        }
    }

    public event Action<bool> onTogglePlayerInvincible;

    public void TogglePlayerInvincible(bool toggle)
    {
        if(onTogglePlayerInvincible != null)
        {
            onTogglePlayerInvincible(toggle);
        }
    }

    public event Action<bool> onToggleIsDay;
    public void ToggleIsDay(bool toggle)
    {
        if(onToggleIsDay != null)
        {
            onToggleIsDay(toggle);
        }
    }

    public event Action onToggleCheatMenu;

    public void ToggleCheatMenu()
    {
        if(onToggleCheatMenu != null)
        {
            onToggleCheatMenu();
        }
    }
}
