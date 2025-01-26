using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : MonoBehaviour
{

    [SerializeField]
    private float speed;

    private new Rigidbody rigidbody;

    HashSet<GameObject> hitObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        hitObjects = new HashSet<GameObject>();
    }

    private void FixedUpdate()
    {
        rigidbody.linearVelocity = new Vector3(speed, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6) //Floor
        {
            return;
        }

        if (hitObjects.Contains(collision.gameObject))
        {
            return;
        }

        hitObjects.Add(collision.gameObject);
        speed *= -1;
    }

    private void OnCollisionExit(Collision collision)
    {
        hitObjects.Remove(collision.gameObject);
    }
}
