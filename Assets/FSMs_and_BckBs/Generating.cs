using UnityEngine;
using System.Collections;
using Pathfinding;
using Steerings;

public class Generating : MonoBehaviour
{
	public GameObject target;
	//public float repathTime = 1; // "recalculate" path every repathTime seconds

	private Seeker seeker;
	//private PathFollowing pathFollowingSteering;
	private Path currentPath;

	private float elapsedTime = 0f;  // time elapsed since last repathing op.

	// Use this for initialization
	void Start()
	{
		seeker = GetComponent<Seeker>();
		if (seeker == null)
			Debug.LogError("No seeker attached in PathFeeder");

		

		// start the path computation process
		seeker.StartPath(this.transform.position, target.transform.position, OnPathComplete);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void OnPathComplete(Path p)
	{
		// this is a "callback" method. if this method is called, a path has been computed and "stored" in p
		currentPath = p;
	}
}
