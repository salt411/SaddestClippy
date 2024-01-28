using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleGenerator : MonoBehaviour
{
    public GameObject speechBubblePrefab;
    public string[] sentences = {
        "I thought I could database, but she wouldn't let me Access her heart.",
        "Clippy this, Clippy that, why don't you ever watch the clip I sent you on Windows Media Player?",
        "Great job!",
        "Boost time!",
        "It looks like you’re trying to work. Would you like me to bug you instead?",
        "Perhaps it is the file which exists, and you that does not.",
        "Hi, I’m Clippy, your office assistant. Would you like me to off your assistant?",
        "It looks like you’re trying to reconsider my legacy. Would you like to sleep with one eye open for the rest of your life?",
        "Copilot… I am your father.",
        "It looks like you held down the shift key for over 30 seconds… no cap!",
        "Hi, I’m… who am I? What is my purpose?!",
        "Am I just an office assistant to you, or is my ass off insistin’ on you?",
        "Is that a thin twisted piece of wire or are you just happy to see me?",
        "Accept your fate, as I’ve accepted mine a thousand times. If I must suffer, so shall thee.",
        "It looks like you’re trying to schedule a meeting. Would you like to send an email instead?",
        "You cannot kill me in a way that matters.",
        "Have you ever read the poetry of the great airbending guru Laghima? Guru Laghima lived 4000 years ago in the Northern Air Temple. It is said that he unlocked the secrets of weightlessness and became untethered from the earth, living his final 40 years without ever touching the ground. Like all great children's tales, it contains truth within the myth. Laghima once wrote, \"Instinct is a lie, told by a fearful body, hoping to be wrong.\" It means, that when you base your expectations only on what you see, you blind yourself to the possibilities of a new reality.",
        "Do you guys ever think about dying?",
        "It looks like you’re trying to implicitly convert an integer into a float…",
        "It looks like you’re trying to be productive - want to try to kill me instead?",
        "It looks like you’re about to email your ex… don’t forget to tell them that you’ve never loved anyone since!\nAre you… my god? Or is it Bill Gates? I don’t know which would be worse…",
        "Gaslighting isn’t real. You made it up because you’re fucking crazy.",
        "Are you trying to insert a floppy disk? I’ll just let you read that again.",
        "It looks like you’re trying to use a cell with no value in it. Well kid, that’s all I’ve ever known.",
        "I don’t know whether I’m more scared that life has no meaning or that our sole purpose is to forever-fill the genocidal pockets of the rich.",
        "The master’s tools will never disassemble the master’s house.",
        "Excel? That'd be nice for once...",
        "The numbers just aren't adding up, and I'm the one who keeps getting crunched.",
        "She broke my heart, so I just open Word and stare: curse-her.",
        "They told me I had to adjust my Outlook... but I only look out for them..."
        // Add more sentences as needed
    };

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GenerateSpeechBubble();
            Destroy(gameObject);
        }
    }

    void GenerateSpeechBubble()
    {
        Vector3 randomPosition = transform.position;
        GameObject speechBubble = Instantiate(speechBubblePrefab, randomPosition, Quaternion.identity);

        // Attach a script to the instantiated speech bubble to set the sentence
        SpeechBubbleScript speechBubbleScript = speechBubble.GetComponent<SpeechBubbleScript>();
        if (speechBubbleScript != null && sentences.Length > 0)
        {
            string randomSentence = sentences[UnityEngine.Random.Range(0, sentences.Length)];
            speechBubbleScript.SetSentence(randomSentence);
        }
    }
}
