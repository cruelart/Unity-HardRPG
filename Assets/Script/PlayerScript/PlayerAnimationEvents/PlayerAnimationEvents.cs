using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private PlayerAttackManager playerAttackManager;
    //맨손 격투용-------------------------------------------
    [SerializeField]
    private PlayerHitBox leftHand_hitBox;

    [SerializeField]
    private PlayerHitBox rightHand_hitBox;

    [SerializeField]
    private PlayerHitBox leftLeg_hitBox;

    [SerializeField]
    private PlayerHitBox rightLeg_hitBox;
    //-----------------------------------------------------

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(PlayerAttackManager _playerAttackManager)
    {
        playerAttackManager = _playerAttackManager;
    }
    //---------------------------맨손 공격------------------------------------
    //왼손 공격
    public void OnLeftHandAttackStart()
    {
        if(leftHand_hitBox != null)
        {
            playerAttackManager.StartAttack(leftHand_hitBox);
        }
    }

    public void OnLeftHandAttackEnd()
    {
        if(leftHand_hitBox != null)
        {
            playerAttackManager.EndAttack(leftHand_hitBox);
        }
    }

    //오른손 공격
    public void OnRightHandAttackStart()
    {
        if (rightHand_hitBox != null)
        {
            playerAttackManager.StartAttack(rightHand_hitBox);
        }
    }

    public void OnRightHandAttackEnd()
    {
        if (rightHand_hitBox != null)
        {
            playerAttackManager.EndAttack(rightHand_hitBox);
        }
    }

    //왼쪽 발 공격
    public void OnLeftLegAttackStart()
    {
        if(leftLeg_hitBox != null)
        {
            playerAttackManager.StartAttack(leftLeg_hitBox);
        }
    }

    public void OnLeftLegAttackEnd()
    {
        if (leftLeg_hitBox != null)
        {
            playerAttackManager.EndAttack(leftLeg_hitBox);
        }
    }

    //오른 발 공격
    public void OnRightLegAttackStart()
    {
        if (rightLeg_hitBox != null)
        {
            playerAttackManager.StartAttack(rightLeg_hitBox);
        }
    }

    public void OnRightLegAttackEnd()
    {
        if (rightLeg_hitBox != null)
        {
            playerAttackManager.EndAttack(rightLeg_hitBox);
        }
    }
    //------------------------------------------------------------------
}
