using System.Collections;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [SerializeField] GameObject Target;
    [SerializeField] GameObject Approach;
    [SerializeField] GameObject accuracyTextPrefab;
    StateManager manage;

    public float AR;

    GameObject center;
    RectTransform TargetTransform;
    RectTransform ApproachTransform;
    Life life;
    enemies enemy;
    private bool deflected = false;
    private void Start()
    {
        life = FindObjectOfType<Life>();
        enemy = FindObjectOfType<enemies>();
        manage = FindObjectOfType<StateManager>();
        AR = 1f;
        TargetTransform = Target.GetComponent<RectTransform>();
        TargetTransform.position = GetComponentInParent<RectTransform>().position;

        ApproachTransform = Approach.GetComponent<RectTransform>();

        center = GameObject.Find("EnzoNTM");


        ApproachTransform.position = center.transform.position;
        DeplacerVers(Approach, Target, 2.5f);
    }


    private void Update()
    {
        if (StateManager.GameOver)
        {
            Destroy(gameObject);
        }
        if (transform.GetSiblingIndex() == 0)
        {
            Target.GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            Target.GetComponent<Collider2D>().enabled = false;
        }
    }

    public bool CheckAccuracy()
    {
        Material material = Approach.GetComponent<Material>();
        if (Approach.transform.localScale.x < 0.70)
        {
            return false;
        }
        else if (Approach.transform.localScale.x > 0.70 && Approach.transform.localScale.x < 0.75)
        {
            manage.PlaySound(1);
            Accuracy.bad.Add(25);
            GameObject go = Instantiate(accuracyTextPrefab, Approach.transform.position, Quaternion.identity, gameObject.transform.parent.parent);
            go.GetComponentInChildren<AccuracyDisplay>().spriteIndex = 1; 
            return true;
        }
        else if (Approach.transform.localScale.x > 0.75 && Approach.transform.localScale.x < 0.80)
        {
            Accuracy.good.Add(50);
            GameObject go = Instantiate(accuracyTextPrefab, Approach.transform.position, Quaternion.identity, gameObject.transform.parent.parent);
            go.GetComponentInChildren<AccuracyDisplay>().spriteIndex = 2;
            manage.PlaySound(1);
            return true;
        }
        else if (Approach.transform.localScale.x > 0.85 && Approach.transform.localScale.x < 0.95)
        {
            Accuracy.great.Add(75);
            GameObject go = Instantiate(accuracyTextPrefab, Approach.transform.position, Quaternion.identity, gameObject.transform.parent.parent);
            go.GetComponentInChildren<AccuracyDisplay>().spriteIndex = 3;
            manage.PlaySound(1);
            return true;
        }
        else if (Approach.transform.localScale.x > 0.95 && Approach.transform.localScale.x < 1.05)
        {
            Accuracy.perfect.Add(100);
            GameObject go = Instantiate(accuracyTextPrefab, Approach.transform.position, Quaternion.identity, gameObject.transform.parent.parent);
            go.GetComponentInChildren<AccuracyDisplay>().spriteIndex = 4;
            manage.PlaySound(1);
            life.ChangeHP(5);
            return true;
        }
        else if (Approach.transform.localScale.x > 1.05 && Approach.transform.localScale.x < 1.10)
        {
            Accuracy.great.Add(75);
            GameObject go = Instantiate(accuracyTextPrefab, Approach.transform.position, Quaternion.identity, gameObject.transform.parent.parent);
            go.GetComponentInChildren<AccuracyDisplay>().spriteIndex = 3;
            manage.PlaySound(1);
            life.ChangeHP(-2);
            return true;
        }
        else if (Approach.transform.localScale.x > 1.10 && Approach.transform.localScale.x < 1.15)
        {
            Accuracy.good.Add(50);
            GameObject go = Instantiate(accuracyTextPrefab, Approach.transform.position, Quaternion.identity, gameObject.transform.parent.parent);
            go.GetComponentInChildren<AccuracyDisplay>().spriteIndex = 2;
            manage.PlaySound(1);
            life.ChangeHP(-3);
            return true;
        }
        else if (Approach.transform.localScale.x > 1.15 && Approach.transform.localScale.x < 1.20)
        {
            Accuracy.bad.Add(25);
            GameObject go = Instantiate(accuracyTextPrefab, Approach.transform.position, Quaternion.identity, gameObject.transform.parent.parent);
            go.GetComponentInChildren<AccuracyDisplay>().spriteIndex = 1;
            manage.PlaySound(1);
            life.ChangeHP(-4);
            return true;
        }
        else if (Approach.transform.localScale.x > 1.20)
        {
            Accuracy.miss.Add(0);
            GameObject go = Instantiate(accuracyTextPrefab, Approach.transform.position, Quaternion.identity, gameObject.transform.parent.parent);
            go.GetComponentInChildren<AccuracyDisplay>().spriteIndex = 0;
            manage.PlaySound(0);
            life.ChangeHP(-5);
            if (NotesGenerator.enemy)
            {
                enemy.incrementeNote();
            }
            return true;
        }
        return false;
    }
    IEnumerator DeplacerCoroutine(GameObject source, GameObject target, float duree)
    {
        Vector3 initialScale = source.transform.localScale;
        Vector3 targetScale = target.transform.localScale;

        Vector3 initialPosition = source.transform.position;
        Vector3 targetPosition = target.transform.position;

        float timeElapsed = 0f;

        while (timeElapsed < duree && !deflected)
        {
            Vector3 direction = (targetPosition - initialPosition) * 2f;
            Vector3 scale = (targetScale - initialScale) * 2f;
            float Distance = (targetPosition - initialPosition).magnitude;
            float LerpLenght = duree / Distance;
            float lerpFactor = Mathf.Clamp01(timeElapsed / duree);

            source.transform.localScale = Vector3.Lerp(initialScale, initialScale + scale, lerpFactor);
            source.transform.position = Vector3.Lerp(initialPosition, initialPosition + direction, lerpFactor);

            timeElapsed += Time.deltaTime;
            if (source.transform.localScale.x > 1.25)
            {
                GameObject go = Instantiate(accuracyTextPrefab, Approach.transform.position, Quaternion.identity, gameObject.transform.parent.parent);
                go.GetComponentInChildren<AccuracyDisplay>().spriteIndex = 0;
                life.ChangeHP(-5);
                Accuracy.miss.Add(0);
                manage.PlaySound(0);
                if (NotesGenerator.enemy && enemy != null)
                {
                    enemy.incrementeNote();
                }
                Destroy(gameObject);
            }
            yield return null;
        }
    }

    public void DeplacerVers(GameObject source, GameObject target, float duree)
    {

        StartCoroutine(DeplacerCoroutine(source, target, duree));
    }

    public void Reflect()
    {
        deflected = true;

        StartCoroutine(Reflect(Approach, 0.1f));
    }
    IEnumerator Reflect(GameObject source, float duree)
    {
        Vector3 initialScale = source.transform.localScale;

        Vector3 initialPosition = source.transform.position;

        float timeElapsed = 0f;

        while (timeElapsed < duree)
        {
            float lerpFactor = Mathf.Clamp01(timeElapsed / duree) * AR;

            source.transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, lerpFactor);
            source.transform.position = Vector3.Lerp(initialPosition, center.transform.position, lerpFactor);

            timeElapsed += Time.deltaTime;
            if (source.transform.localScale.x < 0.1)
            {
                if (NotesGenerator.enemy && enemy!= null)
                {
                    enemy.reduceHP();
                }
                manage.PlaySound(0);
                Destroy(gameObject);
            }
            yield return null;
        }
    }
    public bool IsOUtOfScreen(Vector3 pos)
    {
        if (pos.x > Screen.width)
            return true;
        else if (pos.x < 0)
            return true;
        else if (pos.y > Screen.height)
            return true;
        else if (pos.y < 0)
            return true;
        return false;
    }
}
