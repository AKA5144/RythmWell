using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataToString : MonoBehaviour
{
    public enum Type
    {
        BPM,
        STARS,
        NAME
    }
    [SerializeField] TextMeshProUGUI m_TextMeshPro;
    [SerializeField] PreviewMap preview;
    public Type type;
    public Image m_Image;
    void Start()
    {
       
        if (m_TextMeshPro == null || m_Image == null)
        {
            Debug.LogError("TextMeshProUGUI or Image reference is missing!");
            return;
        }

        UpdateTextAndImage();
    }

    void Update()
    {
        UpdateTextAndImage();
    }
    void UpdateTextAndImage()
    {

        switch (type)
        {
            case Type.BPM:
                m_TextMeshPro.text = "BPM : " + preview.list[preview.index].BPM;
                break;
            case Type.STARS:
                m_TextMeshPro.text = preview.list[preview.index].stars + " stars";
                break;
            case Type.NAME:
                m_TextMeshPro.text = preview.list[preview.index].name;
                break;
        }

    }

}
