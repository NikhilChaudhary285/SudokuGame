using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Testing : MonoBehaviour
{
	[SerializeField]
	private GameObject GridMainPanelDisplay;

	// GridsButtons
	Button[] GridMainPanelDisplayButtons;

	[SerializeField]
	Button userInputGridButton;


	private void Start()
	{
		// Storing GridMainPanelDisplayButtons
		GridMainPanelDisplayButtons = GridMainPanelDisplay.GetComponentsInChildren<Button>();

		GameObject SelectedGridFromButtonReference = userInputGridButton.transform.parent.parent.gameObject;
		Button[] SelectedGridFromButtonReferenceButtons = SelectedGridFromButtonReference.GetComponentsInChildren<Button>();
		for (int SelectedGridFromButtonReferenceButtonIndex = 0; SelectedGridFromButtonReferenceButtonIndex < SelectedGridFromButtonReferenceButtons.Length; SelectedGridFromButtonReferenceButtonIndex++)
		{
			Debug.Log(SelectedGridFromButtonReferenceButtons[SelectedGridFromButtonReferenceButtonIndex].gameObject.name);
		}
		//Debug.Log(SelectedGridFromButtonReference.GetComponentsInChildren<Button>());
	}
}
