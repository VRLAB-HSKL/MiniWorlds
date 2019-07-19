using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/**
 * Die Klasse Load_Room wird in der Szene "MainRoom" verwendet. Sie dient zum zurücksetzen der Disketten
 * und zum Laden der verschiedenen Räume. Zusätzlich ermöglicht sie das ein- und ausfahren der Disketten.
 **/
public class Load_Room : MonoBehaviour {

    public Text screen;         //Computerscreen wird genutzt um den Text der ausgewählten Diskette anzuzeigen.
    public AudioSource source;  //Spielt den Diskettenladesound ab.
    //public GameObject ejectButton;

    private Boolean move;
    private Boolean ejectable;
    private Boolean fillScreen;
    private Boolean loading;
    private Boolean showIntro;
    private Boolean discInside;
    private String disc_name;
    private String ladebalken;
    private GameObject disc;
    private int counter;
    private int moveCounter;
    private String startText;

    /**
     * Die Methode wird zu Beginn einmal aufgerufen. Meist für Initialisierung genutzt.
    **/
    void Start () {
        fillScreen = false;
        showIntro = false;
        move = false;
        loading = false;
        ejectable = false;
        discInside = false;
        ladebalken = "[....................]";
        disc_name = "";
        counter = 0;
        moveCounter = 0;
        source.Stop();

        startText = screen.text;
    }

    /**
     * Diese Methode wird immer dann aufgerufen, wenn ein Objekt mit dem festgelegten Collider (Collider als isTrigger) kollidiert.
     **/
    private void OnTriggerEnter(Collider discCol)
    {
        if(discCol.gameObject.name.Substring(0,4) == "Disc" && !loading)
        {
            loading = true;
            disc_name = discCol.gameObject.name;
            disc_name = disc_name.Substring(5);

            disc = discCol.gameObject;
            move = true;

            fillText();            
        }
    }

    /**
     * Die Methode "moveDisc" zieht die ausgewählte Disc in das Laufwerk ein, sofern sich nicht bereits eine darin befindet.    
     **/
    private void moveDisc()
    {
        if(!discInside)
        {            
            if (moveCounter >= 150)
            {
                move = false;
                moveCounter = 0;
                discInside = true;

                Rigidbody rb = disc.GetComponent<Rigidbody>();
                rb.useGravity = false;

                disc.GetComponent<BoxCollider>().enabled = false;
            }
            moveCounter++;

            //Zieht die Diskette in das Laufwerk.
            float speed = 1f;
            Vector3 direction = new Vector3(0f, 0f, 0.1f);
            disc.transform.position += direction * speed * Time.deltaTime;
        }
    }

    /**
     * Die Methode "ejectDisc" stellt den Gegensatz zu "moveDisc" dar. 
     * Zusätzlich dazu wird der Starttext im Computerbildschirm angezeigt.
     **/
    private void ejectDisc()
    {
        if (discInside)
        {
            if (moveCounter >= 200)
            {
                ejectable = false;
                discInside = false;
                moveCounter = 0;
                screen.text = startText;

                disc.GetComponent<BoxCollider>().enabled = true;

                Rigidbody rb = disc.GetComponent<Rigidbody>();
                rb.useGravity = true;

                loading = false;
            }
            moveCounter++;

            //Eject the disc.
            float speed = 1f;
            Vector3 direction = new Vector3(0f, 0f, -0.1f);
            disc.transform.position += direction * speed * Time.deltaTime;
        }
    }

    /**
     * Die Methode "eject" wird beim Betätigen des Buttons am Laufwerk aufgerufen.
     **/
    public void eject()
    {
        ejectable = true;

        counter = 0;
        ladebalken = "[....................]";
        fillScreen = false;
        loading = false;

        source.Stop();  //Stoppt die Musikwiedergabe der gewählten AudioSource.
    } 

    /**
     * Die Methode "loadRoom" wird beim Betätigen des Play-Buttons aufgerufen.
     **/
    public void loadRoom()
    {
        loadRoom(disc_name);
    }

