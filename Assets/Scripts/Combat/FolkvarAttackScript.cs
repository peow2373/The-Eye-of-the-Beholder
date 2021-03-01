using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolkvarAttackScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DoesFolkvarAttack(int playerAttack, int attackNumber)
    {
        // Check if Folkvar is still alive
        if (!CombatManagerScript.folkvarAlive)
        {
            // Skip the attack
            if (attackNumber == 1) CombatSimulationScript.attack1Delay = 1f;
            else CombatSimulationScript.attack2Delay = 1f;
            return;
        }
        else
        {
            // TODO: Folkvar's attacks
            
            // ATTACK 1
            // Folkvar's Swing Sword
            AttackScript.delayRate = 1f;
        }
    }
}
