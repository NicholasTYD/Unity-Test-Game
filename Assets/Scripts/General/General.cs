using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour
{
    public static General Instance;
    public GameObject Player { get; private set; }
    public float playerBoxColliderWidth { get; private set; }
    public float playerBoxColliderHeight { get; private set; }
    public Vector2 PlayerHitboxOffset { get; private set; }

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Player = GameObject.FindWithTag("Player");
        playerBoxColliderWidth = Player.GetComponent<BoxCollider2D>().size.x;
        playerBoxColliderHeight = Player.GetComponent<BoxCollider2D>().size.y;
        PlayerHitboxOffset = Player.GetComponent<BoxCollider2D>().offset;
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
