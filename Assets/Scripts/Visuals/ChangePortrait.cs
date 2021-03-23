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
    
    private SpriteRenderer sr;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
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
            
            print(speakerName);
        }

        DetermineSpeaker();
    }

    void DetermineSpeaker()
    {
        int currentScene = GameManagerScript.currentScene;
        
        switch (speakerName)
        {
            // Main Characters
            case "Netrixi":
                whoIsTalking = 1;
                break;
            
            case "Iv":
                if (currentScene > 10)
                {
                    if (currentScene == 18) whoIsTalking = 2;
                    else whoIsTalking = 1;
                }
                else whoIsTalking = 2;
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
                break;
            
            
            
            // Enemy Characters
            case "Pirate":
                whoIsTalking = 2;
                break;
            
            case "Kaz":
                if (currentScene == 16)
                {
                    if (MarkerManagerScript.goMarker) if (Input.GetKeyDown(KeyCode.V)) kazSpeaks++;
                    
                    print(kazSpeaks);
                    
                    if (kazSpeaks < 2) whoIsTalking = 1;
                    else whoIsTalking = 2;
                }
                else
                {
                    kazSpeaks = 0;
                    whoIsTalking = 2;
                }
                break;

            case "Brute":
                whoIsTalking = 2;
                break;
            
            case "GateKeeper":
                whoIsTalking = 2;
                break;
            
            case "King":
                whoIsTalking = 2;
                break;
            
            case "Jester":
                whoIsTalking = 2;
                break;
            
            case "Bandit":
                whoIsTalking = 2;
                break;
            
            case "RoyalGuard_1":
            case "RoyalGuard_2":
                whoIsTalking = 2;
                break;
            
            case "Bo":
                whoIsTalking = 2;
                break;
            
            case "Moke":
                whoIsTalking = 2;
                break;
            
            case "KingMoke":
                whoIsTalking = 2;
                break;
            
            
            default:
                whoIsTalking = 0;
                break;
        }
    }
}
