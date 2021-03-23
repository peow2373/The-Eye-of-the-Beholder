using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackText : MonoBehaviour
{

    public bool attack1, attack2, rounds;
    private int roundNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Change color if first attack is selected
        if (CombatManagerScript.firstAttack != 0)
        {
            if (attack1)
            {
                this.gameObject.GetComponent<Text>().color = Color.gray;
                
                // If Netrixi moves
                if (CombatManagerScript.firstAttack == 10) this.gameObject.GetComponent<Text>().text = "{-Go";
                if (CombatManagerScript.firstAttack == 11) this.gameObject.GetComponent<Text>().text = "Go-}";
                
                // If Folkvar moves
                if (CombatManagerScript.firstAttack == 12) this.gameObject.GetComponent<Text>().text = "{-Go";
                if (CombatManagerScript.firstAttack == 13) this.gameObject.GetComponent<Text>().text = "Go-}";
                
                // If Iv moves
                if (CombatManagerScript.firstAttack == 14) this.gameObject.GetComponent<Text>().text = "{-Go";
                if (CombatManagerScript.firstAttack == 15) this.gameObject.GetComponent<Text>().text = "Go-}";
            }
        }
        else
        {
            if (attack1)
            {
                this.gameObject.GetComponent<Text>().color = Color.white;
                this.gameObject.GetComponent<Text>().text = "#1";
            }
        }
        
        // Change color if second attack is selected
        if (CombatManagerScript.secondAttack != 0)
        {
            if (attack2)
            {
                this.gameObject.GetComponent< Text>().color = Color.gray;
                
                // If Netrixi moves
                if (CombatManagerScript.secondAttack == 10) this.gameObject.GetComponent<Text>().text = "{-Go";
                if (CombatManagerScript.secondAttack == 11) this.gameObject.GetComponent<Text>().text = "Go-}";
                
                // If Folkvar moves
                if (CombatManagerScript.secondAttack == 12) this.gameObject.GetComponent<Text>().text = "{-Go";
                if (CombatManagerScript.secondAttack == 13) this.gameObject.GetComponent<Text>().text = "Go-}";
                
                // If Iv moves
                if (CombatManagerScript.secondAttack == 14) this.gameObject.GetComponent<Text>().text = "{-Go";
                if (CombatManagerScript.secondAttack == 15) this.gameObject.GetComponent<Text>().text = "Go-}";
            }
        }
        else
        {
            if (attack2)
            {
                this.gameObject.GetComponent<Text>().color = Color.white;
                this.gameObject.GetComponent<Text>().text = "#2";
            }
        }
        
        // Change round number
        if (rounds)
        {
            roundNumber = CombatManagerScript.roundNumber;
            
            this.gameObject.GetComponent<Text>().text = "Round #" + roundNumber;
        }
    }
}
