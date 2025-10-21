using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    public Transform followTransform;
    bool camerafollow;

    /*
    * Update is called once per frame
    * Change our camera transform position to that of our player
    */
    void FixedUpdate()
    {
        if (camerafollow)
        {
            this.transform.position = new Vector3(followTransform.position.x,
            followTransform.position.y, followTransform.position.z);
        }
        
    }

    /*
     * On collision with boundary object, stop following player
     */
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            camerafollow = false;
        }
    }
}
