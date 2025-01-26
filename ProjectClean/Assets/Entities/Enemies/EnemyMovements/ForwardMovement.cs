using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : MonoBehaviour
{

    [SerializeField]
    private float speed;
    private float startingSpeed;

    [SerializeField]
    private bool _moving = true;
    private new Rigidbody rigidbody;
    HashSet<GameObject> hitObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        hitObjects = new HashSet<GameObject>();
        startingSpeed = speed;
    }

    public void Reset()
    {
        speed = startingSpeed;
    }
    private void FixedUpdate()
    {
        if(_moving)
        {
            rigidbody.linearVelocity = new Vector3(speed, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == 6) //Floor
        {
            if (!_moving)
                _moving = true;
            return;
        }

        if (hitObjects.Contains(collision.gameObject))
        {
            return;
        }

        hitObjects.Add(collision.gameObject);
        speed *= -1;
        transform.rotation = Quaternion.LookRotation(new Vector3(speed, 0,0));
    }

    private void OnCollisionExit(Collision collision)
    {
        hitObjects.Remove(collision.gameObject);
    }
}
