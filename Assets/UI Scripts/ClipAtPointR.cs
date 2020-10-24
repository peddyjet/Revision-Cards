using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipAtPointR : MonoBehaviour
{
   public void PlayClipAtPoint(AudioClip audioClip) { AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position); }
   public void PlayClipAtPoint(Info info) { AudioSource.PlayClipAtPoint(info.clip, info.position); }

    [Serializable]
    public struct Info
    {
        public Vector3 position;
        public AudioClip clip;
    }
}
