using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneCrossfade : MonoBehaviour
{
    public Animator transition;
    public string defaultColour = "#38BEF9";
    public float transitionTime = 1.0f;
    private GameObject fader;
    // Start is called before the first frame update
    void Start() {
        fader = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        changeFaderColor(defaultColour);
    }

    public void LoadNextLevel(string scenename) {
        StartCoroutine(LoadLevel(scenename));
    }

    public void LoadNextLevelWithColor(string scenename, string color) {
        changeFaderColor(color);
        StartCoroutine(LoadLevel(scenename));
    }
    public void ForceTriggerTransition(string color) {
        changeFaderColor(color);
        transition.SetTrigger("Start");
    }

    void changeFaderColor(string color) {
        Color newCol;
        ColorUtility.TryParseHtmlString(color, out newCol);
        fader.GetComponent<Image>().color = newCol;
    }

    IEnumerator LoadLevel(string scenename) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(scenename);
    }
}
