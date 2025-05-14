using toqurqksProductions;
using UnityEngine;

public class ClickCardonGrid : MonoBehaviour
{
    public CardUIPanel cardPanel; // 인스펙터에서 패널 드래그

    private Card card;

    private void Start()
    {
        card = GetComponent<Card>(); // 클릭한 오브젝트에서 Card 스크립트 참조
    }

    private void OnMouseDown()
    {
        if (card != null)
        {
            cardPanel.SetCard(card);
        }
        else
        {
            Debug.LogWarning("카드 컴포넌트를 찾을 수 없습니다.");
        }
    }
}
