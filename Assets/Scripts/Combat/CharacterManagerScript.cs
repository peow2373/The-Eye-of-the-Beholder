using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagerScript : MonoBehaviour
{
    public static int netrixiPosition = 3, folkvarPosition = 5, ivPosition = 1;
    public static int netrixi1st = 3, folkvar1st = 5, iv1st = 1;
    public static int netrixi2nd = 3, folkvar2nd = 5, iv2nd = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void StartCombat()
    {
        netrixiPosition = 3;
        folkvarPosition = 5;
        ivPosition = 1;
    }

    
    public static int MoveNetrixi( int direction )
    {
        // Netrixi moves to the left
        if (direction == 1)
        {
            // If it is the first move
            if (CombatManagerScript.firstAttack == 0)
            {
                // If Iv is in the way
                if (GameManagerScript.ivInParty && CombatManagerScript.ivAlive)
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
                    if (netrixiPosition - 1 != 0)
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
                    if (GameManagerScript.ivInParty && CombatManagerScript.ivAlive)
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
                        if (netrixi1st - 1 != 0)
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
                if (GameManagerScript.folkvarInParty && CombatManagerScript.folkvarAlive)
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
                    if (netrixiPosition + 1 != 6)
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
                    if (GameManagerScript.folkvarInParty && CombatManagerScript.folkvarAlive)
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
                        if (netrixi1st + 1 != 6)
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
        return 0;
    }
    
    
    
    public static int MoveFolkvar( int direction )
    {
        // Folkvar moves to the left
        if (direction == 1)
        {
            // If it is the first move
            if (CombatManagerScript.firstAttack == 0)
            {
                // If Netrixi is in the way
                if (GameManagerScript.netrixiInParty && CombatManagerScript.netrixiAlive)
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
                    if (GameManagerScript.ivInParty && CombatManagerScript.ivAlive)
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
                        if (folkvarPosition - 1 != 0)
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
                    if (GameManagerScript.netrixiInParty && CombatManagerScript.netrixiAlive)
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
                        if (GameManagerScript.ivInParty && CombatManagerScript.ivAlive)
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
                            if (folkvar1st - 1 != 0)
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
                if (folkvarPosition + 1 != 6)
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
                    if (folkvar1st + 1 != 6)
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
        return 0;
    }
    
    
    
    public static int MoveIv( int direction )
    {
        // Iv moves to the left
        if (direction == 1)
        {
            // If it is the first move
            if (CombatManagerScript.firstAttack == 0)
            {
                if (ivPosition - 1 != 0)
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
                    if (iv1st - 1 != 0)
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
                if (GameManagerScript.netrixiInParty && GameManagerScript.folkvarInParty)
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
                    if (GameManagerScript.folkvarInParty && CombatManagerScript.folkvarAlive)
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
                        if (ivPosition + 1 != 6)
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
                if (GameManagerScript.netrixiInParty && CombatManagerScript.netrixiAlive)
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
                    if (GameManagerScript.folkvarInParty && CombatManagerScript.folkvarAlive)
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
                            if (iv1st + 1 != 6)
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
    

    static void UpdateVariables()
    {
        netrixi2nd = netrixi1st;
        folkvar2nd = folkvar1st;
        iv2nd = iv1st;
    }


    public static void ResetVariables()
    {
        netrixiPosition = netrixi2nd;
        netrixi1st = netrixiPosition;
        netrixi2nd = netrixiPosition;
        
        folkvarPosition = folkvar2nd;
        folkvar1st = folkvarPosition;
        folkvar2nd = folkvarPosition;
        
        ivPosition = iv2nd;
        iv1st = ivPosition;
        iv2nd = ivPosition;
    }
}
