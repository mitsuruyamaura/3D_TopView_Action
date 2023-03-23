using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ATBModel_Mono : MonoBehaviour
{
    public ReactiveProperty<float> CurrentATB;

    public float MaxATB { get; private set; }
    public float RecoveryRate { get; private set; }
    public float AttackThreshold { get; private set; }

    
    // 外部から実行するトリガーメソッド
    public void SetTriggerATB(float maxATB, float recoveryRate, float attackThreshold)
    {
        CurrentATB = new ReactiveProperty<float>(0f);
        MaxATB = maxATB;
        RecoveryRate = recoveryRate;
        AttackThreshold = attackThreshold;

        // ATBを回復するストリーム
        this.UpdateAsObservable()
            .Where(_ => CurrentATB.Value < MaxATB)
            .Subscribe(_ => CurrentATB.Value += RecoveryRate * Time.deltaTime);
    }

    // ATB値をリセットする
    public void ResetATB()
    {
        CurrentATB.Value = 0f;
    }
}
