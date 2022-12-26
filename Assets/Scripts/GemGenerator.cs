using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemGenerator : MonoBehaviour
{
    public GemDataSO gemDataSO;
    public Transform[] generateGemTrans;

    public List<Gem> gemList = new();


    void Start() {
        // デバッグ用
        //GenerateGems(10);
    }

    /// <summary>
    /// ジェムの生成
    /// </summary>
    /// <param name="count"></param>
    public void GenerateGems(int count) {
        for (int i = 0; i < count; i++) {
            GemData gemData = gemDataSO.gemDataList[Random.Range(0, gemDataSO.gemDataList.Count)];

            Gem gem = Instantiate(gemData.gemPrefab, 
                new(Random.Range(generateGemTrans[0].position.x, generateGemTrans[1].position.x),
                    10, 
                    Random.Range(generateGemTrans[0].position.z, generateGemTrans[1].position.z)),
                gemData.gemPrefab.transform.rotation);

            gemList.Add(gem);
        }
    }
}
