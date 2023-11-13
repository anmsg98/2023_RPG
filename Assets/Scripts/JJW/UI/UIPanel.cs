using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIPanel : UIBase, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas m_canvas;
    private Vector2 m_canvasSize;

    [SerializeField]
    private RectTransform m_dragRect;

    [SerializeField]
    private UnityEvent<PointerEventData> m_onBeginDragEvt;

    [SerializeField]
    private UnityEvent<PointerEventData> m_onDragEvt;

    [SerializeField]
    private UnityEvent<PointerEventData> m_onEndDragEvt;

    public Canvas canvas
    {
        get
        {
            if (m_canvas == null)
            {
                m_canvas = gameObject.GetComponentInParent<Canvas>();
                m_canvasSize = m_canvas.GetComponent<RectTransform>().sizeDelta;
            }

            return m_canvas;
        }
    }

    protected override void OnEnable()
    {
        if (m_dragRect == null)
        {
            m_dragRect = gameObject.GetComponent<RectTransform>();
        }

        if ((m_dragRect == null) || (canvas == null))
        {
            enabled = false;
        }

        m_onDragEvt.AddListener(OnDragMove);
    }

    protected override void OnDisable()
    {
        m_onDragEvt.RemoveListener(OnDragMove);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // ?: null이 아닌지 검사 후 함수 호출
        m_onBeginDragEvt?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ?: null이 아닌지 검사 후 함수 호출
        m_onDragEvt?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // ?: null이 아닌지 검사 후 함수 호출
        m_onEndDragEvt?.Invoke(eventData);
    }

    public void OnDragMove(PointerEventData eventData)
    {
        Vector2 newPosition = m_dragRect.anchoredPosition + eventData.delta / canvas.scaleFactor;
        Vector2 halfSize = 0.5f * m_dragRect.sizeDelta;
        Vector2 halfCanvasSize = 0.5f * m_canvasSize / canvas.scaleFactor;

        if (newPosition.x - halfSize.x < -halfCanvasSize.x)
        {
            newPosition.x = -halfCanvasSize.x + halfSize.x;
        }
        else if (newPosition.x + halfSize.x > halfCanvasSize.x)
        {
            newPosition.x = halfCanvasSize.x - halfSize.x;
        }

        if (newPosition.y - halfSize.y < -halfCanvasSize.y)
        {
            newPosition.y = -halfCanvasSize.y + halfSize.y;
        }
        else if (newPosition.y + halfSize.y > halfCanvasSize.y)
        {
            newPosition.y = halfCanvasSize.y - halfSize.y;
        }

        m_dragRect.anchoredPosition = newPosition;
    }
}
