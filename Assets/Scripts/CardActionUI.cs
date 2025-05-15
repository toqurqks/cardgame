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
        Debug.Log($"���� ����: {targetCard.name}");
        // ���� ó�� ���� ȣ��
    }

    public void OnMoveButtonPressed()
    {
        Debug.Log($"move: {targetCard.name}");
        // ȿ�� �ߵ� ���� ȣ��
    }

    public void OnCancelButtonPressed()
    {
        Destroy(this.gameObject);
    }
}
