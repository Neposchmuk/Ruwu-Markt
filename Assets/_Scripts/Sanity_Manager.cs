using TMPro;
using UnityEngine;

public class Sanity_Manager : MonoBehaviour
{
    public TMP_Text sanityCounter;

    public TMP_Text jobSecCounter;

    public int sanity;

    private int jobSecurity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sanity = Mathf.Clamp(50, 0, 100);

        jobSecurity = Mathf.Clamp(50, 0, 100);
    }

    private void Update()
    {
        sanityCounter.text = $"{sanity}";

        jobSecCounter.text = $"{jobSecurity}";
    }

    public void ChangeSanity(int addSanity, int addJobSecurity)
    {
        sanity = Mathf.Clamp(sanity + addSanity, 0, 100);

        jobSecurity = Mathf.Clamp(jobSecurity + addJobSecurity, 0, 100);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
