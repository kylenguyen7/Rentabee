using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionButtonController : UIButtonController {
    [SerializeField] string targetScene;
    protected override void OnClick() {
        TransitionController.instance.Transition(targetScene);
    }
}