using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CombatSimulationScript : MonoBehaviour
{
    private static float moveDelay = 1f;
    public static float attack1Delay, attack2Delay;

    public static bool didPlayerMove1 = false, didPlayerMove2 = false;
    public static bool didEnemyMove1 = false, didEnemyMove2 = false;

    public static int enemyAttacker1 = 0, enemyAttacker2 = 0;

    public IEnumerator RunSimulation(int playerFirstAttack, int playerSecondAttack, string enemyFirstAttack, string enemySecondAttack, GameObject runner)
    {
        CombatManagerScript.netrixiAttacks = false;
        CombatManagerScript.folkvarAttacks = false;
        CombatManagerScript.ivAttacks = false;
        
        
        // FIRST ATTACK
        yield return new WaitForSecondsRealtime(moveDelay);
        
        // Check to see if any Main Characters move for their first attack
        CombatSimulationScript.CheckForPlayerMovement(playerFirstAttack, 1);
        // If any Main Characters did move for their first attack
        if (didPlayerMove1) yield return new WaitForSecondsRealtime(moveDelay);

        // Check to see if any Enemy Characters move for their first attack
        CombatSimulationScript.CheckForEnemyMovement(enemyFirstAttack, 1);
        // If any Enemy Characters did move for their first attack
        if (didEnemyMove1) yield return new WaitForSecondsRealtime(moveDelay);
        
        
        
        // Check to see what the player's first attack is
        if (!didPlayerMove1)
        {
            AttackScript.PlayerAttack(playerFirstAttack, 1);
            CombatManagerScript.playerAttacking1 = true;
            // Wait for attack animation to play
            yield return new WaitForSecondsRealtime(attack1Delay);
        }
        
        // Check to see what the enemy's first attack is
        if (!didEnemyMove1)
        {
            AttackScript.EnemyAttack(enemyFirstAttack, 1, enemyAttacker1);
            CombatManagerScript.enemyAttacking1 = true;
            // Wait for attack animation to play
            yield return new WaitForSecondsRealtime(attack1Delay);
        }


        
        
        // SECOND ATTACK

        // Check to see if any Main Characters move for their second attack
        CombatSimulationScript.CheckForPlayerMovement(playerSecondAttack, 2);
        // If any Main Characters did move for their second attack
        if (didPlayerMove2) yield return new WaitForSecondsRealtime(moveDelay);

        // Check to see if any Enemy Characters move for their second attack
        CombatSimulationScript.CheckForEnemyMovement(enemySecondAttack, 2);
        // If any Enemy Characters did move for their second attack
        if (didEnemyMove2) yield return new WaitForSecondsRealtime(moveDelay);
        
        
        
        // Check to see what the player's second attack is
        if (!didPlayerMove2)
        {
            AttackScript.PlayerAttack(playerSecondAttack, 2);
            CombatManagerScript.playerAttacking2 = true;
            // Wait for attack animation to play
            yield return new WaitForSecondsRealtime(attack2Delay);
        }

        // Check to see what the enemy's second attack is
        if (!didEnemyMove2)
        {
            AttackScript.EnemyAttack(enemySecondAttack, 2, enemyAttacker2);
            CombatManagerScript.enemyAttacking2 = true;
            // Wait for attack animation to play
            yield return new WaitForSecondsRealtime(attack2Delay);
        }


        
        

        // Reset variables once simulation is finished
        CombatManagerScript.firstAttack = 0;
        CombatManagerScript.secondAttack = 0;

        NetrixiCombatScript.ResetNetrixiVariables();
        FolkvarCombatScript.ResetFolkvarVariables();
        IvCombatScript.ResetIvVariables();
                
        CombatManagerScript.roundNumber += 1;

        CombatManagerScript.playerAttacking1 = false;
        CombatManagerScript.playerAttacking2 = false;
        CombatManagerScript.enemyAttacking1 = false;
        CombatManagerScript.enemyAttacking2 = false;

        CombatManagerScript.target1Location = 0;
        CombatManagerScript.target2Location = 0;

        CharacterManagerScript.ResetVariables();
        EnemyManagerScript.ClearMoves();

        didPlayerMove1 = false;
        didPlayerMove2 = false;
        didEnemyMove1 = false;
        didEnemyMove2 = false;

        Destroy(runner);
    }




    public static void CheckForPlayerMovement(int playerAttack, int attackNumber)
    {
        // Main Characters
        if (playerAttack == 10 || playerAttack == 11)
        {
            if (CombatManagerScript.netrixiAlive)
            {
                if (attackNumber == 1)
                {
                    CharacterManagerScript.netrixiPosition = CharacterManagerScript.netrixi1st;
                    CombatManagerScript.playerAttacking1 = true;
                    didPlayerMove1 = true;
                }
                else
                {
                    CharacterManagerScript.netrixiPosition = CharacterManagerScript.netrixi2nd;
                    CombatManagerScript.playerAttacking2 = true;
                    didPlayerMove2 = true;
                }
            }
        }
        
        if (playerAttack == 12 || playerAttack == 13)
        {
            if (CombatManagerScript.folkvarAlive)
            {
                if (attackNumber == 1)
                {
                    CharacterManagerScript.folkvarPosition = CharacterManagerScript.folkvar1st;
                    CombatManagerScript.playerAttacking1 = true;
                    didPlayerMove1 = true;
                }
                else
                {
                    CharacterManagerScript.folkvarPosition = CharacterManagerScript.folkvar2nd;
                    CombatManagerScript.playerAttacking2 = true;
                    didPlayerMove2 = true;
                }
            }
        }
        
        if (playerAttack == 14 || playerAttack == 15)
        {
            if (CombatManagerScript.ivAlive)
            {
                if (attackNumber == 1)
                {
                    CharacterManagerScript.ivPosition = CharacterManagerScript.iv1st;
                    CombatManagerScript.playerAttacking1 = true;
                    didPlayerMove1 = true;
                }
                else
                {
                    CharacterManagerScript.ivPosition = CharacterManagerScript.iv2nd;
                    CombatManagerScript.playerAttacking2 = true;
                    didPlayerMove2 = true;
                }
            }
        }
    }


    public static void CheckForEnemyMovement(string enemyAttack, int attackNumber)
    {
        // Enemy Characters
        if (enemyAttack == "Enemy 1 Moves Left" || enemyAttack == "Enemy 1 Moves Right")
        {
            if (CombatManagerScript.enemy1Alive)
            {
                if (attackNumber == 1)
                {
                    EnemyManagerScript.enemy1Position = EnemyManagerScript.enemy1First;
                    CombatManagerScript.enemyAttacking1 = true;
                    didEnemyMove1 = true;
                }
                else
                {
                    EnemyManagerScript.enemy1Position = EnemyManagerScript.enemy1Second;
                    CombatManagerScript.enemyAttacking2 = true;
                    didEnemyMove2 = true;
                }
            }
        }
        
        if (enemyAttack == "Enemy 2 Moves Left" || enemyAttack == "Enemy 2 Moves Right")
        {
            if (CombatManagerScript.enemy2Alive)
            {
                if (attackNumber == 1)
                {
                    EnemyManagerScript.enemy2Position = EnemyManagerScript.enemy2First;
                    CombatManagerScript.enemyAttacking1 = true;
                    didEnemyMove1 = true;
                }
                else
                {
                    EnemyManagerScript.enemy2Position = EnemyManagerScript.enemy2Second;
                    CombatManagerScript.enemyAttacking2 = true;
                    didEnemyMove2 = true;
                }
            }
        }
        
        if (enemyAttack == "Enemy 3 Moves Left" || enemyAttack == "Enemy 3 Moves Right")
        {
            if (CombatManagerScript.enemy3Alive)
            {
                if (attackNumber == 1)
                {
                    EnemyManagerScript.enemy3Position = EnemyManagerScript.enemy3First;
                    CombatManagerScript.enemyAttacking1 = true;
                    didEnemyMove1 = true;
                }
                else
                {
                    EnemyManagerScript.enemy3Position = EnemyManagerScript.enemy3Second;
                    CombatManagerScript.enemyAttacking2 = true;
                    didEnemyMove2 = true;
                }
            }
        }
    }



    public static void DetermineEnemy()
    {
        // TODO: Determine enemy name from attack1 and attack2
    }
}
