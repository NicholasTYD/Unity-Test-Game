using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D entityRb;

    [SerializeField] float speed = 5;

    float horizontalInput;
    float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        this.entityRb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 position = this.transform.position;
        position.x += speed * horizontalInput * Time.deltaTime;
        position.y += speed * verticalInput * Time.deltaTime;

        entityRb.MovePosition(position);
    }
}
