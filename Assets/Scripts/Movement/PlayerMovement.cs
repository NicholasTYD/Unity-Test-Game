using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    float horizontalInput;
    float verticalInput;

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
    
    public override void move()
    {
        Vector2 position = this.transform.position;
        position.x += speed * horizontalInput * Time.deltaTime;
        position.y += speed * verticalInput * Time.deltaTime;

        entityRb.MovePosition(position);

        if (!isFacingCorrectDirection(horizontalInput))
        {
            flip();
        }
    }

    private bool isFacingCorrectDirection(float horizontalInput)
    {
        return (horizontalInput >= 0 && facingRight()) ||
            (horizontalInput < 0 && !facingRight());
    }

    private bool isFacingCorrectDirection(Vector3 directionVector)
    {
        return (directionVector.x > 0 && facingRight()) ||
            (directionVector.x < 0 && !facingRight());
    }

    protected override void flip()
    {
        Vector3 currentScale = this.transform.localScale;
        currentScale.x *= -1;
        this.transform.localScale = currentScale;
    }

    public void FaceMouseDirection()
    {
        Vector3 directionVector = General.GetDirectionVector(this.transform.position, General.GetCurrentMouseWorldPosition());
        if (!isFacingCorrectDirection(directionVector))
        {
            flip();
        }
    }
}
