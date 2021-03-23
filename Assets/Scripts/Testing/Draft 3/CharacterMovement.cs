using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public bool netrixi, folkvar, iv, enemy1, enemy2, enemy3;

    public static float[] xPositions = new float[] { -50f, -40f, -30f, -20f, -10f, 10f, 20f, 30f, 40f, 50f };
    
    public static float upperYPosition = -17f;
    public static float middleYPosition = -19f;
    public static float lowerYPosition = -21f;
    
    
    public static float selectedPosition = 20;

    public static float[] xLocations = new float[10];
    
    public static float upperYLocation;
    public static float middleYLocation;
    public static float lowerYLocation;

    private float offScreen = 1000;

    public static float combatWidth, combatHeight;

    public static float netrixiOffset = 0;
    public static float folkvarOffset = 1f;
    public static float ivOffset = -0.5f;
    
    
    public static float size1Offset = -0.25f;
    public static float size2Offset = 0f;
    public static float size3Offset = 0.7f;
    public static float size4Offset = 1.5f;
    public static float size5Offset = 2f;
    public static float size6Offset = 2.25f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public static void ChangeXLocations(float width, float xLocation)
    {
        for (int i = 0; i < xLocations.Length; i++)
        {
            xLocations[i] = xLocation + (xPositions[i]*width);
        }

        combatWidth = width;
    }
    
    public static void ChangeYLocations(float height, float yLocation)
    {
        upperYLocation = yLocation + (upperYPosition*height);
        middleYLocation = yLocation + (middleYPosition*height);
        lowerYLocation = yLocation + (lowerYPosition*height);

        combatHeight = height;
    }

    // Update is called once per frame
    void Update()
    {
        // Your characters
        if (netrixi)
        {
            if (CombatManagerScript.netrixiAlive && GameManagerScript.netrixiInParty)
            {
                if (!CombatManagerScript.netrixiAttacks)
                {
                    float yLoc = DetermineYLocation(CharacterManagerScript.netrixiPosition) + (netrixiOffset*combatHeight);
                
                    Vector3 loc = new Vector3( xLocations[CharacterManagerScript.netrixiPosition - 1], yLoc);
                    this.transform.position = loc;
                }
            }
            else
            {
                this.transform.position = new Vector3(offScreen, this.transform.position.y);
            }
        }
        
        if (folkvar)
        {
            if (CombatManagerScript.folkvarAlive && GameManagerScript.folkvarInParty)
            {
                if (!CombatManagerScript.folkvarAttacks)
                {
                    float yLoc = DetermineYLocation(CharacterManagerScript.folkvarPosition) + (folkvarOffset*combatHeight);
                
                    Vector3 loc = new Vector3( xLocations[CharacterManagerScript.folkvarPosition - 1], yLoc);
                    this.transform.position = loc;
                }
            }
            else
            {
                this.transform.position = new Vector3(offScreen, this.transform.position.y);
            }
        }
        
        if (iv)
        {
            if (CombatManagerScript.ivAlive && GameManagerScript.ivInParty)
            {
                if (!CombatManagerScript.ivAttacks)
                {
                    float yLoc = DetermineYLocation(CharacterManagerScript.ivPosition) + (ivOffset*combatHeight);
                
                    Vector3 loc = new Vector3( xLocations[CharacterManagerScript.ivPosition - 1], yLoc);
                    this.transform.position = loc;
                }
            }
            else
            {
                this.transform.position = new Vector3(offScreen, this.transform.position.y);
            }
        }
        
        
        
        
        // Enemy characters
        if (enemy1)
        {
            if (CombatManagerScript.enemy1Alive)
            {
                float yLoc = DetermineYLocation(EnemyManagerScript.enemy1Position);
                
                Vector3 loc = new Vector3( xLocations[EnemyManagerScript.enemy1Position - 1], yLoc);
                this.transform.position = loc;
            }
            else
            {
                this.transform.position = new Vector3(offScreen, this.transform.position.y);
            }
        }
        
        if (enemy2)
        {
            if (CombatManagerScript.enemy2Alive && EnemyManagerScript.enemy2 != "null")
            {
                float yLoc = DetermineYLocation(EnemyManagerScript.enemy2Position);
                
                Vector3 loc = new Vector3( xLocations[EnemyManagerScript.enemy2Position - 1], yLoc);
                this.transform.position = loc;
            }
            else
            {
                this.transform.position = new Vector3(offScreen, this.transform.position.y);
            }
        }
        
        if (enemy3)
        {
            if (CombatManagerScript.enemy3Alive && EnemyManagerScript.enemy3 != "null")
            {
                float yLoc = DetermineYLocation(EnemyManagerScript.enemy3Position);
                
                Vector3 loc = new Vector3( xLocations[EnemyManagerScript.enemy3Position - 1], yLoc);
                this.transform.position = loc;
            }
            else
            {
                this.transform.position = new Vector3(offScreen, this.transform.position.y);
            }
        }
    }

    public static float DetermineYLocation(int characterPosition)
    {
        switch (characterPosition)
        {
            case 1:
            case 3:
            case 5:
            case 6:
            case 8:
            case 10:
                return middleYLocation;
            
            case 2:
            case 9:
                return upperYLocation;

            case 4:
            case 7:
                return lowerYLocation;
        }

        return middleYLocation;
    }
}
