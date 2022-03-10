using UnityEngine;
using BTs;

[CreateAssetMenu(fileName = "BT_Ant", menuName = "Behaviour Trees/BT_Ant", order = 1)]
public class BT_Ant : BehaviourTree
{
    /* If necessary declare BT parameters here. 
       All public parameters must be of type string. All public parameters must be
       regarded as keys in/for the blackboard context.
       Use prefix "key" for input parameters (information stored in the blackboard that must be retrieved)
       use prefix "keyout" for output parameters (information that must be stored in the blackboard)

       e.g.
       public string keyDistance;
       public string keyoutObject 
     */

     // construtor
    public BT_Ant()  { 
        /* Receive BT parameters and set them. Remember all are of type string */
    }
    
    public override void OnConstruction()
    {
        DynamicSelector dinamicSelector = new DynamicSelector();
        root = dinamicSelector;
        dinamicSelector.AddChild(new CONDITION_FeelUnsafe("attractor", "safeRadius", "extraSafeRadius"), new ACTION_WanderAround("attractor", "highSW"));
        dinamicSelector.AddChild(new CONDITION_AlwaysTrue(), new ACTION_WanderAround("attractor", "lowSW"));



        /* Write here (method OnConstruction) the code that constructs the Behaviour Tree 
           Remember to set the root attribute to a proper node
           e.g.
            ...
            root = new SEQUENCE();
            ...

          A behaviour tree can use other behaviour trees.  
      */
    }
}