    private void loadRoom(String room)
    {
        switch (room)
        {
            case "Room_1":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Room_1");
                break;
            case "Room_2":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Room_2");
                break;
            case "Room_3":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Room_3");
                break;
            case "Room_4":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Room_4");
                break;
            case "Room_5":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Room_5");
                break;
            case "Room_6":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Room_6");
                break;           
            //case "Room_7":
            //    UnityEngine.SceneManagement.SceneManager.LoadScene("Room_7");
            //    break;
        }

        
    }

    private void fillText()
    {
        fillScreen = true;
    }

    /**
     * Diese Methode wird bei jedem Frame aufgerufen.
     **/
    void Update () {

        if (move)
        {
            moveDisc();
        }
        
        if(ejectable)
        {
            ejectDisc();
        }

        if (fillScreen && !move)
        {
            if (!source.isPlaying) source.Play(0);

            /**
             * Hier wird der digitale Ladebalken erzeugt.
             * Dazu wird ein counter hochgezählt und bei jedem 25. Frame ein zusätzliches "#" geschrieben.
             **/
            if(counter % 25 == 0 && counter != 0)
            {
                char[] ca = ladebalken.ToCharArray();
                ca[counter / 25] = '#';
                ladebalken = new String(ca);
            }

            screen.text = "[Unity@vr_lab]$ load " + disc_name + "\n" + disc_name + " ausgewählt! " + "Es wird geladen... \n" + "Progress: " + "[" +  counter/5 + "%] " + ladebalken;
            counter++;
        }

        if (counter == 501)
        {
            fillScreen = false;
            counter = 0;
            ladebalken = "[....................]";

            showIntro = true;
            source.Stop();
        }

        /**
         * Wenn die Variable "showintro" auf "true" gesetzt wurde, wird geschaut welche Diskette eingelegt wurde.
         * Daraufhin wird die richtige Beschreibung auf dem Bildschirm ausgegeben.
         **/
        if(showIntro)
        {
            switch(disc.gameObject.name.Substring(5))
            {
                case "Room_1":
                    screen.text = "Raum 1 - Bowling" + "\n\n" + "In diesem Raum kannst du dich im Bowling versuchen." + "\n\n" + "Durch betätigen des grünen Buttons gelangst du in diese Welt";
                    break;
                case "Room_2":
                    screen.text = "Raum 2 - Blockworld" + "\n\n" + "In diesem Raum kannst du verschiedenste Dinge mit Blöcken erbauen." + "\n\n" + "Durch betätigen des grünen Buttons gelangst du in diese Welt";
                    break;
                case "Room_3":
                    screen.text = "Raum 3 - Minigolf" + "\n\n" + "In diesem Raum kannst du deine Fähigkeiten im Minigolf unter Beweis stellen. Wenn er denn während der Projektlaufzeit fertig geworden wäre ;)" + "\n\n" + "Durch betätigen des grünen Buttons gelangst du in diese Welt";
                    break;
                case "Room_4":
                    screen.text = "Raum 4 - Hit the Target" + "\n\n" + "In diesem Raum kannst du deine Fähigkeit im Treffen von Zielen unter Beweis stellen. Dazu stehen dir Bälle und ein Bogen zur Verfügung." + "\n\n" + "Durch betätigen des grünen Buttons gelangst du in diese Welt";
                    break;
                case "Room_5":
                    screen.text = "Raum 5 - Earth" + "\n\n" + "In diesem Raum kannst du über eine generierte Karte der Welt fliegen. (In diesem Projekt zunächst nur San Francisco und Umgebung.)" + "\n\n" + "Durch betätigen des grünen Buttons gelangst du in diese Welt";
                    break;                
                case "Room_7":
                    screen.text = "Raum 7 - Labyrinth" + "\n\n" + "Easter Egg found!";
                    break;
            }            
            showIntro = false;
        }
    }
}