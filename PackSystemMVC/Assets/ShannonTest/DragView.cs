using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


/// <summary>
/// 鼠标指针拖拽物体的显示
/// </summary>
public class DragView : MonoBehaviour {

    public GameObject DragPrefab;
    public static DragView DragInstance;

    private GameObject tempObj;
    private GameObject canvasParent;

    private void Awake()
    {
        DragInstance = this;
        canvasParent = GameObject.FindGameObjectWithTag("Canvas");
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateMouseItem(string _name,Sprite _sprite)
    {
        tempObj = Instantiate(DragPrefab);
        tempObj.transform.SetParent(canvasParent.transform);
        tempObj.transform.position = Vector2.zero;
        tempObj.name = _name;
        tempObj.GetComponent<Image>().sprite = _sprite;
        tempObj.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void CreateMouseItem(string _name, Sprite _sprite,int _count)
    {
        tempObj = Instantiate(DragPrefab);
        tempObj.transform.SetParent(canvasParent.transform);
        tempObj.transform.position = Vector2.zero;
        tempObj.name = _name;
        tempObj.GetComponent<Image>().sprite = _sprite;
        tempObj.transform.GetChild(0).GetComponent<Text>().text = _count.ToString();
        tempObj.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDragItem(PointerEventData eventData)
    {
        tempObj.transform.position = eventData.position;
    }

    public void OnDropItem()
    {
        tempObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
        StartCoroutine(Delay(0.1f, () => {
            if (tempObj.transform.parent.name == "Canvas")
            Destroy(tempObj);
        }));
    }

    IEnumerator Delay(float _time,UnityAction action)
    {
        yield return new WaitForSeconds(_time);
        action();
    }
}
