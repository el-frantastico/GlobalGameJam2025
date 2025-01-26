using UnityEngine;
using KinematicCharacterController.Examples;
using KinematicCharacterController;
public class PlayerReset : MonoBehaviour
{
    Vector3 startPos;
    Quaternion startRot;
    [SerializeField] ExampleCharacterController controller;
    [SerializeField] KinematicCharacterMotor motor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(startPos != null)
        {
            startPos = transform.position;
            startRot = transform.rotation;
        }
    }

    public void Reset()
    {
        motor.SetPositionAndRotation(startPos, startRot);
        controller.enabled = true;
        motor.enabled = true;
    }
}
