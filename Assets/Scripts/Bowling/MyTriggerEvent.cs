using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * MyTriggerevent ist eine Hilfsklasse für den Bowlingraum.
 * Sie wird getriggered, wenn eine Kugel den Collider berührt.
 * Dabei ruft sie dann die Hauptklasse Bowling über den gameController auf.
 **/
public class MyTriggerEvent : MonoBehaviour {

    public GameObject gameController;

    /**
     * Die Methode wird zu Beginn einmal aufgerufen. Meist für Initialisierung genutzt.
     **/
    void Start () {
		
	}

    /**
     * Diese Methode wird immer dann aufgerufen, wenn ein Objekt mit dem festgelegten Collider (Collider als isTrigger) kollidiert.
     **/
    private void OnTriggerEnter(Collider collider)
    {
        gameController.GetComponent<Bowling>().MyTriggerEnter(collider); //Ruft die Methode "MyTriggerEnter" mit dem aktuellen collider (der jeweiligen Bowlingkugel) auf.
    }

    /**
     * Diese Methode wird bei jedem Frame aufgerufen.
     **/
    void Update () {
		
	}
}
