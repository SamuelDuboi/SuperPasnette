using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditScreen : MonoBehaviour
{
    public Button backBtn;
    public Animator anim;

    public event Action onBack;
    public event Action closed;
    public event Action opened;
    // Start is called before the first frame update
    void Start()
    {
        backBtn.onClick.AddListener(OnBack);
    }

	private void OnEnable()
	{
        anim.SetBool("isClosing", false);
	}

	private void OnBack()
	{
        onBack?.Invoke();
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

    public void Opened()
	{
        opened?.Invoke();
        //backBtn.interactable = false;
	}
}
