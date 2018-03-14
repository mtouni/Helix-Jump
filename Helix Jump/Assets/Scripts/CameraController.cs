using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private bool isFolloving;
    public GameObject mc;
    public float Yoffset;
    // Methods


    void GoUp(){
        this.isFolloving = false;
        //base.StartCoroutine(this.GoUpCoroutine());
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.isFolloving)
        {
            base.transform.position = new Vector3(0f, Mathf.Min(this.mc.transform.position.y, base.transform.position.y - this.Yoffset) + this.Yoffset, -50f);
        }
    }
}
