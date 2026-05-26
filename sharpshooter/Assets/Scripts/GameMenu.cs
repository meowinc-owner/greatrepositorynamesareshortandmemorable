using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameMenu : MonoBehaviour
{
    public TMP_Text doenstExitText;
    public void StartGame()
    {
        SceneManager.LoadScene("game");
        
    }

    public void Settings()
    {
        // add settings menu later
        doenstExitText.gameObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void Menu()
    {
        SceneManager.LoadScene("menu"); 
    }
}