using System;
using UnityEngine;
using System.Collections;
using TMPro;

public class Player_Gun : MonoBehaviour
{
    public GameObject BarrelLeft;

    public GameObject BarrelRight;

    public GameObject Projectile;

    public TMP_Text AmmoText;

    public AnimationClip ReloadAnimation;

    public ParticleSystem ParticlesLeft;

    public ParticleSystem ParticlesRight;

    public int MaxAmmo { get; private set; }

    public int AmmoCarrying { get; private set; }

    public int AmmoFromStation = 8;

    int _ammoInMagazine;

    bool _weaponLoaded;

    Animator _animator;

    private void Start()
    {
        gameObject.SetActive(false);

        _animator = GetComponent<Animator>();

        AmmoStation.OnAmmoPickup += PickUpAmmoStation;

        MaxAmmo = 30;

        AmmoCarrying = 10;

        _ammoInMagazine = 3;

        _weaponLoaded = true;

        AmmoText.text = $"{AmmoCarrying}" + "\n" + $"{_ammoInMagazine}";
    }

    public void Fire()
    {
        if (!_weaponLoaded || AmmoCarrying < 1)
        {
            Debug.Log("returned");
            return;
        }
        
        GameObject _projectileLeft = Instantiate(Projectile, BarrelLeft.transform.position, BarrelLeft.transform.rotation);
        GameObject _projectileRight = Instantiate(Projectile, BarrelRight.transform.position, BarrelRight.transform.rotation);

        ParticlesLeft.Emit(20);
        ParticlesRight.Emit(20);

        try
        {
           _projectileLeft.GetComponent<Rigidbody>().AddForce(transform.right.normalized * 10, ForceMode.Impulse);
           _projectileRight.GetComponent<Rigidbody>().AddForce(transform.right.normalized * 10, ForceMode.Impulse);
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Projectile is missing Rigidbody");
        }

        AmmoCarrying --;

        _ammoInMagazine--;

        AmmoText.text = $"{AmmoCarrying}" + "\n" + $"{_ammoInMagazine}";

        if(_ammoInMagazine <= 0)
        {
            _weaponLoaded = false;

            StartReload();
        }        
    }

    public void PickUpAmmoStation()
    {
        if (AmmoCarrying == MaxAmmo) return;
        else
        {
            if (AmmoCarrying + AmmoFromStation >= MaxAmmo)
            {
                AmmoCarrying += MaxAmmo - AmmoCarrying;
            }
            else AmmoCarrying += AmmoFromStation;

            _ammoInMagazine = 3;

            AmmoText.text = $"{AmmoCarrying}" + "\n" + $"{_ammoInMagazine}";
        }
    }

    public void StartReload()
    {
        StartCoroutine(Reload());
    }

    public void UnsubscribeEvents()
    {
        AmmoStation.OnAmmoPickup -= PickUpAmmoStation;
    }

    IEnumerator Reload()
    {
        _animator.SetTrigger("Reloading");
        yield return new WaitForSeconds(ReloadAnimation.length + 0.25f);
        if (AmmoCarrying < 3)
        {
            _ammoInMagazine = AmmoCarrying;
        }
        else _ammoInMagazine = 3;
        
        _animator.ResetTrigger("Reloading");
        AmmoText.text = $"{AmmoCarrying}" + "\n" + $"{_ammoInMagazine}";
        _weaponLoaded = true;
    }
}
