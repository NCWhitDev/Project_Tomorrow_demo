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

    public void AttackAnimation()
    {
        //If Player clicks on hit enemy UI button - Plays player hit animation 
        Playeranimator.SetBool("APAttack", true);
    }

    public void HeavyAttackAnimation()
    {
        //If Player clicks on hit enemy UI button - Plays player hit animation 
        Playeranimator.SetBool("APHeavyAttack", true);
    }

    public void BufferAnimation()
    {
        //If Player clicks on buff self UI button - Player plays buff player animation
        Playeranimator.SetBool("APDefend", true);
    }

    public void HurtAnimation()
    {
        //If Player takes damage - Plays hurt animation.
        Playeranimator.SetBool("APHurt", true);
    }
}
