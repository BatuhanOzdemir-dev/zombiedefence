using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetHealthBar : MonoBehaviour
{
    public GameObject buildingObj;
    public GameObject _textMeshPro;
    private int buildingHealth;
    public Text _textHealth;
    
    void Start()
    {
        _textHealth = GetComponent<Text>();
        buildingHealth = buildingObj.GetComponent<ObjectiveHealth>().currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth(buildingHealth);
    }

    private void SetHealth(int health)
    {
        _textHealth.text = health.ToString();
    }
}
