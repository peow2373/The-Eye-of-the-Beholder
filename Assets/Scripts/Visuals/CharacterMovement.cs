using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public bool netrixi, folkvar, iv, enemy1, enemy2, enemy3;

    public static float[] xPositions = new float[] { -50f, -40f, -30f, -20f, -10f, 10f, 20f, 30f, 40f, 50f };
    
    public static float upperYPosition = -18.5f;
    public static float middleYPosition = -20.5f;
    public static float lowerYPosition = -22.5f;

    private SpriteRenderer sr;
    
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
        sr = this.GetComponent<SpriteRenderer>();
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
                    int location = CharacterManagerScript.netrixiPosition;
                    float yLoc = DetermineYLocation(location) + (netrixiOffset*combatHeight);
                
                    Vector3 loc = new Vector3( xLocations[CharacterManagerScript.netrixiPosition - 1], yLoc);
                    this.transform.position = loc;

                    // If Netrixi is being targeted
                    if (EnemyManagerScript.attack1Location == location || EnemyManagerScript.attack1Location2 == location ||
                        EnemyManagerScript.attack2Location == location || EnemyManagerScript.attack2Location2 == location)
                    {
                        sr.color = Color.red;
                    } 
                    else if (!CombatManagerScript.canNetrixiAttack) sr.color = Color.gray;
                    else sr.color = Color.white;
                }
                else if (!CombatManagerScript.canNetrixiAttack) sr.color = Color.gray;
                else sr.color = Color.white;
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
                    int location = CharacterManagerScript.folkvarPosition;
                    float yLoc = DetermineYLocation(location) + (folkvarOffset*combatHeight);
                
                    Vector3 loc = new Vector3( xLocations[CharacterManagerScript.folkvarPosition - 1], yLoc);
                    this.transform.position = loc;
                    
                    // If Folkvar is being targeted
                    if (EnemyManagerScript.attack1Location == location || EnemyManagerScript.attack1Location2 == location ||
                        EnemyManagerScript.attack2Location == location || EnemyManagerScript.attack2Location2 == location)
                    {
                        sr.color = Color.red;
                    } 
                    else if (!CombatManagerScript.canFolkvarAttack) sr.color = Color.gray;
                    else sr.color = Color.white;
                }
                else if (!CombatManagerScript.canFolkvarAttack) sr.color = Color.gray;
                else sr.color = Color.white;
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
                    int location = CharacterManagerScript.ivPosition;
                    float yLoc = DetermineYLocation(location) + (ivOffset*combatHeight);
                
                    Vector3 loc = new Vector3( xLocations[CharacterManagerScript.ivPosition - 1], yLoc);
                    this.transform.position = loc;
                    
                    // If Iv is being targeted
                    if (EnemyManagerScript.attack1Location == location || EnemyManagerScript.attack1Location2 == location ||
                        EnemyManagerScript.attack2Location == location || EnemyManagerScript.attack2Location2 == location)
                    {
                        sr.color = Color.red;
                    } 
                    else if (!CombatManagerScript.canIvAttack) sr.color = Color.gray;
                    else sr.color = Color.white;
                }
                else if (!CombatManagerScript.canIvAttack) sr.color = Color.gray;
                else sr.color = Color.white;
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
                int location = EnemyManagerScript.enemy1Position;
                float yLoc = DetermineYLocation(location) + DetermineYSize(ChangeHealth.sizeOfEnemy1);
                
                Vector3 loc = new Vector3( xLocations[EnemyManagerScript.enemy1Position - 1], yLoc);
                this.transform.position = loc;
                
                // If Enemy 1 is being targeted
                if (CombatManagerScript.netrixiTarget1Location == location || CombatManagerScript.netrixiTarget2Location == location ||
                    CombatManagerScript.folkvarTarget1Location == location || CombatManagerScript.folkvarTarget2Location == location)
                {
                    sr.color = Color.red;
                }
                else if (!CombatManagerScript.canEnemy1Attack && !NetrixiAttackScript.enemy1Transformed) sr.color = Color.gray;
                else sr.color = Color.white;
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
                int location = EnemyManagerScript.enemy2Position;
                float yLoc = DetermineYLocation(location) + DetermineYSize(ChangeHealth.sizeOfEnemy2);
                
                Vector3 loc = new Vector3( xLocations[EnemyManagerScript.enemy2Position - 1], yLoc);
                this.transform.position = loc;
                
                // If Enemy 2 is being targeted
                if (CombatManagerScript.netrixiTarget1Location == location || CombatManagerScript.netrixiTarget2Location == location ||
                    CombatManagerScript.folkvarTarget1Location == location || CombatManagerScript.folkvarTarget2Location == location)
                {
                    sr.color = Color.red;
                } 
                else if (!CombatManagerScript.canEnemy2Attack && !NetrixiAttackScript.enemy2Transformed) sr.color = Color.gray;
                else sr.color = Color.white;
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
                int location = EnemyManagerScript.enemy3Position;
                float yLoc = DetermineYLocation(location) + DetermineYSize(ChangeHealth.sizeOfEnemy3);
                
                Vector3 loc = new Vector3( xLocations[EnemyManagerScript.enemy3Position - 1], yLoc);
                this.transform.position = loc;
                
                // If Enemy 3 is being targeted
                if (CombatManagerScript.netrixiTarget1Location == location || CombatManagerScript.netrixiTarget2Location == location ||
                    CombatManagerScript.folkvarTarget1Location == location || CombatManagerScript.folkvarTarget2Location == location)
                {
                    sr.color = Color.red;
                } 
                else if (!CombatManagerScript.canEnemy3Attack && !NetrixiAttackScript.enemy3Transformed) sr.color = Color.gray;
                else sr.color = Color.white;
            }
            else
            {
                this.transform.position = new Vector3(offScreen, this.transform.position.y);
            }
        }
    }

    public float DetermineYLocation(int characterPosition)
    {
        switch (characterPosition)
        {
            case 1:
            case 3:
            case 5:
                sr.sortingOrder = 5;
                return middleYLocation;
                
            case 6:
            case 8:
            case 10:
                sr.sortingOrder = 2;
                return middleYLocation;
            
            case 2:
                sr.sortingOrder = 4;
                return upperYLocation;
            case 9:
                sr.sortingOrder = 1;
                return upperYLocation;

            case 4:
                sr.sortingOrder = 6;
                return lowerYLocation;
            case 7:
                sr.sortingOrder = 3;
                return lowerYLocation;
        }

        return middleYLocation;
    }

    public float DetermineYSize(int size)
    {
        switch (size)
        {
            case 1:
                return size1Offset;
            
            case 2:
                return size2Offset;
            
            case 3:
                return size3Offset;
            
            case 4:
                return size4Offset;
            
            case 5:
                return size5Offset;
            
            case 6:
                return size6Offset;
        }

        return size2Offset;
    }
}
