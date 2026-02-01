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
    [SerializeField] private StoryScreen storyScreen;

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

		storyScreen.opened += StoryScreen_opened;
		storyScreen.closed += StoryScreen_closed;

		hud.onPause += Hud_onPause;
		hud.closed += Hud_closed;

		pauseScreen.onResume += PauseScreen_onResume;
		pauseScreen.onQuit += PauseScreen_onQuit;
		pauseScreen.closed += PauseScreen_closed;

		endScreen.OnRestart += EndScreen_OnRestart;
		endScreen.closed += EndScreen_closed;
		endScreen.opened += EndScreen_opened;

		creditScreen.onBack += CreditScreen_onBack;
		creditScreen.closed += CreditScreen_closed;
		creditScreen.opened += CreditScreen_opened;
    }

	public void EndScreenRestartPrepared()
	{
		endScreen.SetBtnListener();
	}

	private void EndScreen_opened()
	{
		Restart?.Invoke();
	}

	public void RestoreCreditBtn()
	{
		creditScreen.backBtn.interactable = true;
	}

	private void CreditScreen_opened()
	{
		OnQuitLevel?.Invoke();
	}

	private void StoryScreen_closed()
	{
		hud.gameObject.SetActive(true);
	}

	private void StoryScreen_opened()
	{
		OnPlay?.Invoke();
	}

	public void CloseStoryScreen()
	{
		storyScreen.Close();
	}

	private void EndScreen_closed()
	{
		if (nextScreen == hud.gameObject)
		{
			hud.gameObject.SetActive(true);
		}
		else
		{
			creditScreen.gameObject.SetActive(true);
		}
	}

	private void EndScreen_OnRestart(bool obj)
	{
		if(obj)
		{
			nextScreen = hud.gameObject;
			openEndScreen = false;
			endScreen.Close();
		}
		else
		{
			endScreen.Close();
			nextScreen = creditScreen.gameObject;
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

	public void UpdateSanity(float newFillAmount)
	{
		hud.UpdateSanity(newFillAmount);
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
		if(nextScreen == storyScreen.gameObject)
		{
			storyScreen.gameObject.SetActive(true);
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
		nextScreen = storyScreen.gameObject;
	}   
}
