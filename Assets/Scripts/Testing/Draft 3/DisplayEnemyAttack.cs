using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEnemyAttack : MonoBehaviour
{
    public bool attack1, attack2;

    public Sprite knightMelee, knightRanged, gatekeeper, royalGuard, folkvar;
    public Sprite skullMelee, skullRanged, kaz, skullKing, evilKing;
    public Sprite pirate, brute, barkeeper;

    private SpriteRenderer sr;

    public Text firstAttack, secondAttack;

    private string firstAttackText, secondAttackText;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attack1)
        {
            DetermineAttackSprite(1);
            DetermineMove(1);

            firstAttack.text = firstAttackText;
        }

        if (attack2)
        {
            DetermineAttackSprite(2);
            DetermineMove(2);

            secondAttack.text = secondAttackText;
        }
    }

    void DetermineAttackSprite( int attackNumber)
    {
        string tempMove;
        string attackText = "null";
        
        if (attackNumber == 1) tempMove = EnemyManagerScript.firstAttack;
        else tempMove = EnemyManagerScript.secondAttack;

        switch (tempMove)
        {
            // Royal Knight Melee
            case "Royal Knight Swings Sword":
                attackText = "Swing Sword";
                sr.sprite = knightMelee;
                break;
            case "Royal Knight Blocks":
                attackText = "Block";
                sr.sprite = knightMelee;
                break;
            
            // Folkvar
             case "Folkvar Swings Sword":
                 attackText = "Swing Sword";
                 sr.sprite = folkvar;
                 break;
             case "Folkvar Smites":
                 attackText = "Smite";
                 sr.sprite = folkvar;
                 break;
             
             // Royal Knight Ranged
             case "Royal Knight Shoots Arrow":
                 attackText = "Shoot Arrow";
                 sr.sprite = knightRanged;
                 break;
             case "Royal Knight Heals Team":
                 attackText = "Heal Team";
                 sr.sprite = knightRanged;
                 break;

            // Royal Gatekeeper
             case "Gatekeeper's Dog Barks":
                 attackText = "Dog Barks";
                 sr.sprite = gatekeeper;
                 break;
             case "Gatekeeper's Dog Bites": 
                 attackText = "Dog Bites";
                 sr.sprite = gatekeeper;
                 break;
             case "Gatekeeper Blocks":
                 attackText = "Block";
                 sr.sprite = gatekeeper;
                 break;
             
             
             // Royal Guards
             case "Royal Guard 1 Swings Battle Axe":
                 attackText = "Swing Axe";
                 sr.sprite = royalGuard;
                 break;
             case "Royal Guard 1 Performs a Grand Slam":
                 attackText = "Grand Slam";
                 sr.sprite = royalGuard;
                 break;
             case "Royal Guard 1 Blocks":
                 attackText = "Block";
                 sr.sprite = royalGuard;
                 break;
             case "Royal Guard 2 Swings Battle Axe":
                 attackText = "Swing Axe";
                 sr.sprite = royalGuard;
                 break;
             case "Royal Guard 2 Performs a Grand Slam":
                 attackText = "Grand Slam";
                 sr.sprite = royalGuard;
                 break;
             case "Royal Guard 2 Blocks":
                 attackText = "Block";
                 sr.sprite = royalGuard;
                 break;


             
             // Enemy Skull Melee
             case "Skull Grunt Slashes":
                 attackText = "Swing Sword";
                 sr.sprite = skullMelee;
                 break;
             case "Skull Grunt Blocks":
                 attackText = "Block";
                 sr.sprite = skullMelee;
                 break;
             
             // Enemy Skull Ranged
             case "Skull Grunt Shoots Arrow":
                 attackText = "Shoot Arrow";
                 sr.sprite = skullRanged;
                 break;
             case "Skull Grunt Throws Bomb":
                 attackText = "Throw Bomb";
                 sr.sprite = skullRanged;
                 break;
             
             
             // Kaz
             case "Kaz Shoots Arrow":
                 attackText = "Shoot Arrow";
                 sr.sprite = kaz;
                 break;
             case "Kaz Swings Battle Axe":
                 attackText = "Swing Axe";
                 sr.sprite = kaz;
                 break;
             case "Kaz Performs a Grand Slam":
                 attackText = "Grand Slam";
                 sr.sprite = kaz;
                 break;
             case "Kaz Empowers both Teams":
                 attackText = "Empower";
                 sr.sprite = kaz;
                 break;
             case "Kaz Blocks":
                 attackText = "Block";
                 sr.sprite = kaz;
                 break;
             
             
             // Skull King
             case "Skull King Swings Mace":
                 attackText = "Swing Mace";
                 sr.sprite = skullKing;
                 break;
             case "Skull King Performs a Grand Slam":
                 attackText = "Grand Slam";
                 sr.sprite = skullKing;
                 break;
             case "Skull King Throws Bomb":
                 attackText = "Throw Bomb";
                 sr.sprite = skullKing;
                 break;
             
             // Royal King
             case "Royal King Swings Mace":
                 attackText = "Swing Mace";
                 sr.sprite = evilKing;
                 break;
             case "Royal King Performs a Grand Slam":
                 attackText = "Grand Slam";
                 sr.sprite = evilKing;
                 break;
             case "Royal King Empowers his Team":
                 attackText = "Empower";
                 sr.sprite = evilKing;
                 break;
             case "Royal King Heals his Team":
                 attackText = "Heal Team";
                 sr.sprite = evilKing;
                 break;
             case "Royal King Casts a Shield":
                 attackText = "Block";
                 sr.sprite = evilKing;
                 break;
             
             
             
            // Pirate
            case "Pirate Slashes":
                attackText = "Swing Sword";
                sr.sprite = pirate;
                break;
            case "Pirate Throws Knife":
                attackText = "Throw Knife";
                sr.sprite = pirate;
                break;
            
            
            // Tavern Brute
            case "Tavern Brute Smash":
                attackText = "Smash";
                sr.sprite = brute;
                break;
            case "Tavern Brute Throws Chair":
                attackText = "Throw Chair";
                sr.sprite = brute;
                break;
            
            
            // Barkeeper
            case "Barkeeper Punches You":
                attackText = "Punch You";
                sr.sprite = barkeeper;
                break;
            case "Barkeeper Punches Tavern Brute": 
                attackText = "Punch Brute";
                sr.sprite = barkeeper;
                break;
        }

        if (attackNumber == 1) firstAttackText = attackText;
        else secondAttackText = attackText;
    }

    void DetermineMove(int attackNumber)
    {
        if (attackNumber == 1)
        {
            if (EnemyManagerScript.firstAttack == "Enemy 1 Moves Left" ||
                EnemyManagerScript.firstAttack == "Enemy 1 Moves Right")
            {
                DetermineEnemy(1);
                firstAttackText = "Move";
            }
            
            if (EnemyManagerScript.firstAttack == "Enemy 2 Moves Left" ||
                EnemyManagerScript.firstAttack == "Enemy 2 Moves Right")
            {
                DetermineEnemy(2);
                firstAttackText = "Move";
            }
            
            if (EnemyManagerScript.firstAttack == "Enemy 3 Moves Left" ||
                EnemyManagerScript.firstAttack == "Enemy 3 Moves Right")
            {
                DetermineEnemy(3);
                firstAttackText = "Move";
            }
        }
        
        if (attackNumber == 2)
        {
            if (EnemyManagerScript.secondAttack == "Enemy 1 Moves Left" ||
                EnemyManagerScript.secondAttack == "Enemy 1 Moves Right")
            {
                DetermineEnemy(1);
                secondAttackText = "Move";
            }
            
            if (EnemyManagerScript.secondAttack == "Enemy 2 Moves Left" ||
                EnemyManagerScript.secondAttack == "Enemy 2 Moves Right")
            {
                DetermineEnemy(2);
                secondAttackText = "Move";
            }
            
            if (EnemyManagerScript.secondAttack == "Enemy 3 Moves Left" ||
                EnemyManagerScript.secondAttack == "Enemy 3 Moves Right")
            {
                DetermineEnemy(3);
                secondAttackText = "Move";
            }
        }
    }

    void DetermineEnemy(int enemyNumber)
    {
        string enemyName;
        
        switch (enemyNumber)
        {
            case 1:
                enemyName = EnemyManagerScript.enemy1;
                ChangeSprite(enemyName);
                break;
            
            case 2:
                enemyName = EnemyManagerScript.enemy2;
                ChangeSprite(enemyName);
                break;
            
            case 3:
                enemyName = EnemyManagerScript.enemy3;
                ChangeSprite(enemyName);
                break;
        }
    }

    void ChangeSprite(string enemyName)
    {
        switch (enemyName)
        {
            case "Pirate":
                sr.sprite = pirate;
                break;
                    
            
            
            case "Folkvar":
                sr.sprite = folkvar;
                break;
                    
            case "Royal Knight Melee":
                sr.sprite = knightMelee;
                break;
                    
            case "Royal Knight Ranged":
                sr.sprite = knightRanged;
                break;
            
            
            
            case "Skull Grunt Melee":
                sr.sprite = skullMelee;
                break;
            
            case "Skull Grunt Ranged":
                sr.sprite = skullRanged;
                break;
            
            case "Kaz":
                sr.sprite = kaz;
                break;
            
            
            
            case "Tavern Brute":
                sr.sprite = brute;
                break;
            
            case "Barkeeper":
                sr.sprite = barkeeper;
                break;
            
            
            
            case "Gatekeeper":
                sr.sprite = gatekeeper;
                break;
            
            case "Royal Guard 1":
                sr.sprite = royalGuard;
                break;
            
            case "Royal Guard 2":
                sr.sprite = royalGuard;
                break;
            
            
            
            case "Skull King":
                sr.sprite = skullKing;
                break;
            
            case "Royal King":
                sr.sprite = evilKing;
                break;
        }
    }
}
