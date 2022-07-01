using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private Rigidbody rb;
    private Animator anim;

    private Vector2 lookDirection = new Vector2(1, 0);
    private bool isDash;

    [SerializeField]
    private float moveSpeed;


    void Start() {
        TryGetComponent(out rb);
        TryGetComponent(out anim);
    }

    void Update() {
        // �L�[���͔���
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate() {

        // �������l�����Ȃ��ړ����@�B�ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
        //rb.velocity = new (inputHorizontal * moveSpeed, rb.velocity.y, inputVertical * moveSpeed);

        // �ړ�
        MovePlayer();
    }

    /// <summary>
    /// �v���C���[�̈ړ�
    /// </summary>
    private void MovePlayer() {
        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * vertical + Camera.main.transform.right * horizontal;

        // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

        // �ړ���������ƈړ��A�j���̓���
        SyncMoveAnimation();
    }

    /// <summary>
    /// �ړ���������ƈړ��A�j���̓���
    /// </summary>
    private void SyncMoveAnimation() {

        if (!Mathf.Approximately(horizontal, 0.0f) || !Mathf.Approximately(vertical, 0.0f)) {
            lookDirection.Set(horizontal, vertical);
            lookDirection.Normalize();

            //anim.SetFloat("Look X", lookDirection.x);
            //anim.SetFloat("Look Y", lookDirection.y);

            // �_�b�V���L���ɉ����ăA�j���̍Đ����x�𒲐�
            anim.SetFloat("Speed", isDash ? 2 : lookDirection.sqrMagnitude);

            //Debug.Log(lookDirection.sqrMagnitude);
        } else {
            anim.SetFloat("Speed", 0);
        }
    }
}
