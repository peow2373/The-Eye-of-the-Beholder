using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DangerScript : MonoBehaviour
{
    public bool netrixi, folkvar, iv;
    
    private float timeOff = 6f;
    private float timeVisible = 0.5f;
    private float timeInvisible = 0.5f;

    private SpriteRenderer sr;

    private int startScene;

    private bool startedAnimation;

    private float dangerHeight;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();

        startScene = GameManagerScript.currentScene;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Reposition danger alert
        DetermineDangerHeight();
        transform.position = new Vector3(startPosition.x, startPosition.y + dangerHeight, 0);
        
        if (!startedAnimation)
        {
            StartCoroutine(DisplayDanger(true));
            startedAnimation = true;
        }
        
        // If the combat simulation plays
        if (CombatManagerScript.hasRunSimulation)
        {
            StopAllCoroutines();
            sr.enabled = false;
        }
        
        // If a new scene is loaded
        if (startScene != GameManagerScript.currentScene) Destroy(this.gameObject);
    }

    IEnumerator DisplayDanger(bool skipWait)
    {
        sr.enabled = false;

        if (!skipWait) yield return new WaitForSeconds(timeOff);

        sr.enabled = true;
        yield return new WaitForSeconds(timeVisible);
        
        sr.enabled = false;
        yield return new WaitForSeconds(timeInvisible);
        
        sr.enabled = true;
        yield return new WaitForSeconds(timeVisible);
        
        sr.enabled = false;
        yield return new WaitForSeconds(timeInvisible);
        
        sr.enabled = true;
        yield return new WaitForSeconds(timeVisible);
        
        sr.enabled = false;
        StartCoroutine(DisplayDanger(false));
    }

    void DetermineDangerHeight()
    {
        if (netrixi)
        {
            if (CombatManagerScript.netrixiAttacks) dangerHeight = TargetManager.dangerHeight;
            else dangerHeight = 22;
        }
        
        if (folkvar)
        {
            if (CombatManagerScript.folkvarAttacks) dangerHeight = TargetManager.dangerHeight;
            else dangerHeight = 24;
        }
        
        if (iv)
        {
            if (CombatManagerScript.ivAttacks) dangerHeight = TargetManager.dangerHeight;
            else dangerHeight = 21;
        }
    }
}
