using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System.Linq;

public class FinalDecisionScript : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;

    public Text textPrefab;
    public Button buttonPrefab;
    public Text goMarkerToContinue;

    public GameObject TextContainer;

    bool skipScene = false;
    bool max = false;

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

        else
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                HighlightChoices.S.HighlightChoice(1,3);
                
                SFXManager.S.PlaySFX(43);
                
                eraseUI();
                GameManagerScript.NextScene(skipScene);
                Epilogue.storyTicker = 1;

                GameManagerScript.gameWinner = 1;
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                HighlightChoices.S.HighlightChoice(2,3);
                
                SFXManager.S.PlaySFX(44);
                
                eraseUI();
                GameManagerScript.NextScene(skipScene);
                Epilogue.storyTicker = 5;
                
                GameManagerScript.gameWinner = 2;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                HighlightChoices.S.HighlightChoice(3,3);
                
                SFXManager.S.PlaySFX(45);
                
                eraseUI();
                GameManagerScript.NextScene(skipScene);
                Epilogue.storyTicker = 9;
                
                GameManagerScript.gameWinner = 3;
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

