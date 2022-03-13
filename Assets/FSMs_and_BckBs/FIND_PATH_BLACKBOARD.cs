using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIND_PATH_BLACKBOARD : MonoBehaviour
{
    GameObject[] wanderPoints;
    GameObject[] storePoints;
    GameObject[] hachinPoints;
    public float pointReachedRadius = 2.0f;
    private void Start()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("WANDER_POINTS");
        storePoints = GameObject.FindGameObjectsWithTag("STORE_CHAMBER");
        hachinPoints     = GameObject.FindGameObjectsWithTag("HATCHING_CHAMBER");
    }

    public GameObject GetRandomWanderPoint()
    {
        return wanderPoints[Random.Range(0, wanderPoints.Length)];
    } 
    public GameObject GetRandomStorePont() { 
        return storePoints[Random.Range(0, storePoints.Length)];
    }
    public GameObject GetRandomHachinPoint() { 
        return hachinPoints[Random.Range(0, hachinPoints.Length)];
    }
}
