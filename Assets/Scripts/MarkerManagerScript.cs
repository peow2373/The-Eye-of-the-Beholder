using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManagerScript : MonoBehaviour
{
    public static bool palmMarker = false;
    public static bool netrixiMarker = false;
    public static bool folkvarMarker = false;
    public static bool ivMarker = false;
    public static bool pauseMarker = false;
    public static bool undoMarker = false;
    public static bool extraMarker = false;

    public GameObject hand;
    public float[] xPos = new float[] { -4.5f, 0, 4.5f };
    public float[] yPos = new float[] { 2.5f, 0, -2.5f };
    
    public float[] rotationR = new float[] { 90, 60, 30, 0, -30, -60 };
    public float[] rotationL = new float[] { -90, -60, -30, 0, 30, 60 };

    public float scaleFactor = 2f;
    public bool wasLarger = false, wasSmaller = false;

    public static bool rightHanded = true;
    public static bool beingRotated = false;

    private SpriteRenderer handSR;

    private float currentTime = 0, timer = 0.7f;
    private bool startTimer = true;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        handSR = hand.gameObject.GetComponent<SpriteRenderer>();
        handSR.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        TestForMarkers();
        
        // If Palm marker is visible
        if (palmMarker)
        {
            // Determine location, rotation, and depth of marker on webcam
            MarkerLocate();
            MarkerRotate();
            MarkerDepth();
            
            // Make hand appear 
            handSR.enabled = true;
            startTimer = true;
        }
        
        // If Palm marker is no longer visible
        else
        {
            if (startTimer)
            {
                currentTime = Time.time;
                startTimer = false;
            }

            if (Time.time - currentTime >= timer)
            {
                // Make hand disappear after a set amount of time 
                handSR.enabled = false;
            }
        }
    }

    void TestForMarkers()
    {
        // check to see if Palm marker is visible
        if (Input.GetKeyDown(KeyCode.H)) palmMarker = !palmMarker;
        
        // check to see if Netrixi marker is visible
        if (Input.GetKeyDown(KeyCode.Y)) netrixiMarker = !netrixiMarker;
        
        // check to see if Folkvar marker is visible
        if (Input.GetKeyDown(KeyCode.O)) folkvarMarker = !folkvarMarker;
        
        // check to see if Iv marker is visible
        if (Input.GetKeyDown(KeyCode.I)) ivMarker = !ivMarker;
        
        // check to see if Pause marker is visible
        if (Input.GetKeyDown(KeyCode.P)) pauseMarker = !pauseMarker;
        
        // check to see if Undo marker is visible
        if (Input.GetKeyDown(KeyCode.U)) undoMarker = !undoMarker;
        
        // check to see if Extra marker is visible
        if (Input.GetKeyDown(KeyCode.V)) extraMarker = !extraMarker;
    }

    void MarkerLocate()
    {
        // check to see if Palm marker is in the top-left corner
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector2 loc = new Vector2(xPos[0], yPos[0]);
            hand.transform.position = loc;
        }
        
        // check to see if Palm marker is in the top-middle corner
        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector2 loc = new Vector2(xPos[1], yPos[0]);
            hand.transform.position = loc;
        }
        
        // check to see if Palm marker is in the top-right corner
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 loc = new Vector2(xPos[2], yPos[0]);
            hand.transform.position = loc;
        }
        
        // check to see if Palm marker is in the middle-left corner
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector2 loc = new Vector2(xPos[0], yPos[1]);
            hand.transform.position = loc;
        }
        
        // check to see if Palm marker is in the center corner
        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector2 loc = new Vector2(xPos[1], yPos[1]);
            hand.transform.position = loc;
        }
        
        // check to see if Palm marker is in the middle-right corner
        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector2 loc = new Vector2(xPos[2], yPos[1]);
            hand.transform.position = loc;
        }
        
        // check to see if Palm marker is in the bottom-left corner
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Vector2 loc = new Vector2(xPos[0], yPos[2]);
            hand.transform.position = loc;
        }
        
        // check to see if Palm marker is in the bottom-middle corner
        if (Input.GetKeyDown(KeyCode.X))
        {
            Vector2 loc = new Vector2(xPos[1], yPos[2]);
            hand.transform.position = loc;
        }
        
        // check to see if Palm marker is in the bottom-right corner
        if (Input.GetKeyDown(KeyCode.C))
        {
            Vector2 loc = new Vector2(xPos[2], yPos[2]);
            hand.transform.position = loc;
        }
    }

    void MarkerRotate()
    {
        // if the player is right-handed
        if (rightHanded)
        {
            // check to see if Palm marker is being rotated
            if (Input.GetKeyDown(KeyCode.K))
            {
                beingRotated = !beingRotated;

                if (beingRotated)
                {
                    // put the hand into the starting position
                    hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotationR[0]);
                }
                else
                {
                    // return the hand to a vertical position
                    hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotationR[3]);
                }
            }
            
            // check to see if Palm marker is in the first zone
            if (Input.GetKeyDown(KeyCode.L))
            {
                hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotationR[1]);
                beingRotated = true;
            }
            
            // check to see if Palm marker is in the second zone
            if (Input.GetKeyDown(KeyCode.G))
            {
                hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotationR[2]);
                beingRotated = true;
            }
            
            // check to see if Palm marker is in the third zone
            if (Input.GetKeyDown(KeyCode.B))
            {
                hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotationR[3]);
                beingRotated = true;
            }
            
            // check to see if Palm marker is in the fourth zone
            if (Input.GetKeyDown(KeyCode.T))
            {
                hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotationR[4]);
                beingRotated = true;
            }
            
            // check to see if Palm marker is in the fifth zone
            if (Input.GetKeyDown(KeyCode.R))
            {
                hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotationR[5]);
                beingRotated = true;
            }
        }

        // if the player is left-handed
        else
        {
            // check to see if Palm marker is being rotated
            if (Input.GetKeyDown(KeyCode.K))
            {
                beingRotated = !beingRotated;

                if (beingRotated)
                {
                    // put the hand into the starting position
                    hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotationL[0]);
                }
                else
                {
                    // return the hand to a vertical position
                    hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotationL[3]);
                }
            }
            
            // check to see if Palm marker is in the first zone
            if (Input.GetKeyDown(KeyCode.L))
            {
                hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotationL[1]);
                beingRotated = true;
            }
            
            // check to see if Palm marker is in the second zone
            if (Input.GetKeyDown(KeyCode.G))
            {
                hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotationL[2]);
                beingRotated = true;
            }
            
            // check to see if Palm marker is in the third zone
            if (Input.GetKeyDown(KeyCode.B))
            {
                hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotationL[3]);
                beingRotated = true;
            }
            
            // check to see if Palm marker is in the fourth zone
            if (Input.GetKeyDown(KeyCode.T))
            {
                hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotationL[4]);
                beingRotated = true;
            }
            
            // check to see if Palm marker is in the fifth zone
            if (Input.GetKeyDown(KeyCode.R))
            {
                hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotationL[5]);
                beingRotated = true;
            }
        }
    }

    void MarkerDepth()
    {
        // check to see if Palm marker is nearest to the webcam
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (!wasLarger)
            {
                if (wasSmaller)
                    hand.transform.localScale = new Vector3(hand.transform.localScale.x * scaleFactor * 2, hand.transform.localScale.y * scaleFactor * 2, hand.transform.localScale.z * scaleFactor * 2);
                else
                    hand.transform.localScale = new Vector3(hand.transform.localScale.x * scaleFactor, hand.transform.localScale.y * scaleFactor, hand.transform.localScale.z * scaleFactor);

                wasSmaller = false;
                wasLarger = true;
            }
        }
        
        // check to see if Palm marker is in the middle from the webcam
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (wasLarger) hand.transform.localScale = new Vector3(hand.transform.localScale.x / scaleFactor, hand.transform.localScale.y / scaleFactor, hand.transform.localScale.z / scaleFactor);
            if (wasSmaller) hand.transform.localScale = new Vector3(hand.transform.localScale.x * scaleFactor, hand.transform.localScale.y * scaleFactor, hand.transform.localScale.z * scaleFactor);
           
            wasSmaller = false;
            wasLarger = false;
        }
        
        // check to see if Palm marker is farthest from the webcam
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!wasSmaller)
            {
                if (wasLarger)
                    hand.transform.localScale = new Vector3(hand.transform.localScale.x / (scaleFactor * 2), hand.transform.localScale.y / (scaleFactor * 2), hand.transform.localScale.z / (scaleFactor * 2));
                else
                    hand.transform.localScale = new Vector3(hand.transform.localScale.x / scaleFactor, hand.transform.localScale.y / scaleFactor, hand.transform.localScale.z / scaleFactor);

                wasLarger = false;
                wasSmaller = true;
            }
        }
    }
}

