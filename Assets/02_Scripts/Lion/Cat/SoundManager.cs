using System;
using UnityEngine;

namespace CatGame
{
    public class SoundManager : MonoBehaviour
    {
        public AudioSource audiosource;
        public AudioClip BGMcClip;
        public AudioClip jumpcClip;

        private void Start()
        {
            SetBGM();
        }

        public void SetBGM()
        {
            audiosource.clip = BGMcClip;
            audiosource.playOnAwake = true;
            audiosource.loop = true;
            audiosource.Play();
        }

        public void OnJumpSound()
        {
            audiosource.PlayOneShot(jumpcClip);
        }
    }

}
