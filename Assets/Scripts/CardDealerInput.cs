using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class CardDealerInput : MonoBehaviour
{
    public Button drawButton;
    public ReactiveProperty<int> cardResult = new ();

    private CardDeck deck;

    private void Start()
    {
        deck = new CardDeck();

        drawButton.OnClickAsObservable().Subscribe(_ =>
        {
            // カードを引く
            deck.DrawCard(cardResult);
        }).AddTo(this);
    }
}
