using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardSpawnerController : MonoBehaviour
{
    public static CardSpawnerController instance;
    public void Awake() {
        if(instance != null) {
            Debug.LogError("More than one CardSpawner found!");
            return;
        }

        instance = this;
        animator = GetComponent<Animator>();
    }
    public int cardIndex = 0;

    [SerializeField] GameObject cardPrefab;

    [SerializeField] RectTransform spawnPoint;
    [SerializeField] Transform cardGroup;
    [SerializeField] CardStats[] possibleCards;
    [SerializeField] bool randomize;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI descText;
    private Animator animator;

    private CardController currentCard;
    private CardStats currentCardStats;

    public float CurrentCardSwipeRatio {
        get { return currentCard.SwipeRatio; }
    }

    public CardStats CurrentCardStats {
        get { return currentCardStats; }
    }

    public void Start() {
        SpawnCard();
    }

    public void SpawnCard() {
        if(randomize || cardIndex >= possibleCards.Length) {
            currentCard = Instantiate(cardPrefab, spawnPoint.transform.position, Quaternion.identity, cardGroup).GetComponent<CardController>();
            currentCardStats = possibleCards[Random.Range(4, possibleCards.Length)];
            currentCard.SetStats(currentCardStats);
        }
        else {
            currentCard = Instantiate(cardPrefab, spawnPoint.transform.position, Quaternion.identity, cardGroup).GetComponent<CardController>();
            currentCardStats = possibleCards[cardIndex];
            currentCard.SetStats(currentCardStats);
            cardIndex++;
            if(cardIndex >= possibleCards.Length) {
                // cardIndex = 0;
            }
        }
    }

    public void FadeInTitleDesc() {
        Debug.Log("Fade in triggered!");
        titleText.text = currentCardStats.title;
        descText.text = currentCardStats.description;
        animator.SetTrigger("fadeIn");
    }

    public void FadeOutTitleDesc() {
        Debug.Log("Fading out title and desc!");
        animator.SetTrigger("fadeOut");
    }
}
