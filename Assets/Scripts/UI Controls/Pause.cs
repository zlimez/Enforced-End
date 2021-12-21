using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static void PauseGame() {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public static void ResumeGame() {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}
