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

    public static string enemyTarget;

    public static bool countered = false;
    public static bool playerBlocking = false, playerEmpowering = false;
    public static bool enemyBlocking = false, enemyEmpowering = false;

    public static float animationTime1, animationTime2;

    public static int chairsThrown = 0;
    
    public static void PlayerAttack(int playerAttack, int attackNumber)
    {
        // Check if Netrixi attacks
        if (playerAttack == 1 || playerAttack == 2 || playerAttack == 3)
        {
            NetrixiAttackScript.DoesNetrixiAttack( playerAttack, attackNumber);

            if (attackNumber == 1) CombatSimulationScript.playerAttacker1 = "Netrixi";
            else CombatSimulationScript.playerAttacker2 = "Netrixi";
        }

        // Check if Folkvar attacks
        if (playerAttack == 4 || playerAttack == 5 || playerAttack == 6)
        {
            FolkvarAttackScript.DoesFolkvarAttack( playerAttack, attackNumber);
            
            if (attackNumber == 1) CombatSimulationScript.playerAttacker1 = "Folkvar";
            else CombatSimulationScript.playerAttacker2 = "Folkvar";
        }

        // Check if Iv attacks
        if (playerAttack == 7 || playerAttack == 8 || playerAttack == 9)
        {
            IvAttackScript.DoesIvAttack( playerAttack, attackNumber);
            
            if (attackNumber == 1) CombatSimulationScript.playerAttacker1 = "Iv";
            else CombatSimulationScript.playerAttacker2 = "Iv";
        }

        // TODO: Determine how long the attack animation takes
        CalculatePlayerAnimation(playerAttack, attackNumber);
        
        if (attackNumber == 1) CombatSimulationScript.attack1Delay = animationTime1 + delayRate;
        else CombatSimulationScript.attack2Delay = animationTime2 + delayRate;
    }



    public static void EnemyAttack(string enemyAttack, int attackNumber, int enemyNumber)
    {
        float original;
        float burnRate;
        float delayRate = DamageValues.standardDelay/2;

        switch (enemyNumber)
        {
            // Check if Enemy 1 is still alive
            case 1:
                if (CombatManagerScript.enemy1Alive)
                {
                    if (CombatManagerScript.canEnemy1Attack)
                    {
                        if (attackNumber == 1) DetermineAttack(enemyAttack, 1);
                        else DetermineAttack(enemyAttack, 2);
                    }
                }
                break;
            
            // Check if Enemy 2 is still alive
            case 2:
                if (CombatManagerScript.enemy2Alive)
                {
                    if (CombatManagerScript.canEnemy2Attack)
                    {
                        if (attackNumber == 1) DetermineAttack(enemyAttack, 1);
                        else DetermineAttack(enemyAttack, 2);
                    }
                }
                break;
            
            // Check if Enemy 3 is still alive
            case 3:
                if (CombatManagerScript.enemy3Alive)
                {
                    if (CombatManagerScript.canEnemy3Attack)
                    {
                        if (attackNumber == 1) DetermineAttack(enemyAttack, 1);
                        else DetermineAttack(enemyAttack, 2);
                    }
                }
                break;
        }

        
        // TODO: Determine how long the attack animation takes
        CalculateEnemyAnimation(enemyAttack, attackNumber);

        if (attackNumber == 1)
        {
            CombatSimulationScript.attack1Delay = animationTime1 + delayRate;
            CombatSimulationScript.enemyAttack1Target = enemyTarget;
        }
        else
        {
            CombatSimulationScript.attack2Delay = animationTime2 + delayRate;
            CombatSimulationScript.enemyAttack2Target = enemyTarget;
        }
        
        
        
        void DetermineAttack(string attack, int number)
        {
            int damageValue;
            int temp;
            string attackingEnemy = "null";
            
            int attackLocation1;
            int attackLocation2;

            if (number == 1)
            {
                attackLocation1 = EnemyManagerScript.attack1Location;
                attackLocation2 = EnemyManagerScript.attack1Location2;
                
                temp = CombatSimulationScript.enemyAttacker1;
            }
            else
            {
                attackLocation1 = EnemyManagerScript.attack2Location;
                attackLocation2 = EnemyManagerScript.attack2Location2;
                
                temp = CombatSimulationScript.enemyAttacker2;
            }

            
            switch (temp)
            {
                case 1:
                    attackingEnemy = "Enemy 1";
                    break;
                
                case 2:
                    attackingEnemy = "Enemy 2";
                    break;
                
                case 3:
                    attackingEnemy = "Enemy 3";
                    break;
            }
            
            
            // Make attacks do damage
            switch (attack)
            {
                // Melee Attacks
                
                case "Slashes":
                    original = DamageValues.slash;
                    burnRate = DamageValues.slashBurn;
                    damageValue = (int) (original * enemyDamageModifier);
                    delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                    
                    EnemyAttackScript.MeleeAttack(damageValue, burnRate, attackingEnemy);
                    break;

                case "Swings Sword":
                    original = DamageValues.swingSword;
                    burnRate = DamageValues.swingSwordBurn;
                    damageValue = (int) (original * enemyDamageModifier);
                    delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                    
                    EnemyAttackScript.MeleeAttack(damageValue, burnRate, attackingEnemy);
                    break;

                case "Swings Battle Axe":
                    original = DamageValues.swingAxe;
                    burnRate = DamageValues.swingAxeBurn;
                    damageValue = (int) (original * enemyDamageModifier);
                    delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                    
                    EnemyAttackScript.MeleeAttack(damageValue, burnRate, attackingEnemy);
                    break;
                
                case "Swings Mace":
                    original = DamageValues.swingMace;
                    burnRate = DamageValues.swingMaceBurn;
                    damageValue = (int) (original * enemyDamageModifier);
                    delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                    
                    EnemyAttackScript.MeleeAttack(damageValue, burnRate, attackingEnemy);
                    break;
                
                case "Smashes":
                    original = DamageValues.smash;
                    burnRate = DamageValues.smashBurn;
                    damageValue = (int) (original * enemyDamageModifier);
                    
                    // Re-calculate delayRate if no characters are struck by the attack
                    if (CharacterManagerScript.netrixiPosition == attackLocation1 || CharacterManagerScript.folkvarPosition == attackLocation1 || CharacterManagerScript.ivPosition == attackLocation1)
                    {
                        delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                    }
                    else delayRate = DamageValues.standardDelay;
                    
                    EnemyAttackScript.GrandSlamAttack(damageValue, burnRate, number, attackingEnemy);
                    break;
                
                case "Performs a Grand Slam":
                    original = DamageValues.grandSlam;
                    burnRate = DamageValues.grandSlamBurn;
                    damageValue = (int) (original * enemyDamageModifier);
                    
                    // Re-calculate delayRate if no characters are struck by the attack
                    if (CharacterManagerScript.netrixiPosition == attackLocation1 || CharacterManagerScript.folkvarPosition == attackLocation1 || CharacterManagerScript.ivPosition == attackLocation1)
                    {
                        delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                    }
                    else delayRate = DamageValues.standardDelay;
                    
                    EnemyAttackScript.GrandSlamAttack(damageValue, burnRate, number, attackingEnemy);
                    break;
                
                case "Punches You":
                    original = DamageValues.punch;
                    burnRate = DamageValues.punchBurn;
                    damageValue = (int) (original * enemyDamageModifier);
                    delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                    
                    EnemyAttackScript.PunchAttack(damageValue, burnRate, attackingEnemy, false);
                    break;
                
                case "Punches Tavern Brute":
                    original = DamageValues.punch;
                    burnRate = DamageValues.punchBurn;
                    damageValue = (int) (original * enemyDamageModifier);
                    delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                    
                    EnemyAttackScript.PunchAttack(damageValue, burnRate, attackingEnemy, true);
                    break;
                
                case "Dog Barks":
                    original = DamageValues.dogBark;
                    burnRate = DamageValues.dogBarkBurn;
                    damageValue = (int) (original * enemyDamageModifier);
                    delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                    
                    EnemyAttackScript.DogAttack(damageValue, burnRate, number, attackingEnemy, false);
                    break;
                
                case "Dog Bites":
                    original = DamageValues.dogBite;
                    burnRate = DamageValues.dogBiteBurn;
                    damageValue = (int) (original * enemyDamageModifier);
                    delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                    
                    EnemyAttackScript.DogAttack(damageValue, burnRate, number, attackingEnemy, true);
                    break;
                
                case "Slams with Mace":
                    original = DamageValues.slamWithMace;
                    burnRate = DamageValues.slamWithMaceBurn;
                    damageValue = (int) (original * enemyDamageModifier);
                    
                    // Re-calculate delayRate depending on how many characters are struck by the attack
                    if (CharacterManagerScript.netrixiPosition == attackLocation1 || CharacterManagerScript.folkvarPosition == attackLocation1 || CharacterManagerScript.ivPosition == attackLocation1)
                    {
                        if (CharacterManagerScript.netrixiPosition == attackLocation2 || CharacterManagerScript.folkvarPosition == attackLocation2 || CharacterManagerScript.ivPosition == attackLocation2)
                        {
                            delayRate = ((damageValue * burnRate) * 2) + DamageValues.standardDelay;
                        }
                        else delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                    }
                    else
                    {
                        if (CharacterManagerScript.netrixiPosition == attackLocation2 || CharacterManagerScript.folkvarPosition == attackLocation2 || CharacterManagerScript.ivPosition == attackLocation2)
                        {
                            delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                        }
                        else delayRate = DamageValues.standardDelay;
                    }

                    EnemyAttackScript.SlamMaceAttack(damageValue, burnRate, number, attackingEnemy);
                    break;

                

                // Ranged Attacks
                
                case "Shoots Arrow":
                    original = DamageValues.shootArrow;
                    burnRate = DamageValues.shootArrowBurn;
                    damageValue = (int) (original * enemyDamageModifier);
                    
                    // Re-calculate delayRate if no characters are struck by the attack
                    if (CharacterManagerScript.netrixiPosition == attackLocation1 || CharacterManagerScript.folkvarPosition == attackLocation1 || CharacterManagerScript.ivPosition == attackLocation1)
                    {
                        delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                    }
                    else delayRate = DamageValues.standardDelay;
                
                    EnemyAttackScript.ThrowingAttack(damageValue, burnRate, number, attackingEnemy);
                    break;
                
                case "Throws Knife":
                    original = DamageValues.throwKnife;
                    burnRate = DamageValues.throwKnifeBurn;
                    damageValue = (int) (original * enemyDamageModifier);
                    
                    // Re-calculate delayRate if no characters are struck by the attack
                    if (CharacterManagerScript.netrixiPosition == attackLocation1 || CharacterManagerScript.folkvarPosition == attackLocation1 || CharacterManagerScript.ivPosition == attackLocation1)
                    {
                        delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                    }
                    else delayRate = DamageValues.standardDelay;

                    EnemyAttackScript.ThrowingAttack(damageValue, burnRate, number, attackingEnemy);
                    break;
                
                case "Throws Chair":
                    original = DamageValues.throwChair;
                    burnRate = DamageValues.throwChairBurn;
                    damageValue = (int) (original * enemyDamageModifier);
                    
                    // Re-calculate delayRate if no characters are struck by the attack
                    if (CharacterManagerScript.netrixiPosition == attackLocation1 || CharacterManagerScript.folkvarPosition == attackLocation1 || CharacterManagerScript.ivPosition == attackLocation1)
                    {
                        delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                    }
                    else delayRate = DamageValues.standardDelay;

                    chairsThrown++;
                    
                    EnemyAttackScript.ThrowingAttack(damageValue, burnRate, number, attackingEnemy);
                    break;

                case "Throws Bomb":
                    original = DamageValues.bomb;
                    burnRate = DamageValues.bombBurn;
                    damageValue = (int) (original * enemyDamageModifier);

                    // Re-calculate delayRate if no characters are struck by the attack
                    if (CharacterManagerScript.netrixiPosition == attackLocation1 || CharacterManagerScript.folkvarPosition == attackLocation1 || CharacterManagerScript.ivPosition == attackLocation1)
                    {
                        delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                    }
                    else delayRate = DamageValues.standardDelay;
                    
                    EnemyAttackScript.BombAttack(damageValue, burnRate, number, attackingEnemy);
                    break;



                // Other Attacks
                
                case "Smites":
                    original = DamageValues.smite;
                    burnRate = DamageValues.smiteBurn;
                    damageValue = (int) (original * enemyDamageModifier);

                    // Re-calculate delayRate if no characters are struck by the attack
                    if (CharacterManagerScript.netrixiPosition == attackLocation1 || CharacterManagerScript.folkvarPosition == attackLocation1 || CharacterManagerScript.ivPosition == attackLocation1)
                    {
                        delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                    }
                    else delayRate = DamageValues.standardDelay;

                    EnemyAttackScript.SmiteAttack(damageValue, burnRate, number, attackingEnemy);
                    break;
                
                case "Heals Team":
                    original = DamageValues.healTeam;
                    float healRate = DamageValues.healTeamBurn;
                    int healValue = (int) (original * enemyDamageModifier);
                    
                    int enemy1Heal = IvAttackScript.CalculateMissingHealth(CombatManagerScript.enemy1HP, CombatManagerScript.enemy1StartingHP, healValue);
                    int enemy2Heal = IvAttackScript.CalculateMissingHealth(CombatManagerScript.enemy2HP, CombatManagerScript.enemy2StartingHP, healValue);
                    int enemy3Heal = IvAttackScript.CalculateMissingHealth(CombatManagerScript.enemy3HP, CombatManagerScript.enemy3StartingHP, healValue);

                    int mostHealthRecovered = enemy1Heal;
                    if (enemy2Heal > mostHealthRecovered) mostHealthRecovered = enemy2Heal;
                    if (enemy3Heal > mostHealthRecovered) mostHealthRecovered = enemy3Heal;

                    delayRate = (mostHealthRecovered * healRate) + DamageValues.standardDelay;

                    EnemyAttackScript.HealTeam(enemy1Heal, enemy2Heal, enemy3Heal, healRate, attackingEnemy);
                    break;
                
                case "Blocks":
                    delayRate = 0f;

                    if (number == 1) EnemyAttackScript.BlockAttack(attackingEnemy, CombatManagerScript.firstAttack);
                    else EnemyAttackScript.BlockAttack(attackingEnemy, CombatManagerScript.secondAttack);
                    break;

                case "Empowers Both Teams":
                    delayRate = DamageValues.standardDelay;

                    EnemyAttackScript.EmpowerAttack(attackingEnemy);
                    break;
            }
        }
    }
    
    
    public static void CalculatePlayerAnimation(int playerAttack, int attackNumber)
    {
        //if (attackNumber == 1) print(CombatSimulationScript.playerAttack1Target);
        //else print(CombatSimulationScript.playerAttack2Target);
    }

    public static void CalculateEnemyAnimation(string enemyAttack, int attackNumber)
    {
        //print(enemyTarget);
    }
}
