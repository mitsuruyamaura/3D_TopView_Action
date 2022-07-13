using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int hp;
    private int maxHp;
    private Animator anim;

    /// <summary>
    /// 初期設定
    /// </summary>
    /// <param name="maxHp"></param>
    public void SetUpHp(int maxHp, Animator animator) {
        this.maxHp = maxHp;
        anim = animator;
        hp = this.maxHp == 0 ? 3 : this.maxHp;
    }

    /// <summary>
    /// Hp の計算と生存確認
    /// </summary>
    /// <param name="amount"></param>
    public bool CalcHp(int amount) {
        //hp = Mathf.Min(hp + amount, enemyData.maxHp);

        hp += amount;
        Debug.Log("残りHp : " + hp);

        // 残 HP 確認
        if (hp <= 0) {
            // アニメ
            anim.SetTrigger("Death");

            Destroy(gameObject, 1.5f);

            // エフェクト

            // SE
            return true;
        } else {
            anim.SetTrigger("Damage");

            // エフェクト

            // SE

            return false;
        }
    }
}
