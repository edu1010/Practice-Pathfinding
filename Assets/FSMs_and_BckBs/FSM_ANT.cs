using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class FSM_ANT : FiniteStateMachine
    {
        public enum State { INITIAL, WANDER};

        public State currentState = State.INITIAL;

        // Start is called before the first frame update
        void Start()
        {

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
            }

            // ENTER STATE LOGIC. Depends on newState
            switch (newState)
            {
                case State.INITIAL:

                    break;
            }

            currentState = newState;
        }
    }
}
