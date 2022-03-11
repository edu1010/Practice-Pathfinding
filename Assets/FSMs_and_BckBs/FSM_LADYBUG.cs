using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steerings;

namespace FSM
{
    [RequireComponent(typeof(LADYBUG_BLACKBOARD))]
    public class FSM_LADYBUG : FiniteStateMachine
    {
        public enum State { INITIAL, WANDER, REACH_SEED_OR_EGG};

        public State currentState = State.INITIAL;

        LADYBUG_BLACKBOARD blackboard;

        // Start is called before the first frame update
        void Start()
        {
            blackboard = GetComponent<LADYBUG_BLACKBOARD>();
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
                    //ORDEN:
                    //egg mas cercano
                    //SensingUtils.FindInstanceWithinRadius(gameObject, "EGG", blackboard.closerEggDetectionRadius);

                    // egg random
                    // SensingUtils.FindRandomInstanceWithinRadius(gameObject, "EGG", blackboard.randomEggDetectionRadius);

                    // seed más cercana
                    // SensingUtils.FindInstanceWithinRadius(gameObject, "SEED", blackboard.closeSeedDetectionRadius)

                    // seed random
                    // SensingUtils.FindRandomInstanceWithinRadius(gameObject, "SEED", blackboard.randomSeedDetectionRadius);
                    

                    break;
                case State.REACH_SEED_OR_EGG:
                    // nose si iria mejor hacer dos estados, uno pa cuando va a un egg y otro pa cuando seed o mirar por tag y ale

                    // sensing utils egg mas cerca mientras vas hacia otro egg
                    // SensingUtils.FindInstanceWithinRadius(gameObject, "EGG", blackboard.eggDetectionRadiusWhileReachingAnother);

                    // sensing utils egg cerca mientras vas hacia una seed
                    // SensingUtils.FindInstanceWithinRadius(gameObject, "EGG", blackboard.eggDetectionRadiusWhileReachingSeed);

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