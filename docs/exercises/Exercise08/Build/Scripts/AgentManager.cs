using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public GameObject human;
    public GameObject zombie;
    private GameObject floor;

    //public Human humanPrefab;
    //public Zombie zombiePrefab;

    //private List<Human> humans;
    //private List<Zombie> zombies;
    //public GameObject treasure;
    public int numberHumansStart = 7;

    //public int numHumans = 7;
    //public int numZombies = 1;

    GameObject zombieObject;
    GameObject treasureObject;
    List<GameObject> humansList = new List<GameObject>();
    List<GameObject> zombiesList = new List<GameObject>();

    List<Vehicle> vehiclesHumans = new List<Vehicle>();
    List<Vehicle> vehiclesZombies = new List<Vehicle>();

    //public Transform humanParent;
    //public Transform zombieParent;

    // Start is called before the first frame update
    void Start()
    {
        //if (humans == null)
        //{
        //    humans = new List<Human>();
        //}

        //if (zombies == null)
        //{
        //    zombies = new List<Zombie>();
        //}

        floor = GameObject.Find("floor");

        //CreateHuman(numHumans);
        //CreateZombie(numZombies);


        zombieObject = GameObject.Instantiate(zombie, new Vector3(Random.Range(-1 * floor.transform.localScale.x / 2, floor.transform.localScale.x / 2),
            1.5f, Random.Range(-1 * floor.transform.localScale.z / 2, floor.transform.localScale.z / 2)), Quaternion.identity) as GameObject;
        zombieObject.AddComponent<Zombie>();
        zombiesList.Add(zombieObject);

        //vehiclesZombies.Add(Instantiate(zombie, new Vector3(Random.Range(-1 * floor.transform.localScale.x / 2, floor.transform.localScale.x / 2),
        //    1.5f, Random.Range(-1 * floor.transform.localScale.z / 2, floor.transform.localScale.z / 2)), Quaternion.identity, Vehicle);

        //treasureObject = GameObject.Instantiate(treasure, new Vector3(Random.Range(-1 * floor.transform.localScale.x / 2, floor.transform.localScale.x / 2),
        //    1f, Random.Range(-1 * floor.transform.localScale.z / 2, floor.transform.localScale.z / 2)), Quaternion.identity) as GameObject;

        for (int i = 0; i < numberHumansStart; i++)
        {
            humansList.Add(GameObject.Instantiate(human, new Vector3(Random.Range(-1 * floor.transform.localScale.x / 2, floor.transform.localScale.x / 2),
            1f, Random.Range(-1 * floor.transform.localScale.z / 2, floor.transform.localScale.z / 2)), Quaternion.identity) as GameObject);

            humansList[i].AddComponent<Human>();
            humansList[i].GetComponent<Human>().fleeTarget = zombieObject.GetComponent<Zombie>();
            //humansList[i].GetComponent<Human>().seekTarget = treasureObject;
        }

        //for (int i = 0; i < humansArray.Length; i++)
        //{
        //    humansArray[i] = GameObject.Instantiate(human, new Vector3(Random.Range(-1 * floor.transform.localScale.x / 2, floor.transform.localScale.x / 2),
        //    1f, Random.Range(-1 * floor.transform.localScale.z / 2, floor.transform.localScale.z / 2)), Quaternion.identity) as GameObject;

        //    humansArray[i].AddComponent<Human>();
        //    humansArray[i].GetComponent<Human>().fleeTarget = zombieObject;
        //    humansArray[i].GetComponent<Human>().seekTarget = treasureObject;
        //}

        //treasureObject.AddComponent<Treasure>();
    }

    //private void CreateHuman(int amountToCreate = 1)
    //{
    //    for (int i = 0; i < amountToCreate; i++)
    //    {
    //        CreateHuman(GetRandomPositionInWorld());
    //    }
    //}

    //public void CreateHuman(Vector3 position)
    //{
    //    humans.Add(Instantiate(humanPrefab, position, Quaternion.identity, humanParent));
    //}

    //private void CreateZombie(int amountToCreate = 1)
    //{
    //    for (int i = 0; i < amountToCreate; i++)
    //    {
    //        CreateZombie(GetRandomPositionInWorld());
    //    }
    //}

    //public void CreateZombie(Vector3 position)
    //{
    //    zombies.Add(Instantiate(zombiePrefab, position, Quaternion.identity, zombieParent));
    //}

    //public Vector3 GetRandomPositionInWorld()
    //{
    //    Vector3 position = new Vector3(Random.Range(-1 * floor.transform.localScale.x / 2, floor.transform.localScale.x / 2),
    //        1f, Random.Range(-1 * floor.transform.localScale.z / 2, floor.transform.localScale.z / 2));

    //    return position;
    //}

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < zombies.Count; i++)
        //{
        //    float shortestDistance = 0f;

        //    for (int j = 0; j < humans.Count; j++)
        //    {
        //        if (j == 0)
        //        {
        //            shortestDistance = Vector3.Distance(zombies[i].transform.position, humans[j].transform.position);
        //            zombies[i].seekTarget = humans[j];
        //        }

        //        if (Vector3.Distance(zombies[i].transform.position, humans[j].transform.position) < shortestDistance)
        //        {
        //            shortestDistance = Vector3.Distance(zombies[i].transform.position, humans[j].transform.position);
        //            zombies[i].seekTarget = humans[j];
        //        }

        //        if (CheckForCollision(humans[j], zombies[i]))
        //        {
        //            Vector3 deadHumanPos = humans[j].transform.position;

        //            CreateZombie(deadHumanPos);

        //            Destroy(humans[j]);

        //            humans.Remove(humans[j]);

        //            j--;
        //        }
        //    }

            for (int i = 0; i < zombiesList.Count; i++)
            {
                float shortestDistance = 0f;

                for (int j = 0; j < humansList.Count; j++)
                {
                    if (j == 0)
                    {
                        shortestDistance = Vector3.Distance(zombiesList[i].transform.position, humansList[j].transform.position);
                        zombiesList[i].GetComponent<Zombie>().seekTarget = humansList[j].GetComponent<Human>();
                    }

                    if (Vector3.Distance(zombiesList[i].transform.position, humansList[j].transform.position) < shortestDistance)
                    {
                        shortestDistance = Vector3.Distance(zombiesList[i].transform.position, humansList[j].transform.position);
                        zombiesList[i].GetComponent<Zombie>().seekTarget = humansList[j].GetComponent<Human>();
                    }

                    //if (Vector3.Distance(humansList[j].transform.position, treasureObject.transform.position) < treasureObject.transform.localScale.x)
                    //{
                    //    treasureObject.GetComponent<Treasure>().OnGrab();
                    //}

                    if (CheckForCollision(humansList[j].GetComponent<Human>(), zombiesList[i].GetComponent<Zombie>()))
                    {
                        Vector3 deadHumanPos = humansList[j].transform.position;

                        zombiesList.Add(GameObject.Instantiate(zombie, deadHumanPos, Quaternion.identity) as GameObject);
                        zombiesList[zombiesList.Count - 1].AddComponent<Zombie>();

                        Destroy(humansList[j]);

                        humansList.Remove(humansList[j]);

                        j--;
                    }

                    //if (CheckForBounds(humansList[j]))
                    //{
                    //    if (humansList[j].transform.position.x - (humansList[j].transform.localScale.x / 2) < -1 * floor.transform.localScale.x / 2)
                    //    {
                    //        humansList[j].transform.position.x = (-1 * floor.transform.localScale.x / 2) + (humansList[j].transform.localScale.x / 2);
                    //    }
                    //}
                }
            }
        }

        bool CheckForCollision(Vehicle objA, Vehicle objB)
        {
            bool isHitting = false;

            if (objB.transform.position.x - (objB.transform.localScale.x / 2) < objA.transform.position.x + (objA.transform.localScale.x / 2)
                    && objB.transform.position.x + (objB.transform.localScale.x / 2) > objA.transform.position.x - (objA.transform.localScale.x / 2)
                    && objB.transform.position.z + (objB.transform.localScale.z / 2) > objA.transform.position.z - (objA.transform.localScale.z / 2)
                    && objB.transform.position.z - (objB.transform.localScale.z / 2) < objA.transform.position.z + (objA.transform.localScale.z / 2))
            {
                isHitting = true;
            }

            return isHitting;
        }
       //bool CheckForBounds(GameObject gameObject)
        //{
        //    bool isOOB = false;
            
        //    if (gameObject.transform.position.x - (gameObject.transform.localScale.x / 2) < -1 * floor.transform.localScale.x / 2
        //            && gameObject.transform.position.x + (gameObject.transform.localScale.x / 2) > floor.transform.localScale.x / 2
        //            && gameObject.transform.position.z + (gameObject.transform.localScale.z / 2) > floor.transform.localScale.z / 2
        //            && gameObject.transform.position.z - (gameObject.transform.localScale.z / 2) < -1 * floor.transform.localScale.z / 2)
        //    {
        //        isOOB = true;
        //    }
            
        //    return isOOB;
        //}
}
