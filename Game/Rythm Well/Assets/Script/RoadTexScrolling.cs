using UnityEngine;

public class RoadTexScrolling : MonoBehaviour
{
    Material material;
    [SerializeField]float xSpeed = 0.5f;
    float uvOffset = 0.0f;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        float x = material.GetTextureOffset("_MainTex").x;
        float scale = transform.localScale.z;
        uvOffset = x / scale;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset = new Vector2(0f, uvOffset - Time.time * xSpeed);
    }
}
