using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Healthbar : MonoBehaviour
{
    public Image healthIndicator;
    protected Color low = new Color(255f/255, 0, 0); // Red
    protected Color high = new Color(0, 205f/255, 0); // Greenish

    public virtual void SetHealth(float health, float maxHealth)
    {
        Debug.Log("test");
        float healthPercentage = health / maxHealth;
        healthIndicator.color = Color.Lerp(low, high, healthPercentage);
        healthIndicator.transform.localScale = new Vector3(healthPercentage, 1, 1);
    }
}
