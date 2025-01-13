using TMPro;
using UnityEngine;

public class ScoreFinal : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        score = (Accuracy.miss.Count * 0) + (Accuracy.bad.Count * 25) +
            (Accuracy.good.Count * 50) + (Accuracy.great.Count * 75) + (Accuracy.perfect.Count * 100);
        textMeshProUGUI.text = score.ToString();
    }
}