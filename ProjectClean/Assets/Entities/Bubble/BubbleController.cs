using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 1f;

    [Header("Trajectory Settings")]
    [SerializeField]
    private float travelSpeed = 50f;

    [Header("Capture Settings")]
    [SerializeField]
    private float floatSpeed = 5;
    [SerializeField]
    private float escapeTime = 3;
   
    private Coroutine travelCoroutine;
    private Coroutine floatCoroutine;
    private Coroutine escapeCoroutine;
    private GameObject capturedGameObject;

    private void Start()
    {
        travelCoroutine = StartCoroutine(TravelCoroutine());
    }

    public void Capture(GameObject capturedGameObject)
    {
        if (this.capturedGameObject == null)
        {
            this.capturedGameObject = capturedGameObject;
        }
        else
        {
            return;
        }

        transform.position = capturedGameObject.transform.position;
        capturedGameObject.transform.parent = transform;
        StopCoroutine(travelCoroutine);
        floatCoroutine = StartCoroutine(FloatCoroutine());
        escapeCoroutine = StartCoroutine(EscapeCoroutine());
    }

    public void Escape()
    {
        if (capturedGameObject == null)
        {
            return;
        }

        Rigidbody rigidBody = capturedGameObject.GetComponent<Rigidbody>();
        rigidBody.useGravity = true;

        StopCoroutine(floatCoroutine);
        StopCoroutine(escapeCoroutine);

        Pop(false);
        
    }

    public void Pop(bool isEnemyDestroyed)
    {
        if (isEnemyDestroyed && capturedGameObject != null)
        {
            Destroy(capturedGameObject);
        }

        capturedGameObject.transform.parent = null;
        Destroy(gameObject);
    }


    IEnumerator TravelCoroutine()
    {
        while(true)
        {
            float distance = travelSpeed * Time.deltaTime;
            transform.position = transform.position + transform.right * distance;
            yield return null;
        }
    }

    IEnumerator FloatCoroutine()
    {
        Rigidbody rigidBody = capturedGameObject.GetComponent<Rigidbody>();
        rigidBody.useGravity = false;

        while (true)
        {
            float distance = floatSpeed * Time.deltaTime;
            transform.position = transform.position + transform.up * distance;
            yield return null;
        }
    }

    IEnumerator EscapeCoroutine()
    {
        
        yield return new WaitForSeconds(escapeTime);
        Escape();
    }
}
