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
    [Header("音效")]
    public AudioSource clickAudio;//音效：点击
    public AudioSource prizeSound;//音效：奖励
    [Header("粒子效果")]
    public ParticleSystem psGrow;

    //
    public GameObject fortuneSpinButtonRounded;
    //顶部等级进度
    public Color colorEnd;//结束的颜色
    public Color colorStart;//开始的颜色
    public Transform pointEnd;//坐标：结束的点
    public Transform pointStart;//坐标：开始的点
    [Header("主菜单")]
    public GameObject mainMenu;//主菜单
    public GameObject noadsButton;//按钮：去广告
    public GameObject gcButton;//按钮：游戏中心
    public GameObject restoreButton;//按钮：修复
    public GameObject shopButton;//按钮：商店
    public GameObject soundButton;//按钮：声音打开关闭
    public Animator skinsComingSoon;//按钮：Skin
    [Header("失败后的菜单")]
    public GameObject reviveBlock;//恢复游戏界面
    public Image filledImage;//圆形进度条

    public GameObject prizeBGRounded;//奖励圆形的背景
    public Text prizeMoneyTextRounded;//奖励金币
    
    //转圈
    public GameObject SpinWheel;
    public GameObject spinWheelRounded;//圆形的SpinWheel

    public void BuyNoAds()
    {
        this.clickAudio.Play();
        BaseGameManager<AdsManager>.GetInstance().BuyNoAds();
    }

    //开始游戏
    public void Play()
    {
        BaseSceneManager<mc>.Instance.isGameStarted = true;
        this.mainMenu.SetActive(false);
    }

    public void RestorePurchases()
    {
        //BaseGameManager<AdsManager>.GetInstance().RestorePurchases();
        this.clickAudio.Play();
    }

    //恢复比赛
    public void Revive()
    {
        this.clickAudio.Play();
        this.reviveBlock.SetActive(false);
        //视频激励广告成功弹出，执行this.ReviveSuccessful
        //BaseGameManager<AdsManager>.GetInstance().ShowVideo(new Action<bool>(this.ReviveSuccessful));
        this.ReviveSuccessful(true);
    }

    //恢复比赛成功
    private void ReviveSuccessful(bool success)
    {
        if (success)
        {
            this.reviveBlock.SetActive(false);
            BaseSceneManager<mc>.Instance.Revive();
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
        this.clickAudio.Play();
    }

    //点击：商店
    public void ShopClicked()
    {
        //this.skinsComingSoon.Play("Base Layer.comingSoon", 0, 0f);
    }

    public void ShowGC()
    {
        Social.ShowLeaderboardUI();
        this.clickAudio.Play();
    }

    private static void ShowRateUs()
    {
    }

    //显示恢复界面
    public void ShowRevive()
    {
        UnityEngine.Debug.Log("ShowRevive");
        //if (BaseGameManager<AdsManager>.GetInstance().IsVideoReady())
        //{
        //    //弹出广告，才会恢复比赛
        //    base.StartCoroutine(this.reviveCoroutine());
        //}
        base.StartCoroutine(this.reviveCoroutine());
    }

    //点击按钮：声音
    public void SoundsClicked()
    {
        bool flag = !PlayerPrefs.HasKey("soundsOn") || (PlayerPrefs.GetInt("soundsOn") == 1);
        flag = !flag;
        PlayerPrefs.SetInt("soundsOn", !flag ? 0 : 1);
        AudioListener.volume = !flag ? 0f : 1f;
        this.clickAudio.Play();
    }

    //旋转
    public void Spin()
    {
        base.StartCoroutine(this.SpinCoroutine());
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

    //旋转
    private IEnumerator SpinCoroutine()
    {
        yield return null;
    }

    //恢复比赛
    private IEnumerator reviveCoroutine()
    {
        float time = 4f;
        this.reviveBlock.SetActive(true);
        while (time > 0f)
        {
            time -= Time.deltaTime;
            this.filledImage.fillAmount = time / 4f;
            yield return null;
        }
        this.reviveBlock.SetActive(false);
    }
}
