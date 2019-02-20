using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// Model，背包格子的实例与拖拽响应
/// </summary>
public class PackGridInstantiate : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IDropHandler
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
        ItemController.Instance.OnEndDragItem(grid);
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemController.Instance.OnDropInPack(ThisGrid.grid);
    }
}
