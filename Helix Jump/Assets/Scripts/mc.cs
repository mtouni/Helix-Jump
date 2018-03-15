using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mc : BaseSceneManager<mc>
{
    public GameType gameId;
    public bool isActive = true;
    public bool isGameStarted;

    private bool setUpvelocity;

    public Transform currentPlatform;

    public List<GameObject> decal;

    public static int score;
    public int scoreInRow = 1;
    public int scoreNow = 3;
    public Animator anim;
    public AudioSource splash;

    // Use this for initialization
    void Start()
    {
        isGameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ForceDestroy(Transform platform)
    {
        this.setUpvelocity = true;
        this.setUpvelocity = true;
    }

    private void LateUpdate()
    {
        if (this.setUpvelocity)
        {
            //base.GetComponent<Rigidbody>().velocity = new Vector3(0f, 60f, 0f);
            base.GetComponent<Rigidbody>().velocity = new Vector3(0f, 6f, 0f);
            this.setUpvelocity = false;
        }
    }

    //发生碰撞
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("OnCollisionEnter");
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
                //this.Fail();
            }
        }
        if (other.collider.tag == "Finish")
        {
            //赢了
            //this.Win();
            this.scoreInRow = 0;
        }
        else if (this.currentPlatform != other.collider.transform) {
            this.currentPlatform = other.collider.transform;
            if (this.scoreInRow > ((Base.currentLevel + 1) * 3))
            {
                this.ForceDestroy(other.collider.transform.parent);
                this.scoreInRow = Base.currentLevel + 1;
            }
            else
            {
                this.scoreInRow = Base.currentLevel + 1;
                //GameObject obj2 = UnityEngine.Object.Instantiate<GameObject>(this.decal[UnityEngine.Random.Range(0, this.decal.Count)], new Vector3(base.transform.position.x, this.currentPlatform.transform.position.y + 1.5f, base.transform.position.z), Quaternion.identity, this.currentPlatform);
                //obj2.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
                //obj2.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, (float)UnityEngine.Random.Range(-180, 180)));
                //TapticManager.Impact(ImpactFeedback.Midium);
                //this.splash.Play();
                this.setUpvelocity = true;
                //this.anim.Play("Base Layer.Splash", 0, 0f);
            }
        }
        else
        {
            this.setUpvelocity = true;
        }
    }
}