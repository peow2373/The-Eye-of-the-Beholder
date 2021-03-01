using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetrixiAttackScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DoesNetrixiAttack(int playerAttack, int attackNumber)
    {
        // Check if Netrixi is still alive
        if (!CombatManagerScript.netrixiAlive)
        {
            // Skip the attack
            if (attackNumber == 1) CombatSimulationScript.attack1Delay = 1f;
            else CombatSimulationScript.attack2Delay = 1f;
        }
        else
        {
            float original;
            float burnRate;
            
            
            // ATTACK 1
            // Netrixi's Fireball
            if (playerAttack == 1)
            {
                original = DamageValues.fireball * AttackScript.damageModifier;
                burnRate = DamageValues.fireballBurn;
                AttackScript.delayRate = DamageValues.fireballDelay;
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
            
            
            // ATTACK 2
            // Netrixi's Lightning
            if (playerAttack == 2)
            {
                original = DamageValues.lightning * AttackScript.damageModifier;
                burnRate = DamageValues.lightningBurn;
                AttackScript.delayRate = DamageValues.lightningDelay;
                int damageValue = (int) original;
                
                
                if (attackNumber == 1) DetermineEnemyPosition(CombatManagerScript.target1Location);
                else DetermineEnemyPosition(CombatManagerScript.target2Location);
                
                
                void DetermineEnemyPosition(int targetLocation)
                {
                    switch (targetLocation)
                    {
                        // If an enemy is on square 6
                        case 1:
                            if (EnemyManagerScript.enemy1Position == 6)
                            {
                                HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                            }
                            
                            if (EnemyManagerScript.enemy2Position == 6)
                            {
                                HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                            }
                            
                            if (EnemyManagerScript.enemy3Position == 6)
                            {
                                HealthManagerScript.ChangeHealth("Enemy 3", damageValue, burnRate);
                            }
                            break;
                        
                        // If an enemy is on square 7
                        case 2:
                            if (EnemyManagerScript.enemy1Position == 7)
                            {
                                HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                            }
                            
                            if (EnemyManagerScript.enemy2Position == 7)
                            {
                                HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                            }
                            
                            if (EnemyManagerScript.enemy3Position == 7)
                            {
                                HealthManagerScript.ChangeHealth("Enemy 3", damageValue, burnRate);
                            }
                            break;
                        
                        // If an enemy is on square 8
                        case 3:
                            if (EnemyManagerScript.enemy1Position == 8)
                            {
                                HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                            }
                            
                            if (EnemyManagerScript.enemy2Position == 8)
                            {
                                HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                            }
                            
                            if (EnemyManagerScript.enemy3Position == 8)
                            {
                                HealthManagerScript.ChangeHealth("Enemy 3", damageValue, burnRate);
                            }
                            break;
                        
                        // If an enemy is on square 9
                        case 4:
                            if (EnemyManagerScript.enemy1Position == 9)
                            {
                                HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                            }
                            
                            if (EnemyManagerScript.enemy2Position == 9)
                            {
                                HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                            }
                            
                            if (EnemyManagerScript.enemy3Position == 0)
                            {
                                HealthManagerScript.ChangeHealth("Enemy 3", damageValue, burnRate);
                            }
                            break;
                        
                        // If an enemy is on square 10
                        case 5:
                            if (EnemyManagerScript.enemy1Position == 10)
                            {
                                HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                            }
                            
                            if (EnemyManagerScript.enemy2Position == 10)
                            {
                                HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                            }
                            
                            if (EnemyManagerScript.enemy3Position == 10)
                            {
                                HealthManagerScript.ChangeHealth("Enemy 3", damageValue, burnRate);
                            }
                            break;
                    }
                }
            }
            

            // ATTACK 3
            // Netrixi's Transmutate
            if (playerAttack == 3)
            {
                AttackScript.delayRate = 1f;
                
                // TODO: Finish random probability list
            }
        }
    }
}
