using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip audioClip;

    public bool playOnAwake;
    public bool destroyOnSoundEnd;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize our AudioSource component
        audioSource = GetComponent<AudioSource>();
        
        if (playOnAwake)
        {
            PlaySound();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyOnSoundEnd)
        {
            if (!audioSource.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
