using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [HideInInspector]
    public int iIndex;
    public int OnPickUp() 
    {
        StartCoroutine(WaitAndKill());
        return iIndex;
    }

    IEnumerator WaitAndKill()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
