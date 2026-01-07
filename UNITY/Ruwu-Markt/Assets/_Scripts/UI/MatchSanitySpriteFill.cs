using UnityEngine;
using UnityEngine.UI;

public class MatchSanitySpriteFill : MonoBehaviour
{
    public float fillSpeed = 1;

    Sanity_Manager SM;

    Image sprite;

    private float sanityFloat;

    private float jobSecurityFloat;

    private float sanityPreChange;

    private float jobSecPreChange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SM = GameObject.FindFirstObjectByType<Sanity_Manager>();

        sprite = GetComponent<Image>();

        sanityFloat = SM.sanity;

        jobSecurityFloat = SM.jobSecurity;

        sanityPreChange = sanityFloat;

        jobSecPreChange = jobSecurityFloat;
    }

    public void AdjustSanityFill(int givenValue)
    {
        sprite.fillAmount = givenValue / 100f;
    }
}
