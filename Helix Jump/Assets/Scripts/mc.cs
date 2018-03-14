using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mc : BaseSceneManager<mc>
{
    public GameType gameId;
    public bool isActive = true;
    public bool isGameStarted;

    private bool setUpvelocity;

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
    }

    private void LateUpdate()
    {
        if (this.setUpvelocity)
        {
            base.GetComponent<Rigidbody>().velocity = new Vector3(0f, 60f, 0f);
            this.setUpvelocity = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("OnCollisionEnter");

    }
}