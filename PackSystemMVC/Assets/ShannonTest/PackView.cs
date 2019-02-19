using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackView : MonoBehaviour {

    public Image[] packViews;
    public static PackView Instance;
    private Dictionary<int, Image> packViewList = new Dictionary<int, Image>();

    private void Awake()
    {
        Instance = this;
        foreach(Image packs in packViews)
        {
            int _grid = packs.GetComponent<PackGrid>().grid;
            packViewList.Add(_grid, packs);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdatePackView(int _grid)
    {
        PackEntity waitUpdatePack = PackDatabaseMgr.CheckGrid(_grid);
        if(waitUpdatePack!=null)
        {
            packViewList[_grid].sprite = ItemDatabaseMgr.Find(waitUpdatePack.itemID).sprite;
            packViewList[_grid].color = Color.white;
            packViewList[_grid].transform.GetChild(0).GetComponent<Text>().text = waitUpdatePack.count.ToString();
        }
        else
        {
            packViewList[_grid].sprite = null;
            packViewList[_grid].color = new Color(1, 1, 1, 0);
            packViewList[_grid].transform.GetChild(0).GetComponent<Text>().text = "";
        }
    }
}
