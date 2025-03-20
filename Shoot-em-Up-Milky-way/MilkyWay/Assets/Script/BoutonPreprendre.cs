using UnityEngine;

public class PauseManager : MonoBehaviour

{
    public static bool isPaused = false;
    public GameObject Canvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }
    
    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            Canvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            Canvas.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Canvas.SetActive(false);
    }
}