﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class CombatManagerScript : MonoBehaviour
{
    // Can any of the characters attack?
    public static bool netrixiAttacks = false;
    public static bool folkvarAttacks = false;
    public static bool ivAttacks = false;

    public static int firstAttack = 0, secondAttack = 0;

    private bool isNetrixi = true, isFolkvar = true, isIv = true;
    
    public static int roundNumber = 1;
    
    public static bool netrixiAlive = true, folkvarAlive = true, ivAlive = true;
    
    public static bool enemy1Alive = true, enemy2Alive = true, enemy3Alive = true;

    public static int netrixiHP, folkvarHP, ivHP;
    public static int enemy1HP, enemy2HP, enemy3HP;
    
    public static int enemy1StartingHP, enemy2StartingHP, enemy3StartingHP;

    public static bool playerAttacking1 = false, playerAttacking2 = false, enemyAttacking1 = false, enemyAttacking2 = false;

    public static bool win = false, lose = false;

    public static bool hasWon = false, hasLost = false;
    public static bool hasRunSimulation = false;

    public static int target1Location, target2Location;

    public static bool canNetrixiAttack = true, canFolkvarAttack = true, canIvAttack = true;
    public static bool canEnemy1Attack = true, canEnemy2Attack = true, canEnemy3Attack = true;
    
    public static int netrixiTarget1Location, netrixiTarget2Location;
    public static int folkvarTarget1Location, folkvarTarget2Location;

    public GameObject netrixi, folkvar, iv;
    public GameObject enemy1, enemy2, enemy3;

    public GameObject victory;
    public static bool victorious;
    public static bool moveOnFromVictory;

    // Start is called before the first frame update
    void Start()
    {
        MarkerManagerScript.S.Reset();
        CharacterManagerScript.StartCombat();
        
        firstAttack = 0;
        secondAttack = 0;
        
        netrixiAttacks = false;
        folkvarAttacks = false;
        ivAttacks = false;

        NetrixiCombatScript.ResetNetrixiVariables();
        FolkvarCombatScript.ResetFolkvarVariables();
        IvCombatScript.ResetIvVariables();

        isNetrixi = true;
        isFolkvar = true; 
        isIv = true;

        roundNumber = 1;
        hasRunSimulation = false;
        
        EnemyManagerScript.barkeeperMadNextAttack = false;
        EnemyManagerScript.barkeeperMadNextRound = false;


        // Determine which characters are in the scene
        netrixiAlive = true;
        canNetrixiAttack = true;

        if (GameManagerScript.folkvarInParty)
        {
            folkvarAlive = true;
            canFolkvarAttack = true;
        }
        else
        {
            folkvarAlive = false;
            canFolkvarAttack = false;
        }

        if (GameManagerScript.ivInParty)
        {
            ivAlive = true;
            canIvAttack = true;
        }
        else
        {
            ivAlive = false;
            canIvAttack = false;
        }

        // Determine which enemies are in the scene
        enemy1Alive = true;
        canEnemy1Attack = true;
        
        if (EnemyManagerScript.enemy2 == "null")
        {
            enemy2Alive = false;
            canEnemy2Attack = false;
        }
        else
        {
            enemy2Alive = true;
            canEnemy2Attack = true;
        }

        if (EnemyManagerScript.enemy3 == "null")
        {
            enemy3Alive = false;
            canEnemy3Attack = false;
        }
        else
        {
            enemy3Alive = true;
            canEnemy3Attack = true;
        }
        

        playerAttacking1 = false;
        playerAttacking2 = false;
        enemyAttacking1 = false;
        enemyAttacking2 = false;

        
        // Reset any transformations
        NetrixiAttackScript.enemy1Transformed = false;
        NetrixiAttackScript.enemy2Transformed = false;
        NetrixiAttackScript.enemy3Transformed = false;

        AttackScript.playerBlocking = false;
        AttackScript.playerEmpowering = false;
        AttackScript.enemyBlocking = false;
        AttackScript.enemyEmpowering = false;
        AttackScript.countered = false;

        EnemyAttackScript.netrixiStunned = false;
        EnemyAttackScript.folkvarStunned = false;
        EnemyAttackScript.ivStunned = false;

        EnemyAttackScript.enemy1Stunned = false;
        EnemyAttackScript.enemy2Stunned = false;
        EnemyAttackScript.enemy3Stunned = false;
        
        canEnemy1Attack = true;
        canEnemy2Attack = true;
        canEnemy3Attack = true;
        
        canNetrixiAttack = true;
        canFolkvarAttack = true;
        canIvAttack = true;
        
        // Change enemy sprites
        ChangeSprites.S.ChangeEnemyCharacter(EnemyManagerScript.enemy1, enemy1.GetComponent<SpriteRenderer>());
        ChangeSprites.S.ChangeEnemyCharacter(EnemyManagerScript.enemy2, enemy2.GetComponent<SpriteRenderer>());
        ChangeSprites.S.ChangeEnemyCharacter(EnemyManagerScript.enemy3, enemy3.GetComponent<SpriteRenderer>());

        // Reset target locations
        netrixiTarget1Location = 0;
        netrixiTarget2Location = 0;
        
        folkvarTarget1Location = 0;
        folkvarTarget2Location = 0;

        win = false;
        lose = false;

        hasWon = false;
        hasLost = false;
        
        // Reset character health
        HealthManagerScript.ResetVariables();

        StartCoroutine(TipScript.S.DisplayCombatTip(true));
        victorious = false;
    }

    
    
    // Update is called once per frame
    void Update()
    {
        CharactersAlive();
        EndOfCombat();
        
        if (hasWon) win = false;
        if (hasLost) lose = false;

        if (win) hasWon = true;
        if (lose) hasLost = true;
        
        if (win)
        {
            // TODO: Play won-battle animation
            print("You won!...that battle");
            StartCoroutine(Victory());
            win = false;
        }
        else
        {
            if (lose)
            {
                // TODO: Play lost-battle animation
                if (GameManagerScript.currentScene != 7)
                {
                    GameManagerScript.previousScene = GameManagerScript.currentScene;
                    GameManagerScript.currentScene = 31;
                }
                else GameManagerScript.NextScene(false);
                print("You Lose");
                lose = false;
            }
        }

        // If in the Victory scene
        if (victorious)
        {
            CombatManagerScript.netrixiAttacks = false;
            CombatManagerScript.folkvarAttacks = false;
            CombatManagerScript.ivAttacks = false;
            
            if (moveOnFromVictory)
            {
                if (GameManagerScript.currentScene == 10) GameManagerScript.currentScene = 9;
                else GameManagerScript.NextScene(false);
                
                moveOnFromVictory = false;
                victorious = false;
            }
        }



        // If the Netrixi marker is visible
        if (MarkerManagerScript.netrixiMarker)
        {
            if (isNetrixi)
            {
                if (netrixiAlive)
                {
                    netrixiAttacks = true;
                    folkvarAttacks = false;
                    ivAttacks = false;

                    isNetrixi = false; 
                
                    print("Netrixi will attack with a spell");
                    
                    SFXManager.S.PlaySFX(43);
                }
            }
        }
        else
        {
            isNetrixi = true;
        }

        // If the Folkvar marker is visible
        if (MarkerManagerScript.folkvarMarker)
        {
            if (isFolkvar)
            {
                if (folkvarAlive)
                {
                    folkvarAttacks = true;
                    netrixiAttacks = false;
                    ivAttacks = false;
            
                    isFolkvar = false;
                
                    print("Folkvar will attack with his mighty weapons");
                    
                    SFXManager.S.PlaySFX(44);
                }
            }
        } 
        else
        {
            isFolkvar = true;
        }

        // If the Iv marker is visible
        if (MarkerManagerScript.ivMarker)
        {
            if (isIv)
            {
                if (ivAlive)
                {
                    ivAttacks = true;
                    netrixiAttacks = false;
                    folkvarAttacks = false;
            
                    isIv = false;
                
                    print("Iv will block with her force powers");
                    
                    SFXManager.S.PlaySFX(45);
                }
            }
        }
        else
        {
            isIv = true;
        }
        
        
        // Reset player attacks and move onto next round if player locks in their choices
        if (secondAttack != 0)
        {
            if (MarkerManagerScript.goMarker)
            {
                if (Input.GetKeyDown(KeyCode.V))
                {
                    if (!NetrixiCombatScript.netrixiCondition4[0] || !FolkvarCombatScript.folkvarCondition4[0] || !IvCombatScript.ivCondition4[0])
                    {
                        if (!hasRunSimulation)
                        {
                            // Run combat simulation
                            var go = new GameObject("runner");
                            var runner = go.AddComponent<CombatSimulationScript>();
                            runner.StartCoroutine(runner.RunSimulation(firstAttack, secondAttack, EnemyManagerScript.firstAttack, EnemyManagerScript.secondAttack, go));
                            hasRunSimulation = true;
                            
                            SFXManager.S.PlaySFX(22);
                        }
                    }
                }
            }
        }


        
        // Determine whether the player has chosen a combat move
        if (netrixiAttacks) if (CombatManagerScript.netrixiAlive) NetrixiCombatScript.NetrixiAttack();
        if (folkvarAttacks) if (CombatManagerScript.folkvarAlive) FolkvarCombatScript.FolkvarAttack();
        if (ivAttacks) if (CombatManagerScript.ivAlive) IvCombatScript.IvAction();



        // Undo a chosen move or attack
        if (MarkerManagerScript.undoMarker)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                // Determine what the player's first attack is
                if (firstAttack != 0)
                {
                    // Determine what the player's second attack is
                    if (secondAttack == 0)
                    {
                        // Reset attack
                        CharacterManagerScript.UndoMove();
                        firstAttack = 0;

                        netrixiTarget1Location = 0;
                        folkvarTarget1Location = 0;
                    } 
                    else
                    {
                        // Reset attack
                        CharacterManagerScript.UndoMove();
                        secondAttack = 0;
                        
                        netrixiTarget2Location = 0;
                        folkvarTarget2Location = 0;
                    }
                }
                
                SFXManager.S.PlaySFX(24);
            }
        }
        
        // Reset target locations if they are not chosen
        if (firstAttack != 0)
        {
            if (firstAttack != 2) netrixiTarget1Location = 0;
            if (firstAttack != 5) folkvarTarget1Location = 0;
        }
        if (secondAttack != 0)
        {
            if (secondAttack != 2) netrixiTarget2Location = 0;
            if (secondAttack != 5) folkvarTarget2Location = 0;
        }
    }
    
    
    
    
    public static void NetrixiSpellCast()
    {
        if (!canNetrixiAttack)
        {
            print("Netrixi is currently unable to attack! She can only dodge attacks this round");
        }
        else
        {
            // Netrixi casts her first spell
            if (NetrixiCombatScript.netrixiCondition1[0] && NetrixiCombatScript.netrixiCondition1[1])
            {
                print("Netrixi casts her first spell");
                
                // Make Netrixi attack in game 
                if (firstAttack != 0)
                {
                    if (secondAttack == 0)
                    {
                        if (firstAttack != 1)
                        {
                            secondAttack = 1;
                            SFXManager.S.PlaySFX(23);
                        }
                        else print("Can't choose the same move twice");
                    }
                }
                else
                {
                    firstAttack = 1;
                    SFXManager.S.PlaySFX(23);
                }


                NetrixiCombatScript.ResetNetrixiVariables();
            }
                
            // Netrixi casts her second spell
            if (NetrixiCombatScript.netrixiCondition2[0] && NetrixiCombatScript.netrixiCondition2[1])
            {
                print("Netrixi casts her second spell");
                
                // Make Netrixi attack in game 
                if (firstAttack != 0)
                {
                    if (secondAttack == 0)
                    {
                        if (firstAttack != 2)
                        {
                            target2Location = NetrixiCombatScript.netrixiRotation;
                            secondAttack = 2;
                            SFXManager.S.PlaySFX(23);
                        }
                        else print("Can't choose the same move twice");
                    }
                }
                else
                {
                    target1Location = NetrixiCombatScript.netrixiRotation;
                    firstAttack = 2;
                    SFXManager.S.PlaySFX(23);
                }
                

                NetrixiCombatScript.ResetNetrixiVariables();
            }
                
            // Netrixi casts her third spell
            if (NetrixiCombatScript.netrixiCondition3[0] && NetrixiCombatScript.netrixiCondition3[1] && NetrixiCombatScript.netrixiCondition3[2] && NetrixiCombatScript.netrixiCondition3[3])
            {
                print("Netrixi casts her third spell");
                
                // Make Netrixi attack in game 
                if (firstAttack != 0)
                {
                    if (secondAttack == 0)
                    {
                        if (firstAttack != 3)
                        {
                            secondAttack = 3;
                            SFXManager.S.PlaySFX(23);
                        }
                        else print("Can't choose the same move twice");
                    }
                }
                else
                {
                    firstAttack = 3;
                    SFXManager.S.PlaySFX(23);
                }
                

                NetrixiCombatScript.ResetNetrixiVariables();
            }
        }


        // Netrixi moves
        if (NetrixiCombatScript.netrixiCondition4[0])
        {
            // If Netrixi moves to the left
            if (NetrixiCombatScript.netrixiCondition4[1])
            {
                print("Netrixi moves to the left");
            
                // Make Netrixi move in game 
                if (firstAttack != 0)
                {
                    if (secondAttack == 0)
                    {
                        secondAttack = 10;
                        SFXManager.S.PlaySFX(23);
                    }
                }
                else
                {
                    firstAttack = 10;
                    SFXManager.S.PlaySFX(23);
                }
            
                NetrixiCombatScript.ResetNetrixiVariables();
            }
            
            // If Netrixi moves to the right
            if (NetrixiCombatScript.netrixiCondition4[2])
            {
                print("Netrixi moves to the right");
            
                // Make Netrixi move in game 
                if (firstAttack != 0)
                {
                    if (secondAttack == 0)
                    {
                        secondAttack = 11;
                        SFXManager.S.PlaySFX(23);
                    }
                }
                else
                {
                    firstAttack = 11;
                    SFXManager.S.PlaySFX(23);
                }
            
                NetrixiCombatScript.ResetNetrixiVariables();
            }
        }
    }
    
    
    
    
    public static void FolkvarMeleeAttack()
    {
        if (!canFolkvarAttack)
        {
            print("Folkvar is currently unable to attack! He can only dodge attacks this round");
        }
        else
        {
            // Folkvar uses his first melee attack
            if (FolkvarCombatScript.folkvarCondition1[0] && FolkvarCombatScript.folkvarCondition1[1])
            {
                print("Folkvar uses his first attack");
                
                // Make Folkvar attack in game 
                if (firstAttack != 0)
                {
                    if (secondAttack == 0)
                    {
                        if (firstAttack != 4)
                        {
                            secondAttack = 4;
                            SFXManager.S.PlaySFX(23);
                        }
                        else print("Can't choose the same move twice");
                    }
                }
                else
                {
                    firstAttack = 4;
                    SFXManager.S.PlaySFX(23);
                }


                FolkvarCombatScript.ResetFolkvarVariables();
            }
                
            // Folkvar uses his second melee attack
            if (FolkvarCombatScript.folkvarCondition2[0] && FolkvarCombatScript.folkvarCondition2[1] && FolkvarCombatScript.folkvarCondition2[2])
            {
                print("Folkvar uses his second attack");
                
                // Make Folkvar attack in game 
                if (firstAttack != 0)
                {
                    if (secondAttack == 0)
                    {
                        if (firstAttack != 5)
                        {
                            DetermineTargetEnemy(2);
                            secondAttack = 5;
                            SFXManager.S.PlaySFX(23);
                        }
                        else print("Can't choose the same move twice");
                    }
                }
                else
                {
                    DetermineTargetEnemy(1);
                    firstAttack = 5;
                    SFXManager.S.PlaySFX(23);
                }

                void DetermineTargetEnemy(int attackNumber)
                {
                    int targetEnemy = 0;

                    if (attackNumber == 1)
                    {
                        CombatSimulationScript.DetermineEnemy(EnemyManagerScript.firstAttack, 1);
                        targetEnemy = CombatSimulationScript.enemyAttacker1;
                    }
                    else
                    {
                        CombatSimulationScript.DetermineEnemy(EnemyManagerScript.secondAttack, 2);
                        targetEnemy = CombatSimulationScript.enemyAttacker2;
                    }
                    
                    switch (targetEnemy)
                    {
                        case 1:
                            if (attackNumber == 1) folkvarTarget1Location = EnemyManagerScript.enemy1Position;
                            else folkvarTarget2Location = EnemyManagerScript.enemy1Position;
                            break;
                        
                        case 2:
                            if (attackNumber == 1) folkvarTarget1Location = EnemyManagerScript.enemy2Position;
                            else folkvarTarget2Location = EnemyManagerScript.enemy2Position;
                            break;
                        
                        case 3:
                            if (attackNumber == 1) folkvarTarget1Location = EnemyManagerScript.enemy3Position;
                            else folkvarTarget2Location = EnemyManagerScript.enemy3Position;
                            break;
                    }
                }
                

                FolkvarCombatScript.ResetFolkvarVariables();
            }
                
            // Folkvar uses his third melee attack
            if (FolkvarCombatScript.folkvarCondition3[0] && FolkvarCombatScript.folkvarCondition3[1] && FolkvarCombatScript.folkvarCondition3[2])
            {
                if (GameManagerScript.currentScene >= 18)
                {
                    print("Folkvar uses his third attack");
                
                    // Make Folkvar attack in game 
                    if (firstAttack != 0)
                    {
                        if (secondAttack == 0)
                        {
                            if (firstAttack != 6)
                            {
                                DeterminGroupedEnemies(2);
                                secondAttack = 6;
                                SFXManager.S.PlaySFX(23);
                            }
                            else print("Can't choose the same move twice");
                        }
                    }
                    else
                    {
                        DeterminGroupedEnemies(1);
                        firstAttack = 6;
                        SFXManager.S.PlaySFX(23);
                    }
                
                    
                    void DeterminGroupedEnemies(int attackNumber)
                    {
                        if (EnemyManagerScript.enemy2Position == EnemyManagerScript.enemy1Position + 1)
                        {
                            if (EnemyManagerScript.enemy3Position == EnemyManagerScript.enemy2Position + 1)
                            {
                                if (attackNumber == 1) folkvarTarget1Location = EnemyManagerScript.enemy2Position;
                                else folkvarTarget2Location = EnemyManagerScript.enemy2Position;
                            }
                            else
                            {
                                if (attackNumber == 1) folkvarTarget1Location = EnemyManagerScript.enemy1Position;
                                else folkvarTarget2Location = EnemyManagerScript.enemy1Position;
                            }
                        }
                        else
                        {
                            if (EnemyManagerScript.enemy3Position == EnemyManagerScript.enemy2Position + 1)
                            {
                                if (attackNumber == 1) folkvarTarget1Location = EnemyManagerScript.enemy2Position;
                                else folkvarTarget2Location = EnemyManagerScript.enemy2Position;
                            }
                            else
                            {
                                if (enemy1Alive)
                                {
                                    if (attackNumber == 1) folkvarTarget1Location = EnemyManagerScript.enemy1Position;
                                    else folkvarTarget2Location = EnemyManagerScript.enemy1Position;
                                }
                                else if (enemy2Alive)
                                {
                                    if (attackNumber == 1) folkvarTarget1Location = EnemyManagerScript.enemy2Position;
                                    else folkvarTarget2Location = EnemyManagerScript.enemy2Position;
                                }
                                else
                                {
                                    if (attackNumber == 1) folkvarTarget1Location = EnemyManagerScript.enemy3Position;
                                    else folkvarTarget2Location = EnemyManagerScript.enemy3Position;
                                }
                            }
                        }
                    }

                    FolkvarCombatScript.ResetFolkvarVariables();
                }
                else
                {
                    print("Can't use this attack yet, you must defeat Kaz first!");
                    
                    FolkvarCombatScript.ResetFolkvarVariables();
                }
            }
        }


        // Folkvar moves
        if (FolkvarCombatScript.folkvarCondition4[0])
        {
            // If Folkvar moves to the left
            if (FolkvarCombatScript.folkvarCondition4[1])
            {
                print("Folkvar moves to the left");
            
                // Make Folkvar move in game 
                if (firstAttack != 0)
                {
                    if (secondAttack == 0)
                    {
                        secondAttack = 12;
                        SFXManager.S.PlaySFX(23);
                    }
                }
                else
                {
                    firstAttack = 12;
                    SFXManager.S.PlaySFX(23);
                }
            
                FolkvarCombatScript.ResetFolkvarVariables();
            }
            
            // If Folkvar moves to the right
            if (FolkvarCombatScript.folkvarCondition4[2])
            {
                print("Folkvar moves to the right");
            
                // Make Folkvar move in game 
                if (firstAttack != 0)
                {
                    if (secondAttack == 0)
                    {
                        secondAttack = 13;
                        SFXManager.S.PlaySFX(23);
                    }
                }
                else
                {
                    firstAttack = 13;
                    SFXManager.S.PlaySFX(23);
                }
            
                FolkvarCombatScript.ResetFolkvarVariables();
            }
        }
    }
    
    
    
    
    public static void IvTeamSupport()
    {
        if (!canIvAttack)
        {
            print("Iv is currently unable to attack! She can only dodge attacks this round");
        }
        else
        {
             // Iv uses their first ability
            if (IvCombatScript.ivCondition1[0] && IvCombatScript.ivCondition1[1])
            {
                print("Iv blocks the next attack");
                
                // Make Iv block in game 
                if (firstAttack != 0)
                {
                    if (secondAttack == 0)
                    {
                        if (firstAttack != 7)
                        {
                            secondAttack = 7;
                            SFXManager.S.PlaySFX(23);
                        }
                        else print("Can't choose the same move twice");
                    }
                }
                else
                {
                    firstAttack = 7;
                    SFXManager.S.PlaySFX(23);
                }


                IvCombatScript.ResetIvVariables();
            }

            // Iv uses their second ability
            if (IvCombatScript.ivCondition2[0] && IvCombatScript.ivCondition2[1] && IvCombatScript.ivCondition2[2] && IvCombatScript.ivCondition2[3])
            {
                print("Iv heals the weakest party");
                
                // Make Iv heal in game 
                if (firstAttack != 0)
                {
                    if (secondAttack == 0)
                    {
                        if (firstAttack != 8)
                        {
                            secondAttack = 8;
                            SFXManager.S.PlaySFX(23);
                        }
                        else print("Can't choose the same move twice");
                    }
                }
                else
                {
                    firstAttack = 8;
                    SFXManager.S.PlaySFX(23);
                }
                    
                IvCombatScript.ResetIvVariables();
            }
            
            // Iv uses their third ability
            if (IvCombatScript.ivCondition3[0] && IvCombatScript.ivCondition3[1])
            {
                if (GameManagerScript.currentScene >= 18)
                {
                    print("Iv empowers both teams");
                
                    // Make Iv empower in game 
                    if (firstAttack != 0)
                    {
                        if (secondAttack == 0)
                        {
                            if (firstAttack != 9)
                            {
                                target2Location = IvCombatScript.targetLocation;
                                secondAttack = 9;
                                SFXManager.S.PlaySFX(23);
                            }
                            else print("Can't choose the same move twice");
                        }
                    }
                    else
                    {
                        target1Location = IvCombatScript.targetLocation;
                        firstAttack = 9;
                        SFXManager.S.PlaySFX(23);
                    }
                    
                    IvCombatScript.ResetIvVariables();
                }
                else
                {
                    print("Can't use this attack yet, you must defeat Kaz first!");
                    
                    IvCombatScript.ResetIvVariables();
                }
            }
        }


        // Iv moves
        if (IvCombatScript.ivCondition4[0])
        {
            // If Iv moves to the left
            if (IvCombatScript.ivCondition4[1])
            {
                print("Iv moves to the left");
            
                // Make Iv move in game 
                if (firstAttack != 0)
                {
                    if (secondAttack == 0)
                    {
                        secondAttack = 14;
                        SFXManager.S.PlaySFX(23);
                    }
                }
                else
                {
                    firstAttack = 14;
                    SFXManager.S.PlaySFX(23);
                }
            
                IvCombatScript.ResetIvVariables();
            }
            
            // If Iv moves to the right
            if (IvCombatScript.ivCondition4[2])
            {
                print("Iv moves to the right");
            
                // Make Iv move in game 
                if (firstAttack != 0)
                {
                    if (secondAttack == 0)
                    {
                        secondAttack = 15;
                        SFXManager.S.PlaySFX(23);
                    }
                }
                else
                {
                    firstAttack = 15;
                    SFXManager.S.PlaySFX(23);
                }
            
                IvCombatScript.ResetIvVariables();
            }
        }
    }

    
    


    public static void CharactersAlive()
    {
        // Determine whether the Main Characters are still alive
        if (netrixiHP <= 0)
        {
            // Play death animation
            netrixiAlive = false;
            canNetrixiAttack = false;
        }

        if (folkvarHP <= 0)
        {
            // Play death animation
            folkvarAlive = false;
            canFolkvarAttack = false;
        }

        if (ivHP <= 0)
        {
            // Play death animation
            ivAlive = false;
            canIvAttack = false;
        }


        // Determine whether the Enemy Characters are still alive
        if (enemy1HP <= 0)
        {
            // Play death animation
            enemy1Alive = false;
            canEnemy1Attack = false;
        }

        if (enemy2HP <= 0)
        {
            // Play death animation
            enemy2Alive = false;
            canEnemy2Attack = false;
        }

        if (enemy3HP <= 0)
        {
            // Play death animation
            enemy3Alive = false;
            canEnemy3Attack = false;
        }
        
        HealthManagerScript.CheckHealth();
    }


    public static void EndOfCombat()
    {
        // WIN CONDITION
        if (!enemy1Alive)
        {
            if (EnemyManagerScript.enemy2 != "null")
            {
                if (!enemy2Alive)
                {
                    if (EnemyManagerScript.enemy3 != "null")
                    {
                        // If all enemies 1 + 2 + 3 are dead
                        if (!enemy3Alive) win = true;
                    }
                    
                    // If enemies 1 + 2 are dead
                    else win = true;
                }
            }
            
            // If enemy 1 is dead
            else win = true;
        }
        else
        {
            win = false;
        }
        
        
        // LOSE CONDITION
        if (!netrixiAlive)
        {
            if (GameManagerScript.folkvarInParty)
            {
                if (!folkvarAlive)
                {
                    if (GameManagerScript.ivInParty)
                    {
                        // If Netrixi, Folkvar, and Iv are dead
                        if (!ivAlive) lose = true;
                    }
                    
                    // If Netrixi and Folkvar are dead
                    else lose = true;
                }
            }
            
            // If Netrixi is dead
            else lose = true;
        }
        else
        {
            lose = false;
        }
    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(1.25f);
        
        victory.SetActive(true);
        victorious = true;
        
        SFXManager.S.PlaySFX(21);
    }
}
