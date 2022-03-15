using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIND_PATH_BLACKBOARD : MonoBehaviour
{
    GameObject[] wanderPoints;
    GameObject[] storePoints;
    GameObject[] hachinPoints;
    public float pointReachedRadius = 2.0f;
    public GameObject target;
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

    public void SetTargetToStoreChamber()
    {
        // currentWaypoint =  blackboard.GetRandomStorePont();
        target = GetRandomStorePont();
    }
    public void SetTargetToHachinChamber()
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
