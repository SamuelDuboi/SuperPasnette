using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCManager : MonoBehaviour
{
    public List<NPC> npcs = new List<NPC>();
    public List<Transform> positions = new List<Transform>();
	public float cooldownBetweenMoves;

	private int countArrived = 0;
	private bool isPaused = false;
	public event Action<int> SanityDecrease;

    // Start is called before the first frame update
    public void Init()
    {
		for (int i = 0; i < npcs.Count; i++)
		{
			//npcs[i].Arrived += NPCManager_Arrived;
			npcs[i].SanityDecrease += NPCManager_SanityDecrease;
		}

		//MoveAll();
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

