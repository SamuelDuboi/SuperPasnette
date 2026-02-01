using UnityEngine;

public class AudioManagerCustom : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip button1;
    [SerializeField] AudioClip button2;
    [SerializeField] AudioClip button3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PressButton()
    {
        audioSource.clip = button1;
        audioSource.Play();
    }
}
