using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameWindowManager : MonoBehaviour
{
    // Initial values for Beholder webcam
    public static float webcamSizeX = 640;
    public static float webcamSizeY = 480;
    
    public static float screenSizeX = 1920;
    public static float screenSizeY = 1080;

    public static float gameWindowSizeX, gameWindowSizeY;
    public static float windowWidth, windowHeight;

    public GameObject webcam;
    public Text webcamText;
    public static float webcamWidth, webcamHeight;
    
    public GameObject option1, option2, option3, option4;
    public Text option1Text, option2Text, option3Text, option4Text;
    public static float optionXPadding = 0.0125f, optionYPadding = 0.0125f;
    public static bool option1Centered, option2Centered, option3Centered;

    public GameObject background;

    public GameObject dialogue;
    public Text dialogueText;
    private string currentDialogue;
    public static float percentOfDialogueArea = 1;

    public GameObject portrait;
    public static float portraitYDimension, portraitYLocation;
    public static float dialoguePadding = 0.025f;
    public static float portraitSizeX;
    
    public static float smallerDialoguePercent = 0.65f;
    public static float largerDialoguePercent = 0.65f;

    public GameObject combatTips, tipLine;
    public Text combatTipText;
    public static float tipMarginX = 0;
    public static float tipMarginY = 0;
    public static float tipInnerMargin = 0.02f;

    public GameObject hand, gridRow1, gridRow2, gridColumn1, gridColumn2;
    public static float gridRowPadding, gridColumnPadding;

    public GameObject choice1, choice2, choice3;
    public Text choice1Text, choice2Text, choice3Text;
    public GameObject choiceBackground;
    
    private GameObject[] choices;
    public static float choiceXPadding = 0.0125f, choiceYPadding = 0.0125f;
    
    public static string speakerName;

    public static bool metBrute = true;

    public static GameWindowManager S;
    
    public static float cameraHeight, cameraWidth;

    public static bool choicesReset = false;

    public static float offScreen = 10000;

    public GameObject handAnimation1, handAnimation2, handAnimation3;
    private bool hand1Created, hand2Created, hand3Created;

    public static int largerChoiceFont = 58;
    public static int smallerChoiceFont = 48;

    public GameObject leftCurtain, rightCurtain;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        S = this;
    }

    void Start()
    {
        choices = GameObject.FindGameObjectsWithTag("Dialogue Choice");
    }

    // Update is called once per frame
    void Update()
    {
        gameWindowSizeX = screenSizeX - webcamSizeX;
        gameWindowSizeY = (int) (gameWindowSizeX / 16) * 9;
        
        // Determine the dimensions of the camera within the scene
        Camera camera = Camera.main;
        cameraHeight = 2f * camera.orthographicSize;
        cameraWidth = cameraHeight * camera.aspect;
        
        // Determine the dimensions of the player portrait
        portraitYDimension = (((screenSizeY - gameWindowSizeY) * smallerDialoguePercent) - (2*dialoguePadding*screenSizeY));
        portraitYLocation = (cameraHeight/2) - ((gameWindowSizeY/screenSizeY)*cameraHeight) - (((portraitYDimension/screenSizeY) * cameraHeight)/2) - (dialoguePadding * cameraHeight);

        // Determine how many dialogue choices there are
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


        // If the player is in a combat scene
        if (GameManagerScript.inCombat)
        {
            ChangeGameWindow(cameraWidth, cameraHeight);
            
            percentOfDialogueArea = 1f;
            PositionDialogue(cameraWidth, cameraHeight);
            
            if (!choicesReset)
            {
                ChangeChoiceText.S.ResetChoices(false);
                choicesReset = true;
            }

            ChangePortrait.whoIsTalking = 0;
            PositionPortrait(cameraWidth, cameraHeight);

            if (CombatManagerScript.netrixiAttacks || CombatManagerScript.folkvarAttacks || CombatManagerScript.ivAttacks)
            {
                PositionOptions(cameraWidth, cameraHeight, 4);
            }
            else
            {
                if (CombatManagerScript.hasRunSimulation)
                {
                    PositionOptions(cameraWidth, cameraHeight, 4);
                }
                else PositionOptions(cameraWidth, cameraHeight, 3);
            }
        }
        else
        {
            choicesReset = false;

            option4Text.transform.position = new Vector3(offScreen, offScreen, 0);

            option1Centered = false;
            option2Centered = false;
            option3Centered = false;
            
            ArrangeScreen();
            
            PositionCurtains();
        }
    }

    public void ArrangeScreen()
    {
        Array.Clear(choices,0,choices.Length);

        // Determine dimensions of the camera within the scene
        Camera camera = Camera.main;
        cameraHeight = 2f * camera.orthographicSize;
        cameraWidth = cameraHeight * camera.aspect;

        // Define the margins for the grid rows and columns
        gridRowPadding = (cameraWidth * 0.025f);
        gridColumnPadding = (cameraHeight * 0.025f);

        // Arrange the positions for all the elements of the game
        PositionWebcam(cameraWidth, cameraHeight);
        
        PositionOptions(cameraWidth, cameraHeight, 3);
        
        ChangeGameWindow(cameraWidth, cameraHeight);
        
        choices = GameObject.FindGameObjectsWithTag("Dialogue Choice");
        PositionDialogue(cameraWidth, cameraHeight);
    }

    public void PositionCurtains()
    {
        // Determine dimensions of the camera within the scene
        Camera camera = Camera.main;
        cameraHeight = 2f * camera.orthographicSize;

        // Determine the dimensions of the curtains
        leftCurtain.transform.localScale = new Vector3(1,1,1);
        rightCurtain.transform.localScale = new Vector3(1,1,1);
        
        SpriteRenderer sr1 = leftCurtain.GetComponent<SpriteRenderer>();
        float curtainHeight = sr1.bounds.extents.y * 2;
        float scaleFactor = cameraHeight / curtainHeight;

        leftCurtain.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
        rightCurtain.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
    }


    void PositionWebcam(float cameraWidth, float cameraHeight)
    {
        ResetLocalScale(webcam);
        SpriteRenderer sr = webcam.GetComponent<SpriteRenderer>();
        float currWidth = sr.bounds.extents.x * 2;
        float currHeight = sr.bounds.extents.y * 2;
        
        // Determine the dimensions of the webcam
        webcamWidth = webcamSizeX / screenSizeX;
        webcamHeight = webcamSizeY / screenSizeY;
        
        webcam.transform.localScale = new Vector3(webcamWidth * cameraWidth / currWidth, webcamHeight * cameraHeight / currHeight, 1);

        // Determine the location of the webcam
        float xLocation = (cameraWidth/2) - ((webcamWidth * cameraWidth)/2);
        float yLocation = (cameraHeight/2) - ((webcamHeight * cameraHeight)/2);
        
        webcam.transform.position = new Vector3(xLocation, yLocation, 0);
        
        // Change webcam text location
        webcamText.transform.position = new Vector3(Screen.width - (webcamWidth*Screen.width/2), Screen.height - (webcamHeight*Screen.height/2), 0);
        
        // Change webcam text box size
        float sizeDiffX = webcamWidth*Screen.width - (4 * (Screen.width * optionXPadding));
        float sizeDiffY = webcamHeight*Screen.height - (4 * (Screen.height * optionYPadding));
        
        webcamText.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDiffX, sizeDiffY);

        //print(sizeDiffX + ", " + sizeDiffY);
        
        // Change scale of hand icon
        HandManagerScript.ChangeHandScale(hand, webcamWidth, cameraWidth);

        // Change location of hand icon
        float handXPosition = xLocation - ((webcamWidth * cameraWidth)/2);
        float handYPosition = yLocation - ((webcamHeight * cameraHeight)/2);

        HandManagerScript.xPosWebcam = new[] {handXPosition + ((webcamWidth * cameraWidth * 1.5f)/9), handXPosition + ((webcamWidth * cameraWidth * 1)/2), handXPosition + ((webcamWidth * cameraWidth * 7.5f)/9)};
        HandManagerScript.yPosWebcam = new[] {handYPosition + ((webcamHeight * cameraHeight * 7.5f)/9), handYPosition + ((webcamHeight * cameraHeight * 1)/2), handYPosition + ((webcamHeight * cameraHeight * 1.5f)/9)};
        HandManagerScript.ChangeHandLocation(hand);

        PositionGrid(webcamWidth, webcamHeight, cameraWidth, cameraHeight);
    }


    void PositionOptions(float cameraWidth, float cameraHeight, int optionNumber)
    {
        ResetLocalScale(option1);
        ResetLocalScale(option2);
        ResetLocalScale(option3);
        ResetLocalScale(option4);
        SpriteRenderer sr = option1.GetComponent<SpriteRenderer>();
        float currWidth = sr.bounds.extents.x * 2;
        float currHeight = sr.bounds.extents.y * 2;
        
        // Determine the dimensions of the option
        float optionWidth = webcamSizeX / screenSizeX;
        float optionHeight = ((screenSizeY - webcamSizeY) / screenSizeY);

        switch (optionNumber)
        {
            case 3: 
                option1.transform.localScale = new Vector3(optionWidth * cameraWidth / currWidth, (optionHeight/3) * cameraHeight / currHeight, 1);
                option2.transform.localScale = new Vector3(optionWidth * cameraWidth / currWidth, (optionHeight/3) * cameraHeight / currHeight, 1);
                option3.transform.localScale = new Vector3(optionWidth * cameraWidth / currWidth, (optionHeight/3) * cameraHeight / currHeight, 1);

                ChangeHandAnimation.optionHeight = optionHeight / 3 * cameraHeight;
                break;
                
            case 4: 
                option1.transform.localScale = new Vector3(optionWidth * cameraWidth / currWidth, (optionHeight*3/11) * cameraHeight / currHeight, 1);
                option2.transform.localScale = new Vector3(optionWidth * cameraWidth / currWidth, (optionHeight*3/11) * cameraHeight / currHeight, 1);
                option3.transform.localScale = new Vector3(optionWidth * cameraWidth / currWidth, (optionHeight*3/11) * cameraHeight / currHeight, 1);
                option4.transform.localScale = new Vector3(optionWidth * cameraWidth / currWidth, (optionHeight*2/11) * cameraHeight / currHeight, 1);
                
                ChangeHandAnimation.optionHeight = optionHeight*3/11 * cameraHeight;
                break;
        }
        
        // Determine the location of the option
        float xLocation = (cameraWidth/2) - ((optionWidth * cameraWidth)/2);
        float yLocation = (-cameraHeight/2) + ((optionHeight * cameraHeight)/2);

        switch (optionNumber)
        {
            case 3:
                yLocation = (-cameraHeight / 2) + (((optionHeight / 3) * cameraHeight) * 5 / 2);
                option1.transform.position = new Vector3(xLocation, yLocation, 0);

                yLocation = (-cameraHeight / 2) + (((optionHeight / 3) * cameraHeight) * 3 / 2);
                option2.transform.position = new Vector3(xLocation, yLocation, 0);

                yLocation = (-cameraHeight / 2) + (((optionHeight / 3) * cameraHeight) / 2);
                option3.transform.position = new Vector3(xLocation, yLocation, 0);

                option4.transform.position = new Vector3(offScreen, offScreen, 0);
                break;

            case 4:
                yLocation = (-cameraHeight / 2) + (optionHeight * cameraHeight) - (((optionHeight * 3 / 11) * cameraHeight) / 2);
                option1.transform.position = new Vector3(xLocation, yLocation, 0);

                yLocation = (-cameraHeight / 2) + (optionHeight * cameraHeight) - (((optionHeight * 3 / 11) * cameraHeight) * 3 / 2);
                option2.transform.position = new Vector3(xLocation, yLocation, 0);

                yLocation = (-cameraHeight / 2) + (optionHeight * cameraHeight) - (((optionHeight * 3 / 11) * cameraHeight) * 5 / 2);
                option3.transform.position = new Vector3(xLocation, yLocation, 0);

                yLocation = (-cameraHeight / 2) + (((optionHeight * 2 / 11) * cameraHeight) / 2);
                option4.transform.position = new Vector3(xLocation, yLocation, 0);
                break;
        }
        
        // Change the dimensions of the hand animations
        ChangeHandAnimation.optionWidth = (optionWidth * cameraWidth) / 3;
        ChangeHandAnimation.optionXLocation = (cameraWidth / 2) - (((webcamWidth * cameraWidth) * 11/ 30) / 2);
        ChangeHandAnimation.optionPadding = optionYPadding * cameraHeight;

        // Change the location of the hand animations
        if (optionNumber == 3)
        {
            ChangeHandAnimation.optionYLocation1 = (-cameraHeight / 2) + (((optionHeight / 3) * cameraHeight) * 5 / 2);
            ChangeHandAnimation.optionYLocation2 = (-cameraHeight / 2) + (((optionHeight / 3) * cameraHeight) * 3 / 2);
            ChangeHandAnimation.optionYLocation3 = (-cameraHeight / 2) + (((optionHeight / 3) * cameraHeight) / 2);
        }
        else
        {
            ChangeHandAnimation.optionYLocation1 = (-cameraHeight / 2) + (optionHeight * cameraHeight) - (((optionHeight * 3 / 11) * cameraHeight) / 2);
            ChangeHandAnimation.optionYLocation2 = (-cameraHeight / 2) + (optionHeight * cameraHeight) - (((optionHeight * 3 / 11) * cameraHeight) * 3 / 2);
            ChangeHandAnimation.optionYLocation3 = (-cameraHeight / 2) + (optionHeight * cameraHeight) - (((optionHeight * 3 / 11) * cameraHeight) * 5 / 2);
        }

        // Change option text location
        float xLeft = Screen.width - ((webcamWidth*Screen.width)*2/3);
        float xCentered = Screen.width - ((webcamWidth*Screen.width)/2);
        float yHeight = (Screen.height - (webcamHeight*Screen.height));
        
        float sizeXLeft = (webcamWidth*Screen.width*2/3) - (2 * (Screen.width * optionXPadding));
        float sizeXCentered = (webcamWidth*Screen.width) - (2 * (Screen.width * optionXPadding));
        float sizeDiffY = (Screen.height - (webcamHeight*Screen.height));

        switch (optionNumber)
        {
            case 3:
                if (option1Centered) option1Text.transform.position = new Vector3( xCentered, (yHeight/3)*5/2, 0);
                else option1Text.transform.position = new Vector3(xLeft, (yHeight/3)*5/2, 0);

                if (option2Centered) option2Text.transform.position = new Vector3(xCentered, (yHeight/3)*3/2, 0);
                else option2Text.transform.position = new Vector3(xLeft, (yHeight/3)*3/2, 0);

                if (option3Centered) option3Text.transform.position = new Vector3(xCentered, (yHeight/3)/2, 0);
                else option3Text.transform.position = new Vector3(xLeft, (yHeight/3)/2, 0);
                
                
                sizeDiffY /= 3;
                sizeDiffY -= 2 * (Screen.height * optionYPadding);
                
                option4Text.transform.position = new Vector3(offScreen, offScreen, 0);
                break;
            
            case 4:
                if (option1Centered) option1Text.transform.position = new Vector3(xCentered, (yHeight*2/11) + (yHeight*3/11)*5/2, 0);
                else option1Text.transform.position = new Vector3(xLeft, (yHeight*2/11) + (yHeight*3/11)*5/2, 0);
                
                if (option2Centered) option2Text.transform.position = new Vector3(xCentered, (yHeight*2/11) + (yHeight*3/11)*3/2, 0);
                else option2Text.transform.position = new Vector3(xLeft, (yHeight*2/11) + (yHeight*3/11)*3/2, 0);
                
                if (option3Centered) option3Text.transform.position = new Vector3(xCentered, (yHeight*2/11) + (yHeight*3/11)/2, 0);
                else option3Text.transform.position = new Vector3(xLeft, (yHeight*2/11) + (yHeight*3/11)/2, 0);
                
                
                option4Text.transform.position = new Vector3(Screen.width - ((webcamWidth*Screen.width)/2), (yHeight*2/11)/2, 0);
                sizeDiffY /= 7;
                break;
        }
        
        // Change option textbox sizes
        if (optionNumber == 4)
        {
            float sizeDiffYLarge = (sizeDiffY * 2) - (2 * (Screen.height * optionYPadding));
            float sizeDiffYSmall = sizeDiffY - (2 * (Screen.height * optionYPadding));
            
            if (option1Centered) option1Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeXCentered, sizeDiffYLarge);
            else option1Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeXLeft, sizeDiffYLarge);
            
            if (option2Centered) option2Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeXCentered, sizeDiffYLarge);
            else option2Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeXLeft, sizeDiffYLarge);
            
            if (option3Centered) option3Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeXCentered, sizeDiffYLarge);
            else option3Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeXLeft, sizeDiffYLarge);
            
            option4Text.GetComponent<RectTransform>().sizeDelta = new Vector2((webcamWidth*Screen.width) - (2 * (Screen.width * optionXPadding)), sizeDiffYSmall);
        }
        else
        {
            if (option1Centered) option1Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeXCentered, sizeDiffY);
            else option1Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeXLeft, sizeDiffY);
            
            if (option2Centered) option2Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeXCentered, sizeDiffY);
            else option2Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeXLeft, sizeDiffY);
            
            if (option3Centered) option3Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeXCentered, sizeDiffY);
            else option3Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeXLeft, sizeDiffY);
        }
    }


    void ChangeGameWindow(float cameraWidth, float cameraHeight)
    {
        gameWindowSizeX = screenSizeX - webcamSizeX;
        gameWindowSizeY = (int) (gameWindowSizeX / 16) * 9;

        // Determine the dimensions of the game window
        windowWidth = gameWindowSizeX / screenSizeX;
        windowHeight = gameWindowSizeY / screenSizeY;
        
        SpriteRenderer sr = background.GetComponent<SpriteRenderer>();

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

        // Determine combat dimensions
        ChangeCombatWindow(windowWidth, windowHeight);
    }


    void ChangeCombatWindow(float scaleX, float scaleY)
    {
        GameObject screenResize = GameObject.FindGameObjectWithTag("Resize");

        if (screenResize != null)
        {
            // Determine the dimensions of the combat window
            screenResize.transform.localScale = new Vector3(scaleX, scaleX, 1);
        
            // Determine the location of the combat window
            // Determine the dimensions of the camera within the scene
            Camera camera = Camera.main;
            cameraHeight = 2f * camera.orthographicSize;
            cameraWidth = cameraHeight * camera.aspect;
            
            float xLocation = -cameraWidth/2 + ((scaleX * cameraWidth)/2);
            float yLocation = cameraHeight/2 - (scaleY * cameraHeight)/2;
        
            screenResize.transform.position = new Vector3(xLocation, yLocation, 0);

            if (GameManagerScript.inCombat)
            {
                // Changing values for where characters are displayed
                CharacterMovement.ChangeXLocations(scaleX, xLocation);
                CharacterMovement.ChangeYLocations(scaleY, yLocation);
            
                // Re-populate the dialogue section with combat text
                ChangeCombatText.S.ChangeTextLocations(windowWidth*Screen.width, windowHeight*Screen.height);
            }
        }
    }


    void PositionDialogue(float cameraWidth, float cameraHeight)
    {
        // Determine number of choices for the player
        DetermineChoices(cameraWidth, cameraHeight);
        
        ResetLocalScale(dialogue);
        SpriteRenderer sr = dialogue.GetComponent<SpriteRenderer>();
        float currWidth = sr.bounds.extents.x * 2;
        float currHeight = sr.bounds.extents.y * 2;

        // Determine the dimensions of the dialogue window
        float dialogueWidth = windowWidth;
        float dialogueHeight = ((screenSizeY - gameWindowSizeY) * percentOfDialogueArea) / screenSizeY;
        
        dialogue.transform.localScale = new Vector3(dialogueWidth * cameraWidth / currWidth, dialogueHeight * cameraHeight / currHeight, 1);

        // Determine the location of the dialogue window
        float xLocation = -cameraWidth/2 + ((dialogueWidth * cameraWidth)/2);
        float yLocation = (cameraHeight/2) - ((windowHeight)*cameraHeight) - ((dialogueHeight * cameraHeight)/2);
        
        dialogue.transform.position = new Vector3(xLocation, yLocation, 0);
        
        PositionPortrait(cameraWidth, cameraHeight);

        // Position combat tips
        if (GameManagerScript.inCombat)
        {
            ResetLocalScale(combatTips);
            SpriteRenderer sr2 = combatTips.GetComponent<SpriteRenderer>();
            float currWidth1 = sr2.bounds.extents.x * 2;
            float currHeight1 = sr2.bounds.extents.y * 2;
            
            // Determine the dimensions of the combat tip window
            float tipWidth = (dialogueWidth/2) - (2 * tipMarginX);
            float tipHeight = dialogueHeight - (2 * tipMarginY);
            
            combatTips.transform.localScale = new Vector3( tipWidth * cameraWidth / currWidth1, tipHeight * cameraHeight / currHeight1, 1);
                
            // Determine the location of the combat tip window
            float xPos = -cameraWidth / 2 + ((dialogueWidth * cameraWidth) * 3 / 4);
            
            combatTips.transform.position = new Vector3(xPos, yLocation, 0);
            

            // Position combat tip text
            combatTipText.transform.position = new Vector3((gameWindowSizeX/screenSizeX)*(Screen.width)*3/4, (Screen.height - (windowHeight*Screen.height))*7/18, 0);

            // Change combat tip textbox size
            float sizeDiffX = ((gameWindowSizeX/screenSizeX*Screen.width)/2) - (tipInnerMargin*4*Screen.width);
            float sizeDiffY = ((Screen.height - (windowHeight*Screen.height))*2/3) - (tipInnerMargin*3*Screen.height);

            combatTipText.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
            

            // Determine the dimensions of the tip line
            SpriteRenderer sr1 = tipLine.GetComponent<SpriteRenderer>();

            float lineWidth = sr1.sprite.bounds.extents.x * 2;
            float scaleFactor = (tipWidth - (2 * tipInnerMargin)) * cameraWidth / lineWidth;
                
            tipLine.transform.localScale = new Vector3( scaleFactor, scaleFactor/2, 1);
            
            // Determine the location of the tip line
            float yPos = (cameraHeight/2) - ((windowHeight)*cameraHeight) - ((dialogueHeight * cameraHeight)*2.5f/8);
            tipLine.transform.position = new Vector3(xPos, yPos, 0);
        }
    }


    void PositionPortrait(float cameraWidth, float cameraHeight)
    {
        // Determine the dimensions of the portrait window
        
        ResetLocalScale(portrait);
        SpriteRenderer sr = portrait.GetComponent<SpriteRenderer>();
        float currWidth = sr.bounds.extents.x * 2;
        float currHeight = sr.bounds.extents.y * 2;

        float portraitHeight =  portraitYDimension / screenSizeY;
        float portraitWidth = portraitYDimension / screenSizeX;

        portraitSizeX = portraitWidth * cameraWidth;

        float scaleDifference = portraitWidth * cameraWidth / currWidth;

        portrait.transform.localScale = new Vector3(scaleDifference, scaleDifference, 1);

        // Determine the location of the portrait window
        float xLocation;

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
                xLocation = -cameraWidth/2 + ((gameWindowSizeX / screenSizeX) * cameraWidth) - ((portraitWidth * cameraWidth)/2) - (dialoguePadding * cameraHeight);
            }
            else xLocation = offScreen;
        }
        
        portrait.transform.position = new Vector3(xLocation, portraitYLocation, 0);
        
        
        // Change dialogue text location
        if (!GameManagerScript.inCombat)
        {
            float size = portraitHeight * Screen.height;
            float margin = (dialoguePadding * Screen.height);

            if (ChangePortrait.whoIsTalking == 1)
            {
                // The Player characters are speaking
                dialogueText.transform.position = new Vector3((margin*2.5f) + size + (((gameWindowSizeX/screenSizeX*Screen.width) - size - (margin*5))/2), Screen.height - (windowHeight*Screen.height) - (((Screen.height - (windowHeight*Screen.height)) * percentOfDialogueArea)/2), 0);
                dialogueText.alignment = TextAnchor.MiddleLeft;

                // Change textbox size
                float sizeDiffX = ((gameWindowSizeX/screenSizeX*Screen.width) - size - (margin*5));
                float sizeDiffY = ((Screen.height - (windowHeight*Screen.height)) * percentOfDialogueArea) - (margin * 2);

                dialogueText.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
            } 
            else if (ChangePortrait.whoIsTalking == 2)
            {
                // The Enemy characters are speaking
                dialogueText.transform.position = new Vector3( (margin*2.5f) + (((gameWindowSizeX/screenSizeX*Screen.width) - size - (margin*5))/2), Screen.height - (windowHeight*Screen.height) - (((Screen.height - (windowHeight*Screen.height)) * percentOfDialogueArea)/2), 0);
                dialogueText.alignment = TextAnchor.MiddleRight;
                
                // Change textbox size
                float sizeDiffX = ((gameWindowSizeX/screenSizeX*Screen.width) - size - (margin*5));
                float sizeDiffY = ((Screen.height - (windowHeight*Screen.height)) * percentOfDialogueArea) - (margin * 2);
    
                dialogueText.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
            }
            else
            {
                // No characters are speaking
                dialogueText.transform.position = new Vector3((windowWidth*Screen.width)/2, Screen.height - (windowHeight*Screen.height) - (((Screen.height - (windowHeight*Screen.height)) * percentOfDialogueArea)/2), 0);
                dialogueText.alignment = TextAnchor.MiddleCenter;

                // Change textbox size
                float sizeDiffX = ((gameWindowSizeX/screenSizeX*Screen.width) - (margin * 4));
                float sizeDiffY = ((Screen.height - (windowHeight*Screen.height)) * percentOfDialogueArea) - (margin * 2);

                dialogueText.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
            }
        }
        else dialogueText.transform.position = new Vector3(offScreen, offScreen, 0);
    }
    
    
    void DetermineChoices(float cameraWidth, float cameraHeight)
    {
        int curScene = GameManagerScript.currentScene;
        Button button;

        // If the player is not in a Combat Scene
        if (!GameManagerScript.inCombat)
        {
            switch (choices.Length)
            {
                case 0:
                    // Larger dialogue box
                    percentOfDialogueArea = largerDialoguePercent;

                    PositionChoices(cameraWidth, cameraHeight, 1);
                    PositionPortrait(cameraWidth, cameraHeight);

                    ChangeChoiceText.S.ChangeChoices(false, choices, 0);

                    choice1Text.fontSize = largerChoiceFont;
                    break;

                case 1:
                    // Larger dialogue box
                    percentOfDialogueArea = largerDialoguePercent;

                    PositionChoices(cameraWidth, cameraHeight, 1);
                    PositionPortrait(cameraWidth, cameraHeight);
                    
                    ChangeChoiceText.S.ChangeChoices(false, choices, 1);
                    
                    choice1Text.fontSize = largerChoiceFont;
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

                            PositionChoices(cameraWidth, cameraHeight, 2);
                            PositionPortrait(cameraWidth, cameraHeight);
                            
                            ChangeChoiceText.S.ChangeChoices(true, choices, 2);
                            
                            choice1Text.fontSize = smallerChoiceFont;
                        }
                        else
                        {
                            // Larger dialogue box
                            percentOfDialogueArea = largerDialoguePercent;

                            PositionChoices(cameraWidth, cameraHeight, 1);
                            PositionPortrait(cameraWidth, cameraHeight);
                            
                            ChangeChoiceText.S.ChangeChoices(false, choices, 2);
                            
                            choice1Text.fontSize = largerChoiceFont;
                        }
                    }
                    else if (tempText == "Brute")
                    {
                        if (!metBrute)
                        {
                            // Smaller dialogue box
                            percentOfDialogueArea = smallerDialoguePercent;

                            PositionChoices(cameraWidth, cameraHeight, 2);
                            PositionPortrait(cameraWidth, cameraHeight);
                            
                            ChangeChoiceText.S.ChangeChoices(true, choices, 2);
                            
                            choice1Text.fontSize = smallerChoiceFont;
                        }
                        else
                        {
                            // Larger dialogue box
                            percentOfDialogueArea = largerDialoguePercent;

                            PositionChoices(cameraWidth, cameraHeight, 1);
                            PositionPortrait(cameraWidth, cameraHeight);
                            
                            ChangeChoiceText.S.ChangeChoices(false, choices, 2);
                            
                            choice1Text.fontSize = largerChoiceFont;
                        }
                    }
                    else if (GameManagerScript.currentScene == 31) 
                    {
                        // Larger dialogue box
                        percentOfDialogueArea = largerDialoguePercent;

                        PositionChoices(cameraWidth, cameraHeight, 1);
                        PositionPortrait(cameraWidth, cameraHeight);
                            
                        ChangeChoiceText.S.ChangeChoices(false, choices, 2);
                            
                        choice1Text.fontSize = largerChoiceFont;
                    }
                    else
                    {
                        // Smaller dialogue box
                        percentOfDialogueArea = smallerDialoguePercent;

                        PositionChoices(cameraWidth, cameraHeight, 2);
                        PositionPortrait(cameraWidth, cameraHeight);
                        
                        ChangeChoiceText.S.ChangeChoices(true, choices, 2);
                        
                        choice1Text.fontSize = smallerChoiceFont;
                    }
                    break;
            
                case 3:
                    // Larger dialogue box
                    percentOfDialogueArea = largerDialoguePercent;

                    PositionChoices(cameraWidth, cameraHeight, 1);
                    PositionPortrait(cameraWidth, cameraHeight);
                    
                    ChangeChoiceText.S.ChangeChoices(false, choices, 3);
                    
                    choice1Text.fontSize = largerChoiceFont;
                    break;
            }
        }
        else
        {
            PositionChoices(cameraWidth, cameraHeight, 0);
            portrait.transform.position = new Vector3(offScreen,offScreen,0);
        }
    }


    void PositionChoices(float cameraWidth, float cameraHeight, int numberOfChoices)
    {
        float percentOfArea = 1 - percentOfDialogueArea;
        float choiceWidth, choiceHeight;
        float xLocation, yLocation;
        float sizeDiffX, sizeDiffY;
        
        ResetLocalScale(choice1);
        ResetLocalScale(choice2);
        ResetLocalScale(choice3);
        SpriteRenderer sr = choice1.GetComponent<SpriteRenderer>();
        float currWidth = sr.bounds.extents.x * 2;
        float currHeight = sr.bounds.extents.y * 2;
        
        switch (numberOfChoices)
        {
            case 0:
                choice1.transform.position = new Vector3(offScreen, offScreen, 0);
                choice2.transform.position = new Vector3(offScreen, offScreen, 0);
                choice3.transform.position = new Vector3(offScreen, offScreen, 0);
                
                choice1Text.transform.position = new Vector3(offScreen, offScreen, 0);
                choice2Text.transform.position = new Vector3(offScreen, offScreen, 0);
                choice3Text.transform.position = new Vector3(offScreen, offScreen, 0);
                break;
            
            case 1:
                // Determine the dimensions of the choice window
                choiceWidth = gameWindowSizeX / screenSizeX;
                choiceHeight = ((screenSizeY - gameWindowSizeY) * percentOfArea) / screenSizeY;
                
                choice1.transform.localScale = new Vector3(choiceWidth * cameraWidth / currWidth, choiceHeight * cameraHeight / currHeight, 1);
                
                // Determine the location of the choice window
                xLocation = -cameraWidth/2 + ((choiceWidth * cameraWidth)/2);
                yLocation = -cameraHeight/2 + ((choiceHeight * cameraHeight)/2);
        
                choice1.transform.position = new Vector3(xLocation, yLocation, 0);
                choice2.transform.position = new Vector3(offScreen, offScreen, 0);
                choice3.transform.position = new Vector3(offScreen, offScreen, 0);
                
                // Change choice text location
                choice1Text.transform.position = new Vector3((windowWidth*Screen.width)/2, ((Screen.height - (windowHeight*Screen.height)) * percentOfArea)/2, 0);
                choice2Text.transform.position = new Vector3(offScreen, offScreen, 0);
                choice3Text.transform.position = new Vector3(offScreen, offScreen, 0);
                
                // Change choice text box sizes
                sizeDiffX = Screen.width - (webcamWidth*Screen.width);
                sizeDiffX -= 2 * (Screen.width * choiceXPadding);
                sizeDiffY = (Screen.height - (windowHeight*Screen.height)) * percentOfArea;
                sizeDiffY -= 2 * (Screen.height * choiceYPadding);

                choice1Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
                break;
            
            case 2:
                // Determine the dimensions of the choice windows
                choiceWidth = (gameWindowSizeX / screenSizeX)/2;
                choiceHeight = ((screenSizeY - gameWindowSizeY) * percentOfArea) / screenSizeY;
                
                choice1.transform.localScale = new Vector3(choiceWidth * cameraWidth / currWidth, choiceHeight * cameraHeight / currHeight, 1);
                choice2.transform.localScale = new Vector3(choiceWidth * cameraWidth / currWidth, choiceHeight * cameraHeight / currHeight, 1);
                
                // Determine the locations of the choice windows
                yLocation = -cameraHeight/2 + ((choiceHeight * cameraHeight)/2);
                choice3.transform.position = new Vector3(offScreen, offScreen, 0);
                
                xLocation = -cameraWidth/2 + ((choiceWidth * cameraWidth)/2);
                choice1.transform.position = new Vector3(xLocation, yLocation, 0);
                
                xLocation = -cameraWidth/2 + ((choiceWidth * cameraWidth)*3/2);
                choice2.transform.position = new Vector3(xLocation, yLocation, 0);

                // Change choice text locations
                choice1Text.transform.position = new Vector3((windowWidth*Screen.width)/4, ((Screen.height - (windowHeight*Screen.height)) * percentOfArea)/2, 0);
                choice2Text.transform.position = new Vector3((windowWidth*Screen.width)*3/4, ((Screen.height - (windowHeight*Screen.height)) * percentOfArea)/2, 0);
                choice3Text.transform.position = new Vector3(offScreen, offScreen, 0);
                
                // Change choice text box sizes
                sizeDiffX = (Screen.width - (webcamWidth*Screen.width)) / 2;
                sizeDiffX -= 2 * (Screen.width * choiceXPadding);
                sizeDiffY = (Screen.height - (windowHeight*Screen.height)) * percentOfArea;
                sizeDiffY -= 2 * (Screen.height * choiceYPadding);

                choice1Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
                choice2Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
                break;
            
            case 3:
                // Determine the dimensions of the choice windows
                choiceWidth = (gameWindowSizeX / screenSizeX)/3;
                choiceHeight = ((screenSizeY - gameWindowSizeY) * percentOfArea) / screenSizeY;
                
                choice1.transform.localScale = new Vector3(choiceWidth * cameraWidth / currWidth, choiceHeight * cameraHeight / currHeight, 1);
                choice2.transform.localScale = new Vector3(choiceWidth * cameraWidth / currWidth, choiceHeight * cameraHeight / currHeight, 1);
                choice3.transform.localScale = new Vector3(choiceWidth * cameraWidth / currWidth, choiceHeight * cameraHeight / currHeight, 1);
                
                // Determine the locations of the choice windows
                yLocation = -cameraHeight/2 + ((choiceHeight * cameraHeight)/2);
                
                xLocation = -cameraWidth/2 + ((choiceWidth * cameraWidth)/2);
                choice1.transform.position = new Vector3(xLocation, yLocation, 0);
                
                xLocation = -cameraWidth/2 + ((choiceWidth * cameraWidth)*3/2);
                choice2.transform.position = new Vector3(xLocation, yLocation, 0);
                
                xLocation = -cameraWidth/2 + ((choiceWidth * cameraWidth)*5/2);
                choice3.transform.position = new Vector3(xLocation, yLocation, 0);

                // Change choice text locations
                choice1Text.transform.position = new Vector3((windowWidth*Screen.width)/6, ((Screen.height - (windowHeight*Screen.height)) * percentOfArea)/2, 0);
                choice2Text.transform.position = new Vector3((windowWidth*Screen.width)*3/6, ((Screen.height - (windowHeight*Screen.height)) * percentOfArea)/2, 0);
                choice3Text.transform.position = new Vector3((windowWidth*Screen.width)*5/6, ((Screen.height - (windowHeight*Screen.height)) * percentOfArea)/2, 0);
                
                // Change choice textbox sizes
                sizeDiffX = (Screen.width - (webcamWidth*Screen.width)) / 3;
                sizeDiffX -= 2 * (Screen.width * choiceXPadding);
                sizeDiffY = (Screen.height - (windowHeight*Screen.height)) * percentOfArea;
                sizeDiffY -= 2 * (Screen.height * choiceYPadding);

                choice1Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
                choice2Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
                choice3Text.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDiffX, sizeDiffY);
                break;
        }
        
        ResetLocalScale(choiceBackground);
        SpriteRenderer sr1 = choiceBackground.GetComponent<SpriteRenderer>();
        float currWidth1 = sr1.bounds.extents.x * 2;
        float currHeight1 = sr1.bounds.extents.y * 2;
        
        // Determine the dimensions of the choice background
        choiceWidth = gameWindowSizeX / screenSizeX;
        choiceHeight = ((screenSizeY - gameWindowSizeY) * percentOfArea) / screenSizeY;
                
        choiceBackground.transform.localScale = new Vector3(choiceWidth * cameraWidth / currWidth1, choiceHeight * cameraHeight / currHeight1, 1);
                
        // Determine the location of the choice background
        xLocation = -cameraWidth/2 + ((choiceWidth * cameraWidth)/2);
        yLocation = -cameraHeight/2 + ((choiceHeight * cameraHeight)/2);
        
        choiceBackground.transform.position = new Vector3(xLocation, yLocation, 0);
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


    public void ResetLocalScale(GameObject toTransform)
    {
        Vector3 reset = new Vector3(1,1,1);
        toTransform.transform.localScale = reset;
    }
}
