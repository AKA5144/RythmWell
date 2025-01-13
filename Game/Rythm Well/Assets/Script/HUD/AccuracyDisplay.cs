using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccuracyDisplay : MonoBehaviour
{
    [SerializeField] List<Sprite> sprite;
    [SerializeField] Transform transformPos;
    [SerializeField] Image imageSprite;
    public int spriteIndex;
    private Color color;
    private Vector3 pos;

    void Start()
    {
        imageSprite.sprite = sprite[spriteIndex];
        color = imageSprite.color;
        pos = transformPos.position;
    }

    void Update()
    {
        if (imageSprite.color.a > 0)
        {
            color.a = imageSprite.color.a - Time.deltaTime * 2f;
            imageSprite.color = color;
            pos -= Vector3.up * Time.deltaTime * 2f;
            transformPos.position = pos;
        }
        else
        {
            Destroy(transformPos.gameObject);
        }
    }
}
