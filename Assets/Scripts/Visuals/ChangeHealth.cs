using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHealth : MonoBehaviour
{
    public GameObject netrixi, folkvar, iv;
    public GameObject enemy1, enemy2, enemy3;
    
    public Text netrixiHP, folkvarHP, ivHP;
    public Text enemy1HP, enemy2HP, enemy3HP;

    private float offScreen = GameWindowManager.offScreen;

    public static float netrixiHeight = 6.5f, folkvarHeight = 8.5f, ivHeight = 6f;
    public static float enemy1Height, enemy2Height, enemy3Height;

    public static int sizeOfEnemy1, sizeOfEnemy2, sizeOfEnemy3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If this is not a combat scene
        if (!GameManagerScript.inCombat)
        {
            netrixiHP.transform.position = new Vector3(offScreen, offScreen, 0);
            folkvarHP.transform.position = new Vector3(offScreen, offScreen, 0);
            ivHP.transform.position = new Vector3(offScreen, offScreen, 0);
            
            enemy1HP.transform.position = new Vector3(offScreen, offScreen, 0);
            enemy2HP.transform.position = new Vector3(offScreen, offScreen, 0);
            enemy3HP.transform.position = new Vector3(offScreen, offScreen, 0);
        }
        else
        {
            // Determine dimensions of the camera within the scene
            Camera camera = Camera.main;
            float cameraHeight = 2f * camera.orthographicSize;
            float cameraWidth = cameraHeight * camera.aspect;
            
            
            // If Netrixi is still alive
            if (CombatManagerScript.netrixiAlive)
            {
                // Determines how tall Netrixi is
                Vector3 characterLoc = netrixi.transform.position;
                Vector3 hpLoc = new Vector3((Screen.width/2) + characterLoc.x/cameraWidth*Screen.width, (Screen.height/2) + ((characterLoc.y + netrixiHeight)/cameraHeight*Screen.height), 0);

                netrixiHP.transform.position = hpLoc;
                netrixiHP.text = "" + CombatManagerScript.netrixiHP;
            }
            else netrixiHP.transform.position = new Vector3(offScreen, offScreen, 0);

            // If Folkvar is still alive
            if (CombatManagerScript.folkvarAlive)
            {
                // Determines how tall Folkvar is
                Vector3 characterLoc = folkvar.transform.position;
                Vector3 hpLoc = new Vector3((Screen.width/2) + characterLoc.x/cameraWidth*Screen.width, (Screen.height/2) + ((characterLoc.y + folkvarHeight)/cameraHeight*Screen.height), 0);

                folkvarHP.transform.position = hpLoc;
                folkvarHP.text = "" + CombatManagerScript.folkvarHP;
            }
            else folkvarHP.transform.position = new Vector3(offScreen, offScreen, 0);

            // If Iv is still alive
            if (CombatManagerScript.ivAlive)
            {
                // Determines how tall Iv is
                Vector3 characterLoc = iv.transform.position;
                Vector3 hpLoc = new Vector3((Screen.width/2) + characterLoc.x/cameraWidth*Screen.width, (Screen.height/2) + ((characterLoc.y + ivHeight)/cameraHeight*Screen.height), 0);

                ivHP.transform.position = hpLoc;
                ivHP.text = "" + CombatManagerScript.ivHP;
            }
            else ivHP.transform.position = new Vector3(offScreen, offScreen, 0);
            
            
            
            
            // If Enemy 1 is still alive
            if (CombatManagerScript.enemy1Alive)
            {
                // Determines how tall Enemy 1 is
                DetermineSize(sizeOfEnemy1, 1);
                
                Vector3 characterLoc = enemy1.transform.position;
                Vector3 hpLoc = new Vector3((Screen.width/2) + characterLoc.x/cameraWidth*Screen.width, (Screen.height/2) + ((characterLoc.y + enemy1Height)/cameraHeight*Screen.height), 0);

                enemy1HP.transform.position = hpLoc;
                enemy1HP.text = "" + CombatManagerScript.enemy1HP;
            }
            else enemy1HP.transform.position = new Vector3(offScreen, offScreen, 0);

            // If Enemy 2 is still alive
            if (CombatManagerScript.enemy2Alive && EnemyManagerScript.enemy2 != "null")
            {
                // Determines how tall Enemy 2 is
                DetermineSize(sizeOfEnemy2, 2);
                
                Vector3 characterLoc = enemy2.transform.position;
                Vector3 hpLoc = new Vector3((Screen.width/2) + characterLoc.x/cameraWidth*Screen.width, (Screen.height/2) + ((characterLoc.y + enemy2Height)/cameraHeight*Screen.height), 0);

                enemy2HP.transform.position = hpLoc;
                enemy2HP.text = "" + CombatManagerScript.enemy2HP;
            }
            else enemy2HP.transform.position = new Vector3(offScreen, offScreen, 0);

            // If Enemy 3 is still alive
            if (CombatManagerScript.enemy3Alive && EnemyManagerScript.enemy3 != "null")
            {
                // Determines how tall Enemy 3 is
                DetermineSize(sizeOfEnemy3, 3);
                
                Vector3 characterLoc = enemy3.transform.position;
                Vector3 hpLoc = new Vector3((Screen.width/2) + characterLoc.x/cameraWidth*Screen.width, (Screen.height/2) + ((characterLoc.y + enemy3Height)/cameraHeight*Screen.height), 0);

                enemy3HP.transform.position = hpLoc;
                enemy3HP.text = "" + CombatManagerScript.enemy3HP;
            }
            else enemy3HP.transform.position = new Vector3(offScreen, offScreen, 0);
        }

        CheckForSceneChange();
    }

    private static void DetermineSize(int sizeOfEnemy, int enemyNumber)
    {
        float height = 0f;
        
        switch (sizeOfEnemy)
        {
            case 1:
                height = 6.5f;
                break;
            
            case 2:
                height = 6.5f;
                break;
            
            case 3:
                height = 8f;
                break;
            
            case 4:
                height = 8.5f;
                break;
            
            case 5:
                height = 9.5f;
                break;
            
            case 6:
                height = 9f;
                break;
        }

        if (enemyNumber == 1) enemy1Height = height;
        if (enemyNumber == 2) enemy2Height = height;
        if (enemyNumber == 3) enemy3Height = height;
    }

    private void CheckForSceneChange()
    {
        if (GameManagerScript.curtainsOpening)
        {
            netrixiHP.gameObject.SetActive(false);
            folkvarHP.gameObject.SetActive(false);
            ivHP.gameObject.SetActive(false);
            
            enemy1HP.gameObject.SetActive(false);
            enemy2HP.gameObject.SetActive(false);
            enemy3HP.gameObject.SetActive(false);
        }
        else
        {
            netrixiHP.gameObject.SetActive(true);
            folkvarHP.gameObject.SetActive(true);
            ivHP.gameObject.SetActive(true);
            
            enemy1HP.gameObject.SetActive(true);
            enemy2HP.gameObject.SetActive(true);
            enemy3HP.gameObject.SetActive(true);
        }
    }
}
