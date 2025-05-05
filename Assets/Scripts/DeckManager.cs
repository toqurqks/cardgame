using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using toqurqksProductions;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();

    public int startingHandSize = 3;

    public int maxHandSize = 3;
    public int currentHandSize;
    private HandManager handManager;
    private DrawpileManager drawPileManager;
    private bool startBattleRun = true;

    void Start()
    {
        //���ҽ� �������� ��ü ī�� �ε�
        Card[] cards = Resources.LoadAll<Card>("Cards");

        //Add the loaded cards to ther allCards list
        allCards.AddRange(cards);
    }

    void Awake()
    {
       if(drawPileManager == null)
       {
            drawPileManager = FindFirstObjectByType<DrawpileManager>();

       }
       if(handManager == null)
       {
            handManager = FindFirstObjectByType<HandManager>();
       }
    }

    void Update()
    {
        if(startBattleRun)
        {
            BattleSetup();
        }
       
    }

    public void BattleSetup()
    {
        handManager.BattleSetup(maxHandSize);
        drawPileManager.MakeDrawPile(allCards);
        drawPileManager.BattleSetup(startingHandSize, maxHandSize);
        startBattleRun = false;
    }
}

