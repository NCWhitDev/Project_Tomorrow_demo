using UnityEngine;

public class CharacterBattle : MonoBehaviour
{
    //TO-DO: Add a call to your ActionPlayerUIButtons to get button ref
    //TO-DO: Add a call to your TurnOrder to check if its your turn.
    private ActionPlayer Player;
    private void Awake()
    {
        Player = GetComponent<ActionPlayer>();
    }

    public void APAttack()
    {

    }

    public void APHeavyAttack()
    {

    }

    public void APDefend()
    {

    }

    public void APHurt()
    {

    }
}
