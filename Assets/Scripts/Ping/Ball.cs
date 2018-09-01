using System.Collections.Generic;
using UnityEngine;

namespace Ping
{
    public class Ball : MonoBehaviour
    {
        private Rigidbody2D m_BallRigidBody;
        private AudioSource m_AudioSource;
        private AudioClip m_HitGoalClip;
        private AudioClip m_HitPaddleClip;
        private AudioClip m_HitWallClip;

        private const float c_BallSpeed = 10f;

        private List<Vector2> m_StartingVelocityVectors = new List<Vector2>()
        {
            new Vector2(1, 1), new Vector2(-1, 1), new Vector2(1, -1), new Vector2(-1, -1),
            new Vector2(2, 1), new Vector2(-2, 1), new Vector2(2, -1), new Vector2(-2, -1),
            new Vector2(1, 2), new Vector2(-1, 2), new Vector2(1, -2), new Vector2(-1, -2),
        };

        private void Awake()
        {
            m_BallRigidBody = GetComponent<Rigidbody2D>();
            m_AudioSource = GetComponent<AudioSource>();
            m_HitGoalClip = Resources.Load<AudioClip>("Ping/hit_goal");
            m_HitPaddleClip = Resources.Load<AudioClip>("Ping/hit_paddle");
            m_HitWallClip = Resources.Load<AudioClip>("Ping/hit_wall");
        }

        void Start()
        {
            Random.InitState(System.DateTime.Now.Millisecond);

            ResetBall();
        }

        void OnCollisionEnter2D(Collision2D collider)
        {
            switch (collider.gameObject.name)
            {
                case "LeftPaddle":
                    {
                        float y = (transform.position.y - collider.transform.position.y) / collider.collider.bounds.size.y;
                        SetVelocityVector(new Vector2(1, y));
                        PlaySound(m_HitPaddleClip);
                    }
                    break;
                case "RightPaddle":
                    {
                        float y = (transform.position.y - collider.transform.position.y) / collider.collider.bounds.size.y;
                        SetVelocityVector(new Vector2(-1, y));
                        PlaySound(m_HitPaddleClip);
                    }
                    break;
                case "LeftGoal":
                    {
                        Manager.Instance.RightScore += 1;
                        ResetBall();
                        PlaySound(m_HitGoalClip);
                    }
                    break;
                case "RightGoal":
                    {
                        Manager.Instance.LeftScore += 1;
                        ResetBall();
                        PlaySound(m_HitGoalClip);
                    }
                    break;
                case "TopWall":
                case "BottomWall":
                    {
                        PlaySound(m_HitWallClip);
                    }
                    break;
                default:
                    break;
            }
        }

        private void SetRandomStartingVelocityVector()
        {
            int index = Random.Range(0, m_StartingVelocityVectors.Count);
            Vector2 vector = m_StartingVelocityVectors[index];
            SetVelocityVector(vector);
        }

        private void SetVelocityVector(Vector2 velocityVector)
        {
            m_BallRigidBody.velocity = velocityVector.normalized * c_BallSpeed;
        }

        private void ResetBall()
        {
            transform.position = Vector2.zero;
            m_BallRigidBody.velocity = Vector2.zero;

            Invoke("SetRandomStartingVelocityVector", 2f);
        }

        private void PlaySound(AudioClip audioClip)
        {
            m_AudioSource.clip = audioClip;
            m_AudioSource.Play();
        }
    }
}