using System.Collections;
using System.Collections.Generic;
using System;
using HTC.UnityPlugin.Vive;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectSpawn : MonoBehaviour {

    public Transform target;
    public GameObject myBlock;
    public GameObject mySphere;
    public GameObject myCylinder;
    public GameObject head;
    public GameObject rightHand;
    public GameObject objectContainer;

    private GameObject[] myObjects;
    private float speed;
    private int counter;
    private int objectCounter;
    private int selectedObject;
    private String objectName;
    private Scene scene;
    // Use this for initialization
    void Start () {
        scene = SceneManager.GetActiveScene(); //Abfragen der aktuell geladenen Szene   
        myObjects = new GameObject[] { myBlock, myCylinder, mySphere };
        objectName = "";
        speed = 50f;
        counter = 0;
        objectCounter = 0;
        selectedObject = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if(ViveInput.GetPress(HandRole.RightHand, ControllerButton.Grip))
        {
            GameObject newObject = Instantiate(myObjects[selectedObject]) as GameObject; ;
            objectName = "Object_" + objectCounter;
            newObject.name = objectName;
            newObject.transform.position = rightHand.transform.position;
            newObject.transform.position += new Vector3(0, 0.1F, 0);

            newObject.transform.parent = objectContainer.transform;
            objectCounter++;
        }

        if(ViveInput.GetPress(HandRole.RightHand, ControllerButton.Menu))
        {
            if (selectedObject == 2) selectedObject = 0;
            else selectedObject++;
        }
	}
}
