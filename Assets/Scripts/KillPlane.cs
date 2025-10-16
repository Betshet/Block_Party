using Unity.VisualScripting;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        gameManager.GameOver();
    }
}
