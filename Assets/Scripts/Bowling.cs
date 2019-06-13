using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/**
 * Die Klasse Bowling wird in der Szene "Room_1" zur Steuerung der Bowlingbahn genutzt.
 * Sie zählt den Punktestand und kann die Pins und die Bälle zurücksetzen.
 **/
public class Bowling : MonoBehaviour {

    public Text screen1;
    public Text screen2;
    public Text screen3;

    public float warteZeit = 2;

    private Text[] screenArray;
    private int[] points;

    private GameObject[,] bowlingBalls;
    private GameObject[,] bowlingPins;
    private Vector3[,] positionBall;
    private Vector3[,] rotationBall;
    private Vector3[,] positionPins;
    private Vector3[,] rotationPins;

    private float currentTime;
    private float triggerTime;

    private int ballLaenge;
    private Boolean[] countLanePoints;
    private Boolean[] resetLanePoints;

    /**
     * Die Methode wird zu Beginn einmal aufgerufen.
     * 
     * In der "Start" Methode werden die initiale Rotation und Position der Bälle und Pins gespeichert.
    **/
    void Start() {
        screenArray = new Text[] {screen1, screen2, screen3};
        points = new int[] { 0, 0, 0};
        countLanePoints = new Boolean[] {false, false, false };
        resetLanePoints = new Boolean[] {false, false, false };

        currentTime = Time.time;

        ballLaenge = GameObject.FindGameObjectsWithTag("bowlingBall").Length;   //Suchen von allen GameObjekten, die mit dem Tag "bowlingBall" gekennzeichnet sind. Um die Menge an Bällen im Spiel zu erkennen.
        bowlingBalls = new GameObject[ballLaenge / 3, ballLaenge / 3];

        positionBall = new Vector3[ballLaenge / 3, ballLaenge / 3];
        rotationBall = new Vector3[ballLaenge / 3, ballLaenge / 3];

        bowlingPins = new GameObject[3, 10];
        positionPins = new Vector3[3, 10];
        rotationPins = new Vector3[3, 10];

        fillPinArray();             //Speicherung der Pin Objekten + Rotation und Position
        fillBallArray();            //Speicherung der Bowlingkugeln Objekten
        fillBallPositionAndRotation();  //Speicherung der Position und Rotation der  Kugeln
    }

    /**
     * In der Methode "fillPinArray" werden die drei Pin Sets ausgelesen und in ein Array geschrieben.
     * Zusätzlich wird deren initale Rotation und Position gespeichert.
     **/
    private void fillPinArray()
    {
        for (int i = 0; i < 3; i++)
        {
            String search = "Pin_set" + (i + 1);
            GameObject[] pins = new GameObject[10];
            pins = GameObject.FindGameObjectsWithTag(search);   //Suchen von allen GameObjekten, die mit dem Tag "Pin_set(i+1)" gekennzeichnet sind.
                                                                //In unserem Fall gibt es drei Bahnen auf denen jeweils ein Pin Set existiert.
            for (int j = 0; j < 10; j++)
            {
                bowlingPins[i, j] = pins[j];
                positionPins[i, j] = pins[j].transform.position;
                rotationPins[i, j] = pins[j].transform.eulerAngles;
            }
        }
    }

    /**
     * In der Methode "fillBallArray" werden die Bowlingkugeln ausgelesen und in ein Array geschrieben.
     **/
    private void fillBallArray()
    {
        GameObject[] tempObject = new GameObject[ballLaenge];
        tempObject = GameObject.FindGameObjectsWithTag("bowlingBall");  //Suchen von allen GameObjekten, die mit dem Tag "bowlingBall" gekennzeichnet sind.


        for(int i = 0; i < ballLaenge; i++)
        {
            int a = (int)Char.GetNumericValue(tempObject[i].name[13]);
            int b = (int)Char.GetNumericValue(tempObject[i].name[15]);

            bowlingBalls[(a-1), (b-1)] = tempObject[i];
        }
    }

    /**
     * Die Methode "fillBallPositionAndRotation" wird die initiale Position und Rotation der Bowlingkugeln gespeichert.
     **/
    private void fillBallPositionAndRotation()
    {
        for(int i = 0; i < ballLaenge / 3; i++)
        {
            for(int j = 0; j < ballLaenge / 3; j++)
            {                
                positionBall[i, j] = bowlingBalls[i, j].transform.position;
                rotationBall[i, j] = bowlingBalls[i, j].transform.eulerAngles;                
            }
        }
    }

