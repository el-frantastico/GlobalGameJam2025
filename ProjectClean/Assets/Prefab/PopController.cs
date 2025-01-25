using UnityEngine;

public class PopController : MonoBehaviour
{
    [SerializeField]
    private int score = 5;

    [SerializeField]
    private int maximumLateralProximity = 5;


    private void OnTriggerEnter(Collider other)
    {
        BubbleController bubbleController = other.gameObject.GetComponent<BubbleController>();
        if (bubbleController == null)
        {
            return;
        }

        if (other.gameObject.transform.position.y > this.gameObject.transform.position.y)
        {
            // enemy is higher. 
            return;
        }

        if (other.gameObject.transform.position.x - this.gameObject.transform.position.x > maximumLateralProximity)
        {
            // make sure that we only count if we're on top of the enemy, not hitting from the side. 
            return;
        }

        //GameManager.Instance.PlayerScored(score);
        bubbleController.Pop(true);
    }
}
