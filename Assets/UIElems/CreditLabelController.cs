using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditLabelController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI creditLabel;

    int internalCredit;

    private void Start() {
        internalCredit = PlayerStatsManager.instance.getCredit();
        InvokeRepeating("UpdateInternalCredit", 0f, 0.02f);
    }

    private void UpdateInternalCredit() {
        if (PlayerStatsManager.instance.getCredit() == internalCredit) return;

        int sign = (int)Mathf.Sign(PlayerStatsManager.instance.getCredit() - internalCredit);
        internalCredit += sign;
    }

    private void Update() {
        creditLabel.text = ((int)(internalCredit)).ToString();
    }
}
