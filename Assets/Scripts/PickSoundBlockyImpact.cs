using UnityEngine;

public class PickSoundBlockyImpact : MonoBehaviour
{
    public AudioClip[] collisionSounds; // Array to store different sound clips for each block
    private AudioSource audioSource; // The AudioSource to play sounds from

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource attached to the object
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object is colliding with the cylinder (you can check by tag or name)
        if (collision.gameObject.CompareTag("Cylinder"))
        {
            // Play the sound based on the object (this is where you assign different sounds)
            if (collisionSounds.Length > 0)
            {
                // Play the first sound from the array
                audioSource.PlayOneShot(collisionSounds[Random.Range(0, collisionSounds.Length)]);
            }
        }
    }
}
