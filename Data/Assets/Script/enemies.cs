using System.Collections;
using UnityEngine;

public class enemies : MonoBehaviour
{
    GameObject center;
    private float AR;
    public int hp;
    public int notesMax;
    public int notesCounter;
    Life life;
    void Start()
    {
        life = FindObjectOfType<Life>();
        AR = 1f;
        notesCounter = 0;
        NotesGenerator.enemy = true;
        center = GameObject.Find("EnzoNTM");

        transform.position = center.transform.position;
        StartCoroutine(BeginGrowth(gameObject, 1f));
    }

    public void reduceHP()
    {
        hp--;
    }
    public void incrementeNote()
    {
        notesCounter++;
    }
    IEnumerator BeginGrowth(GameObject source, float duree)
    {
        Vector3 initialScale = source.transform.localScale;


        float timeElapsed = 0f;

        while (timeElapsed < duree)
        {

            float lerpFactor = Mathf.Clamp01(timeElapsed / duree) * AR;

            source.transform.localScale = Vector3.Lerp(initialScale, Vector3.one * 4, lerpFactor);


            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
    void Update()
    {
        if (notesCounter >= notesMax)
        {
            life.ChangeHP(-10);
            NotesGenerator.enemy = false;
            Destroy(transform.gameObject);
        }
        if (hp <= 0)
        {
            NotesGenerator.enemy = false;
            Destroy(transform.gameObject);
        }
    }
}
