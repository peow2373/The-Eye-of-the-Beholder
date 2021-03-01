using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    public bool netrixi, folkvar, iv;

    public Text netrixiHP, folkvarHP, ivHP;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (netrixi)
        {
            if (CombatManagerScript.netrixiAlive)
            {
                netrixiHP.text = "Hp:" + CombatManagerScript.netrixiHP;
            
                Vector3 locationBefore = Camera.main.WorldToScreenPoint(this.transform.position);
                Vector3 locationAfter = new Vector3(locationBefore.x, locationBefore.y + 150f, locationBefore.z);
                netrixiHP.transform.position = locationAfter;
            }
            else
            {
                netrixiHP.transform.position = new Vector3(-1000, -1000, 0);
            }
        }
        
        if (folkvar)
        {
            if (CombatManagerScript.folkvarAlive && GameManagerScript.folkvarInParty)
            {
                folkvarHP.text = "Hp:" + CombatManagerScript.folkvarHP;
            
                Vector3 locationBefore = Camera.main.WorldToScreenPoint(this.transform.position);
                Vector3 locationAfter = new Vector3(locationBefore.x, locationBefore.y + 200f, locationBefore.z);
                folkvarHP.transform.position = locationAfter;
            }
            else
            {
                folkvarHP.transform.position = new Vector3(-1000, -1000, 0);
            }
        }
        
        if (iv)
        {
            if (CombatManagerScript.ivAlive && GameManagerScript.ivInParty)
            {
                ivHP.text = "Hp:" + CombatManagerScript.ivHP;
            
                Vector3 locationBefore = Camera.main.WorldToScreenPoint(this.transform.position);
                Vector3 locationAfter = new Vector3(locationBefore.x, locationBefore.y + 150f, locationBefore.z);
                ivHP.transform.position = locationAfter;
            }
            else
            {
                ivHP.transform.position = new Vector3(-1000, -1000, 0);
            }
        }
    }
}
