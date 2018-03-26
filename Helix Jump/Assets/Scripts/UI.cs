using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/**
 * UI
 **/
public class UI : BaseSceneManager<UI>
{
    public Animator blimAnimator;//溢出动画
    public AudioSource click;//点击声音
    public Color colorEnd;//结束的颜色
    public Color colorStart;//开始的颜色
    public Image filledImage;
    //菜单
    public GameObject fortuneSpinButtonRounded;
    public GameObject gcButton;//按钮：游戏中心
    public GameObject MM;//管理菜单
    public GameObject noadsButton;//按钮：去广告
    public Transform pointEnd;//结束的点
    public Transform pointStart;//开始的点
    public GameObject prizeBGRounded;
    public Text prizeMoneyTextRounded;
    public AudioSource prizeSound;
    public ParticleSystem psGrow;
    public GameObject restoreButton;//按钮：恢复
    public GameObject reviveBlock;//恢复
    public GameObject shopButton;//按钮：商店
    public Animator skinsComingSoon;//按钮：Skin
    public GameObject soundButton;//按钮：声音
    public GameObject SpinWheel;
    public GameObject spinWheelRounded;//圆形的SpinWheel

    public void BuyNoAds()
    {
        this.click.Play();
        BaseGameManager<AdsManager>.GetInstance().BuyNoAds();
    }

    //播放
    public void Play()
    {
        BaseSceneManager<mc>.Instance.isGameStarted = true;
        this.MM.SetActive(false);
    }

    public void RestorePurchases()
    {
        //BaseGameManager<AdsManager>.GetInstance().RestorePurchases();
        this.click.Play();
    }

    public void Revive()
    {
        this.click.Play();
        this.reviveBlock.SetActive(false);
        //BaseGameManager<AdsManager>.GetInstance().ShowVideo(new Action<bool>(this.ReviveSuccessful));
    }

    private void ReviveSuccessful(bool success)
    {
        if (success)
        {
            this.reviveBlock.SetActive(false);
            //BaseSceneManager<mc>.Instance.Revive();
        }
    }

    //点击：设置
    public void SettingsClicked()
    {
        if (this.restoreButton.activeInHierarchy)
        {
            this.noadsButton.SetActive(true);
            this.restoreButton.SetActive(false);
            this.soundButton.SetActive(false);
            this.shopButton.SetActive(true);
            this.gcButton.SetActive(false);
        }
        else
        {
            this.noadsButton.SetActive(false);
            this.restoreButton.SetActive(true);
            this.soundButton.SetActive(true);
            this.shopButton.SetActive(false);
            this.gcButton.SetActive(true);
        }
        //this.click.Play();
    }

    //点击：商店
    public void ShopClicked()
    {
        //this.skinsComingSoon.Play("Base Layer.comingSoon", 0, 0f);
    }

    public void ShowGC()
    {
        Social.ShowLeaderboardUI();
        this.click.Play();
    }

    private static void ShowRateUs()
    {
    }

    //显示恢复，弹广告
    public void ShowRevive()
    {
        if (BaseGameManager<AdsManager>.GetInstance().IsVideoReady())
        {
        //    base.StartCoroutine(this.reviveCoroutine());
        }
    }

    //点击按钮：声音
    public void SoundsClicked()
    {
        bool flag = !PlayerPrefs.HasKey("soundsOn") || (PlayerPrefs.GetInt("soundsOn") == 1);
        flag = !flag;
        PlayerPrefs.SetInt("soundsOn", !flag ? 0 : 1);
        AudioListener.volume = !flag ? 0f : 1f;
        this.click.Play();
    }

    //旋转
    public void Spin()
    {
        //base.StartCoroutine(this.SpinCoroutine());
    }

    private void Start()
    {
        AudioListener.volume = (PlayerPrefs.HasKey("soundsOn") && (PlayerPrefs.GetInt("soundsOn") != 1)) ? 0f : 1f;
    }

    //给我们打分
    public void StartRateUs()
    {
        ShowRateUs();
    }

    private void Update()
    {
        float t = (BaseSceneManager<mc>.Instance.currentPlayformId * 1f) / ((float)(BaseSceneManager<Base>.Instance.platforms.Count - 1));
        BaseSceneManager<mc>.Instance.progression.fillAmount = t;
        BaseSceneManager<mc>.Instance.progression.color = (Color.Lerp(this.colorStart, this.colorEnd, t));
        this.pointStart.GetComponent<Image>().color = (Color.Lerp(this.colorStart, this.colorEnd, t));
        //this.psGrow.main.startColor = Color.Lerp(this.colorStart, this.colorEnd, t);
        //this.psGrow.transform.position = Vector3.Lerp(Camera.main.ScreenToWorldPoint(this.pointStart.position + new Vector3(0f, 0f, 1f)), Camera.main.ScreenToWorldPoint(this.pointEnd.position + new Vector3(0f, 0f, 1f)), t + 0.01f);
        if (t > 0.7f)
        {
        //    this.blimAnimator.enabled = true;
        }
    }

    public void WheelofFortune()
    {
        this.SpinWheel.SetActive(true);
    }

}
