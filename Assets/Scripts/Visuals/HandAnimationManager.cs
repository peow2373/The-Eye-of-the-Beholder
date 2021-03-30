using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandAnimationManager : MonoBehaviour
{
    public static bool animation1Playing;
    public static bool animation2Playing;
    public static bool animation3Playing;

    public static bool startAnimation;

    public GameObject animation1, animation2, animation3;

    public Text optionText1, optionText2, optionText3;
    private string originalText1, originalText2, originalText3;

    public static bool restartAnimation;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if any animations are deactivated
        if (!animation1.activeSelf) animation1Playing = false;
        if (!animation2.activeSelf) animation2Playing = false;
        if (!animation3.activeSelf) animation3Playing = false;

        // Check to see if all hand animations have ended
        if (!animation1Playing && !animation2Playing && !animation3Playing)
        {
            startAnimation = true;

            //ChangeHandAnimation.handMovementOrder++;
            //if (ChangeHandAnimation.handMovementOrder == 4) ChangeHandAnimation.handMovementOrder = 1;
            ChangeHandAnimation.handMovementOrder = 2;
        }
        else
        {
            startAnimation = false;
        }

        CheckForChanges();
        
        // If the animations need to be restarted
        if (restartAnimation)
        {
            ChangeHandAnimation.restartAnimation = true;

            animation1Playing = false;
            animation2Playing = false;
            animation3Playing = false;

            restartAnimation = false;
        }
        else
        {
            ChangeHandAnimation.restartAnimation = false;
        }
    }

    void CheckForChanges()
    {
        // If Option 1 is different from before
        if (optionText1.GetComponent<Text>().text != "")
        {
            if (originalText1 != optionText1.GetComponent<Text>().text)
            {
                ChangeHandAnimation.animationName1 = "";
                restartAnimation = true;
            }
        }
        
        // If Option 2 is different from before
        if (optionText2.GetComponent<Text>().text != "")
        {
            if (originalText2 != optionText2.GetComponent<Text>().text)
            {
                ChangeHandAnimation.animationName2 = "";
                restartAnimation = true;
            }
        }
        
        // If Option 3 is different from before
        if (optionText3.GetComponent<Text>().text != "")
        {
            if (originalText3 != optionText3.GetComponent<Text>().text)
            {
                ChangeHandAnimation.animationName3 = "";
                restartAnimation = true;
            }
        }

        originalText1 = optionText1.GetComponent<Text>().text;
        originalText2 = optionText2.GetComponent<Text>().text;
        originalText3 = optionText3.GetComponent<Text>().text;
    }
}
