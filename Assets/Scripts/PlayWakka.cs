using UnityEngine;

public class PlayWakka : MonoBehaviour
{
    public AudioClip Wakka1;
    public AudioClip Wakka2;

    private AudioSource _audioSource;

    private static bool _switchWakka;

    private void OnDestroy()
    {
        _audioSource = FindObjectOfType<AudioSource>();
        if (_audioSource == null) return;
        _audioSource.PlayOneShot(_switchWakka ? Wakka1 : Wakka2);
        _switchWakka = !_switchWakka;
    }
}
