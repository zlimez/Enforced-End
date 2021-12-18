using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoostStats : MonoBehaviour
{
    public GameObject boostStatUI;
    public PlayerController player;
    public static float statBalance;
    public GameObject balanceHolder;
    public TextMeshProUGUI balanceText;

    void Start() {
        if (player == null) {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.BackQuote)) {
            if (Pause.GameIsPaused) {
                Pause.ResumeGame();
                boostStatUI.SetActive(false);
            } else {
                Pause.PauseGame();
                boostStatUI.SetActive(true);
            }
        }
    }

 public void boostStat(string statType) {
        balanceText = balanceText == null ? balanceHolder.GetComponent<TextMeshProUGUI>() : balanceText;
        switch (statType) {
            case "movement":
            if (statBalance >= 20) {
                player.movement += 20;
                StatBar.allBars[0].value += 20;
                statBalance -= 20;
            } else {
                player.movement += statBalance;
                StatBar.allBars[0].value += statBalance;
                statBalance = 0;
            }
            player.movement = Mathf.Min(player.movement, 100);
            break;
            case "armour":
            if (statBalance >= 20) {
                player.armour += 20;
                StatBar.allBars[1].value += 20;
                statBalance -= 20;
            } else {
                player.armour += statBalance;
                StatBar.allBars[1].value += statBalance;
                statBalance = 0;
            }
            player.armour = Mathf.Min(player.armour, 100);
            break;
            case "hearing":
            if (statBalance >= 20) {
                player.hear += 20;
                StatBar.allBars[2].value += 20;
                statBalance -= 20;
            } else {
                player.hear += statBalance;
                StatBar.allBars[2].value += statBalance;
                statBalance = 0;
            }
            player.hear = Mathf.Min(player.hear, 100);
            break;
            case "sight":
            if (statBalance >= 20) {
                player.sight += 20;
                StatBar.allBars[3].value += 20;
                statBalance -= 20;
            } else {
                player.sight += statBalance;
                StatBar.allBars[3].value += statBalance;
                statBalance = 0;
            }
            player.sight = Mathf.Min(player.sight, 100);
            break;
        }
        balanceText.SetText("Balance: " + statBalance);
    }

    public void sacrificeStat(string statType) {
        Debug.Log(balanceHolder.GetComponent<TextMeshProUGUI>().text);
        balanceText = balanceText == null ? balanceHolder.GetComponent<TextMeshProUGUI>() : balanceText;
        switch (statType) {
            case "movement":
            if (player.movement >= 20) {
                statBalance += 20;
                player.movement -= 20;
                StatBar.allBars[0].value -= 20;
            } else {
                statBalance += player.movement;
                player.movement = 0;
                StatBar.allBars[0].value = 0;
            }
            break;
            case "armour":
            if (player.armour >= 20) {
                statBalance += 20;
                player.armour -= 20;
                StatBar.allBars[1].value -= 20;
            } else {
                statBalance += player.armour;
                player.armour = 0;
                StatBar.allBars[1].value = 0;
            }
            break;
            case "hearing":
             if (player.hear >= 20) {
                statBalance += 20;
                StatBar.allBars[2].value -= 20;
                player.hear -= 20;
            } else {
                statBalance += player.hear;
                player.hear = 0;       
                StatBar.allBars[2].value = 0;
            }
            break;
            case "sight":
             if (player.sight >= 20) {
                statBalance += 20;
                player.sight -= 20;
                StatBar.allBars[3].value -= 20;
            } else {
                statBalance += player.sight;
                player.sight = 0;
                StatBar.allBars[3].value = 0;
            }
            break;
        }
        balanceText.SetText("Balance: " + statBalance);
    }
}
