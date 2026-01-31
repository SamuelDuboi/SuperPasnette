using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCManager : MonoBehaviour
{
    public List<NPC> npcs = new List<NPC>();
	public MrX mrX;
	public GameObject npcsContainer;
	public GameObject clientsContainer;
    public List<Transform> positions = new List<Transform>();
	public float cooldownBetweenMoves;

	private int countArrived = 0;
	private bool isPaused = false;
	public event Action<int> SanityDecrease;
	public event Action OnDeath;

    // Start is called before the first frame update
    public void InitNPC()
    {
		//Spawn NPCs

		/*for (int i = 0; i < npcs.Count; i++)
		{
			npcs[i].Arrived += NPCManager_Arrived;
			npcs[i].SanityDecrease += NPCManager_SanityDecrease;
		}

		MoveAll();*/
    }

	public void InitClients()
	{
		//AddListeners for interaction and Setup random assets on them
	}

	public void KillClients()
	{
		//Called when first object is retrieved, remove Listener for Client, call InitNPC
	}

	public void InitMrX()
	{
		mrX.gameObject.SetActive(true);
		mrX.OnDeath += MrX_OnDeath;
	}

	private void MrX_OnDeath()
	{
		OnDeath?.Invoke();
	}

	public void UpdateMrX(Vector3 dest)
	{
		if (mrX.gameObject.activeSelf)
			mrX.agent.SetDestination(dest);
	}

	public void PauseNPC(bool isPause)
	{
		isPaused = isPause;

		for (int i = 0; i < npcs.Count; i++)
		{
			if (isPause)
				npcs[i].agent.speed = 0;
			else
				npcs[i].agent.speed = npcs[i].baseSpeed;
		}
	}

	public void StopNPC()
	{
		for (int i = 0; i < npcs.Count; i++)
		{
			npcs[i].Arrived -= NPCManager_Arrived;
			npcs[i].SanityDecrease -= NPCManager_SanityDecrease;
			npcs[i].agent.ResetPath();
		}
	}

	private void NPCManager_SanityDecrease(int obj)
	{
		if (isPaused) return;

		SanityDecrease?.Invoke(obj);
		Debug.Log("Sanity Decrease");
	}

	private void NPCManager_Arrived()
	{
		countArrived++;

		if(countArrived == npcs.Count)
		{
			countArrived = 0;
			StartCoroutine(cooldownBeforeNextMove(cooldownBetweenMoves));
		}
	}

	public void MoveAll()
	{
		List<Transform> points = new List<Transform>();

		for (int i = 0; i < positions.Count; i++)
		{
			points.Add(positions[i]);
		}

		int currentIndex = 0;
		foreach (NPC item in npcs)
		{
			currentIndex = UnityEngine.Random.Range(0, points.Count);
			item.Move(points[currentIndex].position);
			points.RemoveAt(currentIndex);
		}
	}

	IEnumerator cooldownBeforeNextMove(float waitingTime)
	{
		yield return new WaitForSeconds(waitingTime);
		MoveAll();
	}
}

