using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    private SpriteRenderer sr;

    public GameObject secondBackground;
    
    public Sprite
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
        volcanoPrisonClosed,
        volcanoPrisonOpened,
        volcano,
        epilogue;

    public static int startBackground;

    public Sprite
        start,
        startNoText,
        start1,
        start2,
        start3,
        start4,
        start5,
        start6,
        start7;
    
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
                if (startBackground <= 1) sr.sprite = start;
                else if (startBackground == 2) sr.sprite = startNoText;
                
                else if (startBackground == 3) sr.sprite = start1;
                else if (startBackground == 4) sr.sprite = start2;
                else if (startBackground == 5) sr.sprite = start3;
                else if (startBackground == 6) sr.sprite = start4;
                else if (startBackground == 7) sr.sprite = start5;
                else if (startBackground == 8) sr.sprite = start6;
                else sr.sprite = start7;
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
                secondBackground.GetComponent<SpriteRenderer>().sprite = tavern;
                break;
            
            case 10:
                sr.sprite = tavern;
                break;
            
            case 11:
                sr.sprite = castleAlone;
                secondBackground.GetComponent<SpriteRenderer>().sprite = castle;
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
                secondBackground.GetComponent<SpriteRenderer>().sprite = volcanoPrisonClosed;
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
