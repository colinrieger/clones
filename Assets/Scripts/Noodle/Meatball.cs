using System.Collections.Generic;
using UnityEngine;

namespace Noodle
{
    public class Meatball : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            NoodleManager.Instance.RandomlyPlaceMeatball(transform);
        }
    }
}