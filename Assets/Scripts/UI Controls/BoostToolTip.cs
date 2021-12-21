using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostToolTip : MonoBehaviour
{
    public Text toolText;
    void Start() {
        enabled = false;
        gameObject.SetActive(false);
    }
    public void RenderTip(string message)
    {
        transform.position = Input.mousePosition;
        toolText.text = message;
        enabled = true;
        gameObject.SetActive(true);
        StartCoroutine(DelayTooltipClose());
    }

    IEnumerator DelayTooltipClose() {
        yield return new WaitForSecondsRealtime(2f);
        enabled = false;
        gameObject.SetActive(false);
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            enabled = false;
            gameObject.SetActive(false);
        }
    }
}
