using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource Player;
    public AudioSource Enemy;
    public AudioSource Other;
    public AudioClip arrowCast;
    public AudioClip arrowLand;
    public AudioClip arrowWhiz;
    public AudioClip axeLand;
    public AudioClip blockCast;
    public AudioClip bombLand;
    public AudioClip bruteSmash;
    public AudioClip chairLand;
    public AudioClip chairThrow;
    public AudioClip dogBark;
    public AudioClip dogBite;
    public AudioClip healTeam;
    public AudioClip Kaz;
    public AudioClip maceLand;
    public AudioClip mokeHeavy;
    public AudioClip swordLand;
    public AudioClip throwingKnife;
    public AudioClip cellDoor;
    public AudioClip crash;
    public AudioClip dial;
    public AudioClip dyingAssassin1;
    public AudioClip dyingAssassin2;
    public AudioClip dyingAssassin3;
    public AudioClip dyingPlayer1;
    public AudioClip dyingPlayer2;
    public AudioClip dyingPlayer3;
    public AudioClip dyingSoldier1;
    public AudioClip dyingSoldier2;
    public AudioClip dyingSoldier3;
    public AudioClip metalClash;
    
    public AudioClip move;
    public AudioClip select;
    public AudioClip block;
    public AudioClip empower;
    public AudioClip fireballCast;
    public AudioClip fireballLand;
    public AudioClip heal;
    public AudioClip lightningCast;
    public AudioClip lightningLand;
    public AudioClip meleeLand;
    public AudioClip slamCast;
    public AudioClip slamLand;
    public AudioClip smiteCast;
    public AudioClip transmutateCast;
    public AudioClip transmutateLand;
    
    public AudioClip paperFlip;
    public AudioClip swordClash;
    public AudioClip netrixiChosen;
    public AudioClip folkvarChosen;
    public AudioClip ivChosen;
    public AudioClip handPositionClick;

    public AudioClip chooseLeftRight;
    public AudioClip attackChosen;
    public AudioClip startFight;
    public AudioClip undo;
    public AudioClip victory;

    public static SFXManager S;

    // Start is called before the first frame update
    void Start()
    {
        S = this;
        //DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlaySFX(int sfxId)
    {
        switch (sfxId)
        {
            // Enemy Attacks
            case 1:
                Enemy.PlayOneShot(arrowCast, 1);
                break;

            case 2:
                Enemy.PlayOneShot(arrowLand, 1);
                break;

            case 3:
                Enemy.PlayOneShot(arrowWhiz, 1);
                break;

            case 4:
                Enemy.PlayOneShot(axeLand, 1);
                break;

            case 5:
                Enemy.PlayOneShot(blockCast, 1);
                break;

            case 6:
                Enemy.PlayOneShot(bombLand, 1);
                break;

            case 7:
                Enemy.PlayOneShot(bruteSmash, 1);
                break;

            case 8:
                Enemy.PlayOneShot(chairLand, 1);
                break;

            case 9:
                Enemy.PlayOneShot(chairThrow, 1);
                break;

            case 10:
                Enemy.PlayOneShot(dogBark, 1);
                break;

            case 11:
                Enemy.PlayOneShot(dogBite, 1);
                break;

            case 12:
                Enemy.PlayOneShot(healTeam, 1);
                break;

            case 13:
                Enemy.PlayOneShot(Kaz, 1);
                break;

            case 14:
                Enemy.PlayOneShot(maceLand, 1);
                break;

            case 15:
                Enemy.PlayOneShot(mokeHeavy, 1);
                break;

            case 16:
                Enemy.PlayOneShot(swordLand, 1);
                break;

            case 17:
                Enemy.PlayOneShot(throwingKnife, 1);
                break;

            case 18:

                break;

            case 19:

                break;

            // General Sounds
            case 20:

                break;
            
            case 21:
                Other.PlayOneShot(victory, 1.25f);
                break;
            
            case 22:
                Other.PlayOneShot(startFight, 1);
                break;
            
            case 23:
                Other.PlayOneShot(attackChosen, 2);
                break;
            
            case 24:
                Other.PlayOneShot(undo, 1);
                break;
            
            case 25:
                Other.PlayOneShot(cellDoor, 1);
                break;

            case 26:
                Other.PlayOneShot(crash, 1);
                break;

            case 27:
                Other.PlayOneShot(dial, 0.65f);
                break;

            case 28:
                Other.PlayOneShot(dyingAssassin1, 1);
                break;

            case 29:
                Other.PlayOneShot(dyingAssassin2, 1);
                break;

            case 30:
                Other.PlayOneShot(dyingAssassin3, 1);
                break;

            case 31:
                Other.PlayOneShot(dyingPlayer1, 1);
                break;

            case 32:
                Other.PlayOneShot(dyingPlayer2, 1);
                break;

            case 33:
                Other.PlayOneShot(dyingPlayer3, 1);
                break;

            case 34:
                Other.PlayOneShot(dyingSoldier1, 1);
                break;

            case 35:
                Other.PlayOneShot(dyingSoldier2, 1);
                break;

            case 36:
                Other.PlayOneShot(dyingSoldier3, 1);
                break;

            case 37:
                Other.PlayOneShot(metalClash, 1);
                break;

            case 38:
                Other.PlayOneShot(move, 1);
                break;

            // Select option
            case 39:
                Other.PlayOneShot(select, 1);
                break;

            // Next Dialogue
            case 40:
                Other.PlayOneShot(paperFlip, 1);
                break;

            // Start Combat
            case 41:
                Other.PlayOneShot(swordClash, 1);
                break;

            // Choose "Left or Right" option
            case 42:
                Other.PlayOneShot(chooseLeftRight, 1);
                break;

            // Choose Netrixi
            case 43:
                Other.PlayOneShot(select, 1);
                break;

            // Choose Folkvar
            case 44:
                Other.PlayOneShot(select, 1);
                break;

            // Choose Iv
            case 45:
                Other.PlayOneShot(select, 1);
                break;

            
            
            
            // Player Attacks
            case 46:
                Player.PlayOneShot(block, 1);
                break;

            case 47:
                Player.PlayOneShot(empower, 1);
                break;

            case 48:
                Player.PlayOneShot(fireballCast, 1);
                break;

            case 49:
                Player.PlayOneShot(fireballLand, 1);
                break;

            case 50:
                Player.PlayOneShot(heal, 1);
                break;

            case 51:
                Player.PlayOneShot(lightningCast, 1);
                break;

            case 52:
                Player.PlayOneShot(lightningLand, 1);
                break;

            case 53:
                Player.PlayOneShot(meleeLand, 1);
                break;

            case 54:
                Player.PlayOneShot(slamCast, 1);
                break;

            case 55:
                Player.PlayOneShot(slamLand, 1);
                break;

            case 56:
                Player.PlayOneShot(smiteCast, 1);
                break;

            case 57:
                Player.PlayOneShot(transmutateCast, 1);
                break;

            case 58:
                Player.PlayOneShot(transmutateLand, 1);
                break;
        }
    }
}
