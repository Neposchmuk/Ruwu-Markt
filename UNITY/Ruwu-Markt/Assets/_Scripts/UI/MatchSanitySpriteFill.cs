using UnityEngine;
using UnityEngine.UI;

public class MatchSanitySpriteFill : MonoBehaviour
{
    enum barType
    {
        SANITY,
        JOB_SECURITY
    }

    public float fillSpeed = 1;

    float fillAmount;

    Sanity_Manager SM;

    [SerializeField] barType type;

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
        GameEventsManager.instance.gameEvents.onSendSanityUpdate += AdjustSanityFill;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.gameEvents.onSendSanityUpdate -= AdjustSanityFill;
    }

    public void AdjustSanityFill(int sanityValue, int jobSecurityValue)
    {

        switch (type)
        {
            case barType.SANITY:
            Debug.Log((float)sanityValue/100);

            fillAmount = Mathf.Lerp(0.15f, 1f, (float)sanityValue/100);

            Debug.Log(fillAmount);


            sprite.fillAmount = fillAmount;
            break;

            case barType.JOB_SECURITY:
            Debug.Log((float)jobSecurityValue/100);

            fillAmount = Mathf.Lerp(0.15f, 1f, (float)jobSecurityValue/100);

            Debug.Log(fillAmount);


            sprite.fillAmount = fillAmount;
            break;
        }
        
    }
}
