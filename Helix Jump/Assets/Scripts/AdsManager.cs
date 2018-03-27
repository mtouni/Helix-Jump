using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : BaseGameManager<AdsManager>
{
    //插屏广告最后显示
    private float interstitialLastShown = float.NegativeInfinity;
    private float interstitialNotReadyCounter = float.PositiveInfinity;
    private bool interstitialReady;
    private bool isPurchaseInProgress;
    //private static IStoreController m_StoreController;
    //private static IExtensionProvider m_StoreExtensionProvider;
    public static string productIDNoAds = "com.h8games.falldown.noads";
    //private Action<bool> videoCallback;
    private float videoNotReadyCounter = float.PositiveInfinity;
    private bool videoReady;

    private void Awake()
    {
        //VoodooSauce.RegisterPurchaseDelegate(this);
        //if (PlayerPrefs.HasKey("NoAds"))
        //{
        //    VoodooSauce.EnablePremium();
        //}
    }

    //购买去广告
    public void BuyNoAds()
    {
        //VoodooSauce.Purchase(productIDNoAds);
    }

    //是否视频广告已经准备好
    public bool IsVideoReady()
    {
        //return VoodooSauce.IsRewardedVideoAvailable();
        return true;
    }

    //Banner广告展示回调
    private void MyBannerDisplayedCallback(float result)
    {
    }

    //激励广告展示成功回调
    private void MyInterstitialCompleteCallback()
    {
    }

    //激励广告失败
    //public void OnInitializeFailure(InitializationFailureReason reason)
    //{
    //}

    //购买成功
    public void OnPurchaseComplete(string productId)
    {
        //VoodooSauce.EnablePremium();
    }

    //购买失败
    //public void OnPurchaseFailure(string productId, PurchaseFailureReason reason)
    //{
    //    Debug.Log("Process purchase failure. Unrecognized product: " + productId + ". Reason: " + reason.ToString());
    //}

    //恢复购买
    public void RestorePurchases()
    {
        //VoodooSauce.RestorePurchases();
    }

    //展示激励广告
    public void ShowInterstitial()
    {
        
        //VoodooSauce.ShowInterstitial(new Action(this.MyInterstitialCompleteCallback));
    }

    //展示视频广告
    //public void ShowVideo(Action<bool> callback)
    //{
    //    VoodooSauce.ShowRewardedVideo(callback);
    //}

    //展示Banner广告
    void Start()
    {
        //VoodooSauce.ShowBanner(new Action<float>(this.MyBannerDisplayedCallback));
    }
}
