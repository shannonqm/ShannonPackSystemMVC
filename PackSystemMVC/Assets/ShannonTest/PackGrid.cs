using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PackGrid : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IDropHandler
{
    public int grid;
    private PackEntity ThisGrid;

    private void Awake()
    {
        ThisGrid = PackDatabaseMgr.New(grid);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ItemController.Instance.OnChoosePack(grid);
    }

    public void OnDrag(PointerEventData eventData)
    {
        ItemController.Instance.OnDragItem(eventData,grid);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ItemController.Instance.OnDropItem(grid);
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemController.Instance.OnDropPack(ThisGrid.grid);
    }
}
