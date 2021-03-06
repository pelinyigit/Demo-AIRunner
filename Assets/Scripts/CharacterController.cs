using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Vector3 firstTouchPosition;
    private Vector3 deltaTouchPosition;
    private Vector3 direction;
    private Rigidbody body;
    private Animator animator;

    public float clampValue;
    public float speed;
    public bool canMoveForward;
    public bool canMoveSideways;
    public bool isGameStarted;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); 
    }

    private void FixedUpdate()
    {
        if (!isGameStarted && Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
            canMoveForward = true;
            canMoveSideways = true;
        }
        Movement();
    }
    private void Movement()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -clampValue, clampValue), transform.position.y, transform.position.z);

        if (!canMoveForward)
        {
            return;
        }

        if (isGameStarted)
        {
            Vector3 forwardMovement = transform.forward * speed * Time.fixedDeltaTime *2f;
            body.MovePosition(body.position + forwardMovement);

            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Run");
                firstTouchPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                deltaTouchPosition = Input.mousePosition - firstTouchPosition;
                direction = new Vector3(deltaTouchPosition.x * 15f, 0f, 0f);

                body.velocity = direction.normalized * 10f;
            }
            else
            {
                body.velocity = Vector3.zero;
            }
        }
    }
}