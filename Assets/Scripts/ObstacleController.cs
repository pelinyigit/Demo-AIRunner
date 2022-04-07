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
        StaticObstacle,
        HalfDonut,
        RotatingPlatform,
        Rotator,
        RotatorStick,
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
        HalfDonutObstacle();
        RotatorObstacle();
        RotatingPlatformObstacle();
    }

    private void MovingObstacle()
    {
        if (obstacleTypes == ObstacleTypes.MovingObstacle)
        {
            transform.DOMoveX(5f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).Play();
        }
    }

    private void HalfDonutObstacle()
    {
        Sequence sequence = DOTween.Sequence();
        if (obstacleTypes == ObstacleTypes.HalfDonut)
        {
            sequence.AppendInterval(3f).Append(transform.DOLocalRotate(new Vector3(270f, 0f, 0f), .5f)
                .SetLoops(3, LoopType.Incremental)
                .SetEase(Ease.Linear)
                .Play()).SetLoops(-1, LoopType.Incremental);
            ;
        }
    }

    private void RotatingPlatformObstacle()
    {
        int rotateRoute = Random.Range(0, 2);

        if (rotateRoute == 0)
        {
            if (obstacleTypes == ObstacleTypes.RotatingPlatform)
            {
                transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y, 180), 2f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).Play();
            }
        }
        else if (rotateRoute == 1)
        {
            if (obstacleTypes == ObstacleTypes.RotatingPlatform)
            {
                transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y, -180), 2f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).Play();
            }
        }
    }

    private void RotatorObstacle()
    {
        if (obstacleTypes == ObstacleTypes.Rotator)
        {
            transform.DORotate(new Vector3(0f, 180, 0f), 1f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && obstacleTypes == ObstacleTypes.HalfDonut || obstacleTypes == ObstacleTypes.MovingObstacle || obstacleTypes == ObstacleTypes.StaticObstacle)
        {
            StartCoroutine(OnHitPlayer());
        }
        else if (other.CompareTag("Player") && obstacleTypes == ObstacleTypes.RotatorStick)
        {
            StartCoroutine(OnRotatorHit( new Vector3(player.transform.position.x + transform.rotation.y, 5f, 2f), 5f));
        }
        else if (other.CompareTag("Player") && obstacleTypes == ObstacleTypes.RotatingPlatform)
        {
            OnSpinOut();
        }
    }

    private void OnSpinOut()
    {
        player.GetComponent<Rigidbody>().AddForce(new Vector3((player.transform.position.x - transform.localRotation.z) *3f, 0f, 0f), ForceMode.Impulse);
    }

    public IEnumerator OnRotatorHit(Vector3 forceDirection, float force)
    {
        player.GetComponent<Rigidbody>().useGravity = true;
        camera.GetComponent<CameraController>().target = null;
        player.GetComponent<Animator>().SetTrigger("Hit");
        player.GetComponent<BoxCollider>().isTrigger = false;
        player.GetComponent<CharacterController>().canMoveForward = false;
        player.GetComponent<Rigidbody>().AddForce(forceDirection * force, ForceMode.Impulse);
        player.GetComponent<Rigidbody>().AddTorque(forceDirection * force, ForceMode.Impulse);
        yield return new WaitForSeconds(1.5f);
        player.transform.GetComponent<CharacterController>().canMoveForward = true;
        player.GetComponent<Animator>().SetTrigger("Run");
        player.transform.position = new Vector3(0, 4.5f, 0);
        player.GetComponent<BoxCollider>().isTrigger = true;
        camera.GetComponent<CameraController>().target = player;
        player.GetComponent<Rigidbody>().useGravity = false;
    }

    private IEnumerator OnHitPlayer()
    {
        Instantiate(particle, player.transform.position + Vector3.up, Quaternion.identity);
        player.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
        player.transform.GetComponent<CharacterController>().canMoveForward = false;
        yield return new WaitForSeconds(1f);
        player.transform.GetComponent<CharacterController>().canMoveForward = true;
        player.transform.position = new Vector3(0, 4.5f, 0);
        player.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = true;
    }
}
