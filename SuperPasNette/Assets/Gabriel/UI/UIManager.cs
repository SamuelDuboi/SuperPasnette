using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TitleScreen titleScreen;
    [SerializeField] private HudScreen hud;
    [SerializeField] private PauseScreen pauseScreen;
    [SerializeField] private EndScreen endScreen;
    [SerializeField] private CreditScreen creditScreen;

	public event Action OnQuitLevel;
	public event Action Restart;
	public event Action<bool> OnPause;
	public event Action OnPlay;

	private GameObject nextScreen;
	private bool openEndScreen = false;

    // Start is called before the first frame update
    void Start()
    {
		titleScreen.onPlay += TitleScreen_onPlay;
		titleScreen.onCredit += TitleScreen_onCredit;
		titleScreen.closed += TitleScreen_closed;

		hud.onPause += Hud_onPause;
		hud.closed += Hud_closed;

		pauseScreen.onResume += PauseScreen_onResume;
		pauseScreen.onQuit += PauseScreen_onQuit;
		pauseScreen.closed += PauseScreen_closed;

		endScreen.OnRestart += EndScreen_OnRestart;
		endScreen.closed += EndScreen_closed;

		creditScreen.onBack += CreditScreen_onBack;
		creditScreen.closed += CreditScreen_closed;
    }

	private void EndScreen_closed()
	{
		if (nextScreen == hud.gameObject)
		{
			hud.gameObject.SetActive(true);
		}
		else
		{
			titleScreen.gameObject.SetActive(true);
		}
	}

	private void EndScreen_OnRestart(bool obj)
	{
		if(obj)
		{
			Restart?.Invoke();
			nextScreen = hud.gameObject;
			openEndScreen = false;
			endScreen.Close();
		}
		else
		{
			OnQuitLevel?.Invoke();
			nextScreen = titleScreen.gameObject;
		}
	}

	public void setEndScreen(ENDSCREEN_TYPE type)
	{
		hud.Close();
		endScreen.Init(type);
		openEndScreen = true;
	}

	private void PauseScreen_closed()
	{
		if (nextScreen == hud.gameObject)
		{
			hud.gameObject.SetActive(true);
		}
		else
		{
			titleScreen.gameObject.SetActive(true);
		}
			
	}

	private void PauseScreen_onResume()
	{
		pauseScreen.Close();
		nextScreen = hud.gameObject;
		OnPause?.Invoke(false);
	}

	private void PauseScreen_onQuit()
	{
		pauseScreen.Close();
		nextScreen = titleScreen.gameObject;
		OnQuitLevel?.Invoke();
	}

	private void Hud_closed()
	{
		if (openEndScreen) endScreen.gameObject.SetActive(true);
		else pauseScreen.gameObject.SetActive(true);
	}

	private void Hud_onPause()
	{
		hud.Close();
		OnPause?.Invoke(true);
	}

	public void UpdateItemInList(int index)
	{
		hud.UpdateItem(index);
	}

	public HudScreen getHud() { return hud; }

	public void FillGroceries(List<string> groceries)
	{
		hud.FillList(groceries);
	}

	public void OnGroceriesList(bool isOpen)
	{
		hud.ShowList(isOpen);
	}

	private void CreditScreen_closed()
	{
		titleScreen.gameObject.SetActive(true);
	}

	private void CreditScreen_onBack()
	{
		creditScreen.Close();
	}

	private void TitleScreen_closed()
	{
		if(nextScreen == hud.gameObject)
		{
			hud.gameObject.SetActive(true);
		}
		else
		{
			creditScreen.gameObject.SetActive(true);
		}
	}

	private void TitleScreen_onCredit()
	{
		titleScreen.Close();
		nextScreen = creditScreen.gameObject;
	}

	private void TitleScreen_onPlay()
	{
		titleScreen.Close();
		nextScreen = hud.gameObject;
		OnPlay?.Invoke();
	}   
}
