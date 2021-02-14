using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarningController : MonoBehaviour
{
    private TextMeshProUGUI myText;

    private void Awake() {
        myText = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string warning) {
        myText.text = warning;
    }
    
    public void MyDestroy() {
        Destroy(gameObject);
    }
}