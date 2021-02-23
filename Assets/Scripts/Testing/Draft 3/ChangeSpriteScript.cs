using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteScript : MonoBehaviour
{
    public bool enemy1, enemy2, enemy3;
    
    private SpriteRenderer sr;
    
    public Sprite knightMelee, knightRanged, gatekeeper, royalGuard, folkvar;
    public Sprite skullMelee, skullRanged, kaz, skullKing, evilKing;
    public Sprite pirate, brute, barkeeper;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy1) ChangeSprite(EnemyManagerScript.enemy1);
        if (enemy2) ChangeSprite(EnemyManagerScript.enemy2);
        if (enemy3) ChangeSprite(EnemyManagerScript.enemy3);
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
                ResizeCharacter(2);
                sr.sprite = skullMelee;
                break;
            
            case "Skull Grunt Ranged":
                ResizeCharacter(1);
                sr.sprite = skullRanged;
                break;
            
            case "Kaz":
                ResizeCharacter(3);
                sr.sprite = kaz;
                break;
            
            
            
            case "Tavern Brute":
                ResizeCharacter(4);
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
                ResizeCharacter(4);
                sr.sprite = skullKing;
                break;
            
            case "Royal King":
                ResizeCharacter(4);
                sr.sprite = evilKing;
                break;
        }
    }

    void ResizeCharacter(int size)
    {
        switch (size)
        {
            // Smallest size
            case 1: 
                this.transform.localScale = new Vector3(1.75f, 1.75f, 1.75f);
                transform.position = new Vector3(transform.position.x, -2.75f, transform.position.z);
                break;
            
            case 2: 
                this.transform.localScale = new Vector3(2f, 2f, 2f);
                transform.position = new Vector3(transform.position.x, -2.6f, transform.position.z);
                break;
            
            case 3: 
                this.transform.localScale = new Vector3(2.25f, 2.25f, 2.25f);
                transform.position = new Vector3(transform.position.x, -2.35f, transform.position.z);
                break;
            
            // Largest size
            case 4: 
                this.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                transform.position = new Vector3(transform.position.x, -2.2f, transform.position.z);
                break;
        }
    }
}
