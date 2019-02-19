using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class UIController : MonoBehaviour {

    private GComponent mainUI;
    private GButton abundant;
    private GButton rare;
    private GButton abundant2;
    private GButton rare2;
    private GImage q1;
    private GGroup Q1Group;
    public AudioSource au;
    public AudioClip audio1;
    public AudioClip audio2;
    public float audio1Time;
    public float audio2Time;
    public GameObject p2;
    private float balance = 10000f;

	// Use this for initialization
	void Start () {
        mainUI = GetComponent<UIPanel>().ui;
        abundant = mainUI.GetChild("abundant").asButton;
        rare = mainUI.GetChild("rare").asButton;
        q1 = mainUI.GetChild("Q1").asImage;
        abundant.onClick.Add(Abundant);
        rare.onClick.Add(Rare);
	}

    private void Q1()
    {
        Transition q1 = mainUI.GetTransition("Q1");
        q1.Play();
    }

    private void Abundant()
    {
        StartCoroutine(Timer1(1.5f, audio1,audio1Time));
    }

    private void Rare()
    {
        StartCoroutine(Timer1(1.5f, audio2,audio2Time));
    }

    IEnumerator Timer1(float _time,AudioClip _audio,float _audioTime)
    {
        abundant.visible = false;
        rare.visible = false;
        q1.visible = false;
        yield return new WaitForSeconds(_time);
        au.clip = _audio;
        au.Play();
        yield return new WaitForSeconds(_audioTime);
        p2.SetActive(true);
    }

    private void P2()
    {
        Transition p2 = mainUI.GetTransition("Sources");
        p2.Play();
    }

    private void P2Fade()
    {
        Transition p2Fade = mainUI.GetTransition("SourcesFade");
        p2Fade.Play();
    }

    private void P3()
    {
        Transition p3 = mainUI.GetTransition("P3");
        p3.Play();
    }

    private void P4()
    {
        Transition p4 = mainUI.GetTransition("P4");
        p4.Play();
    }

    private void P4Fade()
    {
        Transition p4Fade = mainUI.GetTransition("P4Fade");
        p4Fade.Play();
    }

    private void P5()
    {
        balance = 10000f;
        mainUI.GetTransition("P5").SetHook("food", ()=>Minus(2000));
        mainUI.GetTransition("P5").SetHook("cloth", () => Minus(1000));
        mainUI.GetTransition("P5").SetHook("house", () => Minus(1800));
        mainUI.GetTransition("P5").SetHook("amuse", () => Minus(500));
        mainUI.GetTransition("P5").SetHook("school", () => Minus(1200));
        mainUI.GetTransition("P5").SetHook("traffic", () => Minus(300));
        mainUI.GetTransition("P5").SetHook("pocketMoney", () => Minus(100));
        mainUI.GetTransition("P5").SetHook("1", () => MinusValue(2000));
        mainUI.GetTransition("P5").SetHook("2", () => MinusValue(1000));
        mainUI.GetTransition("P5").SetHook("3", () => MinusValue(1800));
        mainUI.GetTransition("P5").SetHook("4", () => MinusValue(500));
        mainUI.GetTransition("P5").SetHook("5", () => MinusValue(1200));
        mainUI.GetTransition("P5").SetHook("6", () => MinusValue(300));
        mainUI.GetTransition("P5").SetHook("7", () => MinusValue(100));
        Transition p5 = mainUI.GetTransition("P5");
        p5.Play();
    }

    private void P7()
    {
        rare2 = mainUI.GetChild("rare2").asButton;
        rare2.onClick.Add(()=>P7Fade());
        abundant2 = mainUI.GetChild("abundant2").asButton;
        abundant2.onClick.Add(() => P7Fade());
        Transition p7 = mainUI.GetTransition("P7");
        p7.Play(); 
    }

    private void Minus(int _minusValue)
    {
        GTween.To(balance, balance-_minusValue, 0.3f).SetEase(EaseType.Linear).OnUpdate((GTweener tweener) => { mainUI.GetChild("money").text = Mathf.Floor(tweener.value.x).ToString(); });
        balance = balance - _minusValue;
    }

    private void MinusValue(int _minusValue)
    {
        mainUI.GetChild("minusValue").text = _minusValue.ToString();
    }

    private void P7Fade()
    {
        Transition p7Fade = mainUI.GetTransition("P7Fade");
        p7Fade.Play();
    }

    IEnumerator Timer2()
    {
        yield return new WaitForSeconds(1f);
        P7Fade();
    }
}
