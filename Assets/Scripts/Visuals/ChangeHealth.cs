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
    private static float percentOfHeight;

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
                DetermineSize(2);
                
                Vector3 characterLoc = netrixi.transform.position;
                Vector3 hpLoc = new Vector3((Screen.width/2) + characterLoc.x/cameraWidth*Screen.width, (Screen.height/2) + (characterLoc.y/cameraHeight*Screen.height) + (percentOfHeight*cameraHeight), 0);

                netrixiHP.transform.position = hpLoc;
                netrixiHP.text = "HP:" + CombatManagerScript.netrixiHP;
            }
            else netrixiHP.transform.position = new Vector3(offScreen, offScreen, 0);

            // If Folkvar is still alive
            if (CombatManagerScript.folkvarAlive)
            {
                // Determines how tall Folkvar is
                DetermineSize(3);
                
                Vector3 characterLoc = folkvar.transform.position;
                Vector3 hpLoc = new Vector3((Screen.width/2) + characterLoc.x/cameraWidth*Screen.width, (Screen.height/2) + (characterLoc.y/cameraHeight*Screen.height) + (percentOfHeight*cameraHeight), 0);

                folkvarHP.transform.position = hpLoc;
                folkvarHP.text = "HP:" + CombatManagerScript.folkvarHP;
            }
            else folkvarHP.transform.position = new Vector3(offScreen, offScreen, 0);

            // If Iv is still alive
            if (CombatManagerScript.ivAlive)
            {
                // Determines how tall Iv is
                DetermineSize(1);

                Vector3 characterLoc = iv.transform.position;
                Vector3 hpLoc = new Vector3((Screen.width/2) + characterLoc.x/cameraWidth*Screen.width, (Screen.height/2) + ((characterLoc.y - 1.25f)/cameraHeight*Screen.height) + (percentOfHeight*cameraHeight), 0);

                ivHP.transform.position = hpLoc;
                ivHP.text = "HP:" + CombatManagerScript.ivHP;
            }
            else ivHP.transform.position = new Vector3(offScreen, offScreen, 0);
            
            
            
            
            // If Enemy 1 is still alive
            if (CombatManagerScript.enemy1Alive)
            {
                // Determines how tall Enemy 1 is
                DetermineSize(sizeOfEnemy1);
                
                Vector3 characterLoc = enemy1.transform.position;
                Vector3 hpLoc = new Vector3((Screen.width/2) + characterLoc.x/cameraWidth*Screen.width, (Screen.height/2) + (characterLoc.y/cameraHeight*Screen.height) + (percentOfHeight*cameraHeight), 0);

                enemy1HP.transform.position = hpLoc;
                enemy1HP.text = "HP:" + CombatManagerScript.enemy1HP;
            }
            else enemy1HP.transform.position = new Vector3(offScreen, offScreen, 0);

            // If Enemy 2 is still alive
            if (CombatManagerScript.enemy2Alive && EnemyManagerScript.enemy2 != "null")
            {
                // Determines how tall Enemy 2 is
                DetermineSize(sizeOfEnemy2);
                
                Vector3 characterLoc = enemy2.transform.position;
                Vector3 hpLoc = new Vector3((Screen.width/2) + characterLoc.x/cameraWidth*Screen.width, (Screen.height/2) + (characterLoc.y/cameraHeight*Screen.height) + (percentOfHeight*cameraHeight), 0);

                enemy2HP.transform.position = hpLoc;
                enemy2HP.text = "HP:" + CombatManagerScript.enemy2HP;
            }
            else enemy2HP.transform.position = new Vector3(offScreen, offScreen, 0);

            // If Enemy 3 is still alive
            if (CombatManagerScript.enemy3Alive && EnemyManagerScript.enemy3 != "null")
            {
                // Determines how tall Enemy 3 is
                DetermineSize(sizeOfEnemy3);
                
                Vector3 characterLoc = enemy3.transform.position;
                Vector3 hpLoc = new Vector3((Screen.width/2) + characterLoc.x/cameraWidth*Screen.width, (Screen.height/2) + (characterLoc.y/cameraHeight*Screen.height) + (percentOfHeight*cameraHeight), 0);

                enemy3HP.transform.position = hpLoc;
                enemy3HP.text = "HP:" + CombatManagerScript.enemy3HP;
            }
            else enemy3HP.transform.position = new Vector3(offScreen, offScreen, 0);
        }
    }

    private static void DetermineSize(int sizeOfEnemy)
    {
        switch (sizeOfEnemy)
        {
            case 1:
                percentOfHeight = 1.5f;
                break;
            
            case 2:
                percentOfHeight = 1.5f;
                break;
            
            case 3:
                percentOfHeight = 1.65f;
                break;
            
            case 4:
                percentOfHeight = 1.75f;
                break;
            
            case 5:
                percentOfHeight = 1.95f;
                break;
            
            case 6:
                percentOfHeight = 1.8f;
                break;
        }
    }
}
