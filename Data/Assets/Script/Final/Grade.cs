using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grade : MonoBehaviour
{
    [SerializeField] List<Sprite> sprite;
    [SerializeField] Image GoSprite;
    void Start()
    {
        if (Accuracy.StaticAcc == 100)
        {
            GoSprite.sprite = sprite[0];
        }
        else if (Accuracy.StaticAcc > 90)
        {
            if (Accuracy.miss.Count == 0)
            {
                GoSprite.sprite = sprite[1];
            }
            else
            {
                GoSprite.sprite = sprite[2];
            }
        }
        else if (Accuracy.StaticAcc > 80)
        {
            GoSprite.sprite = sprite[3];
        }
        else if (Accuracy.StaticAcc > 70)
        {
            GoSprite.sprite = sprite[4];
        }
        else if (Accuracy.StaticAcc < 70)
        {
            GoSprite.sprite = sprite[5];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
