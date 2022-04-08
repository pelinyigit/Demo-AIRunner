using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPartController : MonoBehaviour
{
    [HideInInspector]
    public bool isFinished;

    private GameObject player;

    private void Start()
    {
        player = FindObjectOfType<CharacterController>().gameObject;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Opponent"))
        {
            other.GetComponent<Animator>().SetTrigger("Idle");
            player.GetComponent<CharacterController>().canMoveForward = false;
            player.GetComponent<CharacterController>().canMoveSideways = false;
            Camera.main.GetComponent<CameraController>().OnFinalPart();
            Camera.main.GetComponent<CameraController>().enabled = false;
            isFinished = true;
        }
    }
}
