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

    public static bool netrixiCanMoveLeft = true, netrixiCanMoveRight = true;
    public static bool folkvarCanMoveLeft = true, folkvarCanMoveRight = true;
    public static bool ivCanMoveLeft = true, ivCanMoveRight = true;
    
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


    public static int MoveNetrixi(int direction, bool actuallyMove)
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
                            if (actuallyMove) netrixi1st = netrixiPosition - 1;
                            netrixiCanMoveLeft = true;
                            UpdateVariables();
                            return 1;
                        }
                        else
                        {
                            print("Can't move Netrixi past Iv!");
                            netrixiCanMoveLeft = false;
                            UpdateVariables();
                            return 2;
                        }
                    }
                    else
                    {
                        if (netrixiPosition - 1 > 0)
                        {
                            if (actuallyMove) netrixi1st = netrixiPosition - 1;
                            netrixiCanMoveLeft = true;
                            UpdateVariables();
                            return 1;
                        }
                        else
                        {
                            print("Can't move Netrixi out of bounds!");
                            netrixiCanMoveLeft = false;
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
                                if (actuallyMove) netrixi2nd = netrixi1st - 1;
                                netrixiCanMoveLeft = true;
                                return 1;
                            }
                            else
                            {
                                print("Can't move Netrixi past Iv!");
                                netrixiCanMoveLeft = false;
                                return 2;
                            }
                        }
                        else
                        {
                            if (netrixi1st - 1 > 0)
                            {
                                if (actuallyMove) netrixi2nd = netrixi1st - 1;
                                netrixiCanMoveLeft = true;
                                return 1;
                            }
                            else
                            {
                                print("Can't move Netrixi out of bounds!");
                                netrixiCanMoveLeft = false;
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
                            if (actuallyMove) netrixi1st = netrixiPosition + 1;
                            netrixiCanMoveRight = true;
                            UpdateVariables();
                            return 1;
                        }
                        else
                        {
                            print("Can't move Netrixi past Folkvar!");
                            netrixiCanMoveRight = false;
                            UpdateVariables();
                            return 2;
                        }
                    }
                    else
                    {
                        if (netrixiPosition + 1 < 6)
                        {
                            if (actuallyMove) netrixi1st = netrixiPosition + 1;
                            netrixiCanMoveRight = true;
                            UpdateVariables();
                            return 1;
                        }
                        else
                        {
                            print("Can't move Netrixi into enemy territory!");
                            netrixiCanMoveRight = false;
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
                                if (actuallyMove) netrixi2nd = netrixi1st + 1;
                                netrixiCanMoveRight = true;
                                return 1;
                            }
                            else
                            {
                                print("Can't move Netrixi past Folkvar!");
                                netrixiCanMoveRight = false;
                                return 2;
                            }
                        }
                        else
                        {
                            if (netrixi1st + 1 < 6)
                            {
                                if (actuallyMove) netrixi2nd = netrixi1st + 1;
                                netrixiCanMoveRight = true;
                                return 1;
                            }
                            else
                            {
                                print("Can't move Netrixi into enemy territory!");
                                netrixiCanMoveRight = false;
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
    
    
    
    public static int MoveFolkvar( int direction, bool actuallyMove)
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
                            if (actuallyMove) folkvar1st = folkvarPosition - 1;
                            folkvarCanMoveLeft = true;
                            UpdateVariables();
                            return 1;
                        }
                        else
                        {
                            print("Can't move Folkvar past Netrixi!");
                            folkvarCanMoveLeft = false;
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
                                if (actuallyMove) folkvar1st = folkvarPosition - 1;
                                folkvarCanMoveLeft = true;
                                UpdateVariables();
                                return 1;
                            }
                            else
                            {
                                print("Can't move Folkvar past Iv!");
                                folkvarCanMoveLeft = false;
                                UpdateVariables();
                                return 2;
                            }
                        }
                        else
                        {
                            if (folkvarPosition - 1 > 0)
                            {
                                if (actuallyMove) folkvar1st = folkvarPosition - 1;
                                folkvarCanMoveLeft = true;
                                UpdateVariables();
                                return 1;
                            }
                            else
                            {
                                print("Can't move Folkvar out of bounds!");
                                folkvarCanMoveLeft = false;
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
                                if (actuallyMove) folkvar2nd = folkvar1st - 1;
                                folkvarCanMoveLeft = true;
                                return 1;
                            }
                            else
                            {
                                print("Can't move Folkvar past Netrixi!");
                                folkvarCanMoveLeft = false;
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
                                    if (actuallyMove) folkvar2nd = folkvar1st - 1;
                                    folkvarCanMoveLeft = true;
                                    return 1;
                                }
                                else
                                {
                                    print("Can't move Folkvar past Iv!");
                                    folkvarCanMoveLeft = false;
                                    UpdateVariables();
                                    return 2;
                                }
                            }
                            else
                            {
                                if (folkvar1st - 1 > 0)
                                {
                                    if (actuallyMove) folkvar2nd = folkvar1st - 1;
                                    folkvarCanMoveLeft = true;
                                    return 1;
                                }
                                else
                                {
                                    print("Can't move Folkvar out of bounds!");
                                    folkvarCanMoveLeft = false;
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
                        if (actuallyMove) folkvar1st = folkvarPosition + 1;
                        folkvarCanMoveRight = true;
                        UpdateVariables();
                        return 1;
                    }
                    else
                    {
                        print("Can't move Folkvar into enemy territory!");
                        folkvarCanMoveRight = false;
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
                            if (actuallyMove) folkvar2nd = folkvar1st + 1;
                            folkvarCanMoveRight = true;
                            return 1;
                        }
                        else
                        {
                            print("Can't move Folkvar into enemy territory!");
                            folkvarCanMoveRight = false;
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
    
    
    
    public static int MoveIv( int direction, bool actuallyMove)
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
                        if (actuallyMove) iv1st = ivPosition - 1;
                        ivCanMoveLeft = true;
                        UpdateVariables();
                        return 1;
                    }
                    else
                    {
                        print("Can't move Iv out of bounds!");
                        ivCanMoveLeft = false;
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
                            if (actuallyMove) iv2nd = iv1st - 1;
                            ivCanMoveLeft = true;
                            return 1;
                        }
                        else
                        {
                            print("Can't move Iv out of bounds!");
                            ivCanMoveLeft = false;
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
                            if (actuallyMove) iv1st = ivPosition + 1;
                            ivCanMoveRight = true;
                            UpdateVariables();
                            return 1;
                        }
                        else
                        {
                            print("Can't move Iv past Netrixi!");
                            ivCanMoveRight = false;
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
                                if (actuallyMove) iv1st = ivPosition + 1;
                                ivCanMoveRight = true;
                                UpdateVariables();
                                return 1;
                            }
                            else
                            {
                                print("Can't move Iv past Folkvar!");
                                ivCanMoveRight = false;
                                UpdateVariables();
                                return 2;
                            }
                        }
                        else
                        {
                            if (ivPosition + 1 < 6)
                            {
                                if (actuallyMove) iv1st = ivPosition + 1;
                                ivCanMoveRight = true;
                                UpdateVariables();
                                return 1;
                            }
                            else
                            {
                                print("Can't move Iv into enemy territory!");
                                ivCanMoveRight = false;
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
                                if (actuallyMove) iv2nd = iv1st + 1;
                                ivCanMoveRight = true;
                                return 1;
                            }
                            else
                            {
                                print("Can't move Iv past Netrixi!");
                                ivCanMoveRight = false;
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
                                    if (actuallyMove) iv2nd = iv1st + 1;
                                    ivCanMoveRight = true;
                                    return 1;
                                }
                                else
                                {
                                    print("Can't move Iv past Folkvar!");
                                    ivCanMoveRight = false;
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
                                    if (actuallyMove) iv2nd = iv1st + 1;
                                    ivCanMoveRight = true;
                                    return 1;
                                }
                                else
                                {
                                    print("Can't move Iv into enemy territory!");
                                    ivCanMoveRight = false;
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


        netrixiCanMoveLeft = true;
        netrixiCanMoveRight = true;
        
        folkvarCanMoveLeft = true;
        folkvarCanMoveRight = true;
        
        ivCanMoveLeft = true;
        ivCanMoveRight = true;
    }
}
