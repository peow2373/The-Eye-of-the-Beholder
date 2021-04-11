using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    private SpriteRenderer sr;
    
    public Sprite 
        start,
        forrest,
        mansion, 
        road,
        tavern,
        tavernBar,
        castle,
        castleAlone,
        throneRoom,
        ratwayEntrance,
        ratwayTunnel,
        volcanoEntrance,
        volcanoHall,
        volcanoPrison,
        volcanoPrisonOpened,
        volcano,
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
            case 2:
                sr.sprite = forrest;
                break;
            
            case 3:
            case 4:
                sr.sprite = mansion;
                break;
            
            case 6:
            case 7:
                sr.sprite = road;
                break;
            
            case 8:
            case 9:
                sr.sprite = tavernBar;
                break;
            
            case 10:
                sr.sprite = tavern;
                break;
            
            case 11:
                sr.sprite = castleAlone;
                break;
            
            case 12:
                sr.sprite = castle;
                break;
            
            case 13:
                sr.sprite = throneRoom;
                break;
            
            case 14:
            case 15:
                sr.sprite = ratwayEntrance;
                break;
            
            case 16:
            case 17:
                sr.sprite = ratwayTunnel;
                break;
            
            case 18:
            case 19:
                sr.sprite = volcanoEntrance;
                break;

            case 22:
            case 23:
                sr.sprite = volcanoHall;
                break;
            
            case 24:
                sr.sprite = volcanoPrison;
                break;
            
            case 25:
                sr.sprite = volcanoPrisonOpened;
                break;
            
            case 26:
            case 27:
            case 28:
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
