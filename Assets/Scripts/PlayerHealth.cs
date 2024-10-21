using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;
    [SerializeField] private int maxHealth = 110;
    private int currentHealth;

    private CharacterMovement disableMovement;
    private WeaponInventory disableInventory;
    private FPS_Camera disableCamera;

    public Image DeathScreen;

    public TMP_Text totalDeathsText;
    private int totalDeaths;

    private bool isDead = false;

    private void Start()
    {
        disableMovement = GetComponent<CharacterMovement>();
        disableInventory = GetComponent<WeaponInventory>();
        disableCamera = GetComponent<FPS_Camera>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        totalDeaths = PlayerPrefs.GetInt("totalDeathsPref");
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {

        if (isDead) return;

        currentHealth -= damage;
        healthBar.SetCurrentHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        totalDeaths++;
        PlayerPrefs.SetInt("totalDeathsPref", totalDeaths);
        disableMovement.enabled = false;
        disableInventory.enabled = false;
        disableCamera.enabled = false;
        for(int i=0; i < transform.childCount; i++)
        {
            Weapon weapon = transform.GetChild(i).GetComponentInChildren<Weapon>();
            if(weapon != null)
            {
                weapon.enabled = false;
            }
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 75);
        Cursor.lockState = CursorLockMode.None;
        DeathScreen.gameObject.SetActive(true);
        UpdateUI();
    }

    private void UpdateUI()
    {
        totalDeathsText.text = "Total deaths: " + totalDeaths.ToString();
    }
}
