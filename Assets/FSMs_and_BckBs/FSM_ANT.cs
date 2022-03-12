using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    [RequireComponent(typeof(FSM_FIND_PATH))]
    public class FSM_ANT : FiniteStateMachine
    {
        public enum State { INITIAL, WANDER};

        public State currentState = State.INITIAL;

        FSM_FIND_PATH fsm_findPath;

        // Start is called before the first frame update
        void Start()
        {
            fsm_findPath = GetComponent<FSM_FIND_PATH>();
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
                case State.WANDER:
                    fsm_findPath.Exit();
                    break;
            }

            // ENTER STATE LOGIC. Depends on newState
            switch (newState)
            {
                case State.INITIAL:

                    break;
                case State.WANDER:
                    fsm_findPath.ReEnter();
                    break;
            }

            currentState = newState;
        }
    }
}
