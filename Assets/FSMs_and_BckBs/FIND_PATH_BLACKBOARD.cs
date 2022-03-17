using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIND_PATH_BLACKBOARD : MonoBehaviour
{
    GameObject[] wanderPoints;
    GameObject[] storePoints;
    GameObject[] hatchingPoints;
    public float pointReachedRadius = 2.0f;
    public GameObject target ;
    public bool terminated = false;
    private void Awake()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("WANDER_POINTS");
        storePoints = GameObject.FindGameObjectsWithTag("STORE_CHAMBER");
        hatchingPoints     = GameObject.FindGameObjectsWithTag("HATCHING_CHAMBER");
    }

    public GameObject GetRandomWanderPoint()
    {
        return wanderPoints[Random.Range(0, wanderPoints.Length)];
    } 
    public GameObject GetRandomStorePont() { 
        return storePoints[Random.Range(0, storePoints.Length)];
    }
    public GameObject GetRandomHachinPoint() { 
        return hatchingPoints[Random.Range(0, hatchingPoints.Length)];
    }

    public void SetTargetToStoreChamber()
    {
        // currentWaypoint =  blackboard.GetRandomStorePont();
        target = GetRandomStorePont();
    }
    public void SetTargetToHatchingChamber()
    {
        //currentWaypoint = blackboard.GetRandomHachinPoint();
        target = GetRandomHachinPoint();
    }
    public void SetTargetToWander()
    {
        //currentWaypoint = blackboard.GetRandomHachinPoint();
        target = GetRandomWanderPoint();
    }
}
