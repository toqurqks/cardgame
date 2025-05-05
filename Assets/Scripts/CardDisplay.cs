using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using toqurqksProductions;


public class CardDisplay : MonoBehaviour
{
    public Card cardData;

    public Image cardImage;

    public TMP_Text nameText;

    public TMP_Text durabilityText;

    public TMP_Text damageText;

    public TMP_Text cardScoreText;

    public Image[] typeimages;

    private Color[] cardColors =
    {
        Color.gray, //Unit
        new Color (0.3f, 0.6f, 0,38f), //Tower
        new Color (0.28f, 0.37f, 0.53f)  //Action
    };

    private Color[] typeColors =
    {
        new Color(0.47f, 0f, 0,4f), //Unit
        Color.green, //Tower
        Color.blue, //Action
    };

    void Start()
    {
        UpdateCardDisplay();

    }
    

    public void UpdateCardDisplay()
    {
        //카드 속성에 따른 card image color 업데이트
        cardImage.color = cardColors[(int)cardData.cardType[0]];

        nameText.text = cardData.cardName;
        durabilityText.text = cardData.Durability.ToString();
        damageText.text = $"{cardData.damage}";
        cardScoreText.text = cardData.CardScore.ToString();

        // type가 여러개일 경우를 처리하는 반복문
        // ex) 유닛이면서 타워
        // 없어도 되지않나?
        for (int i = 0; i < typeimages.Length; i++)
        {
            if (i < cardData.cardType.Count)
            {
                if(i < cardData.cardType.Count)
                {
                    typeimages[i].gameObject.SetActive(true);
                    typeimages[i].color = typeColors[(int)cardData.cardType[i]];
                }
                else
                {
                    typeimages[i].gameObject.SetActive(false);

                }
            }
        }
    }

}
