using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Noodle
{
    public class Noodle : MonoBehaviour
    {
        public GameObject TailPrefab;

        private Vector3 m_Direction = Vector3.zero;
        private Queue<Vector3> m_InputQueue = new Queue<Vector3>();
        private Queue<Transform> m_TailQueue = new Queue<Transform>();
        private bool m_Grow = false;

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
            Vector3 lastInput = (m_InputQueue.Count > 0) ? m_InputQueue.Last() : m_Direction;
            
            if (lastInput == input)
                return;

            if (lastInput.x != 0f && input.x != 0f)
                return;

            if (lastInput.y != 0f && input.y != 0f)
                return;

            m_InputQueue.Enqueue(input);
        }

        private void ResetNoodle()
        {
            foreach (Transform tail in m_TailQueue.ToList())
            {
                GameObject.Destroy(tail.gameObject);
            }
            m_TailQueue.Clear();
            m_InputQueue.Clear();
            m_Direction = Vector3.zero;
            transform.position = Vector3.zero;
        }

        private void Move()
        {

            if (m_InputQueue.Count != 0)
                m_Direction = m_InputQueue.Dequeue();
            
            Vector3 lastPosition = transform.position;

            transform.localPosition += (transform.localScale.x * m_Direction);
            if (m_Grow)
            {
                GameObject newTail = Instantiate(TailPrefab, lastPosition, new Quaternion(), transform.parent) as GameObject;
                newTail.name = TailPrefab.name; // remove (Clone), for trigger code
                m_TailQueue.Enqueue(newTail.transform);
                m_Grow = false;
            }
            else if (m_TailQueue.Count > 0)
            {
                Transform tail = m_TailQueue.Dequeue();
                tail.position = lastPosition;
                m_TailQueue.Enqueue(tail);
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            switch (collider.gameObject.name)
            {
                case "TopWall":
                case "BottomWall":
                case "LeftWall":
                case "RightWall":
                case "Tail":
                    ResetNoodle();
                    break;
                case "Meatball":
                    m_Grow = true;
                    break;
                default:
                    break;
            }
        }

    }
}