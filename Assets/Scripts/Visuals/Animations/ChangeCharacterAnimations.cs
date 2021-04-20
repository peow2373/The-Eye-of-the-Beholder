using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacterAnimations : MonoBehaviour
{
    private Animator animator;
    private int previousPlayer;
    private string previousEnemy;

    public static ChangeCharacterAnimations S1, S2, S3, S4, S5, S6;

    public bool netrixi, folkvar, iv, enemy1, enemy2, enemy3;

    public static float idleAnimationSpeed = 0.2f;

    private string previousState;
    
    // Start is called before the first frame update
    void Start()
    {
        if (netrixi) S1 = this;
        if (folkvar) S2 = this;
        if (iv) S3 = this;
        if (enemy1) S4 = this;
        if (enemy2) S5 = this;
        if (enemy3) S6 = this;

        animator = this.GetComponent<Animator>();
    }
    

    public string DetermineStateName()
    {
        animator.SetFloat("Speed", idleAnimationSpeed);
        return "Idle";
    }

    
    public void ChangeCharacter(int animationIndex, string characterName)
    {
        if (characterName == "Netrixi" || characterName == "Folkvar" || characterName == "Iv")
        {
            if (previousPlayer != animationIndex || previousState != DetermineStateName()) animator.Play(DetermineStateName());
            ResetAnimations();
            
            animator.SetInteger("Index", animationIndex);

            previousPlayer = animationIndex;
            previousState = DetermineStateName();
        }
        else
        {
            if (previousEnemy != characterName || previousState != DetermineStateName()) animator.Play(DetermineStateName());
            ResetAnimations();

            if (enemy1)
            {
                if (characterName == "Pirate") animator.SetInteger(characterName, animationIndex);
                if (characterName == "Folkvar Enemy") animator.SetInteger(characterName, animationIndex);
                if (characterName == "Skull Melee") animator.SetInteger(characterName, animationIndex);
                if (characterName == "Brute") animator.SetInteger(characterName, animationIndex);
                if (characterName == "Knight Melee") animator.SetInteger(characterName, animationIndex);
                if (characterName == "Royal Guard") animator.SetInteger(characterName, animationIndex);
            }
            
            if (enemy2)
            {
                if (characterName == "Knight Melee") animator.SetInteger(characterName, animationIndex);
                if (characterName == "Knight Ranged") animator.SetInteger(characterName, animationIndex);
                if (characterName == "Skull Ranged") animator.SetInteger(characterName, animationIndex);
                if (characterName == "Barkeeper") animator.SetInteger(characterName, animationIndex);
                if (characterName == "Jester") animator.SetInteger(characterName, animationIndex);
                if (characterName == "Royal Guard") animator.SetInteger(characterName, animationIndex);
                if (characterName == "Bo") animator.SetInteger(characterName, animationIndex);
            }
            
            if (enemy3)
            {
                if (characterName == "Kaz") animator.SetInteger(characterName, animationIndex);
                if (characterName == "Gatekeeper") animator.SetInteger(characterName, animationIndex);
                if (characterName == "Royal King") animator.SetInteger(characterName, animationIndex);
                if (characterName == "Skull King") animator.SetInteger(characterName, animationIndex);
                if (characterName == "Evil King") animator.SetInteger(characterName, animationIndex);
            }
            
            if (characterName == "Frog") animator.SetInteger(characterName, animationIndex);
            if (characterName == "Cat") animator.SetInteger(characterName, animationIndex);
            if (characterName == "Dog") animator.SetInteger(characterName, animationIndex);

            previousEnemy = characterName;
            previousState = DetermineStateName();
        }
    }

    public void ResetAnimations()
    {
        List<string> enemyNames1 = new List<string>() 
            {"Pirate", "Folkvar Enemy", "Knight Melee", "Skull Melee", "Brute", "Royal Guard"};
        
        List<string> enemyNames2 = new List<string>() 
            {"Knight Melee", "Knight Ranged", "Skull Ranged", "Barkeeper", "Royal Guard", "Jester", "Bo"};
        
        List<string> enemyNames3 = new List<string>() 
            {"Kaz", "Gatekeeper", "Royal King", "Skull King", "Evil King"};

        List<string> animals = new List<string>()
            {"Frog", "Cat", "Dog"};
        
        if (enemy1 || enemy2 || enemy3) for (int i = 0; i < animals.Count; i++) animator.SetInteger(animals[i], -1);

        if (enemy1) for (int i = 0; i < enemyNames1.Count; i++) animator.SetInteger(enemyNames1[i], -1);
        if (enemy2) for (int i = 0; i < enemyNames2.Count; i++) animator.SetInteger(enemyNames2[i], -1);
        if (enemy3) for (int i = 0; i < enemyNames3.Count; i++) animator.SetInteger(enemyNames3[i], -1);
        
        if (netrixi) animator.SetInteger("Index", -1);
        if (folkvar) animator.SetInteger("Index", -1);
        if (iv) animator.SetInteger("Index", -1);
    }
}
