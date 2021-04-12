using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeChoiceText : MonoBehaviour
{
    public static GameObject[] choice1, choice2, choice3, choice4;
    public static GameObject[] option1, option2, option3, option4;
    
    public static bool madeChoice = false;

    public static bool talkingToBrute, talkingToIv;

    public Color nextChoice, firstChoice, secondChoice, whichChoice;
    public Color color1, color2, color3;
    public Color activeDecision, inActiveDecision;

    public static ChangeChoiceText S;

    public static string originalOption1, originalOption2, originalOption3;

    public bool canConfirmAttacks = false;

    public static string attackSelected = "Choose another move\nor character";

    public GameObject handAnimation1, handAnimation2, handAnimation3;

    public static bool justEnteredCombat = true;

    // Start is called before the first frame update
    void Awake()
    {
        S = this;
        
        choice1 = GameObject.FindGameObjectsWithTag("Choice 1");
        choice2 = GameObject.FindGameObjectsWithTag("Choice 2");
        choice3 = GameObject.FindGameObjectsWithTag("Choice 3");

        option1 = GameObject.FindGameObjectsWithTag("Option 1");
        option2 = GameObject.FindGameObjectsWithTag("Option 2");
        option3 = GameObject.FindGameObjectsWithTag("Option 3");
        option4 = GameObject.FindGameObjectsWithTag("Option 4");

        // Changing the color of the different choices
        choice1[0].GetComponent<SpriteRenderer>().color = color1;
        choice2[0].GetComponent<SpriteRenderer>().color = color2;
        choice3[0].GetComponent<SpriteRenderer>().color = color3;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.currentScene == 9)
        {
            madeChoice = false;
            talkingToBrute = false;
            talkingToIv = false;
        }

        
        // If the player is in combat instead
        if (GameManagerScript.inCombat)
        {
            ResetChoices(false);

            // Determine which characters the player can attack with
            if (!CombatManagerScript.netrixiAttacks && !CombatManagerScript.folkvarAttacks && !CombatManagerScript.ivAttacks)
            {
                if (!CombatManagerScript.hasRunSimulation)
                {
                    option1[1].GetComponent<Text>().text = "";
                    option2[1].GetComponent<Text>().text = "";
                    option3[1].GetComponent<Text>().text = "";
                    
                    // The player can attack with Netrixi
                    if (CombatManagerScript.netrixiAlive)
                    {
                        option1[1].GetComponent<Text>().text = "Attack with\nNetrixi";
                    
                        option1[0].GetComponent<SpriteRenderer>().color = color1;
                        ChangeHandAnimation.animationName1 = "Netrixi Marker";
                        handAnimation1.SetActive(true);
                        
                        //if (MarkerManagerScript.netrixiMarker) if (Input.GetKeyDown(KeyCode.Y)) HighlightChoices.S.HighlightChoice(1,3);
                    }
                    else handAnimation1.SetActive(false);

                    // The player can attack with Folkvar
                    if (CombatManagerScript.folkvarAlive)
                    {
                        option2[1].GetComponent<Text>().text = "Attack with\nFolkvar";
                    
                        option2[0].GetComponent<SpriteRenderer>().color = color2;
                        ChangeHandAnimation.animationName2 = "Folkvar Marker";
                        handAnimation2.SetActive(true);
                        
                        //if (MarkerManagerScript.folkvarMarker) if (Input.GetKeyDown(KeyCode.O)) HighlightChoices.S.HighlightChoice(2,3);
                    }
                    else handAnimation2.SetActive(false);

                    // The player can attack with Iv
                    if (CombatManagerScript.ivAlive)
                    {
                        option3[1].GetComponent<Text>().text = "Attack with\nIv";
                    
                        option3[0].GetComponent<SpriteRenderer>().color = color3;
                        ChangeHandAnimation.animationName3 = "Iv Marker";
                        handAnimation3.SetActive(true);
                        
                        //if (MarkerManagerScript.ivMarker) if (Input.GetKeyDown(KeyCode.I)) HighlightChoices.S.HighlightChoice(3,3);
                    }
                    else handAnimation3.SetActive(false);
                }
                else
                {
                    ResetChoices(true);
                }
                
                if (justEnteredCombat)
                {
                    handAnimation1.SetActive(false);
                    handAnimation2.SetActive(false);
                    handAnimation3.SetActive(false);

                    justEnteredCombat = false;
                }
            }
            else
            {
                // Determine which character the player has chosen to attack with
                if (CombatManagerScript.netrixiAttacks) DetermineCombat("Netrixi");
                if (CombatManagerScript.folkvarAttacks) DetermineCombat("Folkvar");
                if (CombatManagerScript.ivAttacks) DetermineCombat("Iv");
            }
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = activeDecision;

            justEnteredCombat = true;
        }
    }

    public void ChangeChoices(bool displayDialogueResponses, GameObject[] choices, int numberOfChoices)
    {
        // Reset how visible the text is
        option1[1].GetComponent<Text>().color = Color.white;
        option2[1].GetComponent<Text>().color = Color.white;
        option3[1].GetComponent<Text>().color = Color.white;
        
        int currentScene = GameManagerScript.currentScene;

        // Changing the text for the choices and options
        switch (numberOfChoices)
        {
            case 0:
            case 1:
                if (!GameManagerScript.endOfGame)
                {
                    if (choices.Length > 0)
                    {
                        if (choices[0].GetComponentInChildren<Text>().text == "Fight")
                        {
                            choice1[1].GetComponent<Text>().text = "Fight!";
                            
                            ChangeOptions(1, "Flip hand to\nStart Fight", "", "");
                            option1[0].GetComponent<SpriteRenderer>().color = whichChoice;
                        }
                        else
                        {
                            choice1[1].GetComponent<Text>().text = "Next...";
                            
                            ChangeOptions(1, "Flip hand to\nContinue", "", "");
                            choice1[0].GetComponent<SpriteRenderer>().color = nextChoice;
                        }
                    }
                    else
                    {
                        choice1[1].GetComponent<Text>().text = "Next...";
                        
                        ChangeOptions(1, "Flip hand to\nContinue", "", "");
                        choice1[0].GetComponent<SpriteRenderer>().color = nextChoice;
                    }
                }
                else
                {
                    choice1[1].GetComponent<Text>().text = "Restart Game?";
                
                    ChangeOptions(1, "Flip hand to\nRestart", "", "");
                    
                }
                
                ChangeHandAnimation.animationName1 = "Flip Hand";
                break;
            
            case 2:
                if (displayDialogueResponses)
                {
                    choice1[1].GetComponent<Text>().text = choices[0].GetComponentInChildren<Text>().text;
                    choice2[1].GetComponent<Text>().text = choices[1].GetComponentInChildren<Text>().text;
                    
                    ChangeOptions(2, "Move hand\nLeft", "Move hand\nRight", "");
                    
                    ChangeHandAnimation.animationName1 = "Move Left";
                    ChangeHandAnimation.animationName2 = "Move Right";
                }
                else
                {
                    if (currentScene == 8 || currentScene == 13)
                    {
                        if (!madeChoice)
                        {
                            choice1[1].GetComponent<Text>().text = "Who will you talk to?";

                            if (currentScene <= 11)
                            {
                                ChangeOptions(2, "Approach\nBrute", "Approach\nMonk", "");
                            }
                            else
                            {
                                ChangeOptions(2, "Approach\nJester", "Approach\nKing", "");
                            }
                            
                            ChangeHandAnimation.animationName1 = "Move Left";
                            ChangeHandAnimation.animationName2 = "Move Right";
                        }
                        else
                        {
                            choice1[1].GetComponent<Text>().text = "Who approaches " + WhoToApproach();
                            
                            ChangeOptions(2, "Choose\nNetrixi", "Choose\nFolkvar", "");
                            
                            ChangeHandAnimation.animationName1 = "Netrixi Marker";
                            ChangeHandAnimation.animationName2 = "Folkvar Marker";
                        }
                    }
                    else
                    {
                        choice1[1].GetComponent<Text>().text = "Who approaches " + WhoToApproach();

                        ChangeOptions(2, "Choose\nNetrixi", "Choose\nFolkvar", "");
                        
                        ChangeHandAnimation.animationName1 = "Netrixi Marker";
                        ChangeHandAnimation.animationName2 = "Folkvar Marker";
                    }
                }
                break;
            
            case 3:
                if (currentScene >= 28) choice1[1].GetComponent<Text>().text = "Who will take the stone?";
                else if (currentScene >= 26) choice1[1].GetComponent<Text>().text = "Who dares to approach " + WhoToApproach();
                else choice1[1].GetComponent<Text>().text = "Who approaches " + WhoToApproach();
                
                ChangeOptions(3, "Choose\nNetrixi", "Choose\nFolkvar", "Choose\nIv");
                
                ChangeHandAnimation.animationName1 = "Netrixi Marker";
                ChangeHandAnimation.animationName2 = "Folkvar Marker";
                ChangeHandAnimation.animationName3 = "Iv Marker";
                break;
            
            case 4:
                ChangeOptions(4, "", "", "");
                break;
        }
        
        // Changing the color of choice 1
        if (!displayDialogueResponses)
        {
            if (choices.Length > 0 && choices[0].GetComponentInChildren<Text>().text == "Fight")
            {
                choice1[0].GetComponent<SpriteRenderer>().color = whichChoice;
            }
            else
            {
                choice1[0].GetComponent<SpriteRenderer>().color = nextChoice;
            }
        }
    }


    public void ChangeOptions(int availableOptions, string text1, string text2, string text3)
    {
        option1[1].GetComponent<Text>().text = text1;
        option2[1].GetComponent<Text>().text = text2;
        option3[1].GetComponent<Text>().text = text3;

        switch (availableOptions)
        {
            case 0:
                option1[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
                option2[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
                option3[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
                break;
            
            case 1:
                if (GameManagerScript.inCombat) option1[0].GetComponent<SpriteRenderer>().color = color1;
                else option1[0].GetComponent<SpriteRenderer>().color = nextChoice;
                
                option2[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
                option3[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
                
                handAnimation1.SetActive(true);
                handAnimation2.SetActive(false);
                handAnimation3.SetActive(false);
                break;
            
            case 2:
                GameObject[] choices = GameObject.FindGameObjectsWithTag("Dialogue Choice");
                Button button = choices[0].GetComponent<Button>();
                string tempText = button.GetComponentInChildren<Text>().text;

                // Determine if the player is choosing a character or responding to dialogue
                if (tempText == "Netrixi")
                {
                    if (!GameWindowManager.metBrute)
                    {
                        option1[0].GetComponent<SpriteRenderer>().color = firstChoice;
                        option2[0].GetComponent<SpriteRenderer>().color = secondChoice;
                        
                        choice1[0].GetComponent<SpriteRenderer>().color = firstChoice;
                        choice2[0].GetComponent<SpriteRenderer>().color = secondChoice;
                    }
                    else
                    {
                        option1[0].GetComponent<SpriteRenderer>().color = color1;
                        option2[0].GetComponent<SpriteRenderer>().color = color2;
                        
                        choice1[0].GetComponent<SpriteRenderer>().color = color1;
                        choice2[0].GetComponent<SpriteRenderer>().color = color2;
                    }
                }
                else
                {
                    option1[0].GetComponent<SpriteRenderer>().color = firstChoice;
                    option2[0].GetComponent<SpriteRenderer>().color = secondChoice;
                    
                    choice1[0].GetComponent<SpriteRenderer>().color = firstChoice;
                    choice2[0].GetComponent<SpriteRenderer>().color = secondChoice;
                }
                
                option3[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
                
                handAnimation1.SetActive(true);
                handAnimation2.SetActive(true);
                handAnimation3.SetActive(false);
                break;
            
            case 3:
                option1[0].GetComponent<SpriteRenderer>().color = color1;
                option2[0].GetComponent<SpriteRenderer>().color = color2;
                option3[0].GetComponent<SpriteRenderer>().color = color3;
                
                option4[0].GetComponent<SpriteRenderer>().color = nextChoice;
                
                handAnimation1.SetActive(true);
                handAnimation2.SetActive(true);
                handAnimation3.SetActive(true);
                break;
        }
    }


    public void DetermineCombat(string character)
    {
        // Reset options
        option1[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
        option2[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
        option3[0].GetComponent<SpriteRenderer>().color = inActiveDecision;

        option1[1].GetComponent<Text>().color = Color.gray;
        option2[1].GetComponent<Text>().color = Color.gray;
        option3[1].GetComponent<Text>().color = Color.gray;
        
        option1[1].GetComponent<Text>().text = originalOption1;
        option2[1].GetComponent<Text>().text = originalOption2;
        option3[1].GetComponent<Text>().text = originalOption3;

        handAnimation1.SetActive(true);
        handAnimation2.SetActive(true);
        handAnimation3.SetActive(true);
        
        int attack1 = CombatManagerScript.firstAttack;
        int attack2 = CombatManagerScript.secondAttack;

        switch (character)
        {
            case "Netrixi":
                if (CombatManagerScript.canNetrixiAttack)
                {
                    option4[1].GetComponent<Text>().text = "Flip hand to Move Netrixi";

                    // Attack 1
                    if (attack1 != 1 && attack2 != 1)
                    {
                        option1[1].GetComponent<Text>().text = "Fireball";
                        originalOption1 = option1[1].GetComponent<Text>().text;
                        
                        option1[0].GetComponent<SpriteRenderer>().color = color1;
                        option1[1].GetComponent<Text>().color = Color.white;
                        
                        GameWindowManager.option1Centered = false;

                        if (!NetrixiCombatScript.netrixiCondition1[0])
                        {
                            ChangeHandAnimation.animationName1 = "Mid to Far";
                        }
                        else
                        {
                            ChangeHandAnimation.animationName1 = "Far to Near";
                        }
                    } 
                    else if (attack1 == 0 || attack2 == 0)
                    {
                        if (!NetrixiCombatScript.canCancelMove) handAnimation1.SetActive(false);
                        GameWindowManager.option1Centered = true;
                        option1[1].GetComponent<Text>().text = attackSelected;
                    }

                    // Attack 2
                    if (attack1 != 2 && attack2 != 2)
                    {
                        option2[1].GetComponent<Text>().text = "Lightning Strike";
                        originalOption2 = option2[1].GetComponent<Text>().text;
                        
                        option2[0].GetComponent<SpriteRenderer>().color = color2;
                        option2[1].GetComponent<Text>().color = Color.white;
                        
                        GameWindowManager.option2Centered = false;
                        
                        if (!NetrixiCombatScript.netrixiCondition2[0])
                        {
                            ChangeHandAnimation.animationName2 = "Start Rotating";
                        }
                        else
                        {
                            ChangeHandAnimation.animationName2 = "Continue Rotating";
                        }
                    }
                    else if (attack1 == 0 || attack2 == 0)
                    {
                        if (!NetrixiCombatScript.canCancelMove) handAnimation2.SetActive(false);
                        GameWindowManager.option2Centered = true;
                        option2[1].GetComponent<Text>().text = attackSelected;
                    }

                    // Attack 3
                    if (attack1 != 3 && attack2 != 3)
                    {
                        option3[1].GetComponent<Text>().text = "Transmutate";
                        originalOption3 = option3[1].GetComponent<Text>().text;
                        
                        option3[0].GetComponent<SpriteRenderer>().color = color3;
                        option3[1].GetComponent<Text>().color = Color.white;
                        
                        GameWindowManager.option3Centered = false;
                        
                        if (!NetrixiCombatScript.netrixiCondition3[1])
                        {
                            ChangeHandAnimation.animationName3 = "D to X";
                        }
                        else
                        {
                            if (!NetrixiCombatScript.netrixiCondition3[2])
                            {
                                ChangeHandAnimation.animationName3 = "X to A";
                            }
                            else
                            {
                                if (!NetrixiCombatScript.netrixiCondition3[3])
                                {
                                    ChangeHandAnimation.animationName3 = "A to W";
                                }
                            }
                        }
                    }
                    else if (attack1 == 0 || attack2 == 0)
                    {
                        if (!NetrixiCombatScript.canCancelMove) handAnimation3.SetActive(false);
                        GameWindowManager.option3Centered = true;
                        option3[1].GetComponent<Text>().text = attackSelected;
                    }
                }
                else
                {
                    option1[1].GetComponent<Text>().text = "Netrixi is unable to Attack";
                    option1[1].GetComponent<Text>().color = Color.gray;
                    originalOption1 = option1[1].GetComponent<Text>().text;

                    if (GameManagerScript.currentScene == 12)
                    {
                        // Gatekeeper scene
                        option2[1].GetComponent<Text>().text = "She was scared\nby the Dog's Bark";
                    }
                    else
                    {
                        if (GameManagerScript.currentScene == 10)
                        {
                            // Tavern scene
                            option2[1].GetComponent<Text>().text = "She is recovering\nfrom being Smashed";
                        }
                        else
                        {
                            option2[1].GetComponent<Text>().text = "She was dazed\nby the Bomb";
                        }
                    }
                    option2[1].GetComponent<Text>().color = Color.gray;
                    originalOption2 = option2[1].GetComponent<Text>().text;
                    
                    option3[1].GetComponent<Text>().text = "She can only Move\nthis round";
                    option3[1].GetComponent<Text>().color = Color.gray;
                    originalOption3 = option3[1].GetComponent<Text>().text;
                    
                    option4[1].GetComponent<Text>().text = "Flip hand to Move Netrixi";
                    
                    if (GameWindowManager.option1Centered) handAnimation1.SetActive(false);
                    if (GameWindowManager.option2Centered) handAnimation2.SetActive(false);
                    if (GameWindowManager.option3Centered) handAnimation3.SetActive(false);
                    
                    GameWindowManager.option1Centered = true;
                    GameWindowManager.option2Centered = true;
                    GameWindowManager.option3Centered = true;
                }
                
                IfHandFlipped("Netrixi");
                break;
            
            case "Folkvar":
                if (CombatManagerScript.canFolkvarAttack)
                {
                    option4[1].GetComponent<Text>().text = "Flip hand to Move Folkvar";

                    // Attack 1
                    if (attack1 != 4 && attack2 != 4) 
                    {
                        option1[1].GetComponent<Text>().text = "Swing Sword";
                        originalOption1 = option1[1].GetComponent<Text>().text;
                        
                        option1[0].GetComponent<SpriteRenderer>().color = color1;
                        option1[1].GetComponent<Text>().color = Color.white;
                        
                        GameWindowManager.option1Centered = false;
                        
                        if (!FolkvarCombatScript.folkvarCondition1[0])
                        {
                            ChangeHandAnimation.animationName1 = "Q to C";
                        }
                    }
                    else if (attack1 == 0 || attack2 == 0)
                    {
                        if (!FolkvarCombatScript.canCancelMove) handAnimation1.SetActive(false);
                        GameWindowManager.option1Centered = true;
                        option1[1].GetComponent<Text>().text = attackSelected;
                    }

                    // Attack 2
                    if (attack1 != 5 && attack2 != 5)
                    {
                        option2[1].GetComponent<Text>().text = "Holy Smite";
                        originalOption2 = option2[1].GetComponent<Text>().text;
                        
                        option2[0].GetComponent<SpriteRenderer>().color = color2;
                        option2[1].GetComponent<Text>().color = Color.white;
                        
                        GameWindowManager.option2Centered = false;
                        
                        if (!FolkvarCombatScript.folkvarCondition2[1])
                        {
                            ChangeHandAnimation.animationName2 = "A to W";
                        }
                        else
                        {
                            if (!FolkvarCombatScript.folkvarCondition2[2])
                            {
                                ChangeHandAnimation.animationName2 = "W to X";
                            }
                        }
                    }
                    else if (attack1 == 0 || attack2 == 0)
                    {
                        if (!FolkvarCombatScript.canCancelMove) handAnimation2.SetActive(false);
                        GameWindowManager.option2Centered = true;
                        option2[1].GetComponent<Text>().text = attackSelected;
                    }

                    // Attack 3
                    if (attack1 != 6 && attack2 != 6)
                    {
                        if (GameManagerScript.currentScene > 17)
                        {
                            option3[1].GetComponent<Text>().text = "Grand Slam";
                            originalOption3 = option3[1].GetComponent<Text>().text;

                            option3[0].GetComponent<SpriteRenderer>().color = color3;
                            option3[1].GetComponent<Text>().color = Color.white;
                            
                            GameWindowManager.option3Centered = false;
                            
                            if (!FolkvarCombatScript.folkvarCondition3[1])
                            {
                                ChangeHandAnimation.animationName3 = "Z to W";
                            }
                            else
                            {
                                if (!FolkvarCombatScript.folkvarCondition3[2])
                                {
                                    ChangeHandAnimation.animationName3 = "W to C";
                                }
                            }
                        }
                        else
                        {
                            option3[1].GetComponent<Text>().text = "Currently Locked";
                            originalOption3 = option3[1].GetComponent<Text>().text;
                            option3[1].GetComponent<Text>().color = Color.gray;
                            
                            handAnimation3.SetActive(false);
                            GameWindowManager.option3Centered = true;
                        }
                    }
                    else if (attack1 == 0 || attack2 == 0)
                    {
                        if (!FolkvarCombatScript.canCancelMove) handAnimation3.SetActive(false);
                        GameWindowManager.option3Centered = true;
                        option3[1].GetComponent<Text>().text = attackSelected;
                    }
                }
                else
                {
                    option1[1].GetComponent<Text>().text = "Folkvar is unable to Attack";
                    option1[1].GetComponent<Text>().color = Color.gray;
                    originalOption1 = option1[1].GetComponent<Text>().text;
                    
                    if (GameManagerScript.currentScene == 12)
                    {
                        // Gatekeeper scene
                        option2[1].GetComponent<Text>().text = "He was scared\nby the Dog's Bark";
                    }
                    else
                    {
                        if (GameManagerScript.currentScene == 10)
                        {
                            // Tavern scene
                            option2[1].GetComponent<Text>().text = "He is recovering\nfrom being Smashed";
                        }
                        else
                        {
                            option2[1].GetComponent<Text>().text = "He was dazed\nby the Bomb";
                        }
                    }
                    option2[1].GetComponent<Text>().color = Color.gray;
                    originalOption2 = option2[1].GetComponent<Text>().text;
                    
                    option3[1].GetComponent<Text>().text = "He can only Move\nthis round";
                    option3[1].GetComponent<Text>().color = Color.gray;
                    originalOption3 = option3[1].GetComponent<Text>().text;
                    
                    option4[1].GetComponent<Text>().text = "Flip hand to Move Folkvar";
                    
                    if (GameWindowManager.option1Centered) handAnimation1.SetActive(false);
                    if (GameWindowManager.option2Centered) handAnimation2.SetActive(false);
                    if (GameWindowManager.option3Centered) handAnimation3.SetActive(false);
                    
                    GameWindowManager.option1Centered = true;
                    GameWindowManager.option2Centered = true;
                    GameWindowManager.option3Centered = true;
                }
                
                IfHandFlipped("Folkvar");
                break;
            
            case "Iv":
                if (CombatManagerScript.canIvAttack)
                {
                    option4[1].GetComponent<Text>().text = "Flip hand to Move Iv";
                    
                    // Attack 1
                    if (attack1 != 7 && attack2 != 7)
                    {
                        if (AttackScript.countered) 
                        {
                            option1[1].GetComponent<Text>().text = "Counter Attack";
                            originalOption1 = option1[1].GetComponent<Text>().text;
                        }
                        else
                        {
                            if (CombatManagerScript.firstAttack == 9)
                            {
                                option1[1].GetComponent<Text>().text = "Counter Attack";
                                originalOption1 = option1[1].GetComponent<Text>().text;
                            }
                            else
                            {
                                option1[1].GetComponent<Text>().text = "Block Attack";
                                originalOption1 = option1[1].GetComponent<Text>().text;
                            }
                        }

                        option1[0].GetComponent<SpriteRenderer>().color = color1;
                        option1[1].GetComponent<Text>().color = Color.white;
                        
                        GameWindowManager.option1Centered = false;
                        
                        if (!IvCombatScript.ivCondition1[0])
                        {
                            ChangeHandAnimation.animationName1 = "Mid to Far";
                        }
                        else
                        {
                            ChangeHandAnimation.animationName1 = "Far to Near";
                        }
                    }
                    else if (attack1 == 0 || attack2 == 0)
                    {
                        if (!IvCombatScript.canCancelMove) handAnimation1.SetActive(false);
                        GameWindowManager.option1Centered = true;
                        option1[1].GetComponent<Text>().text = attackSelected;
                    }

                    // Attack 2
                    if (attack1 != 8 && attack2 != 8)
                    {
                        option2[1].GetComponent<Text>().text = "Heal Ally";
                        originalOption2 = option2[1].GetComponent<Text>().text;
                        
                        option2[0].GetComponent<SpriteRenderer>().color = color2;
                        option2[1].GetComponent<Text>().color = Color.white;
                        
                        GameWindowManager.option2Centered = false;
                        
                        if (!IvCombatScript.ivCondition2[1])
                        {
                            ChangeHandAnimation.animationName2 = "D to A";
                        }
                        else
                        {
                            if (!IvCombatScript.ivCondition2[2])
                            {
                                ChangeHandAnimation.animationName2 = "A to W";
                            }
                            else
                            {
                                if (!IvCombatScript.ivCondition2[3])
                                {
                                    ChangeHandAnimation.animationName2 = "W to X";
                                }
                            }
                        }
                    }
                    else if (attack1 == 0 || attack2 == 0)
                    {
                        if (!IvCombatScript.canCancelMove) handAnimation2.SetActive(false);
                        GameWindowManager.option2Centered = true;
                        option2[1].GetComponent<Text>().text = attackSelected;
                    }

                    // Attack 3
                    if (attack1 != 9 && attack2 != 9)
                    {
                        if (GameManagerScript.currentScene > 17)
                        {
                            option3[1].GetComponent<Text>().text = "Empower Attack";
                            originalOption3 = option3[1].GetComponent<Text>().text;
                        
                            option3[0].GetComponent<SpriteRenderer>().color = color3;
                            option3[1].GetComponent<Text>().color = Color.white;
                            
                            GameWindowManager.option3Centered = false;
                            
                            if (!IvCombatScript.ivCondition3[0])
                            {
                                ChangeHandAnimation.animationName3 = "Start Rotating";
                            }
                            else
                            {
                                if (!IvCombatScript.ivCondition3[1])
                                {
                                    ChangeHandAnimation.animationName3 = "Continue Rotating";
                                }
                            }
                        }
                        else
                        {
                            option3[1].GetComponent<Text>().text = "Currently Locked";
                            originalOption3 = option3[1].GetComponent<Text>().text;
                            option3[1].GetComponent<Text>().color = Color.gray;
                            
                            handAnimation3.SetActive(false);
                            GameWindowManager.option3Centered = true;
                        }
                    }
                    else if (attack1 == 0 || attack2 == 0)
                    {
                        if (!IvCombatScript.canCancelMove) handAnimation3.SetActive(false);
                        GameWindowManager.option3Centered = true;
                        option3[1].GetComponent<Text>().text = attackSelected;
                    }
                }
                else
                {
                    option1[1].GetComponent<Text>().text = "Iv is unable to Attack";
                    option1[1].GetComponent<Text>().color = Color.gray;
                    originalOption1 = option1[1].GetComponent<Text>().text;
                    
                    if (GameManagerScript.currentScene == 12)
                    {
                        // Gatekeeper scene
                        option2[1].GetComponent<Text>().text = "She was scared\nby the Dog's Bark";
                    }
                    else
                    {
                        if (GameManagerScript.currentScene == 10)
                        {
                            // Tavern scene
                            option2[1].GetComponent<Text>().text = "She is recovering\nfrom being Smashed";
                        }
                        else
                        {
                            option2[1].GetComponent<Text>().text = "She was dazed\nby the Bomb";
                        }
                    }
                    option2[1].GetComponent<Text>().color = Color.gray;
                    originalOption2 = option2[1].GetComponent<Text>().text;
                    
                    option3[1].GetComponent<Text>().text = "She can only Move\nthis round";
                    option3[1].GetComponent<Text>().color = Color.gray;
                    originalOption3 = option3[1].GetComponent<Text>().text;
                    
                    option4[1].GetComponent<Text>().text = "Flip hand to Move Iv";
                    
                    if (GameWindowManager.option1Centered) handAnimation1.SetActive(false);
                    if (GameWindowManager.option2Centered) handAnimation2.SetActive(false);
                    if (GameWindowManager.option3Centered) handAnimation3.SetActive(false);
                    
                    GameWindowManager.option1Centered = true;
                    GameWindowManager.option2Centered = true;
                    GameWindowManager.option3Centered = true;
                }
                
                IfHandFlipped("Iv");
                break;
        }

        IfAttacksChosen();
    }


    public void IfHandFlipped(string option)
    {
        option4[1].GetComponent<Text>().text = "Flip hand to Move";
        option4[1].GetComponent<Text>().color = Color.white;
        option4[0].GetComponent<SpriteRenderer>().color = nextChoice;

        if (MarkerManagerScript.goMarker)
        {
            if (Input.GetKeyDown(KeyCode.V)) HighlightChoices.S.HighlightChoice(4,4);
        }
        
        // Change options for Netrixi
        if (option == "Netrixi")
        {
            if (NetrixiCombatScript.netrixiCondition4[0])
            {
                if (!NetrixiCombatScript.netrixiCondition2[1])
                {
                    if (!NetrixiCombatScript.netrixiCondition4[1])
                    {
                        ChangeText("Netrixi");
                    }
                }
            }
        }
        
        // Change options for Folkvar
        if (option == "Folkvar")
        {
            if (FolkvarCombatScript.folkvarCondition4[0])
            {
                if (!FolkvarCombatScript.folkvarCondition4[1])
                {
                    ChangeText("Folkvar");
                }
            }
        }
        
        // Change options for Iv
        if (option == "Iv")
        {
            if (IvCombatScript.ivCondition4[0])
            {
                if (!IvCombatScript.ivCondition3[1])
                {
                    if (!IvCombatScript.ivCondition4[1])
                    {
                        ChangeText("Iv");
                    }
                }
            }
        }

        void ChangeText(string character)
        {
            originalOption1 = option1[1].GetComponent<Text>().text;
            originalOption2 = option2[1].GetComponent<Text>().text;
            originalOption3 = option3[1].GetComponent<Text>().text;

            switch (character)
            {
                case "Netrixi":
                    if (CharacterManagerScript.netrixiCanMoveLeft)
                    {
                        if (CharacterManagerScript.netrixiCanMoveRight) ChangeMove(true, true);
                        else ChangeMove(true, false);
                    }
                    else
                    {
                        if (CharacterManagerScript.netrixiCanMoveRight) ChangeMove(false, true);
                        else ChangeMove(false, false);
                    }
                    break;
                
                case "Folkvar":
                    if (CharacterManagerScript.folkvarCanMoveLeft)
                    {
                        if (CharacterManagerScript.folkvarCanMoveRight) ChangeMove(true, true);
                        else ChangeMove(true, false);
                    }
                    else
                    {
                        if (CharacterManagerScript.folkvarCanMoveRight) ChangeMove(false, true);
                        else ChangeMove(false, false);
                    }
                    break;
                
                case "Iv":
                    if (CharacterManagerScript.ivCanMoveLeft)
                    {
                        if (CharacterManagerScript.ivCanMoveRight) ChangeMove(true, true);
                        else ChangeMove(true, false);
                    }
                    else
                    {
                        if (CharacterManagerScript.ivCanMoveRight) ChangeMove(false, true);
                        else ChangeMove(false, false);
                    }
                    break;
            }


            void ChangeMove(bool canMoveLeft, bool canMoveRight)
            {
                if (canMoveLeft)
                {
                    option1[1].GetComponent<Text>().text = "Move " + character + "\nLeft";
                    option1[1].GetComponent<Text>().color = Color.white;
                    option1[0].GetComponent<SpriteRenderer>().color = firstChoice;

                    handAnimation1.SetActive(true);

                    GameWindowManager.option1Centered = false;
                    
                    ChangeHandAnimation.animationName1 = "Move Left";
                    
                    if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Z))
                    {
                        HighlightChoices.S.HighlightChoice(1,2);
                    }
                }
                else
                {
                    option1[1].GetComponent<Text>().text = character + " is unable to\nMove Left";
                    option1[1].GetComponent<Text>().color = Color.gray;
                    option1[0].GetComponent<SpriteRenderer>().color = inActiveDecision;

                    handAnimation1.SetActive(false);
                    GameWindowManager.option1Centered = true;
                }
                
                if (canMoveRight)
                {
                    option2[1].GetComponent<Text>().text = "Move " + character + "\nRight";
                    option2[1].GetComponent<Text>().color = Color.white;
                    option2[0].GetComponent<SpriteRenderer>().color = secondChoice;

                    handAnimation2.SetActive(true);

                    GameWindowManager.option2Centered = false;
                    
                    ChangeHandAnimation.animationName2 = "Move Right";
                    
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.C))
                    {
                        HighlightChoices.S.HighlightChoice(2,2);
                    }
                }
                else
                {
                    option2[1].GetComponent<Text>().text = character + " is unable to\nMove Right";
                    option2[1].GetComponent<Text>().color = Color.gray;
                    option2[0].GetComponent<SpriteRenderer>().color = inActiveDecision;

                    handAnimation2.SetActive(false);
                    GameWindowManager.option2Centered = true;
                }

                if (!canMoveLeft && !canMoveRight)
                {
                    option3[1].GetComponent<Text>().text = "Another character\nis in the way";
                    GameWindowManager.option3Centered = true;
                }
                else option3[1].GetComponent<Text>().text = "";
            }

            // Change other option texts
            
            option3[1].GetComponent<Text>().color = Color.gray;
            option3[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
            
            option4[1].GetComponent<Text>().text = "Flip hand to Cancel Move";
            option4[1].GetComponent<Text>().color = Color.gray;
            option4[0].GetComponent<SpriteRenderer>().color = inActiveDecision;

            handAnimation3.SetActive(false);
            
            if (MarkerManagerScript.goMarker)
            {
                if (Input.GetKeyDown(KeyCode.V)) HighlightChoices.S.HighlightChoice(4,4);
            }
        }
    }


    public void IfAttacksChosen()
    {
        if (CombatManagerScript.firstAttack != 0 && CombatManagerScript.secondAttack != 0)
        {
            originalOption1 = option1[1].GetComponent<Text>().text;
            originalOption2 = option2[1].GetComponent<Text>().text;
            originalOption3 = option3[1].GetComponent<Text>().text;

            option1[1].GetComponent<Text>().text = "Flip hand to\nConfirm";
            option1[1].GetComponent<Text>().color = Color.white;
                    
            option2[1].GetComponent<Text>().text = "Change an Attack";
            option2[1].GetComponent<Text>().color = Color.white;
                
            option3[1].GetComponent<Text>().text = "";
            option3[1].GetComponent<Text>().color = Color.gray;
            
            option4[1].GetComponent<Text>().text = "";
            option4[1].GetComponent<Text>().color = Color.gray;


            option1[0].GetComponent<SpriteRenderer>().color = nextChoice;
            option2[0].GetComponent<SpriteRenderer>().color = secondChoice;
            option3[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
            option4[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
            
            handAnimation1.SetActive(true);
            handAnimation2.SetActive(true);
            handAnimation3.SetActive(false);

            GameWindowManager.option1Centered = false;
            GameWindowManager.option2Centered = false;
            
            ChangeHandAnimation.animationName1 = "Flip Hand";
            ChangeHandAnimation.animationName2 = "Undo Marker";
            
            if (MarkerManagerScript.goMarker)
            {
                if (Input.GetKeyDown(KeyCode.V)) HighlightChoices.S.HighlightChoice(1,2);
            }
        }
    }


    public void ResetChoices(bool fullReset)
    {
        option1[1].GetComponent<Text>().color = Color.white;
        option2[1].GetComponent<Text>().color = Color.white;
        option3[1].GetComponent<Text>().color = Color.white;
        
        GameWindowManager.option1Centered = false;
        GameWindowManager.option2Centered = false;
        GameWindowManager.option3Centered = false;

        if (!fullReset)
        {
            if (!CombatManagerScript.netrixiAlive)
            {
                option1[1].GetComponent<Text>().text = originalOption1;
                option1[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
            }

            if (!GameManagerScript.folkvarInParty || !CombatManagerScript.folkvarAlive)
            {
                if (CombatManagerScript.firstAttack != 2 && CombatManagerScript.secondAttack != 2)
                {
                    option2[1].GetComponent<Text>().text = originalOption2;
                }
                option2[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
            }

            if (!GameManagerScript.ivInParty || !CombatManagerScript.ivAlive)
            {
                if (CombatManagerScript.firstAttack != 3 && CombatManagerScript.secondAttack != 3)
                {
                    option3[1].GetComponent<Text>().text = originalOption3;
                }
                option3[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
            }
        }
        else
        {
            originalOption1 = "";
            originalOption2 = "";
            originalOption3 = "";
            
            option1[1].GetComponent<Text>().text = originalOption1;
            option2[1].GetComponent<Text>().text = originalOption2;
            option3[1].GetComponent<Text>().text = originalOption3;
            option4[1].GetComponent<Text>().text = "";

            option1[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
            option2[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
            option3[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
            
            option4[0].transform.position = new Vector3(GameWindowManager.offScreen, GameWindowManager.offScreen, 0);
            
            ChangeHandAnimation.animationName1 = "";
            ChangeHandAnimation.animationName2 = "";
            ChangeHandAnimation.animationName3 = "";
            
            handAnimation1.SetActive(false);
            handAnimation2.SetActive(false);
            handAnimation3.SetActive(false);
        }
    }


    public string WhoToApproach()
    {
        switch (GameManagerScript.currentScene)
        {
            case 8:
                if (madeChoice)
                {
                    if (talkingToBrute) return "the brute";
                    else if (talkingToIv) return "the monk";
                }
                else
                {
                    return "the monk";
                }
                break;
            
            case 9:
                return "the monk?";

            case 11:
                return "the gatekeeper?";

            case 13:
                return "the royal jester?";

            case 14:
                return "the bandit guards?";

            case 16:
                return "the bandit leader?";

            case 22:
                return "the royal guards?";

            case 26:
                return "King Moke?";
        }

        return "";
    }
}
