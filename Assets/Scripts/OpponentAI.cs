using UnityEngine;
using UnityEngine.AI;

public class OpponentAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private CharacterController characterController;
    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        characterController = FindObjectOfType<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (characterController.isGameStarted)
        {
            animator.SetTrigger("Run");
            agent.SetDestination(new Vector3(transform.position.x, 5.25f, 311));        
        }
    }
}
