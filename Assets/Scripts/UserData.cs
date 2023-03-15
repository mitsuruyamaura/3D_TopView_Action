using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public static UserData instance;

    public int money;
    
    public CharaData currentCharaData;

    [SerializeField] 
    private int currentCharaId;
    

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }


    public void SetCurrentCharaData() {

        currentCharaData = DataBaseManager.instance.GetCharaData(currentCharaId);
    }
}
