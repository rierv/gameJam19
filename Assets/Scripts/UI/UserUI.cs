using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PointerManager))]
public class UserUI : MonoBehaviour
{
    public static UserUI Instance { get; private set; }
    
    private PointerManager pointerManager;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pointerManager = GetComponent<PointerManager>();
    }

    public void ActivateCursor(bool enabled)
    {
        pointerManager.ActivateCursor(enabled);
    }
}
