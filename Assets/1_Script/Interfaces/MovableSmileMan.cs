using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Class
{
    public abstract class MovableSmileMan : ISmileMan
    {

        private Vector3 direction;
        
        
        #region References
        [SerializeField] private GameObject ParentOfPoints;
        [SerializeField] private float velocity = 3f;
        #endregion
        
        #region private variables

        private point[] points;
        private int indexOfPoint = 0;
        #endregion
        
        protected bool isMoving = false;
        protected bool IsWalking = false;
        
        private void Awake()
        {
            points = ParentOfPoints.GetComponentsInChildren<point>();
            SetAnimator();
        }


        #region virtual methods
        
        /// <summary>
        /// 스마일 맨이 움직임을 시작하도록 합니다.
        /// </summary>
        protected virtual void GetSmileManMove()
        {
            isMoving = true;
        }
        
        
        /// <summary>
        /// 스마일 맨의 움직임 관련 로직을 다룹니다. 이 함수가 Update 함수 안에 포함되어야 스마일 맨이 움직입니다.
        /// </summary>
        protected virtual void UpdateSmileManMovement()
        {
            if (!isMoving) return;
            transform.position = Vector3.MoveTowards(transform.position, points[indexOfPoint].gameObject.transform.position, velocity * Time.deltaTime);
            if (!IsWalking)
            {
                animator.CrossFade(walkHash, 0.3f);
                IsWalking = true;
            }
        }

        protected virtual void HandleMovement()
        {
             if (!Mathf.Approximately(transform.position.x, points[indexOfPoint].gameObject.transform.position.x) ||
                 !Mathf.Approximately(transform.position.z, points[indexOfPoint].gameObject.transform.position.z)) return;
            
            isMoving = false;
            indexOfPoint = (indexOfPoint + 1) % points.Count();
        }

        protected void HandleRotation()
        {
            direction = new Vector3(
                points[indexOfPoint].gameObject.transform.position.x, 0, points[indexOfPoint].gameObject.transform.position.z)
                        - new Vector3(transform.position.x, 0f, transform.position.z);
            direction.Normalize();
            transform.rotation = Quaternion.Euler(direction);
        }
        
        #endregion
    }
}