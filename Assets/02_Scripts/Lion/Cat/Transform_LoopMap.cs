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
        public void init()
        {
            float lastX = 0f;
            foreach (Transform loopObject in LoopObjects)
            {
                float randomY = Random.Range(-5f, 0f);
                loopObject.position = new Vector2(lastX + maxPosition, randomY);
                lastX = loopObject.position.x;
            }
        }

        

        void Update()
        {
            foreach (Transform loopObject in LoopObjects)
            {
                loopObject.position += Vector3.left * moveSpeed * Time.fixedDeltaTime;
                if (loopObject.position.x <= minPosition)
                {
                    float randomY = Random.Range(-5, 0);
                    loopObject.position = new Vector2(lastXPosition+maxPosition, randomY);
                }
                lastXPosition = loopObject.position.x;
            }
        }
    }
}