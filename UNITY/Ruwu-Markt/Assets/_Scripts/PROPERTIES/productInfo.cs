using UnityEngine;

public class productInfo : MonoBehaviour
{
    //This Class stores information about products

    public string productName;

    public int price; [Tooltip("Price in cents")]

    public int PLU;

    public bool ageRestricted;

    public bool hasBeenScanned;
}
