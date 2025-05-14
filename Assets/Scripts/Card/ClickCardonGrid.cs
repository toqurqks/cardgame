using toqurqksProductions;
using UnityEngine;

public class ClickCardonGrid : MonoBehaviour
{
    public CardUIPanel cardPanel; // �ν����Ϳ��� �г� �巡��

    private Card card;

    private void Start()
    {
        card = GetComponent<Card>(); // Ŭ���� ������Ʈ���� Card ��ũ��Ʈ ����
    }

    private void OnMouseDown()
    {
        if (card != null)
        {
            cardPanel.SetCard(card);
        }
        else
        {
            Debug.LogWarning("ī�� ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }
}
