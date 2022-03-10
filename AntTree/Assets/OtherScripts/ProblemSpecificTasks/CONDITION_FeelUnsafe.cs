using UnityEngine;
using BTs;

public class CONDITION_FeelUnsafe : Condition
{
    public string keyAttractor;
    public string keySafeRadius;
    public string keyExtraSafeRadius;

    // Constructor
    public CONDITION_FeelUnsafe(string keyAttractor,
                                string keySafeRadius,
                                string keyExtraSafeRadius)
    {
        this.keyAttractor = keyAttractor;
        this.keySafeRadius = keySafeRadius;
        this.keyExtraSafeRadius = keyExtraSafeRadius;
    }

    private bool lastTick = false;

     private GameObject attractor;
    private float safeRadius;
    private float extraSafeRadius;


    public override void OnInitialize()
    {
        attractor = blackboard.Get<GameObject>(keyAttractor);
        safeRadius = blackboard.Get<float>(keySafeRadius);
        extraSafeRadius = blackboard.Get<float>(keyExtraSafeRadius);
        lastTick = false;
    }

    public override bool Check()
    {
        // outside safe, return false
        // inside extraSafe, return false
        // any other case return same as at lastTick
        // Use SensingUtils.DistanceToTarget
        if( SensingUtils.DistanceToTarget(gameObject,attractor)< extraSafeRadius)
        {
            return false;
        }
        if (SensingUtils.DistanceToTarget(gameObject, attractor) > safeRadius)
        {
            return false;
        }
        /* COMPLETE */

        return lastTick;
    }


}
