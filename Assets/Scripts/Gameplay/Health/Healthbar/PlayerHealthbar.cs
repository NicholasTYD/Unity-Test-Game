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
        // Text displays int instead of floats.
        healthText.text = Mathf.RoundToInt(health) + " / " + Mathf.RoundToInt(maxHealth);
    }
}
