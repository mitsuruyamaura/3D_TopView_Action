using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int hp;
    private int maxHp;
    private Animator anim;

    /// <summary>
    /// �����ݒ�
    /// </summary>
    /// <param name="maxHp"></param>
    public void SetUpHp(int maxHp, Animator animator) {
        this.maxHp = maxHp;
        anim = animator;
        hp = this.maxHp == 0 ? 3 : this.maxHp;
    }

    /// <summary>
    /// Hp �̌v�Z�Ɛ����m�F
    /// </summary>
    /// <param name="amount"></param>
    public bool CalcHp(int amount) {
        //hp = Mathf.Min(hp + amount, enemyData.maxHp);

        hp += amount;
        Debug.Log("�c��Hp : " + hp);

        // �c HP �m�F
        if (hp <= 0) {
            // �A�j��
            anim.SetTrigger("Death");

            Destroy(gameObject, 1.5f);

            // �G�t�F�N�g

            // SE
            return true;
        } else {
            anim.SetTrigger("Damage");

            // �G�t�F�N�g

            // SE

            return false;
        }
    }
}
