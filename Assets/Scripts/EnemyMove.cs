using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// SpringJoint + DOTween
// https://teratail.com/questions/270417

// DOLookAt ???\?b?h
// https://qiita.com/BEATnonanka/items/b4cca6471e77466cec74

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float moveTime = 5.0f;

    private Rigidbody rb;
    private Animator anim;
    private float stateTime = 2.0f;
    private Tween tween;
    private string speedParam = "Speed";


    private void Start() {
        if(!transform.parent.TryGetComponent(out anim)) {
            Debug.Log("親オブジェクトの Animator が取得できません");
        }

        // TODO SetUp ??????AMoveSpeed ?? Animator ????????????

        if (TryGetComponent(out rb)) {
            SetDestination();
        } else {
            Debug.Log("Rigidbody が取得できません");
        }
    }

    /// <summary>
    /// ??W?n?_??Z?b?g
    /// </summary>
    private void SetDestination() {
        Vector3 destination = new(transform.position.x + Random.Range(-5.0f, 5.0f), transform.position.y, transform.position.z + Random.Range(-5.0f, 5.0f));
        //Debug.Log(destination.sqrMagnitude);

        Move(destination);
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="destination"></param>
    private void Move(Vector3 destination) {
        tween = rb.DOMove(destination, moveTime).SetEase(Ease.InQuart).OnComplete(() => StartCoroutine(Wait()));
        transform.parent.DOLookAt(destination, 1.0f).SetEase(Ease.Linear);
        if (anim) {
            anim.SetFloat(speedParam, destination.normalized.sqrMagnitude);
            //Debug.Log(destination.normalized.sqrMagnitude);
        }
    }

    /// <summary>
    /// ??@
    /// </summary>
    /// <returns></returns>
    private IEnumerator Wait() {
        if (anim) {
            anim.SetFloat(speedParam, 0.0f);
        }

        tween.Kill();
        tween = null;

        yield return new WaitForSeconds(stateTime);
        SetDestination();
    }
}
