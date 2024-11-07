using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyController : MonoBehaviour
{
    public Transform player;             // Reference to the player's transform
    public float teleportDistance = 20f; // Distance to maintain when teleporting
    public float minDistance = 5f;       // Game over distance
    public float maxDistance = 50f;      // Max distance before enemy moves closer
    public float teleportInterval = 5f;  // Time between teleports when not observed

    private float teleportTimer = 0f;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Check if enemy is too far and needs to get closer
        if (distance > maxDistance)
        {
            TeleportCloser();
            Debug.Log("Teleport to the player!");
        }

        // Check if player is looking at the enemy
        if (!IsPlayerLookingAtEnemy())
        {
            teleportTimer += Time.deltaTime;

            if (teleportTimer >= teleportInterval)
            {
                TeleportCloser();
                teleportTimer = 0f;
            }
        }
        else
        {
            teleportTimer = 0f;
        }

        // Check for game over condition
        if (distance < minDistance)
        {
            GameOver();
        }
    }

    bool IsPlayerLookingAtEnemy()
    {
        Vector3 directionToEnemy = transform.position - player.position;
        float angle = Vector3.Angle(player.forward, directionToEnemy);

        // Adjust the angle threshold as needed
        return angle < 60f;
    }

    void TeleportCloser()
    {
        Vector3 randomDirection = Random.onUnitSphere;
        randomDirection.y = 0; // Keep the enemy on the same horizontal plane
        randomDirection.Normalize();

        Vector3 newPosition = player.position + randomDirection * teleportDistance;
        transform.position = newPosition;
    }

    void GameOver()
    {
        // Implement your game over logic here
        Debug.Log("Game Over!");
        SceneManager.LoadScene("GameOver");
    }
}
