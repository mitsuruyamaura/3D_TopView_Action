using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out anim);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !anim.IsInTransition(0)) {
            anim.SetBool("Attack", true);
        }
    }

    public void Hit() {
        
    }
}
