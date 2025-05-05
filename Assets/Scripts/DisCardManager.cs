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

    private void Awake()
    {
        UpdateDiscardCount();
    }

    private void UpdateDiscardCount()
    {
        discardCount.text = discardCards.Count.ToString();
        discardCardsCount = discardCards.Count;
    }

}
