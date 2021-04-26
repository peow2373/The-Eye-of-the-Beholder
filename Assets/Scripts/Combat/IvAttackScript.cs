using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvAttackScript : MonoBehaviour
{
    public static bool tempBlock = false;
    public static bool tempEmpower = false;
    
    public static bool blocking = false;
    public static bool empowered = false;

    public static float damageBoost = 1f;

    public static int playerTarget;

    public static bool isBlocking, isEmpowering, isCountering;

    public static bool attackedSinceEmpower = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If Iv is blocking
        if (blocking)
        {
            if (AttackScript.enemyAttack != tempBlock)
            {
                if (!AttackScript.countered)
                {
                    if (!isBlocking)
                    {
                        AttackScript.enemyDamageModifier = DamageValues.blockedDamage;
                        isBlocking = true;

                        AttackScript.playerBlocking = true;
                    }
                }
            }
            else
            {
                AttackScript.playerBlocking = false;
                blocking = false;
                
                if (!AttackScript.enemyBlocking && !AttackScript.enemyEmpowering)
                {
                    // Reset damage modifier
                    AttackScript.enemyDamageModifier = 1f;
                }
            }
        }
        else
        {
            isBlocking = false;
        }

        // If Iv empowered an ability
        if (!empowered)
        {
            if (AttackScript.playerAttack != tempEmpower)
            {
                if (attackedSinceEmpower)
                {
                    if (blocking)
                    {
                        if (!isCountering)
                        {
                            // Reflect attack back at enemy
                            AttackScript.enemyDamageModifier = 1f;
                            isCountering = true;
                        
                            AttackScript.countered = true;
                        }
                    }
                }
            }
            else
            {
                if (attackedSinceEmpower)
                {
                    AttackScript.countered = false;
                    AttackScript.playerEmpowering = false;

                    isCountering = false;

                    attackedSinceEmpower = false;

                    if (!AttackScript.enemyBlocking && !AttackScript.enemyEmpowering)
                    {
                        // Reset damage modifier
                        AttackScript.enemyDamageModifier = 1f;
                        AttackScript.damageModifier = 1f;
                    }
                }
            }

            isEmpowering = false;
        } 
        else 
        {
            if (AttackScript.playerAttack == tempEmpower)
            {
                if (!isEmpowering)
                {
                    AttackScript.enemyDamageModifier = damageBoost;
                    AttackScript.damageModifier = damageBoost;
                    
                    isEmpowering = true;
                    
                    AttackScript.playerEmpowering = true;
                }
            }
            else
            {
                attackedSinceEmpower = true;
                empowered = false;
            }
        }
    }

    
    public static void DoesIvAttack(int playerAttack, int attackNumber)
    {
        if (!CombatManagerScript.ivAlive || !CombatManagerScript.canIvAttack)
        {
            // Skip the attack
            if (attackNumber == 1) CombatSimulationScript.attack1Delay = DamageValues.standardDelay/2;
            else CombatSimulationScript.attack2Delay = DamageValues.standardDelay/2;
            return;
        }
        else
        {
            float original;
            float burnRate;
            
            
            // ATTACK 1
            // Iv's Block
            if (playerAttack == 7)
            {
                // TODO: Play Iv Block animation
                playerTarget = 0;
                
                AttackScript.delayRate = DamageValues.blockDelay;

                blocking = true;
                tempBlock = AttackScript.enemyAttack;
            }
            
            
            // ATTACK 2
            // Iv's Heal
            if (playerAttack == 8)
            {
                original = DamageValues.heal * AttackScript.damageModifier;
                burnRate = DamageValues.healBurn;
                int healValue = (int) original;
                AttackScript.delayRate = (healValue * burnRate) + DamageValues.standardDelay;

                int lowestHP = 500;
                int targetCharacter = 0;

                // Determine which character has the lowest HP;
                if (CombatManagerScript.netrixiAlive)
                    if (CombatManagerScript.netrixiHP < lowestHP)
                    {
                        targetCharacter = 1;
                        lowestHP = CombatManagerScript.netrixiHP; 
                    }
                if (CombatManagerScript.folkvarAlive)
                    if (CombatManagerScript.folkvarHP < lowestHP)
                    {
                        targetCharacter = 2;
                        lowestHP = CombatManagerScript.folkvarHP;
                    }
                if (CombatManagerScript.ivAlive)
                    if (CombatManagerScript.ivHP < lowestHP)
                    {
                        targetCharacter = 3;
                        lowestHP = CombatManagerScript.ivHP;
                    }


                switch (targetCharacter)
                {
                    // Netrixi
                    case 1:
                        // TODO: Play Iv Heal animation
                        playerTarget = 8;

                        HealthManagerScript.ChangeHealth("Netrixi", -healValue, burnRate);
                        print("Heal Netrixi");
                        break;
                    
                    // Folkvar
                    case 2:
                        // TODO: Play Iv Heal animation
                        playerTarget = 9;

                        HealthManagerScript.ChangeHealth("Folkvar", -healValue, burnRate);
                        print("Heal Folkvar");
                        break;
                    
                    // Iv
                    case 3:
                        // TODO: Play Iv Heal animation
                        playerTarget = 10;

                        HealthManagerScript.ChangeHealth("Iv", -healValue, burnRate);
                        print("Heal Iv");
                        break;
                }
            }


            // ATTACK 3
            // Iv's Empower
            if (playerAttack == 9)
            {
                // TODO: Play Iv Empower animation
                playerTarget = 11;
                
                AttackScript.delayRate = DamageValues.empowerDelay;
                
                empowered = true;
                tempEmpower = AttackScript.playerAttack;

                // Determine how much to increase the damage of the next attack by
                if (attackNumber == 1) DetermineDamageBoost(CombatManagerScript.target1Location);
                else DetermineDamageBoost(CombatManagerScript.target2Location);
            }
            
            if (attackNumber == 1) CombatSimulationScript.playerAttack1Target = playerTarget;
            else CombatSimulationScript.playerAttack2Target = playerTarget;
        }
    }
    
    
    
    public static void DetermineDamageBoost(int targetLocation)
    {
        switch (targetLocation)
        {
            case 1:
                damageBoost = DamageValues.empowered1Damage;
                break;
            
            case 2:
                damageBoost = DamageValues.empowered2Damage;
                break;
            
            case 3:
                damageBoost = DamageValues.empowered3Damage;
                break;
        }
    }


    public static int CalculateMissingHealth(int currHP, int maxHP, float maxGainedHP)
    {
        int diffHP = maxHP - currHP;
        int maxRecoveredHP = (int) maxGainedHP;

        
        
        // If the character is missing less health than the maximum that can be recovered
        if (diffHP < maxRecoveredHP) return diffHP;
        else return maxRecoveredHP;
    }
}
