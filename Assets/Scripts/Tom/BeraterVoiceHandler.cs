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

    bool isShown = true;

    [SerializeField]
    bool debugToggle = false;

    private void Start()
    {
        // Maybe setup a tutorial/intro which the berater tells us
    }

    private void Update()
    {
        if (debugToggle)
        {
            if (isShown)
            {
                HideBerater();
            }
            else
            {
                ShowBerater();
            }
            debugToggle = false;
        }
    }

    public void ShowBerater()
    {
        isShown = true;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void HideBerater()
    {
        isShown = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void Say(AudioClip audio)
    {
        ShowBerater();

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

        yield return new WaitForSeconds(2.0f);
        HideBerater();
    }
}
