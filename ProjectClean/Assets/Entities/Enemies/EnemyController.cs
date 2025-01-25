using System.Text.RegularExpressions;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        BubbleController bubbleController = other.GetComponent<BubbleController>();

        if (bubbleController == null)
        {
            return;
        }

        bubbleController.Capture(gameObject);
    }
}
