using UnityEngine;
using TMPro;

public class CountdownBeforeGame : MonoBehaviour
{
    // vars
    [SerializeField] private float countdownTime = 5f;
    [SerializeField] private TMP_Text countdownText;
    private float countdownTimer;
    private bool active;
    
    void Start()
    {
        Restart();
    }
    
    void Update()
    {
        if (!active)
        {
            return;
        }
        
        countdownTimer -= Time.unscaledDeltaTime;
        countdownText.text = countdownTimer.ToString("F2");
        if (countdownTimer <= 0)
        {
            End();
        }
    }

    public void Restart()
    {
        active = true;
        countdownTimer = countdownTime;
        Time.timeScale = 0;
    }

    private void End()
    {
        active = false;
        countdownText.text = "";
        Time.timeScale = 1;
    }
}
