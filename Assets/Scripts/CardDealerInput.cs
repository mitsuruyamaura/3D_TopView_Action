using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class CardDealerInput : MonoBehaviour
{
    public Button drawButton;
    public ReactiveProperty<int> cardResult = new ();

    [SerializeField]
    private int cardCount = 52;
    
    private CardDeck deck;

    private void Start()
    {
        deck = new CardDeck(cardCount);

        drawButton.OnClickAsObservable().Subscribe(_ =>
        {
            // カードを引く
            deck.DrawCard(cardResult);
        }).AddTo(this);
    }
}
