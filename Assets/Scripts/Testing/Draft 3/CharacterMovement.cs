using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public bool netrixi, folkvar, iv, enemy1, enemy2, enemy3;

    private float[] xPositions = new float[] { -13.95f, -11f, -8f, -5.2f, -2.3f, 2.35f, 5.25f, 8.25f, 11.1f, 14.1f };
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Your characters
        if (netrixi)
        {
            Vector3 loc = new Vector3( xPositions[CharacterManagerScript.netrixiPosition - 1], this.transform.position.y);
            this.transform.position = loc;
        }
        
        if (folkvar)
        {
            if (GameManagerScript.folkvarInParty)
            {
                Vector3 loc = new Vector3( xPositions[CharacterManagerScript.folkvarPosition - 1], this.transform.position.y);
                this.transform.position = loc;
            }
            else
            {
                this.transform.position = new Vector3(100, this.transform.position.y);
            }
        }
        
        if (iv)
        {
            if (GameManagerScript.ivInParty)
            {
                Vector3 loc = new Vector3( xPositions[CharacterManagerScript.ivPosition - 1], this.transform.position.y);
                this.transform.position = loc;
            }
            else
            {
                this.transform.position = new Vector3(100, this.transform.position.y);
            }
        }
        
        
        
        
        // Enemy characters
        if (enemy1)
        {
            Vector3 loc = new Vector3( xPositions[EnemyManagerScript.enemy1Position - 1], this.transform.position.y);
            this.transform.position = loc;
        }
        
        if (enemy2)
        {
            if (EnemyManagerScript.enemy2 != "null")
            {
                Vector3 loc = new Vector3( xPositions[EnemyManagerScript.enemy2Position - 1], this.transform.position.y);
                this.transform.position = loc;
            }
            else
            {
                this.transform.position = new Vector3(100, this.transform.position.y);
            }
        }
        
        if (enemy3)
        {
            if (EnemyManagerScript.enemy3 != "null")
            {
                Vector3 loc = new Vector3( xPositions[EnemyManagerScript.enemy3Position - 1], this.transform.position.y);
                this.transform.position = loc;
            }
            else
            {
                this.transform.position = new Vector3(100, this.transform.position.y);
            }
        }
    }
}
