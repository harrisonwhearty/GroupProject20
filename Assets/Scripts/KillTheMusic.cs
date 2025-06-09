using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTheMusic : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    // Call this from the button's OnClick event
    public void ToggleSound()
    {
        if (audioSource != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
            else
            {
                audioSource.Play();
            }
        }
    }
}