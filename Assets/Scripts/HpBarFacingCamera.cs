using UnityEngine;
using DG.Tweening;

public class HpBarFacingCamera : MonoBehaviour
{
    void LateUpdate() {
        // HpBar用Canvasの向きを常にカメラの方向に向ける
        LookHpBarToCamera();
    }

    /// <summary>
    /// HpBar用Canvasの向きを常にカメラの方向に向ける
    /// </summary>
    private void LookHpBarToCamera() {
        // オブジェクトの回転をカメラの回転と完全に一致させる
        transform.rotation = Camera.main.transform.rotation;

        // DOTweenを使用し、オブジェクトをカメラの位置に向ける
        // 最終結果はカメラの方向を向くが、回転の完全一致ではなく、カメラの位置に向かう(正対する)形になる
        // そのため、オブジェクトの回転がカメラの回転と一致するわけではないが、オブジェクトの前面がカメラに向くようになる
        transform.DOLookAt(Camera.main.transform.position, 0);

        // カメラの方向を向く(DOTween ではない場合)
        // Camera.main.transform.rotation * Vector3.forward はカメラの前方向ベクトルを計算
        // Camera.main.transform.rotation * Vector3.up はカメラの上方向ベクトルを計算
        // オブジェクトはカメラに正対し、カメラの方向に向くが、DOTween と同じでオブジェクトの回転がカメラの回転と完全に一致するわけではない
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);
    }
}