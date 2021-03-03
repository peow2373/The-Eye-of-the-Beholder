using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageValues : MonoBehaviour
{
    // Change these values to change the damage of different abilities

    // Netrixi
    public static int fireball = 15;            // How much damage does the attack do?
    public static float fireballBurn = 0.1f;    // How long does the attack take to do 1 HP in damage?
    public static float fireballDelay = 4f;    // How long before the next attack is started?
    
    public static int lightning = 35;
    public static float lightningBurn = 0f;
    public static float lightningDelay = 1f;

    public static int roundsTransformed = 2;
    public static float transmutateDelay = 1f;
    public static int choices = 4;


    //TODO: Folkvar and the rest of the character's attacks
}
