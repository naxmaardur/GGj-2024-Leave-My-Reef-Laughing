using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundEffect : MonoBehaviour
{
    AudioSource source;
    [SerializeField]
    AudioClip[] clips;
    [SerializeField]
    float maxPitch;
    [SerializeField]
    float minPitch;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayClip()
    {
        source.pitch = Random.Range(minPitch, maxPitch);
        source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}
