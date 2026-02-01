using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScreen : MonoBehaviour
{
    public Animator anim;

	public event Action closed;
	public event Action opened;

	private void OnEnable()
	{
		anim.SetBool("isClosing", false);
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
	}
}
