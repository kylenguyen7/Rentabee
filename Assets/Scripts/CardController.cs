using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] Vector2 originalPosition;
    private Vector2 dragStartPosition;
    private RectTransform rectTransform;
    [SerializeField] float maxDragDist;
    [SerializeField] float maxRotation;
    [SerializeField] float maxAngle;
    [SerializeField] float swipeRatioThreshold;
    [SerializeField] float maxSwipedRotation;
    [SerializeField] float maxSwipedAngle;
    [SerializeField] Image myImage;

    private CardStats myStats;
    private AudioSource myAudio;

    // Audio clips
    private AudioClip swipeSound;

    public float SwipeRatio {
        get { return dragRatio; }
    }

    float dragRatio = 0f;

    float radius;
    bool swipedRight = false;
    bool titleDescFadedIn = false;

    private enum cardState {
        none,
        dragging,
        swiped
    }
    private cardState state;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        // originalPosition = new Vector2(0, 0);
        radius = Vector2.Distance(GameManager.instance.cardPivot.anchoredPosition, originalPosition);
        myAudio = GetComponent<AudioSource>();

        swipeSound = Resources.Load<AudioClip>("Sounds/Card Swipe");
    }

    public void SetStats(CardStats stats) {
        myStats = stats;
        myImage.sprite = myStats.image;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if(!titleDescFadedIn) {
            CardSpawnerController.instance.FadeInTitleDesc();
            titleDescFadedIn = true;
        }

        state = cardState.dragging;
        dragStartPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData) {
        Vector2 currentPosition = eventData.position;
        dragRatio = Mathf.Clamp((currentPosition.x - dragStartPosition.x) / maxDragDist, -1f, 1f);
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (Mathf.Abs(dragRatio) > swipeRatioThreshold) {
            if (dragRatio > 0) {
                if (myStats.canBeDenied && myStats.energyCost < 0 && Mathf.Abs(myStats.energyCost) > PlayerStatsManager.instance.getEnergy()) {
                    Debug.Log("Not enough energy to do this!");
                    GameManager.instance.CreateWarning("You don't have enough energy!");
                    dragRatio = 0;
                    state = cardState.none;
                } else if (myStats.canBeDenied && myStats.lumpMoney < 0 && Mathf.Abs(myStats.lumpMoney) > PlayerStatsManager.instance.getMoney()) {
                    Debug.Log("Not enough money to do this!");
                    GameManager.instance.CreateWarning("You don't have enough money!");
                    dragRatio = 0;
                    state = cardState.none;
                } else {
                    state = cardState.swiped;
                }
            }
            else {
                if(!myStats.canBeDenied) {
                    Debug.Log("This card cannot be denied!");
                    GameManager.instance.CreateWarning("You cannot deny this card!");
                    dragRatio = 0;
                    state = cardState.none;
                } else {
                    state = cardState.swiped;
                }
            }
        } else {
            dragRatio = 0;
            state = cardState.none;
        }

        if(state == cardState.swiped) {
            swipedRight = dragRatio > 0;
            OnComplete();
        }
    }

    float currentAngle = 0f;
    float rotation = 0f;

    // Update is called once per frame
    void Update() {

        if(state == cardState.none) {
            float rotZ = rectTransform.rotation.eulerAngles.z;
            if (rotZ > 180) {
                rotZ -= 360;
            }
            if (!Mathf.Approximately(rotZ, 0)) {
                float newRotZ = rotZ + (0 - rotZ) * Mathf.Min(5f * Time.deltaTime, 1f);
                rectTransform.rotation = Quaternion.Euler(0f, 0f, newRotZ);
            }
            Vector2 currentPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = Vector2.Lerp(currentPosition, originalPosition, Mathf.Min(3f * Time.deltaTime, 1f));
        } else {
            if(state == cardState.dragging) {
                rotation = Mathf.Lerp(0, maxRotation * Mathf.Sign(dragRatio), Mathf.Abs(dragRatio));
                currentAngle = Mathf.Lerp(0, maxAngle * Mathf.Sign(dragRatio), Mathf.Abs(dragRatio));

            } else if(state == cardState.swiped) {
                float swipeDirection = swipedRight ? 1f : -1f;

                rotation = Mathf.Lerp(rotation, maxSwipedRotation * Mathf.Sign(swipeDirection), Mathf.Min(2f * Time.deltaTime, 1f));
                currentAngle = Mathf.Lerp(currentAngle, maxSwipedAngle * Mathf.Sign(swipeDirection), Mathf.Min(2f * Time.deltaTime, 1f));            
            }
            float x = -radius * Mathf.Cos((90 + currentAngle) * Mathf.Deg2Rad);
            float y = GameManager.instance.cardPivot.anchoredPosition.y + radius * Mathf.Sin((90 + currentAngle) * Mathf.Deg2Rad);
            rectTransform.rotation = Quaternion.Euler(0f, 0f, -rotation);
            rectTransform.anchoredPosition = new Vector2(x, y);
        }

        if (!titleDescFadedIn && Vector2.Distance(originalPosition, rectTransform.anchoredPosition) < 50f) {
            CardSpawnerController.instance.FadeInTitleDesc();
            titleDescFadedIn = true;
        }
    }

    void OnComplete() {
        CardSpawnerController.instance.FadeOutTitleDesc();
        CardSpawnerController.instance.SpawnCard();

        myAudio.PlayOneShot(swipeSound, 0.5f);

        if(swipedRight) {
            PlayerStatsManager.instance.addMoney(myStats.lumpMoney);
            PlayerStatsManager.instance.addCredit(myStats.lumpCredit);
            PlayerStatsManager.instance.addHappiness(myStats.happiness);
            PlayerStatsManager.instance.addEnergy(myStats.energyCost);
        } else {
            PlayerStatsManager.instance.addMoney(myStats.noLumpMoney);
            PlayerStatsManager.instance.addCredit(myStats.noLumpCredit);
            PlayerStatsManager.instance.addHappiness(myStats.noHappiness);
            PlayerStatsManager.instance.addEnergy(myStats.noEnergyCost);
        }
        Debug.Log(PlayerStatsManager.instance.getMoney());

        PlayerStatsManager.instance.DoTurn();
        Invoke("DestroySelf", 3f);
    }

    void DestroySelf() {
        Destroy(gameObject);
    }
}
