using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class World
{
    private static World _instance;
    public static World Instance
    {
        get
        {
            if (_instance == null)
            {
                CreateInstance();
            }

            return _instance;
        }
    }

    public static void CreateInstance()
    {
        _instance = new World();
        _instance.LoadFatherGenerationPoints();
    }

    private EnemyGenerationPoint[] _enemyGenerationPoints;

    private void LoadFatherGenerationPoints()
    {
        _enemyGenerationPoints = GameObject.FindObjectsOfType<EnemyGenerationPoint>();
    }

    public Father GenerateFather()
    {
        if (_enemyGenerationPoints != null && _enemyGenerationPoints.Length > 0)
        {
            int rand = Random.Range(0, _enemyGenerationPoints.Length);
//            return MonsterGenerator.GenerateMonster(_enemyGenerationPoints[rand].transform).GetComponent<Father>();
            return _enemyGenerationPoints[rand].GenerateFather();

        }
        return null;
        //throw new Exception("There is no generation points for father/evil/enemy/you/housemate!");
    }
}