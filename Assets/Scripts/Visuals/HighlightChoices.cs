using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightChoices : MonoBehaviour
{
    public Color highlightColor;

    public GameObject highlightedChoice, highlightedOption;

    public GameObject choice1, choice2;
    public GameObject option1, option2, option3, option4;

    public static HighlightChoices S;

    private float timeOutDelay = 0.15f;

    private GameObject choice, option;
    
    private SpriteRenderer choiceSR, optionSR;

    private Vector3 choicePosition, optionPosition;
    private Vector3 offScreen = new Vector3(GameWindowManager.offScreen, GameWindowManager.offScreen, 0);
    private bool choiceOffScreen = true, optionOffScreen = true;

    private Vector3 oldOptionSize;

    // Start is called before the first frame update
    void Start()
    {
        S = this;
        
        choiceSR = highlightedChoice.GetComponent<SpriteRenderer>();
        choice = choice1;
        choicePosition = offScreen;
        
        optionSR = highlightedOption.GetComponent<SpriteRenderer>();
        option = option1;
        optionPosition = offScreen;

        oldOptionSize = option.transform.localScale;
    }
    
    // Update is called once per frame
    void Update()
    {
        // Display highlighted choice
        if (choiceOffScreen) highlightedChoice.transform.position = offScreen;

        // Display highlighted option
        if (optionOffScreen) highlightedOption.transform.position = offScreen;
    }

    public void TransformHighlights(bool displayChoices)
    {
        // Reset previous transformations
        highlightedChoice.transform.localScale = new Vector3(1, 1, 1);
        highlightedOption.transform.localScale = new Vector3(1, 1, 1);
        
        // If the player is about to enter combat
        GameObject[] choices = GameObject.FindGameObjectsWithTag("Dialogue Choice");
        if (choices.Length > 0 && choices[0].GetComponentInChildren<Text>().text == "Fight") displayChoices = false;

        if (displayChoices)
        {
            // Transform Choice highlight
            float choiceWidth = choiceSR.bounds.extents.x;
            float choiceHeight = choiceSR.bounds.extents.y;
        
            float choiceDiffX = choice.GetComponent<SpriteRenderer>().bounds.extents.x / choiceWidth;
            float choiceDiffY = choice.GetComponent<SpriteRenderer>().bounds.extents.y / choiceHeight;

            highlightedChoice.transform.position = choice.transform.position;
            highlightedChoice.transform.localScale = new Vector3(choiceDiffX, choiceDiffY, 1);
            choiceSR.color = highlightColor;
        }

        // Transform Option highlight
        float optionWidth = optionSR.bounds.extents.x;
        float optionHeight = optionSR.bounds.extents.y;
        
        float optionDiffX = option.GetComponent<SpriteRenderer>().bounds.extents.x / optionWidth;
        float optionDiffY = option.GetComponent<SpriteRenderer>().bounds.extents.y / optionHeight;
            
        highlightedOption.transform.position = option.transform.position;
        highlightedOption.transform.localScale = new Vector3(optionDiffX, optionDiffY, 1);
        optionSR.color = highlightColor;
    }


    public void HighlightChoice(int chosenChoice, int numberOfChoices)
    {
        switch (chosenChoice)
        {
            case 0:
            case 1:
                if (numberOfChoices == 3)
                {
                    StartCoroutine(HighlightTimeOut(choice1, option1, false));
                }
                else
                {
                    StartCoroutine(HighlightTimeOut(choice1, option1, true));
                }
                break;

            case 2:
                if (numberOfChoices == 2)
                {
                    StartCoroutine(HighlightTimeOut(choice2, option2, true));
                }
                else
                {
                    StartCoroutine(HighlightTimeOut(choice1, option2, false));
                }

                break;

            case 3:
                StartCoroutine(HighlightTimeOut(choice1, option3, false));
                break;

            case 4:
                StartCoroutine(HighlightTimeOut(choice1, option4, false));
                break;
        }
    }
    

    private IEnumerator HighlightTimeOut(GameObject chosenChoice, GameObject chosenOption, bool displayChoices)
    {
        choice = chosenChoice;
        option = chosenOption;
        
        TransformHighlights(displayChoices);

        choiceOffScreen = false;
        optionOffScreen = false;
            
        yield return new WaitForSeconds(timeOutDelay);

        choiceOffScreen = true;
        optionOffScreen = true;
    }
}
