using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolkvarAttackScript : MonoBehaviour
{
    public static int playerTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DoesFolkvarAttack(int playerAttack, int attackNumber)
    {
        // Check if Folkvar is still alive
        if (!CombatManagerScript.folkvarAlive || !CombatManagerScript.canFolkvarAttack)
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
            // Folkvar's Swing Sword
            if (playerAttack == 4)
            {
                original = DamageValues.swingSword * AttackScript.damageModifier;
                burnRate = DamageValues.swingSwordBurn;
                int damageValue = (int) original;

                // Chance of a Critical Strike
                int randomValue = UnityEngine.Random.Range(0, DamageValues.critChance);
                
                if (randomValue < 1) AttackScript.delayRate = (damageValue * burnRate * 2) + DamageValues.standardDelay;
                else AttackScript.delayRate = (damageValue * burnRate) + DamageValues.standardDelay;

                // Attack the first enemy
                if (CombatManagerScript.enemy1Alive)
                {
                    // TODO: Play Folkvar Swing Sword animation
                    playerTarget = 1;
                    
                    if (randomValue < 1)
                    {
                        // Critical strike
                        HealthManagerScript.ChangeHealth("Enemy 1", damageValue * 2, burnRate);

                        print("Critical strike!");
                    }
                    else
                    {
                        // Regular attack
                        HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                    }
                }
                else
                {
                    // Attack the second enemy
                    if (CombatManagerScript.enemy2Alive)
                    {
                        // TODO: Play Folkvar Swing Sword animation
                        playerTarget = 2;
                        
                        if (randomValue < 1)
                        {
                            // Critical strike
                            HealthManagerScript.ChangeHealth("Enemy 2", damageValue * 2, burnRate);
                            
                            print("Critical strike!");
                        }
                        else
                        {
                            // Regular attack
                            HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                        }
                    }
                    else
                    {
                        // Attack the third enemy
                        if (CombatManagerScript.enemy3Alive)
                        {
                            // TODO: Play Folkvar Swing Sword animation
                            playerTarget = 3;
                            
                            if (randomValue < 1)
                            {
                                // Critical strike
                                HealthManagerScript.ChangeHealth("Enemy 3", damageValue * 2, burnRate);
                                
                                print("Critical strike!");
                            }
                            else
                            {
                                // Regular attack
                                HealthManagerScript.ChangeHealth("Enemy 3", damageValue, burnRate);
                            }
                        }
                    }
                }
            }
            
            
            // ATTACK 2
            // Folkvar's Holy Smite
            if (playerAttack == 5)
            {
                original = DamageValues.smite * AttackScript.damageModifier;
                burnRate = DamageValues.smiteBurn;
                int damageValue = (int) original;
                AttackScript.delayRate = (damageValue * burnRate) + DamageValues.standardDelay;
                
                int targetEnemy = 0;

                // Determine which enemy is attacking next
                if (attackNumber == 1) targetEnemy = CombatSimulationScript.enemyAttacker1;
                else targetEnemy = CombatSimulationScript.enemyAttacker2;

                int targetLocation;
                if (attackNumber == 1) targetLocation = CombatManagerScript.folkvarTarget1Location;
                else targetLocation = CombatManagerScript.folkvarTarget2Location;

                switch (targetEnemy)
                {
                    // Enemy 1
                    case 1:
                        if (EnemyManagerScript.enemy1Position == targetLocation)
                        {
                            // TODO: Play Folkvar Holy Smite animation
                            playerTarget = 1;
                                
                            if (attackNumber == 1) CombatSimulationScript.canEnemyMove1 = false;
                            else CombatSimulationScript.canEnemyMove2 = false;
                            
                            // Enemy will no longer perform their next attack
                            HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                        }
                        break;
                    
                    // Enemy 2
                    case 2:
                        if (EnemyManagerScript.enemy2Position == targetLocation)
                        {
                            // TODO: Play Folkvar Holy Smite animation
                            playerTarget = 2;
                                
                            if (attackNumber == 1) CombatSimulationScript.canEnemyMove1 = false;
                            else CombatSimulationScript.canEnemyMove2 = false;
                            
                            // Enemy will no longer perform their next attack
                            HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                        }
                        break;
                    
                    // Enemy 3
                    case 3:
                        if (EnemyManagerScript.enemy3Position == targetLocation)
                        {
                            // TODO: Play Folkvar Holy Smite animation
                            playerTarget = 3;
                                
                            if (attackNumber == 1) CombatSimulationScript.canEnemyMove1 = false;
                            else CombatSimulationScript.canEnemyMove2 = false;
                            
                            // Enemy will no longer perform their next attack
                            HealthManagerScript.ChangeHealth("Enemy 3", damageValue, burnRate);
                        }
                        break;
                }
            }
            
            
            // ATTACK 3
            // Folkvar's Grand Slam
            if (playerAttack == 6)
            {
                original = DamageValues.grandSlam * AttackScript.damageModifier;
                burnRate = DamageValues.grandSlamBurn;
                int damageValue = (int) original;
                AttackScript.delayRate = (damageValue * burnRate) + DamageValues.standardDelay;

                // If Enemy 1 is alive
                if (CombatManagerScript.enemy1Alive)
                {
                    // If Enemy 2 is alive
                    if (CombatManagerScript.enemy2Alive)
                    {
                        // If Enemy 2 is close to Enemy 1
                        if (EnemyManagerScript.enemy1Position + 1 == EnemyManagerScript.enemy2Position)
                        {
                            // If Enemy 3 is alive
                            if (CombatManagerScript.enemy3Alive)
                            {
                                // If Enemy 3 is close to Enemies 1 + 2
                                if (EnemyManagerScript.enemy2Position + 1 == EnemyManagerScript.enemy3Position)
                                {
                                    int position = EnemyManagerScript.enemy2Position;
                                    int enemyPosition;
                                    if (attackNumber == 1) enemyPosition = CombatManagerScript.folkvarTarget1Location;
                                    else enemyPosition = CombatManagerScript.folkvarTarget2Location;

                                    if (position == enemyPosition)
                                    {
                                        // TODO: Play Folkvar Grand Slam animation
                                        playerTarget = 7;
                                        
                                        // Attack Enemy 2 and deal splash damage to Enemies 1 + 3
                                        HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                                        HealthManagerScript.ChangeHealth("Enemy 1", damageValue / 2, burnRate);
                                        HealthManagerScript.ChangeHealth("Enemy 3", damageValue / 2, burnRate);    
                                    }
                                }
                                else
                                {
                                    int position = EnemyManagerScript.enemy1Position;
                                    int enemyPosition;
                                    if (attackNumber == 1) enemyPosition = CombatManagerScript.folkvarTarget1Location;
                                    else enemyPosition = CombatManagerScript.folkvarTarget2Location;

                                    if (position == enemyPosition)
                                    {
                                        // TODO: Play Folkvar Grand Slam animation
                                        playerTarget = 4;
                                        
                                        // Attack Enemy 1 and deal splash damage to Enemy 2
                                        HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                                        HealthManagerScript.ChangeHealth("Enemy 2", damageValue / 2, burnRate);
                                    }
                                }
                            }
                            else
                            {
                                int position = EnemyManagerScript.enemy1Position;
                                int enemyPosition;
                                if (attackNumber == 1) enemyPosition = CombatManagerScript.folkvarTarget1Location;
                                else enemyPosition = CombatManagerScript.folkvarTarget2Location;

                                if (position == enemyPosition)
                                {
                                    // TODO: Play Folkvar Grand Slam animation
                                    playerTarget = 4;
                                    
                                    // Attack Enemy 1 and deal splash damage to Enemy 2
                                    HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                                    HealthManagerScript.ChangeHealth("Enemy 2", damageValue / 2, burnRate);      
                                }
                            }
                        }
                        else
                        {
                            // If Enemy 2 is close to Enemy 3
                            if (EnemyManagerScript.enemy2Position + 1 == EnemyManagerScript.enemy3Position)
                            {
                                int position = EnemyManagerScript.enemy2Position;
                                int enemyPosition;
                                if (attackNumber == 1) enemyPosition = CombatManagerScript.folkvarTarget1Location;
                                else enemyPosition = CombatManagerScript.folkvarTarget2Location;

                                if (position == enemyPosition)
                                {
                                    // TODO: Play Folkvar Grand Slam animation
                                    playerTarget = 5;

                                    // Attack Enemy 2 and deal splash damage to Enemy 3
                                    HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                                    HealthManagerScript.ChangeHealth("Enemy 3", damageValue / 2, burnRate);        
                                }
                            }
                            else
                            {
                                int position = EnemyManagerScript.enemy1Position;
                                int enemyPosition;
                                if (attackNumber == 1) enemyPosition = CombatManagerScript.folkvarTarget1Location;
                                else enemyPosition = CombatManagerScript.folkvarTarget2Location;

                                if (position == enemyPosition)
                                {
                                    // TODO: Play Folkvar Grand Slam animation
                                    playerTarget = 1;

                                    // Attack Enemy 1 and deal no splash damage to other enemies
                                    HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);       
                                }
                            }
                        }
                    }
                    else
                    {
                        // If Enemy 3 is alive
                        if (CombatManagerScript.enemy3Alive)
                        {
                            // If Enemy 1 is close to Enemy 3
                            if (EnemyManagerScript.enemy1Position + 1 == EnemyManagerScript.enemy3Position)
                            {
                                int position = EnemyManagerScript.enemy1Position;
                                int enemyPosition;
                                if (attackNumber == 1) enemyPosition = CombatManagerScript.folkvarTarget1Location;
                                else enemyPosition = CombatManagerScript.folkvarTarget2Location;

                                if (position == enemyPosition)
                                {
                                    // TODO: Play Folkvar Grand Slam animation
                                    playerTarget = 6;

                                    // Attack Enemy 1 and deal splash damage to Enemy 3
                                    HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                                    HealthManagerScript.ChangeHealth("Enemy 3", damageValue / 2, burnRate);       
                                }
                            }
                            else
                            {
                                int position = EnemyManagerScript.enemy1Position;
                                int enemyPosition;
                                if (attackNumber == 1) enemyPosition = CombatManagerScript.folkvarTarget1Location;
                                else enemyPosition = CombatManagerScript.folkvarTarget2Location;

                                if (position == enemyPosition)
                                {
                                    // TODO: Play Folkvar Grand Slam animation
                                    playerTarget = 1;

                                    // Attack Enemy 1 and deal no splash damage to other enemies
                                    HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);       
                                }
                            }
                        }
                        else
                        {
                            int position = EnemyManagerScript.enemy1Position;
                            int enemyPosition;
                            if (attackNumber == 1) enemyPosition = CombatManagerScript.folkvarTarget1Location;
                            else enemyPosition = CombatManagerScript.folkvarTarget2Location;

                            if (position == enemyPosition)
                            {
                                // TODO: Play Folkvar Grand Slam animation
                                playerTarget = 1;

                                // Attack Enemy 1 and deal no splash damage to other enemies
                                HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);            
                            }
                        }
                    }
                }
                
                // If Enemy 1 isn't alive
                else
                {
                    // If Enemy 2 is alive
                    if (CombatManagerScript.enemy2Alive)
                    {
                        // If Enemy 2 is close to Enemy 3
                        if (EnemyManagerScript.enemy2Position + 1 == EnemyManagerScript.enemy3Position)
                        {
                            int position = EnemyManagerScript.enemy2Position;
                            int enemyPosition;
                            if (attackNumber == 1) enemyPosition = CombatManagerScript.folkvarTarget1Location;
                            else enemyPosition = CombatManagerScript.folkvarTarget2Location;

                            if (position == enemyPosition)
                            {
                                // TODO: Play Folkvar Grand Slam animation
                                playerTarget = 5;

                                // Attack Enemy 2 and deal splash damage to Enemy 3
                                HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                                HealthManagerScript.ChangeHealth("Enemy 3", damageValue / 2, burnRate);          
                            }
                        }
                        else
                        {
                            int position = EnemyManagerScript.enemy2Position;
                            int enemyPosition;
                            if (attackNumber == 1) enemyPosition = CombatManagerScript.folkvarTarget1Location;
                            else enemyPosition = CombatManagerScript.folkvarTarget2Location;

                            if (position == enemyPosition)
                            {
                                // TODO: Play Folkvar Grand Slam animation
                                playerTarget = 2;

                                // Attack Enemy 2 and deal no splash damage to other enemies
                                HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                            }
                        }
                    }
                    else
                    {
                        int position = EnemyManagerScript.enemy3Position;
                        int enemyPosition;
                        if (attackNumber == 1) enemyPosition = CombatManagerScript.folkvarTarget1Location;
                        else enemyPosition = CombatManagerScript.folkvarTarget2Location;

                        if (position == enemyPosition)
                        {
                            // TODO: Play Folkvar Grand Slam animation
                            playerTarget = 3;

                            // Attack Enemy 3 and deal no splash damage to other enemies
                            HealthManagerScript.ChangeHealth("Enemy 3", damageValue, burnRate);
                        }
                    }
                }
            }
            
            if (attackNumber == 1) CombatSimulationScript.playerAttack1Target = playerTarget;
            else CombatSimulationScript.playerAttack2Target = playerTarget;
        }
    }
}
