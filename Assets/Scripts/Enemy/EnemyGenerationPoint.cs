using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerationPoint : MonoBehaviour
{
    public Father GenerateFather()
    {
        Debug.Log("Generate");
        Father father = ObjectPool.Get<Father>(transform.position);
        father.transform.rotation = transform.rotation;
        return father;
    }
}
