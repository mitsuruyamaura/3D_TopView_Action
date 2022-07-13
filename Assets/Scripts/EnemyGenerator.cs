using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyBase enemyPrefab;

    [SerializeField]
    private Transform[] generateTrans;

    private bool isGenerateEnd;

    /// <summary>
    /// エネミーの生成
    /// </summary>
    /// <returns></returns>
    public EnemyBase GenerateEnemy() {

        EnemyBase enemy = Instantiate(enemyPrefab, generateTrans[Random.Range(0, generateTrans.Length)].position, Quaternion.identity);

        // TODO EnemyBase の SetUp する

        return enemy;
    }
}
