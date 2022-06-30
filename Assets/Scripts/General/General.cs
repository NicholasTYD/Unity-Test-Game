using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour
{
    public static General Instance;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public Vector2 GetDirectionUnitVector(Vector2 from, Vector2 to)
    {
        return (to - from).normalized;
    }

    public Vector2 GetDirectionVector(Vector2 from, Vector2 to)
    {
        return to - from;
    }

    public Vector2 GetCurrentMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public LayerMask CombineLayerMask(LayerMask a, LayerMask b)
    {
        return a | b;
    }
}
