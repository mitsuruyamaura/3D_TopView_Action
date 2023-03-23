using UnityEngine;
using UniRx;
using DG.Tweening;

public class ATBPresenter : MonoBehaviour
{
    [SerializeField] private float maxATB = 100f;
    [SerializeField] private float recoveryRate = 10f;
    [SerializeField] private float attackThreshold = 80f;

    private ATBModel model;
    private ATBModel_Mono modelMono;
    private ATBView view;

    private void Awake()
    {
        TryGetComponent(out view);
        
        // 通常のクラスの場合
        model = new ATBModel(maxATB, recoveryRate, attackThreshold);
        
        // MonoBehaviour 継承クラスの場合
        if(!TryGetComponent(out model)) return;
        modelMono.SetTriggerATB(maxATB, recoveryRate, attackThreshold);
    }

    private void Start()
    {
        // 攻撃ボタンがクリックされたらATB値をリセットする
        view.OnAttackButtonClick
            .Subscribe(_ => model.ResetATB())
            .AddTo(view);

        // 攻撃ボタンの状態を更新するストリーム
        model.CurrentATB
            .Select(atb => atb >= model.AttackThreshold)
            .SubscribeToInteractable(view.AttackButton)
            .AddTo(view);

        // ATBゲージの表示を更新するストリーム
        model.CurrentATB
            .Subscribe(atb => view.AtbGauge.fillAmount = atb / model.MaxATB)
            // .Subscribe(atb =>
            // {
            //     var fillAmount = atb / model.MaxATB;
            //     view.AtbGauge.DOFillAmount(fillAmount, 0.05f).SetEase(Ease.Linear);
            // })
            .AddTo(view);
    }
}