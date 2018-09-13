using UnityEngine;

namespace Ping
{
    public class NoodleManager : MonoBehaviour
    {
        public static NoodleManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}