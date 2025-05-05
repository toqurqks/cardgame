using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using toqurqksProductions;
using TMPro;

public class DisCardManager : MonoBehaviour
{
    [SerializeField] public List<Card> discardCards = new List<Card>();
    public TextMeshProUGUI discardCount;
    public int discardCardsCount;
    private int totalDiscardScore = 0;

    private void Awake()
    {
        UpdateDiscardCount();
    }
    /* 리필방식 업데이트
    private void UpdateDiscardCount()
    {
        discardCount.text = discardCards.Count.ToString();
        discardCardsCount = discardCards.Count;
    }*/

    private void UpdateDiscardCount()
    {
        discardCount.text = totalDiscardScore.ToString(); // 이제 점수 합산 표시
        discardCardsCount = discardCards.Count;
    }


    public void AddDiscard(Card card)
    {
        if (card != null)
        {
            discardCards.Add(card);
            totalDiscardScore += card.CardScore;
            UpdateDiscardCount();
        }
    }

    public Card PullFromDiscard()
    {
        if(discardCards.Count > 0)
        {
            Card cardToReturn = discardCards[discardCards.Count - 1];
            discardCards.RemoveAt(discardCards.Count - 1);
            UpdateDiscardCount();
            return cardToReturn;
        }
        else
        {
            return null;
        }
    }


    public bool PullSelectCardFromDiscard(Card card)
    {
        if(discardCards.Count > 0 && discardCards.Contains(card))
        {
            discardCards.Remove(card);
            UpdateDiscardCount();
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<Card> PullAllFromDiscard()
    {
        if (discardCards.Count > 0)
        {
            List<Card> cardsToReturn = new List<Card>(discardCards);
            discardCards.Clear();
            UpdateDiscardCount();
            return cardsToReturn;
        }
        else
        {
            return new List<Card>();
        }
    }
}
