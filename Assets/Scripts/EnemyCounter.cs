using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    private GameObject[] enemies;
    private int enemyCount;


    public TMP_Text enemiesLeftText;
    public TMP_Text totalWinsText;
    private int totalWins;

    public Image WinScreen;


    void Start()
    {
        totalWins = PlayerPrefs.GetInt("totalWinsPref");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
        UpdateUI();
        Debug.Log(enemyCount);
    }

    public void CountAfterDeath()
    {
        enemyCount--;
        Debug.Log(enemyCount);

        if (enemyCount == 0)
        {
            totalWins++;
            PlayerPrefs.SetInt("totalWinsPref", totalWins);
            Cursor.lockState = CursorLockMode.None;
            WinScreen.gameObject.SetActive(true);
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        enemiesLeftText.text = "Enemies left: " + enemyCount.ToString();
        totalWinsText.text = "Total wins:  " + totalWins.ToString();
    }
}
