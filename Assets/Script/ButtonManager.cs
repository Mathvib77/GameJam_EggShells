using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SettingsButton()
    {
        Debug.Log("Settings Button clicked");
        // Implement settings functionality here
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
   
}
