using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //List<GameObject> posibleExitPoints;
        GameObject[] posibleExitPoints; //blackboard
        GameObject definitiveExitPoint;
        GameObject child;

        // Start is called before the first frame update
        void Start()
        {
            fsm_findPath = GetComponent<FSM_FIND_PATH>();
            posibleExitPoints = GameObject.FindGameObjectsWithTag("EXITPOINT");
            child = gameObject.transform.GetChild(0).gameObject;
            findPathBlackboard = GetComponent<FIND_PATH_BLACKBOARD>();

            /*
            foreach(GameObject exitPoint in GameObject.FindGameObjectsWithTag("EXITPOINT"))
            {
                posibleExitPoints.Add(exitPoint);
            }
            */
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
                    if (SensingUtils.DistanceToTarget(gameObject, deliveryPoint) <= findPathBlackboard.pointReachedRadius)  //blackboard
                    {
                        ChangeState(State.GO_TO_EXIT_POINT);
                        break;
                    }
                    break;
                case State.GO_TO_EXIT_POINT:
                    if(SensingUtils.DistanceToTarget(gameObject, definitiveExitPoint) <= findPathBlackboard.pointReachedRadius) //blackboard
                    {
                        Debug.Log("Me destruyo");
                        Destroy(gameObject);
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
                    fsm_findPath.Exit();
                    fsm_findPath.ReEnter();
                    findPathBlackboard.target = deliveryPoint;
                    //fsm_findPath.currentWaypoint = deliveryPoint;
                    break;
                case State.GO_TO_EXIT_POINT:
                    fsm_findPath.Exit();
                    fsm_findPath.ReEnter();
                    child.transform.parent = null;
                    definitiveExitPoint = posibleExitPoints[Random.Range(0, posibleExitPoints.Length-1)];
                   // fsm_findPath.currentWaypoint = definitiveExitPoint;
                    findPathBlackboard.target = definitiveExitPoint;
                    break;
            }

            currentState = newState;
        }
    }
}
