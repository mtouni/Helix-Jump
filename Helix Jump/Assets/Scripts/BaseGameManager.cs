using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameManager<T> : MonoBehaviour where T : BaseGameManager<T>
{
    private static T s_instance;

    public static T GetInstance()
    {
        if (BaseGameManager<T>.s_instance == null)
        {
            GameObject original = Resources.Load(typeof(T).Name) as GameObject;
            if ((original == null) || (original.GetComponent<T>() == null))
            {
                Debug.LogError("Prefab for game manager " + typeof(T).Name + " is not found");
            }
            else
            {
                GameObject target = GameObject.Find("GameManagers");
                if (target == null)
                {
                    target = new GameObject("GameManagers");
                    UnityEngine.Object.DontDestroyOnLoad(target);
                }
                GameObject obj4 = UnityEngine.Object.Instantiate<GameObject>(original);
                obj4.transform.parent = target.transform;
                BaseGameManager<T>.s_instance = obj4.GetComponent<T>();
            }
        }
        return BaseGameManager<T>.s_instance;
    }
}
