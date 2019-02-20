using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Model层，物品基类及管理类
/// </summary>
public class ItemEntity
{
    public string uuid = "";
    public string name;
    public Sprite sprite;

    public ItemEntity()
    {

    }
}

public class ItemDatabase
{
    private Dictionary<string, ItemEntity> entities = new Dictionary<string, ItemEntity>();

    public T New<T>()where T:ItemEntity ,new()
    {
        string uuid = System.Guid.NewGuid().ToString();
        T entity = new T();
        entity.uuid = uuid;
        entities[uuid] = entity;
        return entity;
    }

    //public BoxItem New(string _name, int _price, Sprite _sprite)
    //{
    //    string uuid = System.Guid.NewGuid().ToString();
    //    BoxItem entity = new BoxItem();
    //    entity.uuid = uuid;
    //    entity.name = _name;
    //    entity.price = _price;
    //    entity.sprite = _sprite;
    //    entities[uuid] = entity;
    //    return entity;
    //}

    public T New<T>(string _uuid)where T:ItemEntity,new()
    {
        T entity = new T();
        entity.uuid = _uuid;
        entities[_uuid] = entity;
        return entity;
    }

    //public BoxItem New(string _uuid)
    //{
    //    if (entities.ContainsKey(_uuid))
    //    {
    //        BoxItem entity = new BoxItem();
    //        BoxItem item = entities[_uuid] as BoxItem;
    //        entity.name = item.name;
    //        entity.price = item.price;
    //        entity.sprite = item.sprite;
    //        entities[_uuid] = entity;
    //        return entity;
    //    }
    //    return null;
    //}

    public ItemEntity Find(string _uuid)
    {
        if (entities.ContainsKey(_uuid))
            return entities[_uuid];
        return null;
    }

    public void Remove(string _uuid)
    {
        if (!entities.ContainsKey(_uuid))
            return;
        entities.Remove(_uuid);
    }
    public Dictionary<string, ItemEntity> List()
    {
        return entities;
    }
}

public class ItemDatabaseMgr
{
    private static ItemDatabase idb = new ItemDatabase();

    public static T New<T>()where T:ItemEntity,new()
    {
        return idb.New<T>();
    }

    //public static BoxItem New(string _name, int _price, Sprite _sprite)
    //{
    //    return idb.New(_name,_price,_sprite);
    //}

    public static T New<T>(string _uuid)where T:ItemEntity,new()
    {
        return idb.New<T>(_uuid);
    }

    public static ItemEntity Find(string _uuid)
    {
        return idb.Find(_uuid);
    }

    public static void Remove(string _uuid)
    {
        idb.Remove(_uuid);
    }

    public static Dictionary<string, ItemEntity> List()
    {
        return idb.List();
    }
}
