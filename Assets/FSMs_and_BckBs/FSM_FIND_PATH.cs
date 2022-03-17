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
       // public GameObject currentWaypoint;//Esto lo usamos de target entiendo?
        private Seeker seeker;
        private Path currentPath;
        private int index = 0;
        Seek seek;
        bool lastPoint = false;
        // Start is called before the first frame update
        void Awake()
        {
            //seeker = GetComponent<Seeker>();
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
                    if (SensingUtils.DistanceToTarget(gameObject, blackboard.target) <= blackboard.pointReachedRadius) //poner una  variable para pointReachedRadius
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

                    
                   /*profe:
                    * if (currentWaypointIndex == path.vectorPath.Count - 1)
                // use arrive for the last waypoint
                return Arrive.GetSteering(ownKS, SURROGATE_TARGET, wayPointReachedRadius/2, wayPointReachedRadius*2);
            else 
			    return Seek.GetSteering(ownKS, SURROGATE_TARGET);*/
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
                    lastPoint = false;
                    
                    break;
                case State.TERMINATED:
                    blackboard.terminated = true;
                    //blackboard.SetTargetToWander();//REVISAR QUE HACER NO SE SI SIEMRPRE AHI QUE IR A WANDER
                   // ChangeState(State.GENERATING);
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