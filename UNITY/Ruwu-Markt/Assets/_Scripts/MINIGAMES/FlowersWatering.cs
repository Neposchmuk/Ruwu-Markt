using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class FlowersWatering : MonoBehaviour
{
    public static event Action OnFlowerWatered;

    public Color WetColor;

    public float saturationChange;

    public float maxSaturation;

    [SerializeField] private Image saturationMeter;

    private Renderer meshRenderer;

    private float waterSaturation;

    private bool isSaturated;

    




    private void Start()
    {
        meshRenderer = GetComponent<Renderer>();

        waterSaturation = 0;
    }

    void TriggerWaterEvent()
    {
        OnFlowerWatered?.Invoke();
        saturationMeter.color = WetColor;
    }

    public void AddWaterSaturation()
    {
        if (!isSaturated)
        {
            waterSaturation += saturationChange * Time.deltaTime;
            saturationMeter.fillAmount = waterSaturation/100;
            if (waterSaturation >= maxSaturation)
            {
                isSaturated = true;
                GetComponentInChildren<Canvas>().enabled = false;
                TriggerWaterEvent();
            }
        }     
    }
}
