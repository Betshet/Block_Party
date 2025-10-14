using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BlockSpawn : MonoBehaviour
{
    InputAction MouseDelta;
    Vector2 MouseScreenPosition;

    [SerializeField]
    public GameObject BlockSpawner;

    [SerializeField]
    public Collider TableCollider;

    [SerializeField]
    public Camera mainCamera;

    [Tooltip("Dégagement vertical entre le bas du bloc et le plateau pendant le placement.")]
    public float hoverClearance = 2f;

    Vector3 TableCenter;
    float TableTop;
    float TableRadius;

    // Curseur virtuel (dans camera.pixelRect)
    private Vector2 VirtualPosition;          // position écran "virtuelle"
    private Vector2 VirtualVelocity;          // vitesse pour SmoothDamp

    // Bloc
    private float BlocHalfX, BlocHalfZ;            // demi-tailles X/Z (AABB du collider)
    private float PlacementCenterY;

    [Tooltip("Lissage léger du curseur virtuel (secondes). 0 = aucun lissage.")]
    [Range(0f, 0.2f)] public float screenSmoothing = 0.06f;

    [Tooltip("Sensibilité de déplacement du curseur virtuel (px/frame). 1 = brut Input System.")]
    [Range(0.1f, 3f)] public float cursorSensitivity = 1.0f;

    bool bDropping = false;

    [SerializeField]
    public List<GameObject> BlockPrefabList;

    int iCurrentBlockPrefabIndex = 0;

    bool bStart = false;

    [SerializeField]
    GameManager gameManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Table
        Bounds tb = TableCollider.bounds;
        TableCenter = tb.center;
        TableTop = tb.max.y;
        TableRadius = Mathf.Min(tb.extents.x, tb.extents.z);

        //Prefab select


        /*iCurrentBlockPrefabIndex = Random.Range(0, BlockPrefabList.Count-1); 
        BlockSpawner.GetComponent<MeshFilter>().mesh = BlockPrefabList[iCurrentBlockPrefabIndex].GetComponent<MeshFilter>().sharedMesh;*/

        GetRandomBlockToPlace();
        BlockSpawner.GetComponent<MeshFilter>().mesh = BlockPrefabList[iCurrentBlockPrefabIndex].GetComponent<MeshFilter>().sharedMesh;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !bStart)
        {
            bStart = true;
        }
        if (bDropping) return;

        Rect r = mainCamera.pixelRect;
        MouseScreenPosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

        Vector2 target = new Vector2(
            Mathf.Clamp(VirtualPosition.x + MouseScreenPosition.x, r.xMin, r.xMax),
            Mathf.Clamp(VirtualPosition.y + MouseScreenPosition.y, r.yMin, r.yMax)
        );


        Vector2 _virtPos = target;

        // 2) Mapping rect (viewport caméra) -> disque (table)
        //    - centre écran -> centre de table
        //    - bords écran -> bord de table
        Vector2 centerPx = r.center;
        float halfW = r.width * 0.5f;
        float halfH = r.height * 0.5f;


        // Coordonnées normalisées [-1..1] (x = écran horizontal -> axe X, y = écran vertical -> axe Z)
        float nx = Mathf.Clamp((_virtPos.x - centerPx.x) / Mathf.Max(1e-5f, halfW), -1f, 1f);
        float nz = Mathf.Clamp((_virtPos.y - centerPx.y) / Mathf.Max(1e-5f, halfH), -1f, 1f);

        Vector2 s = new Vector2(nx, nz);       // vecteur carré normalisé
        float len = s.magnitude;
        Vector2 dir = (len > 1e-6f) ? (s / len) : Vector2.zero;  // direction radiale dans le disque
        float rho = Mathf.Min(1f, len);                           // amplitude (<= 1)

        // Rayon autorisé pour que TOUT le bloc reste sur la table dans CETTE direction
        float support = Mathf.Sqrt((BlocHalfX * dir.x) * (BlocHalfX * dir.x) + (BlocHalfZ * dir.y) * (BlocHalfZ * dir.y));
        float allowed = Mathf.Max(0f, TableRadius - support);

        // Position finale (stick au bord si rho==1)
        Vector2 xz = new Vector2(TableCenter.x, TableCenter.z) + dir * (rho * allowed);
        Vector3 finalPos = new Vector3(xz.x, hoverClearance, xz.y);

        
        BlockSpawner.transform.position = finalPos;

        if (!bStart) return;
        if (Input.GetMouseButtonDown(0))
        {
            Drop();
        }
    }

    void Drop()
    {
        bDropping = true;
        BlockSpawner.SetActive(false);
        Instantiate(BlockPrefabList[iCurrentBlockPrefabIndex], BlockSpawner.transform.position, Quaternion.identity);
        gameManager.CurrentLevel[iCurrentBlockPrefabIndex]--;
        StartCoroutine(ResetAfterDrop());
    }

    IEnumerator ResetAfterDrop()
    {
        yield return new WaitForSeconds(1f);
        BlockSpawner.SetActive(true);
        gameManager.iBlocksPlaced++;
        bDropping = false;

        GetRandomBlockToPlace();
        BlockSpawner.GetComponent<MeshFilter>().mesh = BlockPrefabList[iCurrentBlockPrefabIndex].GetComponent<MeshFilter>().sharedMesh;
    }

    void GetRandomBlockToPlace()
    {
        bool bFoundBlock = false;
        List<int> currentLevel = gameManager.CurrentLevel;

        int randomBlockInLevel;
        int NumberOfBlocksToPlace;

        bool bLevelFinished = true;
        for (int i = 0; i < currentLevel.Count; i++)
        {
            if (currentLevel[i] > 0)
            {
                bLevelFinished = false;
            }
        }

        if (bLevelFinished)
        {
            EndLevel();
            return;
        }


        while (bFoundBlock == false)
        {
            randomBlockInLevel = Random.Range(0, currentLevel.Count);
            NumberOfBlocksToPlace = currentLevel[randomBlockInLevel];
            if (NumberOfBlocksToPlace > 0)
            {
                bFoundBlock = true;
                iCurrentBlockPrefabIndex = randomBlockInLevel;
            }
        }
    }

    void EndLevel()
    {
        print("End Level");
    }
}
