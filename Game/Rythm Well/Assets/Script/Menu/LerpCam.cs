using UnityEngine;

public class LerpCam : MonoBehaviour
{
    public Transform targetTransform;
    public Transform originalTransform;
    public float lerpSpeed = 0.01f;

    static public bool isCloseToTarget = false;
    static public bool isOriginalPosition = true;
    static public bool Selected = false;
    public PreviewMap preview;
    private void Start()
    {
        isCloseToTarget = false;
        isOriginalPosition = true;
        Selected = false;
    }
    void Update()
    {
        if (isOriginalPosition)
        {
            transform.position = Vector3.Lerp(transform.position, originalTransform.position, lerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalTransform.rotation, lerpSpeed * Time.deltaTime);
        }
        else if (isCloseToTarget)
        {
            transform.position = Vector3.Lerp(transform.position, targetTransform.position, lerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetTransform.rotation, lerpSpeed * Time.deltaTime);
        }
    }
}
