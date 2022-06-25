using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthbar : Healthbar
{
    [SerializeField] TextMeshProUGUI healthText;

    public override void SetHealth(float health, float maxHealth)
    {
        base.SetHealth(health, maxHealth);
        healthText.text = health + " / " + maxHealth;
    }
}
