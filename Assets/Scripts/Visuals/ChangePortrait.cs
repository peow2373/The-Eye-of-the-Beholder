using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePortrait : MonoBehaviour
{
    public static int whoIsTalking = 0;

    public static string speakerText, speakerName, speakerDialogue;

    public static bool folkvarChosen = false;
    public static int kazSpeaks = 0;

    public GameObject characterPortrait;

    public Sprite netrixiNoHood, netrixiHood;
    public Sprite folkvarNoHelmet, folkvarHelmet;
    public Sprite ivEyesClosedNoAmulet, ivEyesClosedAmulet, ivEyesOpenAmulet;

    public Sprite pirate, brute, bo;
    public Sprite gatekeeper, royalGuard, royalKing, jester;
    public Sprite kazNoHood, kazHood, bandit, skullKing, evilKing;
    
    private SpriteRenderer sr;

    public Color friendlyColor, enemyColor;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = characterPortrait.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject dialogueTextBox = GameObject.FindGameObjectWithTag("Dialogue Text");
        if (dialogueTextBox != null)
        {
            speakerText = dialogueTextBox.GetComponent<Text>().text;
            string[] splitArray = speakerText.Split(new string[] {" - "}, System.StringSplitOptions.None);
            speakerName = splitArray[0];
            if (splitArray.Length > 1) speakerDialogue = splitArray[1];
            
            //print(speakerName);
        }

        DetermineSpeaker();
        
        // Resize portrait sprite
        float portraitWidth = sr.sprite.bounds.extents.x * 2;
        float desiredWidth = GameWindowManager.portraitSizeX;

        float scaleDifference = desiredWidth / portraitWidth;
        
        characterPortrait.transform.localScale = new Vector3(scaleDifference, scaleDifference, 1);
        characterPortrait.transform.position = this.transform.position;
        

        // Change portrait background color and direction
        if (whoIsTalking == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = friendlyColor;
            sr.flipX = true;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = enemyColor;
            sr.flipX = false;
        }
    }

    void DetermineSpeaker()
    {
        int currentScene = GameManagerScript.currentScene;
        
        switch (speakerName)
        {
            // Main Characters
            case "Netrixi":
                whoIsTalking = 1;
                
                // Change portriat
                if (currentScene == 3 || currentScene == 8 || currentScene == 9 || currentScene == 13 || currentScene == 18 || currentScene == 26) sr.sprite = netrixiNoHood;
                else sr.sprite = netrixiHood;
                break;
            
            case "Iv":
                if (currentScene > 10)
                {
                    if (currentScene == 18) whoIsTalking = 2;
                    else whoIsTalking = 1;
                }
                else whoIsTalking = 2;

                // Change portrait
                if (currentScene > 15 && currentScene < 24) sr.sprite = ivEyesClosedAmulet;
                else
                {
                    if (currentScene <= 15) sr.sprite = ivEyesClosedNoAmulet;
                    if (currentScene >= 24) sr.sprite = ivEyesOpenAmulet;
                }
                break;
            
            case "Folkvar":
                if (currentScene > 4)
                {
                    if (currentScene == 18) whoIsTalking = 2;
                    else if (currentScene == 8 || currentScene == 9)
                    {
                        if (MarkerManagerScript.folkvarMarker) folkvarChosen = true;
                        
                        if (!folkvarChosen) whoIsTalking = 2;
                        else whoIsTalking = 1;
                    }
                    else if (currentScene == 10 || currentScene == 11) folkvarChosen = false;
                    else whoIsTalking = 1;
                }
                else
                {
                    whoIsTalking = 2;
                }

                // Change portrait
                if (currentScene == 8 || currentScene == 9 || currentScene == 13 || currentScene == 18 || currentScene == 26) sr.sprite = folkvarNoHelmet;
                else sr.sprite = folkvarHelmet;
                break;
            
            
            
            // Enemy Characters
            case "Pirate":
                whoIsTalking = 2;
                sr.sprite = pirate;
                break;
            
            case "Kaz":
                if (currentScene == 16)
                {
                    if (MarkerManagerScript.goMarker) if (Input.GetKeyDown(KeyCode.V)) kazSpeaks++;
                    
                    print(kazSpeaks);
                    
                    if (kazSpeaks < 2) whoIsTalking = 1;
                    else whoIsTalking = 2;

                    sr.sprite = kazNoHood;
                }
                else
                {
                    kazSpeaks = 0;
                    whoIsTalking = 2;

                    sr.sprite = kazHood;
                }
                break;

            case "Brute":
                whoIsTalking = 2;
                sr.sprite = brute;
                break;
            
            case "GateKeeper":
                whoIsTalking = 2;
                sr.sprite = gatekeeper;
                break;
            
            case "King":
                whoIsTalking = 2;
                sr.sprite = royalKing;
                break;
            
            case "Jester":
                whoIsTalking = 2;
                sr.sprite = jester;
                break;
            
            case "Bandit":
                whoIsTalking = 2;
                sr.sprite = bandit;
                break;
            
            case "RoyalGuard_1":
            case "RoyalGuard_2":
                whoIsTalking = 2;
                sr.sprite = royalGuard;
                break;
            
            case "Bo":
                whoIsTalking = 2;
                sr.sprite = bo;
                break;
            
            case "Moke":
                whoIsTalking = 2;
                sr.sprite = skullKing;
                break;
            
            case "KingMoke":
                whoIsTalking = 2;
                sr.sprite = evilKing;
                break;
            
            
            default:
                whoIsTalking = 0;
                break;
        }
    }
}
