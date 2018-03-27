using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mc : BaseSceneManager<mc>
{
    private string[] versionNames = new string[] { "Base", "WorldBuilder", "ShorterLevels", "Animations" };//版本名称

    [Header("基础信息")]
    public GameType gameId;//游戏ID
    public int currentObjectLevel;//当前游戏等级
    public bool isActive = true;//是否在活动
    public bool isGameStarted;//是否游戏开始
    private int sessionsCount;//会话次数（用户弹出评分框）
    public int currentPlayformId;//当前平台ID
    public Transform currentPlatform;//当前平台坐标

    //添加金币重新启动
    public GameObject AddMoneyRestart;

    public Text AddMoneyRestartText;
    //
    public Animator anim;
    public GameObject AnimatedButton;//
    public Text best;//Text：最好成绩
    public GameObject bestUI;//最好成绩的UI（没啥用）
    //
    public GameObject canvas;//画布
    //
    public int currency;//金币
    public List<int> currencyList;//货币列表
    public Text currencyText;//文本：当前文本
    public GameObject currencyUI;//UI：当前金币

    [Header("配置")]
    public List<GameObject> objects;//游戏块

    [Header("音效")]
    public AudioSource AddMoneyRestartSound;
    public AudioSource deathAudio;//音效：死亡
    public AudioSource passAudio;//音效：通过平台
    public AudioSource splashAudio;//音效：球落地水花溅开

    [Header("粒子效果")]
    public ParticleSystem psExtraSplash;//粒子效果：球快熟下落撞破台面撒出的大粒子
    public ParticleSystem psBurn;//粒子效果：燃烧
    public ParticleSystem psBurn1;//粒子效果：燃烧1
    public ParticleSystem psSplash; ///粒子效果：球和台面碰撞撒出的粒子

    [Tooltip("球落地痕迹")]
    public List<GameObject> decal;//球落地痕迹

    [Header("Prefab")]
    [Tooltip("动画：完成比赛")]
    public GameObject finishPrefab;//Prefab:完成比赛
    [Tooltip("动画：Awesome")]
    public GameObject plusAwesomePrefab;//动画：Awesome动画
    [Tooltip("动画：加分")]
    public GameObject plusPrefab;//动画：加分动画

    [Header("等级进度条")]
    public Text levelFrom;//当前等级
    public Text levelTo;//下一等级
    public Image LevelPassedPin;
    public GameObject levelUpButton;
    public Text levelUpText;
    public Image progression;//进展图片

    public PhysicMaterial mat;//物理反弹力
    public Material mcMat;//材质
    public Text newRecord;//新记录

    public GameObject psPickup;

    public GameObject restartMenu;/// 重新开始菜单（完成）
    public Text restartPercentage;//重新开始：完成百分比
    private bool reviveShown;//是否恢复显示，弹广告
    private static int score = 0;//分数
    public int scoreInRow = 1;//当前这行的分数
    public int scoreNow = 3;//当前的分数
    //
    public Text scoreNowText;
    public Text scoreText;//当前分数
    private bool setUpvelocity;//是否需要弹起来
    public float startDrag;//开始时候的阻力：
    public float finalDrag;//结束时候的阻力：
    [Header("设置获胜界面")]
    public GameObject winMenu;//获胜界面
    public Text levelText;//文字：过关，等级提升

    private void Start()
    {
        this.currentObjectLevel = PlayerPrefs.GetInt("currentObjectLevel");
        if (this.gameId == GameType.GAME_WORLD)
        {
            //    this.currencyUI.SetActive(true);
            //    this.levelUpButton.SetActive(true);
            //    this.currency = PlayerPrefs.GetInt("currency");
            //    this.currencyText.text = this.currency.ToString();
        }
        //this.UpdateObjects();
        this.currentPlatform = null;
        this.best.text = "best: " + PlayerPrefs.GetInt("bestScore");
        this.levelFrom.text = (Base.currentLevel + 1).ToString();
        this.levelTo.text = (Base.currentLevel + 2).ToString();
        ////FB.Init(new InitDelegate(this.InitCallback), null, null);
        //this.SocialAuthenticate();
        //VoodooSauce.OnGameStarted();
    }

    void Update()
    {
        //this.scoreNowText.text = this.scoreNow.ToString();
        this.scoreText.text = score.ToString();
        //this.currencyText.text = this.currency.ToString();
        if ((this.currentPlayformId / (BaseSceneManager<Base>.Instance.platforms.Count - 1)) == 1)
        {
            //当前平台ID/平台ID总数 == 1 ，说明过关了
            //this.LevelPassedPin.gameObject.SetActive(true);
        }
        //设置刚体的阻力（当受力移动时物体受到的空气阻力。0表示没有空气阻力,极大时使物体立即停止运动。）
        base.GetComponent<Rigidbody>().drag = Mathf.Lerp(this.startDrag, this.finalDrag, (this.currentPlayformId * 1f) / ((float)(BaseSceneManager<Base>.Instance.platforms.Count - 1)));

        if (base.transform.position.y < BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId].transform.position.y)
        {
            this.passAudio.pitch = 1f + ((this.scoreInRow * 0.2f) / ((float)(Base.currentLevel + 1)));
            this.passAudio.Play();
            //摧毁面板
            base.StartCoroutine(this.destroyLayerCoroutine(BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId]));
            BaseSceneManager<UI>.Instance.Play();//游戏开始
            //    this.bestUI.SetActive(false);
            if ((this.currentPlatform == null) || (this.currentPlatform.transform.parent != BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId].transform))
            {
                //收集
                //UnityEngine.Object.Instantiate<GameObject>(this.psPickup, BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId].transform.position, Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
                this.currentPlayformId++;
                base.StartCoroutine(this.plusCoroutine(this.scoreInRow));
                score += this.scoreInRow;
                this.scoreInRow += Base.currentLevel + 1;
            }
            else
            {
                //UnityEngine.Object.Instantiate<GameObject>(this.psPickup, this.currentPlatform.transform.position, Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
                this.currentPlayformId++;
                this.scoreInRow = Base.currentLevel + 1;
                base.StartCoroutine(this.plusCoroutine(this.scoreInRow));
                score += this.scoreInRow;
            }
            this.currentPlatform = null;
        }
        if (this.scoreInRow > ((Base.currentLevel + 1) * 2))
        {
            //    this.psBurn.gameObject.SetActive(true);
            //    this.psBurn1.gameObject.SetActive(true);
        }
        else
        {
            //    this.psBurn.gameObject.SetActive(false);
            //    this.psBurn1.gameObject.SetActive(false);
        }
        if (this.scoreNow == 0)
        {
            //this.mat.bounciness = 0f;//反弹力
        }
        else
        {
            //this.mat.bounciness = 0.9f;//反弹力
        }
    }

    private void Awake()
    {
        BaseGameManager<AdsManager>.GetInstance();
        for (int i = 0; i < 4; i++)
        {
            //
            //if (this.versionNames[i] == VoodooSauce.GetPlayerCohort())
            //{
            //    this.gameId = (GameType)i;
            //}
        }
    }

    //增加金币
    public void AddMoney(int money)
    {
        this.currency += money;
        PlayerPrefs.SetInt("currency", this.currency);
    }

    private void AuthCallback(bool success)
    {
        UnityEngine.Debug.Log("Authentication finished: " + success.ToString());
    }

    //失败
    public void Fail()
    {
        this.scoreInRow = 0;
        this.isActive = false;
        base.GetComponent<Rigidbody>().isKinematic = true;//让球不再运动
        this.restartMenu.SetActive(true);
        this.deathAudio.Play();
        base.transform.localScale = new Vector3(2.3f, 1.7f, 2.3f);
        this.restartPercentage.text = ((int)((BaseSceneManager<mc>.Instance.currentPlayformId * 100f) / ((float)(BaseSceneManager<Base>.Instance.platforms.Count - 1)))).ToString("D") + "% Completed!";
        if (PlayerPrefs.HasKey("sessionsCount"))
        {
            this.sessionsCount = PlayerPrefs.GetInt("sessionsCount");
        }
        if (((this.sessionsCount == 5) || (this.sessionsCount == 0x19)) || (((this.sessionsCount == 100) || (this.sessionsCount == 200)) || (this.sessionsCount == 500)))
        {
            BaseSceneManager<UI>.Instance.StartRateUs();
        }
        //this.bestUI.SetActive(false);
        if (!this.reviveShown)
        {
            //恢复显示，弹广告
            this.reviveShown = true;
            //BaseSceneManager<UI>.Instance.ShowRevive();
        }
        this.ReportScore((float)score);
        ////VoodooSauce.OnGameFinished((float)score);
        //新记录诞生
        if (PlayerPrefs.GetInt("bestScore") < score)
        {
            this.newRecord.gameObject.SetActive(true);
            PlayerPrefs.SetInt("bestScore", score);
            this.newRecord.text = "NEW RECORD!\n" + score.ToString();
        }
    }

    //包里摧毁
    public void ForceDestroy(Transform platform)
    {
        this.psExtraSplash.Play();
        base.StartCoroutine(this.plusCoroutine(this.scoreInRow));
        score += this.scoreInRow;
        //UnityEngine.Object.Instantiate<GameObject>(this.psPickup, BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId].transform.position, Quaternion.Euler(new Vector3(-90f, 0f, 0f))).GetComponent<ParticleSystem>().Play();
        for (int i = 0; i < BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId].transform.childCount; i++)
        {
            //    BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId].transform.GetChild(i).GetComponent<MeshRenderer>().material = this.mcMat;
        }
        base.StartCoroutine(this.destroyLayerCoroutine(BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId]));
        //TapticManager.Impact(ImpactFeedback.Midium);
        this.splashAudio.Play();
        this.setUpvelocity = true;
        //this.anim.Play("Base Layer.Splash", 0, 0f);//飞溅开来
        this.currentPlayformId++;
    }

    private void InitCallback()
    {
        //if (FB.IsInitialized)
        //{
        //    FB.ActivateApp();
        //}
        //else
        //{
        //    UnityEngine.Debug.Log("Failed to Initialize the Facebook SDK");
        //}
    }

    private void LateUpdate()
    {
        if (this.setUpvelocity)
        {
            //原
            //base.GetComponent<Rigidbody>().velocity = new Vector3(0f, 60f, 0f);
            base.GetComponent<Rigidbody>().velocity = new Vector3(0f, 35f, 0f);
            this.setUpvelocity = false;
        }
    }

    //等级提升
    public void LevelUp()
    {
        if (this.currency >= this.currencyList[this.currentObjectLevel])
        {
            if (this.currentObjectLevel >= this.objects.Count)
            {
                //点击商店
                BaseSceneManager<UI>.Instance.ShopClicked();
            }
            else
            {
                this.AddMoney(-this.currencyList[this.currentObjectLevel]);
                this.currentObjectLevel++;
                PlayerPrefs.SetInt("currentObjectLevel", this.currentObjectLevel);
                this.UpdateObjects();
                if (this.objects[this.currentObjectLevel - 1].GetComponent<Animator>() != null)
                {
                    this.objects[this.currentObjectLevel - 1].GetComponent<Animator>().Play("Base Layer.appear", 0, 0f);
                }
            }
        }
    }

    //发生碰撞
    private void OnCollisionEnter(Collision other)
    {
        UnityEngine.Debug.Log("OnCollisionEnter");
        if (other.collider.tag == "Fail")
        {
            if (this.scoreInRow > ((Base.currentLevel + 1) * 3))
            {
                //如果穿越的平台大于破碎的块数，破碎
                this.currentPlatform = other.collider.transform;
                this.scoreInRow = Base.currentLevel + 1;
                this.ForceDestroy(other.collider.transform.parent);
            }
            else
            {
                UnityEngine.Debug.Log("失败");
                this.Fail();
            }
        }
        if (other.collider.tag == "Finish")
        {
            //赢了
            UnityEngine.Debug.Log("Finish , you are win");
            this.Win();
            this.scoreInRow = 0;
        }
        else if (this.currentPlatform != other.collider.transform)
        {
            //当前位置不等于碰撞的坐标
            this.currentPlatform = other.collider.transform;
            if (this.scoreInRow > ((Base.currentLevel + 1) * 3))
            {
                //如果穿越的平台大于破碎的块数，破碎
                this.ForceDestroy(other.collider.transform.parent);
                this.scoreInRow = Base.currentLevel + 1;
            }
            else
            {
                this.scoreInRow = Base.currentLevel + 1;
                //贴图
                GameObject decalObj = UnityEngine.Object.Instantiate<GameObject>(this.decal[UnityEngine.Random.Range(0, this.decal.Count)], new Vector3(base.transform.position.x, this.currentPlatform.transform.position.y + 1.5f, base.transform.position.z), Quaternion.identity, this.currentPlatform);
                decalObj.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
                decalObj.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, (float)UnityEngine.Random.Range(-180, 180)));
                //震动
                //TapticManager.Impact(ImpactFeedback.Midium);
                this.splashAudio.Play();
                this.setUpvelocity = true;
                //this.anim.Play("Base Layer.Splash", 0, 0f);
            }
        }
        else
        {
            UnityEngine.Debug.Log("还在当前台面");
            this.scoreInRow = Base.currentLevel + 1;
            //贴图
            GameObject decalObj = UnityEngine.Object.Instantiate<GameObject>(this.decal[UnityEngine.Random.Range(0, this.decal.Count)], new Vector3(base.transform.position.x, this.currentPlatform.transform.position.y + 1.5f, base.transform.position.z), Quaternion.identity, this.currentPlatform);
            decalObj.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            decalObj.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, (float)UnityEngine.Random.Range(-180, 180)));
            //震动
            //TapticManager.Impact(ImpactFeedback.Light);
            this.setUpvelocity = true;
            //this.anim.Play("Base Layer.Splash", 0, 0f);
            if (this.scoreNow == 0)
            {
                this.Fail();
            }
            else
            {
                this.splashAudio.Play();
            }
        }
        this.psSplash.Play();
    }

    public void PostCallback(bool success)
    {
        UnityEngine.Debug.Log("Post finished: " + success.ToString());
    }

    //上传分数
    private void ReportScore(float score)
    {
        UnityEngine.Debug.Log("Report Score: " + score.ToString());
        if (!Application.isEditor)
        {
        }
    }

    //重新开始
    public void Restart()
    {
        UnityEngine.Debug.Log("Restart");
        base.StartCoroutine(this.AddMoneyRestartCoroutine());
    }

    //死亡后,恢复比赛
    public void Revive()
    {
        UnityEngine.Debug.Log("恢复比赛");
        base.transform.localScale = new Vector3(1.816f, 1.816f, 1.816f);
        this.isActive = true;
        base.GetComponent<Rigidbody>().isKinematic = false;
        this.restartMenu.SetActive(false);
        this.currentPlayformId--;
        Transform transform = base.transform;
        transform.position += new Vector3(0f, 20f, 0f);
        BaseSceneManager<Base>.Instance.Revive(this.currentPlayformId);
        Transform transform2 = Camera.main.transform;
        transform2.position += new Vector3(0f, 20f, 0f);
    }

    public void SocialAuthenticate()
    {
        if (!Application.isEditor && !Social.localUser.authenticated)
        {
            Social.localUser.Authenticate(new Action<bool>(this.AuthCallback));
        }
    }

    public void StartAnimations()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("VoodooSauce_Cohort", this.versionNames[3]);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartBase()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("VoodooSauce_Cohort", this.versionNames[0]);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartShort()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("VoodooSauce_Cohort", this.versionNames[2]);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartWorld()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("VoodooSauce_Cohort", this.versionNames[1]);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateObjects()
    {
        for (int i = 0; i < this.objects.Count; i++)
        {
            if (i < this.currentObjectLevel)
            {
                this.objects[i].SetActive(true);
            }
        }
        if (this.currency >= this.currencyList[this.currentObjectLevel])
        {
            this.AnimatedButton.SetActive(true);
        }
        else
        {
            this.AnimatedButton.SetActive(false);
        }
        this.levelUpText.text = this.currencyList[this.currentObjectLevel].ToString();
    }

    public void Win()
    {
        this.isActive = false;
        base.GetComponent<Rigidbody>().isKinematic = true;
        if (this.gameId == GameType.GAME_ANIM)
        {
            BaseSceneManager<MainCamera>.Instance.GoUp();
        }
        this.levelText.text = "Level " + ((Base.currentLevel + 1)).ToString() + " passed";
        base.StartCoroutine(this.NextLevel());
    }

    //添加金币重启
    private IEnumerator AddMoneyRestartCoroutine()
    {
        int scoreAdd = mc.score / 50;//添加的分数
        if ((scoreAdd <= 0) || (this.gameId != GameType.GAME_WORLD))
        {
            //break;
        }
        //BaseSceneManager<UI>.Instance.reviveBlock.SetActive(false);
        this.newRecord.gameObject.SetActive(false);
        this.restartPercentage.gameObject.SetActive(false);
        //this.AddMoneyRestartText.text = "+" + scoreAdd.ToString();
        //this.AddMoneyRestart.SetActive(true);
        //this.AddMoneyRestartSound.Play();
        yield return new WaitForSeconds(0.5f);

        this.AddMoney(scoreAdd);
        yield return new WaitForSeconds(0.5f);

        if (PlayerPrefs.HasKey("sessionsCount"))
        {
            this.sessionsCount = PlayerPrefs.GetInt("sessionsCount");
            this.sessionsCount++;
            PlayerPrefs.SetInt("sessionsCount", this.sessionsCount);
        }
        else
        {
            this.sessionsCount++;
            PlayerPrefs.SetInt("sessionsCount", this.sessionsCount);
        }
        mc.score = 0;
        //BaseGameManager<AdsManager>.GetInstance().ShowInterstitial();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //摧毁当前平台
    private IEnumerator destroyLayerCoroutine(GameObject layer)
    {
        UnityEngine.Debug.Log("destroyLayerCoroutine"); // 这里只是进来一次  
        //平台的子类块列表
        List<Transform> objectList = new List<Transform>();
        float speed = 0f;//速度
        float time = 0f;//时间
        if (this.gameId == GameType.GAME_ANIM)
        {
        //    BaseSceneManager<UI>.Instance.psGrow.Play();
        //    BaseSceneManager<UI>.Instance.blimAnimator.Play("Base Layer.Blim", 0, 0f);
        }
        if (layer.GetComponent<Animator>() != null)
        {
            layer.GetComponent<Animator>().enabled = false;
        }
        //获取到这个平台的子类块，添加到列表中
        for (int j = 0; j< layer.transform.childCount; j++)
        {
            objectList.Add(layer.transform.GetChild(j));
        }
        //设置块的父亲节点为空
        for (int k = 0; k< objectList.Count; k++)
        {
            objectList[k].parent = null;
        }
        //
        while (time < 1f)
        {
            UnityEngine.Debug.Log("time ： " + time);
            time += Time.deltaTime;
            speed += Time.deltaTime * Physics.gravity.y;
            for (int m = 0; m < objectList.Count; m++)
            {
                Transform objectItem = objectList[m];
                Vector3 vector2 = (Vector3)((objectItem.forward * Time.deltaTime) * 100f);
                objectItem.position += new Vector3(vector2.x, speed * Time.deltaTime, vector2.z);
                objectItem.Rotate((float)(45f * Time.deltaTime), 90f * Time.deltaTime, (float)(20f * Time.deltaTime));
                UnityEngine.Debug.Log("time ： " + time + "; objectItem : " + objectItem + "; objectItem.position : " + objectItem.position);
            }
            yield return null; //下一帧调用, 什么都不做
        }
        for (int i = 0; i < objectList.Count; i++)
        {
            objectList[i].gameObject.SetActive(false);
        }
        layer.SetActive(false);
    }

    //下个级别
    private IEnumerator NextLevel()
    {
        int i = 0;
        Dictionary<string, object> properties = new Dictionary<string, object>();
        float ypos = this.transform.position.y;
        properties["Level"] = Base.currentLevel;
        //VoodooSauce.TrackCustomEvent("Level Passed", properties);
        //等级提升
        Base.currentLevel++;
        PlayerPrefs.SetInt("currentLevel", Base.currentLevel);

        if (this.gameId != GameType.GAME_ANIM)
        {
            //this.winMenu.SetActive(true);
            yield return new WaitForSeconds(1.5f);
        }
        yield return null;
        i++;
        yield return null;
        if (this.gameId != GameType.GAME_WORLD)
        {
            //BaseGameManager<AdsManager>.GetInstance().ShowInterstitial();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            BaseSceneManager<UI>.Instance.WheelofFortune();//幸运轮
        }
        if (i < 10)
        {
            //UnityEngine.Object.Instantiate<GameObject>(this.finishPrefab, new Vector3(0f, ypos + (5 * i), 0f), Quaternion.identity);
            //yield return new WaitForSeconds(0.1f);
        }
        
    }

    //加分数的特效
    private IEnumerator plusCoroutine(int plusScore)
    {
        GameObject plusObj = null;
        GameObject plusAwesomeObj = null;
        plusObj = UnityEngine.Object.Instantiate<GameObject>(this.plusPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity, this.canvas.transform);
        plusObj.GetComponent<Animator>().Play("Base Layer.plus", 0, 0f);
        plusObj.transform.GetChild(0).GetComponent<Text>().text = "+" + plusScore.ToString();
        if ((this.scoreInRow == ((Base.currentLevel + 1) * 3)) || (this.scoreInRow == ((Base.currentLevel + 1) * 5)))
        {
            plusAwesomeObj = UnityEngine.Object.Instantiate<GameObject>(this.plusAwesomePrefab, new Vector3(300f, 0f, 0f), Quaternion.identity, this.canvas.transform);
            plusAwesomeObj.GetComponent<Animator>().Play("Base Layer.plus", 0, 0f);
        }
        yield return new WaitForSeconds(0.95f);//下一帧调用, 什么都不做
        //回收对象
        UnityEngine.Object.Destroy(plusObj);
        if (plusAwesomeObj != null)
        {
            UnityEngine.Object.Destroy(plusAwesomeObj);
        }
        yield return null; //下一帧调用, 什么都不做
    }
}