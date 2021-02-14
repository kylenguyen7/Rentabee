using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    public static TransitionController instance;
    Animator animator;
    string targetScene;

    private void Awake() {
        animator = GetComponent<Animator>();
        instance = this;
    }

    public void Transition(string newScene) {
        targetScene = newScene;
        animator.SetTrigger("end");
    }

    public void Restart() {
        targetScene = SceneManager.GetActiveScene().name;
        animator.SetTrigger("end");
    }

    public void GoToTargetScene() {
        SceneManager.LoadScene(targetScene);
    }
}
