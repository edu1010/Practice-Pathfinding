using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steerings;

namespace FSM
{
    [RequireComponent(typeof(LADYBUG_BLACKBOARD))]
    [RequireComponent(typeof(FSM_FIND_PATH))]
    [RequireComponent(typeof(FIND_PATH_BLACKBOARD))]
    public class FSM_LADYBUG : FiniteStateMachine
    {
        public enum State { INITIAL, WANDER, REACH_SEED, REACH_EGG, GO_TO_HATCHING_CHAMBER, GO_TO_STORE_CHAMBER};

        public State currentState = State.INITIAL;
        LADYBUG_BLACKBOARD blackboard;
        FIND_PATH_BLACKBOARD findPathBlackboard;
        FSM_FIND_PATH fsm_findPath;

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
                        ChangeState(State.REACH_EGG);
                        break;
                    }
                    else if(SensingUtils.FindRandomInstanceWithinRadius(gameObject, "EGG", blackboard.randomEggDetectionRadius))
                    {
                        blackboard.target = SensingUtils.FindRandomInstanceWithinRadius(gameObject, "EGG", blackboard.randomEggDetectionRadius);
                        ChangeState(State.REACH_EGG);
                        break;
                    }
                    else if(SensingUtils.FindInstanceWithinRadius(gameObject, "SEED", blackboard.closerSeedDetectionRadius))
                    {
                        blackboard.target = SensingUtils.FindInstanceWithinRadius(gameObject, "SEED", blackboard.closerSeedDetectionRadius);
                        ChangeState(State.REACH_SEED);
                        break;
                    }
                    else if(SensingUtils.FindRandomInstanceWithinRadius(gameObject, "SEED", blackboard.randomSeedDetectionRadius))
                    {
                        blackboard.target = SensingUtils.FindRandomInstanceWithinRadius(gameObject, "SEED", blackboard.randomSeedDetectionRadius);
                        ChangeState(State.REACH_SEED);
                        break;
                    }
                    break;
                case State.REACH_SEED:
                    //reached seed
                    if (SensingUtils.DistanceToTarget(gameObject, blackboard.target) <= blackboard.reachedObjectRadius)
                    {
                        ChangeState(State.GO_TO_STORE_CHAMBER); //ns
                        break;
                    }

                    if (SensingUtils.FindInstanceWithinRadius(gameObject, "EGG", blackboard.eggDetectionRadiusWhileReachingSeed))
                    {
                        blackboard.target = SensingUtils.FindInstanceWithinRadius(gameObject, "EGG", blackboard.eggDetectionRadiusWhileReachingSeed);
                        ChangeState(State.REACH_EGG);
                        break;
                    }
                    break;
                case State.REACH_EGG:
                    //reached egg
                    if (SensingUtils.DistanceToTarget(gameObject, blackboard.target) <= blackboard.reachedObjectRadius)
                    {
                        ChangeState(State.GO_TO_HATCHING_CHAMBER); //ns
                        break;
                    }

                    if (SensingUtils.FindInstanceWithinRadius(gameObject, "EGG", blackboard.eggDetectionRadiusWhileReachingAnother))
                    {
                        blackboard.target = SensingUtils.FindInstanceWithinRadius(gameObject, "EGG", blackboard.eggDetectionRadiusWhileReachingAnother);
                    }
                    break;
                case State.GO_TO_HATCHING_CHAMBER:
                    if (SensingUtils.DistanceToTarget(gameObject, fsm_findPath.currentWaypoint) <= blackboard.reachedObjectRadius)
                    {
                        ChangeState(State.WANDER); 
                        break;
                    }

                    break;
                case State.GO_TO_STORE_CHAMBER:

                    break;
            }
        }

        private void ChangeState(State newState)
        {
            // EXIT STATE LOGIC. Depends on current state
            switch (currentState)
            {
                case State.WANDER:
                    fsm_findPath.Exit();
                    break;
                case State.REACH_SEED:
                    blackboard.target.tag = "SEED_ON_LADYBUG";
                    break;
                case State.REACH_EGG:
                    blackboard.target.tag = "EGG_ON_LADYBUG";
                    break;
                case State.GO_TO_STORE_CHAMBER:
                    blackboard.target.transform.parent = null;
                    blackboard.target.tag = "REACHED";
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
                    fsm_findPath.currentWaypoint = findPathBlackboard.GetRandomWanderPoint();
                    break;
                case State.REACH_SEED:
                    break;
                case State.REACH_EGG:
                    break;
                case State.GO_TO_STORE_CHAMBER:
                    blackboard.target.transform.parent = transform;
                    fsm_findPath.SetTargetToStoreChamber();//target store
                    break;
                case State.GO_TO_HATCHING_CHAMBER:
                    blackboard.target.transform.parent = transform;
                    fsm_findPath.SetTargetToHachinChamber();//target hachin
                    break;
            }

            currentState = newState;
        }

    }
}