using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class RankController : MonoBehaviour
{
    private GameObject player;
    private GameObject temp;
    private float playerPos;
    private int playerRank = 7;

    public TextMeshProUGUI tmp;
    public GameObject[] opponents;

    void Start()
    {
        tmp.text = playerRank.ToString();
        player = FindObjectOfType<CharacterController>().gameObject;
    }

    void Update()
    {
        if (player.GetComponent<CharacterController>().isGameStarted)
        {
            playerPos = player.transform.position.z;
            SortOpponents();
        }
    }

    //Bubble Sort
    private void SortOpponents()
    {
        for (int j = 0; j < opponents.Length; j++)
        {
            for (int i = 0; i < opponents.Length - 1; i++)
            {
                if (opponents[i].transform.localPosition.z > opponents[i + 1].transform.localPosition.z)
                {
                    temp = opponents[i + 1];
                    opponents[i + 1] = opponents[i];
                    opponents[i] = temp;

                    RankCharacters();
                }
            }
        }
    }

    private void RankCharacters()
    {
        playerRank = Mathf.Clamp(playerRank, 2, 10);

        if (playerPos > temp.transform.localPosition.z)
        {
            playerRank -= 1;
            tmp.text = playerRank.ToString();
        }
        else
        {
            playerRank += 1;
            tmp.text = playerRank.ToString();
        }
    }
}
