using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
    [SerializeField] NotesGenerator notesGenerator;

    public struct Map
    {
        public string name;
        public double defaultBPM;
        public double beatDivider;
        public Queue<Note> beats;
        public float timeToDescent;

        public readonly int GetLastBeat()
        {
            return beats.ToArray()[beats.Count - 1].beatToSpawn;
        }
    }

    public Map map;
    private double crocheTime;

    private double timer = 0f;
    private int currentCroche = 0;
    private int lastCroche = -1;

    private Note nextBeat = null;

    private bool finish = false;
    private float timerEnd;

    Life life;
    void Awake()
    {
        life = FindObjectOfType<Life>();
        if (!MapReader.Read(GameManager.Instance.mapName, out map))
        {
            Debug.LogWarning("map loading unsuccessful");
            return;
        }

        GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(GameManager.Instance.mapName);

        ChangeBPM(map.defaultBPM);

        nextBeat = map.beats.Dequeue();
        lastCroche = map.GetLastBeat();
    }
    public float GetPourcentageDone()
    {
        return (float)currentCroche / (float)lastCroche;
    }

    private void Start()
    {
        // make the music start at the same time the first "beat" touch the target
        GetComponent<AudioSource>().PlayDelayed(map.timeToDescent / 2);//TimeToDescent a gauche a  droite MIYAMOTO
    }

    public void ChangeBPM(double bpm)
    {
        double noirTime = 1f / (bpm / 60f);           // how much time before a noir appear
        crocheTime = noirTime / map.beatDivider;      // same with croche with beatDivider
    }

    private void Update()
    {
        if (life.getHpPlayer() <= 0)
        {
            finish = true;
        }
        if (finish)
        {
            if (life.getHpPlayer() <= 0)
            {
                GetComponent<AudioSource>().Stop();
                map.beats.Clear();
                StateManager.GameOver = true;
            }
            else
            {
                timerEnd -= Time.deltaTime;
                if (timerEnd <= 0f)
                {
                    SceneManager.LoadScene("Ending");
                    // do end stuff here
                }
            }
        }
    }

    private void FixedUpdate()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if (finish)
        {
            return;
        }

        timer += Time.fixedDeltaTime;
        if (timer >= crocheTime)
        {
            timer -= crocheTime; // timer = offset
            if (nextBeat?.beatToSpawn == ++currentCroche) // check if currentCroche active
            {
                nextBeat.StartNote(this);
                ReadNextBeat();
            }
        }
    }

    private void ReadNextBeat()
    {
        if (!map.beats.TryDequeue(out nextBeat))
        {
            timerEnd = map.timeToDescent + 3f;
            finish = true;
        }
    }

    public void GenerateProjectile(int indexSpawn)
    {
        notesGenerator.GenerateNote(indexSpawn);
    }

    public void GenerateEnemy(string enemy)
    {
        notesGenerator.GenerateEnemy(enemy);
    }
}
