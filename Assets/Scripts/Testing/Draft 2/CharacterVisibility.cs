using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class CharacterVisibility : MonoBehaviour
{
    public bool netrixi, folkvar, iv;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (netrixi)
        {
            if (CombatManagerScript.netrixiAttacks)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.gray;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }

        if (folkvar)
        {
            if (CombatManagerScript.folkvarAttacks)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.gray;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }

        if (iv)
        {
            if (CombatManagerScript.ivAttacks)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.gray;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }
    }
}
