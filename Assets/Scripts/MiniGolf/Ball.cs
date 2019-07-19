using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public GameObject MG_;
    private Vector3 mgPosition;
    private Vector3 ballposition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        mgPosition = MG_.transform.position;
        ballposition = GameObject.Find("golfBall").transform.position;

        
        // if (Input.GetKeyDown("z")) { GetComponent<Rigidbody>().AddRelativeForce(0, 0, 200); }
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 transform = mgPosition - ballposition;
        Vector3 force = new Vector3(0, 0, 50);
       if (collision.gameObject.name == "MG_") { GetComponent<Rigidbody>().AddForceAtPosition(force, transform); }
        
        
    }
}
