using UniRx;
using UnityEngine;

public class ATBModel
{
    public ReactiveProperty<float> CurrentATB { get; private set; }
    private readonly CompositeDisposable disposables = new ();
    
    public float MaxATB { get; private set; }
    public float RecoveryRate { get; private set; }
    public float AttackThreshold { get; private set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="maxATB"></param>
    /// <param name="recoveryRate"></param>
    /// <param name="attackThreshold"></param>
    public ATBModel(float maxATB, float recoveryRate, float attackThreshold)
    {
        CurrentATB = new ReactiveProperty<float>(0f);
        MaxATB = maxATB;
        RecoveryRate = recoveryRate;
        AttackThreshold = attackThreshold;
        
        // ATBを回復するストリーム
        Observable.EveryUpdate()
            .Where(_ => CurrentATB.Value < MaxATB)
            .Subscribe(_ => CurrentATB.Value += RecoveryRate * Time.deltaTime)
            .AddTo(disposables);  // ストリームが生成された時に自動的に CompositeDisposable に追加
    }
    
    /// <summary>
    /// ATB値をリセットする
    /// </summary>
    public void ResetATB()
    {
        CurrentATB.Value = 0f;
    }
    
    /// <summary>
    /// オブジェクト(インスタンス)が破棄されるときにCompositeDisposableのDisposeも呼び出す
    /// </summary>
    public void Dispose()
    {
        // Dispose メソッドが呼ばれたときに、CompositeDisposable に含まれる全てのストリームが自動的に解放
        disposables.Dispose();
    }
}