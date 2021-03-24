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

    public Color nextChoice, firstChoice, secondChoice;
    public Color color1, color2, color3;
    public Color activeDecision, inActiveDecision;

    public static ChangeChoiceText S;

    public static string originalOption1, originalOption2, originalOption3;

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
        if (GameManagerScript.currentScene == 8)
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Z))
            {
                talkingToBrute = true;
                talkingToIv = false;
            }
            
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.C))
            {
                talkingToIv = true;
                talkingToBrute = false;
            }
            
            if (GameWindowManager.metBrute)
            {
                talkingToIv = true;
                talkingToBrute = false;
            }
        }
        
        if (GameManagerScript.currentScene == 11)
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
                    // The player can attack with Netrixi
                    if (CombatManagerScript.netrixiAlive)
                    {
                        option1[1].GetComponent<Text>().text = "Attack with Netrixi";
                    
                        option1[0].GetComponent<SpriteRenderer>().color = color1;
                    }

                    // The player can attack with Folkvar
                    if (CombatManagerScript.folkvarAlive)
                    {
                        option2[1].GetComponent<Text>().text = "Attack with Folkvar";
                    
                        option2[0].GetComponent<SpriteRenderer>().color = color2;
                    }

                    // The player can attack with Iv
                    if (CombatManagerScript.ivAlive)
                    {
                        option3[1].GetComponent<Text>().text = "Attack with Iv";
                    
                        option3[0].GetComponent<SpriteRenderer>().color = color3;
                    }
                }
                else
                {
                    ResetChoices(true);
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
                        }
                        else
                        {
                            choice1[1].GetComponent<Text>().text = "Next...";
                        }
                    }
                    else
                    {
                        choice1[1].GetComponent<Text>().text = "Next...";
                    }

                    ChangeOptions(1, "Flip Hand to Continue", "", "");
                }
                else
                {
                    choice1[1].GetComponent<Text>().text = "Restart Game?";
                
                    ChangeOptions(1, "Flip Hand to Restart", "", "");
                }
                break;
            
            case 2:
                if (displayDialogueResponses)
                {
                    choice1[1].GetComponent<Text>().text = choices[0].GetComponentInChildren<Text>().text;
                    choice2[1].GetComponent<Text>().text = choices[1].GetComponentInChildren<Text>().text;
                    
                    ChangeOptions(2, "Move Hand Left", "Move Hand Right", "");
                }
                else
                {
                    if (currentScene == 8 || currentScene == 13)
                    {
                        if (!madeChoice)
                        {
                            choice1[1].GetComponent<Text>().text = "Who will they talk to?";

                            if (currentScene <= 11)
                            {
                                ChangeOptions(2, "Approach Brute", "Approach Monk", "");
                            }
                            else
                            {
                                ChangeOptions(2, "Approach Jester", "Approach King", "");
                            }
                        }
                        else
                        {
                            choice1[1].GetComponent<Text>().text = "Who approaches " + WhoToApproach();
                            
                            ChangeOptions(2, "Choose Netrixi", "Choose Folkvar", "");
                        }
                    }
                    else
                    {
                        choice1[1].GetComponent<Text>().text = "Who approaches " + WhoToApproach();

                        ChangeOptions(2, "Choose Netrixi", "Choose Folkvar", "");
                    }
                }
                break;
            
            case 3:
                if (currentScene >= 28) choice1[1].GetComponent<Text>().text = "Who will take the stone?";
                else if (currentScene >= 26) choice1[1].GetComponent<Text>().text = "Who dares to approach " + WhoToApproach();
                else choice1[1].GetComponent<Text>().text = "Who approaches " + WhoToApproach();
                
                ChangeOptions(3, "Choose Netrixi", "Choose Folkvar", "Choose Iv");
                break;
            
            case 4:
                ChangeOptions(4, "", "", "");
                break;
        }
        
        // Changing the color of choice 1
        if (!displayDialogueResponses)
        {
            choice1[0].GetComponent<SpriteRenderer>().color = nextChoice;
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
                break;
            
            case 3:
                option1[0].GetComponent<SpriteRenderer>().color = color1;
                option2[0].GetComponent<SpriteRenderer>().color = color2;
                option3[0].GetComponent<SpriteRenderer>().color = color3;
                
                option4[0].GetComponent<SpriteRenderer>().color = nextChoice;
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

        
        int attack1 = CombatManagerScript.firstAttack;
        int attack2 = CombatManagerScript.secondAttack;
        
        switch (character)
        {
            case "Netrixi":
                if (CombatManagerScript.canNetrixiAttack)
                {
                    option4[1].GetComponent<Text>().text = "Flip Hand to Move Netrixi";

                    // Attack 1
                    if (attack1 != 1 && attack2 != 1)
                    {
                        option1[1].GetComponent<Text>().text = "Fireball";
                        originalOption1 = option1[1].GetComponent<Text>().text;
                        
                        option1[0].GetComponent<SpriteRenderer>().color = color1;
                        option1[1].GetComponent<Text>().color = Color.white;
                    }

                    // Attack 2
                    if (attack1 != 2 && attack2 != 2)
                    {
                        option2[1].GetComponent<Text>().text = "Lightning Strike";
                        originalOption2 = option2[1].GetComponent<Text>().text;
                        
                        option2[0].GetComponent<SpriteRenderer>().color = color2;
                        option2[1].GetComponent<Text>().color = Color.white;
                    }

                    // Attack 3
                    if (attack1 != 3 && attack2 != 3)
                    {
                        option3[1].GetComponent<Text>().text = "Transmutation";
                        originalOption3 = option3[1].GetComponent<Text>().text;
                        
                        option3[0].GetComponent<SpriteRenderer>().color = color3;
                        option3[1].GetComponent<Text>().color = Color.white;
                    }
                }
                else
                {
                    option1[1].GetComponent<Text>().text = "Netrixi is currently unable to Attack";
                    option1[1].GetComponent<Text>().color = Color.gray;
                    originalOption1 = option1[1].GetComponent<Text>().text;
                    
                    option2[1].GetComponent<Text>().text = "She can only Move during this round";
                    option2[1].GetComponent<Text>().color = Color.gray;
                    originalOption2 = option2[1].GetComponent<Text>().text;
                    
                    option3[1].GetComponent<Text>().text = "";
                    
                    option4[1].GetComponent<Text>().text = "Flip Hand to Move Netrixi";
                }
                
                IfHandFlipped("Flip Hand to Move Netrixi");
                IfAttacksChosen();
                break;
            
            case "Folkvar":
                if (CombatManagerScript.canFolkvarAttack)
                {
                    option4[1].GetComponent<Text>().text = "Flip Hand to Move Folkvar";

                    // Attack 1
                    if (attack1 != 4 && attack2 != 4) 
                    {
                        option1[1].GetComponent<Text>().text = "Swing Sword";
                        originalOption1 = option1[1].GetComponent<Text>().text;
                        
                        option1[0].GetComponent<SpriteRenderer>().color = color1;
                        option1[1].GetComponent<Text>().color = Color.white;
                    }

                    // Attack 2
                    if (attack1 != 5 && attack2 != 5)
                    {
                        option2[1].GetComponent<Text>().text = "Holy Smite";
                        originalOption2 = option2[1].GetComponent<Text>().text;
                        
                        option2[0].GetComponent<SpriteRenderer>().color = color2;
                        option2[1].GetComponent<Text>().color = Color.white;
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
                        }
                        else
                        {
                            option3[1].GetComponent<Text>().text = "Currently Locked";
                            originalOption3 = option3[1].GetComponent<Text>().text;
                            option3[1].GetComponent<Text>().color = Color.gray;
                        }
                    }
                }
                else
                {
                    option1[1].GetComponent<Text>().text = "Folkvar is currently unable to Attack";
                    option1[1].GetComponent<Text>().color = Color.gray;
                    originalOption1 = option1[1].GetComponent<Text>().text;
                    
                    option2[1].GetComponent<Text>().text = "He can only Move during this round";
                    option2[1].GetComponent<Text>().color = Color.gray;
                    originalOption2 = option2[1].GetComponent<Text>().text;
                    
                    option3[1].GetComponent<Text>().text = "";
                    
                    option4[1].GetComponent<Text>().text = "Flip Hand to Move Folkvar";
                }
                
                IfHandFlipped("Flip Hand to Move Folkvar");
                IfAttacksChosen();
                break;
            
            case "Iv":
                if (CombatManagerScript.canIvAttack)
                {
                    option4[1].GetComponent<Text>().text = "Flip Hand to Move Iv";
                    
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
                    }

                    // Attack 2
                    if (attack1 != 8 && attack2 != 8)
                    {
                        option2[1].GetComponent<Text>().text = "Heal Ally";
                        originalOption2 = option2[1].GetComponent<Text>().text;
                        
                        option2[0].GetComponent<SpriteRenderer>().color = color2;
                        option2[1].GetComponent<Text>().color = Color.white;
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
                        }
                        else
                        {
                            option3[1].GetComponent<Text>().text = "Currently Locked";
                            originalOption3 = option3[1].GetComponent<Text>().text;
                            option3[1].GetComponent<Text>().color = Color.gray;
                        }
                    }
                }
                else
                {
                    option1[1].GetComponent<Text>().text = "Iv is currently unable to Attack";
                    option1[1].GetComponent<Text>().color = Color.gray;
                    originalOption1 = option1[1].GetComponent<Text>().text;
                    
                    option2[1].GetComponent<Text>().text = "She can only Move during this round";
                    option2[1].GetComponent<Text>().color = Color.gray;
                    originalOption2 = option2[1].GetComponent<Text>().text;
                    
                    option3[1].GetComponent<Text>().text = "";
                    
                    option4[1].GetComponent<Text>().text = "Flip Hand to Move Iv";
                }
                
                IfHandFlipped("Flip Hand to Move Iv");
                IfAttacksChosen();
                break;
        }
    }


    public void IfHandFlipped(string option)
    {
        // Change options for Netrixi
        if (option == "Flip Hand to Move Netrixi")
        {
            option4[1].GetComponent<Text>().text = "Flip Hand to Move";
            
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
        if (option == "Flip Hand to Move Folkvar")
        {
            option4[1].GetComponent<Text>().text = "Flip Hand to Move";
            
            if (FolkvarCombatScript.folkvarCondition4[0])
            {
                if (!FolkvarCombatScript.folkvarCondition4[1])
                {
                    ChangeText("Folkvar");
                }
            }
        }
        
        // Change options for Iv
        if (option == "Flip Hand to Move Iv")
        {
            option4[1].GetComponent<Text>().text = "Flip Hand to Move";
            
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
                    if (CharacterManagerScript.netrixiCanMoveLeft) ChangeMove(true, true);
                    else ChangeMove(false, true);
                    
                    if (CharacterManagerScript.netrixiCanMoveRight) ChangeMove(true, false);
                    else ChangeMove(false, false);
                    break;
                
                case "Folkvar":
                    if (CharacterManagerScript.folkvarCanMoveLeft) ChangeMove(true, true);
                    else ChangeMove(false, true);
                    
                    if (CharacterManagerScript.folkvarCanMoveRight) ChangeMove(true, false);
                    else ChangeMove(false, false);
                    break;
                
                case "Iv":
                    if (CharacterManagerScript.ivCanMoveLeft) ChangeMove(true, true);
                    else ChangeMove(false, true);
                    
                    if (CharacterManagerScript.ivCanMoveRight) ChangeMove(true, false);
                    else ChangeMove(false, false);
                    break;
            }


            void ChangeMove(bool canMove, bool directionIsLeft)
            {
                if (directionIsLeft)
                {
                    if (canMove)
                    {
                        option1[1].GetComponent<Text>().text = "Move " + character + " Left";
                        option1[1].GetComponent<Text>().color = Color.white;
                        option1[0].GetComponent<SpriteRenderer>().color = firstChoice;
                    }
                    else
                    {
                        option1[1].GetComponent<Text>().text = character + " is unable to move Left";
                        option1[1].GetComponent<Text>().color = Color.gray;
                        option1[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
                    }
                }
                else
                {
                    if (canMove)
                    {
                        option2[1].GetComponent<Text>().text = "Move " + character + " Right";
                        option2[1].GetComponent<Text>().color = Color.white;
                        option2[0].GetComponent<SpriteRenderer>().color = secondChoice;
                    }
                    else
                    {
                        option2[1].GetComponent<Text>().text = character + " is unable to move Right";
                        option2[1].GetComponent<Text>().color = Color.gray;
                        option2[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
                    }
                }
            }

            // Change other option texts
            option3[1].GetComponent<Text>().text = "";
            option3[1].GetComponent<Text>().color = Color.gray;
            
            option4[1].GetComponent<Text>().text = "Flip Hand to Confirm";
            option4[1].GetComponent<Text>().color = Color.white;
                
            
            // Change colors
            option3[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
            option4[0].GetComponent<SpriteRenderer>().color = nextChoice;
        }
    }


    public void IfAttacksChosen()
    {
        if (CombatManagerScript.firstAttack != 0 && CombatManagerScript.secondAttack != 0)
        {
            originalOption1 = option1[1].GetComponent<Text>().text;
            originalOption2 = option2[1].GetComponent<Text>().text;
            originalOption3 = option3[1].GetComponent<Text>().text;
            
            
            option1[1].GetComponent<Text>().text = "Flip Hand to Confirm";
            option1[1].GetComponent<Text>().color = Color.white;
                    
            option2[1].GetComponent<Text>().text = "Change an Attack";
            option2[1].GetComponent<Text>().color = Color.white;
                
            option3[1].GetComponent<Text>().text = "";
            option3[1].GetComponent<Text>().color = Color.gray;


            option1[0].GetComponent<SpriteRenderer>().color = nextChoice;
            option2[0].GetComponent<SpriteRenderer>().color = secondChoice;
            option3[0].GetComponent<SpriteRenderer>().color = inActiveDecision;
        }
    }


    public void ResetChoices(bool fullReset)
    {
        //this.GetComponent<SpriteRenderer>().color = inActiveDecision;
        
        option1[1].GetComponent<Text>().color = Color.white;
        option2[1].GetComponent<Text>().color = Color.white;
        option3[1].GetComponent<Text>().color = Color.white;

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
        }
    }


    public string WhoToApproach()
    {
        switch (GameManagerScript.currentScene)
        {
            case 8:
                if (talkingToBrute) return "the brute";
                if (talkingToIv) return "the monk";
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
