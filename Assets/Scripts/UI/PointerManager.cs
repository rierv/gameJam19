using UnityEngine;
using UnityEngine.UI;

public class PointerManager: MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color activeColor = Color.yellow;
    
    [SerializeField] private Image cursorImage;
    
    public void ActivateCursor(bool enabled)
    {
        if (enabled)
        {
            cursorImage.color = activeColor;
        }
        else
        {
            cursorImage.color = defaultColor;
        }
    }
}