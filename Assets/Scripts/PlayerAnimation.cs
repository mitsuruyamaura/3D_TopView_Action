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


    public void ChangeAnimationFromTrigger(AnimationState nextState) {
        anim.SetTrigger(nextState.ToString());
    }
       

    public void ChangeAnimationFromFloat(AnimationState nextState, float amount) {
        anim.SetFloat(nextState.ToString(), amount);
    }

    public Animator GetAnimator() {
        return anim;
    }
}
