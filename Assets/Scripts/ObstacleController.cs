using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ObstacleController : MonoBehaviour
{
    public ObstacleTypes obstacleTypes;
    public ParticleSystem particle;

    private GameObject player;
    private Camera camera;
  
    public enum ObstacleTypes
    {
        MovingObstacle,
        StaticObstacle
    }

    void Awake()
    {
        DOTween.Init();
    }

    private void Start()
    {
        player = FindObjectOfType<CharacterController>().gameObject;
        camera = Camera.main;
        MovingObstacle();
    }

    private void MovingObstacle()
    {
        if (obstacleTypes == ObstacleTypes.MovingObstacle)
        {
            transform.DOMoveX(5f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(OnHitPlayer());
        }
    }

    private IEnumerator OnHitPlayer()
    {
        Instantiate(particle, player.transform.position + Vector3.up, Quaternion.identity);
        player.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
        player.transform.GetComponent<CharacterController>().canMoveForward = false;
        yield return new WaitForSeconds(.8f);
        player.transform.GetComponent<CharacterController>().canMoveForward = true;
        player.transform.position = new Vector3(0, 0, 0);
        player.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = true;
    }
}
