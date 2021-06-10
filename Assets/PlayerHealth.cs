using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    [SerializeField] int healthDecrease = 1;
    [SerializeField] Text healthText;

    private void Start()
    {
        healthText.text = maxHealth.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        maxHealth = maxHealth - healthDecrease;
        healthText.text = maxHealth.ToString();
        GetComponent<AudioSource>().Play();
    }
}
