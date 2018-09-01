using UnityEngine;

namespace Ping
{
    public class Paddle : MonoBehaviour
    {
        public string InputName;

        private Rigidbody2D m_PaddleRigidBody;

        private const float m_PaddleSpeed = 6f;

        private void Awake()
        {
            m_PaddleRigidBody = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            float y = Input.GetAxisRaw(InputName);
            m_PaddleRigidBody.velocity = new Vector2(0, y) * m_PaddleSpeed;
        }
    }
}