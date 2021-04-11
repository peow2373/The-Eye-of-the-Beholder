using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System.Linq;

public class TavernAfterFightScript2 : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;

    public Text textPrefab;
    public Button buttonPrefab;
    public Text goMarkerToContinue;

    public GameObject TextContainer;

    bool skipScene = true;
    bool max = false;

    private bool didMarkerDisappear;
    private bool handInMiddleLeft, handInMiddleRight;

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

        string text = "";

        if (max == true)
        {
            text = loadStoryChunkMax();
        }
        else
        {
            text = loadStoryChunk();
        }

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

            choiceButton.onClick.AddListener(delegate
            {
                chooseStoryChoice(choice);
            });

        }
        
        // Fixes an error with an empty dialogue box loading in at the end of the story
        string currentText = text;
        if (currentText == "")
        {
            GameManagerScript.NextScene(skipScene);
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
    void Update() //skip scene = true unless you fight the brute
    {

        if (this.transform.childCount == 0)
        {
            goMarkerToContinue.enabled = true;

            if (MarkerManagerScript.goMarker)
            {
                if (Input.GetKeyDown(KeyCode.V))
                {
                    HighlightChoices.S.HighlightChoice(1,1);
                    
                    SFXManager.S.PlaySFX(40);
                    
                    refreshUI();
                }
            }
        }

        else if (this.transform.childCount == 1)
        {
            goMarkerToContinue.enabled = false;

            if (MarkerManagerScript.goMarker)
            {
                if (Input.GetKeyDown(KeyCode.V))
                {
                    HighlightChoices.S.HighlightChoice(1,1);
                    
                    SFXManager.S.PlaySFX(40);
                    
                    story.ChooseChoiceIndex(0);
                    refreshUI();
                }
            }
        }


        else if (this.transform.childCount == 2)
        {
            goMarkerToContinue.enabled = false;

            if ((string)story.currentChoices[0].text == "Netrixi")
            {
                ChangeChoiceText.talkingToIv = true;
                
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    HighlightChoices.S.HighlightChoice(1,2);
                    
                    SFXManager.S.PlaySFX(43);
                    
                    story.ChooseChoiceIndex(0);
                    refreshUI();
                }

                if (Input.GetKeyDown(KeyCode.O))
                {
                    HighlightChoices.S.HighlightChoice(2,2);
                    
                    SFXManager.S.PlaySFX(44);
                    
                    story.ChooseChoiceIndex(1);
                    refreshUI();
                }
            }

            else
            {
                int currLoc = MarkerManagerScript.currentLocation;
                if (currLoc == 1 || currLoc == 4 || currLoc == 7 || currLoc == 2 || currLoc == 5 || currLoc == 8) handInMiddleLeft = true;
                if (currLoc == 3 || currLoc == 6 || currLoc == 9 || currLoc == 2 || currLoc == 5 || currLoc == 8) handInMiddleRight = true;

                if (handInMiddleRight)
                {
                    if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Z))
                    {
                        HighlightChoices.S.HighlightChoice(1,2);
                        
                        if (MarkerManagerScript.pastLocation != MarkerManagerScript.currentLocation)
                        {
                            SFXManager.S.PlaySFX(42);
                            
                            story.ChooseChoiceIndex(0);
                            refreshUI();
                            handInMiddleRight = false;
                        }
                        else
                        {
                            if (MarkerManagerScript.palmMarker && didMarkerDisappear)
                            {
                                SFXManager.S.PlaySFX(42);
                                
                                story.ChooseChoiceIndex(0);
                                refreshUI();
                                handInMiddleRight = false;
                            }
                        
                            if (!MarkerManagerScript.palmMarker) didMarkerDisappear = true;
                        }
                    }
                }
                
                if (handInMiddleLeft)
                {
                    if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.C))
                    {
                        HighlightChoices.S.HighlightChoice(2,2);
                        
                        if (MarkerManagerScript.pastLocation != MarkerManagerScript.currentLocation)
                        {
                            SFXManager.S.PlaySFX(42);
                            
                            story.ChooseChoiceIndex(1);
                            refreshUI();
                            handInMiddleLeft = false;
                        }
                        else
                        {
                            if (MarkerManagerScript.palmMarker && didMarkerDisappear)
                            {
                                SFXManager.S.PlaySFX(42);
                                
                                story.ChooseChoiceIndex(1);
                                refreshUI();
                                handInMiddleLeft = false;
                            }
                        
                            if (!MarkerManagerScript.palmMarker) didMarkerDisappear = true;
                        }
                    }
                }
            }
        }
    }

    string loadStoryChunkMax()
    {
        string text = "";

        if (story.canContinue) //if there is more content
        {
            text = story.ContinueMaximally(); //continue until next options or no content. will return string
        }

        max = true;
        return text;
    }

    string loadStoryChunk()
    {
        string text = "";

        if (story.canContinue) //if there is more content
        {
            text = story.Continue(); //continue until next options or no content. will return string
        }

        else
        {
            GameManagerScript.NextScene(skipScene);
        }

        return text;
    }
}