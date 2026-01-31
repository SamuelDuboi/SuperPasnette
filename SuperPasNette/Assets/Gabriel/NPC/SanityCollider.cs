using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityCollider : MonoBehaviour
{
	public event Action OnCollide;

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
			OnCollide?.Invoke();
	}

}
