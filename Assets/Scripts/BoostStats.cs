using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostStats : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject boostStatUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Time.timeScale = 1f;
                boostStatUI.SetActive(false);
                GameIsPaused = false;
            } else {
                Time.timeScale = 0f;
                boostStatUI.SetActive(true);
                GameIsPaused = true;
            }
        }
    }
}
