using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ZombieSpawner : MonoBehaviour
{
    public Transform[] zombieSpawners;
    public GameObject zombiePrefab;
    public int zombieCount;
    public Text youWon;


    private void Awake()
    {
        youWon = GameObject.Find("Text").GetComponent<Text>();
        youWon.enabled = false;
    }

    private void Start()
    {
        
        zombieCount = 0;
        SpawnZombie();
    }
    private void OnEnable()
    {
        ZombieController.OnEnemyKilled += SpawnZombie;
    }

    IEnumerator Win()
    {
        
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Main Menu");
    }
    
    void LevelWon()
    {
        StartCoroutine(Win());
    }

    void SpawnZombie()
    {
        if (zombieCount < 5)
        {
            int randomNumber = Mathf.RoundToInt(Random.Range(0f, zombieSpawners.Length - 1));

            Instantiate(zombiePrefab, zombieSpawners[randomNumber].transform.position, Quaternion.identity);
        }
        else
        {
            youWon.enabled = true;
            LevelWon();
        }
    }
}
