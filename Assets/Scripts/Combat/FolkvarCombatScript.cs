using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolkvarCombatScript : MonoBehaviour
{
    // Conditions for each of Folkvar's attacks
    public static bool[] folkvarCondition1 = new bool[] {false, false};
    public static bool[] folkvarCondition2 = new bool[] {false, false, false};
    public static bool[] folkvarCondition3 = new bool[] {false, false, false};
    public static bool[] folkvarCondition4 = new bool[] {false, false, false};

    public static int swing = 1;

    public static int upStart;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static void FolkvarAttack()
    {
        if (CombatManagerScript.folkvarAttacks)
        {
            // First attack
            // If player first puts their hand in the top-left corner
            if (MarkerManagerScript.currentLocation == 1) folkvarCondition1[0] = true;

            if (folkvarCondition1[0] && !folkvarCondition1[1])
            {
                Debug.Log("Sword at the ready");
                
                // If player then moves hand to the bottom-right
                if (Input.GetKeyDown(KeyCode.C)) folkvarCondition1[1] = true;
                    
                // If player instead places their hand in any other corner
                if (!folkvarCondition1[1])
                {
                    if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E))
                    {
                        folkvarCondition1[0] = false;
                    } 
                }
            }


            // Second attack
            // If player moves their hand closer to the webcam first
            if (MarkerManagerScript.currentLocation == 2) folkvarCondition2[0] = true;
            
            if (folkvarCondition2[0])
            {
                // If player then moves hand to the upper-left section
                if (Input.GetKeyDown(KeyCode.E)) folkvarCondition2[1] = true;


                // If player then moves hand downwards
                if (folkvarCondition2[1])
                {
                    print("Sword ready to strike from the heavens");
                    
                    // If player then moves hand to the bottom-middle section
                    if (Input.GetKeyDown(KeyCode.C)) folkvarCondition2[2] = true;

                    // If player instead moves hand to a different location
                    if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Z))
                    {
                        folkvarCondition2[1] = false;
                        folkvarCondition2[0] = false;
                    }
                }
                else
                {
                    // If player instead moves hand to a different location
                    if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Z))
                    {
                        folkvarCondition2[0] = false;
                    }
                }
            }


            // Third attack
            // If player starts by placing their hand in the bottom-left corner
            if (MarkerManagerScript.currentLocation == 7) folkvarCondition3[0] = true;

                if (folkvarCondition3[0] && !folkvarCondition3[1])
                {
                    Debug.Log("Point 1");
                    
                    // If player then places their their hand in the top-middle corner
                    if (Input.GetKeyDown(KeyCode.W)) folkvarCondition3[1] = true;

                    print(folkvarCondition1[1]);

                    // If player instead places their hand in any other corner
                    if (!folkvarCondition3[1])
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            folkvarCondition3[0] = false;
                        } 
                    }
                }
                
                // If player has then placed hand in the top-middle corner
                if (folkvarCondition3[1] && !folkvarCondition3[2])
                {
                    Debug.Log("Point 2");
                    
                    // If player then places their their hand in the bottom-right corner
                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        Debug.Log("Point 3");
                        
                        // The player has successfully made the pattern
                        folkvarCondition3[2] = true;
                    }

                    // If player instead places their hand in any other corner
                    if (!folkvarCondition3[2])
                    {
                        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A))
                        {
                            folkvarCondition3[0] = false;
                            folkvarCondition3[1] = false;
                        }
                    }
                }
                
                
                
                // Check to see if the player decided to move for their turn instead
                if (MarkerManagerScript.goMarker)
                {
                    if (Input.GetKeyDown(KeyCode.V)) folkvarCondition4[0] = true;
                }
            
                if (folkvarCondition4[0])
                {
                    // If the player decides to move Folkvar to the left
                    if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Z))
                    {
                        if (CharacterManagerScript.MoveFolkvar(1) == 1)
                        {
                            folkvarCondition4[1] = true;
                        }
                        
                        // If they cannot move in that direction
                        if (CharacterManagerScript.MoveFolkvar(1) == 2)
                        {
                            print("Choose another direction to move in");
                            folkvarCondition4[0] = false;
                        }
                    }
                    
                    // If the player decides to move Folkvar to the right
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.C))
                    {
                        if (CharacterManagerScript.MoveFolkvar(2) == 1)
                        {
                            folkvarCondition4[2] = true;
                        }
                        
                        // If they cannot move in that direction
                        if (CharacterManagerScript.MoveFolkvar(2) == 2)
                        {
                            print("Choose another direction to move in");
                            folkvarCondition4[0] = false;
                        }
                    }
                }
                else
                {
                    folkvarCondition4[1] = false;
                    folkvarCondition4[2] = false;
                }



            // check to see if Folkvar has made any attacks
            CombatManagerScript.FolkvarMeleeAttack();
            
            
            // Reset variables if an attack is canceled
            if (MarkerManagerScript.undoMarker)
            {
                if (Input.GetKeyDown(KeyCode.U))
                {
                    ResetFolkvarVariables();
                }
            }
        }
    }



    public static void ResetFolkvarVariables()
    {
        folkvarCondition1[0] = false;
        folkvarCondition1[1] = false;

        folkvarCondition2[0] = false;
        folkvarCondition2[1] = false;
        folkvarCondition2[2] = false;

        folkvarCondition3[0] = false;
        folkvarCondition3[1] = false;
        folkvarCondition3[2] = false;
        
        folkvarCondition4[0] = false;
        folkvarCondition4[1] = false;
        folkvarCondition4[2] = false;
    }
}
