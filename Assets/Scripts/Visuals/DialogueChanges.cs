using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChanges : MonoBehaviour
{
    public bool mansionScene;
    public Sprite knightMelee, knightRanged;

    public bool throneScene;

    // Start is called before the first frame update
    void Start()
    {
        if (mansionScene)
        {
            int randomIndex = UnityEngine.Random.Range(0,2);
            GameManagerScript.chooseRandomKnight = randomIndex;

            if (randomIndex == 0) this.GetComponent<SpriteRenderer>().sprite = knightMelee;
            else this.GetComponent<SpriteRenderer>().sprite = knightRanged;
        }

        if (throneScene)
        {
            Animator animator = this.GetComponent<Animator>();
            animator.SetFloat("Speed", ChangeCharacterAnimations.idleAnimationSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