    /**
     * Die Methode "MyTriggerEnter" wird durch das Skript "MyTriggerEvent" aufgerufen.
     * Dies passiert, wenn ein Bowlingball den Collider am Ende der Bahn berührt.
     * 
     * Der Ball wird zurückgesetzt und durch setzen der "triggerTime" und "countLanePoints[x] = true"
     * wird das Zählen der Punkte angestoßen. Nach x Sekunden wird der Punktestand berechnet.
     **/
    public void MyTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Length > 12)
        {
            if (collider.gameObject.name.Substring(0, 12) == "Bowling_ball")
            {
                resetBall(collider.gameObject.name);

                countLanePoints[(int)Char.GetNumericValue(collider.gameObject.name[13]) - 1] = true;
                triggerTime = Time.time;
            }
        }
    }

    /**
     * In der Methode "countPoints" werden die Punkte der jeweiligen Spielbahn(lane) zusammengezählt.
     * Dabei wird jeder Pin der zwischen 45 und 315 Grad gedreht ist, mit 10 Punkten bewertet.
     * 
     * Bei einem Strike gibt es 20 Bonuspunkte.
     **/
    private void countPoints(int lane)
    {        
        if (resetLanePoints[lane - 1])
        { 
            points[lane - 1] = 0;
            resetLanePoints[lane - 1] = false;
        }

        int zähler = 0;
        String search = "Pin_set" + (lane);
        GameObject[] pins = new GameObject[10];
        pins = GameObject.FindGameObjectsWithTag(search);

        for (int i = 0; i < pins.Length; i++)
        {
            if ((Math.Abs(pins[i].transform.eulerAngles.x) > 45 && Math.Abs(pins[i].transform.eulerAngles.x) < 315) || (Math.Abs(pins[i].transform.eulerAngles.z) > 45 && Math.Abs(pins[i].transform.eulerAngles.z) < 315))
            {
                points[lane - 1] = points[lane - 1] + 10;
                pins[i].SetActive(false);
                zähler++;
            }
            if(zähler == 10)
            {
                points[lane - 1] = points[lane - 1] + 20;
            }
        }        
        displayScore(lane);
    }
    
    /**
     * Die Methode "resetBallsAndPins" setzt die Bowlingkugeln und Pins, der jeweiligen Spielbahn(lane), an ihren initialen Standort zurück.
     * 
     * Diese Methode wird durch den blauen Reset-Button aufgerufen.
     **/
    public void resetBallsAndPins(int lane)
    {  
        for (int i = 0; i < ballLaenge / 3; i++)
        {
            bowlingBalls[lane - 1, i].GetComponent<Rigidbody>().isKinematic = true;
        }

        for (int i = 0; i < ballLaenge / 3; i++)
        {
            bowlingBalls[lane - 1, i].transform.position = positionBall[lane - 1, i];
            bowlingBalls[lane - 1, i].transform.eulerAngles = rotationBall[lane - 1, i];

            bowlingBalls[lane - 1, i].GetComponent<Rigidbody>().isKinematic = false;
        }

        resetPins(lane);
    }

    /**
     * Die Methode "resetBall" setzt die übergebene Bowlingkugel auf ihren initialen Standort zurück.
     **/
    private void resetBall(String ballName)
    {
        int a = (int)Char.GetNumericValue(ballName[13]);
        int b = (int)Char.GetNumericValue(ballName[15]);

        bowlingBalls[a - 1, b - 1].GetComponent<Rigidbody>().isKinematic = true;
    
        bowlingBalls[a - 1, b - 1].transform.position = positionBall[a - 1, b - 1];
        bowlingBalls[a - 1, b - 1].transform.eulerAngles = rotationBall[a - 1, b - 1];

        bowlingBalls[a - 1, b - 1].GetComponent<Rigidbody>().isKinematic = false;
    }

    private void resetPoints(int lane)
    {
        resetLanePoints[lane - 1] = true;
        points[lane - 1] = 0;
        displayScore(lane);
    }

    private void displayScore(int lane)
    {
        screenArray[lane - 1].text = "Scoreboard\n\nPunktzahl:\n" + points[lane - 1];
    }

    /**
     * Diese Methode wird von dem roten Reset-Button aufgerufen.
     **/
    public void resetLane(int lane)
    {
        resetPoints(lane);
        resetBallsAndPins(lane);
    }

    /**
     * Die Methode "resetPins" setzt die Pins für die jeweilige Spielbahn(lane) auf ihre initiale Position zurück.
     **/
    private void resetPins(int lane)
    {
        for (int i = 0; i < 10; i++)
        {
            bowlingPins[lane - 1, i].GetComponent<Rigidbody>().isKinematic = true;
        }

        for (int i = 0; i < 10; i++)
        {
            bowlingPins[lane - 1, i].transform.position = positionPins[lane - 1, i];
            bowlingPins[lane - 1, i].transform.eulerAngles = rotationPins[lane - 1, i];

            bowlingPins[lane - 1, i].GetComponent<Rigidbody>().isKinematic = false;
            bowlingPins[lane - 1, i].SetActive(true);
        }
    }

    /**
     * Diese Methode wird bei jedem Frame aufgerufen.
     **/
    void Update () {
        currentTime = Time.time;

        for(int i = 1; i <= 3; i++)
        {
            String search = "Pin_set" + (i);
            GameObject[] pins = GameObject.FindGameObjectsWithTag(search);

            if(pins.Length == 0)
            {
                resetPins(i);
            }
        }

        if (countLanePoints[0])
        {
            if (triggerTime + warteZeit < currentTime)  //Nach 2 (Standardmäßig) Sekunden werden die Punkte addiert.
            {                                           //So wird sichergestellt, dass die Punkte nicht zu früh gezählt werden
                countPoints(1);                         //(Falls sich noch Pins im Fallen befinden)
                countLanePoints[0] = false;
            }            
        } 
        else if(countLanePoints[1])
        {
            if (triggerTime + warteZeit < currentTime)  
            {                                           
                countPoints(2);                         
                countLanePoints[1] = false;
            }            
        }
        else if (countLanePoints[2])
        {
            if (triggerTime + warteZeit < currentTime)
            {
                countPoints(3);
                countLanePoints[2] = false;
            }            
        }
    }
}
