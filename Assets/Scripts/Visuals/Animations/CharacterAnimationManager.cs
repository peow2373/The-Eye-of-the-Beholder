using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    private GameObject netrixiAnimation, folkvarAnimation, ivAnimation;
    private GameObject enemy1Animation, enemy2Animation, enemy3Animation;

    public static CharacterAnimationManager S;
    
    public Sprite[] netrixi = new Sprite[6];
    public Sprite[] folkvar = new Sprite[5];
    public Sprite[] iv = new Sprite[6];

    public Sprite pirate;
    public Sprite folkvarEnemy;
    public Sprite barkeeper;
    public Sprite jester;
    public Sprite royalKing;
    public Sprite skullKing;
    
    public Sprite[] knightMelee = new Sprite[2];
    public Sprite[] knightRanged = new Sprite[2];
    public Sprite[] royalGuard = new Sprite[2];
    public Sprite[] skullMelee = new Sprite[2];
    public Sprite[] skullRanged = new Sprite[2];
    public Sprite[] brute = new Sprite[2];
    public Sprite[] gatekeeper = new Sprite[2];
    public Sprite[] bo = new Sprite[2];
    
    public Sprite[] kaz = new Sprite[3];
    public Sprite[] evilKing = new Sprite[2];

    public Sprite[] frog = new Sprite[3];
    public Sprite[] cat = new Sprite[3];
    public Sprite[] dog = new Sprite[3];
    
    public static bool hasChangedCombatSprite;

    // Start is called before the first frame update
    void Awake()
    {
        S = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManagerScript.inCombat)
        {
            StartCoroutine(PlayAnimations(0));
            hasChangedCombatSprite = false;
        }
        else
        {
            if (hasChangedCombatSprite)
            {
                // Continue updating at a regular pace
                StartCoroutine(PlayAnimations(0));
            }
            else
            {
                // Wait while the combat scene loads in
                StartCoroutine(PlayAnimations(1));
            }
        }
    }

    private IEnumerator PlayAnimations(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        
        // Determine which characters are in the scene
        netrixiAnimation = GameObject.FindGameObjectWithTag("Netrixi");
        folkvarAnimation = GameObject.FindGameObjectWithTag("Folkvar");
        ivAnimation = GameObject.FindGameObjectWithTag("Iv");
        
        enemy1Animation = GameObject.FindGameObjectWithTag("Enemy 1");
        enemy2Animation = GameObject.FindGameObjectWithTag("Enemy 2");
        enemy3Animation = GameObject.FindGameObjectWithTag("Enemy 3");
        
        // Change the animations of the main characters
        if (netrixiAnimation != null) DetermineAnimation(netrixiAnimation, "Netrixi");
        if (folkvarAnimation != null) DetermineAnimation(folkvarAnimation, "Folkvar");
        if (ivAnimation != null) DetermineAnimation(ivAnimation, "Iv");

        // Change the animations of the enemies
        if (enemy1Animation != null) DetermineAnimation(enemy1Animation, "Enemy 1");
        if (enemy2Animation != null) DetermineAnimation(enemy2Animation, "Enemy 2");
        if (enemy3Animation != null) DetermineAnimation(enemy3Animation, "Enemy 3");
        
        hasChangedCombatSprite = true;
    }

    public void DetermineAnimation(GameObject character, string name)
    {
        SpriteRenderer sr = character.GetComponent<SpriteRenderer>();
        Sprite currSprite = sr.sprite;

        switch (name)
        {
            case "Netrixi":
                for (int i = 0; i < netrixi.Length; i++)
                {
                    // Check which sprite Netrixi currently has
                    if (currSprite == netrixi[i]) ChangeCharacterAnimations.S1.ChangeCharacter(i, "Netrixi");
                }
                break;
            
            
            case "Folkvar":
                for (int i = 0; i < folkvar.Length; i++)
                {
                    // Check which sprite Folkvar currently has
                    if (currSprite == folkvar[i]) ChangeCharacterAnimations.S2.ChangeCharacter(i, "Folkvar");
                }
                break;
            
            
            case "Iv":
                for (int i = 0; i < iv.Length; i++)
                {
                    // Check which sprite Iv currently has
                    if (currSprite == iv[i]) ChangeCharacterAnimations.S3.ChangeCharacter(i, "Iv");
                }
                break;
            
                
            case "Enemy 1":
                if (currSprite == pirate) ChangeCharacterAnimations.S4.ChangeCharacter(0, "Pirate");
                if (currSprite == folkvarEnemy) ChangeCharacterAnimations.S4.ChangeCharacter(0, "Folkvar Enemy");

                for (int i = 0; i < 2; i++)
                {
                    if (currSprite == knightMelee[i]) ChangeCharacterAnimations.S4.ChangeCharacter(i, "Knight Melee");
                    if (currSprite == skullMelee[i]) ChangeCharacterAnimations.S4.ChangeCharacter(i, "Skull Melee");
                    if (currSprite == brute[i]) ChangeCharacterAnimations.S4.ChangeCharacter(i, "Brute");
                    if (currSprite == royalGuard[i]) ChangeCharacterAnimations.S4.ChangeCharacter(i, "Royal Guard");
                }
                
                // If enemy was transformed into an animal
                if (currSprite == frog[0]) ChangeCharacterAnimations.S4.ChangeCharacter(0, "Frog");
                if (currSprite == cat[0]) ChangeCharacterAnimations.S4.ChangeCharacter(0, "Cat");
                if (currSprite == dog[0]) ChangeCharacterAnimations.S4.ChangeCharacter(0, "Dog");
                break;
            
            
            case "Enemy 2":
                if (currSprite == barkeeper) ChangeCharacterAnimations.S5.ChangeCharacter(0, "Barkeeper");
                if (currSprite == jester) ChangeCharacterAnimations.S5.ChangeCharacter(0, "Jester");

                for (int i = 0; i < 2; i++)
                {
                    if (currSprite == knightMelee[i]) ChangeCharacterAnimations.S5.ChangeCharacter(i, "Knight Melee");
                    if (currSprite == knightRanged[i]) ChangeCharacterAnimations.S5.ChangeCharacter(i, "Knight Ranged");
                    if (currSprite == skullRanged[i]) ChangeCharacterAnimations.S5.ChangeCharacter(i, "Skull Ranged");
                    if (currSprite == royalGuard[i]) ChangeCharacterAnimations.S5.ChangeCharacter(i, "Royal Guard");
                    if (currSprite == bo[i]) ChangeCharacterAnimations.S5.ChangeCharacter(i, "Bo");
                }
                
                // If enemy was transformed into an animal
                if (currSprite == frog[0]) ChangeCharacterAnimations.S5.ChangeCharacter(0, "Frog");
                if (currSprite == cat[0]) ChangeCharacterAnimations.S5.ChangeCharacter(0, "Cat");
                if (currSprite == dog[0]) ChangeCharacterAnimations.S5.ChangeCharacter(0, "Dog");
                break;
            
            
            case "Enemy 3":
                if (currSprite == royalKing) ChangeCharacterAnimations.S6.ChangeCharacter(0, "Royal King");
                if (currSprite == skullKing) ChangeCharacterAnimations.S6.ChangeCharacter(0, "Skull King");

                for (int i = 0; i < 2; i++)
                {
                    if (currSprite == gatekeeper[i]) ChangeCharacterAnimations.S6.ChangeCharacter(i, "Gatekeeper");
                    if (currSprite == evilKing[i]) ChangeCharacterAnimations.S6.ChangeCharacter(i, "Evil King");
                }
                
                for (int i = 0; i < 3; i++)
                {
                    if (currSprite == kaz[i]) ChangeCharacterAnimations.S6.ChangeCharacter(i, "Kaz");
                    
                    // If enemy was transformed into an animal
                    if (currSprite == frog[i]) ChangeCharacterAnimations.S6.ChangeCharacter(i, "Frog");
                    if (currSprite == cat[i]) ChangeCharacterAnimations.S6.ChangeCharacter(i, "Cat");
                    if (currSprite == dog[i]) ChangeCharacterAnimations.S6.ChangeCharacter(i, "Dog");
                }
                break;
        }
    }
}
