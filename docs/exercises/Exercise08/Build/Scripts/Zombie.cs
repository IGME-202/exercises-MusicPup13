using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Vehicle
{
    //create a manager for all the humans in the game
    public Vehicle seekTarget;
    public float seekFactor = 1f;

    protected override void CalcSteeringForces()
    {
        Vector3 uForce = Vector3.zero;

        //uForce += (Seek(seekTarget) * seekFactor);

        uForce += (Pursue(seekTarget) * seekFactor);

        if (position.x - (transform.localScale.x / 2) < -1 * floor.transform.localScale.x / 2)
        {
            position.x = (-1 * floor.transform.localScale.x / 2) + (transform.localScale.x / 2);
        }

        if (position.x + (transform.localScale.x / 2) > floor.transform.localScale.x / 2)
        {
            position.x = (floor.transform.localScale.x / 2) - (transform.localScale.x / 2);
        }

        if (position.z - (transform.localScale.z / 2) < -1 * floor.transform.localScale.z / 2)
        {
            position.z = (-1 * floor.transform.localScale.z / 2) + (transform.localScale.z / 2);
        }

        if (position.z + (transform.localScale.z / 2) > floor.transform.localScale.z / 2)
        {
            position.z = (floor.transform.localScale.z / 2) - (transform.localScale.z / 2);
        }

        position.y = (floor.transform.localScale.y / 2) + (transform.localScale.y / 2);

        uForce = Vector3.ClampMagnitude(uForce, maxForce);

        ApplyForce(uForce);
    }

    protected override void Start()
    {
        base.Start();
        //material1 = Resources.Load("Debug_green", typeof(Material)) as Material;
        //material2 = Resources.Load("Debug_blue", typeof(Material)) as Material;
        //GetComponent<Renderer>().material = material1;
        //GetComponent<Renderer>().material = material2;
    }
}
