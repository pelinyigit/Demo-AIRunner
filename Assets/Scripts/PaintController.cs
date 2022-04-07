using UnityEngine;

public class PaintController : MonoBehaviour
{
    public GameObject brush;
    public float brushSize;

    private int width = 10;
    private int height = 10;
    private FinalPartController finalPartController;

    private void Start()
    {
        finalPartController = FindObjectOfType<FinalPartController>();    
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && finalPartController.isFinished == true)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                float width = Mathf.Clamp(transform.position.x, 1f, 10f);
                width = hit.point.x;
                float height = Mathf.Clamp(transform.position.y, 1f, 10f);
                height = hit.point.y;
                var GO = Instantiate(brush, new Vector3(width, height, hit.point.z - .8f), Quaternion.Euler(-90f, 0f, 0f));
                GO.transform.localScale = Vector3.one * brushSize;
            }
        }
    }
}
