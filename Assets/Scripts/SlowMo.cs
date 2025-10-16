using UnityEngine;
using System.Collections;

public class EventDepassement : MonoBehaviour
{
    public float slowMotionDuration = 2f; // Durée du ralenti en secondes
    public float slowMotionScale = 0.2f;  // Échelle du temps ralenti (0.2 = 20% de la vitesse normale)

    private bool isTriggered = false;

    private void OnTriggerExit(Collider other)
    {
        if (isTriggered) return;

        if (other.CompareTag("Block"))
        {
            StartCoroutine(HandleSlowMotion(other.gameObject));
        }
    }

    IEnumerator HandleSlowMotion(GameObject block)
    {
        isTriggered = true;
        Debug.Log("Bloc sorti du collider : " + block.name);

        // Ralentir le temps
        Time.timeScale = slowMotionScale;
        Debug.Log("Temps ralenti");

        // Attendre en temps réel
        yield return new WaitForSecondsRealtime(slowMotionDuration);

        // Revenir au temps normal
        Time.timeScale = 1f;
        Debug.Log("Temps normal");

        isTriggered = false;
    }
}