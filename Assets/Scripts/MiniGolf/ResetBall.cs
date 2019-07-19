using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBall : MonoBehaviour {

    private Vector3 ballposittion;

    // Use this for initialization
    void Start () {
        ballposittion = GameObject.Find("golfBall").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void resetBallPosition()
    {
        GameObject.Find("golfBall").transform.position = ballposittion;
    }
}
