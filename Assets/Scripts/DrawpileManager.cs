using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using toqurqksProductions;
using TMPro;

public class DrawpileManager : MonoBehaviour
{
    public List<Card> drawPile = new List<Card>();
    public int startingHandSize = 3;

    private int currentIndex = 0;

    public int maxHandSize;

    public int currentHandSize;
    private HandManager handManager;
    private DisCardManager discardManager;
    public TextMeshProUGUI drawPileCounter;

    void Start()
    {
        handManager = FindFirstObjectByType<HandManager>();

    }

    void Update()
    {
        if (handManager != null)
        {
            currentHandSize = handManager.cardsInHand.Count;
        }
    }

    public void MakeDrawPile(List<Card> cardsToAdd)
    {
        drawPile.AddRange(cardsToAdd);
        Utility.Shuffle(drawPile);
        UpdateDrawPileCount();
    }

    public void BattleSetup(int numberOfCardsToDraw, int setMaxHandSize)
    {
        maxHandSize = setMaxHandSize;
        for (int i = 0; i < numberOfCardsToDraw; i++)
        {
            DrawCard(handManager);
        }
    }

    public void DrawCard(HandManager handManager)
    {
        if (drawPile.Count == 0)
        {
            RefillDeckFromDiscard();
            //영상에서는 슬더스라서 덱 다쓰면 다시 리빌딩 하지만 우리는 패배로 판정
            //return;
        }

        if (currentHandSize < maxHandSize)
        {
            Card nextCard = drawPile[currentIndex];
            handManager.AddCardToHand(nextCard);
            drawPile.RemoveAt(currentIndex);
            UpdateDrawPileCount();
            if (drawPile.Count > 0) currentIndex %= drawPile.Count;
            
        }
    }

    private void RefillDeckFromDiscard()
    {
        if(discardManager == null)
        {
            discardManager = FindFirstObjectByType<DisCardManager>();
        }

        if(discardManager != null && discardManager.discardCardsCount > 0)
        {
            drawPile = discardManager.PullAllFromDiscard();
            Utility.Shuffle(drawPile);
            currentIndex = 0; 
        }
    }
    private void UpdateDrawPileCount()
    {
        drawPileCounter.text = drawPile.Count.ToString();
    }
}

