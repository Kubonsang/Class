using System;
using Class.StateMachine;
using Unity.VisualScripting;
using UnityEngine;

namespace Class
{
    public class SecondCameraController : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        enum order
        {
            main = 1,
            second = 2
        };
        private void Start()
        {
            Debug.Log("SecondCameraController Awake");
            ControllerThismanState().OnEnter += GameOverAction;
            SetCameraDepthOrder(order.main);
        }

        private ThismanState ControllerThismanState()
        {
            return GameManagerEx.Instance.Controller.thismanState;
        }

        private void GameOverAction()
        {
            this.transform.position = Camera.main.transform.position;
            LookAtSmileMan(ControllerThismanState().ThismanTransform);
            SetCameraDepthOrder(order.second);
        }

        public void LookAtSmileMan(Transform smileManTransform)
        {
            this.transform.SetParent(null);
            this.transform.LookAt(smileManTransform);
        }

        private void SetCameraDepthOrder(order order)
        {
            if (order == order.main)
            {
                Camera.main.depth = 1;
                this.GetComponent<Camera>().depth = 0;
            }
            else
            {
                Camera.main.depth = 0;
                this.GetComponent<Camera>().depth = 1;
            }
        }
        
    }
}