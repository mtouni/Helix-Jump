using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : BaseSceneManager<Base>
{
    public GameObject layers;
    //
    private float constructAngle;//初始化构建角度
    public float currentAngle;//当前角度
    private float currentAngleRotLocal;//当前角度破的地方
    public float currentAngleSpeed;//当前的角速度
    public float currentAngleTo;//当前的角度 向
    public static int currentLevel = 0;//当前的游戏等级
    public float currentYOffset;//当前Y轴的偏离距离
    //
    public List<GameObject> begin;//圆台列表：开始
    public List<int> beginCount;//圆台列表数量：开始
    public List<GameObject> easy;//圆台列表：简单难度
    public List<int> easyCount;//圆台列表数量：简单难度
    public List<GameObject> mid;//圆台列表：中等难度
    public List<int> midCount;//圆台列表数量：中等难度
    public List<GameObject> hard;//圆台列表：困难难度
    public List<int> hardCount;//圆台列表数量：困难难度

    public List<GameObject> platforms;//平台列表
    public GameObject finish;//结束的台面

    public AnimationCurve inputCurve;//输入曲线
    public GameObject mainBranch;//中心柱体
    public float maxRotSpeed;//最大角速度

    public float minSwipeDistX;//最小拖动距离：X
    public float minSwipeDistY;//最大拖动距离：X

    public float rotateSpeed;//旋转速度
    private List<float> speedHistory;//速度历史
    private Vector2 startPos;//开始点的坐标
    public GameObject reviveBlock;//恢复块


    // Use this for initialization
    void Start () {
		
    }

    private void Awake()
    {
        //原有代码
        Application.targetFrameRate = 60;
        this.speedHistory = new List<float>();

        //自己加的
        this.beginCount = new List<int>();
        this.beginCount.Add(5);
        this.easyCount = new List<int>();
        this.easyCount.Add(1);
        this.midCount = new List<int>();
        this.midCount.Add(1);
        this.hardCount = new List<int>();
        this.hardCount.Add(1);

        this.platforms = new List<GameObject>();

        this.currentYOffset = 0;
        this.currentAngleSpeed = 10;

        this.startPos = layers.transform.localPosition;
        this.rotateSpeed = 90;
        this.maxRotSpeed = 180;
        Debug.Log("startPos : " + this.startPos);


        //原有代码
        this.CreateLevel();
    }

    /**
     * 创建对应等级游戏
     **/
    public void CreateLevel()
    {
        Debug.Log("CreateLevel");
        this.constructAngle = -20f;//初始化角速度
        currentLevel = PlayerPrefs.GetInt("currentLevel");//获取当前等级
        int num = 1;
        if (BaseSceneManager<mc>.Instance.gameId == GameType.GAME_SHORT)
        {
            num = 3;
        }
        //总的平台数量
        int allPlatformNum = (this.beginCount[currentLevel] / num) + (this.easyCount[currentLevel] / num) + (this.midCount[currentLevel] / num) + (this.hardCount[currentLevel] / num);
        Debug.Log("allPlatformNum :" + allPlatformNum);
        for (int i = 0; i < (allPlatformNum + 1); i++)
        {
            if (i < (this.beginCount[currentLevel] / num))
            {
                //克隆实例，并返回克隆体
                this.platforms.Add(Object.Instantiate<GameObject>(this.begin[Random.Range(0, this.begin.Count)], new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
                this.constructAngle += Mathf.Sign((float)Random.Range(-5, 5)) * Random.Range(20, 60);
                //this.currentYOffset -= 15f;
                this.currentYOffset -= 5f;
                Debug.Log("克隆 :" + i + " ; currentYOffset : " + currentYOffset + " ; " + Random.Range(0, this.begin.Count));
            }
            else if (i < ((this.beginCount[currentLevel] / num) + (this.easyCount[currentLevel] / num)))
            {
                this.platforms.Add(Object.Instantiate<GameObject>(this.easy[Random.Range(0, this.easy.Count)], new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
                this.constructAngle += Mathf.Sign((float)Random.Range(-5, 5)) * Random.Range(60, 80);
                //this.currentYOffset -= 15f;
                this.currentYOffset -= 5f;
            }
            else if (i < (((this.beginCount[currentLevel] / num) + (this.easyCount[currentLevel] / num)) + (this.midCount[currentLevel] / num)))
            {
                this.constructAngle += Mathf.Sign((float)Random.Range(-5, 5)) * Random.Range(80, 120);
                this.platforms.Add(Object.Instantiate<GameObject>(this.mid[Random.Range(0, this.mid.Count)], new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
                //this.currentYOffset -= 17.5f;
                this.currentYOffset -= 5f;
            }
            else if (i < ((((this.beginCount[currentLevel] / num) + (this.easyCount[currentLevel] / num)) + (this.midCount[currentLevel] / num)) + (this.hardCount[currentLevel] / num)))
            {
                this.constructAngle += Mathf.Sign((float)Random.Range(-5, 5)) * Random.Range(90, 180);
                this.platforms.Add(Object.Instantiate<GameObject>(this.hard[Random.Range(0, this.hard.Count)], new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
                //this.currentYOffset -= 18f;
                this.currentYOffset -= 5f;
            }
            else
            {
                this.constructAngle += Mathf.Sign((float)Random.Range(-5, 5)) * Random.Range(90, 180);
                this.platforms.Add(Object.Instantiate<GameObject>(this.finish, new Vector3(0f, this.currentYOffset, 0f), Quaternion.Euler(new Vector3(0f, this.constructAngle, 0f)), base.transform));
            }
        }
        //初始化圆柱位置和放大比例
        //this.mainBranch.transform.position = new Vector3(0f, 45f + (this.currentYOffset / 2f), 0f);
        //this.mainBranch.transform.localScale = new Vector3(9.9f, (90f - this.currentYOffset) / 2f, 9.9f);
        //测试代码
        this.mainBranch.transform.localScale = new Vector3(4f, (this.currentYOffset), 4f);
    }

    //复活，恢复游戏
    public void Revive(int index)
    {
        this.currentAngle = 0f;
        this.platforms.Insert(index, Object.Instantiate<GameObject>(this.reviveBlock, new Vector3(0f, this.platforms[index].transform.position.y, 0f), Quaternion.Euler(new Vector3(0f, 50f, 0f)), base.transform));
    }

    // Update is called once per frame
    void Update () {
        if (BaseSceneManager<mc>.Instance.isActive)
        {
            this.UpdateInput();
            this.currentAngleSpeed = Mathf.Lerp(this.currentAngleSpeed, 0f, 5f * Time.deltaTime);
            this.currentAngle += this.currentAngleSpeed * Time.deltaTime;
            int num = (int)(this.currentAngle / 10f);
            this.currentAngleRotLocal = Mathf.Lerp(this.currentAngleRotLocal, (float)(num * 10), 20f * Time.deltaTime);
            //base.transform.localRotation = Quaternion.Euler(new Vector3(0f, this.currentAngle, 0f));
            base.transform.localRotation = Quaternion.Euler(new Vector3(0f, this.currentAngle, 0f));
            //Debug.Log("this.currentAngle : " + this.currentAngle);
        }
    }

    private void UpdateInput()
    {
        Vector3 vector2 = new Vector3(Input.mousePosition.x, 0f, 0f) - new Vector3(this.startPos.x, 0f, 0f);
        float num = Mathf.Clamp(vector2.magnitude, 0f, this.maxRotSpeed);
        float screenWidth = ((float)Screen.width);
        screenWidth = 720;
        float num2 = num / ((float)Screen.width);
        float item = (-Mathf.Sign(Input.mousePosition.x - this.startPos.x) * num2) * this.rotateSpeed;
        //Debug.Log("num : " + num + " ; num2 : " + num2);
        if (BaseSceneManager<mc>.Instance.isGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("GetMouseButtonDown 0 : " + Input.mousePosition);
                this.speedHistory.Clear();
                this.currentAngleSpeed = 0f;
                this.startPos = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                //Debug.Log("GetMouseButton 0 : " + Input.mousePosition);
                this.currentAngleSpeed = 0f;
                if (num2 > this.minSwipeDistX)
                {
                    this.speedHistory.Add(item);
                }
                else
                {
                    this.speedHistory.Add(0f);
                }
                if (this.speedHistory.Count > 4)
                {
                    this.speedHistory.RemoveAt(0);
                }
                this.currentAngle += item;
                this.startPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0) && (num > this.minSwipeDistX))
            {
                //Debug.Log("GetMouseButtonUp 0");
                float num4 = 0f;
                for (int i = 0; i < this.speedHistory.Count; i++)
                {
                    num4 += this.speedHistory[i];
                }
                this.currentAngleSpeed = 6f * num4;
                this.startPos = Input.mousePosition;
            }
        }
    }
}
