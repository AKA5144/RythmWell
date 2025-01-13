using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    static public bool isMoving;
    void Start()
    {
        isMoving = false;
    }


    void Update()
    {
        if (LerpCam.isOriginalPosition)
        {

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            LerpCam lerpCam = FindObjectOfType<LerpCam>();
            LerpCam.isOriginalPosition = false;

            if (LerpCam.isCloseToTarget)
            {
                LerpCam.Selected = true;
                GameManager.Instance.mapName = lerpCam.preview.list[lerpCam.preview.index].name;
                Debug.Log(lerpCam.preview.list[lerpCam.preview.index].clip);
                SceneManager.LoadScene("Game");
            }
            else
            {
                LerpCam.isCloseToTarget = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LerpCam.isOriginalPosition = true;
            LerpCam.isCloseToTarget = false;
        }
    }
}
