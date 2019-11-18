using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider frontWheelRight_collider;
    public WheelCollider backWheelRight_collider;
    public WheelCollider frontWheelLeft_collider;
    public WheelCollider backWheelLeft_collider;

    public GameObject frontWheelRight_mesh;
    public GameObject backWheelRight_mesh;
    public GameObject frontWheelLeft_mesh;
    public GameObject backWheelLeft_mesh;

    public float topSpeed = 10f;
    public float currentSpeed;

    public float maxTorque = 2000f;
    public float maxBrakeTorque = 2200f;
    public float maxSteerAngle = 45f;


    private float forwardInput_axis;
    private float brakeInput_axis;
    private float turnInput_axis;

    private Rigidbody car_rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        car_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        forwardInput_axis = Input.GetAxis("Vertical");
        turnInput_axis = Input.GetAxis("Horizontal");
        brakeInput_axis = Input.GetAxis("Jump");

        frontWheelLeft_collider.steerAngle = maxSteerAngle * turnInput_axis;
        frontWheelRight_collider.steerAngle = maxSteerAngle * turnInput_axis;


        currentSpeed = 2 * Mathf.PI * backWheelLeft_collider.radius * backWheelLeft_collider.rpm * (6 / 100); 

        if(currentSpeed < topSpeed)
        {
            backWheelLeft_collider.motorTorque = maxTorque * forwardInput_axis;
            backWheelRight_collider.motorTorque = maxTorque * forwardInput_axis;
        }

        backWheelLeft_collider.brakeTorque   = maxBrakeTorque * brakeInput_axis;
        backWheelRight_collider.brakeTorque  = maxBrakeTorque * brakeInput_axis;
        frontWheelLeft_collider.brakeTorque  = maxBrakeTorque * brakeInput_axis;
        frontWheelRight_collider.brakeTorque = maxBrakeTorque * brakeInput_axis;
    }

    private void Update()
    {
        Quaternion frontWheelLeft_quaternion;
        Vector3 frontWheelLeft_vector3;
        frontWheelLeft_collider.GetWorldPose(out frontWheelLeft_vector3, out frontWheelLeft_quaternion);
        frontWheelLeft_mesh.transform.position = frontWheelLeft_vector3;
        frontWheelLeft_mesh.transform.localRotation = frontWheelLeft_quaternion;
        //frontWheelLeft_mesh.transform.Rotate(0, frontWheelLeft_quaternion.y, 0);
        //Debug.Log(frontWheelLeft_quaternion.z);

        Quaternion frontWheelRight_quaternion;
        Vector3 frontWheelRight_vector3;
        frontWheelRight_collider.GetWorldPose(out frontWheelRight_vector3, out frontWheelRight_quaternion);
        frontWheelRight_mesh.transform.position = frontWheelRight_vector3;
        frontWheelRight_mesh.transform.localRotation = frontWheelRight_quaternion;
        //frontWheelRight_mesh.transform.Rotate(0, frontWheelRight_quaternion.y, 0);
        //Debug.Log(frontWheelRight_quaternion.z);

        Quaternion backWheelLeft_quaternion;
        Vector3 backWheelLeft_vector3;
        backWheelLeft_collider.GetWorldPose(out backWheelLeft_vector3, out backWheelLeft_quaternion);
        backWheelLeft_mesh.transform.position = backWheelLeft_vector3;
        backWheelLeft_mesh.transform.localRotation = new Quaternion(backWheelLeft_quaternion.x, backWheelLeft_quaternion.y, backWheelLeft_quaternion.z - 90, 0);
        //backWheelLeft_mesh.transform.Rotate(0, backWheelLeft_quaternion.y, 0);

        Quaternion backWheelRight_quaternion;
        Vector3 backWheelRight_vector3;
        backWheelRight_collider.GetWorldPose(out backWheelRight_vector3, out backWheelRight_quaternion);
        backWheelRight_mesh.transform.position = backWheelRight_vector3;
        backWheelRight_mesh.transform.localRotation = backWheelRight_quaternion;
        //backWheelRight_mesh.transform.Rotate(0, backWheelRight_quaternion.y, 0);

        //Debug.Log(frontWheelLeft_mesh.transform.rotation);
    }
}
