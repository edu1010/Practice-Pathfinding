using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steerings;
using Pathfinding;
namespace FSM
{
    [RequireComponent(typeof(PathFollowing))]
    [RequireComponent(typeof(PathFeeder))]
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(FIND_PATH_BLACKBOARD))]
    public class FSM_FIND_PATH : FiniteStateMachine
    {
        public enum State { INITIAL, GENERATING, FOLLOWING, TERMINATED };

        public State currentState = State.INITIAL;

        FIND_PATH_BLACKBOARD blackboard;
        public PathFeeder pathFeeder;
        PathFollowing pathfollowing;
        public GameObject currentWaypoint;

        //GameObject[] wanderPoints;
        /*
        PathFeeder pathFeeder;
        public GameObject targets;
        private GameObject currentTar;
        LADYBUG_BLACKBOARD blckboard;
        private Path currentPath;
        private Seeker seeker;
        */

        // Start is called before the first frame update
        void Start()
        {
            //seeker = GetComponent<Seeker>();
            pathfollowing = GetComponent<PathFollowing>();
            pathFeeder = GetComponent<PathFeeder>();
            blackboard = GetComponent<FIND_PATH_BLACKBOARD>();
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
            pathFeeder.enabled = false;
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
                    if (SensingUtils.DistanceToTarget(gameObject, currentWaypoint) <= blackboard.pointReachedRadius) //poner una  variable para pointReachedRadius
                    {
                        ChangeState(State.INITIAL);
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
                    /*
                    if (currentPath != null)
                    {
                        ChangeState(State.FOLLOWING);
                    }
                    break;
                    */
                case State.FOLLOWING:
                    pathFeeder.enabled = false;
                    pathFeeder.OnPathComplete(pathfollowing.path);
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
                    //seeker.StartPath(this.transform.position, blckboard.target.transform.position, OnPathComplete);
                    break;
                case State.FOLLOWING:
                    //currentWaypoint = blackboard.GetRandomWanderPoint();
                    pathFeeder.enabled = true;
                    pathFeeder.target = currentWaypoint;
                    //pathFeeder.target = currentTar;
                    break;
                case State.TERMINATED:

                    break;
            }

            currentState = newState;
        }

        /*
        public void OnPathComplete(Path p)
        {
            // this is a "callback" method. if this method is called, a path has been computed and "stored" in p
            currentPath = p;
        }
        */
    }

}