using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public NavMeshAgent agent;
	public float baseSpeed;
	public SanityCollider colision;

	public int sanityDecreaseValue;

    public bool isAtDestination = true;
    public event Action Arrived;
	public event Action<int> SanityDecrease;

	private void Start()
	{
		colision.OnCollide += Colision_OnCollide;
		agent.speed = baseSpeed;
	}

	private void Colision_OnCollide()
	{
		SanityDecrease?.Invoke(sanityDecreaseValue);
	}

	public void Move(Vector3 destination)
	{
        agent.SetDestination(destination);
        isAtDestination = false;
	}

	// Update is called once per frame
	void Update()
    {

		if (isAtDestination)
		{
			return;
		}
        else if(Mathf.Round(agent.remainingDistance) <= 0.1f)
		{
            Arrived?.Invoke();
            isAtDestination = true;
		}
    }
}
