using Assembly_CSharp.Assets.Scripts;
using Assembly_CSharp.Assets.Scripts.Components;
using Assembly_CSharp.Assets.Scripts.Enums;
using UnityEngine;

public class Wateer : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip WaterSong;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.CompareTag(ETag.PLAYER))
        {
            _audioSource.PlayOneShot(WaterSong);
            GameController.Instance.ResetCheckPoint();
        }
    }
}
