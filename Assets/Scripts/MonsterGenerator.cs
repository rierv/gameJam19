using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject monster;

    public static MonsterGenerator Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;
    }

    public static GameObject GenerateMonster(Transform trans)
    {
        return Instantiate(Instance.monster, trans);
    }
}
