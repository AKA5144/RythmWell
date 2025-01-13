using System.Collections.Generic;
using UnityEngine;

public class PreviewMap : MonoBehaviour
{
    public List<SongData> list;
    public GameObject BackGround;
    public float floatSpeed = 1.0f;

    public float maxOffset = 1.0f;
    public float minOffset = 0.5f;

    public float rotateSpeed = 30.0f;

    private Vector3 initialPosition;
    public int index;



    bool hasJumped = false;
    [SerializeField] AudioSource audioSource;
    void Start()
    {
        index = 0;
        initialPosition = transform.position;
        audioSource.clip = list[index].clip;
        audioSource.Play();
    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            index -= 1;
            if (index >= 0 && index < list.Count)
            {
                audioSource.clip = list[index].clip;
                audioSource.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            index += 1;
            if (index >= 0 && index < list.Count)
            {
                audioSource.clip = list[index].clip;
                audioSource.Play();
            }
        }

        if (index < 0)
        {
            index = list.Count - 1;
            audioSource.clip = list[index].clip;
            audioSource.Play();
        }
        if (index > list.Count - 1)
        {
            index = 0;
            audioSource.clip = list[index].clip;
            audioSource.Play();
        }
        if (index >= 0 && index < list.Count)
        {
            LoadData();
        }

        if (!LerpCam.Selected)
        {
            float offset = Mathf.Sin(Time.time * floatSpeed) * (maxOffset - minOffset);

            transform.position = initialPosition + new Vector3(0.0f, offset, 0.0f);

            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
        else
        {

        }
    }

    void LoadData()
    {
        BackGround.GetComponent<MeshRenderer>().material = list[index].material;
        GetComponent<MeshRenderer>().material = list[index].material;
    }
}
