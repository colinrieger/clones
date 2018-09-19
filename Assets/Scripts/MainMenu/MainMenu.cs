using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject GameButtonPrefab;

    private GameObject m_GameScrollViewContent;

    private bool m_GameScrollViewSized = false;

    private struct GameData
    {
        public string Title;
        public string ImagePath;
        public string Scene;
    }
    private List<GameData> m_GameData = new List<GameData>()
    {
        new GameData() { Title = "Ping", ImagePath = "MainMenu/ping", Scene = "Ping" },
        new GameData() { Title = "Noodle", ImagePath = "MainMenu/noodle", Scene = "Noodle" },
    };

    private void Awake()
    {
        m_GameScrollViewContent = transform.Find("GameScrollView/Viewport/Content").gameObject;
    }

    void Update()
    {
        if (!m_GameScrollViewSized)
            SizeGameScrollView();
    }

    void SizeGameScrollView()
    {
        // the scroll view takes a few frames to size
        if (m_GameScrollViewContent.GetComponent<RectTransform>().rect.width == 0)
            return;

        RectTransform rectTransform = m_GameScrollViewContent.GetComponent<RectTransform>();
        GridLayoutGroup gridLayoutGroup = m_GameScrollViewContent.GetComponent<GridLayoutGroup>();
        int columns = gridLayoutGroup.constraintCount;
        int rows = (m_GameData.Count + columns - 1) / columns;
        float viewWidth = rectTransform.rect.width - (gridLayoutGroup.spacing.x * (columns - 1));
        float buttonWidth = viewWidth / columns;
        float viewHeight = (buttonWidth * rows) + (gridLayoutGroup.spacing.y * (rows - 1));
        m_GameScrollViewContent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(buttonWidth, buttonWidth);
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, viewHeight);

        foreach (GameData gameData in m_GameData)
            AddGameButton(gameData);

        m_GameScrollViewSized = true;
    }

    void AddGameButton(GameData gameData)
    {
        GameButton gameButton = Instantiate(GameButtonPrefab, m_GameScrollViewContent.transform).GetComponent<GameButton>();
        gameButton.GameTitle.text = gameData.Title;
        gameButton.GameImage.sprite = Resources.Load<Sprite>(gameData.ImagePath);
        gameButton.GameScene = gameData.Scene;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}