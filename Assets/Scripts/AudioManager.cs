using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        // Keeps music playing when switching scenes, and deletes duplicates
        if (GameObject.FindGameObjectsWithTag("Music").Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}

