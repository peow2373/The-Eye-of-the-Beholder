using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class NetrixiCombatScript : MonoBehaviour
{
    // Conditions for each of Netrixi's attacks
    public static bool[] netrixiCondition1 = new bool[] {false, false};
    public static bool[] netrixiCondition2 = new bool[] {false, false};
    public static bool[] netrixiCondition3 = new bool[] {false, false, false, false, false};
    public static bool[] netrixiCondition4 = new bool[] {false, false, false};

    public static int netrixiRotation = 0;

    public static int targetLocation = 0;

    // Start is called before the first frame update
    void Start()
    {
        netrixiRotation = 0;
        targetLocation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (netrixiRotation != 0)
        {
            targetLocation = netrixiRotation;
        }
    }
    
    public static void NetrixiAttack()
    {
        if (CombatManagerScript.netrixiAttacks)
        {
            // First spell
            // If player pulls their hand back first
            if (Input.GetKeyDown(KeyCode.F)) netrixiCondition1[0] = true;

            if (netrixiCondition1[0])
            {
                // If player then moves hand close to the webcam
                if (Input.GetKeyDown(KeyCode.N)) netrixiCondition1[1] = true;

                // If player instead moves hand back to a resting position
                else if (Input.GetKeyDown(KeyCode.M)) netrixiCondition1[0] = false;
            }


            // Second spell
            // If player starts to rotate their hand first
            if (GameManagerScript.rightHanded)
            {
                if (Input.GetKeyDown(KeyCode.K)) netrixiCondition2[0] = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.J)) netrixiCondition2[0] = true;
            }
            
            
            if (netrixiCondition2[0])
            {
                // If player then rotates hand to desired location and locks in their choice
                if (Input.GetKeyDown(KeyCode.V)) netrixiCondition2[1] = true;
                
                
                // If player then rotates hand to the first zone
                if (Input.GetKeyDown(KeyCode.L)) netrixiRotation = 1;
                
                // If player then rotates hand to the second zone
                if (Input.GetKeyDown(KeyCode.G)) netrixiRotation = 2;
                
                // If player then rotates hand to the third zone
                if (Input.GetKeyDown(KeyCode.B)) netrixiRotation = 3;
                
                // If player then rotates hand to the fourth zone
                if (Input.GetKeyDown(KeyCode.T)) netrixiRotation = 4;
                
                // If player then rotates hand to the fifth zone
                if (Input.GetKeyDown(KeyCode.R)) netrixiRotation = 5;
            }
            
            
            // Third spell
            // If the player is right-handed
            if (GameManagerScript.rightHanded)
            {
                // If player starts by placing their hand in the middle-right corner
                if (MarkerManagerScript.currentLocation == 6) netrixiCondition3[0] = true;

                if (netrixiCondition3[0] && !netrixiCondition3[1])
                {
                    Debug.Log("Point 1");
                
                    // If player then places their their hand in the bottom-middle corner
                    if (Input.GetKeyDown(KeyCode.X)) netrixiCondition3[1] = true;

                    // If player instead places their hand in any other corner
                    if (!netrixiCondition3[1])
                    {
                        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
                        {
                            netrixiCondition3[0] = false;
                        } 
                    }
                }
            
                // If player has then placed hand in the bottom-middle corner
                if (netrixiCondition3[1] && !netrixiCondition3[2])
                {
                    Debug.Log("Point 2");
                
                    // If player then places their their hand in the middle-left corner
                    if (Input.GetKeyDown(KeyCode.A)) netrixiCondition3[2] = true;

                    // If player instead places their hand in any other corner
                    if (!netrixiCondition3[2])
                    {
                        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
                        {
                            netrixiCondition3[0] = false;
                            netrixiCondition3[1] = false;
                        }
                    }
                }
            
                // If player has then placed hand in the middle-left corner
                if (netrixiCondition3[2] && !netrixiCondition3[3])
                {
                    Debug.Log("Point 3");
                    
                    // If player then places their their hand in the top-middle corner
                    if (Input.GetKeyDown(KeyCode.W)) netrixiCondition3[3] = true;

                    // If player instead places their hand in any other corner
                    if (!netrixiCondition3[3])
                    {
                        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
                        {
                            netrixiCondition3[0] = false;
                            netrixiCondition3[1] = false;
                            netrixiCondition3[2] = false;
                        }
                    }
                }

                // If the player has successfully made the pattern
                if (netrixiCondition3[3])
                {
                    Debug.Log("Point 4");
                    
                    if (Input.GetKeyDown(KeyCode.V)) netrixiCondition3[4] = true;
                }
            }
            
            // if the player is left-handed
            else
            {
                // If player starts by placing their hand in the middle-left corner
                if (MarkerManagerScript.currentLocation == 4) netrixiCondition3[0] = true;

                if (netrixiCondition3[0] && !netrixiCondition3[1])
                {
                    Debug.Log("Point 1");
                
                    // If player then places their their hand in the bottom-middle corner
                    if (Input.GetKeyDown(KeyCode.X)) netrixiCondition3[1] = true;

                    // If player instead places their hand in any other corner
                    if (!netrixiCondition3[1])
                    {
                        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
                        {
                            netrixiCondition3[0] = false;
                        } 
                    }
                }
            
                // If player has then placed hand in the bottom-middle corner
                if (netrixiCondition3[1] && !netrixiCondition3[2])
                {
                    Debug.Log("Point 2");
                
                    // If player then places their their hand in the middle-right corner
                    if (Input.GetKeyDown(KeyCode.D)) netrixiCondition3[2] = true;

                    // If player instead places their hand in any other corner
                    if (!netrixiCondition3[2])
                    {
                        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
                        {
                            netrixiCondition3[0] = false;
                            netrixiCondition3[1] = false;
                        }
                    }
                }
            
                // If player has then placed hand in the middle-right corner
                if (netrixiCondition3[2] && !netrixiCondition3[3])
                {
                    Debug.Log("Point 3");
                    
                    // If player then places their their hand in the top-middle corner
                    if (Input.GetKeyDown(KeyCode.W)) netrixiCondition3[3] = true;

                    // If player instead places their hand in any other corner
                    if (!netrixiCondition3[3])
                    {
                        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
                        {
                            netrixiCondition3[0] = false;
                            netrixiCondition3[1] = false;
                            netrixiCondition3[2] = false;
                        }
                    }
                }

                // If the player has successfully made the pattern
                if (netrixiCondition3[3])
                {
                    Debug.Log("Point 4");
                    
                    if (Input.GetKeyDown(KeyCode.V)) netrixiCondition3[4] = true;
                }
            }


            
            // Check to see if the player decided to move for their turn instead
            if (MarkerManagerScript.goMarker)
            {
                if (Input.GetKeyDown(KeyCode.V)) netrixiCondition4[0] = true;
            }
            
            if (netrixiCondition4[0])
            {
                // If the player decides to move Netrixi to the left
                if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Z))
                {
                    if (CharacterManagerScript.MoveNetrixi(1) == 1)
                    {
                        netrixiCondition4[1] = true;
                    }
                    
                    // If they cannot move in that direction
                    if (CharacterManagerScript.MoveNetrixi(1) == 2)
                    {
                        print("Choose another direction to move in");
                        netrixiCondition4[0] = false;
                    }
                }
                    
                // If the player decides to move Netrixi to the right
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.C))
                {
                    if (CharacterManagerScript.MoveNetrixi(2) == 1)
                    {
                        netrixiCondition4[2] = true;
                    }
                    
                    // If they cannot move in that direction
                    if (CharacterManagerScript.MoveNetrixi(2) == 2)
                    {
                        print("Choose another direction to move in");
                        netrixiCondition4[0] = false;
                    }
                }
            }
            else
            {
                netrixiCondition4[1] = false;
                netrixiCondition4[2] = false;
            }


            
            // Check to see if Netrixi can cast any spells
            CombatManagerScript.NetrixiSpellCast();
            
            
            // Reset variables if a spell is canceled
            if (MarkerManagerScript.undoMarker)
            {
                if (Input.GetKeyDown(KeyCode.U))
                {
                    ResetNetrixiVariables();
                }
            }
        }
    }



    public static void ResetNetrixiVariables()
    {
        netrixiCondition1[0] = false;
        netrixiCondition1[1] = false;

        netrixiCondition2[0] = false;
        netrixiCondition2[1] = false;
        
        netrixiRotation = 0;
                
        netrixiCondition3[0] = false;
        netrixiCondition3[1] = false;
        netrixiCondition3[2] = false;
        netrixiCondition3[3] = false;
        netrixiCondition3[4] = false;
        
        
        netrixiCondition4[0] = false;
        netrixiCondition4[1] = false;
        netrixiCondition4[2] = false;
    }
}
