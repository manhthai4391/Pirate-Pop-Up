using UnityEngine;
using UnityEngine.EventSystems;

public class RotateSlider : MonoBehaviour, IDragHandler
{
    [SerializeField]
    GameObject barrel;

    [SerializeField]
    float sensitivity;

    public void OnDrag(PointerEventData eventData)
    {
        barrel.transform.Rotate(Vector3.up, eventData.delta.y * sensitivity);
    }
}
