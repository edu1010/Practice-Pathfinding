using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
namespace FSM
{
    [RequireComponent(typeof(FSM_FIND_PATH))]
    [RequireComponent(typeof(FIND_PATH_BLACKBOARD))]
    public class FSM_ANT : FiniteStateMachine
    {
        public enum State { INITIAL, GO_TO_DELIVERY_POINT, GO_TO_EXIT_POINT};

        public State currentState = State.INITIAL;


        FSM_FIND_PATH fsm_findPath;
        FIND_PATH_BLACKBOARD findPathBlackboard;
        GameObject deliveryPoint;        
        GameObject[] posibleExitPoints; 
        GameObject definitiveExitPoint;
        GameObject child;

        // Start is called before the first frame update
        void Awake()
        {
            fsm_findPath = GetComponent<FSM_FIND_PATH>();
            posibleExitPoints = GameObject.FindGameObjectsWithTag("EXITPOINT");
            child = gameObject.transform.GetChild(0).gameObject;
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
                    ChangeState(State.GO_TO_DELIVERY_POINT);
                    break;
                case State.GO_TO_DELIVERY_POINT:
                    if (SensingUtils.DistanceToTarget(gameObject, deliveryPoint) <= findPathBlackboard.pointReachedRadius)  
                    {
                        ChangeState(State.GO_TO_EXIT_POINT);
                        break;
                    }
                    break;
                case State.GO_TO_EXIT_POINT:
                    if(SensingUtils.DistanceToTarget(gameObject, definitiveExitPoint) <= findPathBlackboard.pointReachedRadius || findPathBlackboard.terminated) 
                    {
                        Debug.Log("Me destruyo");
                        Destroy(transform.gameObject);
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
                case State.INITIAL:
                    break;
                case State.GO_TO_DELIVERY_POINT:
                    fsm_findPath.Exit();
                    if (child.tag.Equals("SEED_ON_ANT"))
                    {
                        child.tag = "SEED";
                    }
                    else
                    {
                        child.tag = "EGG";
                    }
                    child.transform.parent = null;
                    GraphNode node = AstarPath.active.GetNearest(child.transform.position,NNConstraint.Default).node;
                    child.transform.position = (Vector3)node.position;

                    break;
                case State.GO_TO_EXIT_POINT:
                    fsm_findPath.Exit();
                    break;
            }

            // ENTER STATE LOGIC. Depends on newState
            switch (newState)
            {
                case State.INITIAL:
                    break;
                case State.GO_TO_DELIVERY_POINT:
                    deliveryPoint = findPathBlackboard.GetRandomWanderPoint();
                    findPathBlackboard.target = deliveryPoint;

                    fsm_findPath.ReEnter();
                    break;
                case State.GO_TO_EXIT_POINT:
                    definitiveExitPoint = posibleExitPoints[Random.Range(0, posibleExitPoints.Length)];
                    findPathBlackboard.target = definitiveExitPoint;
                    fsm_findPath.ReEnter();
                    break;
            }

            currentState = newState;
        }
    }
}
