using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

//Source: https://docs.unity3d.com/530/Documentation/ScriptReference/UI.Button-onClick.html

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform Ally1;
    [SerializeField] private Transform Ally2;
    [SerializeField] private Transform Enemy1;
    [SerializeField] private Transform Enemy2;
    [SerializeField] private Transform Enemy3;

    private Animator playerAnimator;
    private Transform playerInstance;
    Player playerStatsComp;
    Enemy enemyStatsComp1;
    Enemy enemyStatsComp2;
    Enemy enemyStatsComp3;
    
    public Player playerStats; //Reference to player stats
    private CharacterBattle Charplayer;
    public Enemy enemyStats; //Reference to enemy stats

    private TurnOder turn;

    private enum TurnOder
    {
        WaitingForPlayer, WaitingForAlly1, WaitingForAlly2, WaitingForAlly3,
        WaitingForEnemy1, WaitingForEnemy2, WaitingForEnemy3, 
        WaitingForBoss,
        Busy, Lost, Victory
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
            playerInstance = Instantiate(player, new Vector3(Playerpos1.transform.position.x, Playerpos1.transform.position.y), Quaternion.identity);
            playerAnimator = playerInstance.GetComponent<Animator>();
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

        //TO-DO: Null Reference!!!
        //int temp1 = enemyStatsComp1.speed;
        //int temp2 = playerStatsComp.getAgility();
        //Change temp1 to playerStatsComp.getAgility() and temp2 to enemyStatsComp1.speed later after solving the null value error!!!
        int temp1 = 1; //player
        int temp2 = 0; //enemy(s) for now...
        //playerStatsComp != null && enemyStatsComp2 != null &&

        if (temp1 < temp2)
            {
                turn = TurnOder.WaitingForEnemy1;
                Debug.Log("Enemy 1 goes first.");
            }
        else if (temp1 < temp2)
            {
                //TO-DO: I need to compare enimies some how so they go in a certain order based of eachothers speed.
                turn = TurnOder.WaitingForEnemy2;
                Debug.Log("Enemy 2 goes first.");
            }
        else if (temp1 < temp2)
            {
                turn = TurnOder.WaitingForEnemy3;
                Debug.Log("Enemy 3 goes first.");
            }
        //else if (Playerpos1.playerStats.getAgility() < Enemypos4.enemyStats.speed)
        //{
        //For later when I add a boss
        //}
        else
            {
                Debug.Log("Player goes first." + temp1 + " > " + temp2);
                turn = TurnOder.WaitingForPlayer;
            }
    }

    //Updates every frame
    public void Update()
    {
        if (turn == TurnOder.Lost)
        {
        }

        if(turn == TurnOder.Victory)
        {
        }
    }
  
    public void OnBasicAttackButton()
    {
        if (turn == TurnOder.WaitingForPlayer)
        {
            StartCoroutine(PlayerBasicAttack());
            IEnumerator PlayerBasicAttack()
            {
                //Play basic attack animation
                Debug.Log("Basic test SWIPE!!!");
                playerAnimator.SetBool("APAttack", true);
                //Update target health


                yield return new WaitForSeconds(1f); // waits 1 seconds
                playerAnimator.SetBool("APAttack", false);

            }
        }

        if (turn == TurnOder.WaitingForAlly1) return;
        if (turn == TurnOder.WaitingForAlly2) return;
        if (turn == TurnOder.WaitingForAlly3) return;
    }

    public void OnHeavyAttackButton()
    {
        if (turn == TurnOder.WaitingForPlayer)
        {
            StartCoroutine(PlayerOnHeavyAttackButton());
            IEnumerator PlayerOnHeavyAttackButton()
            {
                //Play Heavy attack animation
                Debug.Log("Heavy test SWIPE!!!");
                playerAnimator.SetBool("APHeavyAttack", true);
                //Update target health

                yield return new WaitForSeconds(1f); // waits 1 seconds
                playerAnimator.SetBool("APHeavyAttack", false);
            }
        }

        if (turn == TurnOder.WaitingForAlly1) return;
        if (turn == TurnOder.WaitingForAlly2) return;
        if (turn == TurnOder.WaitingForAlly3) return;

    }

    public void OnBuffButton()
    {
        if (turn == TurnOder.WaitingForPlayer)
        {
            StartCoroutine(PlayerOnBuffButton());
            IEnumerator PlayerOnBuffButton()
            {
                //Play buff target animation
                Debug.Log("Buff test IM STRONG NOW!!!");
                playerAnimator.SetBool("APDefend", true);
                //Update target health

                yield return new WaitForSeconds(2f); // waits 2 seconds
                playerAnimator.SetBool("APDefend", false);
            }
        }

        if (turn == TurnOder.WaitingForAlly1) return;
        if (turn == TurnOder.WaitingForAlly2) return;
        if (turn == TurnOder.WaitingForAlly3) return;

    }

}
