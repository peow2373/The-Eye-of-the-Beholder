using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject[] positions = new GameObject[10];

    public GameObject netrixiDanger;
    public GameObject folkvarDanger;
    public GameObject ivDanger;
    public GameObject target;

    private bool[] dangerShown = new bool[10];

    public static float dangerHeight = 3f;
    private float targetHeight = 25f;

    private bool[] newRound = new bool[10];

    private int checkScene;

    private bool target1Created, target2Created;
    private GameObject target1, target2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i <= 10; i++)
        {
            CheckForDanger(i);
            CheckForNetrixi(i);
            
            // Reset the danger signs if it's a new scene
            if (checkScene != GameManagerScript.currentScene) newRound[i-1] = true;
            checkScene = GameManagerScript.currentScene;
            
            // Reset the danger signs once the combat simulation has been run
            if (CombatManagerScript.hasRunSimulation) newRound[i-1] = true;
            else
            {
                if (newRound[i-1])
                {
                    dangerShown[i-1] = false;
                    newRound[i-1] = false;
                }
            }
        }
        
        // Check to see if Netrixi's lightning attack is locked in
        if (CombatManagerScript.firstAttack != 0)
        {
            if (target1Created)
            {
                if (CombatManagerScript.firstAttack == 2) StartCoroutine(SendDownwards(target1, 1));
                else
                {
                    Destroy(target1);
                    target1Created = false;
                }
                
            }
        }
        if (CombatManagerScript.secondAttack != 0)
        {
            if (target2Created)
            {
                if (CombatManagerScript.secondAttack == 2) StartCoroutine(SendDownwards(target2, 2));
                else
                {
                    Destroy(target2);
                    target2Created = false;
                }
            }
        }
    }

    void CheckForDanger(int positionNumber)
    {
        int i = positionNumber - 1;
        
        if (EnemyManagerScript.attack1Location == positionNumber || EnemyManagerScript.attack2Location == positionNumber || EnemyManagerScript.attack1Location2 == positionNumber || EnemyManagerScript.attack2Location2 == positionNumber)
        {
            if (!dangerShown[i])
            {
                GameObject dangerAlert = netrixiDanger;

                // If Netrixi is in danger
                if (positionNumber == CharacterManagerScript.netrixiPosition) dangerAlert = netrixiDanger;

                // If Folkvar is in danger
                if (positionNumber == CharacterManagerScript.folkvarPosition) dangerAlert = folkvarDanger;

                // If Iv is in danger
                if (positionNumber == CharacterManagerScript.ivPosition) dangerAlert = ivDanger;

                Vector3 loc = new Vector3(positions[i].transform.position.x, positions[i].transform.position.y, 0);
                Instantiate(dangerAlert, loc, quaternion.identity);

                dangerShown[i] = true;
            }
        }
    }

    void CheckForNetrixi(int positionNumber)
    {
        int i = positionNumber - 1;

        if (CombatManagerScript.firstAttack == 0)
        {
            if (CombatManagerScript.netrixiTarget1Location == positionNumber)
            {
                if (!target1Created)
                {
                    Vector3 loc = new Vector3(positions[i].transform.position.x, positions[i].transform.position.y + targetHeight, 0);
                    target1 = Instantiate(target, loc, quaternion.identity);

                    target1Created = true;
                }
                else
                {
                    Vector3 loc = new Vector3(positions[i].transform.position.x, positions[i].transform.position.y + targetHeight, 0);
                    target1.transform.position = loc;
                }
            }
        }
        else
        {
            if (CombatManagerScript.netrixiTarget2Location == positionNumber)
            {
                if (!target2Created)
                {
                    Vector3 loc = new Vector3(positions[i].transform.position.x, positions[i].transform.position.y + targetHeight, 0);
                    target2 = Instantiate(target, loc, quaternion.identity);

                    target2Created = true;
                }
                else
                {
                    Vector3 loc = new Vector3(positions[i].transform.position.x, positions[i].transform.position.y + targetHeight, 0);
                    target2.transform.position = loc;
                }
            }
        }
    }

    IEnumerator SendDownwards(GameObject thisTarget, int targetNumber)
    {
        Vector3 start = thisTarget.transform.position;
        float wait = 0.002f;

        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(wait);
            
            float yDifference = (targetHeight/2 / 100);
            Vector3 loc = new Vector3(start.x, start.y - (yDifference * i), 1);
            if (thisTarget != null) thisTarget.transform.position = loc;
        }

        Destroy(thisTarget);
        if (targetNumber == 1) target1Created = false;
        if (targetNumber == 2) target2Created = false;
    }
}
