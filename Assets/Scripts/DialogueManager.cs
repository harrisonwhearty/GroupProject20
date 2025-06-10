using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    private bool entered;

    // Start is called before the first frame update
    void Start()
    {
        entered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !entered)
        {
            if(dialogueBox != null)
            {
                dialogueBox.SetActive(true);
                dialogueBox.GetComponent<Dialogue>().StartDialogue();
                entered = true;
            }
        }
    }
}
