using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FinalPartController : MonoBehaviour
{
    [HideInInspector]
    public bool isFinished;

    private GameObject player;
    private GameObject opponent;

    private void Start()
    {
        player = FindObjectOfType<CharacterController>().gameObject;    
        opponent = FindObjectOfType<OpponentAI>().gameObject;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Opponent"))
        {
            other.GetComponent<Animator>().SetTrigger("Idle");
            opponent.GetComponent<NavMeshAgent>().speed = 0;
            player.GetComponent<CharacterController>().canMoveForward = false;
            player.GetComponent<CharacterController>().canMoveSideways = false;
            Camera.main.GetComponent<CameraController>().OnFinalPart();
            Camera.main.GetComponent<CameraController>().enabled = false;
            isFinished = true;
        }
    }
}
