using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMain))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerCombat : Combat
{
    private PlayerMain playerMain;
    private Animator playerAnim;
    private PlayerMovement playerMovement;

    private float attackSpeed = 1;
    // Time given to continue the attack string before combo resets
    private float maxComboTime = 0.5f;

    private int currentAttackSequence = 0;
    private float comboTimeLeft = 0;

    // Center offset of the player's sprite
    private Vector2 playerCenterOffset = new Vector2(0, 0.65f); 
    private Vector2 playerWorldCenterPosition;
    Vector2 mouseWorldPosition;
    Vector2 playerToMouseUnitDirection;
    float angleOfAttack;
    [SerializeField] LayerMask layermask;

    [SerializeField] List<PlayerBasicAttackScriptableObject> playerBasicAttacks;
    

    // Start is called before the first frame update
    void Start()
    {
        this.playerMain = this.GetComponent<PlayerMain>();
        this.playerAnim = this.GetComponent<Animator>();
        this.playerMovement = this.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (comboTimeLeft > 0)
        {
            comboTimeLeft -= Time.deltaTime;
        } else
        {
            currentAttackSequence = 0;
        }
    }

    public override void Attack()
    {
        basicAttack();
    }

    void basicAttack()
    {
        this.playerMovement.FaceMouseDirection();
        this.playerWorldCenterPosition = this.transform.position + (Vector3) this.playerCenterOffset;
        this.mouseWorldPosition = General.GetCurrentMouseWorldPosition();
        this.playerToMouseUnitDirection = General.GetDirectionUnitVector(playerWorldCenterPosition, mouseWorldPosition);
        this.angleOfAttack = Vector2.SignedAngle(Vector2.right, playerToMouseUnitDirection);

        PlayerBasicAttackScriptableObject currentAttack = playerBasicAttacks[currentAttackSequence];
        float currentAttackDuration = currentAttack.baseAttackDuration / attackSpeed;
        Vector2 hurtboxWorldCenterPostiion = playerWorldCenterPosition + (currentAttack.hurtboxCenterOffset * playerToMouseUnitDirection);

        StartCoroutine(executeAttack());

        IEnumerator executeAttack()
        {
            playerMain.lockoutDuration = currentAttackDuration;
            playerAnim.SetTrigger(currentAttack.name);
            yield return new WaitForSeconds(currentAttack.timeBeforeHit);
            CombatMechanics.DamageCircleAll(hurtboxWorldCenterPostiion,
                currentAttack.hurtboxRadius,
                layermask,
                baseAttack * currentAttack.damageMultiplier);

            comboTimeLeft = maxComboTime;
            currentAttackSequence = currentAttackSequence != 2 ? ++currentAttackSequence : 0;
        }
    }
}
