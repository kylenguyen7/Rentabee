using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBarController : BarController
{
    protected override float GetMaxValue() {
        return PlayerStatsManager.MAX_MONEY;
    }

    protected override float GetValue() {
        return PlayerStatsManager.instance.getMoney();
    }
}
