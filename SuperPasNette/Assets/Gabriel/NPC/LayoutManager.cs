using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{
    public List<GameObject> shelfToMove;
	public void ChangeLayout()
	{
		GameObject currentGO;
		Transform currentTR;
		for (int i = 0; i < shelfToMove.Count; i++)
		{
			currentGO = shelfToMove[i];
			currentTR = currentGO.transform.GetChild(0);
			currentGO.transform.position = currentTR.position;
			currentGO.transform.rotation = currentTR.rotation;
			currentGO.transform.localScale = currentTR.localScale;
		}
	}
}
