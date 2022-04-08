using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class RankController : MonoBehaviour
{
    private GameObject player;

    public TextMeshProUGUI tmp;
    public GameObject[] opponents;

    void Start()
    {
        player = FindObjectOfType<CharacterController>().gameObject;    
    }

    void Update()
    {
        if (player.GetComponent<CharacterController>().isGameStarted)
        {
            RankCharacters();
        }
    }

    private void RankCharacters()
    {
        foreach (GameObject opponentRank in opponents)
        {
            if (player.transform.position.z > opponents[opponents.Length -1].transform.position.z)
            {
                Debug.Log("player 1.");
            }
            else
            {
                Debug.Log("opponent ");
            }
        }
        //tmp.text = rank
    }
}
