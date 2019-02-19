using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ProductAdd : MonoBehaviour,IDropHandler {

    private bool isDragging = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //private void OnTriggerEnter(Collider other)
    //{
    //    isDragging = true;
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    isDragging = true;
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (isDragging == false)
    //    {
    //        int tempIndex =  Convert.ToInt32(other.transform.name);
    //        BroadcastMessage("JudgeItem", tempIndex);
    //        StartCoroutine(OneFrame());
    //        isDragging = true;
    //    }
    //}

    IEnumerator OneFrame()
    {
        yield return new WaitForEndOfFrame();
    }

    public void OnDrop(PointerEventData eventData)
    {
        isDragging = false;
        string name = eventData.pointerDrag.transform.name;
        int tempIndex = Convert.ToInt32(name);
        BroadcastMessage("JudgeItem", tempIndex);
    }
}