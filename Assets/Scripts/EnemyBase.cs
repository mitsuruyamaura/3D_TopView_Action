using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


// SpringJoint + DOTween
// https://teratail.com/questions/270417

// DOLookAt メソッド
// https://qiita.com/BEATnonanka/items/b4cca6471e77466cec74

[RequireComponent(typeof(Rigidbody))]
public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private float moveTime;

    [SerializeField]
    private float shootDistance;

    [SerializeField]
    private EnemyAiState aiState = EnemyAiState.Wait;

    private Rigidbody rb;
    private Animator anim;
    private float stateTime = 2.0f;
    private Tween tween;


    private void Start() {
        transform.parent.TryGetComponent(out anim);

        if (TryGetComponent(out rb)) {
            SetDestination();
        } else {
            Debug.Log("Rigidbody が取得出来ません。");
        }
    }

    /// <summary>
    /// 目標地点のセット
    /// </summary>
    private void SetDestination() {
        Vector3 destination = new(transform.position.x + Random.Range(-5.0f, 5.0f), transform.position.y, transform.position.z + Random.Range(-5.0f, 5.0f));
        //Debug.Log(destination.sqrMagnitude);
        tween = rb.DOMove(destination, moveTime).SetEase(Ease.InQuart).OnComplete(() => StartCoroutine(Wait()));
        transform.parent.DOLookAt(destination, 1.0f).SetEase(Ease.Linear);
        if (anim) {
            anim.SetFloat("Speed", destination.normalized.sqrMagnitude);
            //Debug.Log(destination.normalized.sqrMagnitude);
        }
    }

    /// <summary>
    /// 待機
    /// </summary>
    /// <returns></returns>
    private IEnumerator Wait() {
        anim.SetFloat("Speed", 0.0f);
        float timer = 0;
        if (timer < stateTime) {
            timer += Time.deltaTime;
            yield return null;
        }      
        tween.Kill();
        tween = null;

        yield return new WaitForSeconds(stateTime);
        SetDestination();
    }
}
