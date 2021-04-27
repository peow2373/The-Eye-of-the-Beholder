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
    private float farSize = 0.75f;
    private float nearSize = 2f;
    private float animationDelay = 0.035f;

    private float counter = 0;
    
    public AudioClip inkIntroSong;

    private bool handIsFlipping;

    public static bool startTutorial = false;

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
        if (MarkerManagerScript.goMarker)
        {
            // Increment the counter each time dialogue is changed
            if (Input.GetKeyDown(KeyCode.V))
            {
                counter++;
                if (!handIsFlipping) StartCoroutine(FlipHand());
            }
        }

        if (startTutorial)
        {
            StartCoroutine(StartTutorial());
            startTutorial = false;
        }
    }

    private IEnumerator FlipHand()
    {
        ResetHand();
        sr.sprite = closedHand;

        handIsFlipping = true;
        
        float rotateHand = this.transform.eulerAngles.y;

        float standardDelay = 1f;
        float rotationAmount = 5;
        float rotationDelay = animationDelay/1.75f;

        // Flip Hand animation
        for (int i = 0; i < (180 / rotationAmount); i++)
        {
            rotateHand += rotationAmount;
                
            Vector3 rotation = new Vector3(this.transform.eulerAngles.x, rotateHand, this.transform.eulerAngles.z);
                    
            this.transform.eulerAngles = rotation;
            yield return new WaitForSeconds(rotationDelay);

            if (i >= (int) (180 / rotationAmount) / 2 - 5)
            {
                sr.sprite = backHand;
                sr.flipX = true;
            }
        }

        yield return new WaitForSeconds(standardDelay*1.5f);
        
        // Flip Hand back animation
        for (int i = 0; i < (180 / rotationAmount); i++)
        {
            rotateHand -= rotationAmount;
                
            Vector3 rotation = new Vector3(this.transform.eulerAngles.x, rotateHand, this.transform.eulerAngles.z);
                    
            this.transform.eulerAngles = rotation;
            yield return new WaitForSeconds(rotationDelay);

            if (i >= (int) (180 / rotationAmount) / 2 - 2)
            {
                sr.sprite = openHand;
                sr.flipX = false;
            }
        }

        handIsFlipping = false;
    }

    private IEnumerator StartTutorial()
    {
        if (AudioManagerScript.waiting)
        {
            yield return new WaitForSeconds(1f);
            AudioManagerScript.waiting = false;
        }
        
        logoText.SetActive(true);
        logoText.transform.localScale = new Vector3(smallestSize, smallestSize, smallestSize);

        for (int i = 1; i < 100; i++)
        {
            if (i < 3) logoText.GetComponent<SpriteRenderer>().enabled = false;
            else logoText.GetComponent<SpriteRenderer>().enabled = true;
            
            float newSize = ((largestSize - smallestSize) / 100 * i) + smallestSize;
            logoText.transform.localScale = new Vector3(newSize, newSize, newSize);
            
            yield return new WaitForSeconds(animationDelay);
        }

        sr.enabled = true;
        logoText.GetComponent<SpriteRenderer>().sprite = logoNoHand;
        
        yield return new WaitForSeconds(animationDelay * 20);

        sr.sprite = closedHand;
        
        yield return new WaitForSeconds(animationDelay * 10);
        
        sr.sprite = openHand;
        
        yield return new WaitForSeconds(inkIntroSong.length - (animationDelay*220));

        sr.enabled = false;
        logoText.SetActive(false);
        logoText.transform.localScale = new Vector3(smallestSize, smallestSize, smallestSize);
        logoText.GetComponent<SpriteRenderer>().sprite = logoHand;
        
        yield return new WaitForSeconds(animationDelay*80);

        StartCoroutine(StartTutorial());
    }


    void ResetHand()
    {
        transform.localScale = new Vector3(largestSize, largestSize, largestSize);
        transform.eulerAngles = new Vector3(0, 0, 0);

        sr.sprite = openHand;
        sr.flipX = false;

        handIsFlipping = false;
        
        StopCoroutine(FlipHand());
    }
}
