using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    //inspector values
    private Vector3 vehiclePosition;
    private Vector3 direction = Vector3.up;
    private Vector3 velocity;
    private Vector3 acceleration;
    private bool frictionOn;

    public float totalRotation;
    public float angleDelta = 1;
    public float coefFric = 1;

    public float mass;

    public float walkForce;
    public Vector3 gravForce = new Vector3(0f, -1f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        vehiclePosition = transform.position;
        frictionOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        acceleration = Vector3.zero;

        ApplyForce(gravForce);

        //turning friction on and off
        if (Input.GetKeyDown(KeyCode.F) == true)
        {
            if (frictionOn == true)
            {
                frictionOn = false;
            }
            else
            {
                frictionOn = true;
            }
        }

        if (frictionOn == true)
        {
            ApplyFriction(coefFric);
        }

        if (Input.GetMouseButtonDown(0) == true)
        {
            ApplyMouseForce();
        }

        TurnVehicle();
        UpdatePostion();
        Bounce();
        SetTransform();
    }

    void TurnVehicle()
    {
        //left rotation with left arrow key
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = Quaternion.Euler(0, 0, angleDelta) * direction;
            totalRotation += angleDelta;
        }

        //right rotation with right arrow key
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = Quaternion.Euler(0, 0, -angleDelta) * direction;
            totalRotation -= angleDelta;
        }
    }

    void UpdatePostion()
    {
        //Move vehicle forward on key press
        if (Input.GetKey(KeyCode.UpArrow))
        {
            ApplyForce(direction * walkForce);
        }

        velocity += acceleration * Time.deltaTime;

        //use velocity to change object's position
        vehiclePosition += velocity * Time.deltaTime;
    }

    void WrapVehicle()
    {
        Camera cam = Camera.main;
        Vector3 max = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
        Vector3 min = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));

        //position vehicle back on screen if it's off screen
        if (vehiclePosition.x > max.x)
        {
            vehiclePosition.x = min.x;
        }

        if (vehiclePosition.x < min.x)
        {
            vehiclePosition.x = max.x;
        }

        if (vehiclePosition.y > max.y)
        {
            vehiclePosition.y = min.y;
        }

        if (vehiclePosition.y < min.y)
        {
            vehiclePosition.y = max.y;
        }
    }

    void SetTransform()
    {
        //Update rotation
        transform.rotation = Quaternion.Euler(0, 0, totalRotation);

        //update position
        transform.position = vehiclePosition;
    }

    void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    void ApplyFriction(float coeff)
    {
        //creates a friction force and applys it
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction = friction * coeff;
        acceleration += friction;
    }

    void ApplyMouseForce()
    {
        //gets mouse position when mouse is pressed and applys a force to the objects from it
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        velocity -= mouseWorldPos;
        acceleration += velocity;
    }

    void Bounce()
    {
        Camera cam = Camera.main;
        Vector3 max = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
        Vector3 min = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));

        //position vehicle back on screen if it's off screen
        if (vehiclePosition.x > max.x)
        {
            velocity.x *= -1;
        }

        if (vehiclePosition.x < min.x)
        {
            velocity.x *= -1;
        }

        if (vehiclePosition.y > max.y)
        {
            velocity.y *= -1;
        }

        if (vehiclePosition.y < min.y)
        {
            velocity.y *= -1;
        }
    }
}
