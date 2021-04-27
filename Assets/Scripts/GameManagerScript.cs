using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static bool rightHanded = true;

    public static int currentScene = 0, previousScene = 0;

    private string combatScene = "CombatScene";

    public static bool netrixiInParty = true, folkvarInParty = false, ivInParty = false;

    public static bool barkeeperMad = false;

    public static int chooseRandomKnight;

    public static bool inCombat = false;
    
    public static bool endOfGame = false;

    public static int gameWinner = 0;

    public static GameManagerScript S;

    public static bool curtainsOpening;
    
    public GameObject leftCurtain, rightCurtain, curtains;
    public GameObject canvas;
    public GameObject portrait, portraitBG;

    private SpriteRenderer leftSR, rightSR;

    private float cameraWidth, cameraHeight;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        S = this;
        
        leftSR = leftCurtain.GetComponent<SpriteRenderer>();
        rightSR = rightCurtain.GetComponent<SpriteRenderer>();

        currentScene = 0;
        previousScene = -1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForGameChanges();

        // Manually swap to the next scene
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentScene == 8 || currentScene == 9) currentScene += 2;
            
            else if (currentScene == 10) currentScene--;
            
            else currentScene++;
        }

        DetermineNextCombat();

        ChangeScene();
        
        
        // Determine dimensions of the camera within the scene
        Camera camera = Camera.main;
        cameraHeight = 2f * camera.orthographicSize;
        cameraWidth = cameraHeight * camera.aspect;
        
        // Determine the curtains locations
        if (!curtainsOpening)
        {
            float curtainWidth = leftSR.bounds.extents.x;

            leftCurtain.transform.position = new Vector3((-cameraWidth/2) - curtainWidth, leftCurtain.transform.position.y, 0);
            rightCurtain.transform.position = new Vector3((cameraWidth/2) + curtainWidth, rightCurtain.transform.position.y, 0);
        }
    }


    void CheckForGameChanges()
    {
        // Enter/Exit FullScreen mode
        if (Input.GetKeyDown(KeyCode.Escape)) Screen.fullScreen = !Screen.fullScreen;
    }

    IEnumerator OpenCurtains(bool stopOthers)
    {
        if (stopOthers)
        {
            StopAllCoroutines();
            StartCoroutine(OpenCurtains(false));
        }
        
        // Disable canvas
        canvas.GetComponent<Canvas>().enabled = false;
        portrait.GetComponent<SpriteRenderer>().enabled = false;
        portraitBG.GetComponent<SpriteRenderer>().enabled = false;

        curtains.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.01f);
        GameWindowManager.S.PositionCurtains();
        
        curtains.gameObject.SetActive(false);

        curtainsOpening = true;

        // Assign variables
        float curtainWidth = leftSR.bounds.extents.x;
        float waitBeforeOpening = 2f;
        float delay = 0.02f;

        // Curtain starting location
        leftCurtain.transform.position = new Vector3(-curtainWidth, leftCurtain.transform.position.y, 0);
        rightCurtain.transform.position = new Vector3(curtainWidth, rightCurtain.transform.position.y, 0);

        if (AudioManagerScript.waiting) yield return new WaitForSeconds(waitBeforeOpening);
        else yield return new WaitForSeconds(waitBeforeOpening/2);
        
        if (currentScene != 0) AudioManagerScript.S.PlayAudio(currentScene);
            
        for (int i = 0; i <= 100; i++)
        {
            yield return new WaitForSeconds(delay);
                
            float travelDistance = (cameraWidth / 2) / 100 * i;
                
            leftCurtain.transform.position = new Vector3((-curtainWidth) - travelDistance, leftCurtain.transform.position.y, 0);
            rightCurtain.transform.position = new Vector3((curtainWidth) + travelDistance, rightCurtain.transform.position.y, 0);
        }

        if (GameManagerScript.currentScene == 0 || GameManagerScript.currentScene == 29) yield return new WaitForSeconds(4f);
        else yield return new WaitForSeconds(waitBeforeOpening/4);
        
        // Re-enable canvas
        canvas.GetComponent<Canvas>().enabled = true;
        portrait.GetComponent<SpriteRenderer>().enabled = true;
        portraitBG.GetComponent<SpriteRenderer>().enabled = true;
        
        curtainsOpening = false;
    }
    

    void ChangeScene()
    {
        switch (currentScene)
        {
            // Tutorial
            case 0:
                if (previousScene != 0)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene("InkIntro");
                    previousScene = 0;

                    AudioManagerScript.S.PlayAudio(currentScene);
                    print("Tutorial Scene");
                    
                    inCombat = false;
                    endOfGame = false;
                }
                break;
            
                                                            // Spooky forest
            
            // Dialogue with Pirate
            case 1:
                if (previousScene != 1)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene("InkNetrixiPirate");
                    previousScene = 1;

                    print("Dialogue with Pirate");
                    
                    inCombat = false;
                    endOfGame = false;
                }
                break;
            
            // Combat with Pirate
            case 2:
                if (previousScene != 2)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene(combatScene);
                    
                    previousScene = 2;
                    
                    print("Combat with Pirate");
                    
                    inCombat = true;
                }
                break;
            
                                                            // Netrixi's mansion
            
            // Dialogue with Folkvar
            case 3:
                if (previousScene != 3)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene("InkNetrixiMansion");
                    previousScene = 3;
                    
                    print("Dialogue with Folkvar");
                    
                    inCombat = false;
                }
                break;
            
            // Combat with Folkvar
            case 4:
                if (previousScene != 4)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene(combatScene);
                    previousScene = 4;

                    print("Combat with Folkvar");
                    
                    inCombat = true;
                }
                break;
            
            // DELETED: Cutscene with Defeated Folkvar
            case 5:
                if (currentScene == 5)
                {
                    // Skip this scene because it was removed from the game
                    currentScene = 6;
                }
                break;
            
                                                            // Not a well-traveled road
            
            // Cutscene with Kaz
            case 6:
                if (previousScene != 6)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene("InkRoad");
                    previousScene = 6;

                    print("Dialogue with Kaz");
                    
                    inCombat = false;
                }
                break;
            
            // Combat with Kaz
            case 7:
                if (previousScene != 7)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene(combatScene);
                    previousScene = 7;
 
                    print("Combat with Kaz");
                    
                    inCombat = true;
                }
                break;
            
                                                            // Semi-Friendly tavern
            
            // Dialogue with people in Tavern before fight
            case 8:
                if (previousScene != 8)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene("InkTavern");
                    previousScene = 8;

                    print("Dialogue with people in Tavern before fight");
                    
                    inCombat = false;
                }
                break;
            
            // Dialogue with people in Tavern after fight
            case 9:
                if (previousScene != 9)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene("InkTavernAfterFight");
                    previousScene = 9;
       
                    print("Dialogue with people in Tavern after fight");
                    
                    inCombat = false;
                }
                break;
            
            // Combat with Tavern Brute
            case 10:
                if (previousScene != 10)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene(combatScene);
                    previousScene = 10;

                    print("Combat with Tavern Brute");
                    
                    inCombat = true;
                }
                break;
            
                                                            // Outside the castle walls
            
            // Dialogue with Gatekeeper
            case 11:
                if (previousScene != 11)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene("InkCastle");
                    previousScene = 11;
         
                    print("Dialogue with Gatekeeper");
                    
                    inCombat = false;
                }
                break;
            
            // Combat with Gatekeeper
            case 12:
                if (previousScene != 12)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene(combatScene);
                    previousScene = 12;
  
                    print("Combat with Gatekeeper");
                    
                    inCombat = true;
                }
                break;
            
                                                            // Inside the throne room
            
            // Cutscene with the Royal King
            case 13:
                if (previousScene != 13)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene("InkThrone");
                    previousScene = 13;
    
                    print("Dialogue with the Royal King");
                    
                    inCombat = false;
                }
                break;
            
                                                            // At the entrance to the ratways
            
            // Dialogue with Skull Grunts
            case 14:
                if (previousScene != 14)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene("InkTunnelEntrance");
                    previousScene = 14;

                    print("Dialogue with Skull Grunts");
                    
                    inCombat = false;
                }
                break;
            
            // Combat with Skull Grunts
            case 15:
                if (previousScene != 15)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene(combatScene);
                    previousScene = 15;

                    print("Combat with Skull Grunts");
                    
                    inCombat = true;
                }
                break;
            
                                                            // Inside the inner sanctum of the ratways
            
            // Dialogue with Kaz
            case 16:
                if (previousScene != 16)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene("InkKazBoss");
                    previousScene = 16;

                    print("Dialogue with Kaz");
                    
                    inCombat = false;
                }
                break;
            
            // Combat with Kaz
            case 17:
                if (previousScene != 17)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene(combatScene);
                    previousScene = 17;
  
                    print("Combat with Kaz");
                    
                    inCombat = true;
                }
                break;
            
                                                            // On the path to the Beholderite cave
            
            // Dialogue between Netrixi, Folkvar, and Iv
            case 18:
                if (previousScene != 18)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene("InkTunnelExit");
                    previousScene = 18;

                    print("Dialogue between Netrixi, Folkvar, Iv");
                    
                    inCombat = false;
                }
                break;
            
                                                            // At the entrance to the Beholderite cave
            
            // DELETED: Dialogue with Skull Grunts
            case 19:
                
                if (currentScene == 19)
                {
                    // Skip this scene because it was removed from the game
                    currentScene = 22;
                }
                break;
            
            // DELETED" Combat with Skull Grunts
            case 20:
                if (currentScene == 20)
                {
                    // Skip this scene because it was removed from the game
                    currentScene = 22;
                }
                break;
            
                                                            // Narrow tunnel inside the Beholderite cave
            
            // DELETED: Cutscene with Royal Guards
            case 21:
                if (currentScene == 21)
                {
                    // Skip this scene because it was removed from the game
                    currentScene = 22;
                }
                break;
            
            // Dialogue with Royal Guards
            case 22:
                if (previousScene != 22)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene("InkInsideVolcano");
                    previousScene = 22;

                    print("Dialogue with Royal Guards");
                    
                    inCombat = false;
                }
                break;
            
            // Combat with Royal Guards
            case 23:
                if (previousScene != 23)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene(combatScene);
                    previousScene = 23;

                    print("Combat with Royal Guards");
                    
                    inCombat = true;
                }
                break;
            
                                                            // Mining cavern within the Beholderite cave
            
            // Cutscene with Bo
            case 24:
                if (previousScene != 24)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene("InkMine");
                    previousScene = 24;

                    print("Dialogue between Iv and Bo");
                    
                    inCombat = false;
                }
                break;
            
            // Combat with Skull King (Phase 1)
            case 25:
                if (previousScene != 25)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene(combatScene);
                    previousScene = 25;
   
                    print("Combat with Skull King");
                    
                    inCombat = true;
                }
                break;
            
            // Dialogue with Skull King 
            case 26:
                if (previousScene != 26)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene("InkMoke");
                    previousScene = 26;
                    
                    print("Dialogue with Royal King");
                    
                    inCombat = false;
                }
                break;
            
            // Combat with Skull King (Phase 2)
            case 27:
                if (previousScene != 27)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene(combatScene);
                    previousScene = 27;

                    print("Combat with Royal King");
                    
                    inCombat = true;
                }
                break;
            
            // Cutscene with Defeated Skull King
            case 28:
                if (previousScene != 28)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene("InkFinalDecision");
                    previousScene = 28;

                    print("Dialogue after defeating the Royal King");
                    
                    inCombat = false;
                }
                break;
            
            
            
            // End game
            case 29:
                if (previousScene != 29)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene("InkEpilogue");
                    previousScene = 29;

                    print("Epilogue");
                    
                    inCombat = false;
                }
                break;
            
            // Restart game
            case 30:
                if (previousScene != 30)
                {
                    StartCoroutine(OpenCurtains(true));
                    SceneManager.LoadScene("InkIntro");

                    currentScene = 0;
                    previousScene = 30;

                    print("Restarting Game");
                    
                    Reset();
                }
                break;

            // Game Over
            case 31:
                if (previousScene != 31)
                {
                    if (inCombat)
                    {
                        StartCoroutine(OpenCurtains(true));
                        SceneManager.LoadScene("Game Over");

                        print("Game Over");

                        inCombat = false;
                    }
                }
                break;
        }
    }
    
    
    
    
    void DetermineNextCombat()
    {
        // Determine if the character has joined the party yet
        if (currentScene >= 1) netrixiInParty = true;
        else netrixiInParty = false;
        
        if (currentScene >= 7) folkvarInParty = true;
        else folkvarInParty = false;
        
        if (currentScene >= 12) ivInParty = true;
        else ivInParty = false;
        
        switch (currentScene)
        {
            // Fighting a Pirate
            case 2:
                EnemyManagerScript.enemy1 = "Pirate";
                EnemyManagerScript.enemy2 = "null";
                EnemyManagerScript.enemy3 = "null";
                
                EnemyManagerScript.ChangeEnemyLocation( 7, 0, 0 );
                HealthManagerScript.StartingHealth(HealthValues.pirateHP, 0, 0);
                break;
            
            // Fighting Folkvar and his two Royal Knights
            case 4:
                // Change the Royal Knight to Melee or Ranged randomly
                if (chooseRandomKnight == 0)
                {
                    EnemyManagerScript.enemy1 = "Folkvar";
                    EnemyManagerScript.enemy2 = "Royal Knight Melee";
                    EnemyManagerScript.enemy3 = "null";
                    
                    EnemyManagerScript.ChangeEnemyLocation( 7, 9, 0 );
                    HealthManagerScript.StartingHealth(HealthValues.folkvarHP, HealthValues.knightMeleeHP, 0);
                }
                else if (chooseRandomKnight == 1 || chooseRandomKnight == 2)
                {
                    EnemyManagerScript.enemy1 = "Folkvar";
                    EnemyManagerScript.enemy2 = "Royal Knight Ranged";
                    EnemyManagerScript.enemy3 = "null";
                    
                    EnemyManagerScript.ChangeEnemyLocation( 7, 9, 0 );
                    HealthManagerScript.StartingHealth(HealthValues.folkvarHP, HealthValues.knightRangedHP, 0);
                }
                break;

            // Fighting Kaz and his two Skull Grunts
            case 7:
                EnemyManagerScript.enemy1 = "Skull Grunt Melee";
                EnemyManagerScript.enemy2 = "Skull Grunt Ranged";
                EnemyManagerScript.enemy3 = "Kaz 1";
                
                EnemyManagerScript.ChangeEnemyLocation( 6, 8, 10 );
                HealthManagerScript.StartingHealth(HealthValues.skullMeleeHP, HealthValues.skullRangedHP, HealthValues.kazHP);
                break;
            
            // Fighting the Tavern Brute
            case 10:
                EnemyManagerScript.enemy1 = "Tavern Brute";
                
                if (barkeeperMad) EnemyManagerScript.enemy2 = "Barkeeper";
                else EnemyManagerScript.enemy2 = "null";
                
                EnemyManagerScript.enemy3 = "null";
                
                EnemyManagerScript.ChangeEnemyLocation( 7, 9, 0 );
                HealthManagerScript.StartingHealth(HealthValues.bruteHP, HealthValues.barkeeperHP, 0);
                break;

            // Fighting the Gatekeeper
            case 12:
                EnemyManagerScript.enemy1 = "Royal Knight Melee";
                EnemyManagerScript.enemy2 = "Royal Knight Ranged";
                EnemyManagerScript.enemy3 = "Gatekeeper";
                
                EnemyManagerScript.ChangeEnemyLocation( 6, 8, 10 );
                HealthManagerScript.StartingHealth(HealthValues.knightMeleeHP, HealthValues.knightRangedHP, HealthValues.gatekeeperHP);
                break;
            
            // Fighting two Skull Grunts
            case 15:
                EnemyManagerScript.enemy1 = "Skull Grunt Melee";
                EnemyManagerScript.enemy2 = "Skull Grunt Ranged";
                EnemyManagerScript.enemy3 = "null";
                
                EnemyManagerScript.ChangeEnemyLocation( 7, 9, 0 );
                HealthManagerScript.StartingHealth(HealthValues.skullMeleeHP, HealthValues.skullRangedHP, 0);
                break;
            
            // Fighting Kaz and his two Skull Grunts
            case 17:
                EnemyManagerScript.enemy1 = "Skull Grunt Melee";
                EnemyManagerScript.enemy2 = "Skull Grunt Ranged";
                EnemyManagerScript.enemy3 = "Kaz 2";
                
                EnemyManagerScript.ChangeEnemyLocation( 6, 8, 10 );
                HealthManagerScript.StartingHealth(HealthValues.skullMeleeHP, HealthValues.skullRangedHP, HealthValues.kazHP);
                break;

            // Fighting two Royal Guards
            case 23:
                EnemyManagerScript.enemy1 = "Royal Guard 1";
                EnemyManagerScript.enemy2 = "Royal Guard 2";
                EnemyManagerScript.enemy3 = "null";
                
                EnemyManagerScript.ChangeEnemyLocation( 7, 9, 0 );
                HealthManagerScript.StartingHealth(HealthValues.royalGuardHP, HealthValues.royalGuardHP, 0);
                break;
            
            // Fighting the Skull King and his two Skull Grunts
            case 25:
                EnemyManagerScript.enemy1 = "Skull Grunt Melee";
                EnemyManagerScript.enemy2 = "Skull Grunt Ranged";
                EnemyManagerScript.enemy3 = "Skull King";
                
                EnemyManagerScript.ChangeEnemyLocation( 6, 8, 10 );
                HealthManagerScript.StartingHealth(HealthValues.skullMeleeHP, HealthValues.skullRangedHP, HealthValues.skullKingHP);
                break;
            
            // Fighting the Royal King and his two Royal Guards
            case 27:
                EnemyManagerScript.enemy1 = "Royal Guard 1";
                EnemyManagerScript.enemy2 = "Royal Guard 2";
                EnemyManagerScript.enemy3 = "Royal King";
                
                EnemyManagerScript.ChangeEnemyLocation( 6, 8, 10 );
                HealthManagerScript.StartingHealth(HealthValues.royalGuardHP, HealthValues.royalGuardHP, HealthValues.royalKingHP);
                break;
        }
    }
    
    
    
    public static void NextScene(bool skipScene)
    {
        if (!skipScene) currentScene++;
        else currentScene += 2;
    }
    
    
    public void Reset()
    {
        ChangeScene();
        
        GameWindowManager.S.ArrangeScreen();
        
        netrixiInParty = false;
        folkvarInParty = false;
        ivInParty = false;

        barkeeperMad = false;
        EnemyManagerScript.barkeeperMadNextRound = false;
        EnemyManagerScript.barkeeperMadNextAttack = false;
        AttackScript.chairsThrown = 0;

        chooseRandomKnight = 0;
        gameWinner = 0;

        ChangeChoiceText.S.ResetChoices(true);
        CharacterManagerScript.ResetVariables();
        EnemyManagerScript.ClearMoves();
    }
}
