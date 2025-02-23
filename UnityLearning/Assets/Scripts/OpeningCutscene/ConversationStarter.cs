using UnityEngine;
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone.");
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger zone.");
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F key pressed and Player in range.");
            if (myConversation != null)
            {
                Debug.Log("Starting conversation...");
                ConversationManager.Instance.StartConversation(myConversation);
            }
            else
            {
                Debug.LogWarning("Conversation not assigned!");
            }
        }
    }
}
