using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform Ally1;
    [SerializeField] private Transform Ally2;
    [SerializeField] private Transform Enemy1;
    [SerializeField] private Transform Enemy2;
    [SerializeField] private Transform Enemy3;

    public SaveController saveController;

    private Animator playerAnimator;
    private Transform playerInstance;
    //private AnimationClip playerAttackAnim;

    private Animator enemyani_1;
    private Animator enemyani_2;
    private Animator enemyani_3;
    //private AnimationClip enemyAttackAnim_1;
    //private AnimationClip enemyAttackAnim_2;
    //private AnimationClip enemyAttackAnim_3;
    private Transform enemyInstance_1;
    private Transform enemyInstance_2;
    private Transform enemyInstance_3;

    Player playerStatsComp;
    Enemy enemyStatsComp1;
    Enemy enemyStatsComp2;
    Enemy enemyStatsComp3;
    
    public Player playerStats; //Reference to player stats
    private CharacterBattle Charplayer;
    public Enemy enemyStats; //Reference to enemy stats

    private TurnOder turn;
    private Target trackingtarget;

    private bool enemyActionRunning = false;

    private enum TurnOder
    {
        WaitingForPlayer, WaitingForAlly1, WaitingForAlly2, WaitingForAlly3,
        WaitingForEnemy1, WaitingForEnemy2, WaitingForEnemy3, 
        WaitingForBoss,
        Busy, Lost, Victory
    }

    private enum Target
    {
        Boss, Enemy1, Enemy2, Enemy3, AllEnemies, Self, Ally1, Ally2, Ally3, AllAllies
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
            playerStatsComp = playerInstance.GetComponent<Player>();
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
            enemyInstance_1 = Instantiate(Enemy1, new Vector3(Enemypos1.transform.position.x, Enemypos1.transform.position.y), Quaternion.identity);
            enemyani_1 = enemyInstance_1.GetComponent<Animator>();
            enemyStatsComp1 = enemyInstance_1.GetComponent<Enemy>();
        }
        if (Enemy2 != null)
        {
            enemyInstance_2 = Instantiate(Enemy2, new Vector3(Enemypos2.transform.position.x, Enemypos2.transform.position.y), Quaternion.identity);
            enemyani_2 = enemyInstance_2.GetComponent<Animator>();
            enemyStatsComp2 = enemyInstance_2.GetComponent<Enemy>();
        }
        if (Enemy3 != null)
        {
            enemyInstance_3 = Instantiate(Enemy3, new Vector3(Enemypos3.transform.position.x, Enemypos3.transform.position.y), Quaternion.identity);
            enemyani_3 = enemyInstance_3.GetComponent<Animator>();
            enemyStatsComp3 = enemyInstance_3.GetComponent<Enemy>();
        }

        Debug.Log("Battle Start! Determining turn order...");
        Debug.Log("Player Agility: " + playerStatsComp.getAgility());
        Debug.Log("Player Strength: " + playerStatsComp.getStrength());
        Debug.Log("Enemy 1 Speed: " + enemyStatsComp1.speed);
        Debug.Log("Enemy 2 Speed: " + enemyStatsComp2.speed);
        Debug.Log("Enemy 3 Speed: " + enemyStatsComp3.speed);
        Debug.Log("==================================================");
        Debug.Log("Players Health: " + playerStatsComp.health);
        Debug.Log("Enemy 1 Health: " + enemyStatsComp1.health);
        Debug.Log("Enemy 2 Health: " + enemyStatsComp2.health);
        Debug.Log("Enemy 3 Health: " + enemyStatsComp3.health);
        Debug.Log("==================================================");
        Debug.Log("Primary target set to Enemy 1 by default.");
        trackingtarget = Target.Enemy1;

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
                Debug.Log("Player goes first. " + temp1 + " > " + temp2);
                turn = TurnOder.WaitingForPlayer;
            }
    }

    //Updates every frame
    public void Update()
    {
        if (!enemyActionRunning &&
            (turn == TurnOder.WaitingForEnemy1 ||
             turn == TurnOder.WaitingForEnemy2 ||
             turn == TurnOder.WaitingForEnemy3))
        {
            EnemyAction();
        }

        if (turn == TurnOder.Lost)
        {
            // later: show game over
            saveController.LoadGame();
        }

        if (turn == TurnOder.Victory)
        {
            // later: show win screen
        }
    }



    //We gonna need to fix all this below later...

    private Enemy GetEnemyFromTarget(Target target)
    {
        switch (target)
        {
            case Target.Enemy1: return enemyStatsComp1;
            case Target.Enemy2: return enemyStatsComp2;
            case Target.Enemy3: return enemyStatsComp3;


            // case Target.Boss: return bossEnemyComp;  // later
            default: return null;
        }
    }

    //TO-DO: UPDATE AFTER CAPSTONE!!!!!! Turn system is temporary for capstone demo!!!
    // Need a way to change turns when player/enemy action is done. Was gonna try and find animation length but not sure how to do that yet.
    public void EnemyAction()
    {
        //Enemy action logic here
        Debug.Log("Enemy Action happening...");
        if (turn == TurnOder.WaitingForEnemy1 && !IsEnemyAlive(enemyStatsComp1))
        {
            GoToNextTurn();
            return;
        }
        if (turn == TurnOder.WaitingForEnemy2 && !IsEnemyAlive(enemyStatsComp2))
        {
            GoToNextTurn();
            return;
        }
        if (turn == TurnOder.WaitingForEnemy3 && !IsEnemyAlive(enemyStatsComp3))
        {
            GoToNextTurn();
            return;
        }

        // existing logic...
        if (turn == TurnOder.WaitingForEnemy1)
        {
            enemyActionRunning = true;
            StartCoroutine(EnemyBasicAttack());
        }
        else if (turn == TurnOder.WaitingForEnemy2)
        {
            enemyActionRunning = true;
            StartCoroutine(EnemyBasic2Attack());
        }
        else if (turn == TurnOder.WaitingForEnemy3)
        {
            enemyActionRunning = true;
            StartCoroutine(EnemyBasicAttack3());
        }
    }


    IEnumerator EnemyBasicAttack()
    {
        //Play basic attack animation
        Debug.Log("Enemy 1 Basic Attack!!!");
        enemyani_1.SetBool("EnemyAttack", true);
        //Update target health
        playerStatsComp.UpdateHealth(-enemyStatsComp1.level * 2); // change damage calculation later
        Debug.Log("Player Health after attack: " + playerStatsComp.getHealth());
        yield return new WaitForSeconds(2f);
        enemyani_1.SetBool("EnemyAttack", false);
        yield return new WaitForSeconds(4f);
        enemyActionRunning = false;
        GoToNextTurn();
    }

    IEnumerator EnemyBasic2Attack()
    {
        //Play basic attack animation
        Debug.Log("Enemy 2 Basic Attack!!!");
        enemyani_2.SetBool("EnemyAttack", true);
        //Update target health
        playerStatsComp.UpdateHealth(-enemyStatsComp2.level * 2); // change damage calculation later
        Debug.Log("Player Health after attack: " + playerStatsComp.getHealth());
        yield return new WaitForSeconds(3f);
        enemyani_2.SetBool("EnemyAttack", false);
        yield return new WaitForSeconds(4f);
        enemyActionRunning = false;
        GoToNextTurn();
    }

    IEnumerator EnemyBasicAttack3()
    {
        //Play basic attack animation
        Debug.Log("Enemy 3 Basic Attack!!!");
        enemyani_3.SetBool("EnemyAttack", true);
        //Update target health
        playerStatsComp.UpdateHealth(-enemyStatsComp3.level * 2); // change damage calculation later
        Debug.Log("Player Health after attack: " + playerStatsComp.getHealth());
        yield return new WaitForSeconds(3f);
        enemyani_3.SetBool("EnemyAttack", false);
        yield return new WaitForSeconds(4f);
        enemyActionRunning = false;
        GoToNextTurn();
    }

    private bool IsEnemyAlive(Enemy enemy)
    {
        return enemy != null && !enemy.isDead;
    }

    private void GoToNextTurn()
    {
        // If it was player's turn, go to the first alive enemy
        if (turn == TurnOder.WaitingForPlayer)
        {
            if (IsEnemyAlive(enemyStatsComp1))
                turn = TurnOder.WaitingForEnemy1;
            else if (IsEnemyAlive(enemyStatsComp2))
                turn = TurnOder.WaitingForEnemy2;
            else if (IsEnemyAlive(enemyStatsComp3))
                turn = TurnOder.WaitingForEnemy3;
            else
            {
                Debug.Log("All enemies dead -> Victory!");
                turn = TurnOder.Victory;
            }
            return;
        }

        // If it was Enemy1's turn, find the next alive thing
        if (turn == TurnOder.WaitingForEnemy1)
        {
            if (IsEnemyAlive(enemyStatsComp2))
                turn = TurnOder.WaitingForEnemy2;
            else if (IsEnemyAlive(enemyStatsComp3))
                turn = TurnOder.WaitingForEnemy3;
            else
                turn = AnyEnemiesAlive() ? TurnOder.WaitingForPlayer : TurnOder.Victory;

            return;
        }

        if (turn == TurnOder.WaitingForEnemy2)
        {
            if (IsEnemyAlive(enemyStatsComp3))
                turn = TurnOder.WaitingForEnemy3;
            else
                turn = AnyEnemiesAlive() ? TurnOder.WaitingForPlayer : TurnOder.Victory;

            return;
        }

        if (turn == TurnOder.WaitingForEnemy3)
        {
            // last enemy in the chain, go back to player if anyone's still alive
            turn = AnyEnemiesAlive() ? TurnOder.WaitingForPlayer : TurnOder.Victory;
            return;
        }
    }


    private bool AnyEnemiesAlive()
    {
        return IsEnemyAlive(enemyStatsComp1)
            || IsEnemyAlive(enemyStatsComp2)
            || IsEnemyAlive(enemyStatsComp3);
    }



    //TO-DO: SHOULD BE USING CharacterBattle METHODS INSTEAD OF ANIMATOR DIRECTLY!!! ALSO using ActionPlayer...
    //ALSO NEED TO PASS TARGETS LATER!!! And the wait times need to be dynamic based on animation length!!!
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
                if (trackingtarget == Target.AllEnemies)
                {
                    Debug.Log("All enemies took damage!");
                    if(enemyStatsComp1 != null) enemyStatsComp1.UpdateHealth(-playerStatsComp.getStrength());
                    if(enemyStatsComp2 != null) enemyStatsComp2.UpdateHealth(-playerStatsComp.getStrength());
                    if(enemyStatsComp3 != null) enemyStatsComp3.UpdateHealth(-playerStatsComp.getStrength());
                    Debug.Log("Damage hit " + playerStatsComp.getStrength() + " to " + enemyStatsComp1.getHealth() + " " + enemyStatsComp2.getHealth() + " " + enemyStatsComp3.getHealth());
                }
                else
                {
                    Enemy targetEnemy = GetEnemyFromTarget(trackingtarget);
                    if (targetEnemy != null)
                    {
                        targetEnemy.UpdateHealth(-playerStatsComp.getStrength());
                        Debug.Log("Targeted enemy took" + playerStatsComp.getStrength() + " damage! = " + targetEnemy.getHealth());

                        if (targetEnemy.isDead)
                        {
                            Debug.Log("Enemy defeated!");
                            if (IsEnemyAlive(enemyStatsComp1)) trackingtarget = Target.Enemy1;
                            else if (IsEnemyAlive(enemyStatsComp2)) trackingtarget = Target.Enemy2;
                            else if (IsEnemyAlive(enemyStatsComp3)) trackingtarget = Target.Enemy3;
                            //else trackingtarget = Target.Boss; // or leave it unchanged / set None
                        }
                    }
                }
                yield return new WaitForSeconds(1.5f); 
                playerAnimator.SetBool("APAttack", false);
                yield return new WaitForSeconds(4f);
                GoToNextTurn();
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
                int Heavyattk = playerStatsComp.getStrength() * 2; //Example heavy attack does double damage
                //Update target health
                if (trackingtarget == Target.AllEnemies)
                {
                    Debug.Log("All enemies took damage!");
                    if (enemyStatsComp1 != null) enemyStatsComp1.UpdateHealth(-Heavyattk);
                    if (enemyStatsComp2 != null) enemyStatsComp2.UpdateHealth(-Heavyattk);
                    if (enemyStatsComp3 != null) enemyStatsComp3.UpdateHealth(-Heavyattk);
                    Debug.Log(enemyStatsComp1.getHealth() + " " + enemyStatsComp2.getHealth() + " " + enemyStatsComp3.getHealth());
                }
                else
                {
                    Enemy targetEnemy = GetEnemyFromTarget(trackingtarget);
                    if (targetEnemy != null)
                    {
                        targetEnemy.UpdateHealth(-Heavyattk);
                        Debug.Log("Targeted enemy took" + Heavyattk + " damage! = " + targetEnemy.getHealth());
                        if(targetEnemy.isDead)
                        {
                            Debug.Log("Enemy defeated!");
                            if (IsEnemyAlive(enemyStatsComp1)) trackingtarget = Target.Enemy1;
                            else if (IsEnemyAlive(enemyStatsComp2)) trackingtarget = Target.Enemy2;
                            else if (IsEnemyAlive(enemyStatsComp3)) trackingtarget = Target.Enemy3;
                            //else trackingtarget = Target.Boss; // or leave it unchanged / set None
                        }
                    }
                }
                yield return new WaitForSeconds(1.5f); 
                playerAnimator.SetBool("APHeavyAttack", false);
                yield return new WaitForSeconds(4f);
                GoToNextTurn();
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
                if(trackingtarget == Target.Self)
                {
                    playerStatsComp.UpdateHealth(20); //Example buff heals 20 health
                    Debug.Log("Player buffed self! " + playerStatsComp.getHealth());
                }
                else if(trackingtarget == Target.AllAllies)
                {
                    Debug.Log("All allies buffed!");
                    //Assuming Ally1 and Ally2 have similar UpdateHealth methods
                    //Ally1StatsComp.UpdateHealth(20);
                    //Ally2StatsComp.UpdateHealth(20);
                    //Ally3StatsComp.UpdateHealth(20);
                }
                else
                {
                    //TO-DO: Create a GetAllyFromTarget method similar to GetEnemyFromTarget
                }

                yield return new WaitForSeconds(1.5f); 
                playerAnimator.SetBool("APDefend", false);
                yield return new WaitForSeconds(4f);
                GoToNextTurn();
            }
        }

        if (turn == TurnOder.WaitingForAlly1) return;
        if (turn == TurnOder.WaitingForAlly2) return;
        if (turn == TurnOder.WaitingForAlly3) return;

    }

    //Target selection buttons: TODO: Need to highlight selected target later!!!
    public void OnTargetEnemy1Button()
    {
        //Enemy1 target selected
        if (turn == TurnOder.WaitingForPlayer)
        {
            Debug.Log("Enemy 1 target selected");
            trackingtarget = Target.Enemy1;
        }
    }

    public void OnTargetEnemy2Button()
    {
        //Enemy2 target selected
        if (turn == TurnOder.WaitingForPlayer)
        {
            Debug.Log("Enemy 2 target selected");
            trackingtarget = Target.Enemy2;
        }
    }

    public void OnTargetEnemy3Button()
    {
        //Enemy3 target selected
        if (turn == TurnOder.WaitingForPlayer)
        {
            Debug.Log("Enemy 3 target selected");
            trackingtarget = Target.Enemy3;
        }
    }

    public void OnTargetBossButton()
    {
        //Enemy boss target selected
        if (turn == TurnOder.WaitingForPlayer)
        {
            Debug.Log("Boss target selected");
            trackingtarget = Target.Boss;
        }
    }
    public void OnTargetAllEneimesButton()
    {
        if (turn == TurnOder.WaitingForPlayer)
        {
            Debug.Log("All enemy targets selected");
            trackingtarget = Target.AllEnemies;
        }
    }

    public void OnTargetAlly1Button()
    {
        if (turn == TurnOder.WaitingForPlayer)
        {
            Debug.Log("Ally 1 selected");
            trackingtarget = Target.Ally1;
        }
    }

    public void OnTargetAlly2Button()
    {
        if (turn == TurnOder.WaitingForPlayer)
        {
            Debug.Log("Ally 2 selected");
            trackingtarget = Target.Ally2;
        }
    }

    public void OnTargetAlly3Button()
    {
        if (turn == TurnOder.WaitingForPlayer)
        {
            Debug.Log("Ally 3 selected");
            trackingtarget = Target.Ally3;
        }
    }

    public void OnTargetAllAlliesButton()
    {
        if (turn == TurnOder.WaitingForPlayer)
        {
            Debug.Log("All Allies selected");
            trackingtarget = Target.AllAllies;
        }
    }

}
