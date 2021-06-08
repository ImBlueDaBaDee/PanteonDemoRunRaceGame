using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchRateDisplay : MonoBehaviour
{
    private TextMeshProUGUI text;
    int matchRate;
    private bool isPlayerFinished;
    //private PlayerMovement playerScript;


    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.enabled = false;
    }
    void Update()
    {
        isPlayerFinished = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().isFinished;
        matchRate = GameObject.FindGameObjectWithTag("Wall").GetComponent<VertexPaint>().matchRate;
        if (isPlayerFinished) { text.enabled = true; }
        text.text = ("Match Rate :" + matchRate.ToString()+"%");

    }
}
