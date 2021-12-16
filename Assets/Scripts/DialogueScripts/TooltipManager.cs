using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager _instance;
    private Canvas canvas;
    public Text tooltipText;

    public void Awake()
    {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } 
        else {
            _instance = this;
            canvas = GetComponentInParent<Canvas>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void SetAndShowTooltip(string message, Vector3 pos) {
        gameObject.SetActive(true);
        tooltipText.text = message;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(pos);
        Vector2 movePos;

        //Convert the screenpoint to ui rectangle local point
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out movePos);
        //Convert the local point to world point
        transform.position =  canvas.transform.TransformPoint(movePos);
    }

    public void HideToolTip() {
        gameObject.SetActive(false);
    }

}
