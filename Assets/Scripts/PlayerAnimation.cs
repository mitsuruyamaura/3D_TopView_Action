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
    /// アニメの同期(連続攻撃・ジャンプ)
    /// </summary>
    /// <param name="nextState"></param>
    public void ChangeAnimationFromTrigger(AnimationState nextState) {
        anim.SetTrigger(nextState.ToString());
    }
       
    /// <summary>
    /// 移動アニメと速度の同期
    /// </summary>
    /// <param name="nextState"></param>
    /// <param name="amount"></param>
    public void ChangeAnimationFromFloat(AnimationState nextState, float amount) {

        // Locomotion の parameter にチェックを入れて Speed の parameter と連動させる
        anim.SetFloat(nextState.ToString(), amount);
    }

    public Animator GetAnimator() {
        return anim;
    }
}
