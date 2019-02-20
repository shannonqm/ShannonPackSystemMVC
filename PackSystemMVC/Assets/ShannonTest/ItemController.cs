using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// Controller,单例的控制类，接入拖拽事件，进行数据层操作并转到View类更新
/// </summary>
public class ItemController : MonoBehaviour {

    public static ItemController Instance;
    private static ItemEntity dragItem;
    private static PackEntity dragPack;

    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach(PackEntity packs in PackDatabaseMgr.List().Values)
        {
            if(packs.itemID!="")
                print("背包格子："+packs.grid+"商品名字："+ItemDatabaseMgr.Find(packs.itemID).name+"数量："+packs.count);
        }
        if (dragItem != null)
            print("DragItem:" + dragItem.name);
        if (dragPack != null)
            print("DragPack:" + ItemDatabaseMgr.Find(dragPack.itemID).name);
    }

    public void OnChooseItem(string _uuid)      //从商店里拖动开始时调用
    {
        dragItem = ItemDatabaseMgr.Find(_uuid) as BoxItem;       //找到拖动的item在Model里的数据
        DragView.DragInstance.CreateMouseItem(dragItem.name,dragItem.sprite);     //调用View生成鼠标实例
    }

    public void OnDragItem(PointerEventData eventData)      //从商店里拖动时调用
    {
        DragView.DragInstance.OnDragItem(eventData);        //调用View显示拖动
    }

    public void OnEndDragItem()        //从商店里拖动结束时调用
    {
        DragView.DragInstance.OnDropItem();     //调用View销毁鼠标实例
        dragItem = null;
    }

    public void OnDropInPack(int _grid)        //物品放下格子里时调用
    {
        if (dragItem != null)
        {
            if (PackDatabaseMgr.CheckGrid(_grid) == null)
                PackDatabaseMgr.Add(_grid, dragItem.uuid);
            else
            {
                foreach (PackEntity packs in PackDatabaseMgr.List().Values)
                {
                    if(packs.itemID == "")
                    {
                        PackDatabaseMgr.Add(packs.grid, dragItem.uuid);
                        break;
                    }
                }
            }
            PackEntity pack = PackDatabaseMgr.Find(dragItem.uuid);
            PackView.Instance.UpdatePackView(pack.grid);
        }
        else if(dragPack != null)
        {
            PackDatabaseMgr.Exchage(dragPack.grid, _grid);
            PackView.Instance.UpdatePackView(dragPack.grid);
            PackView.Instance.UpdatePackView(_grid);
            DragView.DragInstance.OnDropItem();
            dragPack = null;
        }
    }

    public void OnChoosePack(int _grid)     //从背包里拖动东西时调用
    {
        PackEntity pack = PackDatabaseMgr.CheckGrid(_grid);
        if (pack != null)
        {
            dragPack = pack;
            DragView.DragInstance.CreateMouseItem(ItemDatabaseMgr.Find(dragPack.itemID).name, ItemDatabaseMgr.Find(dragPack.itemID).sprite, pack.count);
        }
    }

    public void OnDragItem(PointerEventData eventData,int _grid)      //从背包里拖动时调用
    {
        PackEntity pack = PackDatabaseMgr.CheckGrid(_grid);
        if (pack != null)
            DragView.DragInstance.OnDragItem(eventData);        //调用View显示拖动
    }

    public void OnEndDragItem(int _grid)        //从背包里拖动结束时调用
    {
        //PackEntity pack = PackDatabaseMgr.CheckGrid(_grid);
        if (dragPack != null)
        {
            PackDatabaseMgr.Remove(dragPack.grid);
            PackView.Instance.UpdatePackView(dragPack.grid);
            DragView.DragInstance.OnDropItem();     //调用View销毁鼠标实例
            dragPack = null;
        }
    }
}
