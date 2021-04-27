using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System.Linq;

public class IntroScript : MonoBehaviour
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

    public static int storyProgression;
    
    public static bool[] locations = new bool[9];

    public static bool wasSmall, wasMedium, wasLarge;

    private bool waiting;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSON.text);

        refreshUI();

        MarkerManagerScript.S.Reset();
    }

    void refreshUI()
    {
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

            choiceButton.onClick.AddListener(delegate
            {
                chooseStoryChoice(choice);
            });

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
        switch (storyProgression)
        {
            // Show Palm Marker
            case 0:
                if (MarkerManagerScript.palmMarker)
                {
                    HighlightChoices.S.HighlightChoice(1,1);
                
                    if (!GameManagerScript.curtainsOpening) SFXManager.S.PlaySFX(42);
                    refreshUI();

                    storyProgression++;
                }
                break;
            
            // Flip Hand
            case 1:
                if (MarkerManagerScript.goMarker)
                {
                    if (Input.GetKeyDown(KeyCode.V))
                    {
                        if (!TutorialHandScript.logoLoading)
                        {
                            if (!waiting) StartCoroutine(WaitBeforeMovingOn());
                        }
                    }
                }
                break;
            
            // Location
            case 2:
                if (locations[0] && locations[1] && locations[2] && locations[3] && locations[4] && locations[5] && locations[6] && locations[7] && locations[8])
                {
                    if (!waiting) StartCoroutine(WaitBeforeMovingOn());
                }
                break;
            
            // Depth
            case 3:
                if (wasSmall && wasMedium && wasLarge) 
                {
                    if (!waiting) StartCoroutine(WaitBeforeMovingOn());
                }
                break;
            
            // Start Rotation
            case 4:
                if (Input.GetKeyDown(KeyCode.K)) 
                {
                    HighlightChoices.S.HighlightChoice(1,1);
                    
                    refreshUI();
                        
                    storyProgression++;
                }
                break;
            
            // Continue Rotation
            case 5:
                if (Input.GetKeyDown(KeyCode.T))
                {
                    storyProgression++;
                    
                    refreshUI();
                }
                break;

            // Start Story
            case 6:
                if (TutorialHandScript.storyStarted)
                {
                    storyProgression++;
                    
                    refreshUI();
                }
                break;
            
            // End Story
            case 7:
                if (TutorialHandScript.doneWithStory)
                {
                    storyProgression++;
                    
                    refreshUI();
                }
                break;
            
            // New Scene
            case 8:
                if (MarkerManagerScript.goMarker)
                {
                    if (Input.GetKeyDown(KeyCode.V)) 
                    {
                        HighlightChoices.S.HighlightChoice(1,1);
                
                        SFXManager.S.PlaySFX(41);
                        refreshUI();
                    }
                }
                break;
        }

        
        
        // Exit the scene at any time
        if (MarkerManagerScript.undoMarker)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                GameManagerScript.NextScene(false);
                
                HighlightChoices.S.HighlightChoice(2,2);
                
                SFXManager.S.PlaySFX(41);
                
                refreshUI();
            }
        }

        if (this.transform.childCount == 1)
        {
            goMarkerToContinue.enabled = false;
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
            GameManagerScript.NextScene(skipScene);
        }

        return text;
    }

    IEnumerator WaitBeforeMovingOn()
    {
        waiting = true;
        SFXManager.S.PlaySFX(42);
        HighlightChoices.S.HighlightChoice(1,1);

        yield return new WaitForSeconds(0.5f);

        refreshUI();
                        
        storyProgression++;

        waiting = false;
    }
}