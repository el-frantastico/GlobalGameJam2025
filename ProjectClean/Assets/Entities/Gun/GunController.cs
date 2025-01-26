using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

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

    [SerializeField]
    private Transform _bubbleCharge;

    void Update()
    {
        if (isFireable && Input.GetMouseButton(0))
        {
            SpawnBubble();
        }
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


        StartCoroutine(FireCooldownCoroutine(fireCooldownTime));   
    }

    IEnumerator FireCooldownCoroutine(float fireCooldownTime)
    {
        float elapsedTime = 0;
        float waitTime = fireCooldownTime;
        float currentVolume = 0;

        isFireable = false;

        while (elapsedTime < waitTime)
        {
            _bubbleCharge.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, elapsedTime / fireCooldownTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;

        isFireable = true;
    }
}
