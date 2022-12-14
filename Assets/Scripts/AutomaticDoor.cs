using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    public Animator doorAnim;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnim.SetTrigger("open");
            
            if (SoundManager.Instance)
            {
                SoundManager.Instance.PlayGeneralAudio(GameEnums.GeneralAudio.DOOROPEN);
            }
            Destroy(gameObject);
        }
    }
}
