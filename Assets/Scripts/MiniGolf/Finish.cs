using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour {

    public Text Screen1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "golfBall")
        {
            GameObject.Find("ResetButton").GetComponent<ResetBall>().resetBallPosition();
            Screen1.text = "Scoreboard\n\nPunktzahl:\n" + 10;

        }
    }

    public void resetScore()
    {
        Screen1.text = "Scoreboard\n\nPunktzahl:\n";
    }
}
