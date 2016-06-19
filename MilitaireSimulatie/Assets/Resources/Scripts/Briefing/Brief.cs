using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TextMesh))]
[RequireComponent(typeof(AudioSource))]
public class Brief : MonoBehaviour {
    [SerializeField]
    private string briefText;
    [SerializeField]
    private float briefInterval = 0.1f;
    [SerializeField]
    private float waitTime = 3f;
    [SerializeField]
    private string sceneName;

    private TextMesh textMesh;
    private AudioSource audioSource;

    private bool ready = false;
	// Use this for initialization
	void Start () {
        textMesh = GetComponent<TextMesh>();
        audioSource = GetComponent<AudioSource>();

        audioSource.loop = true;
        audioSource.Play();
        StartCoroutine(writeText());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator writeText()
    {
        for (int i = 0; i < briefText.Length; i++)
        {
            if (briefText[i] == '~') 
                textMesh.text += "\n";
            else
                textMesh.text += briefText[i];
            yield return new WaitForSeconds (briefInterval);
        }
        audioSource.Stop();
        
        if(!ready)
        {
            ready = true;
            yield return new WaitForSeconds(waitTime);
        }

        SceneManager.LoadScene(sceneName);
    }
}
