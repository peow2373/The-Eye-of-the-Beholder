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
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene("CombatDraft1");
        }
        
        // change scene to combat and enemy testing
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene("CombatDraft2");
        }
        
        // change scene to combat set-up testing
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene("CombatDraft3");
        }
        
        // change scene to the scene outside the castle
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("InkCastle");
        }
    }
}
