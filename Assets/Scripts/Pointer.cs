//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;

public struct PointerEventArgs
{
    public uint controllerIndex;
    public uint flags;
    public float distance;
    public Transform target;
}

public delegate void PointerEventHandler(object sender, PointerEventArgs e);

/**
 * Die Klasse Pointer der Valve Corporation wurde ein wenig abgeändert. (Von einer älteren Version)
 * Hierbei wird lediglich ein Objekt erzeugt (Cube), welches eine breite von 0.002f und eine Länge von 100f besitzt.
 * Dieses Objekt wird zu Beginn der Szene an den rechten Controller gebunden. 
**/
public class Pointer : MonoBehaviour
{
    public Color color;
    public float thickness = 0.002f;
    public GameObject holder;
    public GameObject pointer;
    public GameObject block;

    /**
     * Die Methode wird zu Beginn einmal aufgerufen. Meist für Initialisierung genutzt.
    **/
    void Start()
    {
        holder = new GameObject();
        holder.transform.parent = this.transform;
        holder.transform.localPosition = Vector3.zero;
        holder.transform.localRotation = Quaternion.identity;

        pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.transform.parent = holder.transform;
        pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
        pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
        pointer.transform.localRotation = Quaternion.identity;

        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", color);
        pointer.GetComponent<MeshRenderer>().material = newMaterial;
        block.transform.parent = pointer.transform;
    }

    /**
     * Diese Methode wird bei jedem Frame aufgerufen.
     **/
    void Update()
    {
        
    }
}
