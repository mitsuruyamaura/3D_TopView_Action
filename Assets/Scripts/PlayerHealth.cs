using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int hp;

    
    void Start()
    {
        hp = UserData.instance.currentCharaData != null ? UserData.instance.currentCharaData.maxHp : ConstData.DEFAULT_MAX_HP;
    }
    
    /// Hp ‚ÌŒvŽZ
    /// </summary>
    /// <param name="amount"></param>
    public void CalcHp(int amount) {
        hp = Mathf.Clamp(hp += amount , 0, UserData.instance.currentCharaData.maxHp);
    }


    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.TryGetComponent(out EnemyBase enemyBase)) {

        }
    }
}
