using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeraterVoiceHandler : MonoBehaviour
{
    public AudioClip antwort1;
    public AudioClip menuChairs;
    public AudioClip menuTables;
    public AudioClip menuWardrobes;
    public AudioClip finishOrder;

    public void Say(AudioClip audio)
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("isTalking", true);
        gameObject.GetComponent<AudioSource>().PlayOneShot(audio);

        StartCoroutine(StopTalkingAfter(audio.length, animator));
    }

    public void Antwort1()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("isTalking", true);
        gameObject.GetComponent<AudioSource>().PlayOneShot(antwort1);

        StartCoroutine(StopTalkingAfter(antwort1.length, animator));
    }

    private IEnumerator StopTalkingAfter(float seconds, Animator animator)
    {
        yield return new WaitForSeconds(seconds);
        animator.SetBool("isTalking", false);
    }
}
