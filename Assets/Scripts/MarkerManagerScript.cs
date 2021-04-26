using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarkerManagerScript : MonoBehaviour
{
    public static bool palmMarker = false;
    public static bool netrixiMarker = false;
    public static bool folkvarMarker = false;
    public static bool ivMarker = false;
    public static bool undoMarker = false;
    public static bool goMarker = false;

    public GameObject hand;
    private SpriteRenderer sr;
    
    public static float[] xPos = new float[] { };
    public static float[] yPos = new float[] { };

    public static float[] rotation = new float[] { 90, 72.5f, 45, 22.5f, 0, -90, -72.5f, -45, -22.5f };
    
    public static Vector3 scale = new Vector3(1,1,1);

    public float scaleFactor = 1.5f;
    public static bool wasLarger = false, wasSmaller = false;
    
    public static bool beingRotated = false;
    public static bool rotatingRight, rotatingLeft;

    public static MarkerManagerScript S;

    public static int currentLocation = 5;
    public static int pastLocation = 0;

    void Awake()
    {
        S = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        sr = hand.GetComponent<SpriteRenderer>();
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
        }
    }

    void TestForMarkers()
    {
        // check to see if Palm marker is visible
        if (Input.GetKeyDown(KeyCode.H)) palmMarker = true;
        if (Input.GetKeyDown(KeyCode.L)) palmMarker = false;
        
        // check to see if Netrixi marker is visible
        if (Input.GetKeyDown(KeyCode.Y)) netrixiMarker = !netrixiMarker;
        
        // check to see if Folkvar marker is visible
        if (Input.GetKeyDown(KeyCode.O)) folkvarMarker = !folkvarMarker;
        
        // check to see if Iv marker is visible
        if (Input.GetKeyDown(KeyCode.I)) ivMarker = !ivMarker;

        // check to see if Undo marker is visible
        if (Input.GetKeyDown(KeyCode.U)) undoMarker = !undoMarker;
        
        // check to see if Extra marker is visible
        if (Input.GetKeyDown(KeyCode.V)) goMarker = !goMarker;
    }

    void MarkerLocate()
    {
        // check to see if Palm marker is in the top-left corner
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector2 loc = new Vector2(xPos[0], yPos[0]);
            hand.transform.position = loc;
            pastLocation = currentLocation;
            currentLocation = 1;
            
            //SFXManager.S.PlaySFX(24);
        }
        
        // check to see if Palm marker is in the top-middle corner
        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector2 loc = new Vector2(xPos[1], yPos[0]);
            hand.transform.position = loc;
            pastLocation = currentLocation;
            currentLocation = 2;
            
            //SFXManager.S.PlaySFX(24);
        }
        
        // check to see if Palm marker is in the top-right corner
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 loc = new Vector2(xPos[2], yPos[0]);
            hand.transform.position = loc;
            pastLocation = currentLocation;
            currentLocation = 3;
            
            //SFXManager.S.PlaySFX(24);
        }
        
        // check to see if Palm marker is in the middle-left corner
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector2 loc = new Vector2(xPos[0], yPos[1]);
            hand.transform.position = loc;
            pastLocation = currentLocation;
            currentLocation = 4;
            
            //SFXManager.S.PlaySFX(24);
        }
        
        // check to see if Palm marker is in the center corner
        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector2 loc = new Vector2(xPos[1], yPos[1]);
            hand.transform.position = loc;
            pastLocation = currentLocation;
            currentLocation = 5;
            
            //SFXManager.S.PlaySFX(24);
        }
        
        // check to see if Palm marker is in the middle-right corner
        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector2 loc = new Vector2(xPos[2], yPos[1]);
            hand.transform.position = loc;
            pastLocation = currentLocation;
            currentLocation = 6;
            
            //SFXManager.S.PlaySFX(24);
        }
        
        // check to see if Palm marker is in the bottom-left corner
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Vector2 loc = new Vector2(xPos[0], yPos[2]);
            hand.transform.position = loc;
            pastLocation = currentLocation;
            currentLocation = 7;
            
            //SFXManager.S.PlaySFX(24);
        }
        
        // check to see if Palm marker is in the bottom-middle corner
        if (Input.GetKeyDown(KeyCode.X))
        {
            Vector2 loc = new Vector2(xPos[1], yPos[2]);
            hand.transform.position = loc;
            pastLocation = currentLocation;
            currentLocation = 8;
            
            //SFXManager.S.PlaySFX(24);
        }
        
        // check to see if Palm marker is in the bottom-right corner
        if (Input.GetKeyDown(KeyCode.C))
        {
            Vector2 loc = new Vector2(xPos[2], yPos[2]);
            hand.transform.position = loc;
            pastLocation = currentLocation;
            currentLocation = 9;
            
            //SFXManager.S.PlaySFX(24);
        }
    }

    void MarkerRotate()
    {
        if (!beingRotated)
        {
            // Is the player right-handed?
            if (GameManagerScript.rightHanded)
            {
                // check to see if Palm marker is being rotated
                if (Input.GetKeyDown(KeyCode.K))
                {
                    beingRotated = true;
                    // put the hand into the starting position
                    hand.transform.rotation = Quaternion.Euler(0, 0, rotation[0]);
                    rotatingRight = true;
                    
                    SFXManager.S.PlaySFX(27);
                }
            } 
        
            // Is the player left-handed?
            else {
                // check to see if Palm marker is being rotated
                if (Input.GetKeyDown(KeyCode.J))
                {
                    beingRotated = true;
                    // put the hand into the starting position
                    hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotation[5]);
                    rotatingLeft = true;
                    
                    SFXManager.S.PlaySFX(27);
                }
            }
            
            
        } else {
            
            if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.J)) SFXManager.S.PlaySFX(27);
          
            // check to see if Palm marker is in the second zone
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (rotatingRight) hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotation[1]);
                if (rotatingLeft) hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotation[6]);
                
                SFXManager.S.PlaySFX(27);
            }
            
            // check to see if Palm marker is in the third zone
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (rotatingRight) hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotation[2]);
                if (rotatingLeft) hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotation[7]);
                
                SFXManager.S.PlaySFX(27);
            }
            
            // check to see if Palm marker is in the fourth zone
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (rotatingRight) hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotation[3]);
                if (rotatingLeft) hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotation[8]);
                
                SFXManager.S.PlaySFX(27);
            }
            
            // check to see if Palm marker is in the fifth zone
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (rotatingRight) hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotation[4]);
                if (rotatingLeft) hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotation[4]);
                
                SFXManager.S.PlaySFX(27);
            }
        }
        
        // Reset rotation
        if (Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.V))
        {
            beingRotated = false;
            // return the hand to a vertical position
            hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotation[4]);
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



    public void Reset()
    {
        GameWindowManager.S.ArrangeScreen();
        HandManagerScript.ChangeHandLocation(hand);
        
        EnemyManagerScript.ClearMoves();
    }
}

