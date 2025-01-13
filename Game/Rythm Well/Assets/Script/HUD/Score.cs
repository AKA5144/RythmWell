using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score = new int();
    private TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        score = ((Accuracy.miss.Count * 0) + (Accuracy.bad.Count * 25) +
            (Accuracy.good.Count * 50) + (Accuracy.great.Count * 75) + (Accuracy.perfect.Count * 100));
        textMeshProUGUI.text = score.ToString();
    }
}
