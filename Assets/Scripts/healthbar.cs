using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{

    public GameObject safeHouse;
    private Image img;
    private ObjectiveHealth _objectiveHealth;

    // Start is called before the first frame update
    private void Start()
    {
        _objectiveHealth = safeHouse.GetComponent<ObjectiveHealth>();
    }

    void Awake()
    {
        safeHouse = GameObject.Find("SafeHouse");
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        img.fillAmount = (float) _objectiveHealth.currentHealth /
                         (float) _objectiveHealth.ObjHealth;
    }
}
