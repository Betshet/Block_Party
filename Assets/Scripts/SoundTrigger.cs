using UnityEngine;

public class SoundTrigger : MonoBehaviour
{

    public AudioSource playSound;

    private void OnTriggerEnter(Collider other)
    {
        playSound.Play();
    }
}
