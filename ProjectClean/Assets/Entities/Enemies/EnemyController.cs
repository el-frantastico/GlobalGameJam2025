using System.Text.RegularExpressions;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Collider collider;
    Vector3 startPos;
    Quaternion startRot;
    [SerializeField]
    ForwardMovement _forwardMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public BubbleController _bubbleController;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponentInChildren<Collider>();
        startPos = transform.position;
        startRot = transform.rotation;
    }

    public void Reset()
    {
        transform.position = startPos;
        transform.rotation = startRot;
        _forwardMovement.Reset();
    }

    public void Capture(GameObject gameObject)
    {
        collider.enabled = false;
        rigidbody.linearVelocity = Vector3.zero;
        _forwardMovement.enabled = false;
        _forwardMovement._moving = false;

        rigidbody.isKinematic = true;
        rigidbody.transform.parent = gameObject.transform;
    }

    public void Escape()
    {
        _forwardMovement.enabled = true;
        collider.enabled = true;

        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        rigidbody.transform.parent = null;
    }
}
