using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    public Transform followTransform;

    /*
    * Update is called once per frame
    * Change our camera transform position to that of our player
    */
    void FixedUpdate()
    {
        this.transform.position = new Vector3(followTransform.position.x,
            followTransform.position.y, followTransform.position.z);
    }
}
