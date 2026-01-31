using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudScreen : MonoBehaviour
{
    public Button pauseBtn;
    public GameObject list;
	public GameObject listContainer;
	[SerializeField]
	private GameObject interactionObject;
    public Animator anim;
    public Animator animList;

    private List<TMP_Text> listTexts = new List<TMP_Text>();

    public event Action onPause;
    public event Action closed;

    // Start is called before the first frame update
    void Start()
    {
        pauseBtn.onClick.AddListener(OnPause);
		for (int i = 0; i < listContainer.transform.childCount; i++)
		{
			listTexts.Add(listContainer.transform.GetChild(i).GetComponent<TMP_Text>());
		}
    }

	private void OnEnable()
	{
        anim.SetBool("isClosing", false);
	}

	private void OnPause()
	{
		onPause?.Invoke();
	}

	public void ToggleInteraction (bool bActive)
	{
		interactionObject.SetActive(bActive);
	}


	public void FillList(List<string> groceries)
	{
		for (int i = 0; i < groceries.Count; i++)
		{
            listTexts[i].text = groceries[i];
		}
	}

	public void UpdateItem(int index)
	{
		listTexts[index].fontStyle = FontStyles.Strikethrough;
	}

	public void ShowList(bool isShowing)
	{
        list.SetActive(isShowing);
        animList.SetBool("isClosing", !isShowing);
	}

    public void SmartphoneOpen()
	{

	}

    public void SmartphoneClose()
	{

	}

    public void Close()
	{
        anim.SetBool("isClosing", true);
	}

    public void Closed()
	{
        gameObject.SetActive(false);
        closed?.Invoke();
	}
}
