using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteScript : MonoBehaviour
{
    public bool enemy1, enemy2, enemy3;
    
    private SpriteRenderer sr;
    
    public Sprite knightMelee, knightRanged, gatekeeper, royalGuard, folkvar;
    public Sprite skullMelee, skullRanged, kaz1, kaz2, skullKing, evilKing;
    public Sprite pirate, brute, barkeeper;
    
    private Vector3 size1 = new Vector3(9f, 9f, 9f);
    private Vector3 size2 = new Vector3(10f, 10f, 10f);
    private Vector3 size3 = new Vector3(11f, 11f, 11f);
    private Vector3 size4 = new Vector3(12f, 12f, 12f);
    private Vector3 size5 = new Vector3(13f, 13f, 13f);
    private Vector3 size6 = new Vector3(14f, 14f, 14f);

    private float size1Offset = CharacterMovement.size1Offset;
    private float size2Offset = CharacterMovement.size2Offset;
    private float size3Offset = CharacterMovement.size3Offset;
    private float size4Offset = CharacterMovement.size4Offset;
    private float size5Offset = CharacterMovement.size5Offset;
    private float size6Offset = CharacterMovement.size6Offset;
    
    private float upperPosition;
    private float middlePosition;
    private float lowerPosition;

    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        upperPosition = CharacterMovement.upperYLocation;
        middlePosition = CharacterMovement.middleYLocation;
        lowerPosition = CharacterMovement.lowerYLocation;
        
        if (enemy1) if (CombatManagerScript.canEnemy1Attack) ChangeSprite(EnemyManagerScript.enemy1);
        if (enemy2) if (CombatManagerScript.canEnemy2Attack) ChangeSprite(EnemyManagerScript.enemy2);
        if (enemy3) if (CombatManagerScript.canEnemy3Attack) ChangeSprite(EnemyManagerScript.enemy3);
    }
    
    void ChangeSprite(string enemyName)
    {
        switch (enemyName)
        {
            case "Pirate":
                ResizeCharacter(2);
                sr.sprite = pirate;
                break;
                    
            
            
            case "Folkvar":
                ResizeCharacter(3);
                sr.sprite = folkvar;
                break;
                    
            case "Royal Knight Melee":
                ResizeCharacter(2);
                sr.sprite = knightMelee;
                break;
                    
            case "Royal Knight Ranged":
                ResizeCharacter(1);
                sr.sprite = knightRanged;
                break;
            
            
            
            case "Skull Grunt Melee":
                ResizeCharacter(3);
                sr.sprite = skullMelee;
                break;
            
            case "Skull Grunt Ranged":
                ResizeCharacter(2);
                sr.sprite = skullRanged;
                break;
            
            case "Kaz 1":
                ResizeCharacter(4);
                sr.sprite = kaz1;
                break;
            
            case "Kaz 2":
                ResizeCharacter(4);
                sr.sprite = kaz2;
                break;
            
            
            
            case "Tavern Brute":
                ResizeCharacter(6);
                sr.sprite = brute;
                break;
            
            case "Barkeeper":
                ResizeCharacter(3);
                sr.sprite = barkeeper;
                break;
            
            
            
            case "Gatekeeper":
                ResizeCharacter(3);
                sr.sprite = gatekeeper;
                break;
            
            case "Royal Guard 1":
                ResizeCharacter(3);
                sr.sprite = royalGuard;
                break;
            
            case "Royal Guard 2":
                ResizeCharacter(3);
                sr.sprite = royalGuard;
                break;
            
            
            
            case "Skull King":
                ResizeCharacter(5);
                sr.sprite = skullKing;
                break;
            
            case "Royal King":
                ResizeCharacter(5);
                sr.sprite = evilKing;
                break;
        }
    }

    void ResizeCharacter(int size)
    {
        float enemyPosition = 0;

        if (enemy1)
        {
            if (EnemyManagerScript.enemy1Position == 7) enemyPosition = lowerPosition;
            else if (EnemyManagerScript.enemy1Position == 9) enemyPosition = upperPosition;
            else enemyPosition = middlePosition;
        }
        
        if (enemy2)
        {
            if (EnemyManagerScript.enemy2Position == 7) enemyPosition = lowerPosition;
            else if (EnemyManagerScript.enemy2Position == 9) enemyPosition = upperPosition;
            else enemyPosition = middlePosition;
        }
        
        if (enemy3)
        {
            if (EnemyManagerScript.enemy3Position == 7) enemyPosition = lowerPosition;
            else if (EnemyManagerScript.enemy3Position == 9) enemyPosition = upperPosition;
            else enemyPosition = middlePosition;
        }
        
        
        switch (size)
        {
            // Smallest size
            case 1:
                this.transform.localScale = size1;
                transform.position = new Vector3(transform.position.x, enemyPosition + size1Offset, transform.position.z);
                
                if (enemy1) ChangeHealth.sizeOfEnemy1 = 1;
                if (enemy2) ChangeHealth.sizeOfEnemy2 = 1;
                if (enemy3) ChangeHealth.sizeOfEnemy3 = 1;
                break;
            
            case 2: 
                this.transform.localScale = size2;
                transform.position = new Vector3(transform.position.x, enemyPosition + size2Offset, transform.position.z);
                
                if (enemy1) ChangeHealth.sizeOfEnemy1 = 2;
                if (enemy2) ChangeHealth.sizeOfEnemy2 = 2;
                if (enemy3) ChangeHealth.sizeOfEnemy3 = 2;
                break;
            
            case 3: 
                this.transform.localScale = size3;
                transform.position = new Vector3(transform.position.x, enemyPosition + size3Offset, transform.position.z);
                
                if (enemy1) ChangeHealth.sizeOfEnemy1 = 3;
                if (enemy2) ChangeHealth.sizeOfEnemy2 = 3;
                if (enemy3) ChangeHealth.sizeOfEnemy3 = 3;
                break;
            
            case 4: 
                this.transform.localScale = size4;
                transform.position = new Vector3(transform.position.x, enemyPosition + size4Offset, transform.position.z);
                
                if (enemy1) ChangeHealth.sizeOfEnemy1 = 4;
                if (enemy2) ChangeHealth.sizeOfEnemy2 = 4;
                if (enemy3) ChangeHealth.sizeOfEnemy3 = 4;
                break;
            
            case 5: 
                this.transform.localScale = size5;
                transform.position = new Vector3(transform.position.x, enemyPosition + size5Offset, transform.position.z);
                
                if (enemy1) ChangeHealth.sizeOfEnemy1 = 5;
                if (enemy2) ChangeHealth.sizeOfEnemy2 = 5;
                if (enemy3) ChangeHealth.sizeOfEnemy3 = 5;
                break;
            
            // Largest size
            case 6: 
                this.transform.localScale = size6;
                transform.position = new Vector3(transform.position.x, enemyPosition + size6Offset, transform.position.z);
                
                if (enemy1) ChangeHealth.sizeOfEnemy1 = 6;
                if (enemy2) ChangeHealth.sizeOfEnemy2 = 6;
                if (enemy3) ChangeHealth.sizeOfEnemy3 = 6;
                break;
        }
    }
}
