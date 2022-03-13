using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    [RequireComponent(typeof(FSM_FIND_PATH))]
    public class FSM_ANT : FiniteStateMachine
    {
        public enum State { INITIAL, GO_TO_DELIVERY_POINT, GO_TO_EXIT_POINT};

        public State currentState = State.INITIAL;

        FSM_FIND_PATH fsm_findPath;
        List<GameObject> posibleExitPoints;
        GameObject definitiveExitPoint;

        // Start is called before the first frame update
        void Start()
        {
            fsm_findPath = GetComponent<FSM_FIND_PATH>();

            foreach(GameObject exitPoint in GameObject.FindGameObjectsWithTag("EXITPOINT"))
            {
                posibleExitPoints.Add(exitPoint);
            }
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
                    //fsm_findPath.Exit();
                    break;
            }

            // ENTER STATE LOGIC. Depends on newState
            switch (newState)
            {
                case State.INITIAL:

                    break;
                case State.GO_TO_DELIVERY_POINT:
                    //fsm_findPath.ReEnter();
                    break;
                case State.GO_TO_EXIT_POINT:
                    definitiveExitPoint = posibleExitPoints[Random.Range(0, posibleExitPoints.Count)];
                    break;
            }

            currentState = newState;
        }
    }
}
