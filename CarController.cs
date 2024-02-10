//   _  _    _____       _       _____ _             _ _       
// _| || |_ / ____|     | |     / ____| |           | (_)      
//|_  __  _| (___   ___ | | ___| (___ | |_ _   _  __| |_  ___  
// _| || |_ \___ \ / _ \| |/ _ \\___ \| __| | | |/ _` | |/ _ \ 
//|_  __  _|____) | (_) | | (_) |___) | |_| |_| | (_| | | (_) |
//  |_||_| |_____/ \___/|_|\___/_____/ \__|\__,_|\__,_|_|\___/ 

using UnityEngine;

public class CarController : MonoBehaviour
{
    // Settings
    public float MoveSpeed = 50;
    public float MaxSpeed = 15;
    public float Drag = 0.98f;
    public float SteerAngle = 20;
    public float Traction = 1;
    public float BrakeForce = 30; // Adjust as needed

    // Variables
    private Vector3 MoveForce;

    // Update is called once per frame
    void Update()
    {
        // Moving
        MoveForce += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position += MoveForce * Time.deltaTime;

        // Steering
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime);

        // Brake
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyBrake();
        }

        // Drag and max speed limit
        MoveForce *= Drag;
        MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed);

        // Traction
        MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime) * MoveForce.magnitude;
    }

    void ApplyBrake()
    {
        // Apply braking force opposite to the direction of movement
        MoveForce -= MoveForce.normalized * BrakeForce * Time.deltaTime;
    }
}

//SoloStudio-2024
