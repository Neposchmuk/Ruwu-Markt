using UnityEngine;
using UnityEngine.UI;

public class SanitySpriteChanger : MonoBehaviour
{
    public Sprite[] sprites;

    private Sanity_Manager SM;

    private Image Image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SM = GameObject.Find("Sanity_Manager").GetComponent<Sanity_Manager>();

        Image = GetComponent<Image>();
    }

    public void CheckSanitySprite()
    {
        if(SM.sanity >= 75)
        {
            Image.sprite = sprites[0];
        }
        else if(SM.sanity >=50 && SM.sanity < 75)
        {
            Image.sprite = sprites[1];
        }
        else if (SM.sanity >= 25 && SM.sanity < 50)
        {
            Image.sprite = sprites[2];
        }
        else
        {
            Image.sprite = sprites[3];
        }
    }
}
