using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Model层，背包格子类及背包数据类
/// </summary>
public class PackEntity
{
    public int grid;
    public string itemID;
    public int count;
}

public class PackDatabase  {

    private Dictionary<int, PackEntity> myPack = new Dictionary<int, PackEntity>();

    public PackEntity New(int _grid)
    {
        PackEntity packEntity = new PackEntity();
        packEntity.grid = _grid;
        packEntity.itemID = "";
        packEntity.count = 0;
        myPack[_grid] = packEntity;
        return packEntity;
    }

    public void Add(int _grid,string _itemID)
    {
        PackEntity packEntity = Find(_itemID);
        if (packEntity == null)
        {
            myPack[_grid].itemID = _itemID;
            myPack[_grid].count++;
        }
        else packEntity.count++;
        //if (myPack.ContainsKey(_grid))
        //{
        //    if (myPack[_grid].item == null)
        //    {
        //        myPack[_grid].item = item;
        //        myPack[_grid].count++;
        //    }
        //    else
        //    {
        //        if (myPack[_grid].item.uuid == item.uuid)
        //            myPack[_grid].count++;
        //    }
        //}
    }

    public PackEntity Find(string _itemID)
    {
        foreach(PackEntity _packGrid in myPack.Values)
        {
            if (_packGrid.itemID == _itemID)
                return _packGrid;
        }
        return null;
    }

    public PackEntity CheckGrid(int _grid)
    {
        if (myPack[_grid].itemID != "")
        {
            return myPack[_grid];
        }
        return null;
    }

    public void Exchage(int grid1,int grid2)
    {
        string ItemAgent = myPack[grid1].itemID;
        myPack[grid1].itemID = myPack[grid2].itemID;
        myPack[grid2].itemID = ItemAgent;
        int CountAgent = myPack[grid1].count;
        myPack[grid1].count = myPack[grid2].count;
        myPack[grid2].count = CountAgent;
    }

    public void Remove(int _grid)
    {
        if (!myPack.ContainsKey(_grid))
            return;
        if (myPack[_grid].count >= 1)
            myPack[_grid].count--;
        if (myPack[_grid].count == 0)
            myPack[_grid].itemID = "";
    }

    public Dictionary<int,PackEntity> List()
    {
        return myPack;
    }

    //public int TotalPrice()
    //{
    //    int _totalPrice = 0;
    //    foreach(PackEntity entity in myPack.Values)
    //    {
    //        BoxItem boxItem = entity.item as BoxItem;
    //        _totalPrice += boxItem.price;
    //    }
    //    return _totalPrice;
    //}
}

public class PackDatabaseMgr
{
    private static PackDatabase pdb = new PackDatabase();

    public static PackEntity New(int _grid)
    {
        return pdb.New(_grid);
    }

    public static void Add(int _grid, string _itemID)
    {
        pdb.Add(_grid, _itemID);
    }

    public static PackEntity Find(string _itemID)        //给定某种物品，判断背包里是否有该种物品
    {
        return pdb.Find(_itemID);
    }

    public static PackEntity CheckGrid(int _grid)       //给定某个背包格子，判断里面有什么物品
    {
        return pdb.CheckGrid(_grid);
    }

    public static void Exchage(int grid1, int grid2)
    {
        pdb.Exchage(grid1, grid2);
    }

    public static void Remove(int _grid)
    {
        pdb.Remove(_grid);
    }

    public static Dictionary<int, PackEntity> List()
    {
        return pdb.List();
    }
}