using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int hp;

    private PlayerAnimation playerAnim;
    
    void Start()
    {
        TryGetComponent(out playerAnim);

        hp = UserData.instance.currentCharaData != null ? UserData.instance.currentCharaData.maxHp : ConstData.DEFAULT_MAX_HP;
    }
    
    /// Hp の計算
    /// </summary>
    /// <param name="amount"></param>
    public void CalcHp(int amount) {
        hp = Mathf.Clamp(hp += amount , 0, UserData.instance.currentCharaData.maxHp);

        if (hp <= 0) {
            Debug.Log("Game Over");

            // ダウンアニメ再生
            playerAnim.ChangeAnimationFromTrigger(AnimationState.Down);
        }

        // ダメージアニメ再生
        playerAnim.ChangeAnimationFromTrigger(AnimationState.Damage);

        // SE

    }


    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.TryGetComponent(out EnemyBase enemyBase)) {
            CalcHp(enemyBase.GetEnemyCharaData().attackPower);
        }
    }
}
