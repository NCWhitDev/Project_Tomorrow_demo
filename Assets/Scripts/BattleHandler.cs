using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform ShadowScanner_normal;
    [SerializeField] private Transform ShadowScanner_Error;
    [SerializeField] private Transform ShadowScanner_Truth;

    public void Start()
    {
        //Reference objects used to getpos of players & enemies
        GameObject Playerpos1 = GameObject.Find("Player1_pos");
        float playerPOS1x = Playerpos1.transform.position.x;
        float playerPOS1y = Playerpos1.transform.position.y;
        GameObject Enemypos1 = GameObject.Find("Enemy_pos1");
        float EnemyPOS1x = Enemypos1.transform.position.x;
        float EnemyPOS1y = Enemypos1.transform.position.y;
        GameObject Enemypos2 = GameObject.Find("Enemy_pos2");
        float EnemyPOS2x = Enemypos2.transform.position.x;
        float EnemyPOS2y = Enemypos2.transform.position.y;
        GameObject Enemypos3 = GameObject.Find("Enemy_pos3");
        float EnemyPOS3x = Enemypos3.transform.position.x;
        float EnemyPOS3y = Enemypos3.transform.position.y;


        // Instantiate Clones the object original and returns the clone.
        // When this method clones a child object, it also clones the child's own children. To prevent stack overflow, Unity limits this nested cloning. If you exceed more than half your stack size, Unity throws an InsufficientExecutionStackException.
        // Quaternion.identity represents zero rotation relative to world coordinate system, aligned with world axes.
        Instantiate(player, new Vector3(playerPOS1x, playerPOS1y), Quaternion.identity);

        Instantiate(ShadowScanner_normal, new Vector3(EnemyPOS1x, EnemyPOS1y), Quaternion.identity);

        Instantiate(ShadowScanner_Error, new Vector3(EnemyPOS2x, EnemyPOS2y), Quaternion.identity);

        Instantiate(ShadowScanner_Truth, new Vector3(EnemyPOS3x, EnemyPOS3y), Quaternion.identity);
    }




}
