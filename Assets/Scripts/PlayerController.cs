using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.PostFX;
using UnityEngine.Rendering.PostProcessing;

// AnimationClip �Ƀ^�O��ݒ�
// https://gametukurikata.com/animationanimator/animatorstatetag

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private Rigidbody rb;
    //private Animator anim;
    private PlayerAnimation playerAnim; 

    private Vector2 lookDirection = new Vector2(1, 0);
    private bool isDash;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private CinemachinePostProcessing postProcessing;

    private Vignette vignette;


    /// <summary>
    /// �����ݒ�
    /// </summary>
    /// <param name="camera"></param>
    public void SetUpPlayer(CameraControllerFromCinemachine camera) {

        camera.TryGetComponent(out postProcessing);
        //postProcessing = camera.GetComponent<CinemachinePostProcessing>();

        TryGetComponent(out rb);
        //TryGetComponent(out anim);

        TryGetComponent(out playerAnim);

        // �ʏ�̎擾���@
        //vignette = postProcessing.m_Profile.GetSetting<Vignette>();

        if (!postProcessing.m_Profile.TryGetSettings(out vignette)) {
            Debug.Log("Vignette �擾�o���܂���B");
        } 

        moveSpeed = UserData.instance != null ? UserData.instance.currentCharaData.moveSpeed : ConstData.DEFAULT_MOVE_SPEED;
    }
    
    // void Start() {
    //     TryGetComponent(out rb);
    //     //TryGetComponent(out anim);
    //
    //     TryGetComponent(out playerAnim);
    //
    //     // �ʏ�̎擾���@
    //     //vignette = postProcessing.m_Profile.GetSetting<Vignette>();
    //
    //     if (!postProcessing.m_Profile.TryGetSettings(out vignette)) {
    //         Debug.Log("Vignette �擾�o���܂���B");
    //     } 
    //
    //     moveSpeed = UserData.instance != null ? UserData.instance.currentCharaData.moveSpeed : ConstData.DEFAULT_MOVE_SPEED;
    // }

    void Update() {
        
        if(!playerAnim) return;

        // Attack �^�O�̐ݒ肳��Ă���A�j���Đ����̏ꍇ
        if (playerAnim.GetAnimator().GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
            //Debug.Log("�ړ���~�B�ړ��̃L�[���͒�~");
            // �ړ���~�B�ړ��̃L�[���͒�~
            horizontal = 0;
            vertical = 0;
            rb.velocity = Vector3.zero;
            return;
        }

        // �L�[���͔���
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        isDash = Input.GetKey(KeyCode.LeftShift);

        if (vignette) {
            vignette.intensity.value = isDash ? 0.5f : 0;
        }

        // �ړ���������ƈړ��A�j���̓���
        SyncMoveAnimation();
    }

    void FixedUpdate() {

        if (!rb) return;
        
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

        float speed = isDash ? moveSpeed * 2.0f : moveSpeed;

        // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
        rb.velocity = moveForward * speed + new Vector3(0, rb.velocity.y, 0);

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
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
            //anim.SetFloat("Speed", isDash ? 2 : lookDirection.sqrMagnitude);

            playerAnim.ChangeAnimationFromFloat(AnimationState.Speed, isDash ? 2 : lookDirection.sqrMagnitude);

            //Debug.Log(lookDirection.sqrMagnitude);
        } else {
            //anim.SetFloat("Speed", 0);

            playerAnim.ChangeAnimationFromFloat(AnimationState.Speed, 0);
            Debug.Log("��~");

            //if (!playerAnim.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("Idle") || !playerAnim.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("Locomotion")) {
            //    playerAnim.ChangeAnimationFromTrigger(AnimationState.Idle);
            //}
        }
    }


    private void OnDisable() {
        vignette.intensity.value = 0;
    }
}