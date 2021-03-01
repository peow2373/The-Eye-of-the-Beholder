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

    public static bool playerAttacking1 = false, playerAttacking2 = false, enemyAttacking1 = false, enemyAttacking2 = false;

    public static bool win = false, lose = false;

    public static bool hasWon = false, hasLost = false;

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

        netrixiAlive = true;
        folkvarAlive = true;
        ivAlive = true;
        
        enemy1Alive = true;
        enemy2Alive = true;
        enemy3Alive = true;

        playerAttacking1 = false;
        playerAttacking2 = false;
        enemyAttacking1 = false;
        enemyAttacking2 = false;
        
        win = false;
        lose = false;

        hasWon = false;
        hasLost = false;
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
            GameManagerScript.NextScene(false);
            print("You won!...that battle");
            win = false;
        }
        else
        {
            if (lose)
            {
                // TODO: Play lost-battle animation
                GameManagerScript.NextScene(false);
                print("You Lose");
                lose = false;
            }
        }

        // If the Netrixi marker is visible
        if (MarkerManagerScript.netrixiMarker)
        {
            if (isNetrixi)
            {
                if (netrixiAlive && GameManagerScript.netrixiInParty)
                {
                    netrixiAttacks = true;
                    folkvarAttacks = false;
                    ivAttacks = false;

                    isNetrixi = false; 
                
                    print("Netrixi will attack with a spell");
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
                if (folkvarAlive && GameManagerScript.folkvarInParty)
                {
                    folkvarAttacks = true;
                    netrixiAttacks = false;
                    ivAttacks = false;
            
                    isFolkvar = false;
                
                    print("Folkvar will attack with his mighty weapons");
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
                if (ivAlive && GameManagerScript.ivInParty)
                {
                    ivAttacks = true;
                    netrixiAttacks = false;
                    folkvarAttacks = false;
            
                    isIv = false;
                
                    print("Iv will block with her force powers");
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
                        // Run combat simulation
                        var go = new GameObject("runner");
                        var runner = go.AddComponent<CombatSimulationScript>();
                        runner.StartCoroutine(runner.RunSimulation(firstAttack, secondAttack, EnemyManagerScript.firstAttack, EnemyManagerScript.secondAttack, go));

                        //firstAttack = 0;
                        //secondAttack = 0;
                
                        //netrixiAttacks = false;
                        //folkvarAttacks = false;
                        //ivAttacks = false;
            
                        //NetrixiCombatScript.ResetNetrixiVariables();
                        //FolkvarCombatScript.ResetFolkvarVariables();
                        //IvCombatScript.ResetIvVariables();
                
                        //roundNumber += 1;
                    
                        //CharacterManagerScript.ResetVariables();
                        //EnemyManagerScript.ClearMoves();
                    }
                }
            }
        }


        
        // Determine whether the player has chosen a combat move
        if (netrixiAttacks) NetrixiCombatScript.NetrixiAttack();
        if (folkvarAttacks) FolkvarCombatScript.FolkvarAttack();
        if (ivAttacks) IvCombatScript.IvAction();



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
                    } 
                    else
                    {
                        // Reset attack
                        CharacterManagerScript.UndoMove();
                        secondAttack = 0;
                    }
                }
            }
        }
    }
    
    
    
    
    public static void NetrixiSpellCast()
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
                    if (firstAttack != 1) secondAttack = 1;
                    else print("Can't choose the same move twice");
                }
            }
            else firstAttack = 1;


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
                    if (firstAttack != 2) secondAttack = 2;
                    else print("Can't choose the same move twice");
                }
            }
            else firstAttack = 2;
            

            NetrixiCombatScript.ResetNetrixiVariables();
        }
            
        // Netrixi casts her third spell
        if (NetrixiCombatScript.netrixiCondition3[0] && NetrixiCombatScript.netrixiCondition3[1] && NetrixiCombatScript.netrixiCondition3[2] && NetrixiCombatScript.netrixiCondition3[3] && NetrixiCombatScript.netrixiCondition3[4])
        {
            print("Netrixi casts her third spell");
            
            // Make Netrixi attack in game 
            if (firstAttack != 0)
            {
                if (secondAttack == 0)
                {
                    if (firstAttack != 3) secondAttack = 3;
                    else print("Can't choose the same move twice");
                }
            }
            else firstAttack = 3;
            

            NetrixiCombatScript.ResetNetrixiVariables();
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
                    if (secondAttack == 0) secondAttack = 10;
                }
                else firstAttack = 10;
            
                NetrixiCombatScript.ResetNetrixiVariables();
            }
            
            // If Netrixi moves to the right
            if (NetrixiCombatScript.netrixiCondition4[2])
            {
                print("Netrixi moves to the right");
            
                // Make Netrixi move in game 
                if (firstAttack != 0)
                {
                    if (secondAttack == 0) secondAttack = 11;
                }
                else firstAttack = 11;
            
                NetrixiCombatScript.ResetNetrixiVariables();
            }
        }
    }
    
    
    
    
    public static void FolkvarMeleeAttack()
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
                    if (firstAttack != 4) secondAttack = 4;
                    else print("Can't choose the same move twice");
                }
            }
            else firstAttack = 4;


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
                    if (firstAttack != 5) secondAttack = 5;
                    else print("Can't choose the same move twice");
                }
            }
            else firstAttack = 5;
            

            FolkvarCombatScript.ResetFolkvarVariables();
        }
            
        // Folkvar uses his third melee attack
        if (FolkvarCombatScript.folkvarCondition3[0] && FolkvarCombatScript.folkvarCondition3[1] && FolkvarCombatScript.folkvarCondition3[2])
        {
            print("Folkvar uses his third attack");
            
            // Make Folkvar attack in game 
            if (firstAttack != 0)
            {
                if (secondAttack == 0)
                {
                    if (firstAttack != 6) secondAttack = 6;
                    else print("Can't choose the same move twice");
                }
            }
            else firstAttack = 6;
            

            FolkvarCombatScript.ResetFolkvarVariables();
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
                    if (secondAttack == 0) secondAttack = 12;
                }
                else firstAttack = 12;
            
                FolkvarCombatScript.ResetFolkvarVariables();
            }
            
            // If Folkvar moves to the right
            if (FolkvarCombatScript.folkvarCondition4[2])
            {
                print("Folkvar moves to the right");
            
                // Make Folkvar move in game 
                if (firstAttack != 0)
                {
                    if (secondAttack == 0) secondAttack = 13;
                }
                else firstAttack = 13;
            
                FolkvarCombatScript.ResetFolkvarVariables();
            }
        }
    }
    
    
    
    
    public static void IvTeamSupport()
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
                    if (firstAttack != 7) secondAttack = 7;
                    else print("Can't choose the same move twice");
                }
            }
            else firstAttack = 7;


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
                    if (firstAttack != 8) secondAttack = 8;
                    else print("Can't choose the same move twice");
                }
            }
            else firstAttack = 8;
            

            IvCombatScript.ResetIvVariables();
        }
        
        // Iv uses their third ability
        if (IvCombatScript.ivCondition3[0] && IvCombatScript.ivCondition3[1])
        {
            print("Iv empowers both teams");
            
            // Make Iv empower in game 
            if (firstAttack != 0)
            {
                if (secondAttack == 0)
                {
                    if (firstAttack != 9) secondAttack = 9;
                    else print("Can't choose the same move twice");
                }
            }
            else firstAttack = 9;
            

            IvCombatScript.ResetIvVariables();
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
                    if (secondAttack == 0) secondAttack = 14;
                }
                else firstAttack = 14;
            
               IvCombatScript.ResetIvVariables();
            }
            
            // If Iv moves to the right
            if (IvCombatScript.ivCondition4[2])
            {
                print("Iv moves to the right");
            
                // Make Iv move in game 
                if (firstAttack != 0)
                {
                    if (secondAttack == 0) secondAttack = 15;
                }
                else firstAttack = 15;
            
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
        }
        else netrixiAlive = true;

        if (folkvarHP <= 0)
        {
            // Play death animation
            folkvarAlive = false;
        }
        else folkvarAlive = true;

        if (ivHP <= 0)
        {
            // Play death animation
            ivAlive = false;
        }
        else ivAlive = true;

        
        // Determine whether the Enemy Characters are still alive
        if (enemy1HP <= 0)
        {
            // Play death animation
            enemy1Alive = false;
        }
        else enemy1Alive = true;

        if (enemy2HP <= 0)
        {
            // Play death animation
            enemy2Alive = false;
        }
        else enemy2Alive = true;

        if (enemy3HP <= 0)
        {
            // Play death animation
            enemy3Alive = false;
        }
        else enemy3Alive = true;
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
}
