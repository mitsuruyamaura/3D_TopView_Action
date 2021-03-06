using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayer : MonoBehaviour
{
    private Rigidbody rb;
    //private Animator anim;

    private PlayerAnimation playerAnim;

    private bool isGrounded;

    [Header("ジャンプ力")]
    public float jumpPower;

    [Header("地面判定用レイヤー")]
    public LayerMask groundLayer;

    void Start()
    {
        TryGetComponent(out rb);
        //TryGetComponent(out anim);

        TryGetComponent(out playerAnim);

        jumpPower = UserData.instance != null ? UserData.instance.currentCharaData.jumpPower : ConstData.DEFAULT_JUMP_POWER;
    }

    void Update()
    {
        // Linecastでキャラの足元に地面があるか判定  地面があるときはTrueを返す
        isGrounded = Physics.Linecast(transform.position + transform.up * 1,
                        transform.position - transform.up * 0.3f,
                        groundLayer);

        if (!isGrounded) {
            return;
        }

        if (Input.GetButtonDown("Jump")) {
            Jump();
        }
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    private void Jump() {
        //isGrounded = false;

        //anim.SetTrigger("Jump");

        playerAnim.ChangeAnimationFromTrigger(AnimationState.Jump);
        rb.AddForce(Vector3.up * jumpPower);
    }
}
