using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    protected Vector3 position;
    protected Vector3 direction;
    protected Vector3 velocity;
    protected Vector3 acceleration;
    //protected Material material1;
    //protected Material material2;

    protected GameObject floor;

    [Min(0.0001f)]
    public float mass = 1f;
    public float radius = 1f;
    public float maxSpeed = 1f;
    public float maxForce = 1f;
    public float predictionTime = 1f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        position = transform.position;
        direction = Vector3.right;
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        floor = GameObject.Find("floor");
    }

    // Update is called once per frame
    void Update()
    {
        CalcSteeringForces();
        //Bounce();
        UpdatePostion();
        SetTransform();
    }

    void WrapVehicle()
    {
        //position vehicle back on screen if it's off screen
        if (position.x + transform.localScale.x / 2 > floor.transform.localScale.x / 2)
        {
            position.x = -1 * ((floor.transform.localScale.x / 2) - transform.localScale.x / 2);
        }

        if (position.x - transform.localScale.x / 2 < -1 * (floor.transform.localScale.x / 2))
        {
            position.x = (floor.transform.localScale.x / 2) - transform.localScale.x / 2;
        }

        if (position.z + transform.localScale.z / 2 > floor.transform.localScale.z / 2)
        {
            position.z = -1 * ((floor.transform.localScale.z / 2) - transform.localScale.z / 2);
        }

        if (position.z - transform.localScale.z / 2 < -1 * (floor.transform.localScale.z / 2))
        {
            position.z = (floor.transform.localScale.z / 2) - transform.localScale.z / 2;
        }

        position.y = 1;
    }

    void Bounce()
    {
        //position vehicle back on screen if it's off screen
        if (position.x + transform.localScale.x / 2 > floor.transform.localScale.x / 2)
        {
            velocity.x *= -1;
        }

        if (position.x - transform.localScale.x / 2 < -1 * (floor.transform.localScale.x / 2))
        {
            velocity.x *= -1;
        }

        if (position.z + transform.localScale.z / 2 > floor.transform.localScale.z / 2)
        {
            velocity.z *= -1;
        }

        if (position.z - transform.localScale.z / 2 < -1 * (floor.transform.localScale.z / 2))
        {
            velocity.z *= -1;
        }

        position.y = (floor.transform.localScale.y / 2) + (transform.localScale.y / 2);
    }


    protected void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    protected abstract void CalcSteeringForces();

    public Vector3 Seek(Vector3 targetPos)
    {
        Vector3 desiredVelocity = targetPos - position;

        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 seekingForce = desiredVelocity - velocity;

        return seekingForce;
    }

    public Vector3 Seek(GameObject target)
    {
        return Seek(target.transform.position);
    }

    public Vector3 Pursue(Vehicle target)
    {
        Vector3 desiredPos = target.position + (target.velocity * predictionTime);

        Vector3 desiredVelocity = desiredPos - position;

        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 seekingForce = desiredVelocity - velocity;

        return seekingForce;
    }

    public Vector3 Evade(Vehicle enemey)
    {
        Vector3 desiredPos = enemey.position + (enemey.velocity * predictionTime);

        Vector3 desiredVelocity = position - desiredPos;

        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 fleeingForce = desiredVelocity - velocity;

        return fleeingForce;
    }

    public Vector3 Flee(Vector3 targetPos)
    {
        Vector3 desiredVelocity = position - targetPos;

        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 fleeingForce = desiredVelocity - velocity;

        return fleeingForce;
    }
    public Vector3 Flee(GameObject target)
    {
        return Flee(target.transform.position);
    }

    void SetTransform()
    {
        //Update rotation
        transform.rotation = Quaternion.Euler((direction.x - transform.rotation.x), 0, (direction.z - transform.rotation.z));

        //update position
        transform.position = position;
    }

    void UpdatePostion()
    {
        velocity += acceleration * Time.deltaTime;

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        //use velocity to change object's position
        position += velocity * Time.deltaTime;

        transform.position = position;

        direction = velocity.normalized;

        acceleration = Vector3.zero;
    }

    //void OnRenderObject() // Examples of drawing lines – yours might be more complex!
    //{
    //    // Set the material to be used for the first line
    //    material1.SetPass(0);

    //    // Draws one line		
    //    GL.Begin(GL.LINES);                 // Begin to draw lines
    //    GL.Vertex(position);        // First endpoint of this line
    //    GL.Vertex(direction);        // Second endpoint of this line
    //    GL.End();                       // Finish drawing the line

    //    // Second line
    //    // Set another material to draw this second line in a different color
    //    material2.SetPass(0);
    //    GL.Begin(GL.LINES);
    //    GL.Vertex(position);
    //    GL.Vertex(Vector3.right);
    //    GL.End();
    //}

}
