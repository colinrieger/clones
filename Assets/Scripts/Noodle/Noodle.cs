using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Noodle
{
    public class Noodle : MonoBehaviour
    {
        private Vector3 m_Direction = Vector3.zero;

        private Queue<Vector3> m_InputQueue = new Queue<Vector3>();

        private const float m_TimeStep = .2f;

        void Start()
        {
            InvokeRepeating("Move", m_TimeStep, m_TimeStep);
        }

        void Update()
        {
            float verticalInputValue = Input.GetAxisRaw("Vertical");
            float horizontalInputValue = Input.GetAxisRaw("Horizontal");

            if (verticalInputValue != 0f)
                AddInput((verticalInputValue > 0) ? Vector3.up : Vector3.down);
            else if (horizontalInputValue != 0f)
                AddInput((horizontalInputValue > 0) ? Vector3.right : Vector3.left);
        }

        private void AddInput(Vector3 input)
        {
            Vector3 inputCompare = (m_InputQueue.Count > 0) ? m_InputQueue.Last() : m_Direction;
            
            if (inputCompare == input)
                return;

            if (inputCompare.x != 0f && input.x != 0f)
                return;

            if (inputCompare.y != 0f && input.y != 0f)
                return;

            m_InputQueue.Enqueue(input);
        }

        private void ResetNoodle()
        {
            m_InputQueue.Clear();
            m_Direction = Vector3.zero;
            transform.position = Vector3.zero;
        }

        private void Move()
        {
            if (m_InputQueue.Count != 0)
                m_Direction = m_InputQueue.Dequeue();
            
            transform.localPosition += (transform.localScale.x * m_Direction);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            switch (collider.gameObject.name)
            {
                case "TopWall":
                case "BottomWall":
                case "LeftWall":
                case "RightWall":
                    {
                        ResetNoodle();
                    }
                    break;
                default:
                    break;
            }
        }

    }
}