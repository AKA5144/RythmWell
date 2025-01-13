using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccFinal : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] AudioSource applause;
    // Start is called before the first frame update
    void Start()
    {
        applause.Play();
        textMeshProUGUI.text = Accuracy.StaticAcc.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
