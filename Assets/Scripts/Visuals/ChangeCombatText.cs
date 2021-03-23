using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCombatText : MonoBehaviour
{
    public Text playerAttackText, enemyAttackText;
    public Text roundNumber;

    public Text playerAttack1, playerAttack2;
    public Text enemyAttack1, enemyAttack2;

    public GameObject playerSprite1, playerSprite2;
    public GameObject enemySprite1, enemySprite2;

    public static ChangeCombatText S;

    private float offScreen = GameWindowManager.offScreen;

    private float xPadding, yPadding;

    private SpriteRenderer enemySR1, enemySR2;

    // Start is called before the first frame update
    void Awake()
    {
        S = this;

        enemySR1 = enemySprite1.GetComponent<SpriteRenderer>();
        enemySR2 = enemySprite2.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManagerScript.inCombat)
        {
            // Hide these text boxes if not in combat
            playerAttackText.transform.position = new Vector3(offScreen, offScreen, 0);
            enemyAttackText.transform.position = new Vector3(offScreen, offScreen, 0);
            
            roundNumber.transform.position = new Vector3(offScreen, offScreen, 0);
        }



        // Check to see if the player has decided on their first move
        if (CombatManagerScript.firstAttack == 0)
        {
            playerAttack1.text = "#1";
            playerAttack1.color = Color.gray;
            
            playerAttack1.fontSize = 70;
        }
        else
        {
            playerAttack1.text = DisplayPlayerAttack(CombatManagerScript.firstAttack);
            
            if (CombatManagerScript.playerAttacking1) playerAttack1.color = Color.gray;
            else playerAttack1.color = Color.white;
            
            playerAttack1.fontSize = 50;
        }
        
        // Check to see if the player has decided on their second move
        if (CombatManagerScript.secondAttack == 0)
        {
            playerAttack2.text = "#2";
            playerAttack2.color = Color.gray;
            
            playerAttack2.fontSize = 70;
        }
        else
        {
            playerAttack2.text = DisplayPlayerAttack(CombatManagerScript.secondAttack);
            
            if (CombatManagerScript.playerAttacking2) playerAttack2.color = Color.gray;
            else playerAttack2.color = Color.white;
            
            playerAttack2.fontSize = 50;
        }
        
        
        
        // Check to see if the enemy has decided on their first move
        if (EnemyManagerScript.firstAttack == null)
        {
            enemyAttack1.text = "#1";
            enemyAttack1.color = Color.gray;
        }
        else
        {
            if (DetermineMove(1) == "Move") enemyAttack1.text = "Move";
            else
            {
                enemyAttack1.text = DisplayEnemyAttack(EnemyManagerScript.firstAttack, 1);
            }

            if (CombatManagerScript.enemyAttacking1) enemyAttack1.color = Color.gray;
            else enemyAttack1.color = Color.white;
        }
        
        // Check to see if the enemy has decided on their second move
        if (EnemyManagerScript.secondAttack == null)
        {
            enemyAttack2.text = "#2";
            enemyAttack2.color = Color.gray;
        }
        else
        {
            if (DetermineMove(2) == "Move") enemyAttack2.text = "Move";
            else
            {
                enemyAttack2.text = DisplayEnemyAttack(EnemyManagerScript.secondAttack, 2);
            }

            if (CombatManagerScript.enemyAttacking2) enemyAttack2.color = Color.gray;
            else enemyAttack2.color = Color.white;
        }
    }


    public void ChangeTextLocations(float windowWidth, float windowHeight)
    {
        // Position the attack display texts
        playerAttackText.transform.position = new Vector3(windowWidth/4, Screen.height - (windowHeight) - ((windowHeight/5)/2), 0);
        enemyAttackText.transform.position = new Vector3(windowWidth*3/4, Screen.height - (windowHeight) - ((windowHeight/5)/2), 0);
        
        // Position the round number text
        
        // Top of screen
        roundNumber.transform.position = new Vector3(windowWidth/2, Screen.height - ((windowHeight/6)/2), 0);
        
        // Bottom of Screen
        //roundNumber.transform.position = new Vector3(windowWidth/2, Screen.height - (windowHeight) - ((windowHeight*5/6)/2), 0);
        
        
        // Position player attacks
        playerAttack1.transform.position = new Vector3(windowWidth*2.5f/18, Screen.height - (windowHeight) - ((windowHeight*4/5)/2), 0);
        playerAttack2.transform.position = new Vector3(windowWidth*6.5f/18, Screen.height - (windowHeight) - ((windowHeight*4/5)/2), 0);
        
        // Position enemy attacks
        enemyAttack1.transform.position = new Vector3(windowWidth*11.5f/18, Screen.height - (windowHeight) - ((windowHeight*4/5)/2), 0);
        enemyAttack2.transform.position = new Vector3(windowWidth*15.5f/18, Screen.height - (windowHeight) - ((windowHeight*4/5)/2), 0);
        
        ChangeSpriteLocations(windowWidth, windowHeight);
    }
    
    
    public void ChangeSpriteLocations(float windowWidth, float windowHeight)
    {
        SpriteRenderer p1 = playerSprite1.GetComponent<SpriteRenderer>();
        SpriteRenderer p2 = playerSprite2.GetComponent<SpriteRenderer>();
        SpriteRenderer e1 = enemySprite1.GetComponent<SpriteRenderer>();
        SpriteRenderer e2 = enemySprite2.GetComponent<SpriteRenderer>();

        SpriteRenderer[] sr = new[] {p1, p2, e1, e2};
        
        // Determine dimensions of the camera within the scene
        Camera camera = Camera.main;
        float cameraHeight = 2f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;
        
        // Determine the dimensions of the sprites
        windowWidth = windowWidth / Screen.width * cameraWidth;
        windowHeight = windowHeight / Screen.height * cameraHeight;

        for (int i = 0; i < sr.Length; i++)
        {
            float currWidth = sr[i].sprite.bounds.extents.x * 2;
            float currHeight = sr[i].sprite.bounds.extents.y * 2;

            float desiredHeight = windowHeight / 3;
            
            if (currWidth != windowWidth)
            {
                switch (i)
                {
                    case 0:
                        playerSprite1.transform.localScale = new Vector3(desiredHeight/currWidth, desiredHeight/currHeight, 1);
                        break;
                    
                    case 1:
                        playerSprite2.transform.localScale = new Vector3(desiredHeight/currWidth, desiredHeight/currHeight, 1);
                        break;
                    
                    case 2:
                        enemySprite1.transform.localScale = new Vector3(desiredHeight/currWidth, desiredHeight/currHeight, 1);
                        break;
                    
                    case 3:
                        enemySprite2.transform.localScale = new Vector3(desiredHeight/currWidth, desiredHeight/currHeight, 1);
                        break;
                }
            }
        
            // Determine the locations of the sprites
            float yLocation = -cameraHeight/2 + ((windowHeight/2)/2);
            
            switch (i)
            {
                case 0:
                    playerSprite1.transform.position = new Vector3(-cameraWidth/2 + (windowWidth*2.5f/18), yLocation, 0);
                    break;
                    
                case 1:
                    playerSprite2.transform.position = new Vector3(-cameraWidth/2 + (windowWidth*6.5f/18), yLocation, 0);
                    break;
                    
                case 2:
                    enemySprite1.transform.position = new Vector3(-cameraWidth/2 + (windowWidth*11.5f/18), yLocation, 0);
                    break;
                    
                case 3:
                    enemySprite2.transform.position = new Vector3(-cameraWidth/2 + (windowWidth*15.5f/18), yLocation, 0);
                    break;
            }
            
            
            // Change the player sprites
            if (CombatManagerScript.firstAttack != 0)
            {
                sr[0].enabled = true;
                sr[0].sprite = ChangeSprites.S.DisplayPlayerCharacter(CombatManagerScript.firstAttack, true);
            }
            else sr[0].enabled = false;
            
            if (CombatManagerScript.secondAttack != 0)
            {
                sr[1].enabled = true;
                sr[1].sprite = ChangeSprites.S.DisplayPlayerCharacter(CombatManagerScript.secondAttack, true);
            }
            else sr[1].enabled = false;
            
            
            // Change the enemy sprites
            if (EnemyManagerScript.firstAttack != null) sr[2].enabled = true;
            else sr[2].enabled = false;
            
            if (EnemyManagerScript.secondAttack != null) sr[3].enabled = true;
            else sr[3].enabled = false;
        }
    }


    private string DisplayPlayerAttack(int attack)
    {
        switch (attack)
        {
            // If Netrixi is attacking
            case 1:
                return "Fireball";

            case 2:
                return "Lightning";

            case 3:
                return "Transmutate";

            // If Folkvar is attacking
            case 4:
                return "Swing Sword";

            case 5:
                return "Holy Smite";

            case 6:
                return "Grand Slam";

            // If Iv is attacking
            case 7:
                if (AttackScript.countered)
                {
                    return "Counter";
                }
                else
                {
                    return "Block";
                }

            case 8:
                return "Heal Ally";

            case 9:
                return "Empower";

            // If Netrixi, Folkvar, or Iv moves left
            case 10:
            case 12:
            case 14:
                return "Move Left";

            // If Netrixi, Folkvar, or Iv moves right
            case 11:
            case 13:
            case 15:
                return "Move Right";
        }

        return "";
    }


    private string DisplayEnemyAttack(string attack, int attackNumber)
    {
        switch (attack)
        {
            // Royal Knight Melee
            case "Royal Knight Melee-Swings Sword":
                if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Royal Knight Melee", enemySR1);
                else ChangeSprites.S.ChangeEnemyCharacter("Royal Knight Melee", enemySR2);
                return "Swing Sword";
                
            case "Royal Knight Melee-Blocks":
                if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Royal Knight Melee", enemySR1);
                else ChangeSprites.S.ChangeEnemyCharacter("Royal Knight Melee", enemySR2);
                return "Block";
                
            
            // Folkvar
             case "Folkvar-Swings Sword":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Folkvar", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Folkvar", enemySR2);
                 return "Swing Sword";
             
             case "Folkvar-Smites":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Folkvar", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Folkvar", enemySR2);
                 return "Smite";
             
             
             // Royal Knight Ranged
             case "Royal Knight Ranged-Shoots Arrow":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Royal Knight Ranged", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Royal Knight Ranged", enemySR2);
                 return "Shoot Arrow";
             
             case "Royal Knight Ranged-Heals Team":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Royal Knight Ranged", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Royal Knight Ranged", enemySR2);
                 return "Heal Team";

             
            // Royal Gatekeeper
             case "Gatekeeper-Dog Barks":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Gatekeeper", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Gatekeeper", enemySR2);
                 return "Dog Barks";
                 
             case "Gatekeeper-Dog Bites": 
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Gatekeeper", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Gatekeeper", enemySR2);
                 return "Dog Bites";
              
             case "Gatekeeper-Blocks":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Gatekeeper", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Gatekeeper", enemySR2);
                 return "Block";


            // Royal Guards
             case "Royal Guard 1-Swings Battle Axe":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Royal Guard 1", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Royal Guard 1", enemySR2);
                 return "Swing Axe";
            
             case "Royal Guard 1-Performs a Grand Slam":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Royal Guard 1", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Royal Guard 1", enemySR2);
                 return "Grand Slam";
              
             case "Royal Guard 1-Blocks":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Royal Guard 1", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Royal Guard 1", enemySR2);
                 return "Block";
               
             case "Royal Guard 2-Swings Battle Axe":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Royal Guard 2", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Royal Guard 2", enemySR2);
                 return "Swing Axe";
                
             case "Royal Guard 2-Performs a Grand Slam":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Royal Guard 2", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Royal Guard 2", enemySR2);
                 return "Grand Slam";
              
             case "Royal Guard 2-Blocks":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Royal Guard 2", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Royal Guard 2", enemySR2);
                 return "Block";



            // Enemy Skull Melee
             case "Skull Grunt Melee-Slashes":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Skull Grunt Melee", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Skull Grunt Melee", enemySR2);
                 return "Swing Sword";
             
             case "Skull Grunt Melee-Blocks":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Skull Grunt Melee", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Skull Grunt Melee", enemySR2);
                 return "Block";
             
             
             // Enemy Skull Ranged
             case "Skull Grunt Ranged-Shoots Arrow":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Skull Grunt Ranged", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Skull Grunt Ranged", enemySR2);
                 return "Shoot Arrow";
             
             case "Skull Grunt Ranged-Throws Bomb":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Skull Grunt Ranged", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Skull Grunt Ranged", enemySR2);
                 return "Throw Bomb";


            // Kaz
             case "Kaz 1-Shoots Arrow":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Kaz 1", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Kaz 1", enemySR2);
                 return "Shoot Arrow";
               
             case "Kaz 1-Swings Battle Axe":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Kaz 1", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Kaz 1", enemySR2);
                 return "Swing Axe";
                
             case "Kaz 1-Performs a Grand Slam":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Kaz 1", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Kaz 1", enemySR2);
                 return "Grand Slam";
               
             
            case "Kaz 2-Swings Battle Axe":
                if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Kaz 2", enemySR1);
                else ChangeSprites.S.ChangeEnemyCharacter("Kaz 2", enemySR2);
                 return "Swing Axe";
                 
            case "Kaz 2-Performs a Grand Slam":
                if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Kaz 2", enemySR1);
                else ChangeSprites.S.ChangeEnemyCharacter("Kaz 2", enemySR2);
                 return "Grand Slam";
               
            case "Kaz 2-Empowers Both Teams":
                if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Kaz 2", enemySR1);
                else ChangeSprites.S.ChangeEnemyCharacter("Kaz 2", enemySR2);
                return "Empower";
            
             case "Kaz 2-Blocks": 
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Kaz 2", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Kaz 2", enemySR2);
                 return "Block";


            // Skull King
             case "Skull King-Swings Mace":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Skull King", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Skull King", enemySR2);
                 return "Swing Mace";
                 
             case "Skull King-Performs a Grand Slam":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Skull King", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Skull King", enemySR2);
                 return "Grand Slam";
              
             case "Skull King-Throws Bomb":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Skull King", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Skull King", enemySR2);
                 return "Throw Bomb";
               
             
             // Royal King
             case "Royal King-Swings Mace":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Royal King", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Royal King", enemySR2);
                 return "Swing Mace";
         
             case "Royal King-Performs a Grand Slam":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Royal King", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Royal King", enemySR2);
                 return "Grand Slam";
             
             case "Royal King-Empowers His Team":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Royal King", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Royal King", enemySR2);
                 return "Empower";
             
            case "Royal King-Smites":
                if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Royal King", enemySR1);
                else ChangeSprites.S.ChangeEnemyCharacter("Royal King", enemySR2);
                return "Smite";
            
             case "Royal King-Heals Team":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Royal King", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Royal King", enemySR2);
                 return "Heal Team";
             
             case "Royal King-Blocks":
                 if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Royal King", enemySR1);
                 else ChangeSprites.S.ChangeEnemyCharacter("Royal King", enemySR2);
                 return "Block";



            // Pirate
            case "Pirate-Slashes":
                if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Pirate", enemySR1);
                else ChangeSprites.S.ChangeEnemyCharacter("Pirate", enemySR2);
                return "Swing Sword";
               
            case "Pirate-Throws Knife":
                if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Pirate", enemySR1);
                else ChangeSprites.S.ChangeEnemyCharacter("Pirate", enemySR2);
                return "Throw Knife";


            // Tavern Brute
            case "Tavern Brute-Smashes":
                if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Tavern Brute", enemySR1);
                else ChangeSprites.S.ChangeEnemyCharacter("Tavern Brute", enemySR2);
                return "Smash";
          
            case "Tavern Brute-Throws Chair":
                if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Tavern Brute", enemySR1);
                else ChangeSprites.S.ChangeEnemyCharacter("Tavern Brute", enemySR2);
                return "Throw Chair";


            // Barkeeper
            case "Barkeeper-Punches You":
                if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Barkeeper", enemySR1);
                else ChangeSprites.S.ChangeEnemyCharacter("Barkeeper", enemySR2);
                return "Punch You";
     
            case "Barkeeper-Punches Tavern Brute": 
                if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter("Barkeeper", enemySR1);
                else ChangeSprites.S.ChangeEnemyCharacter("Barkeeper", enemySR2);
                return "Punch Brute";
        }

        return "";
    }


    private string DetermineMove(int attackNumber)
    {
        if (attackNumber == 1)
        {
            if (EnemyManagerScript.firstAttack == "Enemy 1-Moves Left" ||
                EnemyManagerScript.firstAttack == "Enemy 1-Moves Right")
            {
                DetermineEnemy(1, 1);
                return "Move";
            }

            if (EnemyManagerScript.firstAttack == "Enemy 2-Moves Left" ||
                EnemyManagerScript.firstAttack == "Enemy 2-Moves Right")
            {
                DetermineEnemy(2, 1);
                return "Move";
            }

            if (EnemyManagerScript.firstAttack == "Enemy 3-Moves Left" ||
                EnemyManagerScript.firstAttack == "Enemy 3-Moves Right")
            {
                DetermineEnemy(3, 1);
                return "Move";
            }
        }

        if (attackNumber == 2)
        {
            if (EnemyManagerScript.secondAttack == "Enemy 1-Moves Left" ||
                EnemyManagerScript.secondAttack == "Enemy 1-Moves Right")
            {
                DetermineEnemy(1, 2);
                return "Move";
            }

            if (EnemyManagerScript.secondAttack == "Enemy 2-Moves Left" ||
                EnemyManagerScript.secondAttack == "Enemy 2-Moves Right")
            {
                DetermineEnemy(2, 2);
                return "Move";
            }

            if (EnemyManagerScript.secondAttack == "Enemy 3-Moves Left" ||
                EnemyManagerScript.secondAttack == "Enemy 3-Moves Right")
            {
                DetermineEnemy(3, 2);
                return "Move";
            }
        }

        return "Didn't Move";
    }


    void DetermineEnemy(int enemyNumber, int attackNumber)
    {
        string enemyName;
        
        switch (enemyNumber)
        {
            case 1:
                enemyName = EnemyManagerScript.enemy1;
                
                if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter(enemyName, enemySR1);
                else ChangeSprites.S.ChangeEnemyCharacter(enemyName, enemySR2);
                break;
            
            case 2:
                enemyName = EnemyManagerScript.enemy2;
                
                if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter(enemyName, enemySR1);
                else ChangeSprites.S.ChangeEnemyCharacter(enemyName, enemySR2);
                break;
            
            case 3:
                enemyName = EnemyManagerScript.enemy3;
                
                if (attackNumber == 1) ChangeSprites.S.ChangeEnemyCharacter(enemyName, enemySR1);
                else ChangeSprites.S.ChangeEnemyCharacter(enemyName, enemySR2);
                break;
        }
    }
}
