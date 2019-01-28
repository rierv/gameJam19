using UnityEngine;
using UnityEngine.UI;

public class Photo : MonoBehaviour, IInteractable
{
    public Canvas HUD;
    public Text text;
    public Image image;

    private bool ignoreClick = false;

    private void OnTriggerEnter(Collider other)
    {
        text.text = "Press MouseSX!";
    }

    private void Update()
    {
        if (image.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                text.text = "Press MouseSx!";
                image.enabled = false;
                Player.Instance.GetMovement().enabled = true;
                return;
            }
        }
        else
        {
            ignoreClick = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text.text = "";
    }

    public void Interact()
    {
        if (!image.enabled)
        {
            Player.Instance.GetMovement().enabled = false;
            text.text = "";
            image.enabled = true;
            ignoreClick = true;
        }
    }

    public InteractionMode InteractionMode {
        get { return InteractionMode.INTERACT; }
    }
}
