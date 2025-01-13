using UnityEngine;

public class StateManager : MonoBehaviour
{
    Life life;
    static public bool GameOver;
    [SerializeField] GameObject gameOverPannels;

    [SerializeField] AudioSource hitSound;
    [SerializeField] AudioSource missSound;
    void Start()
    {
        GameOver = false;
        life = FindObjectOfType<Life>();
        gameOverPannels.SetActive(false);
    }

    void Update()
    {
        if(GameOver)
        {
            gameOverPannels.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Accuracy.StaticAcc = 100;
            gameOverPannels.SetActive(true);
        }
    }

    public void PlaySound(int i)
    {
        if(i == 0)
        {
            missSound.Play();
        }
        if (i == 1)
        {
            hitSound.Play();
        }
    }
}
