using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    private CapsuleCollider capsuleCol;

    [SerializeField]
    private TrailRenderer trailRenderer;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out anim);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !anim.IsInTransition(0)) {
            anim.SetBool("Attack", true);
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
}
