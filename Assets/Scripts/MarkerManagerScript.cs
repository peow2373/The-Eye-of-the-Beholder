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
    public static bool backMarker = false;
    public static bool goMarker = false;

    public GameObject hand;
    private SpriteRenderer sr;
    
    public float[] xPos = new float[] { };
    public float[] yPos = new float[] { };

    public float[] rotation = new float[] { 90, 60, 30, 0, -30, -60, -90 };

    public float scaleFactor = 2f;
    public bool wasLarger = false, wasSmaller = false;
    
    public static bool beingRotated = false;

    public static MarkerManagerScript S;

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
        
        // Flip marker if player is left-handed
        if (!GameManagerScript.rightHanded)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }

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
        if (Input.GetKeyDown(KeyCode.H)) palmMarker = !palmMarker;
        
        // check to see if Netrixi marker is visible
        if (Input.GetKeyDown(KeyCode.Y)) netrixiMarker = !netrixiMarker;
        
        // check to see if Folkvar marker is visible
        if (Input.GetKeyDown(KeyCode.O)) folkvarMarker = !folkvarMarker;
        
        // check to see if Iv marker is visible
        if (Input.GetKeyDown(KeyCode.I)) ivMarker = !ivMarker;

        // check to see if Undo marker is visible
        if (Input.GetKeyDown(KeyCode.U)) backMarker = !backMarker;
        
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
                }
            } 
        
            // Is the player left-handed?
            else {
                // check to see if Palm marker is being rotated
                if (Input.GetKeyDown(KeyCode.J))
                {
                    beingRotated = true;
                    // put the hand into the starting position
                    hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotation[6]);
                }
            }
            
            
        } else {
            // check to see if Palm marker is in the first zone
            if (Input.GetKeyDown(KeyCode.L))
            {
                hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotation[1]);
            }
            
            // check to see if Palm marker is in the second zone
            if (Input.GetKeyDown(KeyCode.G))
            {
                hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotation[2]);
            }
            
            // check to see if Palm marker is in the third zone
            if (Input.GetKeyDown(KeyCode.B))
            {
                hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotation[3]);
            }
            
            // check to see if Palm marker is in the fourth zone
            if (Input.GetKeyDown(KeyCode.T))
            {
                hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotation[4]);
            }
            
            // check to see if Palm marker is in the fifth zone
            if (Input.GetKeyDown(KeyCode.R))
            {
                hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotation[5]);
            }
        }
        
        // Reset rotation
        if (Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.V))
        {
            beingRotated = false;
            // return the hand to a vertical position
            hand.transform.rotation = Quaternion.Euler(0 ,0 ,rotation[3]);
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
    
    
    
    private float[] xPosStoryDraft3 = new float[] { -10, -10, -10 };
    private float[] yPosStoryDraft3 = new float[] { -10, -10, -10 };
    
    private float[] xPosTutorial = new float[] { -4.5f, 0, 4.5f };
    private float[] yPosTutorial = new float[] { 2.5f, 0, -2.5f };
    
    private float[] xPosCombatDraft1 = new float[] { 1f, 4f, 7f };
    private float[] yPosCombatDraft1 = new float[] { 2.5f, 0, -2.5f };
    
    private float[] xPosCombatDraft2 = new float[] { 1f, 4f, 7f };
    private float[] yPosCombatDraft2 = new float[] { 2.5f, 0, -2.5f };
    
    private float[] xPosCombatDraft3 = new float[] { -3f, 0f, 3f };
    private float[] yPosCombatDraft3 = new float[] { 6.5f, 4f, 1.5f };
    
    private float[] xPosInkCastle = new float[] { 0f, 3f, 6f };
    private float[] yPosInkCastle = new float[] { 2.5f, 1f, -0.5f };

    void ChangeMarkerLocation()
    {
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        
        // if the player is taking the tutorial
        if (sceneName == "Tutorial")
        {
            xPos = xPosTutorial;
            yPos = yPosTutorial;
        }
        
        // if the player is in combat
        if (sceneName == "CombatDraft1")
        {
            xPos = xPosCombatDraft1;
            yPos = yPosCombatDraft1;
        }
        
        // if the player is in combat and decides to move
        if (sceneName == "CombatDraft2")
        {
            xPos = xPosCombatDraft2;
            yPos = yPosCombatDraft2;
        }
        
        // if the player is in combat with an enemy
        if (sceneName == "CombatDraft3")
        {
            xPos = xPosCombatDraft3;
            yPos = yPosCombatDraft3;
        }
        
        // if the player is in dialogue
        if (sceneName == "StoryDraft3")
        {
            xPos = xPosStoryDraft3;
            yPos = yPosStoryDraft3;
        }
        
        // if the player is having dialogue outside the castle
        if (sceneName == "InkCastle")
        {
            xPos = xPosInkCastle;
            yPos = yPosInkCastle;
        }
    }

    
    
    public void Reset()
    {
        // reset variables when a new level is loaded
        if (palmMarker) palmMarker = true;
        else palmMarker = false;

        if (netrixiMarker) netrixiMarker = true;
        else netrixiMarker = false;
        
        if (folkvarMarker) folkvarMarker = true;
        else folkvarMarker = false;
        
        if (ivMarker) ivMarker = true;
        else ivMarker = false;

        if (backMarker) backMarker = true;
        else backMarker = false;
        
        if (goMarker) goMarker = true;
        else goMarker = false;
        
        
        ChangeMarkerLocation();
        Vector2 loc = new Vector2(xPos[1], yPos[1]);
        hand.transform.position = loc;

        wasLarger = false; 
        wasSmaller = false;
        beingRotated = false;
    }
}

