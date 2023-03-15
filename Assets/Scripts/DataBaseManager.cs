using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    [SerializeField]
    private CharaDataSO charaDataSo;

    
    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }   
    }

    /// <summary>
    /// ID で指定した CharaData の取得
    /// </summary>
    /// <param name="searchId"></param>
    /// <returns></returns>
    public CharaData GetCharaData(int searchId) {
        return charaDataSo.charaDataList.Find(x => x.id == searchId);
    }
}
