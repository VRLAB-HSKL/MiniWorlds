using HTC.UnityPlugin.Vive;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockspawnNew : MonoBehaviour
{
    public GameObject myCube;
    public GameObject myCylinder;
    public GameObject mySphere;
    public GameObject objectContainer;
    public GameObject rightHand;

    private int selectedObject;
    private GameObject[] myObjects;
    private int objectCounter;
    private String objectName;
    private bool spawn;
    
    


    // Start is called before the first frame update
    void Start()
    {
        selectedObject = -1;
        myObjects = new GameObject[] { myCube, myCylinder, mySphere };
        objectCounter = 0;
        objectName = "";
        spawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void selectedObjectCounter()
    {
        //Debug.Log("selectedObject Counter : " + selectedObject);
        if (selectedObject == 2)
        {
            selectedObject = 0;
            switchObject();
        }
        else
        {
           selectedObject++;
           switchObject();
        }
        
    }


    private void switchObject()
    {
        //selectedObjectCounter();
        switch (selectedObject)
        {
            case 0: myObjects[0].GetComponent<MeshRenderer>().material.color = Color.green; break;
            case 1: myObjects[1].GetComponent<MeshRenderer>().material.color = Color.green; break;
            case 2: myObjects[2].GetComponent<MeshRenderer>().material.color = Color.green; break;
            //case 3: myObjects[2].GetComponent<MeshRenderer>().material.color = Color.green; break;
        }

        switchColor();
    }

    private void switchColor()
    {
        for(int i = 0; i < myObjects.Length; i++)
        {
            if (i != selectedObject)
            {
                myObjects[i].GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
        }
    }

    private void spawnObject()
    {
        GameObject newObject = null;
        if (selectedObject == 3)
        {
            newObject = Instantiate(myObjects[selectedObject - 1]) as GameObject;
        }
        else {
            Debug.Log("selectedObject Counter : " + selectedObject);
            newObject = Instantiate(myObjects[selectedObject]) as GameObject;
            objectName = "Object_" + objectCounter;
            newObject.name = objectName;
            newObject.transform.position = rightHand.transform.position;
            newObject.transform.position += new Vector3(0, 0.1F, 0);

            newObject.transform.parent = objectContainer.transform;
            objectCounter++;
        }
        
        
    }

    private void Awake()
    {
        ViveInput.AddListenerEx(HandRole.RightHand, ControllerButton.Grip, ButtonEventType.Down, spawnObject);
        ViveInput.AddListenerEx(HandRole.LeftHand, ControllerButton.Grip, ButtonEventType.Down, selectedObjectCounter);
    }

    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(HandRole.RightHand, ControllerButton.Grip, ButtonEventType.Down, spawnObject);
        ViveInput.RemoveListenerEx(HandRole.LeftHand, ControllerButton.Grip, ButtonEventType.Down, selectedObjectCounter);
    }
}
