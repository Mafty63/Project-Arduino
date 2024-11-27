using System.Collections;
using System.Collections.Generic;
using ProjectArduino.Interface;
using ProjectArduino.Managers;
using UnityEngine;
using Uduino;

namespace ProjectArduino.Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        public enum ControlType { Mouse_Delta, IMU_Delta }
        [SerializeField] private ControlType controlType = ControlType.Mouse_Delta;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private Rigidbody2D rb;
        private Vector3 lastMousePosition;

        private void Start()
        {
            if (GameManager.Instance.CurrentGameState == GameManager.GameState.GAMENOTSTARTED)
                rb.bodyType = RigidbodyType2D.Static;

            if (controlType == ControlType.Mouse_Delta)
                lastMousePosition = Input.mousePosition;
            else if (controlType == ControlType.IMU_Delta)
                UduinoManager.Instance.OnDataReceived += OnDataReceived;
        }

        private void Update()
        {
            if (GameManager.Instance.CurrentGameState != GameManager.GameState.GAMEOVER)
            {
                Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

                if (mouseDelta.y > 0)
                {
                    Jump();
                }

                lastMousePosition = Input.mousePosition;
            }
        }

        private void OnDataReceived(string data, UduinoDevice device)
        {
            if (GameManager.Instance.CurrentGameState != GameManager.GameState.GAMEOVER)
            {
                if (data.Contains("Delta Y : "))
                {
                    Jump();
                }
            }
        }

        private void Jump()
        {
            rb.velocity = Vector2.up * jumpForce;

            if (GameManager.Instance.CurrentGameState == GameManager.GameState.GAMESTARTED) return;

            GameManager.Instance.ChangeGameState(GameManager.GameState.GAMESTARTED);
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("Obstacle"))
            {
                GameManager.Instance.ChangeGameState(GameManager.GameState.GAMEOVER);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            InterfaceManager.Instance.Score.AddScore();
        }
    }
}
