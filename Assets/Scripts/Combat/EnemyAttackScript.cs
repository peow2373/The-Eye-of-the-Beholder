using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public static bool blocking = false;
    public static bool empowered = false;

    public static bool tempBlock = false;
    public static bool tempEmpower = false;

    public static bool isBlocking = false;
    public static bool isEmpowering = false;

    public static bool empoweringBothTeams = false;

    public static bool netrixiStunned, folkvarStunned, ivStunned;
    public static bool enemy1Stunned, enemy2Stunned, enemy3Stunned;

    public static int startOfTransformation;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If an Enemy is blocking the next attack
        if (blocking)
        {
            if (AttackScript.enemyAttack == tempBlock)
            {
                if (!isBlocking)
                {
                    AttackScript.damageModifier = DamageValues.blockedDamage;
                    isBlocking = true;
                    
                    print("Enemy is blocking");
                    
                    AttackScript.enemyBlocking = true;
                }
            }
            else
            {
                AttackScript.enemyBlocking = false;
                blocking = false;
                
                if (!AttackScript.countered && !AttackScript.playerBlocking && !AttackScript.playerEmpowering)
                {
                    // Reset damage modifier
                    AttackScript.damageModifier = 1f;

                    //print("Enemy is no longer blocking");
                }
            }
        }
        else
        {
            isBlocking = false;
        }
        
        
        
        // If an Enemy is empowering the next attack for both teams
        if (empowered)
        {
            if (AttackScript.playerAttack == tempEmpower)
            {
                if (!isEmpowering)
                {
                    AttackScript.enemyDamageModifier = DamageValues.empowered3Damage;
                    if (empoweringBothTeams) AttackScript.damageModifier = DamageValues.empowered3Damage;
                    
                    isEmpowering = true;
                    
                    print("Enemy is empowering");

                    AttackScript.enemyEmpowering = true;
                }
            }
            else
            {
                empowered = false;
            }
        } 
        else 
        {
            if (AttackScript.playerAttack == tempEmpower)
            {
                AttackScript.enemyEmpowering = false;
                
                if (!AttackScript.countered && !AttackScript.playerBlocking && !AttackScript.playerEmpowering)
                {
                    // Reset damage modifier
                    AttackScript.enemyDamageModifier = 1f;
                    if (empoweringBothTeams) AttackScript.damageModifier = 1f;
                    
                    //print("Enemy is no longer empowering");
                }
            }
            
            isEmpowering = false;
        }
        
        
        // BOMB ATTACK
        
        // Stun Netrixi if they are hit with a bomb
        if (netrixiStunned)
        {
            CombatManagerScript.canNetrixiAttack = false;
            
            // Reset transformation
            if (CombatManagerScript.roundNumber >= startOfTransformation + DamageValues.roundsStunned)
            {
                netrixiStunned = false;
                
                // TODO: Play transformation animation
                CombatManagerScript.canNetrixiAttack = true;
            }
        }

        // Stun Folkvar if they are hit with a bomb
        if (folkvarStunned)
        {
            CombatManagerScript.canFolkvarAttack = false;
            
            // Reset transformation
            if (CombatManagerScript.roundNumber >= startOfTransformation + DamageValues.roundsStunned)
            {
                folkvarStunned = false;
                
                // TODO: Play transformation animation
                CombatManagerScript.canFolkvarAttack = true;
            }
        }
        
        // Stun Iv if they are hit with a bomb
        if (ivStunned)
        {
            CombatManagerScript.canIvAttack = false;
            
            // Reset transformation
            if (CombatManagerScript.roundNumber >= startOfTransformation + DamageValues.roundsStunned)
            {
                ivStunned = false;
                
                // TODO: Play transformation animation
                CombatManagerScript.canIvAttack = true;
            }
        }
        
        // Stun Enemy 1 if they are hit with a bomb
        if (enemy1Stunned)
        {
            CombatManagerScript.canEnemy1Attack = false;
            
            // Reset transformation
            if (CombatManagerScript.roundNumber >= startOfTransformation + DamageValues.roundsStunned)
            {
                enemy1Stunned = false;
                
                // TODO: Play transformation animation
                CombatManagerScript.canEnemy1Attack = true;
            }
        }
        
        // Stun Enemy 2 if they are hit with a bomb
        if (enemy2Stunned)
        {
            CombatManagerScript.canEnemy2Attack = false;
            
            // Reset transformation
            if (CombatManagerScript.roundNumber >= startOfTransformation + DamageValues.roundsStunned)
            {
                enemy2Stunned = false;
                
                // TODO: Play transformation animation
                CombatManagerScript.canEnemy2Attack = true;
            }
        }
        
        // Stun Enemy 3 if they are hit with a bomb
        if (enemy3Stunned)
        {
            CombatManagerScript.canEnemy3Attack = false;
            
            // Reset transformation
            if (CombatManagerScript.roundNumber >= startOfTransformation + DamageValues.roundsStunned)
            {
                enemy3Stunned = false;
                
                // TODO: Play transformation animation
                CombatManagerScript.canEnemy3Attack = true;
            }
        }
    }
    
    
    
    public static void MeleeAttack(int damageValue, float burnRate, string attackingEnemy)
    {
        // If Folkvar is still alive
        if (CombatManagerScript.folkvarAlive)
        {
            // TODO: Play Melee Attack animation
            AttackScript.enemyTarget = "Folkvar";

            // If Iv is countering this ability
            if (AttackScript.countered) 
                HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
            else 
                HealthManagerScript.ChangeHealth("Folkvar", damageValue, burnRate);

        }
        else
        {
            // If Netrixi is still alive
            if (CombatManagerScript.netrixiAlive)
            {
                // TODO: Play Melee Attack animation
                AttackScript.enemyTarget = "Netrixi";
            
                // If Iv is countering this ability
                if (AttackScript.countered) 
                    HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                else
                    HealthManagerScript.ChangeHealth("Netrixi", damageValue, burnRate);
            }
            else
            {
                // If Iv is still alive
                if (CombatManagerScript.ivAlive)
                {
                    // TODO: Play Melee Attack animation
                    AttackScript.enemyTarget = "Iv";
            
                    // If Iv is countering this ability
                    if (AttackScript.countered) 
                        HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                    else
                        HealthManagerScript.ChangeHealth("Iv", damageValue, burnRate);
                }
            }
        }
    }
    
    
    public static void PunchAttack(int damageValue, float burnRate, string attackingEnemy, bool attackBrute)
    {
        // If the Barkeeper is attacking the Tavern Brute
        if (attackBrute)
        {
            // TODO: Play Punch Attack animation
            AttackScript.enemyTarget = "Brute";
            
            HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
        }
        
        // If the Barkeeper is attacking you
        else
        {
            // If Folkvar is still alive
            if (CombatManagerScript.folkvarAlive)
            {
                // TODO: Play Punch Attack animation
                AttackScript.enemyTarget = "Folkvar";

                // If Iv is countering this ability
                if (AttackScript.countered) 
                    HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                else 
                    HealthManagerScript.ChangeHealth("Folkvar", damageValue, burnRate);

            }
            else
            {
                // If Netrixi is still alive
                if (CombatManagerScript.netrixiAlive)
                {
                    // TODO: Play Punch Attack animation
                    AttackScript.enemyTarget = "Netrixi";
            
                    // If Iv is countering this ability
                    if (AttackScript.countered) 
                        HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                    else
                        HealthManagerScript.ChangeHealth("Netrixi", damageValue, burnRate);
                }
            }
        }
    }
    
    
    public static void ThrowingAttack(int damageValue, float burnRate, int attackNumber, string attackingEnemy)
    {
        // Editor's Note to Self: This is also the function used in the "Smashes" attack 
        
        // If Iv is still alive
        if (CombatManagerScript.ivAlive)
        {
            if (attackNumber == 1)
            {
                if (CharacterManagerScript.ivPosition == EnemyManagerScript.attack1Location)
                {
                    // TODO: Play Throw Projectile animation
                    AttackScript.enemyTarget = "Iv";
            
                    // If Iv is countering this ability
                    if (AttackScript.countered) 
                        HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                    else 
                        HealthManagerScript.ChangeHealth("Iv", damageValue, burnRate);
                }
            }
            else
            {
                if (CharacterManagerScript.ivPosition == EnemyManagerScript.attack2Location)
                {
                    // TODO: Play Throw Projectile animation
                    AttackScript.enemyTarget = "Iv";
            
                    // If Iv is countering this ability
                    if (AttackScript.countered) 
                        HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                    else 
                        HealthManagerScript.ChangeHealth("Iv", damageValue, burnRate);
                }
            }
        }
            
        // If Netrixi is still alive
        if (CombatManagerScript.netrixiAlive)
        {
            if (attackNumber == 1)
            {
                if (CharacterManagerScript.netrixiPosition == EnemyManagerScript.attack1Location)
                {
                    // TODO: Play Throw Projectile animation
                    AttackScript.enemyTarget = "Netrixi";
            
                    // If Iv is countering this ability
                    if (AttackScript.countered) 
                        HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                    else 
                        HealthManagerScript.ChangeHealth("Netrixi", damageValue, burnRate);
                }
            }
            else
            {
                if (CharacterManagerScript.netrixiPosition == EnemyManagerScript.attack2Location)
                {
                    // TODO: Play Throw Projectile animation
                    AttackScript.enemyTarget = "Netrixi";
            
                    // If Iv is countering this ability
                    if (AttackScript.countered) 
                        HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                    else 
                        HealthManagerScript.ChangeHealth("Netrixi", damageValue, burnRate);
                }
            }
        }
            
        // If Folkvar is still alive
        if (CombatManagerScript.folkvarAlive)
        {
            if (attackNumber == 1)
            {
                if (CharacterManagerScript.folkvarPosition == EnemyManagerScript.attack1Location)
                {
                    // TODO: Play Throw Projectile animation
                    AttackScript.enemyTarget = "Folkvar";
            
                    // If Iv is countering this ability
                    if (AttackScript.countered) 
                        HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                    else 
                        HealthManagerScript.ChangeHealth("Folkvar", damageValue, burnRate);
                }   
            }
            else
            {
                if (CharacterManagerScript.folkvarPosition == EnemyManagerScript.attack2Location)
                {
                    // TODO: Play Throw Projectile animation
                    AttackScript.enemyTarget = "Folkvar";
            
                    // If Iv is countering this ability
                    if (AttackScript.countered) 
                        HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                    else 
                        HealthManagerScript.ChangeHealth("Folkvar", damageValue, burnRate);
                } 
                else
                {
                    // TODO: Play Throw Projectile animation
                    AttackScript.enemyTarget = "None";
                }
            }
        }
    }


    public static void RangedAttack(int damageValue, float burnRate)
    {
        // TODO: Play Launch Projectile animation
        AttackScript.enemyTarget = "All";

        // If Iv is countering this ability
        if (AttackScript.countered)
        {
            // Hit all Enemies with the projectile
            if (CombatManagerScript.enemy1Alive) HealthManagerScript.ChangeHealth("Enemy 1", damageValue, burnRate);
            if (CombatManagerScript.enemy1Alive) HealthManagerScript.ChangeHealth("Enemy 2", damageValue, burnRate);
            if (CombatManagerScript.enemy1Alive) HealthManagerScript.ChangeHealth("Enemy 3", damageValue, burnRate);
        }
        else
        {
            // Hit all Main Characters with the projectile
            if (CombatManagerScript.folkvarAlive) HealthManagerScript.ChangeHealth("Folkvar", damageValue, burnRate);
            if (CombatManagerScript.netrixiAlive) HealthManagerScript.ChangeHealth("Netrixi", damageValue, burnRate);
            if (CombatManagerScript.ivAlive) HealthManagerScript.ChangeHealth("Iv", damageValue, burnRate);
        }
    }


    public static void SmiteAttack(int damageValue, float burnRate, int attackNumber, string attackingEnemy)
    {
        DetermineCharactersHit(attackNumber);


        void DetermineCharactersHit(int attackNumber)
        {
            int attackLocation;

            if (attackNumber == 1) attackLocation = EnemyManagerScript.attack1Location;
            else attackLocation = EnemyManagerScript.attack2Location;

            // If Folkvar is struck by the smite
            if (CharacterManagerScript.folkvarPosition == attackLocation)
            {
                // TODO: Play Smite animation
                AttackScript.enemyTarget = "Folkvar";
            
                // If Iv is countering this ability
                if (AttackScript.countered)
                {
                    if (DetermineEnemyHP())
                    {
                        // Execute low-health Enemy for double damage
                        HealthManagerScript.ChangeHealth(attackingEnemy, damageValue * 2, burnRate);
                    }
                    else HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                }
                else
                {
                    if (CombatManagerScript.folkvarHP <= HealthValues.folkvarHP * DamageValues.executeThreshold)
                    {
                        // Execute low-health Character for double damage
                        HealthManagerScript.ChangeHealth("Folkvar", damageValue * 2, burnRate);
                    }
                    else HealthManagerScript.ChangeHealth("Folkvar", damageValue, burnRate);
                }
            }
            else
            {
                // If Netrixi is struck by the smite
                if (CharacterManagerScript.netrixiPosition == attackLocation)
                {
                    // TODO: Play Smite animation
                    AttackScript.enemyTarget = "Netrixi";
            
                    // If Iv is countering this ability
                    if (AttackScript.countered)
                    {
                        if (DetermineEnemyHP())
                        {
                            // Execute low-health Enemy for double damage
                            HealthManagerScript.ChangeHealth(attackingEnemy, damageValue * 2, burnRate);
                        }
                        else HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                    }
                    else
                    {
                        if (CombatManagerScript.netrixiHP <= HealthValues.netrixiHP * DamageValues.executeThreshold)
                        {
                            // Execute low-health Character for double damage
                            HealthManagerScript.ChangeHealth("Netrixi", damageValue * 2, burnRate);
                        }
                        else HealthManagerScript.ChangeHealth("Netrixi", damageValue, burnRate);
                    }
                        
                }
                else
                {
                    // If Iv is struck by the smite
                    if (CharacterManagerScript.ivPosition == attackLocation)
                    {
                        // TODO: Play Smite animation
                        AttackScript.enemyTarget = "Iv";
            
                        // If Iv is countering this ability
                        if (AttackScript.countered)
                        {
                            if (DetermineEnemyHP())
                            {
                                // Execute low-health Enemy for double damage
                                HealthManagerScript.ChangeHealth(attackingEnemy, damageValue * 2, burnRate);
                            }
                            else HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                        }
                        else
                        {
                            if (CombatManagerScript.ivHP <= HealthValues.ivHP * DamageValues.executeThreshold)
                            {
                                // Execute low-health Character for double damage
                                HealthManagerScript.ChangeHealth("Iv", damageValue * 2, burnRate);
                            }
                            else HealthManagerScript.ChangeHealth("Iv", damageValue, burnRate);
                        }
                    }
                    else
                    {
                        // TODO: Play Smite animation
                        AttackScript.enemyTarget = "None";
                    }
                }
            }


            bool DetermineEnemyHP()
            {
                if (attackingEnemy == "Enemy 1")
                {
                    if (CombatManagerScript.enemy1HP <= CombatManagerScript.enemy1StartingHP * DamageValues.executeThreshold) return true;
                }
                
                if (attackingEnemy == "Enemy 2")
                {
                    if (CombatManagerScript.enemy2HP <= CombatManagerScript.enemy2StartingHP * DamageValues.executeThreshold) return true;
                }

                if (attackingEnemy == "Enemy 3")
                {
                    if (CombatManagerScript.enemy3HP <= CombatManagerScript.enemy3StartingHP * DamageValues.executeThreshold) return true;
                }
                return false;
            }
        }
    }


    public static void GrandSlamAttack(int damageValue, float burnRate, int attackNumber, string attackingEnemy)
    {
        DetermineCharactersHit(attackNumber);
        
        
        void DetermineCharactersHit(int attackNumber)
        {
            int attackLocation;

            if (attackNumber == 1) attackLocation = EnemyManagerScript.attack1Location;
            else attackLocation = EnemyManagerScript.attack2Location;
            
            // If Folkvar is standing on the target location
            if (CharacterManagerScript.folkvarPosition == attackLocation)
            {
                if (CharacterManagerScript.netrixiPosition == attackLocation - 1)
                {
                    // TODO: Play Grand Slam animation
                    AttackScript.enemyTarget = "Folkvar + Netrixi";
                            
                    AttackCharacters("Folkvar", "Netrixi", "null");
                }
                else
                {
                    if (CharacterManagerScript.ivPosition == attackLocation - 1)
                    {
                        // TODO: Play Grand Slam animation
                        AttackScript.enemyTarget = "Folkvar + Iv";
                            
                        AttackCharacters("Folkvar", "Iv", "null");
                    }
                    else
                    {
                        // TODO: Play Grand Slam animation
                        AttackScript.enemyTarget = "Folkvar + Iv";
                            
                        AttackCharacters("Folkvar", "null", "null");
                    }
                }
            }
            else
            {
                // If Netrixi is standing on the target location
                if (CharacterManagerScript.netrixiPosition == attackLocation)
                {
                    if (CharacterManagerScript.ivPosition == attackLocation - 1)
                    {
                        if (CharacterManagerScript.folkvarPosition == attackLocation + 1)
                        {
                            // TODO: Play Grand Slam animation
                            AttackScript.enemyTarget = "All";
                            
                            AttackCharacters("Netrixi", "Folkvar", "Iv");
                        }
                        else
                        {
                            // TODO: Play Grand Slam animation
                            AttackScript.enemyTarget = "Netrixi + Iv";
                            
                            AttackCharacters("Netrixi", "Iv", "null");
                        }
                    }
                    else
                    {
                        if (CharacterManagerScript.folkvarPosition == attackLocation + 1)
                        {
                            // TODO: Play Grand Slam animation
                            AttackScript.enemyTarget = "Netrixi + Folkvar";
                            
                            AttackCharacters("Netrixi", "Folkvar", "null");
                        }
                        else
                        {
                            // TODO: Play Grand Slam animation
                            AttackScript.enemyTarget = "Netrixi";
                            
                            AttackCharacters("Netrixi", "null", "null");
                        }
                    }
                }
                else
                {
                    // If Iv is standing on the target location
                    if (CharacterManagerScript.ivPosition == attackLocation)
                    {
                        if (CharacterManagerScript.netrixiPosition == attackLocation + 1)
                        {
                            // TODO: Play Grand Slam animation
                            AttackScript.enemyTarget = "Iv + Netrixi";
                            
                            AttackCharacters("Iv", "Netrixi", "null");
                        }
                        else
                        {
                            if (CharacterManagerScript.folkvarPosition == attackLocation + 1)
                            {
                                // TODO: Play Grand Slam animation
                                AttackScript.enemyTarget = "Iv + Folkvar";
                            
                                AttackCharacters("Iv", "Folkvar", "null");
                            }
                            else
                            {
                                // TODO: Play Grand Slam animation
                                AttackScript.enemyTarget = "Iv";
                            
                                AttackCharacters("Iv", "null", "null");
                            }
                        }
                    }
                    else
                    {
                        // TODO: Play Grand Slam animation
                        AttackScript.enemyTarget = "None";
                    }
                }
            }


            void AttackCharacters(string character1, string character2, string character3)
            {
                if (AttackScript.countered)
                {
                    HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                    CheckForNearbyEnemies();
                }
                else
                {
                    HealthManagerScript.ChangeHealth(character1, damageValue, burnRate);
                    if (character2 != "null") HealthManagerScript.ChangeHealth(character2, damageValue / 2, burnRate);
                    if (character3 != "null") HealthManagerScript.ChangeHealth(character3, damageValue / 2, burnRate);
                }
            }
        }


        void CheckForNearbyEnemies()
            {
                switch (attackingEnemy)
                {
                    case "Enemy 1":
                        if (EnemyManagerScript.enemy1Position + 1 == EnemyManagerScript.enemy2Position)
                        {
                            HealthManagerScript.ChangeHealth("Enemy 2", damageValue / 2, burnRate);
                        }
                        else
                        {
                            if (EnemyManagerScript.enemy1Position + 1 == EnemyManagerScript.enemy3Position)
                            {
                                HealthManagerScript.ChangeHealth("Enemy 3", damageValue / 2, burnRate);
                            }
                        }
                        break;
                    
                    case "Enemy 2":
                        if (EnemyManagerScript.enemy2Position - 1 == EnemyManagerScript.enemy1Position)
                        {
                            HealthManagerScript.ChangeHealth("Enemy 1", damageValue / 2, burnRate);
                        }
                        
                        if (EnemyManagerScript.enemy2Position + 1 == EnemyManagerScript.enemy3Position)
                        {
                            HealthManagerScript.ChangeHealth("Enemy 3", damageValue / 2, burnRate);
                        }
                        break;
                    
                    case "Enemy 3":
                        if (EnemyManagerScript.enemy3Position - 1 == EnemyManagerScript.enemy2Position)
                        {
                            HealthManagerScript.ChangeHealth("Enemy 2", damageValue / 2, burnRate);
                        }
                        else
                        {
                            if (EnemyManagerScript.enemy3Position - 1 == EnemyManagerScript.enemy1Position)
                            {
                                HealthManagerScript.ChangeHealth("Enemy 1", damageValue / 2, burnRate);
                            }
                        }
                        break;
                }
            }
    }
    
    
    public static void HealTeam(int enemy1Heal, int enemy2Heal, int enemy3Heal, float healRate, string attackingEnemy)
    {
        // TODO: Play Heal animation
        AttackScript.enemyTarget = "All Enemies";
        
        // Can Enemy 1 Regain HP?
        if (enemy1Heal > 0) HealthManagerScript.ChangeHealth("Enemy 1", -enemy1Heal, healRate);

        // Can Enemy 2 Regain HP?
        if (enemy2Heal > 0) HealthManagerScript.ChangeHealth("Enemy 2", -enemy2Heal, healRate);

        // Can Enemy 3 Regain HP?
        if (enemy3Heal > 0) HealthManagerScript.ChangeHealth("Enemy 3", -enemy3Heal, healRate);
    }


    public static void BlockAttack(string attackingEnemy, int playerAttack)
    {
        // If Netrixi's attacks are countered
        if (playerAttack == 1 || playerAttack == 2)
        {
            // TODO: Play Block animation
            AttackScript.enemyTarget = "Netrixi";
            
            isBlocking = false;
            blocking = true;
            tempBlock = AttackScript.enemyAttack;
        }
        
        // If Folkvar's attacks are countered
        else if (playerAttack == 4 || playerAttack == 5 || playerAttack == 6)
        {
            // TODO: Play Block animation
            AttackScript.enemyTarget = "Folkvar";
            
            isBlocking = false;
            blocking = true;
            tempBlock = AttackScript.enemyAttack;
        }
        
        // If Iv's attacks are countered
        else if (playerAttack == 7)
        {
            // TODO: Play Block animation
            AttackScript.enemyTarget = "Netrixi";
            
            isBlocking = false;
            blocking = true;
            tempBlock = AttackScript.enemyAttack;
        }

        else
        {
            // The player's attack cannot be blocked
            
            // TODO: Play Block animation
            AttackScript.enemyTarget = "None";
        }
    }


    public static void EmpowerAttack(string attackingEnemy, bool bothTeams)
    {
        if (bothTeams)
        {
            // TODO: Play Empower animation
            AttackScript.enemyTarget = "Everyone";
            
            empoweringBothTeams = true;
        }
        else
        {
            // TODO: Play Empower animation
            AttackScript.enemyTarget = "All Enemies";
            
            empoweringBothTeams = false;
        }

        isEmpowering = false;
        empowered = true;
        tempEmpower = AttackScript.playerAttack;
    }


    public static void DogAttack(int damageValue, float burnRate, int attackNumber, string attackingEnemy, bool bites)
    {
        // Dog Bite Attack
        if (bites)
        {
            MeleeAttack(damageValue, burnRate, attackingEnemy);
        }

        // Dog Bark Attack
        else
        {
            BombAttack(damageValue, burnRate, attackNumber, attackingEnemy);
        }
    }
    
    
    public static void BombAttack(int damageValue, float burnRate, int attackNumber, string attackingEnemy)
    {
        DetermineCharactersHit(attackNumber);

        void DetermineCharactersHit(int attackNumber)
        {
            int attackLocation;

            if (attackNumber == 1) attackLocation = EnemyManagerScript.attack1Location;
            else attackLocation = EnemyManagerScript.attack2Location;

            void StunAttackingEnemy()
            {
                switch (attackingEnemy)
                {
                    case "Enemy 1":
                        enemy1Stunned = true;
                        break;
                    
                    case "Enemy 2":
                        enemy2Stunned = true;
                        break;
                    
                    case "Enemy 3":
                        enemy3Stunned = true;
                        break;
                }
            }
                
            
            // If Folkvar is standing on the target location
            if (CharacterManagerScript.folkvarPosition == attackLocation)
            {
                // TODO: Play Throw Bomb animation
                AttackScript.enemyTarget = "Folkvar";

                // If Iv is countering this ability
                if (AttackScript.countered)
                {
                    StunAttackingEnemy();
                    startOfTransformation = CombatManagerScript.roundNumber;
                    
                    HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                }
                else
                {
                    folkvarStunned = true;
                    startOfTransformation = CombatManagerScript.roundNumber;
                    
                    HealthManagerScript.ChangeHealth("Folkvar", damageValue, burnRate);
                }
            }
            else
            {
                // If Netrixi is standing on the target location
                if (CharacterManagerScript.netrixiPosition == attackLocation)
                {
                    // TODO: Play Throw Bomb animation
                    AttackScript.enemyTarget = "Netrixi";

                    // If Iv is countering this ability
                    if (AttackScript.countered)
                    {
                        StunAttackingEnemy();
                        startOfTransformation = CombatManagerScript.roundNumber;
                    
                        HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                    }
                    else
                    {
                        netrixiStunned = true;
                        startOfTransformation = CombatManagerScript.roundNumber;
                    
                        HealthManagerScript.ChangeHealth("Netrixi", damageValue, burnRate);
                    }
                }
                else
                {
                    // If Iv is standing on the target location
                    if (CharacterManagerScript.ivPosition == attackLocation)
                    {
                        // TODO: Play Throw Bomb animation
                        AttackScript.enemyTarget = "Iv";

                        // If Iv is countering this ability
                        if (AttackScript.countered)
                        {
                            StunAttackingEnemy();
                            startOfTransformation = CombatManagerScript.roundNumber;
                    
                            HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                        }
                        else
                        {
                            ivStunned = true;
                            startOfTransformation = CombatManagerScript.roundNumber;
                    
                            HealthManagerScript.ChangeHealth("Iv", damageValue, burnRate);
                        }
                    }
                }
            }
        }
    }


    public static void SwingMaceAttack(int damageValue, float burnRate, int attackNumber, string attackingEnemy)
    {
        DetermineCharactersHit(attackNumber);


        void DetermineCharactersHit(int attackNumber)
        {
            int attackLocation1, attackLocation2;

            if (attackNumber == 1)
            {
                    attackLocation1 = EnemyManagerScript.attack1Location;
                    attackLocation2 = EnemyManagerScript.attack1Location2;
            }
            else
            {
                    attackLocation1 = EnemyManagerScript.attack2Location;
                    attackLocation2 = EnemyManagerScript.attack2Location2;
            }

            // FIRST TARGET LOCATION

            // If Folkvar is standing on the first target location
            if (CharacterManagerScript.folkvarPosition == attackLocation1)
            {
                // TODO: Play Swing Mace 1st animation
                AttackScript.enemyTarget = "Folkvar";
            
                // If Iv is countering this ability
                if (AttackScript.countered) 
                    HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                else 
                    HealthManagerScript.ChangeHealth("Folkvar", damageValue, burnRate);
            }
            else
            {
                // If Netrixi is standing on the first target location
                if (CharacterManagerScript.netrixiPosition == attackLocation1)
                {
                    // TODO: Play Swing Mace 1st animation
                    AttackScript.enemyTarget = "Netrixi";
            
                    // If Iv is countering this ability
                    if (AttackScript.countered) 
                        HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                    else 
                        HealthManagerScript.ChangeHealth("Netrixi", damageValue, burnRate);
                }
                else
                {
                    // If Iv is standing on the first target location
                    if (CharacterManagerScript.ivPosition == attackLocation1)
                    {
                        // TODO: Play Swing Mace 1st animation
                        AttackScript.enemyTarget = "Iv";
            
                        // If Iv is countering this ability
                        if (AttackScript.countered) 
                            HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                        else 
                            HealthManagerScript.ChangeHealth("Iv", damageValue, burnRate);
                    }
                    else
                    {
                        // TODO: Play Swing Mace 1st animation
                        AttackScript.enemyTarget = "None";
                    }
                }
            }
                
                
            // SECOND TARGET LOCATION
                
            // If Folkvar is standing on the second target location
            if (CharacterManagerScript.folkvarPosition == attackLocation2)
            {
                // TODO: Play Swing Mace 2nd animation
                AttackScript.enemyTarget = "Folkvar";
            
                // If Iv is countering this ability
                if (AttackScript.countered) 
                    HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                else 
                    HealthManagerScript.ChangeHealth("Folkvar", damageValue, burnRate);
            }
            else
            {
                // If Netrixi is standing on the second target location
                if (CharacterManagerScript.netrixiPosition == attackLocation2)
                {
                    // TODO: Play Swing Mace 2nd animation
                    AttackScript.enemyTarget = "Netrixi";
            
                    // If Iv is countering this ability
                    if (AttackScript.countered) 
                        HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                    else 
                        HealthManagerScript.ChangeHealth("Netrixi", damageValue, burnRate);
                }
                else
                {
                    // If Iv is standing on the second target location
                    if (CharacterManagerScript.ivPosition == attackLocation2)
                    {
                        // TODO: Play Swing Mace 2nd animation
                        AttackScript.enemyTarget = "Iv";
            
                        // If Iv is countering this ability
                        if (AttackScript.countered) 
                            HealthManagerScript.ChangeHealth(attackingEnemy, damageValue, burnRate);
                        else 
                            HealthManagerScript.ChangeHealth("Iv", damageValue, burnRate);
                    }
                    else
                    {
                        // TODO: Play Swing Mace 2nd animation
                        AttackScript.enemyTarget = "None";
                    }
                }
            }
        }
    }

    
    
    
    
    

    public static void DetermineTargetLocation(string attack, int attackNumber)
    {
        string enemyAttack;

        string[] splitArray =  attack.Split(char.Parse("-"));
        enemyAttack = splitArray[1];

        // If an Enemy will attack a certain location with a Grand Slam
        if (enemyAttack == "Performs a Grand Slam")
        {
            // If Folkvar is alive
            if (CombatManagerScript.folkvarAlive)
            {
                // If Netrixi is alive
                if (CombatManagerScript.netrixiAlive)
                {
                    // If Folkvar is close to Netrixi
                    if (CharacterManagerScript.folkvarPosition - 1 == CharacterManagerScript.netrixiPosition)
                    {
                        // If Iv is alive
                        if (CombatManagerScript.ivAlive)
                        {
                            // If Iv is close to Folkvar and Netrixi
                            if (CharacterManagerScript.netrixiPosition - 1 == CharacterManagerScript.ivPosition)
                            {
                                // Attack Netrixi and deal splash damage to Folkvar and Iv
                                if (attackNumber == 1) EnemyManagerScript.attack1Location = CharacterManagerScript.netrixiPosition;
                                else EnemyManagerScript.attack2Location = CharacterManagerScript.netrixiPosition;
                            }
                            else
                            {
                                // Attack Folkvar and deal splash damage to Netrixi
                                if (attackNumber == 1) EnemyManagerScript.attack1Location = CharacterManagerScript.folkvarPosition;
                                else EnemyManagerScript.attack2Location = CharacterManagerScript.folkvarPosition;
                            }
                        }
                        else
                        {
                            // Attack Netrixi and deal splash damage to Folkvar
                            if (attackNumber == 1) EnemyManagerScript.attack1Location = CharacterManagerScript.netrixiPosition;
                            else EnemyManagerScript.attack2Location = CharacterManagerScript.netrixiPosition;
                        }
                    }
                    else
                    {
                        // If Netrixi is close to Iv
                        if (CharacterManagerScript.netrixiPosition - 1 == CharacterManagerScript.ivPosition)
                        {
                            // Attack Netrixi and deal splash damage to Iv
                            if (attackNumber == 1) EnemyManagerScript.attack1Location = CharacterManagerScript.netrixiPosition;
                            else EnemyManagerScript.attack2Location = CharacterManagerScript.netrixiPosition;
                        }
                        else
                        {
                            // Attack Folkvar only
                            if (attackNumber == 1) EnemyManagerScript.attack1Location = CharacterManagerScript.folkvarPosition;
                            else EnemyManagerScript.attack2Location = CharacterManagerScript.folkvarPosition;
                        }
                    }
                }
                else
                {
                    // Attack Folkvar and maybe do splash damage to Iv
                    if (attackNumber == 1) EnemyManagerScript.attack1Location = CharacterManagerScript.folkvarPosition;
                    else EnemyManagerScript.attack2Location = CharacterManagerScript.folkvarPosition;
                }
            }

            // If Folkvar isn't alive
            else
            {
                // If Netrixi is alive
                if (CombatManagerScript.netrixiAlive)
                {
                    // If Netrixi is close to Iv
                    if (CharacterManagerScript.netrixiPosition - 1 == CharacterManagerScript.ivPosition)
                    {
                        // Attack Iv and deal splash damage to Netrixi
                        if (attackNumber == 1) EnemyManagerScript.attack1Location = CharacterManagerScript.ivPosition;
                        else EnemyManagerScript.attack2Location = CharacterManagerScript.ivPosition;
                    }
                    else
                    {
                        // Attack Netrixi only
                        if (attackNumber == 1) EnemyManagerScript.attack1Location = CharacterManagerScript.netrixiPosition;
                        else EnemyManagerScript.attack2Location = CharacterManagerScript.netrixiPosition;
                    }
                }
                else
                {
                    // Attack Iv only
                    if (attackNumber == 1) EnemyManagerScript.attack1Location = CharacterManagerScript.ivPosition;
                    else EnemyManagerScript.attack2Location = CharacterManagerScript.ivPosition;
                }
            }
        }
        
        
        
        // If an Enemy will attack a certain location by Swinging a Mace
        else if (enemyAttack == "Swings Mace")
        {
            // Choose a random first square to attack
            int randomLocation1 = UnityEngine.Random.Range(1, 5);
            randomLocation1 = ChooseNewRandomNumber(randomLocation1);

            // Choose a random second square to attack
            int randomLocation2 = UnityEngine.Random.Range(1, 5);
            randomLocation2 = ChooseNewRandomNumber(randomLocation2);
            
            int ChooseNewRandomNumber(int randomNumber)
            {
                if (attackNumber == 2)
                {
                    if (randomNumber == EnemyManagerScript.attack1Location)
                    {
                        randomNumber = UnityEngine.Random.Range(1,5);
                        return ChooseNewRandomNumber(randomNumber);
                    }
                }
                
                return randomNumber;
            }

            
            if (attackNumber == 1)
            {
                EnemyManagerScript.attack1Location = randomLocation1;
                EnemyManagerScript.attack1Location2 = randomLocation2;
            }
            else
            {
                EnemyManagerScript.attack2Location = randomLocation1;
                EnemyManagerScript.attack2Location2 = randomLocation2;
            }
        }



        // If an Enemy will attack a certain location by Throwing a Bomb or a Dog Barks
        else if (enemyAttack == "Throws Bomb" || enemyAttack == "Dog Barks" || enemyAttack == "Smashes")
        {
            List<int> possibleTargets = new List<int>();

            if (CombatManagerScript.netrixiAlive) possibleTargets.Add(CharacterManagerScript.netrixiPosition);
            if (CombatManagerScript.folkvarAlive) possibleTargets.Add(CharacterManagerScript.folkvarPosition);
            if (CombatManagerScript.ivAlive) possibleTargets.Add(CharacterManagerScript.ivPosition);

            // Choose a random square to attack
            int randomIndex = UnityEngine.Random.Range(0, (possibleTargets.Count));
            randomIndex = ChooseNewRandomNumber(randomIndex);
            
            int ChooseNewRandomNumber(int randomNumber)
            {
                if (attackNumber == 2)
                {
                    if (possibleTargets[randomNumber] == EnemyManagerScript.attack1Location)
                    {
                        randomNumber = UnityEngine.Random.Range(0, (possibleTargets.Count));
                        return ChooseNewRandomNumber(randomNumber);
                    }
                }

                return randomNumber;
            }
            

            if (attackNumber == 1) EnemyManagerScript.attack1Location = possibleTargets[randomIndex];
            else EnemyManagerScript.attack2Location = possibleTargets[randomIndex];
        }
        
        
        
        // If an Enemy will attack a certain location by Smiting it
        else if (enemyAttack == "Smites")
        {
            int lowestCharacterHP = 500;
            int targetCharacter = 0;

            // Determine which character has the lowest HP
            if (CombatManagerScript.netrixiAlive)
            {
                targetCharacter = 1;
                lowestCharacterHP = CombatManagerScript.netrixiHP;
            }
            if (CombatManagerScript.folkvarAlive)
                if (CombatManagerScript.folkvarHP < lowestCharacterHP)
                {
                    targetCharacter = 2;
                    lowestCharacterHP = CombatManagerScript.folkvarHP;
                }
            if (CombatManagerScript.ivAlive)
                if (CombatManagerScript.ivHP < lowestCharacterHP)
                {
                    targetCharacter = 3;
                    lowestCharacterHP = CombatManagerScript.ivHP;
                }
            
            switch (targetCharacter)
            {
                // Netrixi has the lowest HP
                case 1:
                    if (attackNumber == 1) EnemyManagerScript.attack1Location = CharacterManagerScript.netrixiPosition;
                    else EnemyManagerScript.attack2Location = CharacterManagerScript.netrixiPosition;
                    break;
                                
                // Folkvar has the lowest HP
                case 2:
                    if (attackNumber == 1) EnemyManagerScript.attack1Location = CharacterManagerScript.folkvarPosition;
                    else EnemyManagerScript.attack2Location = CharacterManagerScript.folkvarPosition;
                    break;
                                
                // Iv has the lowest HP
                case 3:
                    if (attackNumber == 1) EnemyManagerScript.attack1Location = CharacterManagerScript.ivPosition;
                    else EnemyManagerScript.attack2Location = CharacterManagerScript.ivPosition;
                    break;
            }
        }
        
        
        
        // If an Enemy will attack a certain location by Throwing a Chair or a Knife
        else if (enemyAttack == "Throws Knife" || enemyAttack == "Throws Chair")
        {
            // If Iv is still alive
            if (CombatManagerScript.ivAlive)
            {
                // Attack Iv
                if (attackNumber == 1) EnemyManagerScript.attack1Location = CharacterManagerScript.ivPosition;
                else EnemyManagerScript.attack2Location = CharacterManagerScript.ivPosition;
            }
            else
            {
                // If Netrixi is still alive
                if (CombatManagerScript.netrixiAlive)
                {
                    // Attack Netrixi
                    if (attackNumber == 1) EnemyManagerScript.attack1Location = CharacterManagerScript.netrixiPosition;
                    else EnemyManagerScript.attack2Location = CharacterManagerScript.netrixiPosition;
                }
                else
                {
                    // If Folkvar is still alive
                    if (CombatManagerScript.folkvarAlive)
                    {
                        // Attack Folkvar
                        if (attackNumber == 1) EnemyManagerScript.attack1Location = CharacterManagerScript.folkvarPosition;
                        else EnemyManagerScript.attack2Location = CharacterManagerScript.folkvarPosition;
                    }
                }
            }
        }



        // If the Enemy does any other attack, Do not display a target attack location
        else
        {
            if (attackNumber == 1) EnemyManagerScript.attack1Location = 0;
            else EnemyManagerScript.attack2Location = 0;
        }
    }
}
