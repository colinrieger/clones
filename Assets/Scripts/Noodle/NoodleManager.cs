using System.Collections.Generic;
using UnityEngine;

namespace Noodle
{
    public class NoodleManager : MonoBehaviour
    {
        public GameObject MeatballPrefab;
        public Transform Canvas;
        public Transform TopWall;
        public Transform BottomWall;
        public Transform LeftWall;
        public Transform RightWall;

        public static NoodleManager Instance { get; private set; }

        private List<Vector3> m_Positions = new List<Vector3>();
        private List<Vector3> m_MeatballPositions = new List<Vector3>();

        private const int c_NumMeatballs = 4; 

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            Random.InitState(System.DateTime.Now.Millisecond);

            InitializePositions();
            InitializeMeatballs();
        }

        private void InitializePositions()
        {
            int minX = (int)(LeftWall.localPosition.x + (LeftWall.localScale.x / 2.0f));
            int maxX = (int)(RightWall.localPosition.x - (RightWall.localScale.x / 2.0f) - MeatballPrefab.transform.localScale.x);
            int minY = (int)(BottomWall.localPosition.y + (BottomWall.localScale.y / 2.0f));
            int maxY = (int)(TopWall.localPosition.y - (TopWall.localScale.y / 2.0f) - MeatballPrefab.transform.localScale.y);

            for (int x = minX; x <= maxX; x += (int)MeatballPrefab.transform.localScale.x)
                for (int y = minY; y <= maxY; y += (int)MeatballPrefab.transform.localScale.y)
                    m_Positions.Add(new Vector3(x, y));
        }

        private void InitializeMeatballs()
        {
            for (int i = 0; i < c_NumMeatballs; i++)
            {
                GameObject meatball = Instantiate(MeatballPrefab, Canvas) as GameObject;
                meatball.name = MeatballPrefab.name; // remove (Clone), for noodle trigger code
                RandomlyPlaceMeatball(meatball.transform);
            }
        }

        public void RandomlyPlaceMeatball(Transform meatball)
        {
            m_MeatballPositions.Remove(meatball.localPosition);
            Vector3 position;
            do
                position = m_Positions[Random.Range(0, m_Positions.Count)];
            while (position == meatball.localPosition || m_MeatballPositions.Contains(position));

            m_MeatballPositions.Add(position);
            meatball.localPosition = position;
        }
    }
}