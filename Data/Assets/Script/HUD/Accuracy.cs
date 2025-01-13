using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Accuracy : MonoBehaviour
{
    static public List<int> miss = new List<int>();
    static public List<int> bad = new List<int>();
    static public List<int> good = new List<int>();
    static public List<int> great = new List<int>();
    static public List<int> perfect = new List<int>();

    static public float StaticAcc;
    public TextMeshProUGUI textMeshProUGUI;

    private float valeurCible;
    private float dureeAnimation = 1.0f;
    private float tempsDepuisDebutAnimation;
    private float valeurAffichee;
    int totalHits = 0;

    private void Awake()
    {
        miss.Clear();
        bad.Clear();
        good.Clear();
        great.Clear();
        perfect.Clear();

        StaticAcc = 100f;
    }

    void Update()
    {
        CalculateAveragePrecision();
    }

    void CalculateAveragePrecision()
    {
        totalHits = miss.Count + bad.Count + good.Count + great.Count + perfect.Count;
        if (totalHits == 0)
        {
            textMeshProUGUI.text = "100 %";
            return;
        }

        float totalPrecision = (miss.Count * 0f) + (bad.Count * 25f) + (good.Count * 50f) + (great.Count * 75f) + (perfect.Count * 100f);

        float averagePrecision = totalPrecision / totalHits;

        if (Mathf.Abs(averagePrecision - valeurCible) > 0.01f)
        {
            valeurCible = averagePrecision;
            tempsDepuisDebutAnimation = Time.time;
        }

        float progressionAnimation = (Time.time - tempsDepuisDebutAnimation) / dureeAnimation;
        valeurAffichee = Mathf.Lerp(valeurAffichee, valeurCible, progressionAnimation);
        if (valeurAffichee != 100)
        {
            textMeshProUGUI.text = valeurAffichee.ToString("F2") + " %";
            StaticAcc = valeurAffichee;
        }
    }
}