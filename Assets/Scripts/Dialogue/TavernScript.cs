using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System.Linq;

public class TavernScript : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;

    public Text textPrefab;
    public Button buttonPrefab;

    private Vector3 button1 = new Vector3(100, 315f, -5f);
    private Vector3 button2 = new Vector3(400, 315f, -5f);
    private Vector3 button3 = new Vector3(700, 315f, -5f);

    public GameObject TextContainer;

    bool fight = false;
    bool sceneChange = false;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSON.text);

        refreshUI();

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

        if (this.transform.childCount > 2)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                story.ChooseChoiceIndex(0);
                refreshUI();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                story.ChooseChoiceIndex(1);
                refreshUI();
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                story.ChooseChoiceIndex(2);
                refreshUI();
            }
        }

        else if (this.transform.childCount == 2)
        {
            
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

        else
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                if ((string)story.currentChoices[0].text == "Fight")
                {
                    fight = true;
                    sceneChange = true;
                    eraseUI();
                }

                else 
                {
                    sceneChange = true;
                    eraseUI();
                }
            }
        }
    }

    string loadStoryChunk()
    {
        string text = "";

        if (story.canContinue) //if there is more content
        {
            text = story.ContinueMaximally(); //continue until next options or no content. will return string
        }

        return text;
    }
}