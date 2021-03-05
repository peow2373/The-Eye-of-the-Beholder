using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetrixiAttackScript : MonoBehaviour
{
    public static bool enemy1Transformed, enemy2Transformed, enemy3Transformed;

    private static Sprite enemy1Sprite, enemy2Sprite, enemy3Sprite;
    
    private static string enemy1Transform, enemy2Transform, enemy3Transform;
    
    public Sprite cat, catRK;
    public Sprite dog, dogRK;
    public Sprite frog, frogRK;

    public static int startingRound1, startingRound2, startingRound3;

    private static SpriteRenderer sr1, sr2, sr3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject enemy1 = GameObject.FindGameObjectWithTag("Enemy 1");
        sr1 = enemy1.GetComponent<SpriteRenderer>();
        
        GameObject enemy2 = GameObject.FindGameObjectWithTag("Enemy 2");
        sr2 = enemy2.GetComponent<SpriteRenderer>();
        
        GameObject enemy3 = GameObject.FindGameObjectWithTag("Enemy 3");
        sr3 = enemy3.GetComponent<SpriteRenderer>();

        

        // Transform Enemy 1 into an animal
        if (enemy1Transformed)
        {
            sr1.sprite = DetermineAnimal(enemy1Transform, false);
            CombatManagerScript.canEnemy1Attack = false;
            
            // Reset transformation
            if (CombatManagerScript.roundNumber >= startingRound1 + DamageValues.roundsTransformed)
            {
                enemy1Transformed = false;
                
                // Play transformation animation
                sr1.sprite = enemy1Sprite;
                CombatManagerScript.canEnemy1Attack = true;
            }
        }
        
        // Transform Enemy 2 into an animal
        if (enemy2Transformed)
        {
            sr2.sprite = DetermineAnimal(enemy2Transform, false);
            CombatManagerScript.canEnemy2Attack = false;
            
            // Reset transformation
            if (CombatManagerScript.roundNumber >= startingRound2 + DamageValues.roundsTransformed)
            {
                enemy2Transformed = false;

                // Play transformation animation
                sr2.sprite = enemy2Sprite;
                CombatManagerScript.canEnemy2Attack = true;
            }
        }
        
        // Transform Enemy 3 into an animal
        if (enemy3Transformed)
        {
            sr3.sprite = DetermineAnimal(enemy3Transform, true);
            CombatManagerScript.canEnemy3Attack = false;
            
            // Reset transformation
            if (CombatManagerScript.roundNumber >= startingRound3 + DamageValues.roundsTransformed)
            {
                enemy3Transformed = false;
                
                // Play transformation animation
                sr3.sprite = enemy3Sprite;
                CombatManagerScript.canEnemy3Attack = true;
            }
        }
    }

    public static void DoesNetrixiAttack(int playerAttack, int attackNumber)
    {
        // Check if Netrixi is still alive
        if (!CombatManagerScript.netrixiAlive)
        {
            // Skip the attack
            if (attackNumber == 1) CombatSimulationScript.attack1Delay = DamageValues.standardDelay;
            else CombatSimulationScript.attack2Delay = DamageValues.standardDelay;
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
                int damageValue = (int) original;
                
                if (CombatManagerScript.enemy1Alive)
                {
                    if (CombatManagerScript.enemy2Alive && EnemyManagerScript.enemy2 != "null")
                    {
                        // If Enemies 1 + 2 + 3 are alive
                        if (CombatManagerScript.enemy3Alive && EnemyManagerScript.enemy3 != "null")
                        {
                            DamageValues.ChangeDelay(3);
                            
                            // TODO: Play Netrixi Fireball animation
                            HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
                            HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
                            HealthManagerScript.ChangeHealth("Enemy 3", damageValue, burnRate);
                        }
                        // If Enemies 1 + 2 are alive
                        else
                        {
                            DamageValues.ChangeDelay(2);
                            
                            int randomValue = UnityEngine.Random.Range(0,2);

                            if (randomValue <= 1)
                            {
                                // Launch 2 fireballs at Enemy 2
                                // TODO: Play Netrixi Fireball animation
                                HealthManagerScript.ChangeHealth("Enemy 1", damageValue * 1, burnRate);
                                HealthManagerScript.ChangeHealth("Enemy 2", damageValue * 2, burnRate);
                            }
                            else
                            {
                                // Launch 2 fireballs at Enemy 1
                                // TODO: Play Netrixi Fireball animation
                                HealthManagerScript.ChangeHealth("Enemy 1", damageValue * 2, burnRate);
                                HealthManagerScript.ChangeHealth("Enemy 2", damageValue * 1, burnRate);
                            }
                        }
                    }
                    else
                    {
                        // If Enemies 1 + 3 are alive
                        if (CombatManagerScript.enemy3Alive && EnemyManagerScript.enemy3 != "null")
                        {
                            DamageValues.ChangeDelay(2);
                            
                            int randomValue = UnityEngine.Random.Range(0,2);

                            if (randomValue <= 1)
                            {
                                // Launch 2 fireballs at Enemy 3
                                // TODO: Play Netrixi Fireball animation
                                HealthManagerScript.ChangeHealth("Enemy 1", damageValue * 1, burnRate);
                                HealthManagerScript.ChangeHealth("Enemy 3", damageValue * 2, burnRate);
                            }
                            else
                            {
                                // Launch 2 fireballs at Enemy 1
                                // TODO: Play Netrixi Fireball animation
                                HealthManagerScript.ChangeHealth("Enemy 1", damageValue * 2, burnRate);
                                HealthManagerScript.ChangeHealth("Enemy 3", damageValue * 1, burnRate);
                            }
                            
                        }
                        // If only Enemy 1 is alive
                        else
                        {
                            DamageValues.ChangeDelay(1);
                            
                            // TODO: Play Netrixi Fireball animation
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
                            DamageValues.ChangeDelay(2);
                            
                            int randomValue = UnityEngine.Random.Range(0,2);

                            if (randomValue <= 1)
                            {
                                // Launch 2 fireballs at Enemy 3
                                // TODO: Play Netrixi Fireball animation
                                HealthManagerScript.ChangeHealth("Enemy 2", damageValue * 1, burnRate);
                                HealthManagerScript.ChangeHealth("Enemy 3", damageValue * 2, burnRate);
                            }
                            else
                            {
                                // Launch 2 fireballs at Enemy 2
                                // TODO: Play Netrixi Fireball animation
                                HealthManagerScript.ChangeHealth("Enemy 2", damageValue * 2, burnRate);
                                HealthManagerScript.ChangeHealth("Enemy 3", damageValue * 1, burnRate);
                            }
                        }
                        // If only Enemy 2 is alive
                        else
                        {
                            DamageValues.ChangeDelay(1);
                            
                            // TODO: Play Netrixi Fireball animation
                            HealthManagerScript.ChangeHealth("Enemy 2", damageValue * 3, burnRate);
                        }
                    }
                    else
                    {
                        // If only Enemy 3 is alive
                        if (CombatManagerScript.enemy3Alive && EnemyManagerScript.enemy3 != "null")
                        {
                            DamageValues.ChangeDelay(1);
                            
                            // TODO: Play Netrixi Fireball animation
                            HealthManagerScript.ChangeHealth("Enemy 3", damageValue * 3, burnRate);
                        }
                    }
                }
                
                AttackScript.delayRate = DamageValues.fireballDelay;
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
                AttackScript.delayRate = DamageValues.transmutateDelay;
                
                List<String> enemiesToTransform = new List<string>();
                List<String> transformChoices = new List<string>();
                
                // Determine which enemies can be transformed
                if (CombatManagerScript.enemy1Alive && CombatManagerScript.canEnemy1Attack) enemiesToTransform.Add("Enemy 1");
                if (CombatManagerScript.enemy2Alive && CombatManagerScript.canEnemy2Attack) enemiesToTransform.Add("Enemy 2");
                if (CombatManagerScript.enemy3Alive && CombatManagerScript.canEnemy3Attack) enemiesToTransform.Add("Enemy 3");
                
                // Choose a random enemy to transform
                if (enemiesToTransform.Count >= 1)
                {
                    var randomIndex = UnityEngine.Random.Range(0,(enemiesToTransform.Count));
                    
                    // Determine whether the enemy is transformed or not
                    transformChoices.Add("Cat");
                    transformChoices.Add("Dog");
                    transformChoices.Add("Frog");
                    
                    // Creates the option for the transmutation spell to fail
                    for (int i = 0; i < DamageValues.choices - transformChoices.Count; i++)
                    {
                        transformChoices.Add("None");
                    }

                    // Choose a random creature for the enemy to transform into
                    var randomIndex1 = UnityEngine.Random.Range(0,(transformChoices.Count));
                    
                    
                
                    // If an animal was chosen for the enemy to transform into
                    if (transformChoices[randomIndex1] != "None")
                    {
                        TransformEnemy(enemiesToTransform[randomIndex], CombatManagerScript.roundNumber, transformChoices[randomIndex1]);
                    }
                    else
                    {
                        print("Spell cast failed: " + enemiesToTransform[randomIndex] + " was not transformed");
                    }
                }
                else
                {
                    print("There are no enemies to transform");
                }
            }
        }
    }


    private static void TransformEnemy(string enemyTransformed, int startRound, string transformAnimal)
    {
        print(enemyTransformed + " was transformed into a " + transformAnimal);

        // Enemy 1
        if (enemyTransformed == "Enemy 1")
        {
            enemy1Sprite = sr1.sprite;
            
            // Play transformation animation

            enemy1Transform = transformAnimal;
            enemy1Transformed = true;
            startingRound1 = startRound;
        }
        
        // Enemy 2
        if (enemyTransformed == "Enemy 2")
        {
            enemy2Sprite = sr2.sprite;
            
            // Play transformation animation

            enemy2Transform = transformAnimal;
            enemy2Transformed = true;
            startingRound2 = startRound;
        }
        
        // Enemy 3
        if (enemyTransformed == "Enemy 3")
        {
            enemy3Sprite = sr3.sprite;
            
            // Play transformation animation

            enemy3Transform = transformAnimal;
            enemy3Transformed = true;
            startingRound3 = startRound;
        }
    }


    Sprite DetermineAnimal(string transformAnimal, bool kingly)
    {
        // First or Second Enemy
        if (!kingly)
        {
            if (transformAnimal == "Cat") return cat;
            if (transformAnimal == "Dog") return dog;
            if (transformAnimal == "Frog") return frog;
        }
        else
        {
            // Third enemy
            if (EnemyManagerScript.enemy3 == "Royal King")
            {
                if (transformAnimal == "Cat") return catRK;
                if (transformAnimal == "Dog") return dogRK;
                if (transformAnimal == "Frog") return frogRK;
            }
            else
            {
                if (transformAnimal == "Cat") return cat;
                if (transformAnimal == "Dog") return dog;
                if (transformAnimal == "Frog") return frog;
            }
        }

        return frog;
    }
}
