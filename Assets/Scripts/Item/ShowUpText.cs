using UnityEngine;
using UnityEngine.UI;

// UI.Text.text example
//
// A Space keypress changes the message shown on the screen.
// Two messages are used.
//
// Inside Awake a Canvas and Text are created.

public class ShowUpText : MonoBehaviour
{
    private enum UpDown { Down = -1, Start = 0, Up = 1 };
    private Text text;
    public Player player;
    bool show=false;
    public Canvas canvasGO;
    Font arial;
    Canvas canvas;
    public Text TextGO;
    void Start()
    {
        
        
        
        canvas = canvasGO.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        text = TextGO.GetComponent<Text>();
        
        
        arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.font = arial;
        text.fontSize = 48;
        text.alignment = TextAnchor.MiddleCenter;
        show = false;
        text.text = "Press space key";
    }

    void Update()
    {

        // Press the space key to change the Text message.
        if (Input.GetKeyDown(KeyCode.I)&&Vector3.Distance(transform.position, player.transform.position)<10f&&show==false)
        {
           
           
            text.text = "Press space key";
            print(text.text);    
            show = true;
        }
        if (Input.GetKeyDown(KeyCode.I)&& show==true)
        {
            
            text.text = "";
            show = false;

        }
        
    }
}