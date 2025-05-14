using TMPro;
using toqurqksProductions;
using UnityEngine;
using UnityEngine.UI;

public class CardUIPanel : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text attackText;
    public TMP_Text healthText;

    public Button attackButton;
    public Button moveButton;

    private Card currentCard;

    public void SetCard(Card card)
    {
        if (card == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true); // 패널 표시
        currentCard = card;

        nameText.text = card.cardName;
        attackText.text = $"ATK: {card.damage}";
        healthText.text = $"HP: {card.Durability}";

        attackButton.gameObject.SetActive(true);
        moveButton.gameObject.SetActive(true);
    }

    public void HandleAttack()
    {
        Debug.Log($"{currentCard.cardName} attacks!");
        // TODO: 공격 로직 호출
    }

    public void HandleMove()
    {
        Debug.Log($"{currentCard.cardName} moves!");
        // TODO: 이동 로직 호출
    }

    public void ClosePanel()
    {
        currentCard = null;
        gameObject.SetActive(false);
    }
}