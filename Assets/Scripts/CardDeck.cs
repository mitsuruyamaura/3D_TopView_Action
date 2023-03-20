using UnityEngine;
using UniRx;

public class CardDeck
{
    private ReactiveCollection<int> cards = new ();

    public CardDeck()
    {
        // カードのデッキを初期化する
        for (int i = 0; i < 52; i++)
        {
            cards.Add(i + 1);
        }
    }

    public void DrawCard(ReactiveProperty<int> cardResult)
    {
        if (cards.Count > 0)
        {
            // カードをランダムに選び、取り出す
            int index = Random.Range(0, cards.Count);
            int card = cards[index];
            cards.RemoveAt(index);

            // カードの結果を設定する
            cardResult.Value = card;
        }
        else
        {
            Debug.Log("カードが残っていません");
        }
    }
}