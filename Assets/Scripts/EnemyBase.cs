using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


// SpringJoint + DOTween
// https://teratail.com/questions/270417

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

    [SerializeField]
    private Animator anim;
    private float stateTime = 2.0f;
    private EnemyAiState nextState;
    private Tween tween;


    private void Start() {     

        if (TryGetComponent(out rb)) {
            SetDestination();
        } else {
            Debug.Log("Rigidbody ‚ªŽæ“¾o—ˆ‚Ü‚¹‚ñB");
        }
    }

    private void SetDestination() {
        Vector3 destination = new( transform.position.x + Random.Range(-5.0f, 5.0f), transform.position.y, transform.position.z + Random.Range(-5.0f, 5.0f));
        Debug.Log(destination.sqrMagnitude);
        tween = rb.DOMove(destination, destination.sqrMagnitude / 1000).SetEase(Ease.InQuart).OnComplete(() => StartCoroutine(Wait()));
        transform.parent.LookAt(destination);
        if (anim) {
            anim.SetFloat("Speed", destination.normalized.sqrMagnitude);
        }
    }


    private IEnumerator Wait() {
        anim.SetFloat("Speed", 0.0f);
        float timer = 0;
        if (timer < stateTime) {
            timer += Time.deltaTime;
            yield return null;
        }      
        tween.Kill();
        tween = null;
        SetDestination();
    }
}
