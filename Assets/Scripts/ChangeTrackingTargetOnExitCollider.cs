using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeTrackingTargetOnExitCollider : MonoBehaviour
{

    [SerializeField]
    public CinemachineCamera camera;
    bool alsoSetFollow = true; //Si vrai: on change aussi le Tracking Target(Follow). Sinon: seulement l'orientation (LookAt).
    bool pickOnlyFirst = true; //Si activé, on choisit le tout premier objet qui sort, puis on ignore les suivants.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        if (pickOnlyFirst)
        {
            pickOnlyFirst = false;
            camera.Target.TrackingTarget = other.transform;
        }
    }
}
