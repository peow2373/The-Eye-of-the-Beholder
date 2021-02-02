using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackText : MonoBehaviour
{

    public bool attack1, attack2;
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
            }
        }
        else
        {
            if (attack1)
            {
                this.gameObject.GetComponent<Text>().color = Color.white;
            }
        }
        
        // Change color if second attack is selected
        if (CombatManagerScript.secondAttack != 0)
        {
            if (attack2)
            {
                this.gameObject.GetComponent<Text>().color = Color.gray;
            }
        }
        else
        {
            if (attack2)
            {
                this.gameObject.GetComponent<Text>().color = Color.white;
            }
        }
    }
}
