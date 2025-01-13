using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour
{
    private Board board;
    private RawImage image;

    void Start()
    {
        board = FindObjectOfType<Board>();
        image = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        float pourcentageOutCubic = Mathf.Pow(board.GetPourcentageDone(), 3) + 0.5f;

        image.uvRect = new Rect(image.uvRect.position + new Vector2(-pourcentageOutCubic, 0f) * Time.deltaTime, image.uvRect.size);
    }
}
