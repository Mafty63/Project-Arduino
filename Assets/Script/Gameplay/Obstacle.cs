using UnityEngine;

namespace ProjectArduino.Gameplay
{
    public class Obstacle : MonoBehaviour
    {
        private float moveSpeed;

        public void SetSpeed(float speed) => moveSpeed = speed;

        private void Update()
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

            if (transform.position.x <= -10f)
            {
                ObjectPool.Instance.ReturnObject(gameObject);
            }
        }
    }
}