using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSpawner : MonoBehaviour {

    public GameObject egg_ant;
    public GameObject seed_ant;

    float timer;
    float probabilitySpawnASeed = 0.8f;

	// Use this for initialization
	void Start () 
    {
        // get the prefabs
        timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {    
        timer += Time.deltaTime;
        
        if(timer >= Random.Range(10.0f, 15.0f))
        {
            if(Random.Range(0.0f, 1.0f) > probabilitySpawnASeed)
            {
                Instantiate(egg_ant, transform.position, transform.rotation);
                timer = 0.0f;
            }
            else
            {
                Instantiate(seed_ant, transform.position, transform.rotation);
                timer = 0.0f;
            }        
        }
    }
}
