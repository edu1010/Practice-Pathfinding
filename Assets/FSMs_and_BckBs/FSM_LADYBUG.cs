using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steerings;
using Pathfinding;
namespace FSM
{
    [RequireComponent(typeof(LADYBUG_BLACKBOARD))]
    [RequireComponent(typeof(FSM_FIND_PATH))]
    [RequireComponent(typeof(FIND_PATH_BLACKBOARD))]
    public class FSM_LADYBUG : FiniteStateMachine
    {
        public enum State { INITIAL, WANDER, REACH_SEED, REACH_EGG, GO_TO_HATCHING_CHAMBER, GO_TO_STORE_CHAMBER };

        public State currentState = State.INITIAL;
        LADYBUG_BLACKBOARD blackboard;
        FIND_PATH_BLACKBOARD findPathBlackboard;
        FSM_FIND_PATH fsm_findPath;
        GameObject egg;
        GameObject seed;
        // Start is called before the first frame update
        void Start()
        {
            blackboard = GetComponent<LADYBUG_BLACKBOARD>();
            fsm_findPath = GetComponent<FSM_FIND_PATH>();
            findPathBlackboard = GetComponent<FIND_PATH_BLACKBOARD>();
        }

        public override void ReEnter()
        {
            base.ReEnter();

        }

        public override void Exit()
        {
            base.Exit();
        }

        // Update is called once per frame
        void Update()
        {
            switch (currentState)
            {
                case State.INITIAL:
                    ChangeState(State.WANDER);
                    break;
                case State.WANDER:
                    if (SensingUtils.FindInstanceWithinRadius(gameObject, "EGG", blackboard.closerEggDetectionRadius))
                    {
                        blackboard.target = SensingUtils.FindInstanceWithinRadius(gameObject, "EGG", blackboard.closerEggDetectionRadius);
                        findPathBlackboard.target = blackboard.target;
                        ChangeState(State.REACH_EGG);
                        break;
                    }
                    else if (SensingUtils.FindRandomInstanceWithinRadius(gameObject, "EGG", blackboard.randomEggDetectionRadius))
                    {
                        blackboard.target = SensingUtils.FindRandomInstanceWithinRadius(gameObject, "EGG", blackboard.randomEggDetectionRadius);
                        findPathBlackboard.target = blackboard.target;
                        ChangeState(State.REACH_EGG);
                        break;
                    }
                    else if (SensingUtils.FindInstanceWithinRadius(gameObject, "SEED", blackboard.closerSeedDetectionRadius))
                    {
                        blackboard.target = SensingUtils.FindInstanceWithinRadius(gameObject, "SEED", blackboard.closerSeedDetectionRadius);
                        findPathBlackboard.target = blackboard.target;
                        ChangeState(State.REACH_SEED);
                        break;
                    }
                    else if (SensingUtils.FindRandomInstanceWithinRadius(gameObject, "SEED", blackboard.randomSeedDetectionRadius))
                    {
                        blackboard.target = SensingUtils.FindRandomInstanceWithinRadius(gameObject, "SEED", blackboard.randomSeedDetectionRadius);
                        findPathBlackboard.target = blackboard.target;
                        ChangeState(State.REACH_SEED);
                        break;
                    }
                    if (findPathBlackboard.terminated)// go to other wander point
                    {
                        findPathBlackboard.SetTargetToWander();
                        fsm_findPath.ReEnter();
                    }
                    break;
                case State.REACH_SEED:
                    if (!blackboard.target.tag.Equals("SEED"))
                    {
                        ChangeState(State.WANDER);
                        break;
                    }
                    //reached seed
                    if (SensingUtils.DistanceToTarget(gameObject, blackboard.target) <= blackboard.reachedObjectRadius || findPathBlackboard.terminated)
                    {
                        ChangeState(State.GO_TO_STORE_CHAMBER); 
                        break;
                    }
                    if (SensingUtils.FindInstanceWithinRadius(gameObject, "EGG", blackboard.eggDetectionRadiusWhileReachingSeed))
                    {
                        blackboard.target = SensingUtils.FindInstanceWithinRadius(gameObject, "EGG", blackboard.eggDetectionRadiusWhileReachingSeed);
                        findPathBlackboard.target = blackboard.target;
                        ChangeState(State.REACH_EGG);
                        break;
                    }
                    break;
                case State.REACH_EGG:
                    if (!blackboard.target.tag.Equals("EGG"))
                    {
                        ChangeState(State.WANDER);
                        break;
                    }
                    //reached egg
                   // if (SensingUtils.DistanceToTarget(gameObject, blackboard.target) <= blackboard.reachedObjectRadius || findPathBlackboard.terminated)
                    if (SensingUtils.DistanceToTarget(gameObject, blackboard.target) <= blackboard.reachedObjectRadius )
                    {
                        ChangeState(State.GO_TO_HATCHING_CHAMBER);
                        break;
                    }

                    if (SensingUtils.FindInstanceWithinRadius(gameObject, "EGG", blackboard.eggDetectionRadiusWhileReachingAnother))
                    {
                        blackboard.target = SensingUtils.FindInstanceWithinRadius(gameObject, "EGG", blackboard.eggDetectionRadiusWhileReachingAnother);
                        findPathBlackboard.target = blackboard.target;
                    }
                    break;
                case State.GO_TO_HATCHING_CHAMBER://ir a dejar el huevo
                    if (SensingUtils.DistanceToTarget(gameObject, findPathBlackboard.target) <= blackboard.reachedObjectRadius)
                    {
                        ChangeState(State.WANDER);
                        break;
                    }

                    break;
                case State.GO_TO_STORE_CHAMBER://ir a dejar la semilla

                    egg = (SensingUtils.FindInstanceWithinRadius(gameObject, "EGG", blackboard.eggDetectionRadiusWhileReachingSeed));
                    if (egg!=null)
                    {
                        blackboard.target.tag = "SEED";
                        blackboard.target.transform.parent = null;
                        GraphNode node = AstarPath.active.GetNearest(blackboard.target.transform.position, NNConstraint.Default).node;
                        blackboard.target.transform.position = (Vector3)node.position;//dejar semilla
                        blackboard.target = egg;
                        findPathBlackboard.target = blackboard.target;
                        ChangeState(State.REACH_EGG);
                        break;
                    }
                    if (SensingUtils.DistanceToTarget(gameObject, findPathBlackboard.target) <= blackboard.reachedObjectRadius)
                    {
                        ChangeState(State.WANDER);
                        break;
                    }

                    break;
            }
        }

