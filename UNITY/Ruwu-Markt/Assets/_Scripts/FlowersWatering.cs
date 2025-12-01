using UnityEngine;
using System;
using TMPro;

public class FlowersWatering : MonoBehaviour
{
    public static event Action OnFlowerWatered;

    public Material DryMaterial;

    public Material Wetmaterial;

    public float saturationChange;

    public float maxSaturation;

    private Renderer meshRenderer;

    private TMP_Text saturationMeter;

    private float waterSaturation;

    private bool isSaturated;




    private void Start()
    {
        meshRenderer = GetComponent<Renderer>();

        saturationMeter = GetComponentInChildren<TMP_Text>();

        waterSaturation = 0;
    }

    void TriggerWaterEvent()
    {
        OnFlowerWatered?.Invoke();
        if(meshRenderer != null)
        {
            meshRenderer.material = Wetmaterial;
        }

    }

    public void AddWaterSaturation()
    {
        if (!isSaturated)
        {
            waterSaturation += saturationChange * Time.deltaTime;
            saturationMeter.text = $"{Mathf.FloorToInt(waterSaturation)}";
            if (waterSaturation >= maxSaturation)
            {
                isSaturated = true;
                TriggerWaterEvent();
            }
        }     
    }
}
