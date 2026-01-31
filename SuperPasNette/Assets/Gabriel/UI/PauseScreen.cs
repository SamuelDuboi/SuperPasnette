using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public Button resumeBtn;
    public Button quitBtn;

    public Animator anim;

    public event Action onResume;
    public event Action onQuit;
    public event Action closed;

    // Start is called before the first frame update
    void Start()
    {
        resumeBtn.onClick.AddListener(OnResume);
        quitBtn.onClick.AddListener(OnQuit);
    }

	private void OnEnable()
	{
        anim.SetBool("isClosing", false);
	}

	private void OnResume()
	{
        onResume?.Invoke();
	}

	private void OnQuit()
	{
        onQuit?.Invoke();
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
