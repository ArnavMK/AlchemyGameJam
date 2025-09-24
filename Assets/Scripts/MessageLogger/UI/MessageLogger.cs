using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageLogger : MonoBehaviour
{
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TMP_Text meesageText_TMP;
    [SerializeField] private float messagetimeLength = 1f; // Changed to 1 second

    private Queue<string> messageQueue = new Queue<string>();
    private bool isShowingMessage = false;

    private void Start()
    {
        // Cooking related
        Cauldron.instance.OnCook += Cauldron_OnCook;
        Cauldron.instance.OnCookFailed += Cauldron_OnCookFailed;

        // mining related
        MineManager.instance.OnResourceMined += MineManager_OnResourceMined;
    }

    private void MineManager_OnResourceMined(object sender, KeyValuePair<string, string> e)
    {
        AddMessageToQueue(e.Value);
    }

    private void Cauldron_OnCookFailed(object sender, string e)
    {
        AddMessageToQueue(e);
    }

    private void Cauldron_OnCook(object sender, KeyValuePair<Resource, int> e)
    {
        AddMessageToQueue("You created " + e.Value.ToString() + " " +  e.Key);
    }

    private void AddMessageToQueue(string message)
    {
        messageQueue.Enqueue(message);
        
        // Start processing queue if not already running
        if (!isShowingMessage)
        {
            StartCoroutine(ProcessMessageQueue());
        }
    }

    private IEnumerator ProcessMessageQueue()
    {
        isShowingMessage = true;
        
        while (messageQueue.Count > 0)
        {
            string message = messageQueue.Dequeue();
            
            // Show the message
            meesageText_TMP.text = message;
            messagePanel.SetActive(true);
            
            // Wait for the specified time
            yield return new WaitForSeconds(messagetimeLength);
            
            // Hide the message
            messagePanel.SetActive(false);
            
            // Small delay between messages to prevent flickering
            yield return new WaitForSeconds(0.1f);
        }
        
        isShowingMessage = false;
    }
}
