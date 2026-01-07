using UnityEngine;

public class SinkObject : MonoBehaviour
{
    bool _sinkObject;

    Collider _objectCollider;

    private void Start()
    {
        _objectCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (_sinkObject)
        {
            Sink();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _sinkObject = true;
        }
    }

    void Sink()
    {
        transform.Translate(Vector3.down * 0.2f * Time.deltaTime, Space.Self);

        if(transform.position.y <= -1)
        {
            Destroy(gameObject);
        }
    }
}
