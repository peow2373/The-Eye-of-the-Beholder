using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static bool rightHanded = true;

    public static int currentScene = 0, previousScene = 0;

    private string combatScene = "CombatDraft3";
    private string dialogueScene = "InkCastle";


    public static bool netrixiInParty = true, folkvarInParty = false, ivInParty = false;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Manually swap to the next scene
        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentScene++;
        }

        DetermineNextDialogue();
        DetermineNextCombat();
        
        ChangeScene();
    }

    public static void NextScene(bool skipScene)
    {
        if (!skipScene) currentScene++;
        else currentScene += 2;
    }
    
    
    void SceneChangeAnimation()
    {
        
    }

    void ChangeScene()
    {
        switch (currentScene)
        {
            // Tutorial
            case 0:
                if (previousScene != 0)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene("Tutorial");
                    previousScene = 0;
                    print("Tutorial");
                }
                break;
            
                                                            // Spooky forest
            
            // Dialogue with Pirate
            case 1:
                if (previousScene != 1)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(dialogueScene);
                    previousScene = 1;
                    print("Dialogue with Pirate");
                }
                break;
            
            // Combat with Pirate
            case 2:
                if (previousScene != 2)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(combatScene);
                    previousScene = 2;
                    print("Combat with Pirate");
                }
                break;
            
                                                            // Netrixi's mansion
            
            // Dialogue with Folkvar
            case 3:
                if (previousScene != 3)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(dialogueScene);
                    previousScene = 3;
                    print("Dialogue with Folkvar");
                }
                break;
            
            // Combat with Folkvar
            case 4:
                if (previousScene != 4)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(combatScene);
                    previousScene = 4;
                    print("Combat with Folkvar");
                }
                break;
            
            // Cutscene with Defeated Folkvar
            case 5:
                if (previousScene != 5)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(dialogueScene);
                    previousScene = 5;
                    print("Dialogue with Defeated Folkvar");
                }
                break;
            
                                                            // Not a well-traveled road
            
            // Cutscene with Kaz
            case 6:
                if (previousScene != 6)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(dialogueScene);
                    previousScene = 6;
                    print("Dialogue with Kaz");
                }
                break;
            
            // Combat with Kaz
            case 7:
                if (previousScene != 7)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(combatScene);
                    previousScene = 7;
                    print("Combat with Kaz");
                }
                break;
            
                                                            // Friendly tavern
            
            // Cutscene between Netrixi and Folkvar
            case 8:
                if (previousScene != 8)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(dialogueScene);
                    previousScene = 8;
                    print("Dialogue between Netrixi and Folkvar");
                }
                break;
            
            // Dialogue with people in Tavern
            case 9:
                if (previousScene != 9)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(dialogueScene);
                    previousScene = 9;
                    print("Dialogue with people in Tavern");
                }
                break;
            
            // Combat with Tavern Brute
            case 10:
                if (previousScene != 10)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(combatScene);
                    previousScene = 10;
                    print("Combat with Tavern Brute");
                }
                break;
            
                                                            // Outside the castle walls
            
            // Dialogue with Gatekeeper
            case 11:
                if (previousScene != 11)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(dialogueScene);
                    previousScene = 11;
                    print("Dialogue with Gatekeeper");
                }
                break;
            
            // Combat with Gatekeeper
            case 12:
                if (previousScene != 12)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(combatScene);
                    previousScene = 12;
                    print("Combat with Gatekeeper");
                }
                break;
            
                                                            // Inside the throne room
            
            // Cutscene with the Royal King
            case 13:
                if (previousScene != 13)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(dialogueScene);
                    previousScene = 13;
                    print("Dialogue with the Royal King");
                }
                break;
            
                                                            // At the entrance to the ratways
            
            // Dialogue with Skull Grunts
            case 14:
                if (previousScene != 14)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(dialogueScene);
                    previousScene = 14;
                    print("Dialogue with Skull Grunts");
                }
                break;
            
            // Combat with Skull Grunts
            case 15:
                if (previousScene != 15)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(combatScene);
                    previousScene = 15;
                    print("Combat with Skull Grunts");
                }
                break;
            
                                                            // Inside the inner sanctum of the ratways
            
            // Dialogue with Kaz
            case 16:
                if (previousScene != 16)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(dialogueScene);
                    previousScene = 16;
                    print("Dialogue with Kaz");
                }
                break;
            
            // Combat with Kaz
            case 17:
                if (previousScene != 17)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(combatScene);
                    previousScene = 17;
                    print("Combat with Kaz");
                }
                break;
            
                                                            // On the path to the Beholderite cave
            
            // Dialogue between Netrixi, Folkvar, and Iv
            case 18:
                if (previousScene != 18)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(dialogueScene);
                    previousScene = 18;
                    print("Dialogue between Netrixi, Folkvar, Iv");
                }
                break;
            
                                                            // At the entrance to the Beholderite cave
            
            // Dialogue with Skull Grunts
            case 19:
                if (previousScene != 19)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(dialogueScene);
                    previousScene = 19;
                    print("Dialogue with Skull Grunts");
                }
                break;
            
            // Combat with Skull Grunts
            case 20:
                if (previousScene != 20)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(combatScene);
                    previousScene = 20;
                    print("Combat with Skull Grunts");
                }
                break;
            
                                                            // Narrow tunnel inside the Beholderite cave
            
            // Cutscene with Royal Guards
            case 21:
                if (previousScene != 21)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(dialogueScene);
                    previousScene = 21;
                    print("Dialogue between Royal Guards");
                }
                break;
            
            // Dialogue with Royal Guards
            case 22:
                if (previousScene != 22)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(dialogueScene);
                    previousScene = 22;
                    print("Dialogue with Royal Guards");
                }
                break;
            
            // Combat with Royal Guards
            case 23:
                if (previousScene != 23)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(combatScene);
                    previousScene = 23;
                    print("Combat with Royal Guards");
                }
                break;
            
                                                            // Mining cavern within the Beholderite cave
            
            // Cutscene with Bo
            case 24:
                if (previousScene != 24)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(dialogueScene);
                    previousScene = 24;
                    print("Dialogue between Iv and Bo");
                }
                break;
            
            // Combat with Skull King (Phase 1)
            case 25:
                if (previousScene != 25)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(combatScene);
                    previousScene = 25;
                    print("Combat with Skull King");
                }
                break;
            
            // Dialogue with Skull King 
            case 26:
                if (previousScene != 26)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(dialogueScene);
                    previousScene = 26;
                    print("Dialogue with Royal King");
                }
                break;
            
            // Combat with Skull King (Phase 2)
            case 27:
                if (previousScene != 27)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(combatScene);
                    previousScene = 27;
                    print("Combat with Royal King");
                }
                break;
            
            // Cutscene with Defeated Skull King
            case 28:
                if (previousScene != 28)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene(dialogueScene);
                    previousScene = 28;
                    print("Dialogue after defeating the Royal King");
                }
                break;
            
            
            
            // End game
            case 29:
                if (previousScene != 0)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene("Tutorial");
                    previousScene = 0;
                }
                break;
        }
    }
    
    
    
    void DetermineNextDialogue()
    {
        switch (currentScene + 1)
        {
            // Outside the castle
            case 11:
                dialogueScene = "InkCastle";
                break;
            
            // Inside the throne room
            case 13:
                dialogueScene = "InkThrone";
                break;
        }
    }



    void DetermineNextCombat()
    {
        switch (currentScene + 1)
        {
            // Fighting a Pirate
            case 2:
                netrixiInParty = true;
                folkvarInParty = false;
                ivInParty = false;
                break;

            // Fighting Kaz
            case 7:
                netrixiInParty = true;
                folkvarInParty = true;
                ivInParty = false;
                break;

            // Fighting the Gatekeeper
            case 12:
                netrixiInParty = true;
                folkvarInParty = true;
                ivInParty = true;
                break;
        }
    }
}
