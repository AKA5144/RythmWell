using System;
using System.IO;
using UnityEngine;
using static Board;

public abstract class Note
{
    public int beatToSpawn; // when the note start

    public abstract void StartNote(Board board);

    public abstract override string ToString();
}

public class NoteEnemy : Note
{
    public string enemyName;

    public NoteEnemy(int beatToSpawn, string enemyName)
    {
        this.beatToSpawn = beatToSpawn;
        this.enemyName = enemyName;
    }

    public override void StartNote(Board board)
    {
        board.GenerateEnemy(enemyName);
    }

    public override string ToString()
    {
        return ".";
    }
}

public class NoteProjectile : Note
{
    public int indexSpawn; // where the projectile spawn

    public NoteProjectile(int beatToSpawn, int indexSpawn)
    {
        this.beatToSpawn = beatToSpawn;
        this.indexSpawn = indexSpawn;
    }

    public override void StartNote(Board board)
    {
        board.GenerateProjectile(indexSpawn);
    }

    public override string ToString()
    {
        return ".";
    }
}

public class NoteBpm : Note
{
    public int newBpm;

    public NoteBpm(int beatToSpawn, int bpm)
    {
        this.beatToSpawn = beatToSpawn;
        newBpm = bpm;
    }

    public override void StartNote(Board board)
    {
        board.ChangeBPM(newBpm);
    }

    public override string ToString()
    {
        return ".";
    }
}

public static class MapReader
{
    const int NAME_INDEX = 0;
    const int BPM_INDEX = 1;
    const int BEAT_DIVIDER_INDEX = 2;
    const int DESCENT_TIME_INDEX = 3;
    const int MAP_INDEX = 30;

    public static bool Write(Map map)
    {
        string contents = "";
        string myFilePath = Application.persistentDataPath + "/Maps/" + map.name + ".txt";

        for (int i = 0; i < MAP_INDEX + 1; i++)
        {
            switch (i)
            {
                case NAME_INDEX:
                    contents += (map.name + "\n");
                    break;

                case BPM_INDEX:
                    contents += (map.defaultBPM.ToString() + "\n");
                    break;

                case BEAT_DIVIDER_INDEX:
                    contents += (map.beatDivider.ToString() + "\n");
                    break;

                case DESCENT_TIME_INDEX:
                    contents += (map.timeToDescent.ToString() + "\n");
                    break;

                case MAP_INDEX:
                    foreach (var line in map.beats)
                    {
                        contents += WriteBeatLines(line);
                    }
                    break;

                default:
                    contents += "\n";
                    break;
            }
        }


        File.WriteAllText(myFilePath, contents);

        return true;
    }

    private static string WriteBeatLines(Note dataNote)
    {
        string lineTxt = "";
        lineTxt += dataNote.ToString() + "[";

        lineTxt += dataNote.ToString();
        lineTxt += "]\n";

        return lineTxt;
    }

    public static bool Read(string fileName, out Map map)
    {
        map = new Map();
        // read map file
        //string myFilePath = Application.persistentDataPath + "/Maps/" + fileName + ".txt";
        //Debug.Log(myFilePath);
        //string[] fileLignes = File.ReadAllLines(myFilePath);
        Debug.Log(fileName);
        string[] fileLignes = Resources.Load<TextAsset>(fileName).text.Split("\n");

        map.defaultBPM = double.Parse(fileLignes[BPM_INDEX]);
        map.beatDivider = double.Parse(fileLignes[BEAT_DIVIDER_INDEX]);
        map.timeToDescent = float.Parse(fileLignes[DESCENT_TIME_INDEX]);

        map.beats = new();
        int previousBeatIndex = -1;
        for (int i = MAP_INDEX; i < fileLignes.Length; i++)
        {
            var beat = ReadBeatLine(fileLignes[i]);
            if(previousBeatIndex >= beat.beatToSpawn)
            {
                Debug.LogError("index not organised" + beat.beatToSpawn);
            }
            previousBeatIndex = beat.beatToSpawn;
            map.beats.Enqueue(beat);
        }
        
        return true;
    }

    private static Note ReadBeatLine(string dataString)
    {
        int indexBeat = dataString.IndexOf(":", StringComparison.Ordinal);
        switch (dataString[indexBeat + 1])
        {
            case 'P':
                return CreateProjectile(dataString);
            case 'E':
                return CreateEnemy(dataString);
            case 'B':
                return CreateBpm(dataString);
            default:
                Debug.LogError("Can't find candidate in map for : " + dataString[indexBeat + 1]);
                return new NoteProjectile(0, 0);
        }
    }

    private static NoteProjectile CreateProjectile(string dataString)
    {
        int indexBeat = dataString.IndexOf(":", StringComparison.Ordinal);

        int beat = Int32.Parse(dataString[..indexBeat]);

        int indexSpawn = Int32.Parse(dataString[(indexBeat + 2)..]);
        return new NoteProjectile(beat, indexSpawn);
    }

    private static NoteEnemy CreateEnemy(string dataString)
    {
        int indexBeat = dataString.IndexOf(":", StringComparison.Ordinal);
        int beat = Int32.Parse(dataString[..indexBeat]);
        string enemyName = dataString[(indexBeat + 3)..];
        NotesGenerator.enemy = true;
        return new NoteEnemy(beat, enemyName);
    }

    private static NoteBpm CreateBpm(string dataString)
    {
        int indexBeat = dataString.IndexOf(":", StringComparison.Ordinal);
        int beat = Int32.Parse(dataString[..indexBeat]);
        int bpmChange = Int32.Parse(dataString[(indexBeat + 2)..]);

        return new NoteBpm(beat, bpmChange);
    }
}