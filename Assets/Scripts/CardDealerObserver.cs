using UnityEngine;
using UniRx;

public class CardDealerObserver : MonoBehaviour
{
    private CardDealerInput cardDealerInput;

    private void Start()
    {
        if(!TryGetComponent(out cardDealerInput)) {
            Debug.Log("CardDealerInput を取得出来ません");
            return;
        }
        
        // カードの結果を監視する
        cardDealerInput.cardResult.Subscribe(x =>
        {
            Debug.Log($"引いたカード：{x}");
        });
    }
}