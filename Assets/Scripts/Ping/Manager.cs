using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ping
{
    public class Manager : MonoBehaviour
    {
        public Text LeftScoreText;
        public Text RightScoreText;
        public Text CountdownText;
        
        public int LeftScore { get { return m_LeftScore; } set { m_LeftScore = value; LeftScoreText.text = m_LeftScore.ToString(); } }
        public int RightScore { get { return m_RightScore; } set { m_RightScore = value; RightScoreText.text = m_RightScore.ToString(); } }

        public static Manager Instance { get { return m_Instance; } }

        private static Manager m_Instance;

        private int m_LeftScore = 0;
        private int m_RightScore = 0;
        private int m_CountdownValue = 0;

        private void Awake()
        {
            m_Instance = this;
        }

        public void StartCountdown(int countdownValue)
        {
            m_CountdownValue = countdownValue;

            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown()
        {
            while (m_CountdownValue > 0)
            {
                CountdownText.text = m_CountdownValue.ToString();
                m_CountdownValue--;
                yield return new WaitForSeconds(1f);
            }
            CountdownText.text = string.Empty;
        }
    }
}