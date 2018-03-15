using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 摄像机管理类 
*/
public class MainCamera : BaseSceneManager<MainCamera>
{
    private bool isFolloving = true;
    public GameObject mc;
    public float Yoffset;


    public void GoUp()
    {
        this.isFolloving = false;
        //base.StartCoroutine(this.GoUpCoroutine());//协同进行
    }

    // Use this for initialization
    void Start () {
		
	}

    private void Update()
    {
        if (this.isFolloving)
        {
            base.transform.position = new Vector3(0f, Mathf.Min(this.mc.transform.position.y, base.transform.position.y - this.Yoffset) + this.Yoffset, -50f);
        }
    }
}
