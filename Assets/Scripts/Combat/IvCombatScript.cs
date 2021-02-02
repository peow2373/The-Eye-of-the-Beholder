using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvCombatScript : MonoBehaviour
{
    // Conditions for each of Iv's attacks
    public static bool[] ivCondition1 = new bool[] {false, false};
    public static bool[] ivCondition2 = new bool[] {false, false};
    public static bool[] ivCondition3 = new bool[] {false, false, false, false};

    public static int ivRotation = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        ivRotation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static void IvBlock()
    {
        if (CombatManagerScript.ivAttacks)
        {
            // First spell
            // If player pulls their hand back first
            if (Input.GetKeyDown(KeyCode.F)) ivCondition1[0] = true;

            if (ivCondition1[0])
            {
                // If player then moves hand close to the webcam
                if (Input.GetKeyDown(KeyCode.N)) ivCondition1[1] = true;

                // If player instead moves hand back to a resting position
                else if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.U)) ivCondition1[0] = false;
            }


            // Second spell
            // If player starts to rotate their hand first
            if (Input.GetKeyDown(KeyCode.K)) ivCondition2[0] = true;
            
            if (ivCondition2[0])
            {
                // If player then rotates hand to desired location and locks in their choice
                if (Input.GetKeyDown(KeyCode.V)) ivCondition2[1] = true;
                
                
                // If player then rotates hand to the first or second zone
                if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.G)) ivRotation = 1;
                
                // If player then rotates hand to the third or fourth zone
                if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.T)) ivRotation = 2;
                
                // If player then rotates hand to the fifth zone
                if (Input.GetKeyDown(KeyCode.R)) ivRotation = 3;
                

                // If player instead exits rotating mode
                else if (Input.GetKeyDown(KeyCode.U))
                {
                    ivCondition2[0] = false;
                    ivRotation = 0;
                }
            }


            // Third spell
            // If the player is right-handed
            if (MarkerManagerScript.rightHanded)
            {
                // If player starts by placing their hand in the middle-right corner
                if (Input.GetKeyDown(KeyCode.D)) ivCondition3[0] = true;

                if (ivCondition3[0] && !ivCondition3[1])
                {
                    Debug.Log("Point 1");
                
                    // If player then places their their hand in the middle-left corner
                    if (Input.GetKeyDown(KeyCode.A)) ivCondition3[1] = true;

                    // If player instead places their hand in any other corner
                    if (!ivCondition3[1])
                    {
                        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.U))
                        {
                            ivCondition3[0] = false;
                        } 
                    }
                }
            
                // If player has then placed hand in the middle-left corner
                if (ivCondition3[1] && !ivCondition3[2])
                {
                    Debug.Log("Point 2");
                
                    // If player then places their their hand in the top-middle corner
                    if (Input.GetKeyDown(KeyCode.W)) ivCondition3[2] = true;

                    // If player instead places their hand in any other corner
                    if (!ivCondition3[2])
                    {
                        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.U))
                        {
                            ivCondition3[0] = false;
                            ivCondition3[1] = false;
                        }
                    }
                }
            
                // If player has then placed hand in the top-middle corner
                if (ivCondition3[2] && !ivCondition3[3])
                {
                    Debug.Log("Point 3");
                    
                    // If player then places their their hand in the bottom-middle corner
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        Debug.Log("Point 4");
                        
                        // The player has successfully made the pattern
                        ivCondition3[3] = true;
                    }

                    // If player instead places their hand in any other corner
                    if (!ivCondition3[3])
                    {
                        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.U))
                        {
                            ivCondition3[0] = false;
                            ivCondition3[1] = false;
                            ivCondition3[2] = false;
                        }
                    }
                }
            }
            
            // if the player is left-handed
            else
            {
                // If player starts by placing their hand in the middle-left corner
                if (Input.GetKeyDown(KeyCode.A)) ivCondition3[0] = true;

                if (ivCondition3[0] && !ivCondition3[1])
                {
                    Debug.Log("Point 1");
                
                    // If player then places their their hand in the middle-right corner
                    if (Input.GetKeyDown(KeyCode.D)) ivCondition3[1] = true;

                    // If player instead places their hand in any other corner
                    if (!ivCondition3[1])
                    {
                        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.U))
                        {
                            ivCondition3[0] = false;
                        } 
                    }
                }
            
                // If player has then placed hand in the middle-left corner
                if (ivCondition3[1] && !ivCondition3[2])
                {
                    Debug.Log("Point 2");
                
                    // If player then places their their hand in the top-middle corner
                    if (Input.GetKeyDown(KeyCode.W)) ivCondition3[2] = true;

                    // If player instead places their hand in any other corner
                    if (!ivCondition3[2])
                    {
                        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.U))
                        {
                            ivCondition3[0] = false;
                            ivCondition3[1] = false;
                        }
                    }
                }
            
                // If player has then placed hand in the top-middle corner
                if (ivCondition3[2] && !ivCondition3[3])
                {
                    Debug.Log("Point 3");
                    
                    // If player then places their their hand in the bottom-middle corner
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        Debug.Log("Point 4");
                        
                        // The player has successfully made the pattern
                        ivCondition3[3] = true;
                    }

                    // If player instead places their hand in any other corner
                    if (!ivCondition3[3])
                    {
                        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.U))
                        {
                            ivCondition3[0] = false;
                            ivCondition3[1] = false;
                            ivCondition3[2] = false;
                        }
                    }
                }
            }


            
            // check to see if Iv can perform any actions
            CombatManagerScript.IvTeamSupport();
            
            // Reset variables if an action is canceled or completed
            if (Input.GetKeyDown(KeyCode.U))
            {
                ResetIvVariables();
            }
        }
    }



    public static void ResetIvVariables()
    {
        ivCondition1[0] = false;
        ivCondition1[1] = false;

        ivCondition2[0] = false;
        ivCondition2[1] = false;
        
        ivRotation = 0;

        ivCondition3[0] = false;
        ivCondition3[1] = false;
        ivCondition3[2] = false;
        ivCondition3[3] = false;
    }
}
