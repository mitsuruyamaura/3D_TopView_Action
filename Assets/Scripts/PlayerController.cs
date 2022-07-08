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

        moveSpeed = UserData.instance != null ? UserData.instance.currentCharaData.moveSpeed : ConstData.DEFAULT_MOVE_SPEED;
    }

    void Update() {
        // キー入力判定
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate() {

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

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

        // 移動する方向と移動アニメの同期
        SyncMoveAnimation();
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
            anim.SetFloat("Speed", isDash ? 2 : lookDirection.sqrMagnitude);

            //Debug.Log(lookDirection.sqrMagnitude);
        } else {
            anim.SetFloat("Speed", 0);
        }
    }
}
