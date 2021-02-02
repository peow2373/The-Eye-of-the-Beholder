using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManagerScript : MonoBehaviour
{
    private SpriteRenderer sr;

    private float currentTime = 0, timer = 0.7f;
    private bool startTimer = true;
    
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
}
