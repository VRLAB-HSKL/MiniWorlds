using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Die Klasse resetThrow wird verwendet, um in der Szene Room_4 
 * die Wurfelemente und Zielelemente zurückzusetzen
 **/
public class resetThrow : MonoBehaviour {

    private GameObject[] block;
    private GameObject[] glas;
    private GameObject[] ball;
    private Vector3[] positionBlock;
    private Vector3[] rotationBlock;
    private Vector3[] positionGlas;
    private Vector3[] rotationGlas;
    private Vector3[] positionBall;
    private Vector3[] rotationBall;

    /**
     * Die Methode wird zu Beginn einmal aufgerufen.
     * 
     * Hier werden die initialen Positionen 
     * der Bälle (Tag = "throwBall")
     * der Zielblöcke (Tag = "throwBlock") und
     * der Glasflaschen (Tag = "glas"= gespeichert.
    **/
    void Start () {

        ball = GameObject.FindGameObjectsWithTag("throwBall");      //Suchen von allen GameObjekten, die mit dem Tag "throwBall" gekennzeichnet sind.
        positionBall = new Vector3[ball.Length];
        rotationBall = new Vector3[ball.Length];

        block = GameObject.FindGameObjectsWithTag("throwBlock");    //Suchen von allen GameObjekten, die mit dem Tag "throwBlock" gekennzeichnet sind.
        positionBlock = new Vector3[block.Length];
        rotationBlock = new Vector3[block.Length];

        glas = GameObject.FindGameObjectsWithTag("glas");           //Suchen von allen GameObjekten, die mit dem Tag "glas" gekennzeichnet sind.
        positionGlas = new Vector3[glas.Length];
        rotationGlas = new Vector3[glas.Length];

        for (int i = 0; i <= block.Length-1; i++)   //Zielblöcke Position und Rotation speichern
        {            
            positionBlock[i] = block[i].transform.position;
            rotationBlock[i] = block[i].transform.eulerAngles;
        }

        for (int i = 0; i <= glas.Length - 1; i++)  //Glasflaschen Position und Rotation speichern
        {
            positionGlas[i] = glas[i].transform.position;
            rotationGlas[i] = glas[i].transform.eulerAngles;
        }

        for (int i = 0; i <= ball.Length - 1; i++)  //Bälle Position und Rotation speichern
        {
            positionBall[i] = ball[i].transform.position;
            rotationBall[i] = ball[i].transform.eulerAngles;
        }
    }

    /**
     * Diese Methode wird verwendet, um die Bälle zurückzusetzen.
     **/
    public void resetBalls()
    {
        Rigidbody[] rb = new Rigidbody[ball.Length];
        
        for (int i = 0; i <= ball.Length - 1; i++)
        {
            rb[i] = ball[i].GetComponent<Rigidbody>();
            rb[i].isKinematic = true;   //Rigidbody.isKinematik der Bälle auf true setzen, damit ihre Bewegung verhindert wird.
        }

        for (int i = 0; i <= ball.Length - 1; i++)  //Bälle zurücksetzen
        {            
            ball[i].transform.position = positionBall[i];
            ball[i].transform.eulerAngles = rotationBall[i];

            rb[i].isKinematic = false;  //Rigidbody.isKinematik auf false setzen, damit ihre Bewegung wieder aktiviert wird.
        }        
    }

    /**
     * Diese Methode wird verwendet, um die getroffenen Ziele zurückzusetzen.
     * Zusätzlich wird die Methode "resetBalls" aufgerufen.
     **/
    public void resetTargets()
    {
        for (int i = 0; i <= block.Length-1; i++)   //Zielblöcke zurücksetzen
        {
            block[i].transform.position = positionBlock[i];
            block[i].transform.eulerAngles = rotationBlock[i];
        }

        for (int i = 0; i <= glas.Length - 1; i++)  //Glasflaschen zurücksetzen
        {
            glas[i].transform.position = positionGlas[i];
            glas[i].transform.eulerAngles = rotationGlas[i];
        }
        resetBalls();
    }

    /**
     * Diese Methode wird bei jedem Frame aufgerufen.
     **/
    void Update () {
		
	}
}
