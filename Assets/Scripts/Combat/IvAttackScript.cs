using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvAttackScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DoesIvAttack(int playerAttack, int attackNumber)
    {
        if (!CombatManagerScript.ivAlive)
        {
            // Skip the attack
            if (attackNumber == 1) CombatSimulationScript.attack1Delay = 1f;
            else CombatSimulationScript.attack2Delay = 1f;
            return;
        }
        else
        {
            // TODO: Iv's attacks
            
            // ATTACK 1
            // Iv's Block
            AttackScript.delayRate = 1f;
        }
    }
}
