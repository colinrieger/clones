using UnityEngine;
using UnityEngine.UI;

namespace Ping
{
    public class Manager : MonoBehaviour
    {
        public Text LeftScoreText;
        public Text RightScoreText;
        
        public int LeftScore { get { return m_LeftScore; } set { m_LeftScore = value; LeftScoreText.text = m_LeftScore.ToString(); } }
        public int RightScore { get { return m_RightScore; } set { m_RightScore = value; RightScoreText.text = m_RightScore.ToString(); } }

        public static Manager Instance { get { return m_Instance; } }

        private static Manager m_Instance;

        private int m_LeftScore = 0;
        private int m_RightScore = 0;

        private void Awake()
        {
            m_Instance = this;
        }
    }
}