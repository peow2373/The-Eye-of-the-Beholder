using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandAnimationManager : MonoBehaviour
{
    public static bool animation1Playing;
    public static bool animation2Playing;
    public static bool animation3Playing;

    public static bool startAnimation;

    public GameObject animation1, animation2, animation3;

    public Text optionText1, optionText2, optionText3;
    private string originalText1, originalText2, originalText3;

    public static bool restartAnimation;

    public static bool netrixiAttack1, netrixiAttack2, netrixiAttack3;
    public static bool folkvarAttack1, folkvarAttack2, folkvarAttack3;
    public static bool ivAttack1, ivAttack2, ivAttack3;

    public static string animationName1, animationName2, animationName3;

    public static int originalScene;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if any animations are deactivated
        if (!animation1.activeSelf) animation1Playing = false;
        if (!animation2.activeSelf) animation2Playing = false;
        if (!animation3.activeSelf) animation3Playing = false;

        // Check to see if all hand animations have ended
        if (!animation1Playing && !animation2Playing && !animation3Playing)
        {
            startAnimation = true;

            //ChangeHandAnimation.handMovementOrder++;
            //if (ChangeHandAnimation.handMovementOrder == 4) ChangeHandAnimation.handMovementOrder = 1;
            ChangeHandAnimation.handMovementOrder = 2;
        }
        else
        {
            startAnimation = false;
        }

        CheckForChanges();
        CheckForCombatChanges();

        if (ChangeHandAnimation.animationName1 != "") animationName1 = ChangeHandAnimation.animationName1;
        if (ChangeHandAnimation.animationName2 != "") animationName2 = ChangeHandAnimation.animationName2;
        if (ChangeHandAnimation.animationName3 != "") animationName3 = ChangeHandAnimation.animationName3;

        // If the animations need to be restarted
        if (restartAnimation)
        {
            ChangeHandAnimation.restartAnimation = true;

            animation1Playing = false;
            animation2Playing = false;
            animation3Playing = false;

            restartAnimation = false;
        }
        else
        {
            ChangeHandAnimation.restartAnimation = false;
        }
        
        
        // If the Undo button is pressed
        if (MarkerManagerScript.undoMarker)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                ChangeHandAnimation.animationName1 = animationName1;
                ChangeHandAnimation.animationName2 = animationName2;
                ChangeHandAnimation.animationName3 = animationName3;

                ChangeChoiceText.S.handAnimation1.SetActive(true);
                ChangeChoiceText.S.handAnimation2.SetActive(true);
                ChangeChoiceText.S.handAnimation3.SetActive(true);

                restartAnimation = true;
            }
        }
        
        // If the Combat simulation is running
        if (CombatManagerScript.hasRunSimulation) TipScript.S.Reset();
    }

    void CheckForChanges()
    {
        // If Option 1 is different from before
        if (optionText1.GetComponent<Text>().text != "")
        {
            if (originalText1 != optionText1.GetComponent<Text>().text)
            {
                ChangeHandAnimation.animationName1 = "";
                restartAnimation = true;
            }
        }
        
        // If Option 2 is different from before
        if (optionText2.GetComponent<Text>().text != "")
        {
            if (originalText2 != optionText2.GetComponent<Text>().text)
            {
                ChangeHandAnimation.animationName2 = "";
                restartAnimation = true;
            }
        }
        
        // If Option 3 is different from before
        if (optionText3.GetComponent<Text>().text != "")
        {
            if (originalText3 != optionText3.GetComponent<Text>().text)
            {
                ChangeHandAnimation.animationName3 = "";
                restartAnimation = true;
            }
        }
        
        // If the scene has changed
        if (originalScene != GameManagerScript.currentScene) restartAnimation = true;
        originalScene = GameManagerScript.currentScene;

        originalText1 = optionText1.GetComponent<Text>().text;
        originalText2 = optionText2.GetComponent<Text>().text;
        originalText3 = optionText3.GetComponent<Text>().text;
    }


    void CheckForCombatChanges()
    {
        if (GameManagerScript.inCombat)
        {
            // Check Netrixi's attacks
            if (CombatManagerScript.netrixiAttacks)
            {
                // If Attack 1 changes
                if (NetrixiCombatScript.netrixiCondition1[0])
                {
                    if (NetrixiCombatScript.netrixiCondition1[1])
                    {
                        if (netrixiAttack1)
                        {
                            HighlightChoices.S.HighlightChoice(1,3);

                            netrixiAttack1 = false;
                            restartAnimation = true;
                        }
                    }
                    else
                    {
                        if (!netrixiAttack1)
                        {
                            HighlightChoices.S.HighlightChoice(1,3);
                            if (TipScript.attackPlaying != 1) StartCoroutine(TipScript.S.DisplayAttackTip(1));

                            netrixiAttack1 = true;
                            restartAnimation = true;
                        }
                    }
                }
                else
                {
                    if (netrixiAttack1)
                    {
                        HighlightChoices.S.HighlightChoice(1,3);

                        netrixiAttack1 = false;
                        restartAnimation = true;
                    }
                }
                
                // If Attack 2 changes
                if (NetrixiCombatScript.netrixiCondition2[0])
                {
                    if (NetrixiCombatScript.netrixiCondition2[1])
                    {
                        if (netrixiAttack2)
                        {
                            HighlightChoices.S.HighlightChoice(2,3);

                            netrixiAttack2 = false;
                            restartAnimation = true;
                        }
                    }
                    else
                    {
                        if (!netrixiAttack2)
                        {
                            HighlightChoices.S.HighlightChoice(2,3);
                            if (TipScript.attackPlaying != 2) StartCoroutine(TipScript.S.DisplayAttackTip(2));

                            netrixiAttack2 = true;
                            restartAnimation = true;
                        }
                    }
                }
                else
                {
                    if (netrixiAttack2)
                    {
                        HighlightChoices.S.HighlightChoice(2,3);
                        
                        netrixiAttack2 = false;
                        restartAnimation = true;
                    }
                }
                
                // If Attack 3 changes
                if (NetrixiCombatScript.netrixiCondition3[0])
                {
                    if (NetrixiCombatScript.netrixiCondition3[1])
                    {
                        if (NetrixiCombatScript.netrixiCondition3[2])
                        {
                            if (NetrixiCombatScript.netrixiCondition3[3])
                            {
                                if (netrixiAttack3)
                                {
                                    HighlightChoices.S.HighlightChoice(3,3);
                                    
                                    netrixiAttack3 = false;
                                    restartAnimation = true;
                                }
                            }
                            else
                            {
                                if (!netrixiAttack3)
                                {
                                    HighlightChoices.S.HighlightChoice(3,3);

                                    netrixiAttack3 = true;
                                    restartAnimation = true;
                                }
                            }
                        }
                        else
                        {
                            if (netrixiAttack3)
                            {
                                HighlightChoices.S.HighlightChoice(3,3);

                                netrixiAttack3 = false;
                                restartAnimation = true;
                            }
                        }
                    }
                    else
                    {
                        if (!netrixiAttack3)
                        {
                            HighlightChoices.S.HighlightChoice(3,3);
                            if (TipScript.attackPlaying != 3) StartCoroutine(TipScript.S.DisplayAttackTip(3));
                            
                            netrixiAttack3 = true;
                            restartAnimation = true;
                        }
                    }
                }
                else
                {
                    if (netrixiAttack3)
                    {
                        HighlightChoices.S.HighlightChoice(3,3);
                        
                        netrixiAttack3 = false;
                        restartAnimation = true;
                    }
                }
            }
            
            else if (CombatManagerScript.folkvarAttacks)
            {
                // Check Folkvar's attacks
                if (CombatManagerScript.folkvarAttacks)
                {
                    // If Attack 1 changes
                    if (FolkvarCombatScript.folkvarCondition1[0])
                    {
                        if (FolkvarCombatScript.folkvarCondition1[1])
                        {
                            if (folkvarAttack1)
                            {
                                HighlightChoices.S.HighlightChoice(1,3);
                                
                                folkvarAttack1 = false;
                                restartAnimation = true;
                            }
                        }
                        else
                        {
                            if (!folkvarAttack1)
                            {
                                HighlightChoices.S.HighlightChoice(1,3);
                                if (TipScript.attackPlaying != 4) StartCoroutine(TipScript.S.DisplayAttackTip(4));
                                
                                folkvarAttack1 = true;
                                restartAnimation = true;
                            }
                        }
                    }
                    else
                    {
                        if (folkvarAttack1)
                        {
                            HighlightChoices.S.HighlightChoice(1,3);
                            
                            folkvarAttack1 = false;
                            restartAnimation = true;
                        }
                    }
                    
                    // If Attack 2 changes
                    if (FolkvarCombatScript.folkvarCondition2[0])
                    {
                        if (FolkvarCombatScript.folkvarCondition2[1])
                        {
                            if (FolkvarCombatScript.folkvarCondition2[2])
                            {
                                if (!folkvarAttack2)
                                {
                                    HighlightChoices.S.HighlightChoice(2,3);
                                    
                                    folkvarAttack2 = true;
                                    restartAnimation = true;
                                }
                            }
                            else
                            {
                                if (folkvarAttack2)
                                {
                                    HighlightChoices.S.HighlightChoice(2,3);

                                    folkvarAttack2 = false;
                                    restartAnimation = true;
                                }
                            }
                        }
                        else
                        {
                            if (!folkvarAttack2)
                            {
                                HighlightChoices.S.HighlightChoice(2,3);
                                if (TipScript.attackPlaying != 5) StartCoroutine(TipScript.S.DisplayAttackTip(5));
                                
                                folkvarAttack2 = true;
                                restartAnimation = true;
                            }
                        }
                    }
                    else
                    {
                        if (folkvarAttack2)
                        {
                            HighlightChoices.S.HighlightChoice(2,3);
                            
                            folkvarAttack2 = false;
                            restartAnimation = true;
                        }
                    }
                    
                    // If Attack 3 changes
                    if (FolkvarCombatScript.folkvarCondition3[0])
                    {
                        if (FolkvarCombatScript.folkvarCondition3[1])
                        {
                            if (FolkvarCombatScript.folkvarCondition3[2])
                            {
                                if (!folkvarAttack3)
                                {
                                    if (GameManagerScript.currentScene >= 18)
                                    {
                                        HighlightChoices.S.HighlightChoice(3,3);
                                    }
                                    
                                    folkvarAttack3 = true;
                                    restartAnimation = true;
                                }
                            }
                            else
                            {
                                if (folkvarAttack3)
                                {
                                    if (GameManagerScript.currentScene >= 18)
                                    {
                                        HighlightChoices.S.HighlightChoice(3,3);
                                    }

                                    folkvarAttack3 = false;
                                    restartAnimation = true;
                                }
                            }
                        }
                        else
                        {
                            if (!folkvarAttack3)
                            {
                                if (GameManagerScript.currentScene >= 18)
                                {
                                    HighlightChoices.S.HighlightChoice(3,3);
                                    if (TipScript.attackPlaying != 6) StartCoroutine(TipScript.S.DisplayAttackTip(6));
                                }
                                
                                folkvarAttack3 = true;
                                restartAnimation = true;
                            }
                        }
                    }
                    else
                    {
                        if (folkvarAttack3)
                        {
                            if (GameManagerScript.currentScene >= 18)
                            {
                                HighlightChoices.S.HighlightChoice(3,3);
                            }

                            folkvarAttack3 = false;
                            restartAnimation = true;
                        }
                    }
                }
            }
            
            else if (CombatManagerScript.ivAttacks)
            {
                // Check Iv's attacks
                if (CombatManagerScript.ivAttacks)
                {
                    // If Attack 1 changes
                    if (IvCombatScript.ivCondition1[0])
                    {
                        if (IvCombatScript.ivCondition1[1])
                        {
                            if (ivAttack1)
                            {
                                HighlightChoices.S.HighlightChoice(1,3);
                                
                                ivAttack1 = false;
                                restartAnimation = true;
                            }
                        }
                        else
                        {
                            if (!ivAttack1)
                            {
                                HighlightChoices.S.HighlightChoice(1,3);
                                if (TipScript.attackPlaying != 7) StartCoroutine(TipScript.S.DisplayAttackTip(7));
                                
                                ivAttack1 = true;
                                restartAnimation = true;
                            }
                        }
                    }
                    else
                    {
                        if (ivAttack1)
                        {
                            HighlightChoices.S.HighlightChoice(1,3);
                            
                            ivAttack1 = false;
                            restartAnimation = true;
                        }
                    }
                    
                    // If Attack 2 changes
                    if (IvCombatScript.ivCondition2[0])
                    {
                        if (IvCombatScript.ivCondition2[1])
                        {
                            if (IvCombatScript.ivCondition2[2])
                            {
                                if (IvCombatScript.ivCondition2[3])
                                {
                                    if (ivAttack2)
                                    {
                                        HighlightChoices.S.HighlightChoice(2,3);
                                        
                                        ivAttack2 = false;
                                        restartAnimation = true;
                                    }
                                }
                                else
                                {
                                    if (!ivAttack2)
                                    {
                                        HighlightChoices.S.HighlightChoice(2,3);

                                        ivAttack2 = true;
                                        restartAnimation = true;
                                    }
                                }
                            }
                            else
                            {
                                if (ivAttack2)
                                {
                                    HighlightChoices.S.HighlightChoice(2,3);

                                    ivAttack2 = false;
                                    restartAnimation = true;
                                }
                            }
                        }
                        else
                        {
                            if (!ivAttack2)
                            {
                                HighlightChoices.S.HighlightChoice(2,3);
                                if (TipScript.attackPlaying != 8) StartCoroutine(TipScript.S.DisplayAttackTip(8));
                                
                                ivAttack2 = true;
                                restartAnimation = true;
                            }
                        }
                    }
                    else
                    {
                        if (ivAttack2)
                        {
                            HighlightChoices.S.HighlightChoice(2,3);
                            
                            ivAttack2 = false;
                            restartAnimation = true;
                        }
                    }
                }
                
                // If Attack 3 changes
                if (IvCombatScript.ivCondition3[0])
                {
                    if (IvCombatScript.ivCondition3[1])
                    {
                        if (ivAttack3)
                        {
                            if (GameManagerScript.currentScene >= 18)
                            {
                                HighlightChoices.S.HighlightChoice(3,3);
                            }

                            ivAttack3 = false;
                            restartAnimation = true;
                        }
                    }
                    else
                    {
                        if (!ivAttack3)
                        {
                            if (GameManagerScript.currentScene >= 18)
                            {
                                HighlightChoices.S.HighlightChoice(3,3);
                                if (TipScript.attackPlaying != 9) StartCoroutine(TipScript.S.DisplayAttackTip(9)); 
                            }

                            ivAttack3 = true;
                            restartAnimation = true;
                        }
                    }
                }
                else
                {
                    if (ivAttack3)
                    {
                        if (GameManagerScript.currentScene >= 18)
                        {
                            HighlightChoices.S.HighlightChoice(3,3);
                        }

                        ivAttack3 = false;
                        restartAnimation = true;
                    }
                }
            }
        }
    }
}
