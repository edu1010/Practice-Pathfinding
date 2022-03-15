using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steerings;
using Pathfinding;
namespace FSM
{
    [RequireComponent(typeof(PathFollowing))]
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(FIND_PATH_BLACKBOARD))]
    public class FSM_FIND_PATH : FiniteStateMachine
    {
        public enum State { INITIAL, GENERATING, FOLLOWING, TERMINATED };

        public State currentState = State.INITIAL;

        FIND_PATH_BLACKBOARD blackboard;
        PathFollowing pathfollowing;
       // public GameObject currentWaypoint;//Esto lo usamos de target entiendo?
        private Seeker seeker;
        private Path currentPath;

        // Start is called before the first frame update
        void Start()
        {
            //seeker = GetComponent<Seeker>();
            pathfollowing = GetComponent<PathFollowing>();
            blackboard = GetComponent<FIND_PATH_BLACKBOARD>();
            seeker = GetComponent<Seeker>();
            //wanderPoints = GameObject.FindGameObjectsWithTag("WANDER_POINTS");

            /*
            foreach (var variable in targets.GetComponentsInChildren<Transform>())
            {
                if (Random.Range(0f, 1f) > 0.4f)
                {
                    currentTar = variable.gameObject;
                }
            }
            */
        }

        public override void ReEnter()
        {
            ChangeState(State.INITIAL);
            /*
            foreach (var variable in targets.GetComponentsInChildren<Transform>())
            {
                if (Random.Range(0f, 1f) > 0.4f)
                {
                    currentTar = variable.gameObject;
                }
            }
            */
            base.ReEnter();
        }

        public override void Exit()
        {
            pathfollowing.enabled = false;
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
                    pathfollowing.enabled = false;
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
                    seeker.StartPath(this.transform.position, blackboard.target.transform.position, OnPathComplete);
                    break;
                case State.FOLLOWING:
                    pathfollowing.path = currentPath;
                    pathfollowing.enabled = true;
                    
                    //currentWaypoint = blackboard.GetRandomWanderPoint();
                    //pathFeeder.enabled = true;
                    // pathFeeder.target = currentWaypoint;
                    //pathFeeder.target = currentTar;
                    break;
                case State.TERMINATED:
                    blackboard.SetTargetToWander();//REVISAR QUE HACER NO SE SI SIEMRPRE AHI QUE IR A WANDER
                    ChangeState(State.GENERATING);
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