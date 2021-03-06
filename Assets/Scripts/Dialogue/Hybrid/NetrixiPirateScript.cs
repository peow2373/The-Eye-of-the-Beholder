using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System.Linq;

public class NetrixiPirateScript : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;

    public Text textPrefab;
    public Text goMarkerToContinue;
    public Button buttonPrefab;

    private Vector3 button1 = new Vector3(100, 315f, -5f);
    private Vector3 button2 = new Vector3(400, 315f, -5f);
    private Vector3 button3 = new Vector3(700, 315f, -5f);

    public GameObject TextContainer;

    bool skipScene = false;
    
    private bool didMarkerDisappear;
    private bool handInMiddleLeft, handInMiddleRight, handInMiddle;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSON.text);

        refreshUI();

        MarkerManagerScript.S.Reset();
    }

    void refreshUI()
    {
        MarkerManagerScript.pastLocation = MarkerManagerScript.currentLocation;

        eraseUI();

        Text storyText = Instantiate(textPrefab, new Vector3(0, 0, 0), Quaternion.identity) as Text;

        string text = loadStoryChunk();

        List<string> tags = story.currentTags;

        if (tags.Count > 0)
        {
            text = tags[0] + " - " + text;
        }

        storyText.text = text;
        storyText.transform.SetParent(TextContainer.transform, false);

        foreach (Choice choice in story.currentChoices)
        {
            Vector3 locate = new Vector3(0f, 0f, 0f);

            Button choiceButton = Instantiate(buttonPrefab, locate, Quaternion.identity) as Button;
            choiceButton.transform.SetParent(this.transform, false);

            Text choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = choice.text;
        }
    }

    void eraseUI()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }

        foreach (Transform child in TextContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void chooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        refreshUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.childCount <= 1)
        {
            if (this.transform.childCount == 0)
            {
                goMarkerToContinue.enabled = true;
            }
            if (MarkerManagerScript.goMarker)
            {
                if (Input.GetKeyDown(KeyCode.V))
                {
                    HighlightChoices.S.HighlightChoice(1,1);
                    
                    if (story.canContinue) SFXManager.S.PlaySFX(40);
                    refreshUI();
                }
            }
        }

        if (this.transform.childCount > 1)
        {
            goMarkerToContinue.enabled = false;

            int currLoc = MarkerManagerScript.currentLocation;
            if (currLoc == 1 || currLoc == 4 || currLoc == 7 || currLoc == 2 || currLoc == 5 || currLoc == 8) handInMiddleLeft = true;
            if (currLoc == 3 || currLoc == 6 || currLoc == 9 || currLoc == 2 || currLoc == 5 || currLoc == 8) handInMiddleRight = true;

            // If the player picked the left choice
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Z))
            {
                HighlightChoices.S.HighlightChoice(1,2);

                if (handInMiddleRight) handInMiddle = true;
            }
                    
            // If the player picked the right choice
            else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.C))
            {
                HighlightChoices.S.HighlightChoice(2,2);
                
                if (handInMiddleLeft) handInMiddle = true;
            }
            
            if (handInMiddle)
            {
                if (MarkerManagerScript.pastLocation != MarkerManagerScript.currentLocation)
                {
                    SFXManager.S.PlaySFX(42);
                    
                    story.ChooseChoiceIndex(1);
                    refreshUI();
                    
                    handInMiddle = false;
                    handInMiddleLeft = false;
                    handInMiddleRight = false;
                }
                else
                {
                    if (MarkerManagerScript.palmMarker && didMarkerDisappear)
                    {
                        SFXManager.S.PlaySFX(42);
                        
                        story.ChooseChoiceIndex(1);
                        refreshUI();
                        
                        handInMiddle = false;
                        handInMiddleLeft = false;
                        handInMiddleRight = false;
                    }
                        
                    if (!MarkerManagerScript.palmMarker) didMarkerDisappear = true;
                }
            }
        }
    }

    string loadStoryChunk()
    {
        string text = "";

        if (story.canContinue) //if there is more content
        {
            text = story.Continue(); //continue until next options or no content. will return string
            return text;
        }

        else
        {
            skipScene = false;
            SFXManager.S.PlaySFX(41);
            GameManagerScript.NextScene(skipScene);
        }

        return text;
    }
}