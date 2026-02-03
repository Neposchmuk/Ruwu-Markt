using UnityEngine;
using UnityEngine.UI;

public class MatchSanitySpriteFill : MonoBehaviour
{
    public float fillSpeed = 1;

    float fillAmount;

    Sanity_Manager SM;

    [SerializeField]Image sprite;

    private float sanityFloat;

    private float jobSecurityFloat;

    private float sanityPreChange;

    private float jobSecPreChange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*void Start()
    {
        SM = GameObject.FindFirstObjectByType<Sanity_Manager>();

        sprite = GetComponent<Image>();

        sanityFloat = SM.sanity;

        jobSecurityFloat = SM.jobSecurity;

        sanityPreChange = sanityFloat;

        jobSecPreChange = jobSecurityFloat;
    }*/

    private void OnEnable()
    {
        GameEventsManager.instance.gameEvents.onUpdateSanity += AdjustSanityFill;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.gameEvents.onUpdateSanity -= AdjustSanityFill;
    }

    public void AdjustSanityFill(int givenValue)
    {
        Debug.Log((float)givenValue/100);

        fillAmount = Mathf.Lerp(0.15f, 1f, (float)givenValue/100);

        Debug.Log(fillAmount);


        sprite.fillAmount = fillAmount;
    }
}
