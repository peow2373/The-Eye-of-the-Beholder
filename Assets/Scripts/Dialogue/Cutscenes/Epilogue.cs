using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System.Linq;

public class Epilogue : MonoBehaviour
{

    public Text textPrefab;
    public Text goMarkerToContinue;

    public GameObject TextContainer;
    public static int storyTicker;

    // Start is called before the first frame update
    void Start()
    {
        MarkerManagerScript.S.Reset();
        
        refreshUI();
    }

    void refreshUI()
    {

        eraseUI();

        Text storyText = Instantiate(textPrefab, new Vector3(0, 0, 0), Quaternion.identity) as Text;

        string text = "";

        switch (storyTicker)
        {

            case 1:

                text = "Netrixi takes the Beholderite back to her lab where she runs experiments on it and eventually figures out how it can make her the most powerful witch in the Iron Region";
                storyText.text = text;
                storyText.transform.SetParent(TextContainer.transform, false);

                break;

            case 2:

                text = "Iv goes back to find her brother, and although his trauma is great, he is relearning the way of the Monks";
                storyText.text = text;
                storyText.transform.SetParent(TextContainer.transform, false);

                break;

            case 3:

                text = "Folkvar is crowned King and begins an anti-beholderite movement of people dedicated to cleansing the Region of its stain of Beholderite";
                storyText.text = text;
                storyText.transform.SetParent(TextContainer.transform, false);

                break;

            case 4:

                text = "Folkvar takes the stone and uses it to bring order back to the Iron Region. His father's delusion fades, but the King still agrees to hand the crown over to his son. Folkvar eradicates all other Beholderite.";
                storyText.text = text;
                storyText.transform.SetParent(TextContainer.transform, false);

                break;

            case 5:

                text = "Iv returns her brother back to the monk community in the mountains, where he undergoes mental and physical rehabilitation with the support of his loved ones. Even still, something nags at his heart. He still feels the Beholderite stone in the sceptor.";
                storyText.text = text;
                storyText.transform.SetParent(TextContainer.transform, false);

                break;

            case 6:

                text = "Netrixi returns home. She has seen the evil that Beholderite can draw people into - including herself. She needs to get away. She destroys her lab equipment, and leaves her home to wander.";
                storyText.text = text;
                storyText.transform.SetParent(TextContainer.transform, false);


                break;

            case 7:

                text = "Netrixi gives Beholderite to Iv, asking her to keep it safe with the enlightened monks. Netrixi goes back to her house and stares at the ceiling";
                storyText.text = text;
                storyText.transform.SetParent(TextContainer.transform, false);

                break;

            case 8:

                text = "Iv takes the stone and keeps it in her mentally healthy community. Her brother is no longer entirely pure- he feels temptation";
                storyText.text = text;
                storyText.transform.SetParent(TextContainer.transform, false);

                break;

            case 9:

                text = "Folkvar and his father return to the kingdom and his father remembers the his love for his Region. They begin to rebuild together";
                storyText.text = text;
                storyText.transform.SetParent(TextContainer.transform, false);

                break;

            case 10:


                text = "THE END \n \n Thank you for playing!";
                storyText.text = text;
                storyText.transform.SetParent(TextContainer.transform, false);

                // Resize font 
                storyText.fontSize += 10;

                break;
            
            case 11:
                
                GameManagerScript.NextScene(false);
                break;
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

    // Update is called once per frame
    void Update()
    {
        if (MarkerManagerScript.goMarker)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                storyTicker += 1;

                if (storyTicker == 3 || storyTicker == 7 || storyTicker == 9)
                {
                    storyTicker = 10;
                }
                
                refreshUI();
            }
        }
    }
}
