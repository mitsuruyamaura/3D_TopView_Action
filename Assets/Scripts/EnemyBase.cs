using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private EnemyAiState aiState = EnemyAiState.Wait;

    [SerializeField]
    private EnemyData enemyData;

    [SerializeField]
    private float shootDistance;

    private Animator anim;
    private EnemyHealth enemyHealth;
    private CharaData enemyCharaData;
    private GameManager gameManager;

    /// <summary>
    /// �����ݒ�
    /// </summary>
    /// <param name="gameManager"></param>
    public void SetUpEnemyBase(GameManager gameManager) {   // TODO �G�l�~�[�̃f�[�^��Ⴄ
        this.gameManager = gameManager;
        TryGetComponent(out anim);

        if (transform.TryGetComponent(out enemyHealth)) {
            enemyHealth.SetUpHp(enemyData.maxHp, anim);
        }
    }

    //private void Start() {
    //    if (transform.TryGetComponent(out enemyHealth)) {
    //        enemyHealth.SetUpHp(enemyData.maxHp, anim);
    //    }
    //}

    /// <summary>
    /// Hp �̌v�Z�����̏���
    /// </summary>
    /// <param name="amount"></param>
    public void PrepareCalcHp(int amount) {
        // Hp �̌v�Z�Ɛ����m�F
        if (enemyHealth.CalcHp(amount)) {
            // �|����Ă���ꍇ�ɂ́AdestroyCount �����Z
            gameManager.AddDestroyEnemyCount();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public CharaData GetEnemyCharaData() {
        return enemyCharaData;
    }
}
