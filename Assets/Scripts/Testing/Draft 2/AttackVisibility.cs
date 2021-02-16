using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackVisibility : MonoBehaviour
{
    public bool na1s1, na1s2, na1s3, na2s1, na2s2, na2s3;
    public bool fa1s1, fa1s2, fa1s3, fa2s1, fa2s2, fa2s3;
    public bool ia1s1, ia1s2, ia1s3, ia2s1, ia2s2, ia2s3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NetrixiAttack1SpellCasts();
        NetrixiAttack2SpellCasts();

        FolkvarAttack1Melee();
        FolkvarAttack2Melee();

        IvAttack1ForceWield();
        IvAttack2ForceWield();
    }

    // If Netrixi is attacking first
    void NetrixiAttack1SpellCasts()
    {
        // If Netrixi is casting her first spell
        if (na1s1)
        {
            if (CombatManagerScript.firstAttack == 1)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }  
        
        // If Netrixi is casting her second spell
        if (na1s2)
        {
            if (CombatManagerScript.firstAttack == 2)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }  
        
        // If Netrixi is casting her third spell
        if (na1s3)
        {
            if (CombatManagerScript.firstAttack == 3)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }
    }
    
    // If Netrixi is attacking second
    void NetrixiAttack2SpellCasts()
    {
        // If Netrixi is casting her first spell
        if (na2s1)
        {
            if (CombatManagerScript.secondAttack == 1)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }  
        
        // If Netrixi is casting her second spell
        if (na2s2)
        {
            if (CombatManagerScript.secondAttack == 2)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }  
        
        // If Netrixi is casting her third spell
        if (na2s3)
        {
            if (CombatManagerScript.secondAttack == 3)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }
    }
    
    
    // If Folkvar is attacking first
    void FolkvarAttack1Melee()
    {
        // If Folkvar is using his first melee attack
        if (fa1s1)
        {
            if (CombatManagerScript.firstAttack == 4)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }  
        
        // If Folkvar is using his second melee attack
        if (fa1s2)
        {
            if (CombatManagerScript.firstAttack == 5)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }  
        
        // If Folkvar is using his third melee attack
        if (fa1s3)
        {
            if (CombatManagerScript.firstAttack == 6)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }
    }
    
    // If Folkvar is attacking second
    void FolkvarAttack2Melee()
    {
        // If Folkvar is using his first melee attack
        if (fa2s1)
        {
            if (CombatManagerScript.secondAttack == 4)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }  
        
        // If Folkvar is using his second melee attack
        if (fa2s2)
        {
            if (CombatManagerScript.secondAttack == 5)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }  
        
        // If Folkvar is using his third melee attack
        if (fa2s3)
        {
            if (CombatManagerScript.secondAttack == 6)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }
    }
    
    
    // If Iv is blocking an attack first
    void IvAttack1ForceWield()
    {
        // If Iv is blocking
        if (ia1s1)
        {
            if (CombatManagerScript.firstAttack == 7)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }  
        
        // If Iv is empowering
        if (ia1s2)
        {
            if (CombatManagerScript.firstAttack == 8)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }  
        
        // If Iv is healing
        if (ia1s3)
        {
            if (CombatManagerScript.firstAttack == 9)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }
    }
    
    // If Iv is blocking an attack second
    void IvAttack2ForceWield()
    {
        // If Iv is blocking
        if (ia2s1)
        {
            if (CombatManagerScript.secondAttack == 7)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }  
        
        // If Iv is empowering
        if (ia2s2)
        {
            if (CombatManagerScript.secondAttack == 8)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }  
        
        // If Iv is healing
        if (ia2s3)
        {
            if (CombatManagerScript.secondAttack == 9)
            {
                Color tmp = Color.white;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = Color.red;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }
    }
}
