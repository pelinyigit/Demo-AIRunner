using TMPro;
using UnityEngine;

public class PaintController : MonoBehaviour
{
    public GameObject brush;
    public float brushSize;
    public GameObject percantageCanvas;
    public GameObject mainUI;
    public TextMeshProUGUI tmp;
    public RenderTexture rt;

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
            PaintTheWall();
           // PaintPercentage();
        }
    }

    private void PaintTheWall()
    {
        percantageCanvas.SetActive(true);
        mainUI.SetActive(false);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var GO = Instantiate(brush, new Vector3(hit.point.x, hit.point.y, hit.point.z - .1f), Quaternion.Euler(-90f, 0f, 0f));
            GO.transform.localScale = Vector3.one * brushSize;
        }
    }

    private void PaintPercentage()
    {
        var currentPaint = 1;
        var totalPaint = (int)Texture.totalTextureMemory;
        RenderTexture.active = rt;

        var texture2D = new Texture2D(rt.width, rt.height);
        texture2D.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);


        for (int i = 0; i < (int)Texture.totalTextureMemory; i++)
        {
           i = currentPaint;
        }
        
        var neededPercantageValue = currentPaint / totalPaint;
        tmp.text = neededPercantageValue.ToString();
    }
}
