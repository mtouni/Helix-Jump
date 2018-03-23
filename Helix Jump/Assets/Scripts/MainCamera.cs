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
    [Tooltip("摄像机Y方向补偿距离")]
    public float Yoffset;

    public void GoUp()
    {
        this.isFolloving = false;
        base.StartCoroutine(this.GoUpCoroutine());//协同进行
    }

    // Use this for initialization
    void Start () {
		
	}

    private void Update()
    {
        if (this.isFolloving)
        {
            //镜头跟随
            base.transform.position = new Vector3(0f, Mathf.Min(this.mc.transform.position.y, base.transform.position.y - this.Yoffset) + this.Yoffset, -50f);
        }
    }

    //上升
    private IEnumerator GoUpCoroutine()
    {
        float time = 1f;
        if (time > 0f)
        {
            time -= Time.deltaTime;
            Transform transform = this.transform;
            transform.position += (Vector3)((Vector3.up * Time.deltaTime) * 15f);
        }
        yield return null;
    }
}
