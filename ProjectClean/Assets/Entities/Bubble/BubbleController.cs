using KinematicCharacterController.Examples;
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
    private Vector3 floatForce = new Vector3(0,1,0);
    [SerializeField]
    private float escapeTime = 3;
    [SerializeField]
    private Vector3 captureScale = new Vector3(3, 3, 3);
    [SerializeField]
    private LayerMask excludeWhileCaptureLayerMask;

    [Header("Pop Settings")]
    [SerializeField]
    private float popForce;

    private Coroutine travelCoroutine;
    private Coroutine escapeCoroutine;
    private SphereCollider sphereCollider;
    private Rigidbody rigidBody;

    private GameObject capturedGameObject;
    private Rigidbody capturedRigidbody;
    private CapsuleCollider capturedCollider;

    

    private void Start()
    {
        travelCoroutine = StartCoroutine(TravelCoroutine());
        sphereCollider = GetComponent<SphereCollider>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (capturedGameObject == null)
        {
            EnemyController enemyController = collision.gameObject.GetComponentInChildren<EnemyController>();
            if (enemyController != null)
            {
                Capture(collision.gameObject);
                return;
            }
        }
        else
        {
            ExampleCharacterController characterController = collision.gameObject.GetComponentInChildren<ExampleCharacterController>();
            if (characterController != null)
            {
                if (characterController.gameObject.transform.position.y > this.gameObject.transform.position.y)
                {
                    Pop(true);
                }
                else
                {
                    Pop();
                    Vector3 forceDirection = Vector3.Normalize(sphereCollider.transform.position - characterController.transform.position);
                    capturedRigidbody.AddForce(popForce * forceDirection);
                }

                return;
            }
        }
        if (collision.transform.gameObject.CompareTag("heal"))
        {
            GameManager.Instance.InvokePlayerHeal(.25f);
        }

        Pop();
    }

    public void Capture(GameObject capturedGameObject)
    {
        if (this.capturedGameObject == null)
        {
            capturedRigidbody = capturedGameObject.GetComponent<Rigidbody>();
            this.capturedGameObject = capturedGameObject;
            sphereCollider.excludeLayers = excludeWhileCaptureLayerMask;
        }
        else
        {
            return;
        }

        capturedCollider = capturedGameObject.GetComponentInChildren<CapsuleCollider>();
        if (capturedCollider == null)
        {
            transform.position = capturedGameObject.transform.position;
        }
        else
        {
            capturedCollider.enabled = false;
            transform.position = capturedCollider.transform.position;
        }

        transform.localScale = captureScale;
        capturedRigidbody.isKinematic = true;
        capturedGameObject.transform.parent = transform;

        StopCoroutine(travelCoroutine);
        escapeCoroutine = StartCoroutine(EscapeCoroutine());
        rigidBody.AddForce(floatForce);
    }

    public void Pop(bool isEnemyDestroyed = false)
    {
        if (capturedGameObject)
        {
            if (isEnemyDestroyed)
            {
                Destroy(capturedGameObject);
            }
            else
            {
                capturedRigidbody.isKinematic = false;
                capturedRigidbody.useGravity = true;
                capturedCollider.enabled = true;
                capturedGameObject.transform.parent = null;
            }
        }

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

    IEnumerator EscapeCoroutine()
    {
        
        yield return new WaitForSeconds(escapeTime);
        if (capturedGameObject != null)
        {
            Pop(false);
        }
    }
}
