using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SimpleCarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;

    public WheelCollider frontLeftW, frontRightW;
    public WheelCollider leftRearW, rightRearW;
    public Transform frontLeftT, frontRightT;
    public Transform leftRearT, rightRearT;

    public float maxSteerAngle = 30;
    public float motorForce = 50; // Allow to modify the motorTorque (couple) on WheelCollider


    void Update()
    {
        Debug.Log(frontLeftT.position);
    }
    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Debug.Log(horizontalInput);
        Debug.Log(verticalInput);

    }

    private void Steer()
    {
        steeringAngle = maxSteerAngle * horizontalInput;
        frontLeftW.steerAngle = steeringAngle;
        frontRightW.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        //Here I apply acceleration to the front wheels
        //I can also apply acceleration to rear wheels or to all wheels

        //VerticalInput because the wheel is vertical and going forward
        frontLeftW.motorTorque = verticalInput * motorForce;
        frontRightW.motorTorque = verticalInput * motorForce;

    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftW, frontLeftT);
        UpdateWheelPose(frontRightW, frontRightT);
        UpdateWheelPose(leftRearW, leftRearT);
        UpdateWheelPose(rightRearW, rightRearT);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        Vector3 pos = transform.position;
        Quaternion quat = transform.rotation;
        
        collider.GetWorldPose(out pos, out quat);

        transform.position = pos;
        transform.rotation = quat;
    }

    private void FixedUpdate()//Run zero, once or several times per frame
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }
}
