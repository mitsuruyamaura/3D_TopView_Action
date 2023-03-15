using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    [SerializeField]
    private CharaDataSO charaDataSo;

    [SerializeField] 
    private SkinDataSO skinDataSo;

    
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

    /// <summary>
    /// ID で指定したキャラ・プレファブの取得
    /// </summary>
    /// <param name="searchId"></param>
    /// <returns></returns>
    public PlayerController GetSkinData(int searchId) {
        return skinDataSo.skinDataList.Find(x => x.id == searchId).charaPrefab;
    }
}
