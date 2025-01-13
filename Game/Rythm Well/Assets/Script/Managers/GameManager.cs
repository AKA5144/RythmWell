using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string mapName = "No Title";

    static GameManager instance;
    public static GameManager Instance
    {
        get 
        {
            if (instance == null)
            {
                GameObject gameObject = new GameObject(nameof(GameManager));
                gameObject.AddComponent<GameManager>();
            }

            return instance; 
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
            Debug.LogWarning("Find duplicate of GameManager !");
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
