using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayer : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;

    private bool isGrounded;

    [Header("�W�����v��")]
    public float jumpPower;

    [Header("�n�ʔ���p���C���[")]
    public LayerMask groundLayer;

    void Start()
    {
        TryGetComponent(out rb);
        TryGetComponent(out anim);
    }

    void Update()
    {
        // Linecast�ŃL�����̑����ɒn�ʂ����邩����  �n�ʂ�����Ƃ���True��Ԃ�
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
    /// �W�����v
    /// </summary>
    private void Jump() {
        //isGrounded = false;

        anim.SetTrigger("Jump");

        rb.AddForce(Vector3.up * jumpPower);
    }
}
