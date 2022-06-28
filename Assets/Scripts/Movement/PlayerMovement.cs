using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMain))]
public class PlayerMovement : Movement
{
    private PlayerMain playerMain;
    private Animator playerAnim;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;

    public float horizontalInput { get; private set; }
    public float verticalInput { get; private set; }
    private bool inRollState;
    private bool rollReady = true;
    [SerializeField] PlayerRollScriptableObject playerRoll;

    protected override void Start()
    {
        base.Start();
        this.playerMain = this.GetComponent<PlayerMain>();
        this.playerAnim = this.GetComponent<Animator>();
        this.playerMovement = this.GetComponent<PlayerMovement>();
        this.playerHealth = this.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inRollState)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }
    }

    private void FixedUpdate()
    {
        if (inRollState)
        {
            Move();
        }
    }

    public override void Move()
    {
        Vector2 position = this.transform.position;
        position.x += speed * horizontalInput * Time.deltaTime;
        position.y += speed * verticalInput * Time.deltaTime;

        entityRb.MovePosition(position);
        playerAnim.SetFloat("InputProduct",
            Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        if (!isFacingCorrectDirection(horizontalInput))
        {
            flip();
        }
    }

    public void Roll()
    {
        if (!rollReady)
        {
            return;
        }

        playerMain.lockoutDuration = playerRoll.rollDuration;
        playerHealth.grantInvulnerability = playerRoll.rollDuration;
        playerAnim.SetTrigger(playerRoll.name);


        StartCoroutine(lockInputs());

        IEnumerator lockInputs()
        {
            inRollState = true;
            rollReady = false;
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            if (horizontalInput == 0 && verticalInput == 0)
            {
                horizontalInput = facingRight() ? 1 : -1;
            }
            yield return new WaitForSeconds(playerRoll.rollDuration);
            inRollState = false;
            yield return new WaitForSeconds(playerRoll.rollCooldown);
            rollReady = true;
        }
    }

    private bool isFacingCorrectDirection(float horizontalInput)
    {
        return (Mathf.Approximately(horizontalInput, 0) ||
            (horizontalInput > 0 && facingRight()) ||
            (horizontalInput < 0 && !facingRight()));
    }

    private bool isFacingCorrectDirection(Vector3 directionVector)
    {
        return (Mathf.Approximately(directionVector.x, 0) ||
            (directionVector.x > 0 && facingRight()) ||
            (directionVector.x < 0 && !facingRight()));
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
