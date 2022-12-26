using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int maxEnemyGenerateCount;

    [SerializeField]
    private float generateInterval;

    [SerializeField]
    private int generateGemCount;

    private EnemyGenerator enemyGenerator;
    private GemGenerator gemGenerator;

    private int enemyGenerateCount;

    private int destroyEnemyCount;


    void Start()
    {
        if(TryGetComponent(out enemyGenerator)) {
            StartCoroutine(PrepareGenerateEnemy());
            StartCoroutine(ObserveEnemyCount());
        }

        if (TryGetComponent(out gemGenerator)) {
            gemGenerator.GenerateGems(generateGemCount);
        }
    }

    /// <summary>
    /// エネミーの生成準備
    /// </summary>
    /// <returns></returns>
    private IEnumerator PrepareGenerateEnemy() {

        float timer = 0;

        while (maxEnemyGenerateCount > enemyGenerateCount) {
            timer += Time.deltaTime;
            if (generateInterval <= timer) {
                timer = 0;
                enemyGenerateCount++;
                enemyGenerator.GenerateEnemy();
            }           
            yield return null;
        }
        Debug.Log("エネミーの生成終了");
    }

    /// <summary>
    /// エネミーの倒された数を監視
    /// </summary>
    /// <returns></returns>
    private IEnumerator ObserveEnemyCount() {

        while (maxEnemyGenerateCount > destroyEnemyCount) {

            // TODO ゲームステートによる一時停止機能を追加

            yield return null;
        }
        Debug.Log("ゲームクリア");
    }

    /// <summary>
    /// エネミーの倒した数を加算
    /// </summary>
    public void AddDestroyEnemyCount() {
        destroyEnemyCount++;
    }
}
