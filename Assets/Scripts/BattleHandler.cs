using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

//Source: https://docs.unity3d.com/530/Documentation/ScriptReference/UI.Button-onClick.html

public class NewEmptyCSharpScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform Ally1;
    [SerializeField] private Transform Ally2;
    [SerializeField] private Transform Enemy1;
    [SerializeField] private Transform Enemy2;
    [SerializeField] private Transform Enemy3;

    public Player playerStats; //Reference to player stats
    private CharacterBattle Charplayer;
    public Enemy enemyStats; //Reference to enemy stats
    public Button BasicAttackButton; //Reference to Basic Attack UIButton.
    public Button HeavyAttackButton; //Reference to Heavy Attack UIButton.
    public Button BuffORDebuffButton; //Reference to Buff/Debuff UIButton.

    private TurnOder turn;

    private enum TurnOder
    {
        WaitingForPlayer, WaitingForAlly1, WaitingForAlly2, WaitingForAlly3,
        WaitingForEnemy1, WaitingForEnemy2, WaitingForEnemy3, 
        WaitingForBoss,
        Busy
    }

    public void Start()
    {
        //Reference objects used to getpos of players & enemies
        GameObject Playerpos1 = GameObject.Find("Player1_pos");
        GameObject Ally1pos = GameObject.Find("Alley1_pos");
        GameObject Ally2pos = GameObject.Find("Alley2_pos");
        GameObject Enemypos1 = GameObject.Find("Enemy_pos1");
        GameObject Enemypos2 = GameObject.Find("Enemy_pos2");
        GameObject Enemypos3 = GameObject.Find("Enemy_pos3");

        // Instantiate Clones the object original and returns the clone.
        // When this method clones a child object, it also clones the child's own children. To prevent stack overflow, Unity limits this nested cloning. If you exceed more than half your stack size, Unity throws an InsufficientExecutionStackException.
        // Quaternion.identity represents zero rotation relative to world coordinate system, aligned with world axes.
        if (player != null)
        {
            Instantiate(player, new Vector3(Playerpos1.transform.position.x, Playerpos1.transform.position.y), Quaternion.identity);
        }

        if (Ally1 != null)
        {
           Instantiate(Ally1, new Vector3(Ally1pos.transform.position.x, Ally1pos.transform.position.y), Quaternion.identity);
        }

        if (Ally2 != null)
        {
           Instantiate(Ally2, new Vector3(Ally2pos.transform.position.x, Ally2pos.transform.position.y), Quaternion.identity);
        }

        if (Enemy1 != null)
        {
            Instantiate(Enemy1, new Vector3(Enemypos1.transform.position.x, Enemypos1.transform.position.y), Quaternion.identity);
        }
        if (Enemy2 != null)
        {
            Instantiate(Enemy2, new Vector3(Enemypos2.transform.position.x, Enemypos2.transform.position.y), Quaternion.identity);
        }
        if (Enemy3 != null)
        {
            Instantiate(Enemy3, new Vector3(Enemypos3.transform.position.x, Enemypos3.transform.position.y), Quaternion.identity);
        }

            // With the following, which uses GetComponent to access the required components:
            Player playerStatsComp = Playerpos1.GetComponent<Player>();
            Enemy enemyStatsComp1 = Enemypos1.GetComponent<Enemy>();
            Enemy enemyStatsComp2 = Enemypos2.GetComponent<Enemy>();
            Enemy enemyStatsComp3 = Enemypos3.GetComponent<Enemy>();

            if (playerStatsComp != null && enemyStatsComp1 != null && playerStatsComp.getAgility() < enemyStatsComp1.speed)
            {
                turn = TurnOder.WaitingForEnemy1;
            }
            else if (playerStatsComp != null && enemyStatsComp2 != null && playerStatsComp.getAgility() < enemyStatsComp2.speed)
            {
                turn = TurnOder.WaitingForEnemy2;
            }
            else if (playerStatsComp != null && enemyStatsComp3 != null && playerStatsComp.getAgility() < enemyStatsComp3.speed)
            {
                turn = TurnOder.WaitingForEnemy3;
            }
        //else if (Playerpos1.playerStats.getAgility() < Enemypos4.enemyStats.speed)
        //{
        //For later when I add a boss
        //}
            else
            {
                turn = TurnOder.WaitingForPlayer;
            }
    }

    //Updates every frame
    public void Update()
    {
       //TO-DO: Check whos turn it is, and see what button they click with that character.
       if(turn == TurnOder.WaitingForPlayer)
       {
            Button basicAttackB = BasicAttackButton.GetComponent<Button>();
            Button heavyAttackB = HeavyAttackButton.GetComponent<Button>();
            Button buffdebuffB = BuffORDebuffButton.GetComponent<Button>();
            //Its the Action Players turn.
            basicAttackB.onClick.AddListener(calltoAPAttack);
            heavyAttackB.onClick.AddListener(calltoAPHeavyAttack);
            buffdebuffB.onClick.AddListener(calltoAPuffer);
        }
       else if(turn == TurnOder.WaitingForAlly1)
       {
            //Its the Ally1's turn.
       }
       else if(turn == TurnOder.WaitingForAlly2)
       {
            //Its the Ally2's turn.
       }
       else if (turn == TurnOder.WaitingForAlly3)
       {
            //Its the Ally3's turn.
       }
       else if (turn == TurnOder.WaitingForEnemy1)
       {
            //Its the Ally3's turn.
       }
       else if (turn == TurnOder.WaitingForEnemy2)
       {
           //Its the Ally3's turn.
       }
       else if (turn == TurnOder.WaitingForEnemy3)
       {
           //Its the Ally3's turn.
       }
       else if (turn == TurnOder.WaitingForBoss)
       {
           //Its the Ally3's turn.
       }
       else
       {
            Debug.Log("There is no state.");
            Debug.Log("Uhh...There should be a state...?");
       }
    }

    void calltoAPAttack()
    {
        Charplayer.Attack();
    }

    void calltoAPHeavyAttack()
    {

    }

    void calltoAPuffer()
    {

    }
}
