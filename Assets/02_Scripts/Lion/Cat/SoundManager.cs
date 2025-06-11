using System;
using UnityEngine;

namespace CatGame
{
    public class SoundManager : MonoBehaviour
    {
        public enum clipType
        {
            BGMcClip,
            jumpcClip,
            getcClip,
        }
        public AudioSource audiosource;
        public AudioClip BGMcClip;
        public AudioClip jumpcClip;
        public AudioClip getcClip;

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

        public void PlayClip(clipType type)
        {
            switch (type)
            {
                case clipType.BGMcClip:
                    audiosource.PlayOneShot(BGMcClip);
                    break;
                case clipType.jumpcClip:
                    audiosource.PlayOneShot(jumpcClip);
                    break;
                case clipType.getcClip:
                    audiosource.PlayOneShot(getcClip);
                    break;
            }
        }
    }

}
