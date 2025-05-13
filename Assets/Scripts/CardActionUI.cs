using UnityEngine;

public class CardActionUI : MonoBehaviour
{
    private GameObject targetCard;

    public void Initialize(GameObject card)
    {
        targetCard = card;
    }

    public void OnAttackButtonPressed()
    {
        Debug.Log($"공격 실행: {targetCard.name}");
        // 공격 처리 로직 호출
    }

    public void OnMoveButtonPressed()
    {
        Debug.Log($"move: {targetCard.name}");
        // 효과 발동 로직 호출
    }

    public void OnCancelButtonPressed()
    {
        Destroy(this.gameObject);
    }
}
