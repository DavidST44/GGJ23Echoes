using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortableAudioSource : MonoBehaviour
{
    AudioSource myAudio;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = gameObject.GetComponent<AudioSource>();
        myAudio.Play();
    }
    private void Update()
    {
        if (!myAudio.isPlaying)
            Destroy(gameObject);
    }
}
