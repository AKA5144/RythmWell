using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesGenerator : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] GameObject prefabNote;
    [SerializeField] float delay = 5f;
    static public bool enemy = false;
    void Start()
    {
        StartCoroutine(GenerateNotes());
    }

    IEnumerator GenerateNotes()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            GenerateRandomNotes();
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) 
        {
            enemy = false;
        }
    }
    public void GenerateNote(int index)
    {
        Transform SpawnPoint = spawnPoints[index];
        GameObject go = Instantiate(prefabNote, SpawnPoint.position, SpawnPoint.rotation, SpawnPoint);      
    }

    public void GenerateEnemy(string name)
    {
        //GameObject go = Instantiate(Resources.Load<GameObject>("Enemy"));
        //go.transform.SetParent(transform.parent, false) ;
    }
    int noteCounter = 1;
    void GenerateRandomNotes()
    {
        if (spawnPoints.Count > 0)
        {
            Transform SpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            GameObject newPrefab = Instantiate(prefabNote, SpawnPoint.position, SpawnPoint.rotation, SpawnPoint);
            newPrefab.name = "note" + noteCounter;
            noteCounter++;
        }
        else
        {
            Debug.LogWarning("Aucun point de spawn disponible.");
        }
    }
}
