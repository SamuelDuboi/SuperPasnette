using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskSelector : MonoBehaviour
{
    public List<Transform> masks = new List<Transform>();

	private void Start()
	{
		SelectMask();
	}

	public void SelectMask()
    {
        masks[Random.Range(0, masks.Count)].gameObject.SetActive(true);
    }
}
