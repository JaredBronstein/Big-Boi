using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField]
    private string sceneLoad;

    private bool isPlayerInTrigger;

    private void Awake()
    {
        if (sceneLoad == string.Empty)
        {
            throw new System.Exception(gameObject.name + " has no scene to load. Add one in you dummy!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger)
        {
            SceneManager.LoadScene(sceneLoad);
        }
    }
}
