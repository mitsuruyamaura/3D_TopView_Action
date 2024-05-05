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
        if(!TryGetComponent(out view)) return;
        
        // 通常のクラスの場合
        model = new ATBModel(maxATB, recoveryRate, attackThreshold);
        Debug.Log("Model 初期設定完了");
        
        // MonoBehaviour 継承クラスの場合
        if(!TryGetComponent(out modelMono)) return;
        modelMono.SetTriggerATB(maxATB, recoveryRate, attackThreshold);
        
        Debug.Log("クラスの取得・初期設定完了");
    }

    private void Start()
    {
        // 攻撃ボタンがクリックされたらATB値をリセットする
        view.OnAttackButtonClick
            .Subscribe(_ => model.ResetATB())
            .AddTo(this);

        // 攻撃ボタンの状態を更新するストリーム
        model.CurrentATB
            .Select(atb => atb >= model.AttackThreshold)
            .SubscribeToInteractable(view.AttackButton)
            .AddTo(this);

        // ATBゲージの表示を更新するストリーム
        model.CurrentATB
            .Subscribe(atb => view.AtbGauge.fillAmount = atb / model.MaxATB)
            
            // DOTween の場合。こちらも正常に動作する
            // .Subscribe(atb =>
            // {
            //     var fillAmount = atb / model.MaxATB;
            //     view.AtbGauge.DOFillAmount(fillAmount, 0.01f).SetEase(Ease.Linear);
            // })
            .AddTo(this);
        
        Debug.Log("ストリームの設定完了");
    }
}