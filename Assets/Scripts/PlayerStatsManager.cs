using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager instance;
    public void Awake() {
        if (instance != null) {
            Debug.LogError("More than one PlayerStatsManager found!");
            return;
        }
        instance = this;
    }

    private static float WIN_THRESHOLD = 0.75f;

    public static float MAX_HAPPINESS = 100f;
    public static float MAX_MONEY = 100f;
    public static int MAX_ENERGY = 10;
    public static int MIN_CREDIT = 200;
    public static int MAX_CREDIT = 850;

    private float money = 50f;
    private int credit = 400;
    private float happiness = 20;

    private float passiveMoney;
    private int passiveCredit;

    private int energy;
    private int week = 1;

    public void DoTurn() {
        addMoney(passiveMoney);
        addCredit(passiveCredit);
        Debug.Log(CardSpawnerController.instance.cardIndex);
        if(CardSpawnerController.instance.cardIndex >= 5) {
            addEnergy(1);
            incrementWeek();
        }
    }

    private void CheckFinishGame() {
        if(getMoney() / MAX_MONEY > WIN_THRESHOLD && getHappiness() / MAX_HAPPINESS > WIN_THRESHOLD) {
            GameManager.instance.FinishGame();
        }
    }

    private void Update() {
        CheckFinishGame();
    }

    public float getMoney() { return money; }
    public void addMoney(float add) { money = Mathf.Clamp(money + add, 0, MAX_MONEY); }

    public int getCredit() { return credit; }
    public void addCredit(int add) { credit = (int)Mathf.Clamp(credit + add, MIN_CREDIT, MAX_CREDIT); }

    public int getWeek() { return week; }
    public void incrementWeek() { week++; }

    // public float getPassiveMoney() { return passiveMoney; }
    // public void addPassiveMoney(float add) { passiveMoney = Mathf.Min(passiveMoney + add, 0); }

    // public int getPassiveCredit() { return passiveCredit; }
    // public void addPassiveCredit(int add) { passiveCredit = (int)Mathf.Min(passiveCredit + credit, 0); }

    public int getEnergy() { return energy;  }
    public void addEnergy(int add) { energy = (int)Mathf.Clamp(energy + add, 0, MAX_ENERGY); }

    public float getHappiness() { return happiness; }
    public void addHappiness(float add) { happiness = Mathf.Clamp(happiness + add, 0, MAX_HAPPINESS); }
}
