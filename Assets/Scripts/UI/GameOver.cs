using UnityEngine;
using UnityEngine.SceneManagement;

[PoolableObject("poolable/GameOver")]
public class GameOver : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnRestart()
    {
        SceneManager.LoadScene("house");
    }
}
