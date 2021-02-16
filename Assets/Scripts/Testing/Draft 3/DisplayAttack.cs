using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAttack : MonoBehaviour
{
    public bool playerText1, playerText2;
    
    public Text playerAttack1, playerAttack2;
    public Text playerChoice1, playerChoice2;
    
    public Sprite netrixi, folkvar, iv;
    private SpriteRenderer sr;
    
    
    public bool enemyAttack1, enemyAttack2;

    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Display the player's first choice
        if (playerText1)
        {
            PlayerChoice1();
        }
        
        // Display the player's second choice
        if (playerText2)
        {
            PlayerChoice2();
        }
    }

    void PlayerChoice1()
    {
        // If there is no attack selected yet
        if (CombatManagerScript.firstAttack == 0)
        {
            playerChoice1.text = "";
            playerAttack1.color = Color.gray;
            sr.enabled = false;
        }
        
        
        
        // If player has chosen Netrixi's Fireball spell
        if (CombatManagerScript.firstAttack == 1)
        {
            playerChoice1.text = "Fireball";
            playerAttack1.color = Color.white;
            sr.enabled = true;
            sr.sprite = netrixi;
        }
        
        // If player has chosen Netrixi's Lightning spell
        if (CombatManagerScript.firstAttack == 2)
        {
            playerChoice1.text = "Lightning";
            playerAttack1.color = Color.white;
            sr.enabled = true;
            sr.sprite = netrixi;
        }
        
        // If player has chosen Netrixi's Transmutate spell
        if (CombatManagerScript.firstAttack == 3)
        {
            playerChoice1.text = "Transmutate";
            playerAttack1.color = Color.white;
            sr.enabled = true;
            sr.sprite = netrixi;
        }
        
        
        
        // If player has chosen Folkvar's Swing Sword attack
        if (CombatManagerScript.firstAttack == 4)
        {
            playerChoice1.text = "Swing Sword";
            playerAttack1.color = Color.white;
            sr.enabled = true;
            sr.sprite = folkvar;
        }
        
        // If player has chosen Folkvar's Smite attack
        if (CombatManagerScript.firstAttack == 5)
        {
            playerChoice1.text = "Smite";
            playerAttack1.color = Color.white;
            sr.enabled = true;
            sr.sprite = folkvar;
        }
        
        // If player has chosen Folkvar's Grand Slam attack
        if (CombatManagerScript.firstAttack == 6)
        {
            playerChoice1.text = "Grand Slam";
            playerAttack1.color = Color.white;
            sr.enabled = true;
            sr.sprite = folkvar;
        }
        
        
        
        // If player has chosen Iv's block
        if (CombatManagerScript.firstAttack == 7)
        {
            playerChoice1.text = "Block";
            playerAttack1.color = Color.white;
            sr.enabled = true;
            sr.sprite = iv;
        }
        
        // If player has chosen Iv's empower
        if (CombatManagerScript.firstAttack == 8)
        {
            playerChoice1.text = "Empower";
            playerAttack1.color = Color.white;
            sr.enabled = true;
            sr.sprite = iv;
        }
        
        // If player has chosen Iv's heal
        if (CombatManagerScript.firstAttack == 9)
        {
            playerChoice1.text = "Heal";
            playerAttack1.color = Color.white;
            sr.enabled = true;
            sr.sprite = iv;
        }
        
        
        
        // If player has chosen to move left with Netrixi
        if (CombatManagerScript.firstAttack == 10)
        {
            playerChoice1.text = "Move Left";
            playerAttack1.color = Color.white;
            sr.enabled = true;
            sr.sprite = netrixi;
        }
        
        // If player has chosen to move right with Netrixi
        if (CombatManagerScript.firstAttack == 11)
        {
            playerChoice1.text = "Move Right";
            playerAttack1.color = Color.white;
            sr.enabled = true;
            sr.sprite = netrixi;
        }
        
        
        
        // If player has chosen to move left with Folkvar
        if (CombatManagerScript.firstAttack == 12)
        {
            playerChoice1.text = "Move Left";
            playerAttack1.color = Color.white;
            sr.enabled = true;
            sr.sprite = folkvar;
        }
        
        // If player has chosen to move right with Folkvar
        if (CombatManagerScript.firstAttack == 13)
        {
            playerChoice1.text = "Move Right";
            playerAttack1.color = Color.white;
            sr.enabled = true;
            sr.sprite = folkvar;
        }
        
        
        
        // If player has chosen to move left with Iv
        if (CombatManagerScript.firstAttack == 14)
        {
            playerChoice1.text = "Move Left";
            playerAttack1.color = Color.white;
            sr.enabled = true;
            sr.sprite = iv;
        }
        
        // If player has chosen to move right with Iv
        if (CombatManagerScript.firstAttack == 15)
        {
            playerChoice1.text = "Move Right";
            playerAttack1.color = Color.white;
            sr.enabled = true;
            sr.sprite = iv;
        }
    }
    
    
    
    
    void PlayerChoice2()
    {
        // If there is no attack selected yet
        if (CombatManagerScript.secondAttack == 0)
        {
            playerChoice2.text = "";
            playerAttack2.color = Color.gray;
            sr.enabled = false;
        }
        
        
        
        // If player has chosen Netrixi's Fireball spell
        if (CombatManagerScript.secondAttack == 1)
        {
            playerChoice2.text = "Fireball";
            playerAttack2.color = Color.white;
            sr.enabled = true;
            sr.sprite = netrixi;
        }
        
        // If player has chosen Netrixi's Lightning spell
        if (CombatManagerScript.secondAttack == 2)
        {
            playerChoice2.text = "Lightning";
            playerAttack2.color = Color.white;
            sr.enabled = true;
            sr.sprite = netrixi;
        }
        
        // If player has chosen Netrixi's Transmutate spell
        if (CombatManagerScript.secondAttack == 3)
        {
            playerChoice2.text = "Transmutate";
            playerAttack2.color = Color.white;
            sr.enabled = true;
            sr.sprite = netrixi;
        }
        
        
        
        // If player has chosen Folkvar's Swing Sword attack
        if (CombatManagerScript.secondAttack == 4)
        {
            playerChoice2.text = "Swing Sword";
            playerAttack2.color = Color.white;
            sr.enabled = true;
            sr.sprite = folkvar;
        }
        
        // If player has chosen Folkvar's Smite attack
        if (CombatManagerScript.secondAttack == 5)
        {
            playerChoice2.text = "Smite";
            playerAttack2.color = Color.white;
            sr.enabled = true;
            sr.sprite = folkvar;
        }
        
        // If player has chosen Folkvar's Grand Slam attack
        if (CombatManagerScript.secondAttack == 6)
        {
            playerChoice2.text = "Grand Slam";
            playerAttack2.color = Color.white;
            sr.enabled = true;
            sr.sprite = folkvar;
        }
        
        
        
        // If player has chosen Iv's block
        if (CombatManagerScript.secondAttack == 7)
        {
            playerChoice2.text = "Block";
            playerAttack2.color = Color.white;
            sr.enabled = true;
            sr.sprite = iv;
        }
        
        // If player has chosen Iv's empower
        if (CombatManagerScript.secondAttack == 8)
        {
            playerChoice2.text = "Empower";
            playerAttack2.color = Color.white;
            sr.enabled = true;
            sr.sprite = iv;
        }
        
        // If player has chosen Iv's heal
        if (CombatManagerScript.secondAttack == 9)
        {
            playerChoice2.text = "Heal";
            playerAttack2.color = Color.white;
            sr.enabled = true;
            sr.sprite = iv;
        }
        
        
        
        // If player has chosen to move left with Netrixi
        if (CombatManagerScript.secondAttack == 10)
        {
            playerChoice2.text = "Move Left";
            playerAttack2.color = Color.white;
            sr.enabled = true;
            sr.sprite = netrixi;
        }
        
        // If player has chosen to move right with Netrixi
        if (CombatManagerScript.secondAttack == 11)
        {
            playerChoice2.text = "Move Right";
            playerAttack2.color = Color.white;
            sr.enabled = true;
            sr.sprite = netrixi;
        }
        
        
        
        // If player has chosen to move left with Folkvar
        if (CombatManagerScript.secondAttack == 12)
        {
            playerChoice2.text = "Move Left";
            playerAttack2.color = Color.white;
            sr.enabled = true;
            sr.sprite = folkvar;
        }
        
        // If player has chosen to move right with Folkvar
        if (CombatManagerScript.secondAttack == 13)
        {
            playerChoice2.text = "Move Right";
            playerAttack2.color = Color.white;
            sr.enabled = true;
            sr.sprite = folkvar;
        }
        
        
        
        // If player has chosen to move left with Iv
        if (CombatManagerScript.secondAttack == 14)
        {
            playerChoice2.text = "Move Left";
            playerAttack2.color = Color.white;
            sr.enabled = true;
            sr.sprite = iv;
        }
        
        // If player has chosen to move right with Iv
        if (CombatManagerScript.secondAttack == 15)
        {
            playerChoice2.text = "Move Right";
            playerAttack2.color = Color.white;
            sr.enabled = true;
            sr.sprite = iv;
        }
    }
}
