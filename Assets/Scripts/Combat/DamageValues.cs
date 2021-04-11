using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageValues : MonoBehaviour
{
    // Change these values to change the damage of different abilities

    public static float standardDelay = 1f;
    public static float standardBurn = 0.01f;

    // MAIN CHARACTERS
    
    // Netrixi
    public static float fireball = 25;                    // How much damage does the attack do?
    public static float fireballBurn = 0.025f;            // How long does the attack take to do 1 HP in damage?
    public static float fireballDelay;                    // How long before the next attack is started?
    
    public static float lightning = 50;
    public static float lightningBurn = standardBurn;

    public static int roundsTransformed = 2;
    public static float transmutateDelay = standardDelay;
    public static int choices = 4;

    
    // Folkvar
    public static float swingSword = 60;
    public static int critChance = 12;                    // Determines the likelihood of a critical strike
    public static float swingSwordBurn = standardBurn;

    public static float smite = 25;
    public static float smiteBurn = 0.025f;

    public static float grandSlam = 85;
    public static float grandSlamBurn = standardBurn;


    // Iv
    public static float blockedDamage = 0.25f;            // What percent damage will the next attack deal?
    public static float blockDelay = standardDelay; 
    
    public static float empowered1Damage = 1.25f;        // How much is the damage of the next attack increased by?
    public static float empowered2Damage = 1.5f;         
    public static float empowered3Damage = 2f;           
    public static float empowerDelay = standardDelay;

    public static float heal = 50;
    public static float healBurn = 0.075f;



    // ENEMIES
    
    // Pirate
    public static float slash = 40;
    public static float slashBurn = standardBurn;

    public static float throwKnife = 35;
    public static float throwKnifeBurn = standardBurn;


    // Royal Knight Ranged
    public static float shootArrow = 35;
    public static float shootArrowBurn = standardBurn;

    public static float healTeam = 35;
    public static float healTeamBurn = 0.075f;
    public static int enemyCanHeal = 25;                // How much damage an enemy needs to have taken to be considered worthy to heal
    
    
    // Gatekeeper
    public static float dogBite = 75;
    public static float dogBiteBurn = standardBurn;
    
    public static float dogBark = 50;
    public static float dogBarkBurn = standardBurn;


    // Tavern Brute
    public static float smash = 80;
    public static float smashBurn = standardBurn;
    
    public static float throwChair = 50;
    public static float throwChairBurn = standardBurn;
    
    
    // Barkeeper
    public static float punch = 50;
    public static float punchBurn = standardBurn;
    
    
    // Skull Grunt Ranged
    public static float bomb = 25;
    public static float bombBurn = standardBurn;
    public static float roundsStunned = 2;
    
    
    // Royal Guards
    public static float swingAxe = 75;
    public static float swingAxeBurn = standardBurn;
    
    
    // Skull King
    public static float swingMace = 120;
    public static float swingMaceBurn = standardBurn;
    
    public static float slamWithMace = 00;
    public static float slamWithMaceBurn = standardBurn;



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
}
