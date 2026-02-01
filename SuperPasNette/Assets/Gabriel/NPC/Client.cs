using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    public List<string> barks = new List<string>();

	private List<string> randomizedBarks = new List<string>();

	private void Start()
	{
		foreach (string item in barks)
		{
			randomizedBarks.Add(item);
		}
	}

	public string GetBark()
	{
		string chosenText;
		int randomNumber = Random.Range(0, randomizedBarks.Count);

		chosenText = randomizedBarks[randomNumber];

		randomizedBarks.RemoveAt(randomNumber);

		return chosenText;
	}

}
