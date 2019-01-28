using UnityEngine;

public class WorldItems
{
    private static WorldItems _instance;
    private Holdable[] _holdables;
    
    public static WorldItems Instance
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
        _instance = new WorldItems();
        _instance.UpdateItemsList();
    }

    public Holdable GetNearest(Vector3 position, float minDist)
    {
        Holdable holdable = null;
        float dist = Mathf.Infinity;
        
        foreach (Holdable i in _holdables)
        {
            dist = Vector3.Distance(i.transform.position, position);
            if (dist<minDist)
            {
                minDist = dist;
                holdable = i;
            }
        }

        return holdable;
    }
    
    public Holdable GetNearest(Vector3 position)
    {
        return GetNearest(position, Mathf.Infinity);
    }

    private void UpdateItemsList()
    {
        _holdables = GameObject.FindObjectsOfType<Holdable>();
    }
}
