using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    public Image GameImage;
    public Text GameTitle;
    public string GameScene;

    public void LoadGameScene()
    {
        SceneManager.LoadScene(GameScene);
    }
}