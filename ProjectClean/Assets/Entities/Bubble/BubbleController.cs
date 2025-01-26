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
    private Vector3 floatForce = new Vector3(0, 1, 0);
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

    private EnemyController capturedEnemy;
    
    private void Start()
    {
        travelCoroutine = StartCoroutine(TravelCoroutine());
        sphereCollider = GetComponent<SphereCollider>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (capturedEnemy == null)
        {
            EnemyController enemyToCapture = collision.gameObject.GetComponentInChildren<EnemyController>();
            if (enemyToCapture != null)
            {
                Capture(enemyToCapture);
                return;
            }
        }
        else
        {
            ExampleCharacterController characterController = collision.gameObject.GetComponentInChildren<ExampleCharacterController>();
            if (characterController != null)
            {
                Pop(true);
            }
        }
        if (collision.transform.gameObject.CompareTag("heal"))
        {
            GameManager.Instance.InvokePlayerHeal(.25f);
        }
    }

    public GameObject GetCapturedObject()
    {
        return capturedEnemy.gameObject;
    }

    public void Capture(EnemyController enemyToCapture)
    {
        if (this.capturedEnemy == null)
        {
            transform.localScale = captureScale;

            MeshRenderer render = enemyToCapture.gameObject.GetComponentInChildren<MeshRenderer>();
            if (render == null)
            {
                transform.position = enemyToCapture.transform.position;
            }
            else
            {
                transform.position = render.transform.position;
            }

            capturedEnemy = enemyToCapture;
            capturedEnemy.Capture(gameObject);

            StopCoroutine(travelCoroutine);
            escapeCoroutine = StartCoroutine(EscapeCoroutine());
            rigidBody.AddForce(floatForce);
        }
    }

    public void Pop(bool isEnemyDestroyed = false)
    {
        if (capturedEnemy)
        {
            EnemyController enemyController = capturedEnemy.GetComponentInChildren<EnemyController>();
            if (enemyController != null)
            {
                if (isEnemyDestroyed)
                {
                    GameManager.Instance.PlayerScored(100);
                    GetCapturedObject().SetActive(false);
                }
                else
                {
                    enemyController.Escape(); 
                }
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
        if (capturedEnemy != null)
        {
            Pop(false);
        }
    }
}
