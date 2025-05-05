using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using toqurqksProductions;


public class HandManager : MonoBehaviour
{   
    public GameObject cardPrefab; // cardprefab in inspector
    public Transform handTransform;
    public float fanSpread = 7.5f;
    public float cardSpacing = 100f;
    public float verticalSpacing = 100f;
    public int maxHandSize = 12;
    public List<GameObject> cardsInHand = new List<GameObject>(); // 핸드에 들고있는 카드들


    void Start()
    {

    }

    public void AddCardToHand(Card cardData)
    {
        if(cardsInHand.Count < maxHandSize)
        {
            GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
            cardsInHand.Add(newCard);

            newCard.GetComponent<CardDisplay>().cardData = cardData;
        }
        UpdateHandVisuals();
    }
    public void AddCardToHand()
    {
        //멀리건
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        cardsInHand.Add(newCard);

        UpdateHandVisuals();
    }

    void Update()
    {
       //UpdateHandVisuals();
    }

    public void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;

        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }
     
        
        
        for (int i=0; i <  cardCount; i++)
        {
            float roatationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, roatationAngle);

            float horizentaloffset = (cardSpacing * (i - (cardCount - 1) / 2f));

            float normalizedPosition = (2f * i / (cardCount - 1) - 1f); // 카드위치 Nomalize , -1 ~ 1
            float verticaloffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition);
            
           
            // 카드 위치 세팅
            cardsInHand[i].transform.localPosition = new Vector3(horizentaloffset, verticaloffset, 0f);
        }
    }


}
