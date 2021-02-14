using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    protected bool hovered = false;
    bool pressed = false;
    [SerializeField] Image myButton;
    [SerializeField] Sprite unhoveredButtonSprite;
    [SerializeField] Sprite hoveredButtonSprite;
    [SerializeField] Sprite selectedButtonSprite;
    // public GameManager.Upgrade upgrade;

    protected bool clickable = true;

    protected void Update() {
        if (!clickable) {
            myButton.sprite = selectedButtonSprite;
            return;
        }

        if (hovered) {
            if (pressed && Input.GetMouseButtonUp(0)) {
                OnClick();
                pressed = false;
            }

            if (Input.GetMouseButtonDown(0)) {
                pressed = true;
            }

            myButton.sprite = pressed ? selectedButtonSprite : hoveredButtonSprite;
        } else {
            myButton.sprite = unhoveredButtonSprite;
        }
    }

    protected abstract void OnClick();

    protected virtual void OnHover() { }
    protected virtual void OnUnhover() { }

    public void OnPointerEnter(PointerEventData eventData) {
        hovered = true;
        OnHover();
    }

    public void OnPointerExit(PointerEventData eventData) {
        hovered = false;
        pressed = false;
        OnUnhover();
    }
}
