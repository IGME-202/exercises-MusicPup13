using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionCheckMethod
{
    AABB,
    Circle
}

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    List<SpriteRenderer> objects = new List<SpriteRenderer>();

    [SerializeField]
    CollisionCheckMethod checkMethod;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            switch (checkMethod)
            {
                case CollisionCheckMethod.AABB:
                    checkMethod = CollisionCheckMethod.Circle;
                    break;
                case CollisionCheckMethod.Circle:
                    checkMethod = CollisionCheckMethod.AABB;
                    break;
            }
        }

        bool isPlayerHit = false;

        for (int i = 1; i < objects.Count; i++)
        {
            if (CheckForCollision(objects[0], objects[i], checkMethod))
            {
                objects[i].color = Color.red;

                isPlayerHit = true;
                //objects[0].color = Color.red;
            }
            else
            {
                objects[i].color = Color.white;

                //objects[0].color = Color.white;
            }
        }

        if (isPlayerHit)
        {
            objects[0].color = Color.red;
        }
        else
        {
            objects[0].color = Color.white;
        }
    }

    bool CheckForCollision(SpriteRenderer objA, SpriteRenderer objB, CollisionCheckMethod collisionCheck)
    {
        bool isHitting = false;

        switch(collisionCheck)
        {
            case CollisionCheckMethod.AABB:
                if (objB.bounds.min.x < objA.bounds.max.x 
                    && objB.bounds.max.x > objA.bounds.min.x 
                    && objB.bounds.max.y > objA.bounds.min.y 
                    && objB.bounds.min.y < objA.bounds.max.y)
                {
                    isHitting = true;
                }
                break;
            case CollisionCheckMethod.Circle:
                if (Vector3.Distance(objA.bounds.center, objB.bounds.center) < (objA.bounds.size.magnitude + objB.bounds.size.magnitude) / 2f)
                {
                    isHitting = true;
                }
                break;
        }

        return isHitting;
    }
}
