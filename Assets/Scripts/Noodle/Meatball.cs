using System.Collections.Generic;
using UnityEngine;

namespace Noodle
{
    public class Meatball : MonoBehaviour
    {
        public Transform TopWall;
        public Transform BottomWall;
        public Transform LeftWall;
        public Transform RightWall;
        
        private List<Vector3> m_Positions = new List<Vector3>();

        void Start()
        {
            Random.InitState(System.DateTime.Now.Millisecond);

            int minX = (int)(LeftWall.localPosition.x + (LeftWall.localScale.x / 2.0f));
            int maxX = (int)(RightWall.localPosition.x - (RightWall.localScale.x / 2.0f) - transform.localScale.x);
            int minY = (int)(BottomWall.localPosition.y + (BottomWall.localScale.y / 2.0f));
            int maxY = (int)(TopWall.localPosition.y - (BottomWall.localScale.y / 2.0f) - transform.localScale.y);

            for (int x = minX; x <= maxX; x += (int)transform.localScale.x)
                for (int y = minY; y <= maxY; y += (int)transform.localScale.y)
                    m_Positions.Add(new Vector3(x, y));
        }

        private void RandomlyPlace()
        {
            Vector3 position;
            do
                position = m_Positions[Random.Range(0, m_Positions.Count)];
            while (position == transform.localPosition);
            // don't put it back in the same spot
            // since the noodle has the same position (due to the collision), this doubles for noodle position check
            // if we place the meatball on a noodle tail, it will enter the trigger code (without growing) and attempt to place again

            gameObject.transform.localPosition = position;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            RandomlyPlace();
        }
    }
}