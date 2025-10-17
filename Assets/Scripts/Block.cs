using UnityEngine;

public class Block : MonoBehaviour
{

    public enum BlockType
    {
        Cube,
        Cone,
        Thorus,
        Sphere,
        None
    }
    [SerializeField]
    public BlockType blockType = BlockType.None;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
