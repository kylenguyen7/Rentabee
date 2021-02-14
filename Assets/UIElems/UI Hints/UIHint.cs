using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class UIHint : MonoBehaviour
{
    private float hintThreshold = 0.05f;
    [SerializeField] TextMeshProUGUI myText;

    protected abstract int calculateHint(float value);
    protected abstract int OnRightHint();
    protected abstract int OnLeftHint();

    private string convertHint(int num) {
        if (num == 0) return "";

        char c = num > 0 ? '+' : '–';
        return new string(c, Mathf.Abs(num));
    }

    public void Update() {
        string hint = "";
        if(Mathf.Abs(CardSpawnerController.instance.CurrentCardSwipeRatio) > hintThreshold) {
            if (CardSpawnerController.instance.CurrentCardSwipeRatio > 0) hint = convertHint(OnRightHint());
            else hint = convertHint(OnLeftHint());
        } else {
            hint = "";
        }

        myText.text = hint;
    }
}
