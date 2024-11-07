using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Collectible : MonoBehaviour
{
    public static int score = 0; // Static variable to track the score
    public Text scoreText; // Reference to the TextMeshPro text component for displaying score

    private void Start()
    {
        UpdateScoreText(); // Initialize score display at the start
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides
        if (other.CompareTag("Player"))
        {
            // Increment the score
            score += 1;
            Debug.Log("+1");
            UpdateScoreText();

            // Destroy this object
            Destroy(gameObject);
        }
    }

    // Method to update the score text
    private void UpdateScoreText()
    {
        scoreText.text =  score.ToString() + "/10";
    }

    public void WinGame()
    {
        SceneManager.LoadScene("WinGame");
    }
}
