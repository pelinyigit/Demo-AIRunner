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
            for (int i = 0; i < opponents.Length; i++)
            {

            }
        }
        //tmp.text = rank
    }
}
