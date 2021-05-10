using UnityEngine;
using System;

[Serializable]
public enum DriveType
{
	RearWheelDrive,
	FrontWheelDrive,
	AllWheelDrive
}

public class WheelDrive : MonoBehaviour
{
    [Tooltip("Maximum steering angle of the wheels")]
	public float maxAngle = 30f;
	[Tooltip("Maximum torque applied to the driving wheels")]
	public float maxTorque = 400f;
	[Tooltip("Maximum brake torque applied to the driving wheels")]
	public float brakeTorque = 30000f;
	[Tooltip("If you need the visual wheels to be attached automatically, drag the wheel shape here.")]
	public GameObject wheelShape;
	public GameObject frontWheelLeft;
	public GameObject frontWheelRight;
	public GameObject rearWheelLeft;
	public GameObject rearWheelRight;

	[Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
	public DriveType driveType;

    private WheelCollider[] m_Wheels;

	void Start()
	{
		m_Wheels = GetComponentsInChildren<WheelCollider>();
	}

	void Update()
	{

		float angle = maxAngle * Input.GetAxis("Horizontal");
		float torque = maxTorque * Input.GetAxis("Vertical");

		float handBrake = Input.GetKey(KeyCode.Space) ? brakeTorque : 0;

		foreach (WheelCollider wheel in m_Wheels)
		{
			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = angle;

			if (wheel.transform.localPosition.z < 0)
			{
				wheel.brakeTorque = handBrake;
			}

			if (wheel.transform.localPosition.z < 0 && driveType != DriveType.FrontWheelDrive)
			{
				wheel.motorTorque = torque;
			}

			if (wheel.transform.localPosition.z >= 0 && driveType != DriveType.RearWheelDrive)
			{
				wheel.motorTorque = torque;
			}

			if (wheelShape) 
			{
				Quaternion q;
				Vector3 p;
				wheel.GetWorldPose (out p, out q);

				Transform shapeTransformFrontLeft = frontWheelLeft.transform;
				Transform shapeTransformFrontRight = frontWheelRight.transform;
				Transform shapeTransformRearLeft = rearWheelLeft.transform;
				Transform shapeTransformRearRight = rearWheelRight.transform;

                if (wheel.name == "SportCar20_WheelHubFrontLeft")
                {
					shapeTransformFrontLeft.rotation = q;
					shapeTransformFrontLeft.position = p;
				} else if(wheel.name == "SportCar20_WheelHubFrontRight") {
					shapeTransformFrontRight.rotation = q;
					shapeTransformFrontRight.position = p;
				} else if(wheel.name == "SportCar20_WheelHubRearLeft") {
					shapeTransformRearLeft.rotation = q;
				} else if(wheel.name == "SportCar20_WheelHubRearRight") {
					shapeTransformRearRight.rotation = q;
				}
			}
		}
	}
}
