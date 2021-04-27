using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpilogueScript : MonoBehaviour
{
    private Animator animator;

    public Sprite netrixi, folkvar, iv;

    public GameObject stick;
    
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        this.gameObject.transform.localScale = new Vector3(3.5f,3.5f,1);

        animator = this.gameObject.GetComponent<Animator>();
        animator.SetFloat("Speed", ChangeCharacterAnimations.idleAnimationSpeed);
        animator.enabled = false;

        StartCoroutine(ScaleCharacter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ScaleCharacter()
    {
        float pause = 0.35f;
        float moveDelay = 0.025f;
        float waitBeforeStart = 2f;
        float startingHeight = 11f;
        
        stick.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        this.transform.position = new Vector3(0, startingHeight, 0);
        
        // Change sprite
        switch (GameManagerScript.gameWinner)
        {
            case 1: this.GetComponent<SpriteRenderer>().sprite = netrixi;
                break;
                
            case 2: this.GetComponent<SpriteRenderer>().sprite = folkvar;
                break;
            
            case 3: this.GetComponent<SpriteRenderer>().sprite = iv;
                break;
        }

        yield return new WaitForSeconds(waitBeforeStart);

        float iterations = 190f;

        // Increase size of character
        for (int i = 0; i <= iterations; i++)
        {
            float move = (float) (startingHeight - 1) / iterations;
            
            this.gameObject.transform.position = new Vector3(transform.position.x, startingHeight - (move * i), 1);
            
            yield return new WaitForSeconds(moveDelay);

            if (i == iterations)
            {
                yield return new WaitForSeconds(pause);

                animator.enabled = true;
                animator.Play("End");
                animator.SetInteger("Winner", GameManagerScript.gameWinner);
            }
        }
    }
}
