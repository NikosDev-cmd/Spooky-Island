using UnityEngine; 
using UnityEngine.SceneManagement;
using GamePix;
public class MainMenu : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Game");
        Gpx.Ads.InterstitialAd();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        Gpx.Ads.InterstitialAd();
    }
}