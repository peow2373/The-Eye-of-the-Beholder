using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // change scene to dialogue testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("StoryDraft3");
        }
        
        // change scene to combat testing
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            SceneManager.LoadScene("CombatDraft1");
        }
        
        // change scene to combat and enemy testing
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene("CombatDraft2");
        }
    }
}
