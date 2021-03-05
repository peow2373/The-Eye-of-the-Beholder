using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static bool rightHanded = true;

    public static int currentScene = 0, previousScene = 0;

    private string combatScene = "CombatDraft3";


    public static bool netrixiInParty = true, folkvarInParty = false, ivInParty = false;

    public static bool barkeeperMad = false;

    private int randomIndex;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        
        randomIndex = UnityEngine.Random.Range(0,2);
    }

    // Update is called once per frame
    void Update()
    {
        // Manually swap to the next scene
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentScene == 8 || currentScene == 9) currentScene += 2;
            
            else if (currentScene == 10) currentScene--;
            
            else currentScene++;
        }

        // Change the scene
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "StartGame")
        {
            currentScene = 0;
            SceneManager.LoadScene("InkIntro");
        }

        DetermineNextCombat();

        ChangeScene();
    }


    void SceneChangeAnimation()
    {
        // TODO: Play scene change animation
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
                    SceneManager.LoadScene("InkIntro");
                    previousScene = 0;
                    print("Tutorial Scene");
                }
                break;
            
                                                            // Spooky forest
            
            // Dialogue with Pirate
            case 1:
                if (previousScene != 1)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene("InkNetrixiPirate");
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
                    SceneManager.LoadScene("InkNetrixiMansion");
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
                    SceneChangeAnimation();
                    SceneManager.LoadScene("InkRoad");
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
            
                                                            // Semi-Friendly tavern
            
            // Dialogue with people in Tavern before fight
            case 8:
                if (previousScene != 8)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene("InkTavern");
                    previousScene = 8;
                    print("Dialogue with people in Tavern before fight");
                }
                break;
            
            // Dialogue with people in Tavern after fight
            case 9:
                if (previousScene != 9)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene("InkTavernAfterFight");
                    previousScene = 9;
                    print("Dialogue with people in Tavern after fight");
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
                    SceneManager.LoadScene("InkCastle");
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
                    SceneManager.LoadScene("InkThrone");
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
                    SceneManager.LoadScene("InkTunnelEntrance");
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
                    SceneManager.LoadScene("InkKazBoss");
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
                    SceneManager.LoadScene("InkTunnelExit");
                    previousScene = 18;
                    print("Dialogue between Netrixi, Folkvar, Iv");
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
                    SceneChangeAnimation();
                    SceneManager.LoadScene("InkInsideVolcano");
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
                    SceneManager.LoadScene("InkMine");
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
                    SceneManager.LoadScene("InkMoke");
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
                    SceneManager.LoadScene("InkFinalDecision");
                    previousScene = 28;
                    print("Dialogue after defeating the Royal King");
                }
                break;
            
            
            
            // End game
            case 29:
                if (previousScene != 29)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene("InkEpilogue");
                    previousScene = 29;
                    
                    print("Epilogue");
                }
                break;
            
            // Restart game
            case 30:
                if (previousScene != 30)
                {
                    SceneChangeAnimation();
                    SceneManager.LoadScene("InkIntro");

                    currentScene = 0;
                    previousScene = 30;

                    print("Restarting Game");
                    
                    Reset();
                }
                break;
        }
    }
    
    
    
    
    void DetermineNextCombat()
    {
        // Determine if the character has joined the party yet
        if (currentScene >= 2) netrixiInParty = true;
        if (currentScene >= 7) folkvarInParty = true;
        if (currentScene >= 12) ivInParty = true;
        
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
                if (randomIndex == 0)
                {
                    EnemyManagerScript.enemy1 = "Folkvar";
                    EnemyManagerScript.enemy2 = "Royal Knight Melee";
                    EnemyManagerScript.enemy3 = "null";
                    
                    EnemyManagerScript.ChangeEnemyLocation( 7, 8, 0 );
                    HealthManagerScript.StartingHealth(HealthValues.folkvarHP, HealthValues.knightMeleeHP, 0);
                }
                else if (randomIndex == 1 || randomIndex == 2)
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
                
                EnemyManagerScript.ChangeEnemyLocation( 6, 8, 9 );
                HealthManagerScript.StartingHealth(HealthValues.skullMeleeHP, HealthValues.skullRangedHP, HealthValues.kazHP);
                break;
            
            // Fighting the Tavern Brute
            case 10:
                EnemyManagerScript.enemy1 = "Tavern Brute";
                
                if (barkeeperMad) EnemyManagerScript.enemy2 = "Barkeeper";
                else EnemyManagerScript.enemy2 = "null";
                
                EnemyManagerScript.enemy3 = "null";
                
                EnemyManagerScript.ChangeEnemyLocation( 6, 8, 0 );
                HealthManagerScript.StartingHealth(HealthValues.bruteHP, HealthValues.barkeeperHP, 0);
                break;

            // Fighting the Gatekeeper
            case 12:
                EnemyManagerScript.enemy1 = "Royal Knight Melee";
                EnemyManagerScript.enemy2 = "Royal Knight Ranged";
                EnemyManagerScript.enemy3 = "Gatekeeper";
                
                EnemyManagerScript.ChangeEnemyLocation( 6, 7, 10 );
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
                
                EnemyManagerScript.ChangeEnemyLocation( 7, 8, 0 );
                HealthManagerScript.StartingHealth(HealthValues.royalGuardHP, HealthValues.royalGuardHP, 0);
                break;
            
            // Fighting the Skull King and his two Skull Grunts
            case 25:
                EnemyManagerScript.enemy1 = "Skull Grunt Melee";
                EnemyManagerScript.enemy2 = "Skull Grunt Ranged";
                EnemyManagerScript.enemy3 = "Skull King";
                
                EnemyManagerScript.ChangeEnemyLocation( 7, 9, 10 );
                HealthManagerScript.StartingHealth(HealthValues.skullMeleeHP, HealthValues.skullRangedHP, HealthValues.skullKingHP);
                break;
            
            // Fighting the Royal King and his two Royal Guards
            case 27:
                EnemyManagerScript.enemy1 = "Royal Guard 1";
                EnemyManagerScript.enemy2 = "Royal Guard 2";
                EnemyManagerScript.enemy3 = "Royal King";
                
                EnemyManagerScript.ChangeEnemyLocation( 6, 7, 9 );
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
        
        netrixiInParty = false;
        folkvarInParty = false;
        ivInParty = false;

        barkeeperMad = false;
        EnemyManagerScript.barkeeperMadNextRound = false;

        CharacterManagerScript.ResetVariables();
        EnemyManagerScript.ClearMoves();
    }
}
