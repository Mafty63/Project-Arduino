using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectArduino
{
    public class PlayerController : MonoBehaviour
    {
        public float jumpForce = 5f;

        [SerializeField] private Rigidbody2D rb;
        private Vector3 lastMousePosition;

        private void Start()
        {
            lastMousePosition = Input.mousePosition; 
        }

        private  void Update()
        {
             Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

            if (mouseDelta.y > 0)
             {
               Jump();
           }

        lastMousePosition = Input.mousePosition;
        }

         private  void Jump()
         {
              rb.velocity = Vector2.up * jumpForce;
         }
    }
}
