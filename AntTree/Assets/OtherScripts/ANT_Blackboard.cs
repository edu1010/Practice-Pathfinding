using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANT_Blackboard : Blackboard
{
    public GameObject attractor;
    public GameObject nest;
    public float safeRadius = 40;
    public float extraSafeRadius = 20;
    public float lowSW = 0.2f;
    public float highSW = 0.8f;
    public float seedDetectionRadius = 60;
}
