using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public UIManager uiManager;
	public NPCManager npcManager;
	public LayoutManager layoutManager;
	public CharacterController player;

	private int sanity = 100;

	private void Start()
	{
		npcManager.SanityDecrease += NpcManager_SanityDecrease;
		uiManager.OnPause += UiManager_OnPause;
		uiManager.OnPlay += UiManager_OnPlay;
		uiManager.Restart += UiManager_Restart;
	}

	private void UiManager_Restart()
	{
		Debug.Log("Reload LevelScene");
	}

	private void UiManager_OnPlay()
	{
		npcManager.Init();
	}

	private void UiManager_OnPause(bool obj)
	{
		npcManager.PauseNPC(obj);
		player.setPause(obj);
	}

	private void NpcManager_SanityDecrease(int obj)
	{
		sanity -= obj;
		if (sanity <= 0)
		{
			uiManager.setEndScreen(ENDSCREEN_TYPE.SANITY);
			npcManager.StopNPC();
		}
	}
}
