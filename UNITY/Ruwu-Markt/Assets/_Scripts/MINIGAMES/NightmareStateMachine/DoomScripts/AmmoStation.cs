using UnityEngine;
using TMPro;
using System.Collections;
using System.Threading;
using System;

public class AmmoStation : MonoBehaviour
{
    public static Action OnAmmoPickup;

    public TMP_Text StatusText;

    public bool IsLocked { get; private set; }

    int _lockedTime = 20;

    int _lockedTimeLeft = 20;


    public void AmmoPicked()
    {
        OnAmmoPickup?.Invoke();

        IsLocked = true;

        Debug.Log(IsLocked);

        InvokeRepeating("CountAmmoCooldown", 0, 1);
    }

    void CountAmmoCooldown()
    {
        StatusText.text = $"{_lockedTimeLeft}";

        if (_lockedTimeLeft == 0)
        {
            IsLocked = false;

            CancelInvoke("CountAmmoCooldown");

            _lockedTimeLeft = _lockedTime;

            StatusText.text = "Ammo ready!";
            return;
        }

        _lockedTimeLeft--;   
    }
}
