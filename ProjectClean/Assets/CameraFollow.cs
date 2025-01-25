using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject _target;
    [SerializeField] Vector3 _offset;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = _offset + _target.transform.position;
    }
}
