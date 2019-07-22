using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit_Teleport : MonoBehaviour {

    private Scene scene; //Variable zum speichern der aktuellen Szene
    private SphereCollider sphereCollider;
    /**
     * Die Methode wird zu Beginn einmal aufgerufen. Meist für Initialisierung genutzt.
    **/
    void Start () {
        scene = SceneManager.GetActiveScene(); //Abfragen der aktuell geladenen Szene
        //sphereCollider = GameObject.Find("ViveRig").GetComponent<SphereCollider>();
    }

    /**
     * Diese Methode wird bei jedem Frame aufgerufen.
     **/
    void Update () {
		
	}

    /**
     * Diese Methode wird immer dann aufgerufen, wenn ein Objekt mit dem festgelegten Collider (Collider als isTrigger) kollidiert.
     **/
    private void OnTriggerEnter(Collider collider)  //Wird aufgerufen wenn der etwas den Collider berührt.
    {                                               //collider ist hier das Objekt, welches den festen Collider berührt.
                                                    // if (collider.gameObject.name == "HeadCollider") 
                                                    // if (collider == sphereCollider)
        if (collider.gameObject.GetComponent<SphereCollider>())
        {            
            if(scene.name == "MainRoom")
            {
                Application.Quit();

                #if UNITY_EDITOR
                     UnityEditor.EditorApplication.isPlaying = false;
                #endif
            }
            else
            {
                SceneManager.LoadScene("MainRoom");
            }
        }
    }
}
