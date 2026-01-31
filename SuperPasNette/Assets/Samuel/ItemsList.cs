using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour
{
    List<string> lNames;
    UIManager manager;
    // Start is called before the first frame update
    void Start()
    {
        lNames = new List<string>();
        Item[] items = FindObjectsOfType<Item>();
        int cpt = 0;
        foreach(Item item in items) 
        {
            lNames.Add(item.gameObject.name);
            item.iIndex = cpt;
            cpt++;
        };

        manager = FindObjectOfType<UIManager>();
        if (manager) 
        {
            manager.OnPlay += () =>
                 {
                     StartCoroutine(fillName());
                
                 };
        }
        else 
        {
            print("tu as oublier le ui manager");   
        }
    }

    IEnumerator fillName() 
    {
        yield return new WaitForSeconds(1.5f);// wait for the animation
        while (lNames.Count > 6) 
        {
            lNames.RemoveAt(Random.Range(0, lNames.Count));
        }
        manager.FillGroceries(lNames);
    }

}
