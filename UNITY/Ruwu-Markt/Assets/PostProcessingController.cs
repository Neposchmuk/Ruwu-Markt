using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingController : MonoBehaviour
{
    [SerializeField] Volume volume;

    private void OnEnable()
    {
        GameEventsManager.instance.gameEvents.onUpdateSanity += ChangeVolumeWeight;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.gameEvents.onUpdateSanity -= ChangeVolumeWeight;
    }
    void Start()
    {
        GameEventsManager.instance.gameEvents.RequestSanityUpdate();
    }

    private void ChangeVolumeWeight(int sanity)
    {
        volume.weight = Mathf.Lerp(0, 1, (float)(100-sanity)/100);
    }

}
