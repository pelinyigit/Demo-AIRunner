using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacleFailController : MonoBehaviour
{
    private ObstacleController obstacleController;

    private void Start()
    {
        obstacleController = FindObjectOfType<ObstacleController>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //TODO: Force out 
            //TODO: Clamp Value decrease at the end 
            obstacleController.StartCoroutine(obstacleController.OnRotatorHit(new Vector3(transform.rotation.z + other.transform.position.x, 5f, 0f), 3f));
            other.GetComponent<CharacterController>().clampValue = 5;
        }
    }

  
}
