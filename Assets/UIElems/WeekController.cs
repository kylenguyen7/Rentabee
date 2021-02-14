using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeekController : MonoBehaviour
{
    private TextMeshProUGUI myText;

    private void Start() {
        myText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        myText.text = "Week " + PlayerStatsManager.instance.getWeek().ToString();
    }
}
