using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPartController : MonoBehaviour
{
    [HideInInspector]
    public bool isFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Animator>().SetTrigger("Idle");
            other.GetComponent<CharacterController>().canMoveForward = false;
            other.GetComponent<CharacterController>().canMoveSideways = false;
            Camera.main.GetComponent<CameraController>().OnFinalPart();
            Camera.main.GetComponent<CameraController>().enabled = false;
            isFinished = true;
        }
    }
}
