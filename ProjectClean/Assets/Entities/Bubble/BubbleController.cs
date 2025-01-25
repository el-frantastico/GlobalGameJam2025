using UnityEngine;

public class BubbleController : MonoBehaviour
{
    [SerializeField]
    private float speed = 50f;

    [SerializeField]
    private float lifetime = 1f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLocation(Time.deltaTime);
    }

    void UpdateLocation(float deltaTime)
    {
        float distance = speed * deltaTime;
        transform.position = transform.position + transform.right * distance;
    }
}
