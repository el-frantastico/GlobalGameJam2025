using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    Vector3 startPos;
    Quaternion startRot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    public void Reset()
    {
        transform.position = startPos;
        transform.rotation = startRot;
    }
}
