using System.Collections;
using Codes.Util;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DialogueSystem : MonoBungleton<DialogueSystem>
    {
        public TextMeshProUGUI textComponent;
        public TextMeshProUGUI textName;
        public string[] lines;
        public float textSpeed;

        private int _index;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            textComponent.text = string.Empty;
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.Space)) return;
            if (textComponent.text == lines[_index]) NextLine();
            else
            { 
                StopAllCoroutines();
                textComponent.text = lines[_index];
            }
        }

        public void StartDialogue()
        {
            _index = 0;
            StartCoroutine(TypeLine());
        }

        private IEnumerator TypeLine()
        {
            textComponent.text = string.Empty;
            foreach (var c in lines[_index])
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }

        private void NextLine()
        {
            if(_index < lines.Length - 1)
            {
                _index++;
                StartCoroutine(TypeLine());
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public void SetDialogue(string[] lines, string name)
        {
            this.lines = lines;
            this.textName.text = name;
        }
    }
}