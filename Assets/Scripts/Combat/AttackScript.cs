using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public static float damageModifier = 1f;
    public static float enemyDamageModifier = 1f;
    
    public static float delayRate = DamageValues.standardDelay;

    public static bool enemyAttack = true;
    public static bool playerAttack = true;
    
    public static void PlayerAttack(int playerAttack, int attackNumber)
    {
       // Check if Netrixi attacks
        if (playerAttack == 1 || playerAttack == 2 || playerAttack == 3)
        {
            NetrixiAttackScript.DoesNetrixiAttack( playerAttack, attackNumber);
        }
        
        // Check if Folkvar attacks
        if (playerAttack == 4 || playerAttack == 5 || playerAttack == 6)
        {
            FolkvarAttackScript.DoesFolkvarAttack( playerAttack, attackNumber);
        }
        
        // Check if Iv attacks
        if (playerAttack == 7 || playerAttack == 8 || playerAttack == 9)
        {
            IvAttackScript.DoesIvAttack( playerAttack, attackNumber);
        }
        
        
        if (attackNumber == 1) CombatSimulationScript.attack1Delay = delayRate;
        else CombatSimulationScript.attack2Delay = delayRate;
    }



    public static void EnemyAttack(string enemyAttack, int attackNumber, int enemyNumber)
    {
        // TODO: Check if Enemy 1 is still alive
        


        float original;
        float burnRate;
        float delayRate = DamageValues.standardDelay;
        //int damageValue = (int) (original * enemyDamageModifier);
        
        
        
        // TODO: Simulation of enemy attacks
        
        
        
        if (attackNumber == 1) CombatSimulationScript.attack1Delay = delayRate;
        else CombatSimulationScript.attack2Delay = delayRate;
        
        
        // Determines whether this is attack 1 or 2
    }
}
