using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steerings;

namespace FSM
{
    [RequireComponent(typeof(PathFollowing))]
    [RequireComponent(typeof(PathFeeder))]
    public class FSM_FIND_PATH : FiniteStateMachine
    {
        public enum State { INITIAL, GENERATING, FOLLOWING, TERMINATED };

        public State currentState = State.INITIAL;

        PathFeeder pathFeeder;
        public GameObject targets;
        private GameObject currentTar;

        // Start is called before the first frame update
        void Start()
        {
            pathFeeder = GetComponent<PathFeeder>();
            foreach (var variable in targets.GetComponentsInChildren<Transform>())
            {
                if (Random.Range(0f, 1f) > 0.4f)
                {
                    currentTar = variable.gameObject;
                }
            }
        }

        public override void ReEnter()
        {
            foreach (var variable in targets.GetComponentsInChildren<Transform>())
            {
                if (Random.Range(0f, 1f) > 0.4f)
                {
                    currentTar = variable.gameObject;
                }
            }
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
                    ChangeState(State.FOLLOWING);
                    break;
                case State.GENERATING:

                    break;
                case State.FOLLOWING:

                    break;
                case State.TERMINATED:

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
                case State.GENERATING:

                    break;
                case State.FOLLOWING:

                    break;
                case State.TERMINATED:

                    break;
            }

            // ENTER STATE LOGIC. Depends on newState
            switch (newState)
            {
                case State.INITIAL:

                    break;
                case State.GENERATING:

                    break;
                case State.FOLLOWING:
                    pathFeeder.target = currentTar;
                    break;
                case State.TERMINATED:

                    break;
            }

            currentState = newState;
        }
    }
}