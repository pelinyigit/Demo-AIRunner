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
            obstacleController.StartCoroutine(obstacleController.OnRotatorHit());
        }
    }

  
}
