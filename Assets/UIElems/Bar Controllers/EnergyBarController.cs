using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarController : BarController {
    protected override float GetMaxValue() {
        return PlayerStatsManager.MAX_ENERGY;
    }

    protected override float GetValue() {
        return PlayerStatsManager.instance.getEnergy();
    }
}
