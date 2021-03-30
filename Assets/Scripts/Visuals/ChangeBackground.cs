using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    private SpriteRenderer sr;
    
    public Sprite 
        start,                // Needs to be completed still
        forrest,
        mansion,                // Needs to be completed still
        road,
        tavern,                // Needs to be completed still
        castle,
        throneRoom,
        ratwayEntrance,
        ratwayTunnel,                // Needs to be completed still
        volcanoEntrance,                // Needs to be completed still
        volcanoHall,                // Needs to be completed still
        volcanoPrison,
        volcano,                // Needs to be completed still
        epilogue;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManagerScript.currentScene)
        {
            case 0:
                sr.sprite = start;
                break;
            
            case 1:
                sr.sprite = forrest;
                break;
            
            case 3:
                sr.sprite = mansion;
                break;
            
            case 6:
                sr.sprite = road;
                break;
            
            case 8:
                sr.sprite = tavern;
                break;
            
            case 11:
                sr.sprite = castle;
                break;
            
            case 13:
                sr.sprite = throneRoom;
                break;
            
            case 14:
                sr.sprite = ratwayEntrance;
                break;
            
            case 16:
                sr.sprite = ratwayTunnel;
                break;
            
            case 18:
                sr.sprite = volcanoEntrance;
                break;

            case 22:
                sr.sprite = volcanoHall;
                break;
            
            case 24:
                sr.sprite = volcanoPrison;
                break;
            
            case 26:
                sr.sprite = volcano;
                break;

            case 29:
                sr.sprite = epilogue;
                break;
        }
        
        // Flip the background image of the final scene because it looks better that way
        int scene = GameManagerScript.currentScene;
        if (scene == 26 || scene == 27 || scene == 28) sr.flipX = true;
        else sr.flipX = false;
    }
}
