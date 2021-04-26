using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipScript : MonoBehaviour
{
    private Vector3 offscreen = new Vector3(GameWindowManager.offScreen, GameWindowManager.offScreen, 0);

    public GameObject tipBackground, line;
    public Text enemyMoves, move1, move2;
    public Text tipText;

    private float timeVisible = 6;
    private float timeInvisible = 5;
    private float showAttack = 5f;

    private string previousTip;

    public static bool tipActivated;
    public static bool attackTipActivated;

    private bool firstAttackSelected, secondAttackSelected;

    private string previousOption;
    public Text optionText;
    
    public static int attackPlaying;

    public static int oldRound;

    public static TipScript S;
    
    // Start is called before the first frame update
    void Awake()
    {
        S = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Reset the tips if they aren't supposed to be visible
        if (!GameManagerScript.inCombat || CombatManagerScript.hasRunSimulation) {
            Reset();
            StopAllCoroutines();
        }
        
        // Reset the attack tips if the Undo marker is shown
        if (MarkerManagerScript.goMarker) if (Input.GetKeyDown(KeyCode.U)) Reset();

        // Check to see if all the combat tips have been displayed
        if (!listUsed)
        {
            if (AllTipsUsed(false)) listUsed = true;
        }
        else
        {
            if (AllTipsUsed(true)) listUsed = false;
        }

        // Check to see if the first attack was reset
        if (CombatManagerScript.firstAttack == 0) firstAttackSelected = false;
        else
        {
            if (!firstAttackSelected)
            {
                Reset();
                if (!CombatManagerScript.hasRunSimulation) StartCoroutine(DisplayCombatTip(true));
                firstAttackSelected = true;
            }

            // Check to see if the second attack was reset
            if (CombatManagerScript.secondAttack == 0) secondAttackSelected = false;
            else
            {
                if (!secondAttackSelected)
                {
                    Reset();
                    if (!CombatManagerScript.hasRunSimulation) StartCoroutine(DisplayCombatTip(true));
                    secondAttackSelected = true;
                }
            }
        }
        
        // If it is a new round
        if (CombatManagerScript.roundNumber != oldRound)
        {
            if (!tipActivated && !attackTipActivated) StartCoroutine(DisplayCombatTip(true));
            TipScript.attackPlaying = 0;
        }

        oldRound = CombatManagerScript.roundNumber;
    }
    
    private bool AllTipsUsed(bool unUsedTip) {
        for ( int i = 0; i < combatTipUsed.Length - 1; i++ )
        {
            if ( combatTipUsed[i] == unUsedTip ) return false; 
        }
        return true;
    }
    
    
    
    public static bool listUsed;
    public static List<string> combatTips = new List<string>
    {
        "You can choose a different character at any time",
        "Combat is turn-based, you must choose 2 moves each turn",
        "To change a chosen attack, hold up the 'Undo' marker",
        "Some attacks can be dodged by moving out of 'Danger'",
        "Melee attacks strike whoever is closest and cannot be dodged",
        "You can't choose the same attack twice in a round",
        "The Player's attack will strike before an Enemy's attack",
        "'Move' and 'Block' will occur before the next attack",
        "Some attacks will stun a character, but they can be dodged",
        "It is important to consider what the 'Enemy's Moves' are",
    };
    public static bool[] combatTipUsed = new bool[combatTips.Count];

    public IEnumerator DisplayCombatTip(bool waitBeforeDisplay)
    {
        attackPlaying = 0;
        
        if (waitBeforeDisplay) yield return new WaitForSeconds(timeInvisible);

        if (!attackTipActivated && !tipActivated && !CombatManagerScript.hasRunSimulation)
        {
            // Display combat tip
            tipActivated = true;
            
            tipBackground.SetActive(true);
            move1.enabled = false;
            move2.enabled = false;
            tipText.enabled = true;
        
            line.SetActive(true);
            enemyMoves.fontSize = 56;
            tipText.fontSize = 50;

            int randomIndex = UnityEngine.Random.Range(0, combatTips.Count-1);

            if (AllTipsUsed(true)) randomIndex = 3;
            else randomIndex = CheckTip(randomIndex);
        
            // Checks to see if a new random tip needs to be generated
            int CheckTip(int number)
            {
                if (DetermineTip(number, false) == "New tip")
                {
                    int randomIndexAgain = UnityEngine.Random.Range(0, combatTips.Count-1);
                    return CheckTip(randomIndexAgain);
                }
                return number;
            }

            enemyMoves.text = "Words of Advice";
            tipText.text = DetermineTip(randomIndex, true);
            previousTip = tipText.text;
        }
        
        yield return new WaitForSeconds(timeVisible);

        // Reset everything
        if (tipActivated && !attackTipActivated) Reset();

        yield return new WaitForSeconds(timeInvisible);

        if (!attackTipActivated && !CombatManagerScript.hasRunSimulation) StartCoroutine(DisplayCombatTip(false));
    }

    string DetermineTip(int index, bool forreal)
    {
        for (int i = 0; i < combatTips.Count; i++)
        {
            string tip = combatTips[index];
            
            if (previousTip != tip)
            {
                if (combatTipUsed[index] == listUsed)
                {
                    if (forreal) combatTipUsed[index] = !combatTipUsed[index];
                    return tip;
                }
            }
        }

        return "New tip";
    }
    
    
    
    public static List<string> attackName = new List<string>
    {
        "Fireball",
        "Lightning Strike",
        "Transmutate",
        "Swing Sword",
        "Holy Smite",
        "Grand Slam",
        "Block",
        "Heal",
        "Empower",
        "Move",
    };
    
    public static List<string> attackInfo = new List<string>
    {
        "Launches 3 fireballs that target the remaining enemies*Each fireball deals " + DamageValues.fireball + " damage, and they can all hit the same target",
        "Summons a lightning strike dealing " + DamageValues.lightning + " damage*Rotate your hand to choose a specific target location",
        "Chance to transform a random enemy into an animal*If successful, the enemy cannot attack until next round ends",
        
        "Strikes the nearest enemy for " + DamageValues.swingSword + " damage*Has a " + (int) 100/DamageValues.critChance + "% chance to critically strike for " + DamageValues.swingSword*2 + " damage instead",
        "Disables the enemy's next attack/movement*Only deals " + DamageValues.smite + " damage, but cannot be dodged by the enemy",
        "Targets the center of the largest group of enemies*Deals " + DamageValues.grandSlam + " damage to target, " + (int) DamageValues.grandSlam/2 + " damage to nearby enemies",
        
        "Blocks " + DamageValues.blockedDamage*100 + "% of the next attack's damage*'Empower' then 'Block' to counter the next enemy attack instead",
        "Heals the weakest character for " + DamageValues.heal + " health*Characters can be healed higher than their starting health",
        "Amplify the damage of both the player's and enemy's next attacks*Rotate your hand to choose how much to amplify the damage",
        
        "Move a character to dodge certain attacks*You cannot move two characters onto the same location",
    };

    public IEnumerator DisplayAttackTip(int attackNumber, bool endOtherCoroutines)
    {
        if (endOtherCoroutines)
        {
            StopAllCoroutines();
            StartCoroutine(DisplayAttackTip(attackNumber, false));
        }
        
        string[] info = attackInfo[attackNumber - 1].Split(char.Parse("*"));
        
        if (attackPlaying != attackNumber && !CombatManagerScript.hasRunSimulation)
        {
            // Display attack tip
            attackTipActivated = true;
            //tipActivated = false;

            tipBackground.SetActive(true);
            move1.enabled = false;
            move2.enabled = false;
            tipText.enabled = true;
        
            line.SetActive(true);
            enemyMoves.fontSize = 56;
            tipText.fontSize = 48;

            enemyMoves.text = attackName[attackNumber - 1];
            tipText.text = info[0];
            attackPlaying = attackNumber;
            previousTip = tipText.text;
        }

        yield return new WaitForSeconds(showAttack);
        
        if (attackPlaying == attackNumber && !CombatManagerScript.hasRunSimulation) if (info.Length > 1) tipText.text = info[1];
        
        yield return new WaitForSeconds(showAttack);

        if (attackPlaying == attackNumber)
        {
            Reset();
            if (!CombatManagerScript.hasRunSimulation) StartCoroutine(DisplayCombatTip(true));
        }
    }

    
    
    public void Reset()
    {
        tipBackground.SetActive(false);

        move1.enabled = true;
        move2.enabled = true;
        tipText.enabled = false;
        
        line.SetActive(false);
        
        enemyMoves.fontSize = 62;
        enemyMoves.text = "Enemy's Moves";
        tipText.text = "";
        
        tipActivated = false;
        attackTipActivated = false;
    }
}
