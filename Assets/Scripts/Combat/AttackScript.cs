using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public static float damageModifier = 1f;
    
    public static void PlayerAttack(int playerAttack, int attackNumber)
    {
        // Check if Netrixi is still alive
        if (playerAttack == 1 || playerAttack == 2 || playerAttack == 3)
        {
            if (!CombatManagerScript.netrixiAlive)
            {
                if (attackNumber == 1) CombatSimulationScript.attack1Delay = 1f;
                else CombatSimulationScript.attack2Delay = 1f;
                return;
            }
        }
        
        // Check if Folkvar is still alive
        if (playerAttack == 4 || playerAttack == 5 || playerAttack == 6)
        {
            if (!CombatManagerScript.folkvarAlive)
            {
                if (attackNumber == 1) CombatSimulationScript.attack1Delay = 1f;
                else CombatSimulationScript.attack2Delay = 1f;
                return;
            }
        }
        
        // Check if Iv is still alive
        if (playerAttack == 7 || playerAttack == 8 || playerAttack == 9)
        {
            if (!CombatManagerScript.ivAlive)
            {
                if (attackNumber == 1) CombatSimulationScript.attack1Delay = 1f;
                else CombatSimulationScript.attack2Delay = 1f;
                return;
            }
        }
        
        
        float original;
        float burnRate;
        float delayRate = 1f;
        
        
        
        // Netrixi's Fireball attack
        if (playerAttack == 1)
        {
            original = DamageValues.fireball * damageModifier;
            burnRate = DamageValues.fireballBurn;
            delayRate = DamageValues.fireballDelay;
            int damageValue = (int) original;
            
            if (CombatManagerScript.enemy1Alive)
            {
                if (CombatManagerScript.enemy2Alive && EnemyManagerScript.enemy2 != "null")
                {
                    // If Enemies 1 + 2 + 3 are alive
                    if (CombatManagerScript.enemy3Alive && EnemyManagerScript.enemy3 != "null")
                    {
                        // TODO: Play Netrixi fireball animation
                        HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                        HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                        HealthManagerScript.ChangeHealth("Enemy 3", damageValue, burnRate);
                    }
                    // If Enemies 1 + 2 are alive
                    else
                    {
                        // TODO: Play Netrixi fireball animation
                        HealthManagerScript.ChangeHealth("Enemy 1", damageValue * 1, burnRate);
                        HealthManagerScript.ChangeHealth("Enemy 2", damageValue * 2, burnRate);
                    }
                }
                else
                {
                    // If Enemies 1 + 3 are alive
                    if (CombatManagerScript.enemy3Alive && EnemyManagerScript.enemy3 != "null")
                    {
                        // TODO: Play Netrixi fireball animation
                        HealthManagerScript.ChangeHealth("Enemy 1", damageValue * 1, burnRate);
                        HealthManagerScript.ChangeHealth("Enemy 3", damageValue * 2, burnRate);
                    }
                    // If only Enemy 1 is alive
                    else
                    {
                        // TODO: Play Netrixi fireball animation
                        HealthManagerScript.ChangeHealth("Enemy 1", damageValue * 3, burnRate);
                    }
                }
            }
            else
            {
                if (CombatManagerScript.enemy2Alive && EnemyManagerScript.enemy2 != "null")
                {
                    // If Enemies 2 + 3 are alive
                    if (CombatManagerScript.enemy3Alive && EnemyManagerScript.enemy3 != "null")
                    {
                        // TODO: Play Netrixi fireball animation
                        HealthManagerScript.ChangeHealth("Enemy 2", damageValue * 1, burnRate);
                        HealthManagerScript.ChangeHealth("Enemy 3", damageValue * 2, burnRate);
                    }
                    // If only Enemy 2 is alive
                    else
                    {
                        // TODO: Play Netrixi fireball animation
                        HealthManagerScript.ChangeHealth("Enemy 2", damageValue * 3, burnRate);
                    }
                }
                else
                {
                    // If only Enemy 3 is alive
                    if (CombatManagerScript.enemy3Alive && EnemyManagerScript.enemy3 != "null")
                    {
                        // TODO: Play Netrixi fireball animation
                        HealthManagerScript.ChangeHealth("Enemy 3", damageValue * 3, burnRate);
                    }
                }
            }
        }
        
        
        // Netrixi's Lightning attack
        if (playerAttack == 2)
        {
            original = DamageValues.lightning * damageModifier;
            burnRate = DamageValues.lightningBurn;
            delayRate = DamageValues.lightningDelay;
            int damageValue = (int) original;
            
            // TODO: Get target location data from CombatManagerScript
        }
        
        
        
        if (attackNumber == 1) CombatSimulationScript.attack1Delay = delayRate;
        else CombatSimulationScript.attack2Delay = delayRate;
    }



    public static void EnemyAttack(string enemyAttack, int attackNumber, int enemyNumber)
    {
        // TODO: Check if Enemy 1 is still alive
        


        float original;
        float burnRate;
        float delayRate = 1f;
        
        
        
        // TODO: Simulation of enemy attacks
        
        
        
        if (attackNumber == 1) CombatSimulationScript.attack1Delay = delayRate;
        else CombatSimulationScript.attack2Delay = delayRate;
    }
}
