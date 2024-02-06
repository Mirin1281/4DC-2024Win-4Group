using UnityEngine;

namespace Mirin
{
    public class ObjectWall : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.SetActive(false);
        }
    }
}