using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System.Linq;

public class GameOverScript : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;

    public Text textPrefab;
    public Button buttonPrefab;
    public Text goMarkerToContinue;

    public GameObject TextContainer;
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

        if (max == false)
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
        if (this.transform.childCount == 2)
        {
            goMarkerToContinue.enabled = true;

            if (MarkerManagerScript.goMarker)
            {
                if (Input.GetKeyDown(KeyCode.V))
                {
                    HighlightChoices.S.HighlightChoice(1, 2);

                    SFXManager.S.PlaySFX(40);
                    
                    GameManagerScript.currentScene = GameManagerScript.previousScene;
                    GameManagerScript.previousScene = 31;
                    GameManagerScript.S.Reset();
                    
                    refreshUI();
                }
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                HighlightChoices.S.HighlightChoice(2, 2);
                
                SFXManager.S.PlaySFX(24);
                
                GameManagerScript.currentScene = 0;
                GameManagerScript.previousScene = 31;
                GameManagerScript.S.Reset();
                
                refreshUI();
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

        return text;
    }
}

