using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    private GameObject floor;

    private void Start()
    {
        floor = GameObject.Find("floor");
    }

    public void OnGrab()
    {
        transform.position = new Vector3(Random.Range(-1 * floor.transform.localScale.x / 2, floor.transform.localScale.x / 2), 
            1, Random.Range(-1 * floor.transform.localScale.z / 2, floor.transform.localScale.z / 2));
    }
}
