using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : MonoBehaviour
{
    public TextMeshProUGUI text;
    private float lifetime = 0.8f;
    private float distance = 1.25f;
    private float fadeoutAfterPercentageLifetime = 0.75f;

    private Vector2 initialPosition;
    private Vector2 targetPosition;
    private float timer;

    // Start is called before the first frame update
    void Awake()
    {
        this.transform.LookAt(2 * transform.position - Camera.main.transform.position);

        this.initialPosition = transform.position;
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

    public void setText(float value)
    {
        text.text = value.ToString();
    }

    public void setText(string str)
    {
        text.text = str;
    }
}
