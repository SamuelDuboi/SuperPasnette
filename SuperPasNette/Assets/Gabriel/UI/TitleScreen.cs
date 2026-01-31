using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public Button playBtn;
    public Button quitBtn;
    public Button creditBtn;
    public Animator anim;

    public event Action onPlay;
    public event Action onCredit;
    public event Action closed;

    // Start is called before the first frame update
    void Start()
    {
        playBtn.onClick.AddListener(OnPlay);
        quitBtn.onClick.AddListener(OnQuit);
        creditBtn.onClick.AddListener(OnCredit);
    }

	private void OnEnable()
	{
        anim.SetBool("isClosing", false);
	}

	private void OnCredit()
    {
        onCredit?.Invoke();
    }

    private void OnQuit()
    {
        Application.Quit();
    }

    private void OnPlay()
    {
        onPlay?.Invoke();
    }

    public void Close()
    {
        anim.SetBool("isClosing", true);
    }

    public void Closed()
    {
        gameObject.SetActive(false);
        closed?.Invoke();
    }

}
