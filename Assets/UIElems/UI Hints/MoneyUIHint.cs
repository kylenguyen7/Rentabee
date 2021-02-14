using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyUIHint : UIHint {
    protected override int calculateHint(float value) {
        if (value == 0) {
            return 0;
        }

        if (value <= -0.2f * PlayerStatsManager.MAX_MONEY) {
            return -2;
        } else if (value < 0) {
            return -1;
        } else if (value < 0.2f * PlayerStatsManager.MAX_MONEY) {
            return 1;
        } else {
            return 2;
        }
    }

    protected override int OnLeftHint() {
        return calculateHint(CardSpawnerController.instance.CurrentCardStats.noLumpMoney);
    }

    protected override int OnRightHint() {
        return calculateHint(CardSpawnerController.instance.CurrentCardStats.lumpMoney);
    }
}
