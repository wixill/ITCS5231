using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
 

    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(ObjectDialogue dialogue)
    {
        nameText.text = dialogue.name;
     

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplaySentence();
      
    }

    public void DisplaySentence()
    {
        if(sentences.Count == 0)
        {
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

}
