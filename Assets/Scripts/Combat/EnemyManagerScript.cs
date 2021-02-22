using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManagerScript : MonoBehaviour
{
    public static string enemy1 = null, enemy2 = null, enemy3 = null;

    public static List<String> availableMoves = new List<String>();

    public static string firstAttack, secondAttack;

    public static int enemy1Position, enemy2Position, enemy3Position;
    public static int enemy1First, enemy2First, enemy3First;
    public static int enemy1Second, enemy2Second, enemy3Second;

    public static bool hasChangedScene = false;

    // Start is called before the first frame update
    void Start()
    {
        ClearMoves(1);

        hasChangedScene = false;
        
        // Determine enemies present within the scene
        DetermineEnemyType(enemy1);
        DetermineEnemyType(enemy2);
        DetermineEnemyType(enemy3);
    }

    // Update is called once per frame
    void Update()
    {
        // If the Tavern Brute angers the Barkeeper and causes him to join the fight
        if (firstAttack == "Tavern Brute Throws Chair" || secondAttack == "Tavern Brute Throws Chair")
        {
            GameManagerScript.barkeeperMad = true;

            print("Barkeeper Joins the Fight!");
            
            DetermineEnemyType(enemy1);
            DetermineEnemyType(enemy2);
            DetermineEnemyType(enemy3);
        }
        
        
        // Determine enemy choices for their first attack
        if (firstAttack == "null")
        {
            DetermineEnemyChoice(1);
        }
        else
        {
            if (secondAttack == "null")
            {
                DetermineEnemyChoice(2);
            }
        }

        
        if (firstAttack != "null") Debug.Log(firstAttack);
        if (secondAttack != "null") Debug.Log(secondAttack);
    }


    void DetermineEnemyChoice(int attackNumber)
    {
        // Choose the first attack
        if (attackNumber == 1)
        {
            DetermineAvailableMoves(1);
            
            // Choose a random move for the enemy to make
            var randomIndex = UnityEngine.Random.Range(0,(availableMoves.Count));
            
            // Lock in the enemy's choice for their first attack
            firstAttack = availableMoves[randomIndex];
            
            availableMoves.Clear();
            
            UpdateVariables(1);
        }
        
        // Choose the second attack
        if (attackNumber == 2)
        {
            // Determine enemies present within the scene again
            DetermineEnemyType(enemy1);
            DetermineEnemyType(enemy2);
            DetermineEnemyType(enemy3);
            
            DetermineAvailableMoves(2);
            
            // Choose a random move for the enemy to make
            var randomIndex = UnityEngine.Random.Range(0,(availableMoves.Count));

            // Lock in the enemy's choice for their second attack
            secondAttack = availableMoves[FindDuplicateActions(randomIndex)];
                
            UpdateVariables(2);
        }
    }


    int FindDuplicateActions(int chosenSecondAttack)
    {
        for (int i = 0; i < availableMoves.Count; i++)
        {
            if (availableMoves[i] == firstAttack)
            {
                // If the chosen second attack is the same as the first attack
                if (chosenSecondAttack == i)
                {
                    // Choose a new random move for the enemy to make
                    int randomIndex = UnityEngine.Random.Range(0,(availableMoves.Count));
                    print("New second attack chosen");

                    // Call recursive function again to check if the new chosen attack meets the same conditions
                    int repeatTest = FindDuplicateActions(randomIndex);
                    return repeatTest;
                }
            }
            
            // If the first move the enemy made was an attack
            if (firstAttack == "Enemy 1 Moves Left" || firstAttack == "Enemy 1 Moves Right" || firstAttack == "Enemy 2 Moves Left" || firstAttack == "Enemy 2 Moves Right" || firstAttack == "Enemy 3 Moves Left" || firstAttack == "Enemy 3 Moves Right")
            {
                // If the chosen second attack was also decided to be a move
                if (availableMoves[chosenSecondAttack] == "Enemy 1 Moves Left" || availableMoves[chosenSecondAttack] == "Enemy 1 Moves Right" || availableMoves[chosenSecondAttack] == "Enemy 2 Moves Left" || availableMoves[chosenSecondAttack] == "Enemy 2 Moves Right" || availableMoves[chosenSecondAttack] == "Enemy 3 Moves Left" || availableMoves[chosenSecondAttack] == "Enemy 3 Moves Right")
                {
                    // Choose a new random move for the enemy to make
                    int randomIndex = UnityEngine.Random.Range(0,(availableMoves.Count));
                    print("New second attack chosen");

                    // Call recursive function again to check if the new chosen attack meets the same conditions
                    int repeatTest = FindDuplicateActions(randomIndex);
                    return repeatTest;
                }
            }
        }

        return chosenSecondAttack;
    }


    void DetermineAvailableMoves(int attackNumber)
    {
        if (attackNumber == 1)
        {
            // If Enemy 1 can move left
            if (CanEnemyMoveLeft(enemy1Position))
            {
                availableMoves.Add("Enemy 1 Moves Left");
            }
            
            // If Enemy 1 can move right
            if (CanEnemyMoveRight(enemy1Position))
            {
                availableMoves.Add("Enemy 1 Moves Right");
            }

            
            
            // If Enemy 2 is present in the scene
            if (enemy2 != "null")
            {
                // If Enemy 2 can move left
                if (CanEnemyMoveLeft(enemy2Position))
                {
                    availableMoves.Add("Enemy 2 Moves Left");
                }
            
                // If Enemy 2 can move right
                if (CanEnemyMoveRight(enemy2Position))
                {
                    availableMoves.Add("Enemy 2 Moves Right");
                }
            }


            
            // If Enemy 3 is present in the scene
            if (enemy3 != "null")
            {
                // If Enemy 3 can move left
                if (CanEnemyMoveLeft(enemy3Position))
                {
                    availableMoves.Add("Enemy 3 Moves Left");
                }
            
                // If Enemy 3 can move right
                if (CanEnemyMoveRight(enemy3Position))
                {
                    availableMoves.Add("Enemy 3 Moves Right");
                }
            }
        }
       
        if (attackNumber == 2)
        {
            // If Enemy 1 can move left
            if (CanEnemyMoveLeft(enemy1First))
            {
                availableMoves.Add("Enemy 1 Moves Left");
            }
            
            // If Enemy 1 can move right
            if (CanEnemyMoveRight(enemy1First))
            {
                availableMoves.Add("Enemy 1 Moves Right");
            }
            
            
            
            // If Enemy 2 is present in the scene
            if (enemy2 != "null")
            {
                // If Enemy 2 can move left
                if (CanEnemyMoveLeft(enemy2First))
                {
                    availableMoves.Add("Enemy 2 Moves Left");
                }
            
                // If Enemy 2 can move right
                if (CanEnemyMoveRight(enemy2First))
                {
                    availableMoves.Add("Enemy 2 Moves Right");
                }
            }


            
            // If Enemy 3 is present in the scene
            if (enemy3 != "null")
            {
                // If Enemy 3 can move left
                if (CanEnemyMoveLeft(enemy3First))
                {
                    availableMoves.Add("Enemy 3 Moves Left");
                }
            
                // If Enemy 3 can move right
                if (CanEnemyMoveRight(enemy3First))
                {
                    availableMoves.Add("Enemy 3 Moves Right");
                }
            }
        }
    }

    
    
    
    
    bool CanEnemyMoveLeft(int enemyPosition)
    {
        // If the enemy has not yet chosen the first move
        if (firstAttack == "null")
        {
            if (enemyPosition - 1 != enemy2Position)
            {
                if (enemyPosition - 1 != enemy1Position)
                {
                    if (enemyPosition - 1 != 5)
                    {
                        //print("Enemy can move left");
                        return true;
                    }
                }
            }
        }
        else
        {
            // If the enemy has not yet chosen the second move
            if (secondAttack == "null")
            {
                if (enemyPosition - 1 != enemy2First)
                {
                    if (enemyPosition - 1 != enemy1First)
                    {
                        if (enemyPosition - 1 != 5)
                        {
                            //print("Enemy can move left");
                            return true;
                        }
                    }
                }
            }
        }
        
        return false;
    }
    
    
    bool CanEnemyMoveRight(int enemyPosition)
    {
        // If the enemy has not yet chosen the first move
        if (firstAttack == "null")
        {
            if (enemyPosition + 1 != enemy2Position)
            {
                if (enemyPosition + 1 != enemy3Position)
                {
                    if (enemyPosition + 1 != 11)
                    {
                        //print("Enemy can move right");
                        return true;
                    }
                }
            }
        }
        else
        {
            // If the enemy has not yet chosen the second move
            if (secondAttack == "null")
            {
                if (enemyPosition + 1 != enemy2First)
                {
                    if (enemyPosition + 1 != enemy3First)
                    {
                        if (enemyPosition + 1 != 11)
                        {
                            //print("Enemy can move right");
                            return true;
                        }
                    }
                }
            }
        }
        
        return false;
    }



    
    

    void DetermineEnemyType(string enemyName)
    {
        switch (enemyName)
        {
            case "Pirate":
                availableMoves.Add("Pirate Slashes");
                availableMoves.Add("Pirate Throws Knife");
                break;
            
            
            
            case "Royal Knight Melee":
                availableMoves.Add("Royal Knight Swings Sword");
                availableMoves.Add("Royal Knight Blocks");
                break;
            
            case "Folkvar":
                availableMoves.Add("Folkvar Swings Sword");
                availableMoves.Add("Folkvar Smites");
                break;
            
            case "Royal Knight Ranged":
                availableMoves.Add("Royal Knight Shoots Arrow");
                availableMoves.Add("Royal Knight Heals Team");
                break;
            
            
            
            case "Skull Grunt Melee":
                availableMoves.Add("Skull Grunt Slashes");
                availableMoves.Add("Skull Grunt Blocks");
                break;
            
            case "Skull Grunt Ranged":
                availableMoves.Add("Skull Grunt Shoots Arrow");
                availableMoves.Add("Skull Grunt Throws Bomb");
                break;
            
            case "Kaz":
                // Stage 1
                availableMoves.Add("Kaz Shoots Arrow");
                // Stage 2
                availableMoves.Add("Kaz Swings Battle Axe");
                availableMoves.Add("Kaz Performs a Grand Slam");
                // Stage 3
                availableMoves.Add("Kaz Empowers both Teams");
                availableMoves.Add("Kaz Blocks");
                break;
            
            
            
            case "Tavern Brute":
                availableMoves.Add("Tavern Brute Smash");
                availableMoves.Add("Tavern Brute Throws Chair");
                break;
            
            case "Barkeeper":
                availableMoves.Add("Barkeeper Punches You");
                availableMoves.Add("Barkeeper Punches Tavern Brute");
                break;
            
            
            
            case "Gatekeeper":
                availableMoves.Add("Gatekeeper's Dog Barks");
                availableMoves.Add("Gatekeeper's Dog Bites");
                availableMoves.Add("Gatekeeper Blocks");
                break;
            
            
            
            case "Royal Guard 1":
                availableMoves.Add("Royal Guard 1 Swings Battle Axe");
                availableMoves.Add("Royal Guard 1 Performs a Grand Slam");
                availableMoves.Add("Royal Guard 1 Blocks");
                break;
            
            case "Royal Guard 2":
                availableMoves.Add("Royal Guard 2 Swings Battle Axe");
                availableMoves.Add("Royal Guard 2 Performs a Grand Slam");
                availableMoves.Add("Royal Guard 2 Blocks");
                break;
            
            
            
            case "Skull King":
                availableMoves.Add("Skull King Swings Mace");
                availableMoves.Add("Skull King Performs a Grand Slam");
                availableMoves.Add("Skull King Throws Bomb");
                break;
            
            case "Royal King":
                // Stage 1
                availableMoves.Add("Royal King Swings Mace");
                availableMoves.Add("Royal King Performs a Grand Slam");
                // Stage 2
                availableMoves.Add("Royal King Empowers his Team");
                availableMoves.Add("Royal King Heals his Team");
                availableMoves.Add("Royal King Casts a Shield");
                break;
        }
    }


    
    
    void UpdateVariables(int attackNumber)
    {
        // If the first attack has been decided
        if (attackNumber == 1)
        {
            // Enemy 1
            if (firstAttack == "Enemy 1 Moves Left") enemy1First = enemy1Position - 1;
            else if (firstAttack == "Enemy 1 Moves Right") enemy1First = enemy1Position + 1;
            else enemy1First = enemy1Position;
            
            // Enemy 2
            if (firstAttack == "Enemy 2 Moves Left") enemy2First = enemy2Position - 1;
            else if (firstAttack == "Enemy 2 Moves Right") enemy2First = enemy2Position + 1;
            else enemy2First = enemy2Position;
            
            // Enemy 3
            if (firstAttack == "Enemy 3 Moves Left") enemy3First = enemy3Position - 1;
            else if (firstAttack == "Enemy 3 Moves Right") enemy3First = enemy3Position + 1;
            else enemy3First = enemy3Position;
        } 
        
        // If the second attack has been decided
        if (attackNumber == 2)
        {
            // Enemy 1
            if (secondAttack == "Enemy 1 Moves Left") enemy1Second = enemy1First - 1;
            else if (secondAttack == "Enemy 1 Moves Right") enemy1Second = enemy1First + 1;
            else enemy1Second = enemy1First;
                
            // Enemy 2
            if (secondAttack == "Enemy 2 Moves Left") enemy2Second = enemy2First - 1;
            else if (secondAttack == "Enemy 2 Moves Right") enemy2Second = enemy2First + 1;
            else enemy2Second = enemy2First;
        
            // Enemy 3
            if (secondAttack == "Enemy 3 Moves Left") enemy3Second = enemy3First - 1;
            else if (secondAttack == "Enemy 3 Moves Right") enemy3Second = enemy3First + 1;
            else enemy3Second = enemy3First;
        }
    }
    
    
    
    
    public static void ClearMoves(int resetSceneType)
    {
        availableMoves.Clear();
        firstAttack = "null";
        secondAttack = "null";

        if (resetSceneType == 2)
        {
            enemy1Position = enemy1Second;
            enemy2Position = enemy2Second;
            enemy3Position = enemy3Second;
        }

        enemy1First = enemy1Position;
        enemy2First = enemy2Position;
        enemy3First = enemy3Position;
        
        enemy1Second = enemy1Position;
        enemy2Second = enemy2Position;
        enemy3Second = enemy3Position;
    }
    
    
    public static void ChangeEnemyLocation(int enemyPlace1, int enemyPlace2, int enemyPlace3)
    {
        if (!hasChangedScene)
        {
            enemy1Position = enemyPlace1;
            enemy2Position = enemyPlace2;
            enemy3Position = enemyPlace3;
        
            enemy1First = 0;
            enemy2First = 0;
            enemy3First = 0;
        
            enemy1Second = 0;
            enemy2Second = 0;
            enemy3Second = 0;

            hasChangedScene = true;
        }
    }
}
