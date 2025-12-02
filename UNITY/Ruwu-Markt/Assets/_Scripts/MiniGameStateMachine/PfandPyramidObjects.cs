using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PfandPyramidObjects : MonoBehaviour
{
    public List<GameObject> pyramidCrates;

    public List<GameObject> placingZones;

    public GameObject pyramidZone;

    private void Start()
    {
        /*foreach(GameObject pyramidCrate in pyramidCrates)
        {
            pyramidCrate.SetActive(false);
        }*/

        foreach (GameObject placeZone in placingZones)
        {
            placeZone.SetActive(false);
        }

        pyramidZone.SetActive(false);
    }
}
