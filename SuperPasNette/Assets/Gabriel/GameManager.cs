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
	private int nPickedUpItem = 0;

	private void Start()
	{
		npcManager.SanityDecrease += NpcManager_SanityDecrease;
		npcManager.OnDeath += NpcManager_OnDeath;
		uiManager.OnPause += UiManager_OnPause;
		uiManager.OnPlay += UiManager_OnPlay;
		uiManager.Restart += UiManager_Restart;
		player.OnPickUp += Player_OnPickUp;
		player.OnTalk += Player_OnTalk;
	}

	private void Player_OnTalk(Client obj)
	{
		if(obj.gameObject.CompareTag("Cashier") && nPickedUpItem == 0)
		{
			Debug.Log("You Win");
			StartCoroutine(TimeBeforeOpenEndScreen(2f));
		}
		else
		{
			Debug.Log("Tu entends quelqu'un parler");
		}
	}

	private void Player_OnPickUp()
	{
		nPickedUpItem++;
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

	IEnumerator TimeBeforeOpenEndScreen(float waitingTime)
	{
		yield return new WaitForSeconds(waitingTime);
		uiManager.setEndScreen(ENDSCREEN_TYPE.WIN);
		
	}
}
