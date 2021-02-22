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

        if (Input.GetKeyDown(KeyCode.V))
        {
            refreshUI();
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
}