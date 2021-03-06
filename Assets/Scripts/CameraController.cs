using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public bool cameraSet;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        if (target != null)
        {

        transform.position = target.transform.position + offset;
        }
        else
        {
            return;
        }
    }

    public void OnFinalPart()
    {
        Sequence sequence = DOTween.Sequence();

        target = null;
        sequence.Append(transform.DOMove(new Vector3(0, 15, 320), 1f)).Join(transform.DORotate(new Vector3(0, 0, 0), 1f));
    }
}