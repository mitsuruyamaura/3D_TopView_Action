using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    //private Animator anim;
    private PlayerAnimation playerAnim;

    private int attackPower;

    [SerializeField]
    private CapsuleCollider capsuleCol;

    [SerializeField]
    private TrailRenderer trailRenderer;

    void Start()
    {
        //TryGetComponent(out anim);
        TryGetComponent(out playerAnim);

        // TODO CharaData から貰う
        attackPower = UserData.instance != null ? UserData.instance.currentCharaData.attackPower : ConstData.DEFAULT_ATTACK_POWER;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !playerAnim.GetAnimator().IsInTransition(0)) {
            //anim.SetBool("Attack", true);
            //anim.SetFloat("Speed", 0.0f);

            playerAnim.ChangeAnimationFromTrigger(AnimationState.Attack);
            playerAnim.ChangeAnimationFromFloat(AnimationState.Speed, 0);
        }
    }

    
    /// <summary>
    /// AnimationEvent から実行
    /// コライダーとトレイルのオンオフ切り替え
    /// </summary>
    /// <param name="switchIndex"></param>
    private void SwitchWeaponCollider(int switchIndex) {
        // コライダーオンオフ切り替え
        capsuleCol.enabled = switchIndex == 0 ? true : false;
        //Debug.Log(capsuleCol.enabled);

        // トレイルオンオフ切り替え
        trailRenderer.enabled = switchIndex == 0 ? true : false;
    }


    private void OnTriggerEnter(Collider col) {
        if (!capsuleCol.enabled) {
            return;
        }

        if (col.gameObject.TryGetComponent(out EnemyBase enemyBase)) {
            enemyBase.PrepareCalcHp(-attackPower);
            Debug.Log("攻撃ヒット");
        }
    }
}
