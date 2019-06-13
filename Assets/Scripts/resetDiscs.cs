using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Die Klasse resetDiscs wird zum Zurücksetzen der Disketten genutzt.
 **/
public class resetDiscs : MonoBehaviour {

    private GameObject[] discs;
    private Vector3[] position;
    private Vector3[] rotation;
    //private Transform[] transform;

    /**
     * Die Methode wird zu Beginn einmal aufgerufen.
     * 
     * Hier werden zunächst die benötigten GameObjekte gesucht und ihre initiale Position und Rotation gespeichert.
    **/
    void Start () {
        discs = GameObject.FindGameObjectsWithTag("Disc");  //Suchen von allen GameObjekten, die mit dem Tag "Disc" gekennzeichnet sind.
        position = new Vector3[discs.Length];
        rotation = new Vector3[discs.Length];

		for(int i = 0; i <= discs.Length-1; i++)
        {
            position[i] = discs[i].transform.position;
            rotation[i] = discs[i].transform.eulerAngles;
        }
	}

    /**
     * Diese Methode wird genutzt, um die Disketten an ihren Ursprungsort zurückzusetzen.
     * Hierzu wird Rotation und Position auf den Startwert zurückgesetzt.
     * 
     * Die Methode wird durch den Reset-Button ausgelöst.
     **/
    public void resetAllDiscs()
    {
        for (int i = 0; i <= discs.Length-1; i++)
        {
            discs[i].transform.position = position[i];
            discs[i].transform.eulerAngles = rotation[i];
        }
    }

    /**
     * Diese Methode wird bei jedem Frame aufgerufen.
     **/
    void Update () {
		
	}
}
