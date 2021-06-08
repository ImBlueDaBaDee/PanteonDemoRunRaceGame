using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Positioning : MonoBehaviour
{
    private GameObject[] opponents;
    private GameObject player;

    private TextMeshProUGUI rankingText;

    void Start()
    {
        rankingText = this.GetComponent<TextMeshProUGUI>();
        opponents = GameObject.FindGameObjectsWithTag("Opponent");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        int place = 1;
        for (int i = 0; i < opponents.Length; i++)
        {
            if (player.transform.position.z < opponents[i].transform.position.z) { place++; }
        }
        rankingText.text = (place + "/" + opponents.Length);
    }
}
