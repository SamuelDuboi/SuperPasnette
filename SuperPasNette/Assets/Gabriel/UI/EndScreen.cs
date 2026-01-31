using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
	public TMP_Text title;
	public TMP_Text text;
	public TMP_Text buttonText;
	public Button mainBtn;

	public Animator anim;

	public event Action<bool> OnRestart;
	public event Action closed;

	private ENDSCREEN_TYPE chosenType;

	private void OnEnable()
	{
		anim.SetBool("isClosing", false);
	}

	public void Init(ENDSCREEN_TYPE type)
	{
		chosenType = type;
		mainBtn.onClick.AddListener(OnClick);

		switch (chosenType)
		{
			case ENDSCREEN_TYPE.MR_X:
				title.text = "Tu as perdu";
				text.text = "Vous n’avez plus le choix : le masque qu’il vous tend est le vôtre. Le mettre vous ouvre les yeux et toute cette course-poursuite vous semble être une perte de temps. Ce qu’il vous faut maintenant, c’est de continuer les courses.Toujours plus de courses. Pour toujours.";
				buttonText.text = "Reessayer";
				break;
			case ENDSCREEN_TYPE.SANITY:
				title.text = "Tu as perdu";
				text.text = "Un grand coup de fatigue vous envahit. A quoi bon cette soirée? Demain vous paraît loin et inutile. Et vous êtes si bien ici…";
				buttonText.text = "Reessayer";
				break;
			case ENDSCREEN_TYPE.WIN:
				title.text = "Tu as gagne";
				text.text = "Vous produits sont lentement scannés par le caissier qui ne vous adresse pas un mot. Est - il déçu? Est-il complice? Est-il juste fatigué ? Vous sortez et rejoignez la soirée en vous demandant si tout ceci était bien réel.";
				buttonText.text = "Suivant";
				break;
		}
	}

	private void OnClick()
	{
		switch (chosenType)
		{
			case ENDSCREEN_TYPE.MR_X:
				OnRestart?.Invoke(true);
				break;
			case ENDSCREEN_TYPE.SANITY:
				OnRestart?.Invoke(true);
				break;
			case ENDSCREEN_TYPE.WIN:
				OnRestart?.Invoke(false);
				break;
		}
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
