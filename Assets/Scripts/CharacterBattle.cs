using UnityEngine;

public class CharacterBattle : MonoBehaviour
{
    private ActionPlayer Player;
    //private AllyNameHere1 ally1;
    //private AllyNameHere2 ally2;
    //private AllyNameHere3 ally3;
    private void Awake()
    {
        Player = GetComponent<ActionPlayer>();
    }

    public void Attack(int x)
    {
        if (x == 1)
        {
            Player.AttackAnimation();
        }
    }

    public void HeavyAttack(int x)
    {
        if(x == 1)
        {
            Player.HeavyAttackAnimation();
        }
        
    }

    public void Buff(int x)
    {
        if (x == 1)
        {
            Player.BufferAnimation();
        }
    }

    public void Hurt(int x)
    {
        if (x == 1)
        {
            Player.HurtAnimation();
        }
    }
}
