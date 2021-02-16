using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelected : MonoBehaviour
{
    public bool netrixi, folkvar, iv;
    
    private Vector3 deselectedSize = new Vector3(1.75f, 1.75f, 1.75f);
    private Vector3 selectedSize = new Vector3(2.5f, 2.5f, 2.5f);

    private float deselectedPosition = -2.95f;
    private float selectedPosition = -0.5f;
    
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
                this.transform.localScale = selectedSize;
                this.transform.position = new Vector3( this.transform.position.x, selectedPosition, this.transform.position.z);
            }
            else
            {
                this.transform.localScale = deselectedSize;
                this.transform.position = new Vector3( this.transform.position.x, deselectedPosition + 0.2f, this.transform.position.z);
            }
        }

        if (folkvar)
        {
            if (CombatManagerScript.folkvarAttacks)
            {
                this.transform.localScale = selectedSize;
                this.transform.position = new Vector3( this.transform.position.x, selectedPosition, this.transform.position.z);
            }
            else
            {
                this.transform.localScale = new Vector3(deselectedSize.x + 0.25f, deselectedSize.y + 0.25f, deselectedSize.z);
                this.transform.position = new Vector3( this.transform.position.x, deselectedPosition + 0.35f, this.transform.position.z);
            }
        }

        if (iv)
        {
            if (CombatManagerScript.ivAttacks)
            {
                this.transform.localScale = selectedSize;
                this.transform.position = new Vector3( this.transform.position.x, selectedPosition, this.transform.position.z);
            }
            else
            {
                this.transform.localScale = deselectedSize;
                this.transform.position = new Vector3( this.transform.position.x, deselectedPosition + 0.2f, this.transform.position.z);
            }
        }
    }
}
