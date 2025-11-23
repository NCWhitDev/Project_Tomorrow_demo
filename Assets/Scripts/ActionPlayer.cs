using System;
using UnityEngine;

/*
 * This handels ActionPlayer Animations
 * 
 * 
 */ 
public class ActionPlayer : MonoBehaviour
{
    public Animator Playeranimator;

    //Update is called once per frame

    public void playerStrikeAnimation()
    {
        //If Player clicks on hit enemy UI button - Plays player hit animation 
        Playeranimator.SetBool("APAttack", true);
    }

    public void playerHeavyStrikeAnimation()
    {
        //If Player clicks on hit enemy UI button - Plays player hit animation 
        Playeranimator.SetBool("APHeavyAttack", true);
    }

    public void PlayerDefendAnimation()
    {
        //If Player clicks on buff self UI button - Player plays buff player animation
        Playeranimator.SetBool("APDefend", true);
    }

    public void PlayerHurtAnimation()
    {
        //If Player takes damage - Plays hurt animation.
        Playeranimator.SetBool("APHurt", true);
    }
}
