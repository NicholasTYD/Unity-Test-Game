using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMain))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerCombat : Combat, ISavable
{
    private Animator playerAnim;
    private PlayerMovement playerMovement;

    [SerializeField] private float attackSpeed = 1;
    private float maxComboTime = 0.5f; // Time given to continue the attack string before combo reset
    private float UPGRADED_COMBO_TIME = 1.5f;
    private int currentAttackSequence = 0;
    private int currentHurtboxSequence = 0;
    private float comboTimeLeft = 0;

    private bool inBlockState;
    private float MAX_BLOCK_COOLDOWN = 0.5f;
    private float blockCooldownTimer;

    private float maxParryDamageBonusDuration = 3;
    private float parryDamageBonusMultiplier;
    private float currentParryDamageBonusMultiplier = 1;
    private float parryDamageBonusTimeLeft = 0;

    public bool parryStrikeUnlocked { get; set; }
    private float currentParryStrikeBonus = 1;

    // Center offset of the player's sprite
    private Vector2 playerCenterOffset = new Vector2(0, 0.65f);
    private Vector2 playerWorldCenterPosition;
    Vector2 mouseWorldPosition;
    Vector2 playerToMouseUnitDirection;
    
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
        this.parryDamageBonusMultiplier = playerBlock.parryBonusDamageMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        this.handleCooldowns();
    }

    public void IncreaseAttack(float amount)
    {
        attack += amount;
    }

    public void IncreaseAttackSpeed(float amount)
    {
        attackSpeed += amount;
    }

    public void IncreaseParryDamageBonusDuration(float amount)
    {
        maxParryDamageBonusDuration += amount;
    }

    public void IncreaseParryDamageBonusMultiplier(float amount)
    {
        parryDamageBonusMultiplier += amount;
    }

    public void UpgradeComboTime()
    {
        maxComboTime = UPGRADED_COMBO_TIME;
    }

    public override void Attack()
    {
        basicAttack();
    }

    void basicAttack()
    {
        updateAimDirection();

        currentHurtboxSequence = currentAttackSequence;
        PlayerBasicAttackScriptableObject currentAttack = playerBasicAttacks[currentAttackSequence];
        float currentAttackDuration = currentAttack.baseAttackDuration / attackSpeed;
        entityMain.lockoutDuration = currentAttackDuration;
        playerAnim.SetFloat("AttackSpeedMultiplier", attackSpeed);
        playerAnim.SetTrigger(currentAttack.name);
        currentAttackSequence = currentAttackSequence != 2 ? ++currentAttackSequence : 0;
        // Small leeway of 0.01s to account for uneven time ticks.
        comboTimeLeft = Mathf.Max(comboTimeLeft, currentAttackDuration + 0.01f);
    }

    public void CreateHurtbox()
    {
        PlayerBasicAttackScriptableObject currentAttack = playerBasicAttacks[currentHurtboxSequence];
        Vector2 hurtboxWorldCenterPostiion = playerWorldCenterPosition + (currentAttack.hurtboxCenterOffset * playerToMouseUnitDirection);

        CombatMechanics.Instance.DamageCircleAll(hurtboxWorldCenterPostiion,
                currentAttack.hurtboxRadius,
                enemyLayerMask,
                attack * currentParryDamageBonusMultiplier * currentParryStrikeBonus * currentAttack.damageMultiplier);
        currentParryStrikeBonus = 1;
    }

    public void RefreshComboTime()
    {
        comboTimeLeft = maxComboTime;

        Debug.Log(currentAttackSequence);
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
                blockCooldownTimer = MAX_BLOCK_COOLDOWN;
                inBlockState = false;
            }
        }
    }

    public bool Parried()
    {
        blockCooldownTimer = MAX_BLOCK_COOLDOWN;
        if (inBlockState)
        {
            PlayerBlockScriptableObject block = playerBlock;
            Vector2 hurtboxWorldCenterPostiion = playerWorldCenterPosition + (block.hurtboxCenterOffset * playerToMouseUnitDirection);
            LayerMask layersToTest = General.Instance.CombineLayerMask(enemyLayerMask, enemyProjectileLayerMask);
            Collider2D entityCheck = Physics2D.OverlapCircle(hurtboxWorldCenterPostiion,
                block.hurtboxRadius,
                layersToTest);
            if (entityCheck != null)
            {
                entityMain.lockoutDuration = block.baseParryDuration;
                parryDamageBonusTimeLeft = maxParryDamageBonusDuration + block.baseParryDuration;
                currentParryDamageBonusMultiplier = parryDamageBonusMultiplier;
                playerAnim.SetTrigger(block.parryName);
                inBlockState = false;
                CombatMechanics.Instance.InstantiateParryText(this.transform.position);
                if (parryStrikeUnlocked)
                {
                    currentParryStrikeBonus = block.parryStrikeBonusMultiplier;
                }
                return true;
            }
        }
        return false;
    }

    public void interruptCombat()
    {
        inBlockState = false;
        currentAttackSequence = 0;
    }

    private void updateAimDirection()
    {
        this.playerMovement.FaceMouseDirection();
        this.playerWorldCenterPosition = this.transform.position + (Vector3)this.playerCenterOffset;
        this.mouseWorldPosition = General.Instance.GetCurrentMouseWorldPosition();
        this.playerToMouseUnitDirection = General.Instance.GetDirectionUnitVector(playerWorldCenterPosition, mouseWorldPosition);
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

    public void SaveData(SaveData saveData)
    {
        saveData.Attack = attack;
        saveData.AttackSpeed = attackSpeed;
        saveData.ParryDamageBonusDuration = maxParryDamageBonusDuration;
        saveData.ParryDamageBonusMultiplier = parryDamageBonusMultiplier;
        saveData.MaxComboTime = maxComboTime;
        saveData.ParryStrikeUnlocked = parryStrikeUnlocked;
    }

    public void LoadData(SaveData saveData)
    {
        this.attack = saveData.Attack;
        this.attackSpeed = saveData.AttackSpeed;
        this.maxParryDamageBonusDuration = saveData.ParryDamageBonusDuration;
        this.parryDamageBonusMultiplier = saveData.ParryDamageBonusMultiplier;
        this.maxComboTime = saveData.MaxComboTime;
        this.parryStrikeUnlocked = saveData.ParryStrikeUnlocked;
    }
}
