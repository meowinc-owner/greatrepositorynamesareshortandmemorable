using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenu : MonoBehaviour
{
    public TMP_Text doenstExitText;
    public void StartGame()
    {
        SceneManager.LoadScene("weaponSelect");
        
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