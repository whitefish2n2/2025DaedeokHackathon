using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public GameObject dialogPanel;
    public Button nextButton;

    public float typingSpeed = 0.05f;

    private string[] lines;
    private int currentLine = 0;
    private bool isTyping = false;

    void Start()
    {
        dialogPanel.SetActive(false);
        nextButton.onClick.AddListener(NextLine);
    }

    public void StartDialog(string[] dialogLines)
    {
        lines = dialogLines;
        currentLine = 0;
        dialogPanel.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogText.text = "";
        foreach (char c in lines[currentLine])
        {
            dialogText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogText.text = lines[currentLine];
            isTyping = false;
            return;
        }

        currentLine++;
        if (currentLine < lines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogPanel.SetActive(false);
        }
    }
}