        private void ChangeState(State newState)
        {
            // EXIT STATE LOGIC. Depends on current state
            switch (currentState)
            {
                case State.WANDER:
                    break;
                case State.REACH_SEED:
                    break;
                case State.REACH_EGG:
                    break;
                case State.GO_TO_STORE_CHAMBER:
                    if (!(newState.Equals(State.REACH_EGG)))
                    {
                        blackboard.target.tag = "REACHED";
                        blackboard.target.transform.parent = null;
                    }
                   
                    break;
                case State.GO_TO_HATCHING_CHAMBER:
                    blackboard.target.transform.parent = null;
                    blackboard.target.tag = "REACHED";
                    break;
            }

            // ENTER STATE LOGIC. Depends on newState
            switch (newState)
            {
                case State.WANDER:
                    fsm_findPath.ReEnter();
                    findPathBlackboard.SetTargetToWander();
                    break;
                case State.REACH_SEED:
                    fsm_findPath.ReEnter();
                    break;
                case State.REACH_EGG:
                    fsm_findPath.ReEnter();
                    break;
                case State.GO_TO_STORE_CHAMBER:
                    blackboard.target.tag = "SEED_ON_LADYBUG";
                    blackboard.target.transform.parent = transform;
                    findPathBlackboard.SetTargetToStoreChamber();//target store
                    fsm_findPath.ReEnter();
                    break;
                case State.GO_TO_HATCHING_CHAMBER:
                    blackboard.target.tag = "EGG_ON_LADYBUG";
                    blackboard.target.transform.parent = transform;
                    findPathBlackboard.SetTargetToHatchingChamber();//target hachin
                    fsm_findPath.ReEnter();
                    break;
            }

            currentState = newState;
        }

    }
}