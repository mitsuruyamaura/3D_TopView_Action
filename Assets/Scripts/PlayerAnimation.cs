using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AnimationState {
    Attack,
    Down,
    Damage,
    Jump,
    Speed,

}

public class PlayerAnimation : MonoBehaviour {

    private Animator anim;

    void Start() {
        TryGetComponent(out anim);
    }

    /// <summary>
    /// �A�j���̓���(�A���U���E�W�����v)
    /// </summary>
    /// <param name="nextState"></param>
    public void ChangeAnimationFromTrigger(AnimationState nextState) {
        anim.SetTrigger(nextState.ToString());
    }
       
    /// <summary>
    /// �ړ��A�j���Ƒ��x�̓���
    /// </summary>
    /// <param name="nextState"></param>
    /// <param name="amount"></param>
    public void ChangeAnimationFromFloat(AnimationState nextState, float amount) {

        // Locomotion �� parameter �Ƀ`�F�b�N������ Speed �� parameter �ƘA��������
        anim.SetFloat(nextState.ToString(), amount);
    }

    public Animator GetAnimator() {
        return anim;
    }
}
