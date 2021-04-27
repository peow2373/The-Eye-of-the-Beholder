using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandScript : MonoBehaviour
{
    public GameObject logoText;
    public Sprite logoHand;
    public Sprite logoNoHand;
    
    public Sprite openHand;
    public Sprite closedHand;
    public Sprite backHand;

    private SpriteRenderer sr;

    private float smallestSize = 0f;
    private float largestSize = 1.25f;
    private float animationDelay = 0.035f;

    public AudioClip inkIntroSong;

    public static bool startTutorial = false;

    public GameObject gridRow1, gridRow2;
    public GameObject gridColumn1, gridColumn2;

    private float cameraWidth, cameraHeight;
    public static float windowDimensionX, windowDimensionY;

    public static bool logoLoading;
    
    public GameObject[] locations = new GameObject[9];

    public GameObject farHand, midHand, nearHand;

    public GameObject decoyHand, startRotating, continueRotating;

    public static bool doneWithStory;
    public static bool storyStarted;
    
    public GameObject[] targets = new GameObject[5];
    public GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        sr.enabled = false;
        
        logoText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Determine dimensions of the camera within the scene
        Camera camera = Camera.main;
        cameraHeight = 2f * camera.orthographicSize;
        cameraWidth = cameraHeight * camera.aspect;
        windowDimensionX = GameWindowManager.windowWidth * cameraWidth;
        windowDimensionY = GameWindowManager.windowHeight * cameraHeight;
        
        if (MarkerManagerScript.goMarker)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                if (IntroScript.storyProgression == 1)
                {
                    if (!logoLoading) StartCoroutine(FlipHand());
                }
            }
        }

        if (startTutorial)
        {
            StartCoroutine(StartTutorial());
            startTutorial = false;
        }
        
        DetermineLocation();
        DetermineDepth();
        DetermineRotation();
    }

    private IEnumerator FlipHand()
    {
        transform.localScale = new Vector3(largestSize, largestSize, largestSize);

        sr.flipX = false;
        sr.sprite = closedHand;

        float rotateHand = this.transform.eulerAngles.y;

        float standardDelay = 1f;
        float rotationAmount = 5;
        float rotationDelay = animationDelay/1.75f;

        // Flip Hand animation
        for (int i = 0; i < (180 / rotationAmount); i++)
        {
            rotateHand += rotationAmount;
                
            Vector3 rotation = new Vector3(this.transform.eulerAngles.x, rotateHand, transform.eulerAngles.z);

            this.transform.eulerAngles = rotation;
            yield return new WaitForSeconds(rotationDelay);

            if (i >= (int) (180 / rotationAmount) / 2 - 5)
            {
                sr.sprite = backHand;
                sr.flipX = true;
            }
        }
        
        logoText.SetActive(false);
        if (ChangeBackground.startBackground < 2)
        {
            ChangeBackground.startBackground = 2;
            
            gridRow1.SetActive(true);
            gridRow2.SetActive(true);
            gridColumn1.SetActive(true);
            gridColumn2.SetActive(true);
        }


        yield return new WaitForSeconds(standardDelay*1.5f);

        // Flip Hand back animation
        for (int i = 0; i < (180 / rotationAmount); i++)
        {
            rotateHand -= rotationAmount;
                
            Vector3 rotation = new Vector3(this.transform.eulerAngles.x, rotateHand, transform.eulerAngles.z);
                    
            this.transform.eulerAngles = rotation;
            yield return new WaitForSeconds(rotationDelay);

            if (i >= (int) (180 / rotationAmount) / 2 - 2)
            {
                sr.sprite = openHand;
                sr.flipX = false;
            }
        }
    }

    private IEnumerator StartTutorial()
    {
        logoLoading = true;
        
        if (AudioManagerScript.waiting)
        {
            yield return new WaitForSeconds(1f);
            AudioManagerScript.waiting = false;
        }
        
        logoText.SetActive(true);
        logoText.transform.localScale = new Vector3(smallestSize, smallestSize, smallestSize);

        float totalTime = 95 * animationDelay;
        float iterations = totalTime / animationDelay;

        for (int i = 1; i <= iterations; i++)
        {
            if (i < 3) logoText.GetComponent<SpriteRenderer>().enabled = false;
            else logoText.GetComponent<SpriteRenderer>().enabled = true;
            
            float newSize = ((largestSize - smallestSize) / iterations * i);
            logoText.transform.localScale = new Vector3(newSize + smallestSize, newSize + smallestSize, 1);
            
            yield return new WaitForSeconds(animationDelay);
        }

        sr.enabled = true;
        logoText.GetComponent<SpriteRenderer>().sprite = logoNoHand;
        
        yield return new WaitForSeconds(animationDelay * 20);

        sr.sprite = closedHand;
        
        yield return new WaitForSeconds(animationDelay * 10);
        
        sr.sprite = openHand;
        logoLoading = false;
        
        yield return new WaitForSeconds(inkIntroSong.length - (animationDelay*220));

        sr.enabled = false;
        logoText.SetActive(false);
        logoText.transform.localScale = new Vector3(smallestSize, smallestSize, smallestSize);
        logoText.GetComponent<SpriteRenderer>().sprite = logoHand;
        
        yield return new WaitForSeconds(animationDelay*80);

        StartCoroutine(StartTutorial());
    }


    void DetermineLocation()
    {
        Vector3 newLoc;
        
        if (IntroScript.storyProgression == 2)
        {
            // Top Row
            if (Input.GetKeyDown(KeyCode.Q))
            {
                newLoc = new Vector3((-cameraWidth/2) + (windowDimensionX/6),(cameraHeight/2) - (windowDimensionY*5/24),0);
                transform.position = newLoc;
                
                locations[0].SetActive(true);
                IntroScript.locations[0] = true;
            }
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                newLoc = new Vector3((-cameraWidth/2) + (windowDimensionX/2),(cameraHeight/2) - (windowDimensionY*5/24),0);
                transform.position = newLoc;
                
                locations[1].SetActive(true);
                IntroScript.locations[1] = true;
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                newLoc = new Vector3((-cameraWidth/2) + (windowDimensionX*5/6),(cameraHeight/2) - (windowDimensionY*5/24),0);
                transform.position = newLoc;
                
                locations[2].SetActive(true);
                IntroScript.locations[2] = true;
            }
            
            
            // Middle Row
            if (Input.GetKeyDown(KeyCode.A))
            {
                newLoc = new Vector3((-cameraWidth/2) + (windowDimensionX/6),(cameraHeight/2) - (windowDimensionY*13/24),0);
                transform.position = newLoc;
                
                locations[3].SetActive(true);
                IntroScript.locations[3] = true;
            }
            
            if (Input.GetKeyDown(KeyCode.S))
            {
                newLoc = new Vector3((-cameraWidth/2) + (windowDimensionX/2),(cameraHeight/2) - (windowDimensionY*13/24),0);
                transform.position = newLoc;
                
                locations[4].SetActive(true);
                IntroScript.locations[4] = true;
            }
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                newLoc = new Vector3((-cameraWidth/2) + (windowDimensionX*5/6),(cameraHeight/2) - (windowDimensionY*13/24),0);
                transform.position = newLoc;
                
                locations[5].SetActive(true);
                IntroScript.locations[5] = true;
            }
            
            
            // Bottom Row
            if (Input.GetKeyDown(KeyCode.Z))
            {
                newLoc = new Vector3((-cameraWidth/2) + (windowDimensionX/6),(cameraHeight/2) - (windowDimensionY*21/24),0);
                transform.position = newLoc;
                
                locations[6].SetActive(true);
                IntroScript.locations[6] = true;
            }
            
            if (Input.GetKeyDown(KeyCode.X))
            {
                newLoc = new Vector3((-cameraWidth/2) + (windowDimensionX/2),(cameraHeight/2) - (windowDimensionY*21/24),0);
                transform.position = newLoc;
                
                locations[7].SetActive(true);
                IntroScript.locations[7] = true;
            }
            
            if (Input.GetKeyDown(KeyCode.C))
            {
                newLoc = new Vector3((-cameraWidth/2) + (windowDimensionX*5/6),(cameraHeight/2) - (windowDimensionY*21/24),0);
                transform.position = newLoc;
                
                locations[8].SetActive(true);
                IntroScript.locations[8] = true;
            }
        }
        else
        {
            // Reset things
            gridRow1.SetActive(false);
            gridRow2.SetActive(false);
            
            gridColumn1.SetActive(false);
            gridColumn2.SetActive(false);
            
            for (int i = 0; i < locations.Length; i++) locations[i].SetActive(false);
        }
    }

    void DetermineDepth()
    {
        if (IntroScript.storyProgression == 3)
        {
            transform.position = new Vector3((-cameraWidth/2) + (windowDimensionX/2),(cameraHeight/2) - (windowDimensionY/2),0);
            sr.enabled = false;
            
            farHand.SetActive(true);
            midHand.SetActive(true);
            nearHand.SetActive(true);

            // Far away
            if (Input.GetKeyDown(KeyCode.F))
            {
                farHand.GetComponent<SpriteRenderer>().color = Color.white;
                IntroScript.wasSmall = true;
            }
            
            // Medium distance
            if (Input.GetKeyDown(KeyCode.M))
            {
                midHand.GetComponent<SpriteRenderer>().color = Color.white;
                IntroScript.wasMedium = true;
            }
            
            // Nearby
            if (Input.GetKeyDown(KeyCode.N))
            {
                nearHand.GetComponent<SpriteRenderer>().color = Color.white;
                IntroScript.wasLarge = true;
            }
        }
        else
        {
            farHand.SetActive(false);
            midHand.SetActive(false);
            nearHand.SetActive(false);
        }
    }

    void DetermineRotation()
    {
        decoyHand.transform.localScale = transform.localScale;
        decoyHand.transform.position = transform.position;
        decoyHand.transform.eulerAngles = transform.eulerAngles;

        if (IntroScript.storyProgression == 4)
        {
            sr.enabled = true;
            decoyHand.SetActive(true);
            startRotating.SetActive(true);
            
            // Turn on targets
            for (int i = 0; i < targets.Length; i++) targets[i].SetActive(true);
            arrow.SetActive(true);

            if (Input.GetKeyDown(KeyCode.K))
            {
                transform.eulerAngles = new Vector3(0, 0, 90);
                targets[0].GetComponent<SpriteRenderer>().color = Color.red;
            }
        } 
        else if (IntroScript.storyProgression == 5)
        {
            startRotating.SetActive(false);
            continueRotating.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.K))
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);
                ResetColors();
                targets[0].GetComponent<SpriteRenderer>().color = Color.red;
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, (float) 90*3/4);
                ResetColors();
                targets[1].GetComponent<SpriteRenderer>().color = Color.red;
            }
            
            if (Input.GetKeyDown(KeyCode.B))
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, (float) 90*2/4);
                ResetColors();
                targets[2].GetComponent<SpriteRenderer>().color = Color.red;
            }
            
            if (Input.GetKeyDown(KeyCode.T))
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, (float) 90/4);
                ResetColors();
                targets[3].GetComponent<SpriteRenderer>().color = Color.red;
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                ResetColors();
                targets[4].GetComponent<SpriteRenderer>().color = Color.red;
            }
        } 
        else if (IntroScript.storyProgression == 6)
        {
            if (MarkerManagerScript.goMarker)
            {
                if (Input.GetKeyDown(KeyCode.V))
                {
                    StartCoroutine(FlipRotatedHand());
                }
            }
        }
        else
        {
            decoyHand.SetActive(false);
            startRotating.SetActive(false);
            continueRotating.SetActive(false);

            if (IntroScript.storyProgression == 7)
            {
                if (MarkerManagerScript.goMarker)
                {
                    if (Input.GetKeyDown(KeyCode.V))
                    {
                        ChangeBackground.startBackground = 9;
                        doneWithStory = true;
                    }
                }
            }
        }
    }

    void ResetColors()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            SpriteRenderer sr = targets[i].GetComponent<SpriteRenderer>();
            sr.color = Color.white;
        }
    }

    IEnumerator FlipRotatedHand()
    {
        decoyHand.SetActive(false);

        float storyWait = 4.5f;
        float handWait = 0.75f;
        
        SFXManager.S.PlaySFX(42);
        
        yield return new WaitForSeconds(handWait/2.5f);

        sr.sprite = closedHand;

        yield return new WaitForSeconds(handWait/2.5f);
        
        sr.sprite = backHand;
        arrow.SetActive(false);

        yield return new WaitForSeconds(handWait);

        sr.enabled = false;
        for (int i = 0; i < targets.Length; i++) targets[i].SetActive(false);

        yield return new WaitForSeconds(handWait);

        
        
        storyStarted = true;
        
        if (!doneWithStory) ChangeBackground.startBackground = 2;
        yield return new WaitForSeconds(storyWait/2);

        if (!doneWithStory) ChangeBackground.startBackground = 3;
        yield return new WaitForSeconds(storyWait);

        if (!doneWithStory) ChangeBackground.startBackground = 4;
        yield return new WaitForSeconds(storyWait);
        
        if (!doneWithStory) ChangeBackground.startBackground = 5;
        yield return new WaitForSeconds(storyWait);
        
        if (!doneWithStory) ChangeBackground.startBackground = 6;
        yield return new WaitForSeconds(storyWait);
        
        if (!doneWithStory) ChangeBackground.startBackground = 7;
        yield return new WaitForSeconds(storyWait);
        
        if (!doneWithStory) ChangeBackground.startBackground = 8;
        yield return new WaitForSeconds(storyWait);
        
        if (!doneWithStory) ChangeBackground.startBackground = 9;
        yield return new WaitForSeconds(storyWait);

        doneWithStory = true;
    }
}
