using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get;
        private set;
    }
    private Father father;

    private void Awake()
    {
        Instance = this;
        World.CreateInstance();
        WorldItems.CreateInstance();
        ObjectPool.ClearPool();
    }

    public void GenerateFather()
    {
        try
        {
            father = World.Instance.GenerateFather();
        }
        catch (ExitGUIException e)
        {
            
        }
    }

    public void RemoveFather()
    {
        ObjectPool.Add(father);
        father = null;
    }

    public bool HasFather()
    {
        return father != null;
    }
}