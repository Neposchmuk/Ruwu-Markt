using UnityEngine;
using TMPro;
using System.Collections;
using System.Threading;
using System;
using UnityEngine.UI;

public class AmmoStation : MonoBehaviour
{
    [SerializeField] Image ammoImage;

    public static Action OnAmmoPickup;

    public TMP_Text StatusText;

    public bool IsLocked { get; private set; }

    int _lockedTime = 20;

    int _lockedTimeLeft = 20;

    void Start()
    {
        StatusText.enabled = false;
    }


    public void AmmoPicked()
    {
        OnAmmoPickup?.Invoke();

        IsLocked = true;

        Debug.Log(IsLocked);

        InvokeRepeating("CountAmmoCooldown", 0, 1);
    }

    void CountAmmoCooldown()
    {
        ammoImage.enabled = false;
        StatusText.enabled = true;
        StatusText.text = $"{_lockedTimeLeft}";

        if (_lockedTimeLeft == 0)
        {
            IsLocked = false;

            CancelInvoke("CountAmmoCooldown");

            _lockedTimeLeft = _lockedTime;

            StatusText.text = "";
            StatusText.enabled = false;
            ammoImage.enabled = true;
            return;
        }

        _lockedTimeLeft--;   
    }
}
