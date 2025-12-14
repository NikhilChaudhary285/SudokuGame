using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Change name of this script (FirstButtonSelector Script) to (GridButtonSelector Script)
public class FirstButtonSelector : MonoBehaviour
{
	// FirstButton
	public Button FirstButton;

	// SudokoValidator Script
	public SudokoValidator sudokoValidator;

	// ScriptsReferences Script
	public ScriptsReferences scriptsReferences;

	// Button userInputGridButton (Reference To Just Store GridUserInputButton)
	Button userInputGridButton;

	#region ColorManagerForGridButtons
	// gridButtonSelectedColor
	Color gridButtonSelectedColor;

	// gridButtonVHGButtonsSelectedColor
	Color gridButtonVHGButtonsSelectedColor;

	// ButtonReferenceIndexToChangeColor
	int ButtonReferenceIndexToChangeColor;

	// userInputGridButtonIndex
	string userInputGridButtonIndex;

	// colorChangeGridButton
	Button colorChangeGridButton;
	#endregion

	// ButtonReferences : Main Dictionary To store ButtonReferences With Index Grasping These ButtonReferences from ButtonReferences Script (Dictionary ButtonReferences)
	Dictionary<int, Button> ButtonReferences;

	// IsInteractable : Property for Specific Button(FirstButton) i.e. it IsInteractable or Not)
	public bool IsInteractable = true;

	// Start is called before the first frame update
	void Start()
	{
		// ButtonReferences : Main Dictionary To store ButtonReferences With Index Grasping These ButtonReferences from ButtonReferences Script (Dictionary ButtonReferences)
		ButtonReferences = scriptsReferences.buttonReferencesGenerator.ButtonReferences;

		#region DefaultButtonSelected
		// FirstButton Selected By Default
		FirstButton.Select();		
		#endregion

		#region ColorManagerForGridButtons
		// gridButtonSelectedColor
		ColorUtility.TryParseHtmlString("#A4D7E0", out gridButtonSelectedColor);
		// gridButtonVHGButtonsSelectedColor
		ColorUtility.TryParseHtmlString("#D9D9D9", out gridButtonVHGButtonsSelectedColor);
		#endregion

		#region Calling below methods by default for selected FirstButton by Default
		// userInputGridButton
		userInputGridButton = FirstButton;
		// Calling PressedGridButton() from sudokoValidator Script
		sudokoValidator.PressedGridButton(userInputGridButton);
		// Calling ReInitializeAllGridButtonsGameObjectImageColorToDefault() from (scriptsReferences.gridButtonSelector) Script
		scriptsReferences.gridButtonSelector.ReInitializeAllGridButtonsGameObjectImageColorToDefault();
		// ColorSelectorForGridButtonAndgridButtonVHGButtons
		ColorSelectorForGridButtonAndgridButtonVHGButtons();
		#endregion


	}

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
}
