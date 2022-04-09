using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ObstacleController : MonoBehaviour
{

    public ObstacleTypes obstacleTypes;
    public GameObject particle;
    
    private GameObject player;
    private GameObject opponent;
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
        opponent = GameObject.FindGameObjectWithTag("Opponent");
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
        if ((other.CompareTag("Player") || other.CompareTag("Opponent")) && obstacleTypes == ObstacleTypes.HalfDonut || obstacleTypes == ObstacleTypes.MovingObstacle || obstacleTypes == ObstacleTypes.StaticObstacle)
        {
            StartCoroutine(OnHitPlayer(other.gameObject));
        }
        else if ((other.CompareTag("Player") || other.CompareTag("Opponent")) && obstacleTypes == ObstacleTypes.RotatorStick)
        {
            StartCoroutine(OnRotatorHit( new Vector3(player.transform.position.x + transform.rotation.y, 5f, 2f), 5f, other.gameObject));
        }
        else if ((other.CompareTag("Player") || other.CompareTag("Opponent")) && obstacleTypes == ObstacleTypes.RotatingPlatform)
        {
            OnSpinOut(other.gameObject);
        }
    }

    private void OnSpinOut(GameObject gameObject)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3((gameObject.transform.position.x - transform.localRotation.z) *3f, 0f, 0f), ForceMode.Impulse);
    }

    public IEnumerator OnRotatorHit(Vector3 forceDirection, float force, GameObject gameObject)
    {
        if (gameObject.GetComponent<CharacterController>() == null)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<Animator>().SetTrigger("Hit");
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * force, ForceMode.Impulse);
            gameObject.GetComponent<Rigidbody>().AddTorque(forceDirection * force, ForceMode.Impulse);
            yield return new WaitForSeconds(1.5f);
            gameObject.GetComponent<Animator>().SetTrigger("Run");
            gameObject.transform.position = new Vector3(0, 4.25f, 0);
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            camera.GetComponent<CameraController>().target = null;
            gameObject.GetComponent<Animator>().SetTrigger("Hit");
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            gameObject.GetComponent<CharacterController>().canMoveForward = false;
            gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * force, ForceMode.Impulse);
            gameObject.GetComponent<Rigidbody>().AddTorque(forceDirection * force, ForceMode.Impulse);
            yield return new WaitForSeconds(1.5f);
            gameObject.transform.GetComponent<CharacterController>().canMoveForward = true;
            gameObject.GetComponent<Animator>().SetTrigger("Run");
            gameObject.transform.position = new Vector3(0, 4.25f, 0);
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            camera.GetComponent<CameraController>().target = player;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private IEnumerator OnHitPlayer(GameObject gameObject)
    {
        var GOparticle = Instantiate(particle, gameObject.transform.position + Vector3.up, Quaternion.identity);
        if (gameObject.GetComponent<CharacterController>() == null)
        {
            gameObject.transform.position = new Vector3(0, 4.25f, -1);
            Destroy(GOparticle, 2f);
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
            gameObject.transform.GetComponent<CharacterController>().canMoveForward = false;
            yield return new WaitForSeconds(1f);
            gameObject.transform.GetComponent<CharacterController>().canMoveForward = true;
            gameObject.transform.position = new Vector3(0, 4.25f, 0);
            gameObject.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = true;
            Destroy(GOparticle, 2f);
        }
            Destroy(GOparticle, 2f);
    }
}
