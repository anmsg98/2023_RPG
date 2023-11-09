using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIPanel : UIBase, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas m_canvas;

    [SerializeField]
    private RectTransform m_dragRect;

    [SerializeField]
    private UnityEvent<PointerEventData> onBeginDragEvt;

    [SerializeField]
    private UnityEvent<PointerEventData> onDragEvt;

    [SerializeField]
    private UnityEvent<PointerEventData> onEndDragEvt;

    public Canvas canvas
    {
        get
        {
            if (m_canvas == null)
            {
                m_canvas = GetComponentInParent<Canvas>();
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

        if (m_dragRect == null || canvas == null)
        {
            enabled = false;
        }

        onDragEvt.AddListener(OnDragMove);
    }

    protected override void OnDisable()
    {
        onDragEvt.RemoveListener(OnDragMove);
    }

    public void OnDragMove(PointerEventData eventData)
    {
        m_dragRect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // ?: null이 아닌지 검사 후 함수 호출
        onBeginDragEvt?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ?: null이 아닌지 검사 후 함수 호출
        onDragEvt?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // ?: null이 아닌지 검사 후 함수 호출
        onEndDragEvt?.Invoke(eventData);
    }
}
