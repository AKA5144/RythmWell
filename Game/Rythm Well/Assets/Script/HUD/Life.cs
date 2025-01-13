using TMPro;
using UnityEngine;

public class Life : MonoBehaviour
{
    private int life = new int();
    private TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
        life = 100;
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
    public void ChangeHP(int hp)
    {
        if ((life + hp) >= 100)
        {
            life = 100;
        }
        else if ((life - hp) < 0)
        {
            life = 0;
        }
        else
        {
            life += hp;
        }
    }

    public int getHpPlayer()
    {
        return life;
    }
    // Update is called once per frame
    void Update()
    {
        if (life > 100)
            life = 100;
        if (life < 0)
            life = 0;
        textMeshProUGUI.text = life.ToString("F2") + " %";
    }
}
