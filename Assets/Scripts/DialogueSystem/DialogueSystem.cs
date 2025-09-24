using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{

    public static DialogueSystem instance { get; private set; }

    private Dictionary<string, DialogueBlock> planetToDialogueMap = new();

    public bool isDialogueOngoing = false;

    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private AudioSource dialogueBleep;

    private void Awake()
    {
        instance = this;
        // Initialize all planet dialogues
        planetToDialogueMap.Add("Moon", new DialogueBlock("Moon"));
        planetToDialogueMap.Add("Venus", new DialogueBlock("Venus"));
        planetToDialogueMap.Add("Mars", new DialogueBlock("Mars"));
        planetToDialogueMap.Add("Jupiter", new DialogueBlock("Jupiter"));
        planetToDialogueMap.Add("Mercury", new DialogueBlock("Mercury"));
        planetToDialogueMap.Add("Saturn", new DialogueBlock("Saturn"));
    }

    private void Start()
    {
        PlanetDiscoverySystem.instance.OnPlanetDiscovered += OnPlanetDiscovered;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        // Good practice: unsubscribe when destroyed
        if (PlanetDiscoverySystem.instance != null)
        {
            PlanetDiscoverySystem.instance.OnPlanetDiscovered -= OnPlanetDiscovered;
        }
    }

    private void OnPlanetDiscovered(object sender, string e)
    {
        Debug.Log("Planet discovered " + e + "Will play the dilaogue");
        if (planetToDialogueMap.ContainsKey(e))
        {
            StartCoroutine(DialogueEggTimer(planetToDialogueMap[e]));
        }
        else
        {
            Debug.LogWarning("No dialogue block found for planet: " + e);
        }
    }

    public void StartDialogueTimer(DialogueBlock block)
    {
        StartCoroutine(DialogueEggTimer(block));
    }

    public IEnumerator DialogueEggTimer(DialogueBlock block)
    {
        yield return new WaitForSeconds(1f);
        isDialogueOngoing = true;
        transform.GetChild(0).gameObject.SetActive(true);

        // Block all input when dialogue starts

        // Loop through every line in the block
        foreach (var dialogue in block.GetDialogues())
        {
            string text = dialogue.Key;
            float waitTime = dialogue.Value;

            // For now: just log it (later you'd show it in UI)
            Debug.Log("NPC says: " + text);
            dialogueText.SetText(text);

            // Wait before the next line
            dialogueBleep.Play();
            yield return new WaitForSeconds(waitTime);
        }

        Debug.Log("Dialogue block finished.");
        isDialogueOngoing = false;
        transform.GetChild(0).gameObject.SetActive(false);
        // Unblock input when dialogue ends
    }
}
