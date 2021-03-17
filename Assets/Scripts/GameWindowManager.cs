using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameWindowManager : MonoBehaviour
{
    public static float webcamSizeX = 680;
    public static float webcamSizeY = 520;
    
    // Initial values for Beholder webcam
    //public static float webcamSizeX = 640;
    //public static float webcamSizeY = 480;

    public static float gameWindowSizeX, gameWindowSizeY;

    public GameObject webcam;
    public Text webcamText;
    
    public GameObject option1, option2, option3;
    public Text option1Text, option2Text, option3Text;
    public static float optionXPadding = 0.0125f, optionYPadding = 0.0125f;

    public GameObject background;

    public GameObject dialogue;
    public Text dialogueText;
    private string currentDialogue;
    public static float percentOfDialogueArea;

    public GameObject portrait;
    public static float dialoguePadding;

    public GameObject hand, gridRow1, gridRow2, gridColumn1, gridColumn2;
    public static float gridRowPadding, gridColumnPadding;

    public GameObject choice1, choice2, choice3;
    public Text choice1Text, choice2Text, choice3Text;
    
    private GameObject[] choices;
    public static float choiceXPadding = 0.0125f, choiceYPadding = 0.0125f;
    
    public static string speakerName;

    public static bool metBrute = true;

    public static GameWindowManager S;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        S = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Determine dimensions of the camera within the scene
        Camera camera = Camera.main;
        float cameraHeight = 2f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;


        choices = GameObject.FindGameObjectsWithTag("Dialogue Choice");
        GameObject dialogueTextBox = GameObject.FindGameObjectWithTag("Dialogue Text");

        if (dialogueTextBox != null)
        {
            string unSplitDialogue = dialogueTextBox.GetComponent<Text>().text;
            string[] splitArray = unSplitDialogue.Split(new string[] {" - "}, System.StringSplitOptions.None);
            
            string spokenWords;
            if (splitArray.Length > 1)
            {
                spokenWords = splitArray[1];
            
                if (dialogueText.text != spokenWords)
                {
                    // Determine location of the player portrait
                    PositionPortrait(cameraWidth, cameraHeight);
                }
                dialogueText.text = spokenWords;
            }
            else
            {
                if (dialogueText.text != unSplitDialogue)
                {
                    // Determine location of the player portrait
                    PositionPortrait(cameraWidth, cameraHeight);
                }
                dialogueText.text = unSplitDialogue;
            }
        }
        
        // Check to see if the dialogue has changed
        if (dialogueTextBox != null)
        {
            if ((string)dialogueTextBox.GetComponent<Text>().text != currentDialogue)
            {
                PositionDialogue(cameraWidth, cameraHeight);
                DetermineChoices(cameraWidth, cameraHeight);
            }
            currentDialogue = dialogueTextBox.GetComponent<Text>().text;
        }
        

        // If the player is in a combat scene
        if (GameManagerScript.inCombat)
        {
            percentOfDialogueArea = 1f;
            PositionDialogue(cameraWidth, cameraHeight);

            ChangePortrait.whoIsTalking = 0;
            PositionPortrait(cameraWidth, cameraHeight);
        }
    }

    public void ArrangeScreen()
    {
        Array.Clear(choices,0,choices.Length);
        
        gameWindowSizeX = Screen.width - webcamSizeX;
        gameWindowSizeY = (int) (gameWindowSizeX / 16) * 9;
        
        // Determine dimensions of the camera within the scene
        Camera camera = Camera.main;
        float cameraHeight = 2f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;

        // Define the margins for the grid rows and columns
        gridRowPadding = (cameraWidth * 0.025f);
        gridColumnPadding = (cameraHeight * 0.025f);

        // Arrange the positions for all the elements of the game
        PositionWebcam(cameraWidth, cameraHeight);
        
        PositionOptions(cameraWidth, cameraHeight, 1);
        PositionOptions(cameraWidth, cameraHeight, 2);
        PositionOptions(cameraWidth, cameraHeight, 3);
        
        ChangeGameWindow(cameraWidth, cameraHeight);
        
        choices = GameObject.FindGameObjectsWithTag("Dialogue Choice");
        PositionDialogue(cameraWidth, cameraHeight);
    }


    void PositionWebcam(float cameraWidth, float cameraHeight)
    {
        // Determine the dimensions of the webcam
        float webcamWidth = webcamSizeX / Screen.width;
        float webcamHeight = webcamSizeY / Screen.height;
        
        webcam.transform.localScale = new Vector3(webcamWidth * cameraWidth, webcamHeight * cameraHeight, 1);

        // Determine the location of the webcam
        float xLocation = (cameraWidth/2) - ((webcamWidth * cameraWidth)/2);
        float yLocation = (cameraHeight/2) - ((webcamHeight * cameraHeight)/2);
        
        webcam.transform.position = new Vector3(xLocation, yLocation, 0);
        
        // Change webcam text location
        webcamText.transform.position = new Vector3(Screen.width - (webcamSizeX/2), Screen.height - (webcamSizeY/2), 0);
        
        // Change scale of hand icon
        SpriteRenderer sr = hand.GetComponent<SpriteRenderer>();
        float handScale = sr.sprite.bounds.extents.x * 2;
        
        // Change this value to change the scale of the hand icon in relation to the webcam window
        HandManagerScript.scale = ((webcamWidth * cameraWidth) / 4) / handScale;
        
        // Change location of hand icon
        float handXPosition = xLocation - ((webcamWidth * cameraWidth)/2);
        float handYPosition = yLocation - ((webcamHeight * cameraHeight)/2);

        HandManagerScript.xPosWebcam = new[] {handXPosition + ((webcamWidth * cameraWidth * 1.5f)/9), handXPosition + ((webcamWidth * cameraWidth * 1)/2), handXPosition + ((webcamWidth * cameraWidth * 7.5f)/9)};
        HandManagerScript.yPosWebcam = new[] {handYPosition + ((webcamHeight * cameraHeight * 7.5f)/9), handYPosition + ((webcamHeight * cameraHeight * 1)/2), handYPosition + ((webcamHeight * cameraHeight * 1.5f)/9)};
        HandManagerScript.ChangeHandLocation();

        PositionGrid(webcamWidth, webcamHeight, cameraWidth, cameraHeight);
    }


    void PositionOptions(float cameraWidth, float cameraHeight, int optionNumber)
    {
        // Determine the dimensions of the option
        float optionWidth = webcamSizeX / Screen.width;
        float optionHeight = ((Screen.height - webcamSizeY) / Screen.height)/3;

        switch (optionNumber)
        {
            case 1: option1.transform.localScale = new Vector3(optionWidth * cameraWidth, optionHeight * cameraHeight, 1);
                break;
            
            case 2: option2.transform.localScale = new Vector3(optionWidth * cameraWidth, optionHeight * cameraHeight, 1);
                break;
            
            case 3: option3.transform.localScale = new Vector3(optionWidth * cameraWidth, optionHeight * cameraHeight, 1);
                break;
        }
        
        // Determine the location of the option
        float xLocation = (cameraWidth/2) - ((optionWidth * cameraWidth)/2);
        float yLocation = (-cameraHeight/2) + ((optionHeight * cameraHeight)/2);
        
        switch (optionNumber)
        {
            case 1:
                yLocation = (-cameraHeight/2) + ((optionHeight * cameraHeight)*5/2);
                
                option1.transform.position = new Vector3(xLocation, yLocation, 0);
                break;
            
            case 2:
                yLocation = (-cameraHeight/2) + ((optionHeight * cameraHeight)*3/2);

                option2.transform.position = new Vector3(xLocation, yLocation, 0);
                break;
            
            case 3: 
                yLocation = (-cameraHeight/2) + ((optionHeight * cameraHeight)/2);
                
                option3.transform.position = new Vector3(xLocation, yLocation, 0);
                break;
        }
        
        // Change option text location
        float yHeight = (Screen.height - webcamSizeY) / 3;
        
        float sizeDiffX = webcamSizeX - (2 * (Screen.width * optionXPadding));
        float sizeDiffY = (Screen.height - webcamSizeY);

        switch (optionNumber)
        {
            case 1:
                option1Text.transform.position = new Vector3(Screen.width - (webcamSizeX/2), yHeight*5/2, 0);
                sizeDiffY -= 2 * (Screen.height * optionYPadding);
                break;
            
            case 2:
                option2Text.transform.position = new Vector3(Screen.width - (webcamSizeX/2), yHeight*3/2, 0);
                sizeDiffY /= 2;
                
                sizeDiffY -= 2 * (Screen.height * optionYPadding);
                break;
            
            case 3:
                option3Text.transform.position = new Vector3(Screen.width - (webcamSizeX/2), yHeight/2, 0);
                sizeDiffY /= 3;
                sizeDiffY -= 2 * (Screen.height * optionYPadding);
                break;
        }
        
        // Change option textbox sizes
        option1Text.rectTransform.sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
        option2Text.rectTransform.sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
        option3Text.rectTransform.sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
    }


    void ChangeGameWindow(float cameraWidth, float cameraHeight)
    {
        SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
        
        // Determine the dimensions of the game window
        float windowWidth = gameWindowSizeX / Screen.width;
        float windowHeight = gameWindowSizeY / Screen.height;

        float currWidth = sr.sprite.bounds.extents.x * 2;
        float currHeight = sr.sprite.bounds.extents.y * 2;

        if (currWidth != windowWidth * cameraWidth)
        {
            background.transform.localScale = new Vector3((windowWidth * cameraWidth)/currWidth, (windowHeight * cameraHeight)/currHeight, 1);
        }
        
        // Determine the location of the game window
        float xLocation = -cameraWidth/2 + ((windowWidth * cameraWidth)/2);
        float yLocation = cameraHeight/2 - (windowHeight * cameraHeight)/2;
        
        background.transform.position = new Vector3(xLocation, yLocation, 0);
    }


    void PositionDialogue(float cameraWidth, float cameraHeight)
    {
        // Determine number of choices for the player
        DetermineChoices(cameraWidth, cameraHeight);

        // Determine the dimensions of the dialogue window
        float dialogueWidth = gameWindowSizeX / Screen.width;
        float dialogueHeight = ((Screen.height - gameWindowSizeY) * percentOfDialogueArea) / Screen.height;
        
        dialogue.transform.localScale = new Vector3(dialogueWidth * cameraWidth, dialogueHeight * cameraHeight, 1);

        // Determine the location of the dialogue window
        float xLocation = -cameraWidth/2 + ((dialogueWidth * cameraWidth)/2);
        float yLocation = (cameraHeight/2) - ((gameWindowSizeY/Screen.height)*cameraHeight) - ((dialogueHeight * cameraHeight)/2);
        
        dialogue.transform.position = new Vector3(xLocation, yLocation, 0);
        
        PositionPortrait(cameraWidth, cameraHeight);
    }


    void PositionPortrait(float cameraWidth, float cameraHeight)
    {
        // Determine the dimensions of the portrait window
        float portraitSize = (((Screen.height - gameWindowSizeY) * percentOfDialogueArea) - (2*dialoguePadding*Screen.height));
        
        float portraitHeight =  portraitSize / Screen.height;
        float portraitWidth = portraitSize / Screen.width;

        portrait.transform.localScale = new Vector3(portraitWidth * cameraWidth, portraitHeight * cameraHeight, 1);

        // Determine the location of the portrait window
        float xLocation;
        float yLocation = (cameraHeight/2) - ((gameWindowSizeY/Screen.height)*cameraHeight) - ((portraitHeight * cameraHeight)/2) - (dialoguePadding * cameraHeight);

        // Change the location of the portrait to the left side
        if (ChangePortrait.whoIsTalking == 1)
        {
            xLocation = -cameraWidth/2 + ((portraitWidth * cameraWidth)/2) + (dialoguePadding * cameraHeight);
        }
        else
        {
            // Change the location of the portrait to the right side
            if (ChangePortrait.whoIsTalking == 2)
            {
                xLocation = -cameraWidth/2 + ((gameWindowSizeX / Screen.width) * cameraWidth) - ((portraitWidth * cameraWidth)/2) - (dialoguePadding * cameraHeight);
            }
            else xLocation = -1000;
        }
        
        portrait.transform.position = new Vector3(xLocation, yLocation, 0);
        
        // Change dialogue text location
        if (!GameManagerScript.inCombat)
        {
            float size = portraitHeight * Screen.height;
            float margin = (dialoguePadding * Screen.height);
            float xLoc = (gameWindowSizeX - size - (margin*3)) / 2;

            if (ChangePortrait.whoIsTalking == 1)
            {
                // The Player characters are speaking
                dialogueText.transform.position = new Vector3(xLoc + size + (margin * 2), Screen.height - gameWindowSizeY - (((Screen.height - gameWindowSizeY) * percentOfDialogueArea)/2), 0);
                dialogueText.alignment = TextAnchor.MiddleLeft;

                // Change textbox size
                float sizeDiffX = xLoc*2;
                float sizeDiffY = ((Screen.height - gameWindowSizeY) * percentOfDialogueArea) - (margin * 2);

                dialogueText.rectTransform.sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
            } 
            else if (ChangePortrait.whoIsTalking == 2)
            {
                // The Enemy characters are speaking
                dialogueText.transform.position = new Vector3( xLoc + margin, Screen.height - gameWindowSizeY - (((Screen.height - gameWindowSizeY) * percentOfDialogueArea)/2), 0);
                dialogueText.alignment = TextAnchor.MiddleRight;
                
                // Change textbox size
                float sizeDiffX = xLoc*2;
                float sizeDiffY = ((Screen.height - gameWindowSizeY) * percentOfDialogueArea) - (margin * 2);

                dialogueText.rectTransform.sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
            }
            else
            {
                // No characters are speaking
                dialogueText.transform.position = new Vector3(gameWindowSizeX/2, Screen.height - gameWindowSizeY - (((Screen.height - gameWindowSizeY) * percentOfDialogueArea)/2), 0);
                dialogueText.alignment = TextAnchor.MiddleCenter;

                // Change textbox size
                float sizeDiffX = (gameWindowSizeX - (margin * 2));
                float sizeDiffY = ((Screen.height - gameWindowSizeY) * percentOfDialogueArea) - (margin * 2);

                dialogueText.rectTransform.sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
            }
        }
        else dialogueText.transform.position = new Vector3(-500, - 500, 0);
    }
    
    
    void DetermineChoices(float cameraWidth, float cameraHeight)
    {
        int curScene = GameManagerScript.currentScene;
        Button button;

        // If the player is in a Combat Scene
        if (!GameManagerScript.inCombat && choices != null)
        {
            float smallerDialoguePercent = 0.6f;
            float smallerDialoguePadding = 0.03f;
            
            float largerDialoguePercent = 0.7f;
            float largerDialoguePadding = 0.035f;
            
            switch (choices.Length)
            {
                case 0:
                case 1:
                    // Larger dialogue box
                    percentOfDialogueArea = largerDialoguePercent;
                    dialoguePadding = largerDialoguePadding;
                    
                    PositionChoices(cameraWidth, cameraHeight, 1);
                    PositionPortrait(cameraWidth, cameraHeight);
                    break;
            
                case 2:
                    button = choices[0].GetComponent<Button>();
                    string tempText = button.GetComponentInChildren<Text>().text;
                    
                    //print(tempText);

                    if (tempText == "Netrixi" || tempText == "Approach Jester")
                    {
                        if (!metBrute)
                        {
                            // Smaller dialogue box
                            percentOfDialogueArea = smallerDialoguePercent;
                            dialoguePadding = smallerDialoguePadding;
                    
                            PositionChoices(cameraWidth, cameraHeight, 2);
                            PositionPortrait(cameraWidth, cameraHeight);
                        }
                        else
                        {
                            // Larger dialogue box
                            percentOfDialogueArea = largerDialoguePercent;
                            dialoguePadding = largerDialoguePadding;
                    
                            PositionChoices(cameraWidth, cameraHeight, 1);
                            PositionPortrait(cameraWidth, cameraHeight);
                        }
                    }
                    else if (tempText == "Brute")
                    {
                        if (!metBrute)
                        {
                            // Smaller dialogue box
                            percentOfDialogueArea = smallerDialoguePercent;
                            dialoguePadding = smallerDialoguePadding;
                    
                            PositionChoices(cameraWidth, cameraHeight, 2);
                            PositionPortrait(cameraWidth, cameraHeight);
                        }
                        else
                        {
                            // Larger dialogue box
                            percentOfDialogueArea = largerDialoguePercent;
                            dialoguePadding = largerDialoguePadding;
                    
                            PositionChoices(cameraWidth, cameraHeight, 1);
                            PositionPortrait(cameraWidth, cameraHeight);
                        }
                    }
                    else
                    {
                        // Smaller dialogue box
                        percentOfDialogueArea = smallerDialoguePercent;
                        dialoguePadding = smallerDialoguePadding;
                    
                        PositionChoices(cameraWidth, cameraHeight, 2);
                        PositionPortrait(cameraWidth, cameraHeight);
                    }
                    break;
            
                case 3:
                    // Larger dialogue box
                    percentOfDialogueArea = largerDialoguePercent;
                    dialoguePadding = largerDialoguePadding;
                    
                    PositionChoices(cameraWidth, cameraHeight, 1);
                    PositionPortrait(cameraWidth, cameraHeight);
                    break;
            }
        }
        else
        {
            PositionChoices(cameraWidth, cameraHeight, 0);
            portrait.transform.position = new Vector3(-1000,-1000,0);
        }
    }


    void PositionChoices(float cameraWidth, float cameraHeight, int numberOfChoices)
    {
        float percentOfArea = 1 - percentOfDialogueArea;
        float choiceWidth, choiceHeight;
        float xLocation, yLocation;
        float sizeDiffX, sizeDiffY;
        
        switch (numberOfChoices)
        {
            case 0:
                choice1.transform.position = new Vector3(-1000, -1000, 0);
                choice2.transform.position = new Vector3(-1000, -1000, 0);
                choice3.transform.position = new Vector3(-1000, -1000, 0);
                
                choice1Text.transform.position = new Vector3(-1000, -1000, 0);
                choice2Text.transform.position = new Vector3(-1000, -1000, 0);
                choice3Text.transform.position = new Vector3(-1000, -1000, 0);
                break;
            
            case 1:
                // Determine the dimensions of the choice window
                choiceWidth = gameWindowSizeX / Screen.width;
                choiceHeight = ((Screen.height - gameWindowSizeY) * percentOfArea) / Screen.height;
                
                choice1.transform.localScale = new Vector3(choiceWidth * cameraWidth, choiceHeight * cameraHeight, 1);
                
                // Determine the location of the choice window
                xLocation = -cameraWidth/2 + ((choiceWidth * cameraWidth)/2);
                yLocation = -cameraHeight/2 + ((choiceHeight * cameraHeight)/2);
        
                choice1.transform.position = new Vector3(xLocation, yLocation, 0);
                choice2.transform.position = new Vector3(-500, -500, 0);
                choice3.transform.position = new Vector3(-500, -500, 0);
                
                // Change choice text location
                choice1Text.transform.position = new Vector3(gameWindowSizeX/2, ((Screen.height - gameWindowSizeY) * percentOfArea)/2, 0);
                choice2Text.transform.position = new Vector3(-1000, -1000, 0);
                choice3Text.transform.position = new Vector3(-1000, -1000, 0);
                
                // Change choice text box sizes
                sizeDiffX = Screen.width - webcamSizeX;
                sizeDiffX -= 2 * (Screen.width * choiceXPadding);
                sizeDiffY = (Screen.height - gameWindowSizeY) * percentOfArea;
                sizeDiffY -= 2 * (Screen.height * choiceYPadding);

                choice1Text.rectTransform.sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
                break;
            
            case 2:
                // Determine the dimensions of the choice windows
                choiceWidth = (gameWindowSizeX / Screen.width)/2;
                choiceHeight = ((Screen.height - gameWindowSizeY) * percentOfArea) / Screen.height;
                
                choice1.transform.localScale = new Vector3(choiceWidth * cameraWidth, choiceHeight * cameraHeight, 1);
                choice2.transform.localScale = new Vector3(choiceWidth * cameraWidth, choiceHeight * cameraHeight, 1);
                
                // Determine the locations of the choice windows
                yLocation = -cameraHeight/2 + ((choiceHeight * cameraHeight)/2);
                choice3.transform.position = new Vector3(-500, -500, 0);
                
                xLocation = -cameraWidth/2 + ((choiceWidth * cameraWidth)/2);
                choice1.transform.position = new Vector3(xLocation, yLocation, 0);
                
                xLocation = -cameraWidth/2 + ((choiceWidth * cameraWidth)*3/2);
                choice2.transform.position = new Vector3(xLocation, yLocation, 0);

                // Change choice text locations
                choice1Text.transform.position = new Vector3(gameWindowSizeX/4, ((Screen.height - gameWindowSizeY) * percentOfArea)/2, 0);
                choice2Text.transform.position = new Vector3(gameWindowSizeX*3/4, ((Screen.height - gameWindowSizeY) * percentOfArea)/2, 0);
                choice3Text.transform.position = new Vector3(-1000, -1000, 0);
                
                // Change choice text box sizes
                sizeDiffX = (Screen.width - webcamSizeX) / 2;
                sizeDiffX -= 2 * (Screen.width * choiceXPadding);
                sizeDiffY = (Screen.height - gameWindowSizeY) * percentOfArea;
                sizeDiffY -= 2 * (Screen.height * choiceYPadding);

                choice1Text.rectTransform.sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
                choice2Text.rectTransform.sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
                break;
            
            case 3:
                // Determine the dimensions of the choice windows
                choiceWidth = (gameWindowSizeX / Screen.width)/3;
                choiceHeight = ((Screen.height - gameWindowSizeY) * percentOfArea) / Screen.height;
                
                choice1.transform.localScale = new Vector3(choiceWidth * cameraWidth, choiceHeight * cameraHeight, 1);
                choice2.transform.localScale = new Vector3(choiceWidth * cameraWidth, choiceHeight * cameraHeight, 1);
                choice3.transform.localScale = new Vector3(choiceWidth * cameraWidth, choiceHeight * cameraHeight, 1);
                
                // Determine the locations of the choice windows
                yLocation = -cameraHeight/2 + ((choiceHeight * cameraHeight)/2);
                
                xLocation = -cameraWidth/2 + ((choiceWidth * cameraWidth)/2);
                choice1.transform.position = new Vector3(xLocation, yLocation, 0);
                
                xLocation = -cameraWidth/2 + ((choiceWidth * cameraWidth)*3/2);
                choice2.transform.position = new Vector3(xLocation, yLocation, 0);
                
                xLocation = -cameraWidth/2 + ((choiceWidth * cameraWidth)*5/2);
                choice3.transform.position = new Vector3(xLocation, yLocation, 0);

                // Change choice text locations
                choice1Text.transform.position = new Vector3(gameWindowSizeX/6, ((Screen.height - gameWindowSizeY) * percentOfArea)/2, 0);
                choice2Text.transform.position = new Vector3(gameWindowSizeX*3/6, ((Screen.height - gameWindowSizeY) * percentOfArea)/2, 0);
                choice3Text.transform.position = new Vector3(gameWindowSizeX*5/6, ((Screen.height - gameWindowSizeY) * percentOfArea)/2, 0);
                
                // Change choice textbox sizes
                sizeDiffX = (Screen.width - webcamSizeX) / 3;
                sizeDiffX -= 2 * (Screen.width * choiceXPadding);
                sizeDiffY = (Screen.height - gameWindowSizeY) * percentOfArea;
                sizeDiffY -= 2 * (Screen.height * choiceYPadding);

                choice1Text.rectTransform.sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
                choice2Text.rectTransform.sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
                choice3Text.rectTransform.sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
                break;
        }
    }


    void PositionGrid(float containerWidth, float containerHeight, float cameraWidth, float cameraHeight)
    {
        float gridX = (cameraWidth/2) - ((containerWidth * cameraWidth)/2);
        float gridY = (cameraHeight/2) - ((containerHeight * cameraHeight)/2);
        
        float gridThickness = 0.25f;
        
        // Change scale of the grid
        SpriteRenderer row = gridRow1.GetComponent<SpriteRenderer>();
        SpriteRenderer column = gridColumn1.GetComponent<SpriteRenderer>();
        
        float rowScale = row.sprite.bounds.extents.x * 2;
        float rowThickness = row.sprite.bounds.extents.y * 2;
        float columnScale = column.sprite.bounds.extents.x * 2;
        float columnThickness = column.sprite.bounds.extents.y * 2;
        
        // Change scale of the grid rows
        float xScale = (containerWidth * (cameraWidth - (gridRowPadding*2)));
        float yThick = ((cameraHeight * gridThickness)/rowThickness);
        gridRow1.transform.localScale = new Vector3( xScale / rowScale, yThick, 1);
        gridRow2.transform.localScale = new Vector3( xScale / rowScale, yThick, 1);
        
        // Change scale of the grid columns
        float yScale = (containerHeight * (cameraHeight - (gridColumnPadding*2)));
        float xThick = ((cameraHeight * gridThickness)/columnThickness);
        gridColumn1.transform.localScale = new Vector3( xThick, yScale / columnScale, 1);
        gridColumn2.transform.localScale = new Vector3( xThick, yScale / columnScale, 1);
        
        // Position grid rows
        gridRow1.transform.position = new Vector3(gridX, (cameraHeight/2) - ((containerHeight * cameraHeight * 1)/3));
        gridRow2.transform.position = new Vector3(gridX, (cameraHeight/2) - ((containerHeight * cameraHeight * 2)/3));
        
        // Position grid columns
        gridColumn1.transform.position = new Vector3((cameraWidth/2) - ((containerWidth * cameraWidth * 1)/3), gridY);
        gridColumn2.transform.position = new Vector3((cameraWidth/2) - ((containerWidth * cameraWidth * 2)/3), gridY);
    }
}