using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class RankController : MonoBehaviour
{
    private GameObject player;

    public TextMeshProUGUI tmp;

    void Start()
    {
        player = FindObjectOfType<CharacterController>().gameObject;    
    }

    void Update()
    {
        RankCharacters();
    }

    private void RankCharacters()
    {
        //tmp.text = rank
    }
}
