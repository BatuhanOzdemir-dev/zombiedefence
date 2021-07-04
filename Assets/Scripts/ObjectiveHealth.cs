using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectiveHealth : MonoBehaviour
{

    [SerializeField]
    public int ObjHealth = 100;
    public int currentHealth;
    public Image healthBar;
    
    void Awake()
    {
        currentHealth = ObjHealth;
        healthBar.fillAmount = currentHealth / ObjHealth;
    }


    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            GameLost();
        }
    }

    public void GameLost()
    {
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Level 1");
    }
}
