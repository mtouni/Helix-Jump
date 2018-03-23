using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : BaseSceneManager<Base>
{
    //私有变量
    private float constructAngle;//构建角度
    private List<float> speedHistory;//速度历史
    private Vector2 startPosition;//开始点的坐标
    //公开变量
    [Header("基础信息（测试预览）")]
    [Tooltip("当前角度")]
    public float currentAngle;
    //private float currentAngleRotLocal;//当前角度旋转
    [Tooltip("当前的角速度")]
    public float currentAngleSpeed;//当前的角速度
    [Tooltip("当前的游戏等级")]
    public static int currentLevel = 0;//当前的游戏等级
    public float currentYOffset;//当前Y轴的偏离距离
    [Header("圆台")]
    public List<GameObject> platforms;//平台列表

    [Header("圆台模型配置")]
    public List<GameObject> beginModels;//圆台模型：开始
    public List<GameObject> easyModels;//圆台模型：简单难度
    public List<GameObject> midModels;//圆台模型：中等难度
    public List<GameObject> hardModels;//圆台模型：困难难度
    public GameObject finish;//结束的台面
    [Header("圆台数量配置（按等级）")]
    public List<int> beginCountConfig;//圆台数量配置：开始
    public List<int> easyCountConfig;//圆台数量配置：简单难度 
    public List<int> midCountConfig;//圆台数量配置：中等难度
    public List<int> hardCountConfig;//圆台数量配置：困难难度
    [Tooltip("中心柱体")]
    public GameObject mainBranch;//中心柱体
    [Tooltip("恢复比赛后第一块平台")]
    public GameObject reviveBlock;//恢复块

    [Header("参数")]
    [Tooltip("最大旋转速度")]
    public float maxRotateSpeed;//最大旋转速度
    [Tooltip("旋转速度")]
    public float rotateSpeed;//旋转速度
    public float minSwipeDistX;//最小拖动距离：X
    public float minSwipeDistY;//最小拖动距离：X

    private void Awake()
    {
        //设置一个特定的帧率渲染
        Application.targetFrameRate = 60;
        this.speedHistory = new List<float>();
        this.platforms = new List<GameObject>();
        //创建对应等级的游戏
        this.CreateLevel();
    }

    /**
     * 创建对应等级游戏
     **/
    public void CreateLevel()
    {
        this.constructAngle = -20f;//角度
        currentLevel = PlayerPrefs.GetInt("currentLevel");//获取当前等级
        Debug.Log("CreateLevel , level : " + currentLevel);
        int num = 1;
        if (BaseSceneManager<mc>.Instance.gameId == GameType.GAME_SHORT)
        {
            num = 3;
        }
        //总的平台数量 = 开始难度的数量 + 简单难度的数量 + 中等难度数量 + 困难难度的数量
        int allPlatformNum = (this.beginCountConfig[currentLevel] / num) + (this.easyCountConfig[currentLevel] / num) + (this.midCountConfig[currentLevel] / num) + (this.hardCountConfig[currentLevel] / num);
        Debug.Log("总的层数 : " + allPlatformNum);
        //创建所有平台
        for (int i = 0; i < (allPlatformNum + 1); i++)
        {
            if (i < (this.beginCountConfig[currentLevel] / num))
            {
                this.platforms.Add(Object.Instantiate<GameObject>(this.beginModels[Random.Range(0, this.beginModels.Count)], new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
                this.constructAngle += Mathf.Sign((float)Random.Range(-5, 5)) * Random.Range(20, 60);
                this.currentYOffset -= 15f;
            }
            else if (i < ((this.beginCountConfig[currentLevel] / num) + (this.easyCountConfig[currentLevel] / num)))
            {
                this.platforms.Add(Object.Instantiate<GameObject>(this.easyModels[Random.Range(0, this.easyModels.Count)], new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
                this.constructAngle += Mathf.Sign((float)Random.Range(-5, 5)) * Random.Range(60, 80);
                this.currentYOffset -= 15f;
            }
            else if (i < (((this.beginCountConfig[currentLevel] / num) + (this.easyCountConfig[currentLevel] / num)) + (this.midCountConfig[currentLevel] / num)))
            {
                this.constructAngle += Mathf.Sign((float)Random.Range(-5, 5)) * Random.Range(80, 120);
                this.platforms.Add(Object.Instantiate<GameObject>(this.midModels[Random.Range(0, this.midModels.Count)], new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
                this.currentYOffset -= 17.5f;
            }
            else if (i < ((((this.beginCountConfig[currentLevel] / num) + (this.easyCountConfig[currentLevel] / num)) + (this.midCountConfig[currentLevel] / num)) + (this.hardCountConfig[currentLevel] / num)))
            {
                this.constructAngle += Mathf.Sign((float)Random.Range(-5, 5)) * Random.Range(90, 180);
                this.platforms.Add(Object.Instantiate<GameObject>(this.hardModels[Random.Range(0, this.hardModels.Count)], new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
                this.currentYOffset -= 18f;
            }
            else
            {
                this.constructAngle += Mathf.Sign((float)Random.Range(-5, 5)) * Random.Range(90, 180);
                this.platforms.Add(Object.Instantiate<GameObject>(this.finish, new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
            }
        }
        //初始化圆柱位置和放大比例
        this.mainBranch.transform.position = new Vector3(0f, 45f + (this.currentYOffset / 2f), 0f);
        this.mainBranch.transform.localScale = new Vector3(9.9f, (90f - this.currentYOffset) / 2f, 9.9f);
    }

    //复活，恢复游戏
    public void Revive(int index)
    {
        this.currentAngle = 0f;
        //添加保护平台
        this.platforms.Insert(index, Object.Instantiate<GameObject>(this.reviveBlock, new Vector3(0f, this.platforms[index].transform.position.y, 0f), Quaternion.Euler(new Vector3(0f, 50f, 0f)), base.transform));
    }
    
    void Update()
    {
        if (BaseSceneManager<mc>.Instance.isActive)
        {
            this.UpdateInput();
            //当前角速度
            this.currentAngleSpeed = Mathf.Lerp(this.currentAngleSpeed, 0f, 5f * Time.deltaTime);
            //当前角度
            this.currentAngle += this.currentAngleSpeed * Time.deltaTime;
            //
            //int num = (int)(this.currentAngle / 10f);
            //this.currentAngleRotLocal = Mathf.Lerp(this.currentAngleRotLocal, (float)(num * 10), 20f * Time.deltaTime);
            base.transform.localRotation = Quaternion.Euler(new Vector3(0f, this.currentAngle, 0f));
        }
    }

    //手势输入
    private void UpdateInput()
    {
        //手势移动的向量（只计算X轴）
        Vector3 moveVector = new Vector3(Input.mousePosition.x, 0f, 0f) - new Vector3(this.startPosition.x, 0f, 0f);
        //移动向量长度（最小0，最大maxRotateSpeed）
        float moveX = Mathf.Clamp(moveVector.magnitude, 0f, this.maxRotateSpeed);
        //屏幕宽度
        float screenWidth = ((float)Screen.width);
        Debug.Log("屏幕宽度 : " + screenWidth);
        //X方向移动占屏幕总宽度的百分比
        float moveXPercent = moveX / screenWidth;
        //获取到移动( 移动的X位置)
        float speed = (-Mathf.Sign(Input.mousePosition.x - this.startPosition.x) * moveXPercent) * this.rotateSpeed;
        if (BaseSceneManager<mc>.Instance.isGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //鼠标左键按下
                this.speedHistory.Clear();
                this.currentAngleSpeed = 0f;
                this.startPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                //鼠标左键按着
                this.currentAngleSpeed = 0f;
                if (moveXPercent > this.minSwipeDistX)
                {
                    this.speedHistory.Add(speed);
                }
                else
                {
                    this.speedHistory.Add(0f);
                }
                if (this.speedHistory.Count > 4)
                {
                    this.speedHistory.RemoveAt(0);
                }
                this.currentAngle += speed;
                this.startPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0) && (moveX > this.minSwipeDistX))
            {
                //鼠标左键抬起
                float speedX = 0f;
                for (int i = 0; i < this.speedHistory.Count; i++)
                {
                    speedX += this.speedHistory[i];
                }
                this.currentAngleSpeed = 6f * speedX;
                this.startPosition = Input.mousePosition;
            }
        }
    }
}
