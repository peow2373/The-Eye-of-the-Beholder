using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManagerScript : MonoBehaviour
{
    public static bool hasChangedHealth = false;

    public static bool netrixiDead = true, folkvarDead = true, ivDead = true;
    public static bool enemy1Dead = true, enemy2Dead = true, enemy3Dead = true;
    
    // Start is called before the first frame update
    void Start()
    {
        hasChangedHealth = false;
    }


    public static void ResetVariables()
    {
        netrixiDead = false;
        if (GameManagerScript.folkvarInParty) folkvarDead = false;
        else folkvarDead = true;
        if (GameManagerScript.ivInParty) ivDead = false;
        else ivDead = true;
        
        enemy1Dead = false;
        if (EnemyManagerScript.enemy2 != "null") enemy2Dead = false;
        else enemy2Dead = true;
        if (EnemyManagerScript.enemy3 != "null") enemy3Dead = false;
        else enemy3Dead = true;
    }


    public static void CheckHealth()
    {
        // Main Characters
        if (!CombatManagerScript.netrixiAlive)
        {
            if (!netrixiDead)
            {
                netrixiDead = true;
                // TODO: Play Netrixi death animation
                SFXManager.S.PlaySFX(31);
            }
        }
        
        if (!CombatManagerScript.folkvarAlive)
        {
            if (!folkvarDead)
            {
                folkvarDead = true;
                // TODO: Play Folkvar death animation
                SFXManager.S.PlaySFX(31);
            }
        }
        
        if (!CombatManagerScript.ivAlive)
        {
            if (!ivDead)
            {
                ivDead = true;
                // TODO: Play Iv death animation
                SFXManager.S.PlaySFX(31);
            }
        }
        
        // Enemy Characters
        if (!CombatManagerScript.enemy1Alive)
        {
            if (!enemy1Dead)
            {
                enemy1Dead = true;
                // TODO: Play Enemy 1 death animation
                SFXManager.S.PlaySFX(35);
            }
        }
        
        if (!CombatManagerScript.enemy2Alive)
        {
            if (!enemy2Dead)
            {
                enemy2Dead = true;
                // TODO: Play Enemy 2 death animation
                SFXManager.S.PlaySFX(34);
            }
        }
        
        if (!CombatManagerScript.enemy3Alive)
        {
            if (!enemy3Dead)
            {
                enemy3Dead = true;
                // TODO: Play Enemy 3 death animation
                SFXManager.S.PlaySFX(36);
            }
        }
    }
    
    
    public static void ChangeHealth(string character, int damage, float timeDelay)
    {
        switch (character)
        {
            // Main Characters
            case "Netrixi":
                var netrixi = new GameObject("runner");
                var runner1 = netrixi.AddComponent<CoroutineRunner>();
                runner1.StartCoroutine(runner1.Wait("Netrixi", damage, netrixi, timeDelay));
                break;
            
            case "Folkvar":
                var folkvar = new GameObject("runner");
                var runner2 = folkvar.AddComponent<CoroutineRunner>();
                runner2.StartCoroutine(runner2.Wait("Folkvar", damage, folkvar, timeDelay));
                break;
            
            case "Iv":
                var iv = new GameObject("runner");
                var runner3 = iv.AddComponent<CoroutineRunner>();
                runner3.StartCoroutine(runner3.Wait("Iv", damage, iv, timeDelay));
                break;
            
            
            
            // Enemy Characters
            case "Enemy 1":
                var enemy1 = new GameObject("runner");
                var runner4 = enemy1.AddComponent<CoroutineRunner>();
                runner4.StartCoroutine(runner4.Wait("Enemy 1", damage, enemy1, timeDelay));
                break;
            
            case "Enemy 2":
                var enemy2 = new GameObject("runner");
                var runner5 = enemy2.AddComponent<CoroutineRunner>();
                runner5.StartCoroutine(runner5.Wait("Enemy 2", damage, enemy2, timeDelay));
                break;
            
            case "Enemy 3":
                var enemy3 = new GameObject("runner");
                var runner6 = enemy3.AddComponent<CoroutineRunner>();
                runner6.StartCoroutine(runner6.Wait("Enemy 3", damage, enemy3, timeDelay));
                break;
        }
    }



    public static void StartingHealth(int enemy1, int enemy2, int enemy3)
    {
        if (!hasChangedHealth)
        {
            CombatManagerScript.enemy1HP = enemy1;
            CombatManagerScript.enemy2HP = enemy2;
            CombatManagerScript.enemy3HP = enemy3;
            
            CombatManagerScript.enemy1StartingHP = enemy1;
            CombatManagerScript.enemy2StartingHP = enemy2;
            CombatManagerScript.enemy3StartingHP = enemy3;

            hasChangedHealth = true;
        }
    }
}



public class CoroutineRunner : MonoBehaviour
{
    public IEnumerator Wait(string character, int damage, GameObject runner, float timeDelay)
    {
        int counter;
        
        // If the character was harmed
        if (damage >= 0) counter = damage;
        // If the character was healed
        else counter = -damage;
        
        for (int i = 0; i < counter; i++)
        {
            yield return new WaitForSecondsRealtime(timeDelay);
        
            // If the character was harmed
            if (damage >= 0) HurtCharacter(character);
            // If the character was healed
            else HealCharacter(character);

            if (i == damage - 1) Destroy(runner);
        }
    }

    private static void HurtCharacter(string character)
    {
        switch (character)
        {
            case "Netrixi": CombatManagerScript.netrixiHP--;
                break;
            
            case "Folkvar": CombatManagerScript.folkvarHP--;
                break;
            
            case "Iv": CombatManagerScript.ivHP--;
                break;
            
            
            case "Enemy 1": CombatManagerScript.enemy1HP--;
                break;
            
            case "Enemy 2": CombatManagerScript.enemy2HP--;
                break;
            
            case "Enemy 3": CombatManagerScript.enemy3HP--;
                break;
        }
    }
    
    
    private static void HealCharacter(string character)
    {
        switch (character)
        {
            case "Netrixi": 
                if (CombatManagerScript.netrixiHP < HealthValues.netrixiHP) CombatManagerScript.netrixiHP++;
                break;
            
            case "Folkvar": 
                if (CombatManagerScript.folkvarHP < HealthValues.folkvarHP) CombatManagerScript.folkvarHP++;
                break;
            
            case "Iv": 
                if (CombatManagerScript.ivHP < HealthValues.ivHP) CombatManagerScript.ivHP++;
                break;
            
            
            case "Enemy 1": 
                if (CombatManagerScript.enemy1HP < CombatManagerScript.enemy1StartingHP) CombatManagerScript.enemy1HP++;
                break;
            
            case "Enemy 2": 
                if (CombatManagerScript.enemy2HP < CombatManagerScript.enemy2StartingHP) CombatManagerScript.enemy2HP++;
                break;
            
            case "Enemy 3": 
                if (CombatManagerScript.enemy3HP < CombatManagerScript.enemy3StartingHP) CombatManagerScript.enemy3HP++;
                break;
        }
    }
}
