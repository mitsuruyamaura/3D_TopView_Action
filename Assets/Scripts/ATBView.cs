using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ATBView : MonoBehaviour
{
    [SerializeField] private Image atbGauge;           // ATBゲージのImageコンポーネント
    [SerializeField] private Button attackButton;      // 攻撃ボタンのButtonコンポーネント
    
    // クリックイベントを外部クラスで購読できる
    public IObservable<Unit> OnAttackButtonClick => attackButton.OnClickAsObservable();  //  attackButton.OnClickAsObservable(); のプロパティ

    public Image AtbGauge => atbGauge;   // プロパティ
    public Button AttackButton => attackButton;   // プロパティ
}