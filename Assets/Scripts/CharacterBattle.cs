using UnityEngine;

public class CharacterBattle : MonoBehaviour
{
    //TO-DO: Add a call to your ActionPlayerUIButtons to get button ref
    //TO-DO: Add a call to your TurnOrder to check if its your turn.
    private ActionPlayer Player;
    private Player playerstats;
    public Animator Playeranimator;
    private void Awake()
    {
        Player = GetComponent<ActionPlayer>();
    }

    public void Attack()
    {
        Player.AttackAnimation();
    }

    public void HeavyAttack()
    {
        Player.HeavyAttackAnimation();
    }

    public void Buff()
    {
        Player.BufferAnimation();
    }

    public void Hurt()
    {
        Player.HurtAnimation();
    }
}
