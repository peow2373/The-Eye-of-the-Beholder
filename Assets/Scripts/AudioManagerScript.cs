using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public AudioSource beneathCastleGS;
    public AudioSource beneathCastleExciting;
    public AudioSource avengeGS;
    public AudioSource avengeExciting;
    public AudioSource disciplesExciting;
    public AudioSource disciplesCalm;
    public AudioSource chant;
    public AudioSource celestial;
    public static int scene;

    //Play the music
    bool m_Play;
    //Detect when you use the toggle, ensures music isn’t played multiple times
    bool m_ToggleChange;

    public static AudioManagerScript S;

    // Start is called before the first frame update
    void Awake()
    {
        S = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio(int currentScene)
    {
        scene = currentScene;

        if (scene == 0)
        {
            Debug.Log(scene);
        }

        if (scene == 1)
        {
            Debug.Log(scene);            
        }

        if (scene == 2)
        {
            Debug.Log(scene);
        }

        if (scene == 3)
        {
            Debug.Log(scene);            
        }

        if (scene == 4)
        {

        }

        if (scene == 5)
        {

        }

        if (scene == 6)
        {

        }

        if (scene == 7)
        {

        }

        if (scene == 8)
        {

        }

        if (scene == 9)
        {

        }

        if (scene == 10)
        {

        }

        //Dialogue GK
        if (scene == 11)
        {
            beneathCastleExciting.Play();
        }

        //Combat GK
        if (scene == 12)
        {

        }

        //Throne Room
        if (scene == 13)
        {
            beneathCastleExciting.Stop();
        }

        if (scene == 14)
        {
            avengeGS.Play();
        }

        if (scene == 15)
        {
            avengeGS.Stop();
        }

        if (scene == 16)
        {
            avengeGS.Play();
        }

        if (scene == 17)
        {
            avengeGS.Stop();
            disciplesExciting.Play();
        }

        if (scene == 18)
        {
            disciplesExciting.Stop();
        }

        if (scene == 19)
        {

        }

        if (scene == 20)
        {

        }

        if (scene == 21)
        {

        }

        if (scene == 22)
        {
            chant.Play();
        }

        if (scene == 23)
        {

        }

        if (scene == 24)
        {
            chant.Stop();
        }

        if (scene == 25)
        {
            disciplesExciting.Play();
        }

        if (scene == 26)
        {
            disciplesExciting.Stop();
        }

        if (scene == 27)
        {
            avengeExciting.Play();
        }

        //Final Decision
        if (scene == 28)
        {
            avengeExciting.Stop();
            celestial.Play();
        }

        if (scene == 29)
        {
            celestial.Stop();
        }
    }
}

