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
			currentTR = currentGO.transform;
			
			for (int j = 0; j < currentGO.transform.childCount; j++)
			{
				if (currentGO.transform.GetChild(j).CompareTag("TransformObj"))
					currentTR = currentGO.transform.GetChild(j);
			}

			currentGO.transform.position = currentTR.position;
			currentGO.transform.rotation = currentTR.rotation;
			currentGO.transform.localScale = currentTR.localScale;
		}
	}
}
