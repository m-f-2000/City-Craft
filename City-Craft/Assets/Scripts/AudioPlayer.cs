using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gp7
{

    public class AudioPlayer : MonoBehaviour
    {
        public AudioClip Sound;
        public AudioSource Source;

        public static AudioPlayer instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(this.gameObject);

        }

        public void PlayPlacementSound()
        {
            if(Sound != null)
            {
                Source.PlayOneShot(Sound);
            }
        }
    }
}