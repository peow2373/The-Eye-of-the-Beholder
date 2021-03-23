using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprites : MonoBehaviour
{
    public static ChangeSprites S;
    
    public GameObject netrixi, folkvar, iv;
    public GameObject enemy1, enemy2, enemy3;

    private SpriteRenderer sr1, sr2, sr3;

    public Sprite netrixiHood, netrixiFace, netrixiFaceCompass, netrixiWeapon, netrixiWeaponCompass;
    public Sprite folkvarHelmet, folkvarFace, folkvarWeapon, folkvarTwoWeapons;
    public Sprite ivEyesClosed, ivEyesClosedAmulet, ivEyesOpen, ivWeapon, ivWeaponAmulet, ivWeaponEyesOpen;
    
    public Sprite knightMelee, knightRanged, gatekeeper, royalGuard;
    public Sprite skullMelee, skullRanged, kazHood, kazNoHood, skullKing, royalKing;
    public Sprite pirate, brute, barkeeper;

    void Awake()
    {
        S = this;
    }
    
    // Update is called once per frame
    void Update()
    {
        // Update Netrixi sprite
        SpriteRenderer sr1 = netrixi.GetComponent<SpriteRenderer>();
        sr1.sprite = DisplayPlayerCharacter(1, true);
        
        // Update Folkvar sprite
        SpriteRenderer sr2 = folkvar.GetComponent<SpriteRenderer>();
        sr2.sprite = DisplayPlayerCharacter(4, true);
        
        // Update Iv sprite
        SpriteRenderer sr3 = iv.GetComponent<SpriteRenderer>();
        sr3.sprite = DisplayPlayerCharacter(7, true);
    }

    public Sprite DisplayPlayerCharacter(int attack, bool hasWeapon)
    {
        int currScene = GameManagerScript.currentScene;
        
        switch (attack)
        {
            // If Netrixi is attacking
            case 1:
            case 2:
            case 3:
            case 10:
            case 11:
                if (currScene > 2 && currScene <= 7)
                {
                    if (hasWeapon) return netrixiWeaponCompass;
                    else return netrixiFaceCompass;
                } 
                else if (currScene > 7 && currScene <= 15)
                {
                    if (hasWeapon) return netrixiWeapon;
                    else return netrixiFace;
                } 
                else if (currScene > 15)
                {
                    if (hasWeapon) return netrixiWeaponCompass;
                    else return netrixiFaceCompass;
                }
                else
                {
                    if (hasWeapon) return netrixiWeapon;
                    else return netrixiHood;
                }

            // If Folkvar is attacking
            case 4:
            case 5:
            case 6:
            case 12:
            case 13:
                if (currScene > 6 && currScene <= 15)
                {
                    if (hasWeapon) return folkvarTwoWeapons;
                    else return folkvarFace;
                }
                else if (currScene > 15)
                {
                    if (hasWeapon) return folkvarTwoWeapons;
                    else return folkvarFace;
                }
                else
                {
                    if (hasWeapon) return folkvarWeapon;
                    else return folkvarHelmet;
                }
            
            // If Iv is attacking
            case 7:
            case 8:
            case 9:
            case 14:
            case 15:
                if (currScene > 15 && currScene < 24)
                {
                    if (hasWeapon) return ivWeaponAmulet;
                    else return ivEyesClosedAmulet;
                }
                else if (currScene >= 25)
                {
                    if (hasWeapon) return ivWeaponEyesOpen;
                    else return ivEyesOpen;
                }
                else
                {
                    if (hasWeapon) return ivWeapon;
                    else return ivEyesClosed;
                }
        }

        return null;
    }


    public void ChangeEnemyCharacter(string enemy, SpriteRenderer sr)
    {
        switch (enemy)
        {
            case "Royal Knight Melee":
                sr.sprite = knightMelee;
                break;
            
            case "Royal Knight Ranged":
                sr.sprite = knightRanged;
                break;
            
            case "Folkvar":
                sr.sprite = folkvarWeapon;
                break;
            
            case "Gatekeeper":
                sr.sprite = gatekeeper;
                break;
            
            case "Royal Guard 1":
            case "Royal Guard 2":
                sr.sprite = royalGuard;
                break;
            
            
            case "Skull Grunt Melee":
                sr.sprite = skullMelee;
                break;
            
            case "Skull Grunt Ranged":
                sr.sprite = skullRanged;
                break;
            
            case "Kaz 1":
                sr.sprite = kazHood;
                break;
            
            case "Kaz 2":
                sr.sprite = kazNoHood;
                break;
            
            case "Skull King":
                sr.sprite =  skullKing;
                break;
            
            case "Royal King":
                sr.sprite = royalKing;
                break;
            
            
            case "Pirate":
                sr.sprite = pirate;
                break;
            
            case "Tavern Brute":
                sr.sprite = brute;
                break;
            
            case "Barkeeper":
                sr.sprite = barkeeper;
                break;
        }
    }
}
