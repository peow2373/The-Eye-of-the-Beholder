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
    public AudioSource mine;
    public AudioSource mansion;
    public AudioSource road;
    public AudioSource tunnelExit;
        
    public static int scene;

    public static AudioManagerScript S;
    
    private AudioSource[] allAudioSources;

    // Start is called before the first frame update
    void Awake()
    {
        S = this;
        DontDestroyOnLoad(this.gameObject);

        allAudioSources = FindObjectsOfType<AudioSource>();
    }

    void StopAllAudio() {
        for (int i = 0; i < allAudioSources.Length; i++) 
        {
            allAudioSources[i].Stop();
        }
    }

    public void PlayAudio(int currentScene)
    {
        scene = currentScene;

        //Intro
        if (scene == 0)
        {
            StopAllAudio();
            inkIntro.Play();
        }

        //Pirate Dialogue
        if (scene == 1)
        {
            StopAllAudio();
            pirateDialogue.Play();
        }

        //Pirate Combat
        if (scene == 2)
        {
            StopAllAudio();
            pirateCombat.Play();
        }

        //Mansion Dialogue
        if (scene == 3)
        {
            StopAllAudio();
            mansion.Play();
        }

        //Mansion Combat
        if (scene == 4)
        {

        }

        //Road Dialogue
        if (scene == 6)
        {
            StopAllAudio();
            road.Play();
        }

        //Road Combat
        if (scene == 7)
        {
            StopAllAudio();
            disciplesExciting.Play();
        }

        //Tavern Dialogue
        if (scene == 8)
        {
            StopAllAudio();
            celestial.Play();
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
            StopAllAudio();
            beneathCastleExciting.Play();
        }

        //Combat GK
        if (scene == 12)
        {

        }

        //Throne Room
        if (scene == 13)
        {
            StopAllAudio();
            celestial.Play();
        }

        //Tunnel Entrance Dialogue
        if (scene == 14)
        {
            StopAllAudio();
            avengeGS.Play();
        }

        //Tunnel Entrance Combat
        if (scene == 15)
        {
            StopAllAudio();
            celestial.Play();
        }

        //Kaz Dialogue
        if (scene == 16)
        {
            StopAllAudio();
            avengeGS.Play();
        }

        //Kaz Combat
        if (scene == 17)
        {
            StopAllAudio();
            disciplesExciting.Play();
        }

        //Volcano Entrance
        if (scene == 18)
        {
            StopAllAudio();
            tunnelExit.Play();
        }

        //Cavern Dialogue
        if (scene == 22)
        {
            StopAllAudio();
            chant.Play();
        }

        //Cavern Combat
        if (scene == 23)
        {

        }

        //Mine Dialogue
        if (scene == 24)
        {
            StopAllAudio();
            mine.Play();
        }

        //Mine Combat
        if (scene == 25)
        {
            StopAllAudio();
            disciplesExciting.Play();
        }

        //Moke Dialogue
        if (scene == 26)
        {
            StopAllAudio();
            Moke.Play();
        }

        //Moke Combat
        if (scene == 27)
        {
            StopAllAudio();
            avengeExciting.Play();
        }

        //Final Decision
        if (scene == 28)
        {
            StopAllAudio();
            celestial.Play();
        }

        //Epilogue
        if (scene == 29)
        {

        }
    }
}

