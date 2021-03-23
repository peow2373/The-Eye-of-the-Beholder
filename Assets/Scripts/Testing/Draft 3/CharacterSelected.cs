using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterSelected : MonoBehaviour
{
    public bool netrixi, folkvar, iv;

    private Vector3 selected, deselected;

    private static float deselectedSize = 9f;
    private static float selectedSize = deselectedSize + 4f;

    // Start is called before the first frame update
    void Start()
    {
        selected = new Vector3(selectedSize, selectedSize, selectedSize);
        deselected = new Vector3(deselectedSize, deselectedSize, deselectedSize);
    }

    // Update is called once per frame
    void Update()
    {
        float selectedLocation = CharacterMovement.selectedPosition;

        float combatWidth = CharacterMovement.combatWidth;
        float combatHeight = CharacterMovement.combatHeight;

        float netrixiOffset = CharacterMovement.netrixiOffset;
        float folkvarOffset = CharacterMovement.folkvarOffset;
        float ivOffset = CharacterMovement.ivOffset;
        
        if (netrixi)
        {
            // If Netrixi is in the party yet
            if (GameManagerScript.netrixiInParty)
            {
                // If Netrixi is alive
                if (CombatManagerScript.netrixiAlive)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                
                    if (CombatManagerScript.netrixiAttacks)
                    {
                        float yLoc = CharacterMovement.DetermineYLocation(CharacterManagerScript.netrixiPosition) + (netrixiOffset*combatHeight) + (selectedLocation*combatHeight);
                        
                        this.transform.localScale = new Vector3(selectedSize, selectedSize, selectedSize);
                        this.transform.position = new Vector3( this.transform.position.x, yLoc, this.transform.position.z);
                    }
                    else
                    {
                        float yLoc = CharacterMovement.DetermineYLocation(CharacterManagerScript.netrixiPosition) + (netrixiOffset*combatHeight);
                        
                        this.transform.localScale = new Vector3(deselectedSize + 0.5f, deselectedSize + 0.5f, deselectedSize);
                        this.transform.position = new Vector3( this.transform.position.x, yLoc, this.transform.position.z);
                    }
                }
                else
                {
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                }
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }

        
        
        if (folkvar)
        {
            // If Folkvar is in the party yet
            if (GameManagerScript.folkvarInParty)
            {
                // If Folkvar is alive
                if (CombatManagerScript.folkvarAlive)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                
                    if (CombatManagerScript.folkvarAttacks)
                    {
                        float yLoc = CharacterMovement.DetermineYLocation(CharacterManagerScript.folkvarPosition) + (folkvarOffset*combatHeight) + (selectedLocation*combatHeight);
                        
                        this.transform.localScale = new Vector3(selectedSize, selectedSize, selectedSize);
                        this.transform.position = new Vector3( this.transform.position.x, yLoc, this.transform.position.z);
                    }
                    else
                    {
                        float yLoc = CharacterMovement.DetermineYLocation(CharacterManagerScript.folkvarPosition) + (folkvarOffset*combatHeight);
                        
                        this.transform.localScale = new Vector3(deselectedSize + 2f, deselectedSize + 2f, deselectedSize);
                        this.transform.position = new Vector3( this.transform.position.x, yLoc, this.transform.position.z);
                    }
                }
                else
                {
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                }
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }

        
        
        if (iv)
        {
            // If Iv is in the party yet
            if (GameManagerScript.ivInParty)
            {
                // If Iv is alive
                if (CombatManagerScript.ivAlive)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                
                    if (CombatManagerScript.ivAttacks)
                    {
                        float yLoc = CharacterMovement.DetermineYLocation(CharacterManagerScript.ivPosition) + (ivOffset*combatHeight) + (selectedLocation*combatHeight);
                        
                        this.transform.localScale = selected;
                        this.transform.position = new Vector3( this.transform.position.x, yLoc, this.transform.position.z);
                    }
                    else
                    {
                        float yLoc = CharacterMovement.DetermineYLocation(CharacterManagerScript.ivPosition) + (ivOffset*combatHeight);
                        
                        this.transform.localScale = deselected;
                        this.transform.position = new Vector3( this.transform.position.x, yLoc, this.transform.position.z);
                    }
                }
                else
                {
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                }
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }
    }
}
