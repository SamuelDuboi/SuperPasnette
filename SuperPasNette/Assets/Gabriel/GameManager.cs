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
		npcManager.OnDeath += NpcManager_OnDeath;
		uiManager.OnPause += UiManager_OnPause;
		uiManager.OnPlay += UiManager_OnPlay;
		uiManager.Restart += UiManager_Restart;
	}

	private void Update()
	{
		npcManager.UpdateMrX(player.transform.position);
	}


	private void NpcManager_OnDeath()
	{
		uiManager.setEndScreen(ENDSCREEN_TYPE.MR_X);
		npcManager.StopNPC();
	}

	private void UiManager_Restart()
	{
		Debug.Log("Reload LevelScene");
	}

	private void UiManager_OnPlay()
	{
		npcManager.InitNPC();
		//npcManager.InitMrX();
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
