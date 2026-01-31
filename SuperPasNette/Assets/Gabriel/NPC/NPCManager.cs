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

    // Start is called before the first frame update
    void Start()
    {
		for (int i = 0; i < npcs.Count; i++)
		{
			npcs[i].Arrived += NPCManager_Arrived;
		}

		MoveAll();
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
			currentIndex = Random.Range(0, points.Count);
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

