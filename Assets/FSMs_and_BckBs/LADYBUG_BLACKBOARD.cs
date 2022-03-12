using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LADYBUG_BLACKBOARD : MonoBehaviour
{
    public float reachedObjectRadius = 2.0f;
    public GameObject target;

    [Header ("Eggs")]
    public float closerEggDetectionRadius = 50.0f;
    public float randomEggDetectionRadius = 180.0f;
    public float eggDetectionRadiusWhileReachingAnother = 50.0f;

    [Header("Seed")]
    public float closeSeedDetectionRadius = 80.0f;
    public float randomSeedDetectionRadius = 125.0f;
    public float eggDetectionRadiusWhileReachingSeed = 25.0f;
}    

