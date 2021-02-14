using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessBarConroller : BarController {
    protected override float GetMaxValue() {
        return PlayerStatsManager.MAX_HAPPINESS;
    }

    protected override float GetValue() {
        return PlayerStatsManager.instance.getHappiness();
    }
}
