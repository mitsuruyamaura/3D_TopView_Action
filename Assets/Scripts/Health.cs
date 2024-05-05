using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHp;

    private int currentHp;

    [SerializeField]
    private Slider healthSlider; // HPを表示するSlider

    private Camera mainCamera; // メインカメラ


    void Start() {
        
        // Hp の初期化
        InitialHealth(maxHp);

        mainCamera = Camera.main;
    }

    /// <summary>
    /// Hp の初期化
    /// </summary>
    /// <param name="initialHp"></param>
    public void InitialHealth(int initialHp)
    {
        maxHp = initialHp;
        currentHp = maxHp;

        // Hp 用のスライダー設定
        healthSlider.maxValue = currentHp;
        healthSlider.value = currentHp;
    }

    /// <summary>
    /// ダメージ用
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        currentHp = Mathf.Max(currentHp - damage, 0);

        // HPが変動するアニメーション
        healthSlider.DOValue(currentHp, 0.5f);

        // Hp の計算と生存確認
        if (currentHp <= 0) {

            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// 回復用
    /// </summary>
    /// <param name="recoveryPoint"></param>
    public void Heal(int recoveryPoint)
    {
        currentHp = Mathf.Min(currentHp + recoveryPoint, maxHp);
    }
    
    /// <summary>
    /// 兼用
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeHealth(int amount)
    {
        currentHp = Mathf.Clamp(currentHp + amount, 0, maxHp);
    }

    void LateUpdate() {

        // カメラの方向を向く
        healthSlider.transform.DOLookAt(mainCamera.transform.position, 0);

        // カメラの方向を向く(DOTween ではない場合)
        //healthSlider.transform.LookAt(healthSlider.transform.position + mainCamera.transform.rotation * Vector3.forward,
        //    mainCamera.transform.rotation * Vector3.up);
    }
}