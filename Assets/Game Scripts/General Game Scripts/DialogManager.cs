using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] Text dialogCharName;
    [SerializeField] int typeSpeed;

    public bool dialogDone = false;

    Dialog dialog;
    int currentLine = 0;
    bool isTyping;

    public event Action OnShowDialog;
    public event Action OnHideDialog;

    public static DialogManager Instance { get; private set; }

    // awake is before start
    private void Awake() {
        Instance = this;
        dialogBox.SetActive(false);
    }


    // Call in GameStateManager
    public void DialogUpdate() {
        if (Input.GetKeyUp(KeyCode.E) && !isTyping) {
            currentLine++;
            if(currentLine < dialog.dialogLines.Count) {
                StartCoroutine(TypeDialog(dialog.dialogLines[currentLine]));
            }
            else {
                dialogBox.SetActive(false);
                OnHideDialog?.Invoke();
                dialogDone = true;
            }
        }
    }

    public void ResetDialog()
    {
        this.currentLine = 0;
    }


    // ShowDialog function to be called by other classes
    public IEnumerator ShowDialog(Dialog dialog, string charName) {
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke(); // OnShowDialog event
        this.dialog = dialog;
        dialogBox.SetActive(true);
        dialogCharName.text = charName;
        StartCoroutine(TypeDialog(dialog.dialogLines[0]));
    }

    // coroutine to run through dialog lines by letter
    public IEnumerator TypeDialog(string dline) {
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in dline.ToCharArray()) {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / typeSpeed);
        }
        isTyping = false;
    }


}
