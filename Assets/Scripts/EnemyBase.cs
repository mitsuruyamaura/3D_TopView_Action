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
    /// 初期設定
    /// </summary>
    /// <param name="gameManager"></param>
    public void SetUpEnemyBase(GameManager gameManager) {   // TODO エネミーのデータを貰う
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
    /// Hp の計算処理の準備
    /// </summary>
    /// <param name="amount"></param>
    public void PrepareCalcHp(int amount) {
        // Hp の計算と生存確認
        if (enemyHealth.CalcHp(amount)) {
            // 倒されている場合には、destroyCount を加算
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
