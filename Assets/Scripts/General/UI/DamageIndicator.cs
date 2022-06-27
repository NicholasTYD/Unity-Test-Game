using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageIndicator : MonoBehaviour
{
    public TextMeshProUGUI text;
    private float lifetime = 0.8f;
    private float distance = 2;
    private float fadeoutAfterPercentageLifetime = 1f;
    private Vector3 offset = new Vector3(0, 1, 0);

    private Vector2 initialPosition;
    private Vector2 targetPosition;
    private float timer;

    // Start is called before the first frame update
    void Awake()
    {
        this.transform.LookAt(2 * transform.position - Camera.main.transform.position);

        this.initialPosition = offset + transform.position;
        this.targetPosition = transform.position + new Vector3(0, distance, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > lifetime)
        {
            Destroy(gameObject);
        }

        float currentPercentageLifetime = timer / lifetime;
        if (currentPercentageLifetime > fadeoutAfterPercentageLifetime)
        {
            text.color = Color.Lerp(text.color, Color.clear,
                (timer - fadeoutAfterPercentageLifetime) / (lifetime - fadeoutAfterPercentageLifetime));
        }

        this.transform.position = Vector2.Lerp(initialPosition, targetPosition, Mathf.Sin(1.5f * timer / lifetime));
    }

    public void setDamageText(float value)
    {
        text.text = value.ToString();
    }
}
