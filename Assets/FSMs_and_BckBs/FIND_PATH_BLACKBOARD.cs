using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIND_PATH_BLACKBOARD : MonoBehaviour
{
    GameObject[] wanderPoints;
    public float pointReachedRadius = 2.0f;
    private void Start()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("WANDER_POINTS");
    }

    public GameObject GetRandomWanderPoint()
    {
        return wanderPoints[Random.Range(0, wanderPoints.Length)];
    }
}
