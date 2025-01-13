using TMPro;
using UnityEngine;

public class AccuracyAmount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    public enum accuGrade
    {
        PERFECT,
        GREAT,
        GOOD,
        BAD,
        MISS
    }
    public accuGrade type;
    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case accuGrade.PERFECT:
                textMeshProUGUI.text = new string("x " + Accuracy.perfect.Count);
                break;
            case accuGrade.GREAT:
                textMeshProUGUI.text = new string("x " + Accuracy.great.Count);
                break;
            case accuGrade.GOOD:
                textMeshProUGUI.text = new string("x " + Accuracy.good.Count);
                break;
            case accuGrade.BAD:
                textMeshProUGUI.text = new string("x " + Accuracy.bad.Count);
                break;
            case accuGrade.MISS:
                textMeshProUGUI.text = new string("x " + Accuracy.miss.Count);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
