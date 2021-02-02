using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class CombatManagerScript : MonoBehaviour
{
    // Can any of the characters attack?
    public static bool netrixiAttacks = false;
    public static bool folkvarAttacks = false;
    public static bool ivAttacks = false;

    public static int firstAttack = 0, secondAttack = 0;

    private bool isNetrixi = true, isFolkvar = true, isIv = true;
    
    // Start is called before the first frame update
    void Start()
    {
        MarkerManagerScript.S.Reset();
        firstAttack = 0;
        secondAttack = 0;

        isNetrixi = true;
        isFolkvar = true; 
        isIv = true;
    }

    // Update is called once per frame
    void Update()
    {
        // If the Netrixi marker is visible
        if (MarkerManagerScript.netrixiMarker)
        {
            if (isNetrixi)
            {
                netrixiAttacks = true;
                folkvarAttacks = false;
                ivAttacks = false;

                isNetrixi = false; 
                
                print("Netrixi will attack with a spell");
            }
        }
        else
        {
            isNetrixi = true;
        }

        // If the Folkvar marker is visible
        if (MarkerManagerScript.folkvarMarker)
        {
            if (isFolkvar)
            {
                folkvarAttacks = true;
                netrixiAttacks = false;
                ivAttacks = false;
            
                isFolkvar = false;
                
                print("Folkvar will attack with his mighty weapons");
            }
        } 
        else
        {
            isFolkvar = true;
        }

        // If the Iv marker is visible
        if (MarkerManagerScript.ivMarker)
        {
            if (isIv)
            {
                ivAttacks = true;
                netrixiAttacks = false;
                folkvarAttacks = false;
            
                isIv = false;
                
                print("Iv will block with her force powers");
            }
        }
        else
        {
            isIv = true;
        }

        // Determine whether the player has chosen a combat move
        NetrixiCombatScript.NetrixiAttack();
        FolkvarCombatScript.FolkvarAttack();
        IvCombatScript.IvBlock();

        // Determine what the player's first attack is
        if (firstAttack != 0)
        {
            // Determine what the player's second attack is
            if (secondAttack == 0)
            {
                // Reset attack if player chooses
                if (Input.GetKeyDown(KeyCode.U)) firstAttack = 0;
            } 
            else
            {
                // Reset attack if player chooses
                if (Input.GetKeyDown(KeyCode.U)) secondAttack = 0;
            }
        }
    }
    
    
    
    public static void NetrixiSpellCast()
    {
        // Netrixi casts her first spell
        if (NetrixiCombatScript.netrixiCondition1[0] && NetrixiCombatScript.netrixiCondition1[1])
        {
            print("Netrixi casts her first spell");
            
            // Make Netrixi attack in game 
            if (firstAttack != 0)
            {
                if (secondAttack == 0) secondAttack = 1;
            }
            else firstAttack = 1;


            NetrixiCombatScript.ResetNetrixiVariables();
        }
            
        // Netrixi casts her second spell
        if (NetrixiCombatScript.netrixiCondition2[0] && NetrixiCombatScript.netrixiCondition2[1])
        {
            print("Netrixi casts her second spell");
            
            // Make Netrixi attack in game 
            if (firstAttack != 0)
            {
                if (secondAttack == 0) secondAttack = 2;
            }
            else firstAttack = 2;
            

            NetrixiCombatScript.ResetNetrixiVariables();
        }
            
        // Netrixi casts her third spell
        if (NetrixiCombatScript.netrixiCondition3[0] && NetrixiCombatScript.netrixiCondition3[1] && NetrixiCombatScript.netrixiCondition3[2] && NetrixiCombatScript.netrixiCondition3[3] && NetrixiCombatScript.netrixiCondition3[4])
        {
            print("Netrixi casts her third spell");
            
            // Make Netrixi attack in game 
            if (firstAttack != 0)
            {
                if (secondAttack == 0) secondAttack = 3;
            }
            else firstAttack = 3;
            

            NetrixiCombatScript.ResetNetrixiVariables();
        }
    }
    
    
    
    public static void FolkvarMeleeAttack()
    {
        // Folkvar uses his first melee attack
        if (FolkvarCombatScript.folkvarCondition1[0] && FolkvarCombatScript.folkvarCondition1[1])
        {
            print("Folkvar uses his first attack");
            
            // Make Folkvar attack in game 
            if (firstAttack != 0)
            {
                if (secondAttack == 0) secondAttack = 4;
            }
            else firstAttack = 4;


            FolkvarCombatScript.ResetFolkvarVariables();
        }
            
        // Folkvar uses his second melee attack
        if (FolkvarCombatScript.folkvarCondition2[0] && FolkvarCombatScript.folkvarCondition2[1])
        {
            print("Folkvar uses his second attack");
            
            // Make Folkvar attack in game 
            if (firstAttack != 0)
            {
                if (secondAttack == 0) secondAttack = 5;
            }
            else firstAttack = 5;
            

            FolkvarCombatScript.ResetFolkvarVariables();
        }
            
        // Folkvar uses his third melee attack
        if (FolkvarCombatScript.folkvarCondition3[0] && FolkvarCombatScript.folkvarCondition3[1] && FolkvarCombatScript.folkvarCondition3[2])
        {
            print("Folkvar uses his third attack");
            
            // Make Folkvar attack in game 
            if (firstAttack != 0)
            {
                if (secondAttack == 0) secondAttack = 6;
            }
            else firstAttack = 6;
            

            FolkvarCombatScript.ResetFolkvarVariables();
        }
    }
    
    
    
    public static void IvTeamSupport()
    {
        // Iv uses their first ability
        if (IvCombatScript.ivCondition1[0] && IvCombatScript.ivCondition1[1])
        {
            print("Iv blocks the next attack");
            
            // Make Iv block in game 
            if (firstAttack != 0)
            {
                if (secondAttack == 0) secondAttack = 7;
            }
            else firstAttack = 7;


            IvCombatScript.ResetIvVariables();
        }
            
        // Iv uses their first ability
        if (IvCombatScript.ivCondition2[0] && IvCombatScript.ivCondition2[1])
        {
            print("Iv empowers both teams");
            
            // Make Iv empower in game 
            if (firstAttack != 0)
            {
                if (secondAttack == 0) secondAttack = 8;
            }
            else firstAttack = 8;
            

            IvCombatScript.ResetIvVariables();
        }
            
        // Iv uses their first ability
        if (IvCombatScript.ivCondition3[0] && IvCombatScript.ivCondition3[1] && IvCombatScript.ivCondition3[2] && IvCombatScript.ivCondition3[3])
        {
            print("Iv heals the weakest party");
            
            // Make Iv heal in game 
            if (firstAttack != 0)
            {
                if (secondAttack == 0) secondAttack = 9;
            }
            else firstAttack = 9;
            

            IvCombatScript.ResetIvVariables();
        }
    }
}
