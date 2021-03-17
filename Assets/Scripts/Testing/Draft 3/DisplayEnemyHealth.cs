using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEnemyHealth : MonoBehaviour
{
    public bool enemy1, enemy2, enemy3;

    public Text enemy1HP, enemy2HP, enemy3HP;

    private Vector3 locationBefore, locationAfter;

    private int[] heightDifference = new int[5] {125, 150, 175, 200, 225};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        locationBefore = Camera.main.WorldToScreenPoint(this.transform.position);
        
        if (enemy1)
        {
            if (CombatManagerScript.enemy1Alive)
            {
                DetermineEnemy(EnemyManagerScript.enemy1);
            
                enemy1HP.transform.position = locationAfter;
                enemy1HP.text = "Hp:" + CombatManagerScript.enemy1HP;
            }
            else
            {
                enemy1HP.transform.position = new Vector3(-1000, -1000, 0);
            }
        }
        
        if (enemy2)
        {
            if (CombatManagerScript.enemy2Alive && EnemyManagerScript.enemy2 != "null")
            {
                DetermineEnemy(EnemyManagerScript.enemy2);
            
                enemy2HP.transform.position = locationAfter;
                enemy2HP.text = "Hp:" + CombatManagerScript.enemy2HP;
            }
            else
            {
                enemy2HP.transform.position = new Vector3(-1000, -1000, 0);
            }
        }
        
        if (enemy3)
        {
            if (CombatManagerScript.enemy3Alive && EnemyManagerScript.enemy3 != "null")
            {
                DetermineEnemy(EnemyManagerScript.enemy3);
            
                enemy3HP.transform.position = locationAfter;
                enemy3HP.text = "Hp:" + CombatManagerScript.enemy3HP;
            }
            else
            {
                enemy3HP.transform.position = new Vector3(-1000, -1000, 0);
            }
        }
    }

    void DetermineEnemy(string enemyName)
    {
        if (enemyName == "Pirate")
        {
            locationAfter = new Vector3(locationBefore.x, locationBefore.y + heightDifference[1], locationBefore.z);
        }
        
        if (enemyName == "Tavern Brute")
        {
            locationAfter = new Vector3(locationBefore.x, locationBefore.y + heightDifference[3], locationBefore.z);
        }
        
        if (enemyName == "Barkeeper")
        {
            locationAfter = new Vector3(locationBefore.x, locationBefore.y + heightDifference[2], locationBefore.z);
        }
        
        
        
        
        if (enemyName == "Folkvar")
        {
            locationAfter = new Vector3(locationBefore.x, locationBefore.y + heightDifference[3], locationBefore.z);
        }
        
        if (enemyName == "Royal Knight Melee")
        {
            locationAfter = new Vector3(locationBefore.x, locationBefore.y + heightDifference[2], locationBefore.z);
        }
        
        if (enemyName == "Royal Knight Ranged")
        {
            locationAfter = new Vector3(locationBefore.x, locationBefore.y + heightDifference[1], locationBefore.z);
        }
        
        if (enemyName == "Gatekeeper")
        {
            locationAfter = new Vector3(locationBefore.x, locationBefore.y + heightDifference[2], locationBefore.z);
        }
        
        if (enemyName == "Royal Guard 1" || enemyName == "Royal Guard 2")
        {
            locationAfter = new Vector3(locationBefore.x, locationBefore.y + heightDifference[3], locationBefore.z);
        }
        
        
        
        
        if (enemyName == "Skull Grunt Melee")
        {
            locationAfter = new Vector3(locationBefore.x, locationBefore.y + heightDifference[2], locationBefore.z);
        }
        
        if (enemyName == "Skull Grunt Ranged")
        {
            locationAfter = new Vector3(locationBefore.x, locationBefore.y + heightDifference[1], locationBefore.z);
        }
        
        if (enemyName == "Kaz 1")
        {
            locationAfter = new Vector3(locationBefore.x, locationBefore.y + heightDifference[3], locationBefore.z);
        }
        
        if (enemyName == "Kaz 2")
        {
            locationAfter = new Vector3(locationBefore.x, locationBefore.y + heightDifference[3], locationBefore.z);
        }
        
        
        
        
        if (enemyName == "Skull King")
        {
            locationAfter = new Vector3(locationBefore.x, locationBefore.y + heightDifference[4], locationBefore.z);
        }
        
        if (enemyName == "Royal King")
        {
            locationAfter = new Vector3(locationBefore.x, locationBefore.y + heightDifference[4], locationBefore.z);
        }
    }
}
