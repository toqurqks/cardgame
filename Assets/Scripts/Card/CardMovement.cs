using toqurqksProductions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardState
{
    InHand,
    OnGrid,
    BeingDragged,
    Inactive
}

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private RectTransform canvasRectTransform;
    private Vector3 originalScale;
    private int currentState = 0;
    private Quaternion originalRotation;
    private Vector3 originalPosition;
    private GridManager gridManager;
    public CardState cardState = CardState.InHand;


    [SerializeField] private float selectScale = 1.1f;
    [SerializeField] private Vector2 cardPlay;
    [SerializeField] private Vector3 playPosition;
    [SerializeField] private GameObject glowEffect;
    [SerializeField] private GameObject playArrow;
    [SerializeField] private float lerpFactor = 0.1f;
    [SerializeField] private int cardPlayDivider = 4;
    [SerializeField] private float cardPlayMultiplier = 1f;
    [SerializeField] private bool needUpdateCardPlayPosition = false;
    [SerializeField] private int playPositionYDivider = 2;
    [SerializeField] private float playPositionYMultiplier = 1f;
    [SerializeField] private int playPositionXDivider = 4;
    [SerializeField] private float playPositionXMultiplier = 1f;
    [SerializeField] private bool needUpdatePlayPosition = false;
    




    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas != null)
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }
        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;

        updateCardPlayPosition();
        updatePlayPosition();
        gridManager = FindFirstObjectByType<GridManager>();

    }

    void Update()
    {
        if (needUpdateCardPlayPosition)
        {
            updateCardPlayPosition();
        }

        if (needUpdatePlayPosition)
        {
            updatePlayPosition();
        }

        switch (currentState)
        {
            case 1:
                HandleHoverState();
                break;
            case 2:
                HandleDragState();
                if (!Input.GetMouseButton(0)) // ���콺 ��ư�� ���� ���
                {
                    TransitionToState0();
                }
                break;
            case 3:
                HandlePlayState();
                
                break;

        }
    }

    private void TransitionToState0()
    {
        currentState = 0;
        rectTransform.localScale = originalScale; // scale ����
        rectTransform.localRotation = originalRotation; // rotation ����
        rectTransform.localPosition = originalPosition; // position ����
        glowEffect.SetActive(false); // ����Ʈ ����
        playArrow.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentState == 0)
        {
            originalPosition = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;
            originalScale = rectTransform.localScale;

            currentState = 1;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            
            TransitionToState0();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
{
    if (currentState == 0 && cardState == CardState.OnGrid)
    {
        var display = GetComponent<CardDisplay>();
        var panel = FindFirstObjectByType<CardUIPanel>();

        if (display?.cardData != null && panel != null)
        {
            panel.SetCard(display.cardData);
        }
    }
    else if (currentState == 1)
    {
        currentState = 2;
    }
}

    public void OnDrag(PointerEventData eventData)
    {
        if (currentState == 2)
        {
            if(Input.mousePosition.y > cardPlay.y)
            {
                currentState = 3;
                playArrow.SetActive(true);
                rectTransform.localPosition = Vector3.Lerp(rectTransform.position, playPosition, lerpFactor);
            }
        }

        
    }


    private void HandleHoverState()
    {
        glowEffect.SetActive(true);
        rectTransform.localScale = originalScale * selectScale;


    }

    private void HandleDragState()
    {
        // ī���� ��ġ�� ����ġ
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.position = Vector3.Lerp(rectTransform.position, Input.mousePosition, lerpFactor);

    }
    

    private void HandlePlayState()
    {
        rectTransform.localPosition = playPosition; // ���� ��ġ ����
        rectTransform.localRotation = Quaternion.identity;

        if (!Input.GetMouseButton(0)) // ���콺 ��ư�� ���� ���
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider != null)
            {
                // 1. �ʵ忡 ��ġ�� ���
                GridCell cell = hit.collider.GetComponent<GridCell>();
                if (cell != null)
                {
                    Vector2 targetPos = cell.gridIndex;
                    if (gridManager.AddObjectToGrid(GetComponent<CardDisplay>().cardData.prefab, targetPos))
                    {
                        FinalizeCardPlay();
                        return;
                    }
                }
            }

            // ��ȿ�� ����� �ƴϸ� ���󺹱�
            TransitionToState0();
        }

        // ���콺�� �ٽ� �Ʒ��� ������ ���
        if (Input.mousePosition.y < cardPlay.y)
        {
            currentState = 2;
            playArrow.SetActive(false);
        }
    }
    private void FinalizeCardPlay()
    {
        // �ʵ� ��ġ ó��
        HandManager handManager = FindFirstObjectByType<HandManager>();
        handManager.cardsInHand.Remove(gameObject);
        handManager.UpdateHandVisuals();

        cardState = CardState.OnGrid;
        currentState = 0;
        glowEffect.SetActive(false);
        playArrow.SetActive(false);
        Destroy(gameObject);
    }
  


    private void updateCardPlayPosition()
    {
        if (cardPlayDivider != 0 && canvasRectTransform != null)
        {
            float segment = cardPlayMultiplier / cardPlayDivider;
            cardPlay.y = canvasRectTransform.rect.height * segment;
        }
    }
    private void updatePlayPosition()
    {
        if(canvasRectTransform != null && playPositionXDivider != 0 && playPositionYDivider !=0)
        {
            float segmentX = playPositionXMultiplier / playPositionXDivider;
            float segmentY = playPositionYMultiplier / playPositionYDivider;

            playPosition.x = canvasRectTransform.rect.width * segmentX;
            playPosition.y = canvasRectTransform.rect.height * segmentY;
        }
    }
}
