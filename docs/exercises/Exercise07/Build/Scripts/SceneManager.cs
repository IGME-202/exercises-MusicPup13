using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject small;
    public GameObject medium;
    public GameObject large;

    public float mass;

    // Start is called before the first frame update
    void Start()
    {
        //sets masses
        GameObject.Find("small").GetComponent<Vehicle>().mass = 1;
        GameObject.Find("medium").GetComponent<Vehicle>().mass = 5;
        GameObject.Find("large").GetComponent<Vehicle>().mass = 20;

        GameObject[] monsters = new GameObject[3];  //array for the objects

        //instantiates objects and puts them in the array
        monsters[0] = GameObject.Instantiate(small, new Vector3(10f, -20f, 0f), Quaternion.identity) as GameObject;
        monsters[1] = GameObject.Instantiate(medium, new Vector3(-10f, 40f, 0f), Quaternion.identity) as GameObject;
        monsters[2] = GameObject.Instantiate(large, new Vector3(15f, 60f, 0f), Quaternion.identity) as GameObject;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
