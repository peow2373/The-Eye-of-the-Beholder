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

    public static bool canChangeBlock = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (blocking)
        {
            if (AttackScript.enemyAttack != tempBlock)
            {
                if (canChangeBlock) AttackScript.enemyDamageModifier = DamageValues.blockedDamage;
            }
            else
            {
                // Reset damage modifier
                AttackScript.enemyDamageModifier = 1f;
                blocking = false;
                canChangeBlock = true;
            }
        }

        if (!empowered)
        {
            if (AttackScript.playerAttack == tempEmpower)
            {
                if (!blocking)
                {
                    // Reset damage modifier
                    AttackScript.enemyDamageModifier = 1f;
                    AttackScript.damageModifier = 1f;
                }
            }
        } 
        else 
        {
            if (AttackScript.playerAttack == tempEmpower)
            {
                if (blocking)
                {
                    // Reflect attack back at enemy
                    AttackScript.enemyDamageModifier = 0f;
                    canChangeBlock = false;
                }
                else
                {
                    AttackScript.enemyDamageModifier = damageBoost;
                    AttackScript.damageModifier = damageBoost;
                }
            }
            else
            {
                empowered = false;
            }
        }
    }

    
    public static void DoesIvAttack(int playerAttack, int attackNumber)
    {
        if (!CombatManagerScript.ivAlive)
        {
            // Skip the attack
            if (attackNumber == 1) CombatSimulationScript.attack1Delay = DamageValues.standardDelay;
            else CombatSimulationScript.attack2Delay = DamageValues.standardDelay;
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
                AttackScript.delayRate = DamageValues.blockDelay;

                blocking = true;
                tempBlock = AttackScript.enemyAttack;
                
                // TODO: Play Iv block animation
            }
            
            
            // ATTACK 2
            // Iv's Heal
            if (playerAttack == 8)
            {
                original = DamageValues.heal * AttackScript.damageModifier;
                burnRate = DamageValues.healBurn;
                int damageValue = (int) original;
                
                int lowestHP = 500;
                int targetCharacter = 0;

                // Determine which character has the lowest HP;
                if (CombatManagerScript.netrixiAlive)
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
                        // If Netrixi is below her starting HP
                        if (CombatManagerScript.netrixiHP < HealthValues.netrixiHP)
                        {
                            // TODO: Play Iv heal animation
                            int temp = CalculateMissingHealth(CombatManagerScript.netrixiHP, HealthValues.netrixiHP, damageValue);
                            DamageValues.ChangeHealDelay(temp, 0);
                            
                            HealthManagerScript.ChangeHealth("Netrixi", -temp, burnRate);
                            print("Heal Netrixi");
                        }
                        else
                        {
                            print("Netrixi is already at full health!");
                            DamageValues.healDelay = DamageValues.standardDelay;
                        }
                        break;
                    
                    // Folkvar
                    case 2:
                        // If Folkvar is below his starting HP
                        if (CombatManagerScript.folkvarHP < HealthValues.folkvarHP)
                        {
                            // TODO: Play Iv heal animation
                            int temp = CalculateMissingHealth(CombatManagerScript.folkvarHP, HealthValues.folkvarHP, damageValue);
                            DamageValues.ChangeHealDelay(temp, 0);
                            
                            HealthManagerScript.ChangeHealth("Folkvar", -temp, burnRate);
                            print("Heal Folkvar");
                        }
                        else
                        {
                            print("Folkvar is already at full health!");
                            DamageValues.healDelay = DamageValues.standardDelay;
                        }
                        break;
                    
                    // Iv
                    case 3:
                        // If Iv is below her starting HP
                        if (CombatManagerScript.ivHP < HealthValues.ivHP)
                        {
                            // TODO: Play Iv heal animation
                            int temp = CalculateMissingHealth(CombatManagerScript.ivHP, HealthValues.ivHP, damageValue);
                            DamageValues.ChangeHealDelay(temp, 0);

                            HealthManagerScript.ChangeHealth("Iv", -temp, burnRate);
                            print("Heal Iv");
                        }
                        else
                        {
                            print("Iv is already at full health!");
                            DamageValues.healDelay = DamageValues.standardDelay;
                        }
                        break;
                }
                
                AttackScript.delayRate = DamageValues.healDelay;
            }


            // ATTACK 3
            // Iv's Empower
            if (playerAttack == 9)
            {
                AttackScript.delayRate = DamageValues.empowerDelay;
                
                empowered = true;
                tempEmpower = AttackScript.playerAttack;

                // Determine how much to increase the damage of the next attack by
                if (attackNumber == 1) DetermineDamageBoost(CombatManagerScript.target1Location);
                else DetermineDamageBoost(CombatManagerScript.target2Location);
                
                
                // TODO: Fix Empower-Counter interaction
                // TODO: Play Iv empower animation
            }
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
        int maxRecoveredHP = -(int) maxGainedHP;

        
        
        // If the character is missing less health than the maximum that can be recovered
        if (diffHP < maxRecoveredHP) return diffHP;
        else return maxRecoveredHP;
    }
}
