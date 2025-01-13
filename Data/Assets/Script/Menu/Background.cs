using UnityEngine;

public class Background : MonoBehaviour
{
    public float floatSpeed = 1.0f;

    public float maxOffset = 1.0f;
    public float minOffset = 0.8f;



    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * floatSpeed) * (maxOffset - minOffset);

        transform.position = initialPosition + new Vector3(0.0f, offset, 0.0f);

    }
}
