using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    /*What else needs to get done
     * acceleration occurs when user presses the up arrow key
     * code vehicular deceleration when the user releases the up arrow key
     * "wrap" the vehicle so it's always on-screen
     * make the vehicle turn left and right when you press the left and right arrow keys
     */

    Vector3 vehiclePosition;
    Vector3 direction;
    Vector3 velocity;

    //accel vector will calculate the rate of change per second
    Vector3 acceleration;
    public float accelerationRate;

    //Don't need a constant speed anymore since the "speed" changes per frame
    //we do need a speed limit!
    public float maximumSpeed;

    //how fast we want the vehicle to turn
    public float turnSpeed;

    Camera cam;
    float camHeight;
    float camWidth;

    // Start is called before the first frame update
    void Start()
    {
        vehiclePosition = new Vector3(0, 0, 0);
        direction = new Vector3(1, 0, 0);
        velocity = new Vector3(0, 0, 0);
        acceleration = new Vector3(0, 0, 0);
        accelerationRate = 0.001f;
        maximumSpeed = 0.25f;
        turnSpeed = 0.25f;
        cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if (vehiclePosition.x < (-camWidth / 2) || vehiclePosition.x > (camWidth/2))
        {
            vehiclePosition.x = -1 * vehiclePosition.x;
        }

        if (vehiclePosition.y < (-camHeight/2) || vehiclePosition.y > (camHeight / 2))
        {
            vehiclePosition.y = -1 * vehiclePosition.y;
        }

        //rotate the direction vector by 1 degree each frame
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = Quaternion.Euler(0, 0, turnSpeed) * direction;
            direction.x = Math.Abs(direction.x) * -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = Quaternion.Euler(0, 0, turnSpeed) * direction;
            direction.x = Math.Abs(direction.x);
        }
        else
        {
            return;
        }

        //calculate the acceleration vector
        acceleration = direction * accelerationRate;
        //add acceleration to velocity
        if (Input.GetKey(KeyCode.UpArrow))
        {
            velocity += acceleration;
        }
        else
        {
            //decceleration thing
            velocity -= acceleration;
        }
        
        //limit the velocity so it doesn't move too quickly
        velocity = Vector3.ClampMagnitude(velocity, maximumSpeed);
        //draw the vehicle at that position
        vehiclePosition += velocity;
        transform.position = vehiclePosition;
        //set the vehicle's rotation to match the direction
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }
}
