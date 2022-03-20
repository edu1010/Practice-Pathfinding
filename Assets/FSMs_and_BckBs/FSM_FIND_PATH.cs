using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steerings;
using Pathfinding;
namespace FSM
{
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(FIND_PATH_BLACKBOARD))]
    [RequireComponent(typeof(Seek))]
    public class FSM_FIND_PATH : FiniteStateMachine
    {
        public enum State { INITIAL, GENERATING, FOLLOWING, TERMINATED };

        public State currentState = State.INITIAL;

        FIND_PATH_BLACKBOARD blackboard;
        private Seeker seeker;
        private Path currentPath;
        private int index = 0;
        Seek seek;
        // Start is called before the first frame update
        void Awake()
        {
            blackboard = GetComponent<FIND_PATH_BLACKBOARD>();
            seeker = GetComponent<Seeker>();
            seek = GetComponent<Seek>();//the rest of the points
            GameObject surrogate_target = new GameObject();
            seek.target = Instantiate(surrogate_target);
        }
        public override void ReEnter()
        {
            ChangeState(State.INITIAL);
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
                    ChangeState(State.GENERATING);
                    break;
                case State.GENERATING:
                    break;
                case State.FOLLOWING:
                    if (SensingUtils.DistanceToTarget(gameObject, blackboard.target) <= blackboard.pointReachedRadius) 
                    {
                        ChangeState(State.TERMINATED);
                        break;
                    }
                    if (currentPath.vectorPath.Count <= 1)
                    {
                        ChangeState(State.GENERATING);
                        break;
                    }
                    float distance = (transform.position - currentPath.vectorPath[index]).magnitude;
                    if (distance <= blackboard.pointReachedRadius)
                    {
                        index++;
                        if(index>= currentPath.vectorPath.Count)
                        {
                            ChangeState(State.TERMINATED);
                            break;
                        }
                        seek.target.transform.position = currentPath.vectorPath[index];
                    }
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
                    seek.enabled = false;
                    break;
                case State.TERMINATED:
                    blackboard.terminated = false;
                    break;
            }

            // ENTER STATE LOGIC. Depends on newState
            switch (newState)
            {
                case State.INITIAL:

                    break;
                case State.GENERATING:
                    seeker.StartPath(this.transform.position, blackboard.target.transform.position, OnPathComplete);
                    break;
                case State.FOLLOWING:
                    index = 0;
                    seek.enabled = true;
                    seek.target.transform.position = currentPath.vectorPath[index];
                    
                    break;
                case State.TERMINATED:
                    blackboard.terminated = true;
                    break;
            }

            currentState = newState;
        }

        
        public void OnPathComplete(Path p)
        {
            // this is a "callback" method. if this method is called, a path has been computed and "stored" in p
            currentPath = p;
            ChangeState(State.FOLLOWING);
        }
        
    }

}