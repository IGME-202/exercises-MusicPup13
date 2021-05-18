using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igloo : MonoBehaviour
{
    public GameObject myPrefab;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(myPrefab, new Vector3(57, 0, 18), Quaternion.identity);

        Instantiate(myPrefab, new Vector3(5, 0, 64), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
