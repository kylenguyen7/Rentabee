using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BarController : MonoBehaviour
{
    [SerializeField] RectTransform greenBar;

    protected abstract float GetMaxValue();
    protected abstract float GetValue();

    public void Update() {
        var scale = greenBar.localScale;
        scale.x = Mathf.Lerp(scale.x, GetValue() / GetMaxValue(), Mathf.Min(4f * Time.deltaTime, 1f));
        greenBar.localScale = scale;
    }
}
