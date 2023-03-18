using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.PostFX;
using UnityEngine.Rendering.PostProcessing;

// AnimationClip にタグを設定
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
    /// 初期設定
    /// </summary>
    /// <param name="camera"></param>
    public void SetUpPlayer(CameraControllerFromCinemachine camera) {

        camera.TryGetComponent(out postProcessing);
        //postProcessing = camera.GetComponent<CinemachinePostProcessing>();

        TryGetComponent(out rb);
        //TryGetComponent(out anim);

        TryGetComponent(out playerAnim);

        // 通常の取得方法
        //vignette = postProcessing.m_Profile.GetSetting<Vignette>();

        if (!postProcessing.m_Profile.TryGetSettings(out vignette)) {
            Debug.Log("Vignette 取得出来ません。");
        } 

        moveSpeed = UserData.instance != null ? UserData.instance.currentCharaData.moveSpeed : ConstData.DEFAULT_MOVE_SPEED;
    }
    
    // void Start() {
    //     TryGetComponent(out rb);
    //     //TryGetComponent(out anim);
    //
    //     TryGetComponent(out playerAnim);
    //
    //     // 通常の取得方法
    //     //vignette = postProcessing.m_Profile.GetSetting<Vignette>();
    //
    //     if (!postProcessing.m_Profile.TryGetSettings(out vignette)) {
    //         Debug.Log("Vignette 取得出来ません。");
    //     } 
    //
    //     moveSpeed = UserData.instance != null ? UserData.instance.currentCharaData.moveSpeed : ConstData.DEFAULT_MOVE_SPEED;
    // }

    void Update() {
        
        if(!playerAnim) return;

        // Attack タグの設定されているアニメ再生中の場合
        if (playerAnim.GetAnimator().GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
            //Debug.Log("移動停止。移動のキー入力停止");
            // 移動停止。移動のキー入力停止
            horizontal = 0;
            vertical = 0;
            rb.velocity = Vector3.zero;
            return;
        }

        // キー入力判定
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        isDash = Input.GetKey(KeyCode.LeftShift);

        if (vignette) {
            vignette.intensity.value = isDash ? 0.5f : 0;
        }

        // 移動する方向と移動アニメの同期
        SyncMoveAnimation();
    }

    void FixedUpdate() {

        if (!rb) return;
        
        // 向きを考慮しない移動方法。移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        //rb.velocity = new (inputHorizontal * moveSpeed, rb.velocity.y, inputVertical * moveSpeed);

        // 移動
        MovePlayer();
    }

    /// <summary>
    /// プレイヤーの移動
    /// </summary>
    private void MovePlayer() {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * vertical + Camera.main.transform.right * horizontal;

        float speed = isDash ? moveSpeed * 2.0f : moveSpeed;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = moveForward * speed + new Vector3(0, rb.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    /// <summary>
    /// 移動する方向と移動アニメの同期
    /// </summary>
    private void SyncMoveAnimation() {

        if (!Mathf.Approximately(horizontal, 0.0f) || !Mathf.Approximately(vertical, 0.0f)) {
            lookDirection.Set(horizontal, vertical);
            lookDirection.Normalize();

            //anim.SetFloat("Look X", lookDirection.x);
            //anim.SetFloat("Look Y", lookDirection.y);

            // ダッシュ有無に応じてアニメの再生速度を調整
            //anim.SetFloat("Speed", isDash ? 2 : lookDirection.sqrMagnitude);

            playerAnim.ChangeAnimationFromFloat(AnimationState.Speed, isDash ? 2 : lookDirection.sqrMagnitude);

            //Debug.Log(lookDirection.sqrMagnitude);
        } else {
            //anim.SetFloat("Speed", 0);

            playerAnim.ChangeAnimationFromFloat(AnimationState.Speed, 0);
            Debug.Log("停止");

            //if (!playerAnim.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("Idle") || !playerAnim.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("Locomotion")) {
            //    playerAnim.ChangeAnimationFromTrigger(AnimationState.Idle);
            //}
        }
    }


    private void OnDisable() {
        vignette.intensity.value = 0;
    }
}