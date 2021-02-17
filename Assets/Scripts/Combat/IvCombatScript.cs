using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvCombatScript : MonoBehaviour
{
    // Conditions for each of Iv's attacks
    public static bool[] ivCondition1 = new bool[] {false, false};
    public static bool[] ivCondition2 = new bool[] {false, false, false, false};
    public static bool[] ivCondition3 = new bool[] {false, false};
    public static bool[] ivCondition4 = new bool[] {false, false, false};

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
    
    public static void IvAction()
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
                else if (Input.GetKeyDown(KeyCode.M)) ivCondition1[0] = false;
            }
            
            
            // Second spell
            // If the player is right-handed
            if (GameManagerScript.rightHanded)
            {
                // If player starts by placing their hand in the middle-right corner
                if (Input.GetKeyDown(KeyCode.D)) ivCondition2[0] = true;

                if (ivCondition2[0] && !ivCondition2[1])
                {
                    Debug.Log("Point 1");

                    // If player then places their their hand in the middle-left corner
                    if (Input.GetKeyDown(KeyCode.A)) ivCondition2[1] = true;

                    // If player instead places their hand in any other corner
                    if (!ivCondition2[1])
                    {
                        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S) ||
                            Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
                        {
                            ivCondition2[0] = false;
                        }
                    }
                }
            }
            
            // If the player is left-handed
            else
            {
                // If player starts by placing their hand in the middle-left corner
                if (Input.GetKeyDown(KeyCode.A)) ivCondition2[0] = true;

                if (ivCondition2[0] && !ivCondition2[1])
                {
                    Debug.Log("Point 1");
                
                    // If player then places their their hand in the middle-right corner
                    if (Input.GetKeyDown(KeyCode.D)) ivCondition2[1] = true;

                    // If player instead places their hand in any other corner
                    if (!ivCondition2[1])
                    {
                        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
                        {
                            ivCondition2[0] = false;
                        } 
                    }
                }
            }
            
            // If player has completed the first two moves
            if (ivCondition2[1] && !ivCondition2[2])
            {
                Debug.Log("Point 2");

                // If player then places their their hand in the top-middle corner
                if (Input.GetKeyDown(KeyCode.W)) ivCondition2[2] = true;

                // If player instead places their hand in any other corner
                if (!ivCondition2[2])
                {
                    if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S) ||
                        Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
                    {
                        ivCondition2[0] = false;
                        ivCondition2[1] = false;
                    }
                }
            }

            // If player has then placed hand in the top-middle corner
            if (ivCondition2[2] && !ivCondition2[3])
            {
                Debug.Log("Point 3");
                    
                // If player then places their their hand in the bottom-middle corner
                if (Input.GetKeyDown(KeyCode.X))
                {
                    Debug.Log("Point 4");
                        
                    // The player has successfully made the pattern
                    ivCondition2[3] = true;
                }

                // If player instead places their hand in any other corner
                if (!ivCondition2[3])
                {
                    if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
                    {
                        ivCondition2[0] = false;
                        ivCondition2[1] = false;
                        ivCondition2[2] = false;
                    }
                }
            }
            


            // Third spell
            // If player starts to rotate their hand first
            if (GameManagerScript.rightHanded)
            {
                if (Input.GetKeyDown(KeyCode.K)) ivCondition3[0] = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.J)) ivCondition3[0] = true;
            }
            
            
            if (ivCondition3[0])
            {
                // If player then rotates hand to desired location and locks in their choice
                if (Input.GetKeyDown(KeyCode.V)) ivCondition3[1] = true;
                
                
                // If player then rotates hand to the first or second zone
                if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.G)) ivRotation = 1;
                
                // If player then rotates hand to the third or fourth zone
                if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.T)) ivRotation = 2;
                
                // If player then rotates hand to the fifth zone
                if (Input.GetKeyDown(KeyCode.R)) ivRotation = 3;
                
            }



            // Check to see if the player decided to move for their turn instead
            if (MarkerManagerScript.goMarker)
            {
                if (Input.GetKeyDown(KeyCode.V)) ivCondition4[0] = true;
            }
            
            if (ivCondition4[0])
            {
                // If the player decides to move Folkvar to the left
                if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Z))
                {
                    if (CharacterManagerScript.MoveIv(1) == 1)
                    {
                        ivCondition4[1] = true;
                    }
                    
                    // If they cannot move in that direction
                    if (CharacterManagerScript.MoveIv(1) == 2)
                    {
                        ivCondition4[0] = false;
                    }
                }
                    
                // If the player decides to move Folkvar to the right
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.C))
                {
                    if (CharacterManagerScript.MoveIv(2) == 1)
                    {
                        ivCondition4[2] = true;
                    }
                    
                    // If they cannot move in that direction
                    if (CharacterManagerScript.MoveIv(2) == 2)
                    {
                        ivCondition4[0] = false;
                    }
                }
            }
            else
            {
                ivCondition4[1] = false;
                ivCondition4[2] = false;
            }
            
            
            
            // check to see if Iv can perform any actions
            CombatManagerScript.IvTeamSupport();
            
            
            // Reset variables if an action is canceled
            if (MarkerManagerScript.backMarker)
            {
                if (Input.GetKeyDown(KeyCode.U))
                {
                    ResetIvVariables();
                }
            }
        }
    }



    public static void ResetIvVariables()
    {
        ivCondition1[0] = false;
        ivCondition1[1] = false;

        ivCondition2[0] = false;
        ivCondition2[1] = false;
        ivCondition2[2] = false;
        ivCondition2[3] = false;

        ivCondition3[0] = false;
        ivCondition3[1] = false;

        ivRotation = 0;
        
        ivCondition4[0] = false;
        ivCondition4[1] = false;
        ivCondition4[2] = false;
    }
}
