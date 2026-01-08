using UnityEngine;
using UnityEngine.UI;

public class SanitySpriteChanger : MonoBehaviour
{
    public Sprite[] sprites;

    [SerializeField] private GameObject widgetParent;

    private Image Image;


    private void Awake()
    {
        GameEventsManager.instance.gameEvents.onToggleSanityWidget += ToggleWidgetParent;
        GameEventsManager.instance.gameEvents.onUpdateSanity += CheckSanitySprite;
        Debug.Log("Added Listeners");
    }

    private void OnDisable()
    {
        widgetParent.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.gameEvents.onToggleSanityWidget -= ToggleWidgetParent;
        GameEventsManager.instance.gameEvents.onUpdateSanity -= CheckSanitySprite;
    }

    private void Start()
    {
        Image = GetComponent<Image>();

        widgetParent.SetActive(false);
    }

    public void CheckSanitySprite(int sanity)
    {
        if(sanity >= 75)
        {
            Image.sprite = sprites[0];
        }
        else if(sanity >=50 && sanity < 75)
        {
            Image.sprite = sprites[1];
        }
        else if (sanity >= 25 && sanity < 50)
        {
            Image.sprite = sprites[2];
        }
        else
        {
            Image.sprite = sprites[3];
        }
    }

    private void ToggleWidgetParent(bool toggle)
    {
        if (widgetParent.activeSelf == true) return;

        widgetParent.SetActive(toggle);
    }
}
