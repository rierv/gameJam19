using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool
{
    private static ObjectPool _instance;
    private bool _initialized;
    private List<GameObject> pool;
    private List<GameObject> prefs;
    
    public static ObjectPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ObjectPool();
                _instance.Init();
            }

            return _instance;
        }
    }

    public static void ClearPool()
    {
        if (Instance.pool != null)
        {
            Instance.pool.Clear();
        }
    }

    private void Init()
    {
        pool = new List<GameObject>();
        prefs = new List<GameObject>();
        LoadPrefs();
        _initialized = true;
    }

    private void LoadPrefs()
    {
        List<Type> poolables = Context.Get<PoolableObjectAttribute>();
        foreach (var type in poolables)
        {
            var attribs = type.GetCustomAttributes(typeof(PoolableObjectAttribute), false);
            if (attribs != null && attribs.Length > 0)
            {
                var aMonoBehaviour = attribs[0] as PoolableObjectAttribute;
                var prefName = aMonoBehaviour.GetPrefLocation();
                var go = Resources.Load<GameObject>(prefName);
                if (go != null)
                {
                    prefs.Add(go);
                }
                else
                {
                    throw new SystemException(prefName + " is not a prefab");
                }
            }
            
        }
    }

    #region Private Methods

    private T InstantiateFromPrefs<T>(Transform parent) where T : MonoBehaviour
    {
        T temp = null;

        for (var i = 0; i < prefs.Count; i++)
            if (prefs[i] != null && prefs[i].GetComponent<T>() != null)
            {
                temp = Object.Instantiate(prefs[i], parent).GetComponent<T>();
                temp.name = temp.GetType().ToString();
                return temp;
            }

        return temp;
    }

    private T InstantiateFromPrefs<T>(Vector3 position) where T : MonoBehaviour
    {
        T temp = null;

        for (var i = 0; i < prefs.Count; i++)
            if (prefs[i] != null && prefs[i].GetComponent<T>() != null)
            {
                temp = Object.Instantiate(prefs[i], position, Quaternion.identity).GetComponent<T>();
                temp.name = temp.GetType().ToString();
                return temp;
            }

        return temp;
    }

    #endregion

    #region Add

    public static void Add(MonoBehaviour obj)
    {
        Instance._Add(obj.gameObject);
    }

    private void _Add(GameObject obj)
    {
        obj.SetActive(false);
        pool.Add(obj);
    }

    #endregion

    #region Get

    public static T Get<T>() where T : MonoBehaviour
    {
        return Instance._Get<T>(null);
    }

    public static T Get<T>(Transform parent) where T : MonoBehaviour
    {
        return Instance._Get<T>(parent);
    }

    public static T Get<T>(Vector3 position) where T : MonoBehaviour
    {
        return Instance._Get<T>(position);
    }

    private T _Get<T>(Transform parent) where T : MonoBehaviour
    {
        T temp;

        for (var i = 0; i < pool.Count; i++)
        {
            var component = pool[i].GetComponent<T>();
            if (component != null)
            {
                temp = component;
                pool.RemoveAt(i);
                temp.gameObject.SetActive(true);
                if (parent != null) temp.transform.SetParent(parent);

                temp.SendMessage("OnPooled", SendMessageOptions.DontRequireReceiver);
                return temp;
            }
        }

        temp = InstantiateFromPrefs<T>(parent);
        temp.SendMessage("OnPooled", SendMessageOptions.DontRequireReceiver);
        return temp;
    }

    private T _Get<T>(Vector3 position) where T : MonoBehaviour
    {
        T temp;

        for (var i = 0; i < pool.Count; i++)
        {
            var component = pool[i].GetComponent<T>();
            if (component != null)
            {
                temp = component;
                pool.RemoveAt(i);
                temp.gameObject.SetActive(true);
                if (position != null) temp.transform.position = position;

                temp.SendMessage("OnPooled", SendMessageOptions.DontRequireReceiver);
                return temp;
            }
        }

        temp = InstantiateFromPrefs<T>(position);
        temp.SendMessage("OnPooled", SendMessageOptions.DontRequireReceiver);
        return temp;
    }

    #endregion

    #region Remove

    public static bool Remove(GameObject obj)
    {
        return Instance._Remove(obj);
    }

    private bool _Remove(GameObject obj)
    {
        return pool.Remove(obj);
    }

    #endregion
}