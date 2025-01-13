using System;
using UnityEngine;
using UnityEngine.UI;

public class Ground : MonoBehaviour
{
    Board board;
    Image image;

    void Start()
    {
        board = FindObjectOfType<Board>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float pourcentage = board.GetPourcentageDone();
        float color = pourcentage * 0.5f + 0.5f;
        image.color = new Color(color, color, color);

        float scale = pourcentage * 4.4f;
        gameObject.transform.parent.GetComponent<RectTransform>().localScale = new Vector2(scale, scale);
    }
}
