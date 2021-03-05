using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageValues : MonoBehaviour
{
    // Change these values to change the damage of different abilities

    public static float standardDelay = 1f;

    // Netrixi
    public static float fireball = 15;                    // How much damage does the attack do?
    public static float fireballBurn = 0.075f;            // How long does the attack take to do 1 HP in damage?
    public static float fireballDelay;                    // How long before the next attack is started?
    
    public static float lightning = 35;
    public static float lightningBurn = 0f;
    public static float lightningDelay = 1f;

    public static int roundsTransformed = 2;
    public static float transmutateDelay = 1f;
    public static int choices = 4;

    
    // Folkvar
    public static float swingSword = 40;
    public static int critChance = 10;                    // Determines the likelihood of a critical strike
    public static float swingSwordBurn = 0.01f;
    public static float swingSwordDelay = 1f;

    public static float smite = 25;
    public static float executeThreshold = 0.25f;        // Enemies must be below 25% health to be executed
    public static float smiteBurn = 0f;
    public static float smiteDelay = 1f;

    public static float grandSlam = 40;
    public static float grandSlamBurn = 0.01f;
    public static float grandSlamDelay = 1f;
    
    
    // Iv
    public static float blockedDamage = 0.75f;           // How much is the damage of the next attack reduced by?
    public static float blockDelay = 1f; 
    
    public static float empowered1Damage = 1.25f;        // How much is the damage of the next attack increased by?
    public static float empowered2Damage = 1.5f;         
    public static float empowered3Damage = 2f;           
    public static float empowerDelay = 1f;

    public static float heal = -40;
    public static float healBurn = 0.075f;    
    public static float healDelay;   

    
    
    public static void ChangeDelay(int enemiesAlive)
    {
        // If there are 3 Enemies alive
        if (enemiesAlive == 3)
        {
            fireballDelay = (fireballBurn * (fireball * AttackScript.damageModifier) * 1) + standardDelay;
        }
        
        // If there are 2 Enemies alive
        if (enemiesAlive == 2)
        {
            fireballDelay = (fireballBurn * (fireball * AttackScript.damageModifier) * 2) + standardDelay;
        }
        
        // If there is only 1 Enemy alive
        if (enemiesAlive == 1)
        {
            fireballDelay = (fireballBurn * (fireball * AttackScript.damageModifier) * 3) + standardDelay;
        }
    }

    public static void ChangeHealDelay(int healingDone, int enemyNumber)
    {
        // Change the delay for Iv's heal
        if (enemyNumber == 0) healDelay = (healBurn * healingDone) + standardDelay;
    }
}
