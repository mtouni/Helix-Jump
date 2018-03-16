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
    public GameObject AddMoneyRestart;
    public AudioSource AddMoneyRestartSound;
    public Text AddMoneyRestartText;
    public Animator anim;
    public GameObject AnimatedButton;//
    public Text best;//Text：最好成绩
    public GameObject bestUI;//最好成绩的UI
    //
    public GameObject canvas;//画布
    //
    public int currency;//金币
    public List<int> currencyList;//货币列表
    public Text currencyText;//文本：当前文本
    public GameObject currencyUI;//UI：当前金币
    //
    public int currentObjectLevel;//当前等级
    public Transform currentPlatform;//当前平台坐标
    public int currentPlayformId;//当前平台ID
    public AudioSource death;//音效：死亡
    public List<GameObject> decal;//贴花纸
    public ParticleSystem extraSplash;
    public GameObject finishPrefab;//完成比赛的Prefab
    public GameType gameId;
    public bool isActive = true;
    public bool isGameStarted;
    public Text levelFrom;
    public Image LevelPassedPin;
    public Text levelText;//文字：过关，等级提升
    public Text levelTo;
    public GameObject levelUpButton;
    public Text levelUpText;
    public PhysicMaterial mat;//物理反弹力
    public Material mcMat;
    public Text newRecord;
    public List<GameObject> objects;//游戏块
    public AudioSource pass;//音乐播放：通过
    public GameObject plusAwesomePrefab;
    public GameObject plusPrefab;
    public Image progression;
    public ParticleSystem psBurn;//粒子效果：燃烧
    public ParticleSystem psBurn1;//粒子效果：燃烧1
    public GameObject psPickup;
    public ParticleSystem psSplash; ///粒子效果
    public GameObject restartMenu;/// 重新开始菜单（完成）
    public Text restartPercentage;
    private bool reviveShown;
    private static int score = 0;//分数
    public int scoreInRow = 1;
    public int scoreNow = 3;
    //
    public Text scoreNowText;
    public Text scoreText;//当前分数
    private int sessionsCount;
    private bool setUpvelocity;//是否需要弹起来
    public AudioSource splash;//音乐播放：球落地水花溅开
    public float startDrag;//开始时候的阻力：
    public float finalDrag;//结束时候的阻力：
    private string[] versionNames = new string[] { "Base", "WorldBuilder", "ShorterLevels", "Animations" };//版本名称
    public GameObject winMenu;//获胜界面

    // Use this for initialization
    private void Start()
    {
        //自己加的
        isGameStarted = true;
        //原本的
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
        //this.levelFrom.text = (Base.currentLevel + 1).ToString();
        //this.levelTo.text = (Base.currentLevel + 2).ToString();
        ////FB.Init(new InitDelegate(this.InitCallback), null, null);
        //this.SocialAuthenticate();
        //VoodooSauce.OnGameStarted();
    }


    // Update is called once per frame
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
        //    this.pass.pitch = 1f + ((this.scoreInRow * 0.2f) / ((float)(Base.currentLevel + 1)));
        //    this.pass.Play();
        //    base.StartCoroutine(this.destroyLayerCoroutine(BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId]));
        //    BaseSceneManager<UI>.Instance.Play();
        //    this.bestUI.SetActive(false);
        //    if ((this.currentPlatform == null) || (this.currentPlatform.transform.parent != BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId].transform))
        //    {
        //        UnityEngine.Object.Instantiate<GameObject>(this.psPickup, BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId].transform.position, Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
        //        this.currentPlayformId++;
        //        base.StartCoroutine(this.plusCoroutine(this.scoreInRow));
        //        score += this.scoreInRow;
        //        this.scoreInRow += Base.currentLevel + 1;
        //    }
        //    else
        //    {
        //        UnityEngine.Object.Instantiate<GameObject>(this.psPickup, this.currentPlatform.transform.position, Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
        //        this.currentPlayformId++;
        //        this.scoreInRow = Base.currentLevel + 1;
        //        base.StartCoroutine(this.plusCoroutine(this.scoreInRow));
        //        score += this.scoreInRow;
        //    }
        //    this.currentPlatform = null;
        }
        //if (this.scoreInRow > ((Base.currentLevel + 1) * 2))
        //{
        //    this.psBurn.gameObject.SetActive(true);
        //    this.psBurn1.gameObject.SetActive(true);
        //}
        //else
        //{
        //    this.psBurn.gameObject.SetActive(false);
        //    this.psBurn1.gameObject.SetActive(false);
        //}
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
        //BaseGameManager<AdsManager>.GetInstance();
        //for (int i = 0; i < 4; i++)
        //{
            //if (this.versionNames[i] == VoodooSauce.GetPlayerCohort())
            //{
            //    this.gameId = (GameType)i;
            //}
        //}
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
        //this.death.Play();
        //base.transform.localScale = new Vector3(2.3f, 1.7f, 2.3f);
        //this.restartPercentage.text = ((int)((BaseSceneManager<mc>.Instance.currentPlayformId * 100f) / ((float)(BaseSceneManager<Base>.Instance.platforms.Count - 1)))).ToString("D") + "% Completed!";
        //if (PlayerPrefs.HasKey("sessionsCount"))
        //{
        //    this.sessionsCount = PlayerPrefs.GetInt("sessionsCount");
        //}
        //if (((this.sessionsCount == 5) || (this.sessionsCount == 0x19)) || (((this.sessionsCount == 100) || (this.sessionsCount == 200)) || (this.sessionsCount == 500)))
        //{
        //    BaseSceneManager<UI>.Instance.StartRateUs();
        //}
        //this.bestUI.SetActive(false);
        //if (!this.reviveShown)
        //{
        //    this.reviveShown = true;
        //    BaseSceneManager<UI>.Instance.ShowRevive();
        //}
        //this.ReportScore((float)score);
        ////VoodooSauce.OnGameFinished((float)score);
        //if (PlayerPrefs.GetInt("bestScore") < score)
        //{
        //    this.newRecord.gameObject.SetActive(true);
        //    PlayerPrefs.SetInt("bestScore", score);
        //    this.newRecord.text = "NEW RECORD!\n" + score.ToString();
        //}
    }

    public void ForceDestroy(Transform platform)
    {
        //this.extraSplash.Play();
        //base.StartCoroutine(this.plusCoroutine(this.scoreInRow));
        score += this.scoreInRow;
        //UnityEngine.Object.Instantiate<GameObject>(this.psPickup, BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId].transform.position, Quaternion.Euler(new Vector3(-90f, 0f, 0f))).GetComponent<ParticleSystem>().Play();
        //for (int i = 0; i < BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId].transform.childCount; i++)
        //{
        //    BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId].transform.GetChild(i).GetComponent<MeshRenderer>().material = this.mcMat;
        //}
        //base.StartCoroutine(this.destroyLayerCoroutine(BaseSceneManager<Base>.Instance.platforms[this.currentPlayformId]));
        //TapticManager.Impact(ImpactFeedback.Midium);
        this.splash.Play();
        this.setUpvelocity = true;
        //this.anim.Play("Base Layer.Splash", 0, 0f);
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
            //base.GetComponent<Rigidbody>().velocity = new Vector3(0f, 60f, 0f);
            base.GetComponent<Rigidbody>().velocity = new Vector3(0f, 7f, 0f);
            this.setUpvelocity = false;
        }
    }

    public void LevelUp()
    {
        if (this.currency >= this.currencyList[this.currentObjectLevel])
        {
            if (this.currentObjectLevel >= this.objects.Count)
            {
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
        else if (this.currentPlatform != other.collider.transform) {
            //当前位置不等于碰撞的坐标
            this.currentPlatform = other.collider.transform;
            if (this.scoreInRow > ((Base.currentLevel + 1) * 3))
            {
                this.ForceDestroy(other.collider.transform.parent);
                this.scoreInRow = Base.currentLevel + 1;
            }
            else
            {
                this.scoreInRow = Base.currentLevel + 1;
                //贴图
                GameObject obj2 = UnityEngine.Object.Instantiate<GameObject>(this.decal[UnityEngine.Random.Range(0, this.decal.Count)], new Vector3(base.transform.position.x, this.currentPlatform.transform.position.y + 1.5f, base.transform.position.z), Quaternion.identity, this.currentPlatform);
                obj2.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
                obj2.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, (float)UnityEngine.Random.Range(-180, 180)));
                //震动
                //TapticManager.Impact(ImpactFeedback.Midium);
                this.splash.Play();
                this.setUpvelocity = true;
                //this.anim.Play("Base Layer.Splash", 0, 0f);
            }
        }
        else
        {
            UnityEngine.Debug.Log("还在当前台面");
            this.scoreInRow = Base.currentLevel + 1;
            //贴图
            GameObject obj3 = UnityEngine.Object.Instantiate<GameObject>(this.decal[UnityEngine.Random.Range(0, this.decal.Count)], new Vector3(base.transform.position.x, this.currentPlatform.transform.position.y + 1.5f, base.transform.position.z), Quaternion.identity, this.currentPlatform);
            obj3.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            obj3.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, (float)UnityEngine.Random.Range(-180, 180)));
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
                this.splash.Play();
            }
        }
        //this.psSplash.Play();
    }

    public void PostCallback(bool success)
    {
        UnityEngine.Debug.Log("Post finished: " + success.ToString());
    }

    private void ReportScore(float score)
    {
        UnityEngine.Debug.Log("Report Score: " + score.ToString());
        if (!Application.isEditor)
        {
        }
    }

    public void Restart()
    {
        //base.StartCoroutine(this.AddMoneyRestartCoroutine());
    }

    public void Revive()
    {
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
        //base.StartCoroutine(this.NextLevel());
    }
}