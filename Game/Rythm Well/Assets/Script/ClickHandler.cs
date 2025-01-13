using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] Notes note;
    private void Start()
    {

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                OnPointerClick();
            }
        }
    }


    void OnPointerClick()
    {
        if (note.CheckAccuracy())
        {
            if (!NotesGenerator.enemy)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {             
                note.Reflect();
            }
        }
    }
}
