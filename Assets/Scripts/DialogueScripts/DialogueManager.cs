using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager _instance;
    public BoostStats boostMenu;
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    private Queue<string> sentences;
    private bool isSentenceTyping = false;
    private string lastSentence = "";
    private UnityEvent dialogueEndEvent;


    // Start is called before the first frame update
    void Start()
    {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } 
        else {
            _instance = this;
            sentences = new Queue<string>();
            enabled = false;
            StartCoroutine(TriggerStartDialogue());
        }
    }

    IEnumerator TriggerStartDialogue() {
        if (boostMenu != null) boostMenu.enabled = false;
        Pause.PauseGame();
        yield return new WaitForSecondsRealtime(1.5f);
        if (boostMenu != null) boostMenu.enabled = true;
        Pause.ResumeGame();
        DialogueStartTrigger[] dialogueStartTriggers = FindObjectsOfType<DialogueStartTrigger>();
        if (dialogueStartTriggers.Length >= 1) {
            dialogueStartTriggers[0].TriggerDialogue();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            DisplayNextSentence();
        }
        else if (Input.GetKeyDown("x"))
        {
            EndDialogue();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (boostMenu != null) boostMenu.enabled = false;
        Pause.PauseGame();
        animator.SetBool("IsOpen", true);
        sentences.Clear();
        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        nameText.text = dialogue.name;
        dialogueEndEvent = dialogue.dialogueEndEvent;
        DisplayNextSentence();
        enabled = true;
    }

    public void DisplayNextSentence() {
        if (isSentenceTyping) {
            StopAllCoroutines();
            dialogueText.text = lastSentence;
            isSentenceTyping = false;
            return;
        }
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }
        lastSentence = sentences.Dequeue();
        if (lastSentence.Length >= 6 && lastSentence.Substring(0, 6) == "@name=") {
            nameText.text = lastSentence.Substring(6);
            lastSentence = sentences.Dequeue();
        }
        StartCoroutine(TypeSentence(lastSentence));
        isSentenceTyping = true;
    }

    IEnumerator TypeSentence (string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(0.02f);
        }
        isSentenceTyping = false;
    }

    public void EndDialogue() {
        StopAllCoroutines();
        animator.SetBool("IsOpen", false);
        enabled = false;
        isSentenceTyping = false;
        dialogueEndEvent?.Invoke();
        if (boostMenu != null) boostMenu.enabled = true;
        Pause.ResumeGame();
    }
}
