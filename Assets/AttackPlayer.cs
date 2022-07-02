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

    public void Hit() {
        
    }

    /// <summary>
    /// AnimationEvent Ç©ÇÁé¿çs
    /// </summary>
    /// <param name="switchIndex"></param>
    private void SwitchWeaponCollider(int switchIndex) {
        capsuleCol.enabled = switchIndex == 0 ? true : false;
        Debug.Log(capsuleCol.enabled);

        trailRenderer.enabled = switchIndex == 0 ? true : false;
    }
}
