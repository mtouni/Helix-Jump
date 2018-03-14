using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSceneManager<T> : MonoBehaviour where T : BaseSceneManager<T>
{
    // Fields
    private static T s_instance;

    // Properties
    public static T Instance
    {
        get
        {
            if (s_instance == null)
            {
                Debug.Log("Instance new");
                s_instance = FindObjectOfType<T>();
            }
            if (FindObjectsOfType<T>().Length > 1)
            {
                Debug.Log("More than 1!");
                return s_instance;
            }
            if (s_instance == null)
            {
                string instanceName = typeof(T).Name;
                //Debug.Log("Instance Name: " + instanceName);
                GameObject instanceGO = GameObject.Find(instanceName);
                if (instanceGO == null)
                    instanceGO = new GameObject(instanceName);
                s_instance = instanceGO.AddComponent<T>();
                DontDestroyOnLoad(instanceGO);  //保证实例不会被释放
               //Debug.Log("Add New Singleton " + s_instance.name + " in Game!");
            }
            else
            {
                //Debug.Log("Already exist: " + s_instance.name);
            }
            return s_instance;
        }
    }

    protected virtual void OnDestroy()
    {
        s_instance = null;
    }
}