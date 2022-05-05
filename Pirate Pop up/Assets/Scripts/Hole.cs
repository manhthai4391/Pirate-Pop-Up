using UnityEngine.EventSystems;
using UnityEngine;

public class Hole : MonoBehaviour, IDropHandler
{
    [SerializeField]
    int holeIndex;

    [SerializeField]
    GamePlay gamePlay;

    [SerializeField]
    GameObject swordDrawer;

    [SerializeField]
    GameObject outline;
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag == swordDrawer)
        {
            gamePlay.SpawnSword(holeIndex);
            Destroy(outline);
            Destroy(gameObject);
        }
    }

    public void Select()
    {
        outline.SetActive(true);
    }

    public void Deselect()
    {
        outline.SetActive(false);
    }
}
