using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private Animator anim;
    private int attackPower;

    [SerializeField]
    private CapsuleCollider capsuleCol;

    [SerializeField]
    private TrailRenderer trailRenderer;

    void Start()
    {
        TryGetComponent(out anim);

        // TODO CharaData ����Ⴄ
        attackPower = 1;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !anim.IsInTransition(0)) {
            anim.SetBool("Attack", true);
            anim.SetFloat("Speed", 0.0f);
        }
    }

    
    /// <summary>
    /// AnimationEvent ������s
    /// �R���C�_�[�ƃg���C���̃I���I�t�؂�ւ�
    /// </summary>
    /// <param name="switchIndex"></param>
    private void SwitchWeaponCollider(int switchIndex) {
        // �R���C�_�[�I���I�t�؂�ւ�
        capsuleCol.enabled = switchIndex == 0 ? true : false;
        //Debug.Log(capsuleCol.enabled);

        // �g���C���I���I�t�؂�ւ�
        trailRenderer.enabled = switchIndex == 0 ? true : false;
    }


    private void OnTriggerEnter(Collider col) {
        if (!capsuleCol.enabled) {
            return;
        }

        if (col.gameObject.TryGetComponent(out EnemyBase enemyBase)) {
            enemyBase.PrepareCalcHp(-attackPower);
            Debug.Log("�U���q�b�g");
        }
    }
}
