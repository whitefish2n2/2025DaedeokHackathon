using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI textcomponent;
    public TextMeshProUGUI textname;
    public string[] lines;
    public float textSpeed;

    private int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textcomponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){
            if (textcomponent.text == lines[index])
            {
                NextLIne();
            }
            else { 
                StopAllCoroutines();
                textcomponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index])
        {
            textcomponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

    }

    void NextLIne()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textcomponent.text = string.Empty;
            StartCoroutine (TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}