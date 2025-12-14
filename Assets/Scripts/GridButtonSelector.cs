using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridButtonSelector : MonoBehaviour
{
	// GridMainPanelDisplay
	[SerializeField]
	private GameObject GridMainPanelDisplay;

	// GridsButtons
	Button[] GridMainPanelDisplayButtons;

	// GridUserInputButton
	public Button GridUserInputButton;

	// SudokoValidator Script
	public SudokoValidator sudokoValidator;

	// Button userInputGridButton (Reference To Just Store GridUserInputButton)
	Button userInputGridButton;

	// ScriptsReferences Script
	[SerializeField]
	private ScriptsReferences scriptsReferences;

	// ButtonReferences : Main Dictionary To store ButtonReferences With Index Grasping These ButtonReferences from ButtonReferences Script (Dictionary ButtonReferences)
	Dictionary<int, Button> ButtonReferences;

	#region ColorManagerForGridButtons
	// gridButtonSelectedColor
	Color gridButtonSelectedColor;

	// gridButtonVHGButtonsSelectedColor
	Color gridButtonVHGButtonsSelectedColor;

	// defaultColorForGridMainDisplayButtons
	Color defaultColorForGridMainDisplayButtons = Color.white;
	#endregion

	// ButtonReferenceIndexToChangeColor
	int ButtonReferenceIndexToChangeColor;

	// userInputGridButtonIndex
	string userInputGridButtonIndex;

	// colorChangeGridButton
	Button colorChangeGridButton;

	// IsInteractable : Property for Specific Button i.e. it IsInteractable or Not)
	public bool IsInteractable = true;

	// buttonText
	public string text = "";

	private void Awake()
	{
		// We performing this below initialization because we have to take this reference before start method calling
		// Storing GridMainPanelDisplayButtons
		GridMainPanelDisplayButtons = GridMainPanelDisplay.GetComponentsInChildren<Button>();

	}

	// Start is called before the first frame update
	void Start()
	{
		AddListeners();

		#region ColorManagerForGridButtons
		// gridButtonSelectedColor
		ColorUtility.TryParseHtmlString("#A4D7E0", out gridButtonSelectedColor);
		// gridButtonVHGButtonsSelectedColor
		ColorUtility.TryParseHtmlString("#D9D9D9", out gridButtonVHGButtonsSelectedColor);
		#endregion

		// ButtonReferences : Main Dictionary To store ButtonReferences With Index Grasping These ButtonReferences from ButtonReferences Script (Dictionary ButtonReferences)
		ButtonReferences = scriptsReferences.buttonReferencesGenerator.ButtonReferences;

	}
	void AddListeners()
	{
		GridUserInputButton.onClick.AddListener(GivingGridUserInput);
		GridUserInputButton.onClick.AddListener(ReInitializeAllGridButtonsGameObjectImageColorToDefault);
		GridUserInputButton.onClick.AddListener(ColorSelectorForGridButtonAndgridButtonVHGButtons); // After Calling ColorSelectorForGridButtonAndgridButtonVHGButtons Method of Specifc GridButton's GridButtonSelector Script
	}

	void GivingGridUserInput()
	{
		if(IsInteractable)
		{
			userInputGridButton = GridUserInputButton;
			sudokoValidator.PressedGridButton(userInputGridButton);
		}
		else
		{
			userInputGridButton = null;
			sudokoValidator.PressedGridButton(userInputGridButton);
		}

	}

	#region ColorManagerForGridButtons

	#region ColorSelectorForGridButtonAndgridButtonVHGButtons
	public void ColorSelectorForGridButtonAndgridButtonVHGButtons()
	{
		// Change ColorForGridButtonAndgridButtonVHGButtons if selectedButton IsInteractable ( IsInteractable == true )
		if (IsInteractable == true)
		{
			//Changes the userInputGridButton GameObject Image's Color to gridButtonSelectedColor
			userInputGridButton.gameObject.GetComponent<Image>().color = gridButtonSelectedColor;

			// Storing userInputGridButtonIndex For Comparisons Vertically, Horizontally or Grid Wise || _userInputGridButton's IndexValue (For: at which Specific Position that Button is exist IndexWise For Comparisons VHG Wise)
			userInputGridButtonIndex = userInputGridButton.gameObject.name.Substring(0, 2);

			// Storing userInputGridButtonIndex In Integers For Comparisons Vertically and Horizontally by Splitting userInputGridButtonIndex in two Indexes like : 1) For Row 2) For Col 3) For Grid Maybe
			// RowIndex
			int RowIndex = int.Parse(userInputGridButtonIndex.Substring(0, 1));
			// ColIndex
			int ColIndex = int.Parse(userInputGridButtonIndex.Substring(1, 1));

			#region RowWiseColorChangeGridButtons For Loop : Change Color for Horizontal Buttons Comparisons (RowWiseButtons)
			for (int i = 0; i < 9; i++)
			{
				string RowIndexGenerator = $"{RowIndex}{i}";
				ButtonReferenceIndexToChangeColor = int.Parse(RowIndexGenerator);

				// Calling Below ColorSelectorForGridButtonVHGButtons() Method
				ColorSelectorForGridButtonVHGButtons(ButtonReferenceIndexToChangeColor);
			}
			#endregion

			#region ColumnWiseColorChangeGridButtons For Loop : Change Color for Vetical Buttons Comparisons (ColumnWiseButtons)
			for (int i = 0; i < 9; i++)
			{
				string ColIndexGenerator = $"{i}{ColIndex}";
				ButtonReferenceIndexToChangeColor = int.Parse(ColIndexGenerator);

				// Calling Below ColorSelectorForGridButtonVHGButtons() Method
				ColorSelectorForGridButtonVHGButtons(ButtonReferenceIndexToChangeColor);
			}
			#endregion

			#region SelectedGridWiseColorChangeGridButtons For Loop : Change Color for Selected Grid Buttons Comparisons (SelectedGridWiseButtons)
			// SelectedGridFromButtonReference
			GameObject SelectedGridFromButtonReference = userInputGridButton.transform.parent.parent.gameObject;
			// SelectedGridFromButtonReferenceButtons
			Button[] SelectedGridFromButtonReferenceButtons = SelectedGridFromButtonReference.GetComponentsInChildren<Button>();

			#region SelectedGridFromButtonReferenceButtonsWise For Loop : Change Color for currentSelectedButtonGridButtons Comparisons 
			for (int SelectedGridFromButtonReferenceButtonIndex = 0; SelectedGridFromButtonReferenceButtonIndex < SelectedGridFromButtonReferenceButtons.Length; SelectedGridFromButtonReferenceButtonIndex++)
			{
				string GridButtonIndexGenerator = $"{SelectedGridFromButtonReferenceButtons[SelectedGridFromButtonReferenceButtonIndex].gameObject.name.Substring(0, 2)}";
				ButtonReferenceIndexToChangeColor = int.Parse(GridButtonIndexGenerator);

				// Calling Below ColorSelectorForGridButtonVHGButtons() Method
				ColorSelectorForGridButtonVHGButtons(ButtonReferenceIndexToChangeColor);
			}
			#endregion

			#endregion
		}

	}
	#endregion

	#region ColorSelectorForGridButtonVHGButtons Only : (and this method is called after above method and in this first selectedGridButton Color Changed and after that below ColorSelectorForGridButtonVHGButtons Method is Each time to Change ColorForGridButtonVHGButtons)
	void ColorSelectorForGridButtonVHGButtons(int ButtonReferenceIndexToChangeColor)
	{
		// colorChangeGridButton : Current colorChangeGridButton
		colorChangeGridButton = ButtonReferences[ButtonReferenceIndexToChangeColor];

		// Condition : To not execute below code for same selected userInputGridButton
		if (colorChangeGridButton != userInputGridButton)
		{
			colorChangeGridButton.gameObject.GetComponent<Image>().color = gridButtonVHGButtonsSelectedColor;
		}
	}
	#endregion
	#region ReInitializeAllGridButtonsColorToDefault
	public void ReInitializeAllGridButtonsGameObjectImageColorToDefault()
	{
		for (int GridMainPanelDisplayButtonIndex = 0; GridMainPanelDisplayButtonIndex < GridMainPanelDisplayButtons.Length; GridMainPanelDisplayButtonIndex++)
		{
			//ReInitializeAllGridButtonsGameObjectImageColorToDefault to defaultColor or white color
			GridMainPanelDisplayButtons[GridMainPanelDisplayButtonIndex].gameObject.GetComponent<Image>().color = defaultColorForGridMainDisplayButtons;

		}
	}
	#endregion

	#endregion

	#region TryGetTextByName
	public bool TryGetTextByName(string name, out TMP_Text text)
	{
		text = null;
		TMP_Text[] texts = gameObject.GetComponentsInChildren<TMP_Text>();
        foreach (var currentText in texts)
        {
            if(currentText.name.Equals(name))
			{
				text = currentText;
				return true;
			}
        }
        return false;
	}
	#endregion

	#region SetNumber
	public void SetNumber(string number, Color selectedGivenColor)
	{
		if(TryGetTextByName("Value", out TMP_Text text))
		{
			text.text = number;
			text.color = selectedGivenColor;

            for(int i = 1; i < 10; i++)
            {
				if (TryGetTextByName($"number_{i}", out TMP_Text currentText))
				{
					currentText.text = "";
				}
			}
        }

	}
	#endregion

	#region SetSmallNumber
	public void SetSmallNumber(string number, Color defaultColor)
	{
		if (TryGetTextByName($"number_{number}", out TMP_Text text))
		{
			if(text.text != "")
			{
				text.text = "";
			}
			else
			{
				text.text = number;
				text.color = defaultColor;
			}

			if (TryGetTextByName($"Value", out TMP_Text currentText))
			{
				currentText.text = "";
			}

		}

	}
	#endregion
}
