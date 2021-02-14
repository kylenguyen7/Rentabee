using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private AudioSource myAudio;
    // private AudioSource myMusicAudio;
    private AudioClip errorSound;

    public void Awake() {
        if (instance != null) {
            Debug.LogError("More than one GameManager found!");
            return;
        }
        instance = this;

        myAudio = GetComponents<AudioSource>()[0];
        errorSound = Resources.Load<AudioClip>("Sounds/Error Sound");
    }

    public RectTransform cardPivot;
    [SerializeField] RectTransform warningsGroup;
    [SerializeField] RectTransform gameEndObjectGroup;
    private bool finishedGame = false;

    public void CreateWarning(string message) {
        var warningPrefab = Resources.Load<GameObject>("Warning");
        var warning = Instantiate(warningPrefab, new Vector2(0, 0), Quaternion.identity, warningsGroup).GetComponent<WarningController>();
        warning.SetText(message);
        myAudio.PlayOneShot(errorSound, 0.05f);
    }

    public void FinishGame() {
        if(finishedGame) {
            return;
        }

        var endgame = Resources.Load<GameObject>("End Game Screen");
        Instantiate(endgame, new Vector2(0, 0), Quaternion.identity, gameEndObjectGroup);
        finishedGame = true;
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            PlayerStatsManager.instance.addCredit(PlayerStatsManager.MAX_CREDIT);
            PlayerStatsManager.instance.addMoney(PlayerStatsManager.MAX_MONEY);
            PlayerStatsManager.instance.addHappiness(PlayerStatsManager.MAX_HAPPINESS);
        }
    }
}
