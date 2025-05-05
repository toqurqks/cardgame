using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using toqurqksProductions;
using TMPro;

/*
public class DrawpileManager : MonoBehaviour
{
    public List<Card> drawPile = new List<Card>();
    public int startingHandSize = 6;

    private int currentIndex = 0;
    public int maxHandSize;
    public int currentHandSize;
    private HandManager handManager;
    //private DiscardManager discardManager;
    public TextMeshProUGUI drawPileCounter;

    void Start()
    {
        handManager = FindFirstObjectByType<HandManager>();

    }

    private void Update()
    {
        if (handManager != null)
        {
            currentHandSize = handManager.cardsInHand.Count;
        }
    }

    public void DrawCard(HandManager handManager)
    {
        if (drawPile.Count == 0)
            return;

        if (currentHandSize < maxHandSize)
        {
            Card nextCard = drawPile[currentIndex];
            handManager.AddCardToHand(nextCard);
            currentIndex = (currentIndex + 1) % drawPile.Count;
        }
    }
}
   
*/
