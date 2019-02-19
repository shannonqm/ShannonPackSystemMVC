using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoxItem : ItemEntity
{
    public int price;

    public BoxItem()
    {

    }

    public void Instantiate(string _name, int _price, Sprite _sprite)
    {
        name = _name;
        price = _price;
        sprite = _sprite;
    }
}

public class ItemInstantiate : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public string _name;
    public int _price;
    public Sprite _sprite;
    public BoxItem ThisItem {  get; private set; }

    private void Awake()
    {
        //ThisItem = ItemDatabaseMgr.New(_name,_price,_sprite);
        //ThisItem = new BoxItem(_name, _price, _sprite);
        ThisItem = ItemDatabaseMgr.New<BoxItem>();
        ThisItem.Instantiate(_name, _price, _sprite);
        //ThisItem.name = _name;
        //ThisItem.price = _price;
        //ThisItem.sprite = _sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ItemController.Instance.OnChooseItem(ThisItem.uuid);
    }

    public void OnDrag(PointerEventData eventData)
    {
        ItemController.Instance.OnDragItem(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ItemController.Instance.OnDropItem();
    }
}
