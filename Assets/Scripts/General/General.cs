using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour
{
    public static Vector2 GetDirectionUnitVector(Vector2 from, Vector2 to)
    {
        return (to - from).normalized;
    }

    public static Vector2 GetDirectionVector(Vector2 from, Vector2 to)
    {
        return to - from;
    }

    public static Vector2 GetCurrentMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public static LayerMask CombineLayerMask(LayerMask a, LayerMask b)
    {
        return a | b;
    }
}
