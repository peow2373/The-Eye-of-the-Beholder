using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System.Linq;

public class VolcanoEntranceScript: MonoBehaviour
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

        if (this.transform.childCount == 0)
        {
            goMarkerToContinue.enabled = true;

            if (Input.GetKeyDown(KeyCode.V))
            {
                refreshUI();
            }
        }

        else if (this.transform.childCount == 1)
        { 
            goMarkerToContinue.enabled = false;
            if (Input.GetKeyDown(KeyCode.V))
            {
                refreshUI();
                skipScene = false;
                GameManagerScript.NextScene(skipScene);
            }
        }


        else if (this.transform.childCount == 2)
        {
            goMarkerToContinue.enabled = false;

            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Z))
            {
                story.ChooseChoiceIndex(0);
                refreshUI();
            }

            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.C))
            {
                story.ChooseChoiceIndex(1);
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
