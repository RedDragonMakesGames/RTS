using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Vector3 offset = Vector3.zero;

    private Slider slider = null;
    private HealthComponent health = null;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        health = GetComponent<HealthComponent>();
        if (slider == null || health == null)
        {
            Debug.LogError("Component not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = health.maxHealth;
        slider.value = health.GetHealth();
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
    }
}
