using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CatGame
{


    public class Transform_LoopMap : MonoBehaviour
    {
        public Transform[] LoopObjects;
        public float moveSpeed;
        public float minPosition;
        public float maxPosition;
        private float lastXPosition;
        
        public enum ColliderType { Pipe, Apple, Both }
        public ColliderType colliderType;
        
        public void init()
        {
            moveSpeed = 5f;
            float lastX = 0f;
            foreach (Transform loopObject in LoopObjects)
            {
                SetRandomSetting(loopObject,lastX + maxPosition);
                lastX = loopObject.position.x;
            }
        }
        void Update()
        {
            foreach (Transform loopObject in LoopObjects)
            {
                loopObject.position += Vector3.left * (moveSpeed * Time.deltaTime);
                if (loopObject.position.x <= minPosition)
                {
                    SetRandomSetting(loopObject,lastXPosition+maxPosition);
                }
                lastXPosition = loopObject.position.x;
            }
        }

        void SetRandomSetting(Transform loopObject, float xPos)
        {
            float randomY = Random.Range(-5, 0);
            loopObject.position = new Vector2(xPos, randomY);
            colliderType = (ColliderType)Random.Range(0, 3);
            foreach (Transform item in loopObject)
            {
                item.gameObject.SetActive(false);
            }
            
            switch (colliderType)
            {
                case ColliderType.Pipe:
                    loopObject.GetChild(0).gameObject.SetActive(true);
                    loopObject.GetChild(3).gameObject.SetActive(true);
                    break;
                case ColliderType.Apple:
                    loopObject.GetChild(1).gameObject.SetActive(true);
                    break;
                case ColliderType.Both:
                    loopObject.GetChild(0).gameObject.SetActive(true);
                    loopObject.GetChild(1).gameObject.SetActive(true);
                    loopObject.GetChild(3).gameObject.SetActive(true);
                    break;
            }
        }
    }
}