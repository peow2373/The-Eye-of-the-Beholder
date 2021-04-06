using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public AudioSource inkIntro;
    public AudioSource pirateDialogue;
    public AudioSource pirateCombat;
    public AudioSource beneathCastleGS;
    public AudioSource beneathCastleExciting;
    public AudioSource avengeGS;
    public AudioSource avengeExciting;
    public AudioSource disciplesExciting;
    public AudioSource disciplesCalm;
    public AudioSource chant;
    public AudioSource Moke;
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

        //Intro
        if (scene == 0)
        {
            inkIntro.Play();
        }

        //Pirate Dialogue
        if (scene == 1)
        {
            inkIntro.Stop();
            pirateDialogue.Play();
        }

        //Pirate Combat
        if (scene == 2)
        {
            pirateDialogue.Stop();
            pirateCombat.Play();
        }

        //Mansion Dialogue
        if (scene == 3)
        {
            pirateCombat.Stop();
            celestial.Play();
        }

        //Mansion Combat
        if (scene == 4)
        {

        }

        //Omitted
        if (scene == 5)
        {

        }

        //Road Dialogue
        if (scene == 6)
        {

        }

        //Road Combat
        if (scene == 7)
        {

        }

        //Tavern Dialogue
        if (scene == 8)
        {

        }

        //Tavern Dialogue
        if (scene == 9)
        {

        }

        //Tavern Combat
        if (scene == 10)
        {

        }

        //Dialogue GK
        if (scene == 11)
        {
            celestial.Stop();
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
            celestial.Play();
        }

        //Tunnel Entrance Dialogue
        if (scene == 14)
        {
            celestial.Stop();
            avengeGS.Play();
        }

        //Tunnel Entrance Combat
        if (scene == 15)
        {
            avengeGS.Stop();
            celestial.Play();
        }

        //Kaz Dialogue
        if (scene == 16)
        {
            celestial.Stop();
            avengeGS.Play();
        }

        //Kaz Combat
        if (scene == 17)
        {
            avengeGS.Stop();
            disciplesExciting.Play();
        }

        //Volcano Entrance
        if (scene == 18)
        {
            disciplesExciting.Stop();
            celestial.Play();
        }

        //omitted
        if (scene == 19)
        {

        }

        //omitted
        if (scene == 20)
        {

        }

        //omitted
        if (scene == 21)
        {

        }

        //Cavern Dialogue
        if (scene == 22)
        {
            celestial.Stop();
            chant.Play();
        }

        //Cavern Combat
        if (scene == 23)
        {

        }

        //Mine Dialogue
        if (scene == 24)
        {
            chant.Stop();
            celestial.Play();
        }

        //Mine Combat
        if (scene == 25)
        {
            celestial.Stop();
            disciplesExciting.Play();
        }

        //Moke Dialogue
        if (scene == 26)
        {
            disciplesExciting.Stop();
            Moke.Play();
        }

        //Moke Combat
        if (scene == 27)
        {
            Moke.Stop();
            avengeExciting.Play();
        }

        //Final Decision
        if (scene == 28)
        {
            avengeExciting.Stop();
            celestial.Play();
        }

        //Epilogue
        if (scene == 29)
        {

        }
    }
}

