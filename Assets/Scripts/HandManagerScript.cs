using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandManagerScript : MonoBehaviour
{
    private SpriteRenderer sr;

    public Sprite rightHanded, leftHanded;
    public Sprite rightHandedFlipped, leftHandedFlipped;

    private float currentTime = 0, timer = 0.7f;
    private bool startTimer = true;

    public static float scale = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Flip marker if player is left-handed
        if (GameManagerScript.rightHanded)
        {
            sr.flipX = false;

            if (MarkerManagerScript.goMarker)
            {
                sr.enabled = true;
                sr.sprite = rightHandedFlipped;
            }
            else
            {
                sr.sprite = rightHanded;
            }
        }
        else
        {
            sr.flipX = true;
            
            if (MarkerManagerScript.goMarker)
            {
                sr.enabled = true;
                sr.sprite = leftHandedFlipped;
            }
            else
            {
                sr.sprite = leftHanded;
            }
        }
        

        // If Palm marker is visible
        if (MarkerManagerScript.palmMarker)
        {
            // Make hand appear 
            sr.enabled = true;
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
                sr.enabled = false;
            }
        }
    }
    
    
    
    
    //public static float[] xPosStartGame = new float[] { -4.5f, 0, 4.5f };
    //public static float[] yPosStartGame = new float[] { 2.5f, 0, -2.5f };
    
    public static float[] xPosEndGame = new float[] { 50f, 50, 50f };
    public static float[] yPosEndGame = new float[] { 50f, 50, 50f };
    
    public static float[] xPosCombat = new float[] { -3f, 0f, 3f };
    public static float[] yPosCombat = new float[] { 6.5f, 4f, 1.5f };
    
    public static float[] xPosDialogue = new float[] { 0f, 3f, 6f };
    public static float[] yPosDialogue = new float[] { 2.5f, 1f, -0.5f };
    
    public static float[] xPosWebcam = new float[] { -3f, 0f, 3f };
    public static float[] yPosWebcam = new float[] { 6.5f, 4f, 1.5f };
    

    public static void ChangeHandLocation(GameObject hand)
    {
        MarkerManagerScript.xPos = xPosWebcam;
        MarkerManagerScript.yPos = yPosWebcam;

        int currLoc = MarkerManagerScript.currentLocation;

        switch (currLoc)
        {
            case 0:
                hand.transform.position = new Vector3(HandManagerScript.xPosWebcam[1], HandManagerScript.yPosWebcam[1]);
                break;
            
            case 1:
                hand.transform.position = new Vector3(HandManagerScript.xPosWebcam[0], HandManagerScript.yPosWebcam[0]);
                break;
            
            case 2:
                hand.transform.position = new Vector3(HandManagerScript.xPosWebcam[1], HandManagerScript.yPosWebcam[0]);
                break;
            
            case 3:
                hand.transform.position = new Vector3(HandManagerScript.xPosWebcam[2], HandManagerScript.yPosWebcam[0]);
                break;
            
            case 4:
                hand.transform.position = new Vector3(HandManagerScript.xPosWebcam[0], HandManagerScript.yPosWebcam[1]);
                break;
            
            case 5:
                hand.transform.position = new Vector3(HandManagerScript.xPosWebcam[1], HandManagerScript.yPosWebcam[1]);
                break;
            
            case 6:
                hand.transform.position = new Vector3(HandManagerScript.xPosWebcam[2], HandManagerScript.yPosWebcam[1]);
                break;
            
            case 7:
                hand.transform.position = new Vector3(HandManagerScript.xPosWebcam[0], HandManagerScript.yPosWebcam[2]);
                break;
            
            case 8:
                hand.transform.position = new Vector3(HandManagerScript.xPosWebcam[1], HandManagerScript.yPosWebcam[2]);
                break;
            
            case 9:
                hand.transform.position = new Vector3(HandManagerScript.xPosWebcam[2], HandManagerScript.yPosWebcam[2]);
                break;
        }

        
        int curScene = GameManagerScript.currentScene;
        
        // If the player is in the Start Game Scene
        if (curScene == 0)
        {
            //MarkerManagerScript.xPos = xPosStartGame;
            //MarkerManagerScript.yPos = yPosStartGame;
        }

        // If the player is in the End Game Scene
        if (curScene == 29)
        {
            //MarkerManagerScript.xPos = xPosEndGame;
            //MarkerManagerScript.yPos = yPosEndGame;
        }
    }


    public static void ChangeHandScale(GameObject hand, float webcamWidth, float cameraWidth)
    {
        SpriteRenderer sr = hand.GetComponent<SpriteRenderer>();
        float handScale = sr.sprite.bounds.extents.x * 2;
        
        if (!MarkerManagerScript.wasLarger)
        {
            if (!MarkerManagerScript.wasSmaller)
            {
                float scaleFactor = ((webcamWidth / 4) * cameraWidth) / handScale;
                hand.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
            }
            else
            {
                float scaleFactor = ((webcamWidth / 4) * cameraWidth) / (handScale*2);
                hand.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
            }
        }
        else
        {
            float scaleFactor = ((webcamWidth / 4) * cameraWidth) / (handScale/2);
            hand.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
        }
    }
}
