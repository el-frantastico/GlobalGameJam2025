using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private bool isFireable = true;

    [SerializeField]
    private float fireCooldownTime = 1f;

    [SerializeField]
    private Transform bubbleSpawnTransform;

    [SerializeField]
    private GameObject bubbleGameObject;

    [SerializeField]
    private Transform _gunRotationTarget;

    void Update()
    {
        if (isFireable && Input.GetMouseButton(0))
        {
            SpawnBubble();
        }

        Quaternion gunRotation = new Quaternion(0f, transform.rotation.y, -_gunRotationTarget.rotation.x, transform.rotation.w);
        transform.rotation = gunRotation;
    }

    void SpawnBubble()
    {
        if (bubbleSpawnTransform == null)
        {
            bubbleSpawnTransform = this.transform;
        }

        if (bubbleGameObject == null)
        {
            bubbleGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }
        else
        {
            GameObject bubbleInstance = GameObject.Instantiate(bubbleGameObject);
            bubbleInstance.transform.position = bubbleSpawnTransform.position;
            bubbleInstance.transform.rotation = bubbleSpawnTransform.rotation;
        }

        GameManager.Instance.PlayerScored(5);


        StartCoroutine(FireCooldownCoroutine(fireCooldownTime));   
    }

    IEnumerator FireCooldownCoroutine(float fireCooldownTime)
    {
        isFireable = false;
        yield return new WaitForSeconds(fireCooldownTime);
        isFireable = true;
    }
}
