using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMain))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerCombat : Combat
{
    private Animator playerAnim;
    private PlayerMovement playerMovement;

    private bool isInterrupted;

    [SerializeField] private float attackSpeed = 1;
    private float maxComboTime = 0.5f; // Time given to continue the attack string before combo resets
    private int currentAttackSequence = 0;
    private float comboTimeLeft = 0;

    private bool inBlockState;
    private float maxBlockCooldown = 0.5f;
    private float blockCooldownTimer;

    private float maxParryDamageBonusDuration = 3;
    private float currentParryDamageBonusMultiplier = 1;
    private float parryDamageBonusTimeLeft = 0;

    // Center offset of the player's sprite
    private Vector2 playerCenterOffset = new Vector2(0, 0.65f);
    private Vector2 playerWorldCenterPosition;
    Vector2 mouseWorldPosition;
    Vector2 playerToMouseUnitDirection;
    float angleOfAttack;
    [SerializeField] LayerMask enemyLayerMask;
    [SerializeField] LayerMask enemyProjectileLayerMask;

    [SerializeField] List<PlayerBasicAttackScriptableObject> playerBasicAttacks;
    [SerializeField] PlayerBlockScriptableObject playerBlock;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        this.playerAnim = this.GetComponent<Animator>();
        this.playerMovement = this.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        this.handleCooldowns();
    }

    public override void Attack()
    {
        basicAttack();
    }

    void basicAttack()
    {
        updateAimDirection();

        PlayerBasicAttackScriptableObject currentAttack = playerBasicAttacks[currentAttackSequence];
        float currentAttackDuration = currentAttack.baseAttackDuration / attackSpeed;
        float currentTimeBeforeHit = currentAttack.timeBeforeHit / attackSpeed;
        Vector2 hurtboxWorldCenterPostiion = playerWorldCenterPosition + (currentAttack.hurtboxCenterOffset * playerToMouseUnitDirection);

        StartCoroutine(executeAttack());

        IEnumerator executeAttack()
        {
            isInterrupted = false;
            entityMain.lockoutDuration = currentAttackDuration;
            playerAnim.SetFloat("AttackSpeedMultiplier", attackSpeed);
            playerAnim.SetTrigger(currentAttack.name);
            yield return new WaitForSeconds(currentTimeBeforeHit);
            if (isInterrupted)
            {
                yield break;
            }
            CombatMechanics.Instance.DamageCircleAll(hurtboxWorldCenterPostiion,
                currentAttack.hurtboxRadius,
                enemyLayerMask,
                baseAttack * currentParryDamageBonusMultiplier * currentAttack.damageMultiplier);

            comboTimeLeft = maxComboTime;
            currentAttackSequence = currentAttackSequence != 2 ? ++currentAttackSequence : 0;
        }
    }
    public void Block()
    {
        if (blockCooldownTimer > 0 || inBlockState)
        {
            return;
        }
        updateAimDirection();

        PlayerBlockScriptableObject block = playerBlock;

        StartCoroutine(executeParry());

        IEnumerator executeParry()
        {
            inBlockState = true;
            entityMain.lockoutDuration = block.baseBlockDuration;
            playerAnim.SetTrigger(block.name);
            yield return new WaitForSeconds(block.baseBlockDuration);
            if (inBlockState)
            {
                blockCooldownTimer = maxBlockCooldown;
                inBlockState = false;
            }
        }
    }

    public bool Parried()
    {
        if (inBlockState)
        {
            PlayerBlockScriptableObject block = playerBlock;
            Vector2 hurtboxWorldCenterPostiion = playerWorldCenterPosition + (block.hurtboxCenterOffset * playerToMouseUnitDirection);
            LayerMask layersToTest = General.CombineLayerMask(enemyLayerMask, enemyProjectileLayerMask);
            Collider2D entityCheck = Physics2D.OverlapCircle(hurtboxWorldCenterPostiion,
                block.hurtboxRadius,
                layersToTest);
            if (entityCheck != null)
            {
                entityMain.lockoutDuration = block.baseParryDuration;
                parryDamageBonusTimeLeft = maxParryDamageBonusDuration + block.baseParryDuration;
                currentParryDamageBonusMultiplier = block.parryBonusDamageMultiplier;
                playerAnim.SetTrigger(block.parryName);
                inBlockState = false;
                CombatMechanics.Instance.InstantiateParryText(this.transform.position);
                return true;
            }
        }
        return false;
    }

    public void interruptCombat()
    {
        this.isInterrupted = true;
        inBlockState = false;
        currentAttackSequence = 0;
    }

    private void updateAimDirection()
    {
        this.playerMovement.FaceMouseDirection();
        this.playerWorldCenterPosition = this.transform.position + (Vector3)this.playerCenterOffset;
        this.mouseWorldPosition = General.GetCurrentMouseWorldPosition();
        this.playerToMouseUnitDirection = General.GetDirectionUnitVector(playerWorldCenterPosition, mouseWorldPosition);
        this.angleOfAttack = Vector2.SignedAngle(Vector2.right, playerToMouseUnitDirection);
    }

    private void handleCooldowns()
    {
        if (comboTimeLeft > 0)
        {
            comboTimeLeft -= Time.deltaTime;
        }
        else
        {
            currentAttackSequence = 0;
        }

        if (parryDamageBonusTimeLeft > 0)
        {
            parryDamageBonusTimeLeft -= Time.deltaTime;
        } else
        {
            currentParryDamageBonusMultiplier = 1;
        }

        if (blockCooldownTimer > 0)
        {
            blockCooldownTimer -= Time.deltaTime;
        }
    }
}
