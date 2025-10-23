using Unity.Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] BoxCollider2D mapBoundary;
    CinemachineConfiner2D confiner;
    [SerializeField] Direction transitionDirection;
    [SerializeField] float moveUp;
    [SerializeField] float moveDown;
    [SerializeField] float moveLeft;
    [SerializeField] float moveRight;
    enum Direction { Up,
        Down,
        Left,
        Right
    }

    private void Awake()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    /*
     * Detects when the player enters the transition area and updates the camera confiner and player position.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            confiner.BoundingShape2D = mapBoundary;
            UpdatePlayerPos(collision.gameObject);
            
        }
    }
    

    /*
     * Updates the player's position based on the transition direction.
     */
    private void UpdatePlayerPos(GameObject player)
    {
        Vector3 newPos = player.transform.position;

        switch (transitionDirection)
        {
            case Direction.Up:
                newPos.y += moveUp;
                break;

            case Direction.Down:
                newPos.y -= moveDown;
                break;

            case Direction.Left:
                newPos.x -= moveLeft;
                break;

            case Direction.Right:
                newPos.x += moveRight;
                break;
        }

        player.transform.position = newPos;
    }
}
