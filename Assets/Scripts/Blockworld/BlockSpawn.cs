using System.Collections;
using System.Collections.Generic;
using System;
using HTC.UnityPlugin.Vive;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockSpawn : MonoBehaviour
{

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
    void Start()
    {
        scene = SceneManager.GetActiveScene(); //Abfragen der aktuell geladenen Szene   
        myObjects = new GameObject[] { myBlock, myCylinder, mySphere };
        objectName = "";
        speed = 50f;
        counter = 0;
        objectCounter = 0;
        selectedObject = 0;
    }

    private void Awake()
    {
        ViveInput.AddListenerEx(HandRole.RightHand, ControllerButton.Grip, ButtonEventType.Down, spawnObject);
        ViveInput.AddListenerEx(HandRole.RightHand, ControllerButton.Menu, ButtonEventType.Down, switchObject);
    }

    private void OnDestroy()
    {
        ViveInput.RemoveListenerEx(HandRole.RightHand, ControllerButton.Grip, ButtonEventType.Down, spawnObject);
        ViveInput.RemoveListenerEx(HandRole.RightHand, ControllerButton.Menu, ButtonEventType.Down, switchObject);
    }

    // Update is called once per frame
    void Update()
    {
        // if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.Grip))
        // {

    }

    //if (ViveInput.GetPress(HandRole.RightHand, ControllerButton.Menu))
    //{
    //   if (selectedObject == 2) selectedObject = 0;
    //   else selectedObject++;
    //}
    //}

    private void spawnObject()
    {
        GameObject newObject = Instantiate(myObjects[selectedObject]) as GameObject; ;
        objectName = "Object_" + objectCounter;
        newObject.name = objectName;
        newObject.transform.position = rightHand.transform.position;
        newObject.transform.position += new Vector3(0, 0.1F, 0);

        newObject.transform.parent = objectContainer.transform;
        objectCounter++;
    }

    private void switchObject()
    {
        if (selectedObject == 2) selectedObject = 0;
        else selectedObject++;
    }
}
