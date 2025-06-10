using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textCompnent;
    public string[] lines;
    public float textSpeed;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textCompnent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textCompnent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textCompnent.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        if (lines.Length == 0)
            return;

        index = 0;
        textCompnent.text = string.Empty; // Ensure text component is cleared
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textCompnent.text = string.Empty; // Ensure text component is cleared
        yield return null; // Ensure text component is cleared

        foreach (char c in lines[index].ToCharArray())
        {
            textCompnent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textCompnent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }


}
