using System.Text.RegularExpressions;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody rigidbody;
    Vector3 startPos;
    Quaternion startRot;
    [SerializeField]
    ForwardMovement _forwardMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        startPos = transform.position;
        startRot = transform.rotation;
    }

    public void Reset()
    {
        transform.position = startPos;
        transform.rotation = startRot;
        _forwardMovement.Reset();
    }

    public void OnCollisionEnter(Collision collision)
    {
        BubbleController bubbleController = GetComponent<BubbleController>();
        if (bubbleController.GetCapturedObject() == this.gameObject)
        {
            bubbleController.Pop();
        }
    }
}
