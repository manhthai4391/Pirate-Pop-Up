using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class SwordDrawer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    Image swordImage;

    [SerializeField]
    CanvasGroup canvasGroup;

    [SerializeField]
    CanvasScaler scaler;

    Vector2 initPosition;
    [SerializeField]
    string holeTag;

    Hole selected;

    public bool isDragging;

    private void Start()
    {
        initPosition = swordImage.rectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        swordImage.rectTransform.anchoredPosition += eventData.delta * scaler.scaleFactor;
        //raycast hole
        foreach(GameObject obj in eventData.hovered)
        {
            if(obj.CompareTag(holeTag))
            {
                if(selected != null)
                {
                    selected.Deselect();
                }
                selected = obj.GetComponent<Hole>();
                selected.Select();
            }
        }
    }

    public void Return()
    {
        swordImage.rectTransform.position = initPosition;
        canvasGroup.blocksRaycasts = true;
        isDragging = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Return();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        isDragging = true;
    }
}
