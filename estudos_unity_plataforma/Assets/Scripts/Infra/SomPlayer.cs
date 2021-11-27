using Assembly_CSharp.Assets.Scripts.Enums;
using UnityEngine;

public class SomPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip ClipSound;
    public float MaxDistance = 1;
    public bool Loop;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = ClipSound;
        _audioSource.loop = Loop;
        _audioSource.mute = true;
        _audioSource.Play();
    }

    void Update()
    {
        var Listener = GameObject.FindGameObjectWithTag(ETag.PLAYER).transform;
        
        if(Listener == null)
            return;

        var playerDistance = Vector2.Distance(transform.position, Listener.position);

        if (playerDistance <= MaxDistance)
            _audioSource.mute = false;
        else
            _audioSource.mute = true;
    }

}