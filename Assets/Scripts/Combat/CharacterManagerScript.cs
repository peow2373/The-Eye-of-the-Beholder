using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagerScript : MonoBehaviour
{
    public static int netrixiPosition = 3, folkvarPosition = 5, ivPosition = 1;
    public static int netrixi1st = 3, folkvar1st = 5, iv1st = 1;
    public static int netrixi2nd = 3, folkvar2nd = 5, iv2nd = 1;
    
    public static int netrixiStartingPosition = 2;
    public static int folkvarStartingPosition = 4;
    public static int ivStartingPosition = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if characters are alive
        if (!CombatManagerScript.netrixiAlive)
        {
            netrixiPosition = 0;
            netrixi1st = 0;
            netrixi2nd = 0;
        }
        
        if (!CombatManagerScript.folkvarAlive)
        {
            folkvarPosition = 0;
            folkvar1st = 0;
            folkvar2nd = 0;
        }
        
        if (!CombatManagerScript.ivAlive)
        {
            ivPosition = 0;
            iv1st = 0;
            iv2nd = 0;
        }
    }

    public static void StartCombat()
    {
        // Reset character position
        netrixiPosition = netrixiStartingPosition;
        folkvarPosition = folkvarStartingPosition;
        ivPosition = ivStartingPosition;
        
        netrixi1st = netrixiPosition;
        folkvar1st = folkvarPosition;
        iv1st = ivPosition;
        
        netrixi2nd = netrixi1st;
        folkvar2nd = folkvar1st;
        iv2nd = iv1st;


        // Reset character HP
        CombatManagerScript.netrixiHP = HealthValues.netrixiHP;
        CombatManagerScript.folkvarHP = HealthValues.folkvarHP;
        CombatManagerScript.ivHP = HealthValues.ivHP;
    }

    
    public static int MoveNetrixi( int direction )
    {
        // If Netrixi is still alive
        if (CombatManagerScript.netrixiAlive)
        {
            // Netrixi moves to the left
            if (direction == 1)
            {
                // If it is the first move
                if (CombatManagerScript.firstAttack == 0)
                {
                    // If Iv is in the way
                    if (CombatManagerScript.ivAlive)
                    {
                        if (netrixiPosition - 1 != ivPosition)
                        {
                            netrixi1st = netrixiPosition - 1;
                            UpdateVariables();
                            return 1;
                        }
                        else
                        {
                            print("Can't move Netrixi past Iv!");
                            UpdateVariables();
                            return 2;
                        }
                    }
                    else
                    {
                        if (netrixiPosition - 1 > 0)
                        {
                            netrixi1st = netrixiPosition - 1;
                            UpdateVariables();
                            return 1;
                        }
                        else
                        {
                            print("Can't move Netrixi out of bounds!");
                            UpdateVariables();
                            return 2;
                        }
                    }
                }
                
                // If it is the second move
                else
                {
                    // If Iv is in the way
                    if (CombatManagerScript.secondAttack == 0)
                    {
                        if (CombatManagerScript.ivAlive)
                        {
                            if (netrixi1st - 1 != iv2nd)
                            {
                                netrixi2nd = netrixi1st - 1;
                                return 1;
                            }
                            else
                            {
                                print("Can't move Netrixi past Iv!");
                                return 2;
                            }
                        }
                        else
                        {
                            if (netrixi1st - 1 > 0)
                            {
                                netrixi2nd = netrixi1st - 1;
                                return 1;
                            }
                            else
                            {
                                print("Can't move Netrixi out of bounds!");
                                return 2;
                            }
                        }
                    }
                }
            }
            
            
            // Netrixi moves to the right
            if (direction == 2)
            {
                // If it is the first move
                if (CombatManagerScript.firstAttack == 0)
                {
                    // If Folkvar is in the way
                    if (CombatManagerScript.folkvarAlive)
                    {
                        if (netrixiPosition + 1 != folkvarPosition)
                        {
                            netrixi1st = netrixiPosition + 1;
                            UpdateVariables();
                            return 1;
                        }
                        else
                        {
                            print("Can't move Netrixi past Folkvar!");
                            UpdateVariables();
                            return 2;
                        }
                    }
                    else
                    {
                        if (netrixiPosition + 1 < 6)
                        {
                            netrixi1st = netrixiPosition + 1;
                            UpdateVariables();
                            return 1;
                        }
                        else
                        {
                            print("Can't move Netrixi into enemy territory!");
                            UpdateVariables();
                            return 2;
                        }
                    }
                }
                
                // If it is the second move
                else
                {
                    // If Folkvar is in the way
                    if (CombatManagerScript.secondAttack == 0)
                    {
                        if (CombatManagerScript.folkvarAlive)
                        {
                            if (netrixi1st + 1 != folkvar2nd)
                            {
                                netrixi2nd = netrixi1st + 1;
                                return 1;
                            }
                            else
                            {
                                print("Can't move Netrixi past Folkvar!");
                                return 2;
                            }
                        }
                        else
                        {
                            if (netrixi1st + 1 < 6)
                            {
                                netrixi2nd = netrixi1st + 1;
                                return 1;
                            }
                            else
                            {
                                print("Can't move Netrixi into enemy territory!");
                                return 2;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            return 2;
        }

        return 0;
    }
    
    
    
    public static int MoveFolkvar( int direction )
    {
        // If Folkvar is still alive
        if (CombatManagerScript.folkvarAlive)
        {
            // Folkvar moves to the left
            if (direction == 1)
            {
                // If it is the first move
                if (CombatManagerScript.firstAttack == 0)
                {
                    // If Netrixi is in the way
                    if (CombatManagerScript.netrixiAlive)
                    {
                        if (folkvarPosition - 1 != netrixiPosition)
                        {
                            folkvar1st = folkvarPosition - 1;
                            UpdateVariables();
                            return 1;
                        }
                        else
                        {
                            print("Can't move Folkvar past Netrixi!");
                            UpdateVariables();
                            return 2;
                        }
                    }
                    else
                    {
                        // If Iv is in the way
                        if (CombatManagerScript.ivAlive)
                        {
                            if (folkvarPosition - 1 != ivPosition)
                            {
                                folkvar1st = folkvarPosition - 1;
                                UpdateVariables();
                                return 1;
                            }
                            else
                            {
                                print("Can't move Folkvar past Iv!");
                                UpdateVariables();
                                return 2;
                            }
                        }
                        else
                        {
                            if (folkvarPosition - 1 > 0)
                            {
                                folkvar1st = folkvarPosition - 1;
                                UpdateVariables();
                                return 1;
                            }
                            else
                            {
                                print("Can't move Folkvar out of bounds!");
                                UpdateVariables();
                                return 2;
                            }
                        }
                    }
                }
                
                // If it is the second move
                else
                {
                    if (CombatManagerScript.secondAttack == 0)
                    {
                        if (CombatManagerScript.netrixiAlive)
                        {
                            if (folkvar1st - 1 != netrixi2nd)
                            {
                                folkvar2nd = folkvar1st - 1;
                                return 1;
                            }
                            else
                            {
                                print("Can't move Folkvar past Netrixi!");
                                return 2;
                            }
                        }
                        else
                        {
                            // If Iv is in the way
                            if (CombatManagerScript.ivAlive)
                            {
                                if (folkvar1st - 1 != iv2nd)
                                {
                                    folkvar2nd = folkvar1st - 1;
                                    return 1;
                                }
                                else
                                {
                                    print("Can't move Folkvar past Iv!");
                                    UpdateVariables();
                                    return 2;
                                }
                            }
                            else
                            {
                                if (folkvar1st - 1 > 0)
                                {
                                    folkvar2nd = folkvar1st - 1;
                                    return 1;
                                }
                                else
                                {
                                    print("Can't move Folkvar out of bounds!");
                                    UpdateVariables();
                                    return 2;
                                }
                            }
                        }
                    }
                }
            }
            
            // Folkvar moves to the right
            if (direction == 2)
            {
                // If it is the first move
                if (CombatManagerScript.firstAttack == 0)
                {
                    if (folkvarPosition + 1 < 6)
                    {
                        folkvar1st = folkvarPosition + 1;
                        UpdateVariables();
                        return 1;
                    }
                    else
                    {
                        print("Can't move Folkvar into enemy territory!");
                        UpdateVariables();
                        return 2;
                    }
                }
                
                // If it is the second move
                else
                {
                    if (CombatManagerScript.secondAttack == 0)
                    {
                        if (folkvar1st + 1 < 6)
                        {
                            folkvar2nd = folkvar1st + 1;
                            return 1;
                        }
                        else
                        {
                            print("Can't move Folkvar into enemy territory!");
                            return 2;
                        }
                    }
                }
            }
        }
        else
        {
            return 2;
        }
        
        return 0;
    }
    
    
    
    public static int MoveIv( int direction )
    {
        // If Iv is still alive
        if (CombatManagerScript.ivAlive)
        {
            // Iv moves to the left
            if (direction == 1)
            {
                // If it is the first move
                if (CombatManagerScript.firstAttack == 0)
                {
                    if (ivPosition - 1 > 0)
                    {
                        iv1st = ivPosition - 1;
                        UpdateVariables();
                        return 1;
                    }
                    else
                    {
                        print("Can't move Iv out of bounds!");
                        UpdateVariables();
                        return 2;
                    }
                }
                
                // If it is the second move
                else
                {
                    if (CombatManagerScript.secondAttack == 0)
                    {
                        if (iv1st - 1 > 0)
                        {
                            iv2nd = iv1st - 1;
                            return 1;
                        }
                        else
                        {
                            print("Can't move Iv out of bounds!");
                            return 2;
                        }
                    }
                }
            }
            
            // Iv moves to the right
            if (direction == 2)
            {
                // If it is the first move
                if (CombatManagerScript.firstAttack == 0)
                {
                    // If Netrixi is in the way
                    if (CombatManagerScript.netrixiAlive)
                    {
                        if (ivPosition + 1 != netrixiPosition)
                        {
                            iv1st = ivPosition + 1;
                            UpdateVariables();
                            return 1;
                        }
                        else
                        {
                            print("Can't move Iv past Netrixi!");
                            UpdateVariables();
                            return 2;
                        }
                    }
                    else
                    {
                        // If Folkvar is in the way
                        if (CombatManagerScript.folkvarAlive)
                        {
                            if (ivPosition + 1 != folkvarPosition)
                            {
                                iv1st = ivPosition + 1;
                                UpdateVariables();
                                return 1;
                            }
                            else
                            {
                                print("Can't move Iv past Folkvar!");
                                UpdateVariables();
                                return 2;
                            }
                        }
                        else
                        {
                            if (ivPosition + 1 < 6)
                            {
                                iv1st = ivPosition + 1;
                                UpdateVariables();
                                return 1;
                            }
                            else
                            {
                                print("Can't move Iv into enemy territory!");
                                UpdateVariables();
                                return 2;
                            }
                        }
                    }
                }
                
                // If it is the second move
                else
                {
                    // If Netrixi is in the way
                    if (CombatManagerScript.netrixiAlive)
                    {
                        if (CombatManagerScript.secondAttack == 0)
                        {
                            if (iv1st + 1 != netrixi2nd)
                            {
                                iv2nd = iv1st + 1;
                                return 1;
                            }
                            else
                            {
                                print("Can't move Iv past Netrixi!");
                                return 2;
                            }
                        }
                    }
                    else
                    {
                        // If Folkvar is in the way
                        if (CombatManagerScript.folkvarAlive)
                        {
                            if (CombatManagerScript.secondAttack == 0)
                            {
                                if (iv1st + 1 != folkvar2nd)
                                {
                                    iv2nd = iv1st + 1;
                                    return 1;
                                }
                                else
                                {
                                    print("Can't move Iv past Folkvar!");
                                    return 2;
                                }
                            }
                        }
                        else
                        {
                            if (CombatManagerScript.secondAttack == 0)
                            {
                                if (iv1st + 1 < 6)
                                {
                                    iv2nd = iv1st + 1;
                                    return 1;
                                }
                                else
                                {
                                    print("Can't move Iv into enemy territory!");
                                    return 2;
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            return 2;
        }
        
        return 0;
    }

    
    public static void UndoMove()
    {
        // Reset 2nd move
        if (CombatManagerScript.secondAttack != 0)
        {
            netrixi2nd = netrixi1st;
            folkvar2nd = folkvar1st;
            iv2nd = iv1st;
        }
        else
        {
            // Reset 1st move
            if (CombatManagerScript.firstAttack != 0)
            {
                netrixi1st = netrixiPosition;
                folkvar1st = folkvarPosition;
                iv1st = ivPosition;
                
                netrixi2nd = netrixiPosition;
                folkvar2nd = folkvarPosition;
                iv2nd = ivPosition;
            }
        }
    }
    

    public static void UpdateVariables()
    {
        netrixi2nd = netrixi1st;
        folkvar2nd = folkvar1st;
        iv2nd = iv1st;
    }


    public static void ResetVariables()
    {
        netrixi1st = netrixiPosition;
        netrixi2nd = netrixiPosition;
        
        folkvar1st = folkvarPosition;
        folkvar2nd = folkvarPosition;
        
        iv1st = ivPosition;
        iv2nd = ivPosition;
    }
}
