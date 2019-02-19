using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProductDrag : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler 
{
    public GameObject dragProduct;
    public int index;
    private GameObject tempObj;
    private GameObject canvasParent;
    public bool isDragging = false;

    //Json2Database itemDateBase;
    private ItemInstantiate productItem;

    // Use this for initialization
    void Start()
    {
        //itemDateBase = GameObject.Find("ItemDataBase").GetComponent<Json2Database>();
        index = Convert.ToInt32(name);
        //productItem = itemDateBase.FetchItemByID(index);
        canvasParent = GameObject.FindGameObjectWithTag("Canvas");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        tempObj = Instantiate(dragProduct);
        tempObj.transform.SetParent(canvasParent.transform);
        tempObj.transform.position = Vector2.zero;
        tempObj.name = this.name;
        //tempObj.GetComponent<Image>().sprite = productItem.Sprite;
        tempObj.GetComponent<CanvasGroup>().blocksRaycasts = false;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        tempObj.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        tempObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
        isDragging = false;
        StartCoroutine(DestroyDelay());  
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(0.1f);
        if (tempObj.transform.parent.name == "Canvas")
            Destroy(tempObj);
    }
}
