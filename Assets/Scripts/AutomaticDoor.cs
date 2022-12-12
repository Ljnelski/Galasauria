using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    public Animator doorAnim;
    [SerializeField] private AudioSource doorOpenAudioSource;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnim.SetTrigger("open");
            doorOpenAudioSource.Play();
            Destroy(gameObject);
        }
    }
}
