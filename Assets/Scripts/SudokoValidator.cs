using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SudokoValidator : MonoBehaviour
{
	// GridMainPanelDisplay
	[SerializeField]
	private GameObject GridMainPanelDisplay;

	// GridsButtons
	Button[] GridMainPanelDisplayButtons;

	// ScriptsReferences Script
	[SerializeField]
	private ScriptsReferences scriptsReferences;

	#region ColorManagerForGridButtons
	// correctColorForGridMainDisplayButtonsForCorrectInput
	Color correctColorForGridMainDisplayButtonsForCorrectInput;
	
	// errorColorForGridMainDisplayButtonsForWrongInput
	Color errorColorForGridMainDisplayButtonsForWrongInput;

	// gridButtonSelectedColor
	Color gridButtonSelectedColor;

	// defaultColorForGridMainDisplayButtons
	Color defaultColorForGridMainDisplayButtons = Color.white;
	#endregion

	#region Initializes Some Values
	// Getting All Grids
	private GameObject[] Grids;

	// Each Grid MainCellButtons Count to full random values in it
	private Button[] EachGridMainCellButtons;

	// randomIndexList for storing values for grid and to give randomly
	List<int> randomIndexList;

	// GridReferences
	Dictionary<int, GameObject> GridReferences = new Dictionary<int, GameObject>();

	// GridButtonsInitializes
	List<Button[]> GridButtonsInitializes = new List<Button[]>();

	int GridMainCellButtonsArraySize;

	#endregion

	// ButtonReferences : Main Dictionary To store ButtonReferences With Index Grasping These ButtonReferences from ButtonReferences Script (Dictionary ButtonReferences)
	Dictionary<int, Button> ButtonReferences;

	// _userInputButton (Holding NormalUserInputButton Pressed)
	Button userInputButton;

	// userInputGridButton (Holding userInputGridButton Pressed)
	[HideInInspector]
	public Button userInputGridButton;
	
	// Storing _userInputButton || UserInputButton's Value (Specific Value)
	string userInput;	
	
	// Storing _userInputGridButton || _userInputGridButton's Value (Specific Value)
	string userInputGridButtonIndex;	
	

	// Storing _userInputGridButton || _userInputGridButton's Value (Specific Value)
	string selectedButtonToPutValueIndex;

	// userInputValueExist : To Check userInputValueExist or not it is responsible to tell how much logics have to be run for comparisons and the logics are: 1) For Row 2) For Col 3) For Grid
	bool userInputValueExist = false;
	
	#region InformationButton Concept
	// Button InformationButton
	[SerializeField]
	private Button InformationButton;

	// IsInformationButtonActive
	[HideInInspector]
	public bool IsInformationButtonActive = false;
	#endregion

	// randomSudokuObject
	private int[,] randomSudokuObject;

	#region ButtonsOfWorongValues Concept
	// ButtonsOfWorongValues
	List<Button> ButtonsOfWorongValues = new List<Button>();

	// Storing buttonWorongValue || buttonWorongValue's Value (Specific Value)
	string buttonWorongValue;

	// Storing buttonsOfWorongValueGridButtonIndex || buttonsOfWorongValueGridButtonIndex's Value (Specific Value)
	string buttonsOfWorongValueGridButtonIndex;

	// buttonsOfWorongValueGridButton (Holding buttonsOfWorongValueGridButton Pressed)
	Button buttonsOfWorongValueGridButton;

	// buttonWorongValueExist : To Check buttonWorongValueExist or not it is responsible to tell we have to change " buttonsOfWorongValueGridButton " Color to Correct Color or not(i.e. Leave as it is) and it is manipulated by these comparisons and the logics are: 1) For Row 2) For Col 3) For Grid
	bool buttonWorongValueExist = false;
	#endregion

	// AllFieldsFilledOrValueCorrect
	private bool AllFieldsFilledOrValueCorrect;

	// AllFieldsFilled
	bool AllFieldsFilled = false;

	private void Awake()
	{
		// We performing this below initialization because we have to take this reference before start method calling
		// Storing GridMainPanelDisplayButtons
		GridMainPanelDisplayButtons = GridMainPanelDisplay.GetComponentsInChildren<Button>();

	}

	// Start is called before the first frame update
	void Start()
	{
		#region usable after (1
		// For Only Getting Grids Count or Length
		Grids = GameObject.FindGameObjectsWithTag("Grid");

		/*// Gettings Each Grid MainCellButtons Count to fill random values in it
		EachGridMainCellButtons = Grids[0].GetComponentsInChildren<Button>();
		GridMainCellButtonsArraySize = EachGridMainCellButtons.Length;

		// ReinitializeListRandomList
		ReinitializeListRandomList(GridMainCellButtonsArraySize);

		// StoringGridReferences
		StoringGridReferences();

		// InitializingGridButtonsReferences
		InitializingGridButtonsReferences();*/
		#endregion

		AddListeners();
		#region ColorManagerForGridButtons
		// correctColorForGridMainDisplayButtonsForCorrectInput
		ColorUtility.TryParseHtmlString("#761717", out correctColorForGridMainDisplayButtonsForCorrectInput);
		
		// errorColorForGridMainDisplayButtonsForWrongInput
		ColorUtility.TryParseHtmlString("#E32E2E", out errorColorForGridMainDisplayButtonsForWrongInput);

		// gridButtonSelectedColor
		ColorUtility.TryParseHtmlString("#A4D7E0", out gridButtonSelectedColor);
		#endregion

		// ButtonReferences : Main Dictionary To store ButtonReferences With Index Grasping These ButtonReferences from ButtonReferences Script (Dictionary ButtonReferences)
		ButtonReferences = scriptsReferences.buttonReferencesGenerator.ButtonReferences;

		// ReInitializing AllFieldsFilledOrValueCorrect
		AllFieldsFilledOrValueCorrect = false;

		// InsertingValues
		//InsertingValues();

		// AllFieldsFilledValidator
		//AllFieldsFilledValidator();

	}

	#region usable after (1
	#region StoringGridReferences
	void StoringGridReferences()
	{
		string baseName = "Grid";
		for (int index = 0; index < Grids.Length; index++)
		{
			GridReferences.Add(index, GameObject.Find($"{index + 1}{baseName}")); //Grid || Index

		}
	}
	#endregion

	#region InitializingGridButtonsReferences
	void InitializingGridButtonsReferences()
	{
		GridButtonsInitializes.Add(GridReferences[0].GetComponentsInChildren<Button>()); //1GridButtonsElements || Index: 0
		GridButtonsInitializes.Add(GridReferences[1].GetComponentsInChildren<Button>()); //2GridButtonsElements || Index: 1
		GridButtonsInitializes.Add(GridReferences[2].GetComponentsInChildren<Button>()); //3GridButtonsElements || Index: 2
		GridButtonsInitializes.Add(GridReferences[3].GetComponentsInChildren<Button>()); //4GridButtonsElements || Index: 3
		GridButtonsInitializes.Add(GridReferences[4].GetComponentsInChildren<Button>()); //5GridButtonsElements || Index: 4
		GridButtonsInitializes.Add(GridReferences[5].GetComponentsInChildren<Button>()); //6GridButtonsElements || Index: 5
		GridButtonsInitializes.Add(GridReferences[6].GetComponentsInChildren<Button>()); //7GridButtonsElements || Index: 6
		GridButtonsInitializes.Add(GridReferences[7].GetComponentsInChildren<Button>()); //8GridButtonsElements || Index: 7
		GridButtonsInitializes.Add(GridReferences[8].GetComponentsInChildren<Button>()); //9GridButtonsElements || Index: 8

	}
	#endregion

	#region ReinitializeList (randomIndexList)
	void ReinitializeListRandomList(int GridMainCellButtonsArraySize)
	{
		randomIndexList = new List<int>();
		for (int i = 1; i < (GridMainCellButtonsArraySize + 1); i++)
		{
			randomIndexList.Add(i);
		}
	}
	#endregion
	#endregion

	void AddListeners()
	{
		#region InformationButton Concept
		// IsInformationButtonActive
		InformationButton.onClick.AddListener(OnClick_InformationButton);
		#endregion
	}

	#region TakingUserInput
	public void TakingUserInput(Button _userInputButton)
	{
		userInputButton = _userInputButton;

		if (userInputGridButton != null)
		{
			// Calling UserInputAnalyzer Method : Whenever userGivingInput and with selected PressedGridButton
			UserInputAnalyzer(userInputButton, userInputGridButton);

			// ButtonsOfWorongValues For Loop : To Implement the logic of changing color of ButtonsOfWorongValues on the basis of if buttonWorongValueExist or not, and it will be checked by all comparisons Logics : 1) For Row 2) For Col 3) For Grid 
			for (int ButtonsOfWorongValuesIndex = 0; ButtonsOfWorongValuesIndex < ButtonsOfWorongValues.Count; ButtonsOfWorongValuesIndex++)
			{
				ButtonsOfWorongValuesAnalyzer(ButtonsOfWorongValues[ButtonsOfWorongValuesIndex]);
			}

			// AllFieldsFilledValidator() Method Logic : Conditions To Check if AllSudokuButtonsText is not empty or filled text value : On the basis of this we can represent Win Message
			AllFieldsFilledValidator();

		}
	}
	#endregion

	#region PressedGridButton
	public void PressedGridButton(Button _userInputGridButton)
	{
		userInputGridButton = _userInputGridButton;
	}
	#endregion

	#region UserInputAnalyzer
	void UserInputAnalyzer(Button _userInputButton, Button _userInputGridButton)
	{
		//Debug.Log($"userInputButton: {_userInputButton.gameObject.name.Substring(0, 1)} userGridInput: {_userInputGridButton.gameObject.name.Substring(0, 2)}");

		userInputValueExist = false;

		// Storing userInput || UserInputButton's Value (Specific Value)
		userInput = _userInputButton.gameObject.name.Substring(0, 1);

		// Storing userInputGridButtonIndex For Comparisons Vertically, Horizontally or Grid Wise || _userInputGridButton's IndexValue (For: at which Specific Position that Button is exist IndexWise For Comparisons VHG Wise)
		userInputGridButtonIndex = _userInputGridButton.gameObject.name.Substring(0, 2);

		// Storing userInputGridButtonIndex In Integers For Comparisons Vertically and Horizontally by Splitting userInputGridButtonIndex in two Indexes like : 1) For Row 2) For Col 3) For Grid Maybe
		// RowIndex
		int RowIndex = int.Parse(userInputGridButtonIndex.Substring(0, 1));
		// ColIndex
		int ColIndex = int.Parse(userInputGridButtonIndex.Substring(1, 1));

		/*** There are 3 Logics for check userInputValueExist or not : 1) For Row 2) For Col 3) For Grid ***/

		#region IsNumberPossibleInRow : if userInputValueNotExist then check next logic : 1) For Row or Horizontally
		if (!userInputValueExist)
		{
			#region RowWiseChecking For Loop : for Horizontal Comparisons
			for (int i = 0; i < 9; i++)
			{
				string RowIndexGenerator = $"{RowIndex}{i}";
				int ButtonReferenceRowIndex = int.Parse(RowIndexGenerator);

				if (ButtonReferences[ButtonReferenceRowIndex].GetComponentInChildren<TMP_Text>().text == userInput)
				{
					// Condition : To not execute below code for same selected userInputGridButton
					if (ButtonReferences[ButtonReferenceRowIndex] != userInputGridButton)
					{
						userInputValueExist = true;
						break;
					}
				}
			}
			#endregion
		}
		#endregion

		#region IsNumberPossibleInColumn : if userInputValueNotExist then check next logic : 2) For Column or Vertically
		if (!userInputValueExist)
		{
			#region ColumnWiseChecking For Loop : for Vertical Comparisons 
			for (int i = 0; i < 9; i++)
			{
				string ColIndexGenerator = $"{i}{ColIndex}";
				int ButtonReferenceColIndex = int.Parse(ColIndexGenerator);

				if (ButtonReferences[ButtonReferenceColIndex].GetComponentInChildren<TMP_Text>().text == userInput)
				{
					// Condition : To not execute below code for same selected userInputGridButton
					if (ButtonReferences[ButtonReferenceColIndex] != userInputGridButton)
					{
						userInputValueExist = true;
						break;
					}

				}
			}
			#endregion
		}
		#endregion

		#region IsNumberPossibleInGrid : if userInputValueNotExist then check next logic : 3) For Grid or GridButtonsWise || GridButtonsWiseChecking
		if (!userInputValueExist)
		{
			// userInputGridButton
			GameObject SelectedGridFromButtonReference = userInputGridButton.transform.parent.parent.gameObject;
			// SelectedGridFromButtonReferenceButtons
			Button[] SelectedGridFromButtonReferenceButtons = SelectedGridFromButtonReference.GetComponentsInChildren<Button>();

			#region SelectedGridFromButtonReferenceButtons WiseChecking For Loop : for currentSelectedButtonGridButtons Comparisons 
			for (int SelectedGridFromButtonReferenceButtonIndex = 0; SelectedGridFromButtonReferenceButtonIndex < SelectedGridFromButtonReferenceButtons.Length; SelectedGridFromButtonReferenceButtonIndex++)
			{
				string GridButtonIndexGenerator = $"{SelectedGridFromButtonReferenceButtons[SelectedGridFromButtonReferenceButtonIndex].gameObject.name.Substring(0, 2)}";
				int ButtonReferenceGridButtonIndex = int.Parse(GridButtonIndexGenerator);

				if (ButtonReferences[ButtonReferenceGridButtonIndex].GetComponentInChildren<TMP_Text>().text == userInput)
				{
					// Condition : To not execute below code for same selected userInputGridButton
					if (ButtonReferences[ButtonReferenceGridButtonIndex] != userInputGridButton)
					{
						userInputValueExist = true;
						break;
					}

				}
			}
			#endregion
		}
		#endregion

		#region FINALLY : Putting Correct Final Value if userInputValueNotExist then at that time we have checked all comparisons : 1) For Row 2) For Col 3) For Grid
		if (!userInputValueExist)
		{
			if(userInputGridButton.GetComponent<GridButtonSelector>().text != userInput)
			{
				// Putting UserInput In userInputGridButton but with correct Color of Text
				userInputGridButton.GetComponentInChildren<TMP_Text>().text = userInput;
				userInputGridButton.GetComponentInChildren<TMP_Text>().color = correctColorForGridMainDisplayButtonsForCorrectInput;

				userInputGridButton.GetComponent<GridButtonSelector>().text = userInput;

				// Adding ButtonsOfWorongValues : To Store References of these to change colors of these on the basis of comparisonsVHGWise and it is used when two or more values change by user at a time (near about same time)
				if(ButtonsOfWorongValues.Contains(userInputGridButton))
				{
					ButtonsOfWorongValues.Remove(userInputGridButton);
				}
			}
			else
			{
				// Putting emptyValue In userInputGridButton if agian enter same number
				userInputGridButton.GetComponentInChildren<TMP_Text>().text = "";

				userInputGridButton.GetComponent<GridButtonSelector>().text = "";

			}		
		}
		else
		{
			if (userInputGridButton.GetComponent<GridButtonSelector>().text != userInput)
			{
				// Putting UserInput In userInputGridButton but with red Color of Text
				userInputGridButton.GetComponentInChildren<TMP_Text>().text = userInput;
				userInputGridButton.GetComponentInChildren<TMP_Text>().color = errorColorForGridMainDisplayButtonsForWrongInput;

				userInputGridButton.GetComponent<GridButtonSelector>().text = userInput;

				// Adding ButtonsOfWorongValues : To Store References of these to change colors of these on the basis of comparisonsVHGWise and it is used when two or more values change by user at a time (near about same time)
				ButtonsOfWorongValues.Add(userInputGridButton);
			}
			else
			{
				// Putting emptyValue In userInputGridButton if agian enter same number
				userInputGridButton.GetComponentInChildren<TMP_Text>().text = "";

				userInputGridButton.GetComponent<GridButtonSelector>().text = "";

			}
		}
		#endregion

		#region Empty field when click again Concept
		/*if (!userInputValueExist)
		{
			if (userInputGridButton.GetComponentInChildren<GridButtonSelector>().text != "")
			{
				// Putting emptyText In userInputGridButton if GridButtonSelector Script text (<GridButtonSelector>().text) is not empty
				userInputGridButton.GetComponentInChildren<TMP_Text>().text = "";
			}
			else
			{
				// Putting UserInput In userInputGridButton but with correct Color of Text
				userInputGridButton.GetComponentInChildren<TMP_Text>().text = userInput;
				userInputGridButton.GetComponentInChildren<TMP_Text>().color = Color.black;

				userInputGridButton.GetComponentInChildren<GridButtonSelector>().text = userInput;
			}
		}
		else
		{
			if (userInputGridButton.GetComponentInChildren<GridButtonSelector>().text != "")
			{
				// Putting emptyText In userInputGridButton if GridButtonSelector Script text (<GridButtonSelector>().text) is not empty
				userInputGridButton.GetComponentInChildren<TMP_Text>().text = "";
			}
			else
			{
				// Putting UserInput In userInputGridButton but with red Color of Text
				userInputGridButton.GetComponentInChildren<TMP_Text>().text = userInput;
				userInputGridButton.GetComponentInChildren<TMP_Text>().color = errorColorForGridMainDisplayButtonsForWrongInput;

				userInputGridButton.GetComponentInChildren<GridButtonSelector>().text = userInput;
			}
		}*/
		#endregion

		#region FINALLY : Putting Correct Final Value With Correct Color of Text and Knowledge of IsInformationButtonActive or Not // if userInputValueNotExist then at that time we have checked all comparisons : 1) For Row 2) For Col 3) For Grid
		// if userInputValueNotExist then at that time we have checked all comparisons : 1) For Row 2) For Col 3) For Grid and we find that userInputValueNotExist in all these comparisons positions,
		// So we can put userInput in Selcted userInputGridButton with correct Color of Text
		// 1) For Row 2) For Col :->

		/*if (IsInformationButtonActive) // if InformationButtonActive then SetSmallNumber with correct Color of Text
		{
			// Putting (Information) UserInput In userInputGridButton but with correct Color of Text
			userInputGridButton.GetComponentInChildren<GridButtonSelector>().SetSmallNumber(userInput, Color.black);

		}
		else // else if InformationButtonNotActive then SetNumber but with specific Color of Text and Color concept is below
		{
			if (!userInputValueExist)
			{
				// Putting UserInput In userInputGridButton but with correct Color of Text
				userInputGridButton.GetComponentInChildren<TMP_Text>().text = userInput;
				userInputGridButton.GetComponentInChildren<TMP_Text>().color = Color.black;

			}
			else
			{
				// Putting UserInput In userInputGridButton but with red Color of Text
				userInputGridButton.GetComponentInChildren<TMP_Text>().text = userInput;
				userInputGridButton.GetComponentInChildren<TMP_Text>().color = errorColorForGridMainDisplayButtonsForWrongInput;
			}
		}*/
		#endregion
	}

	#endregion

	#region ButtonsOfWorongValuesAnalyzer
	void ButtonsOfWorongValuesAnalyzer(Button _buttonsOfWorongValueGridButton)
	{
		Debug.Log($"_buttonsOfWorongValueGridButton: {_buttonsOfWorongValueGridButton.gameObject.name.Substring(0, 2)}");

		buttonWorongValueExist = false;

		// buttonsOfWorongValueGridButton
		buttonsOfWorongValueGridButton = _buttonsOfWorongValueGridButton;

		// Storing buttonWorongValue || buttonWorongValue's Value (Specific Value)
		buttonWorongValue = _buttonsOfWorongValueGridButton.GetComponentInChildren<TMP_Text>().text;

		// Storing userInputGridButtonIndex For Comparisons Vertically, Horizontally or Grid Wise || _userInputGridButton's IndexValue (For: at which Specific Position that Button is exist IndexWise For Comparisons VHG Wise)
		buttonsOfWorongValueGridButtonIndex = _buttonsOfWorongValueGridButton.gameObject.name.Substring(0, 2);

		// Storing userInputGridButtonIndex In Integers For Comparisons Vertically and Horizontally by Splitting userInputGridButtonIndex in two Indexes like : 1) For Row 2) For Col 3) For Grid Maybe
		// RowIndex
		int RowIndex = int.Parse(buttonsOfWorongValueGridButtonIndex.Substring(0, 1));
		// ColIndex
		int ColIndex = int.Parse(buttonsOfWorongValueGridButtonIndex.Substring(1, 1));

		/*** There are 3 Logics for check userInputValueExist or not : 1) For Row 2) For Col 3) For Grid ***/

		#region IsNumberPossibleInRow : if userInputValueNotExist then check next logic : 1) For Row or Horizontally
		if (!buttonWorongValueExist)
		{
			#region RowWiseChecking For Loop : for Horizontal Comparisons
			for (int i = 0; i < 9; i++)
			{
				string RowIndexGenerator = $"{RowIndex}{i}";
				int ButtonReferenceRowIndex = int.Parse(RowIndexGenerator);

				if (ButtonReferences[ButtonReferenceRowIndex].GetComponentInChildren<TMP_Text>().text == buttonWorongValue)
				{
					// Condition : To not execute below code for same selected userInputGridButton
					if (ButtonReferences[ButtonReferenceRowIndex] != buttonsOfWorongValueGridButton)
					{
						buttonWorongValueExist = true;
						break;
					}
				}
			}
			#endregion
		}
		#endregion

		#region IsNumberPossibleInColumn : if userInputValueNotExist then check next logic : 2) For Column or Vertically
		if (!buttonWorongValueExist)
		{
			#region ColumnWiseChecking For Loop : for Vertical Comparisons 
			for (int i = 0; i < 9; i++)
			{
				string ColIndexGenerator = $"{i}{ColIndex}";
				int ButtonReferenceColIndex = int.Parse(ColIndexGenerator);

				if (ButtonReferences[ButtonReferenceColIndex].GetComponentInChildren<TMP_Text>().text == buttonWorongValue)
				{
					// Condition : To not execute below code for same selected userInputGridButton
					if (ButtonReferences[ButtonReferenceColIndex] != buttonsOfWorongValueGridButton)
					{
						buttonWorongValueExist = true;
						break;
					}

				}
			}
			#endregion
		}
		#endregion

		#region IsNumberPossibleInGrid : if userInputValueNotExist then check next logic : 3) For Grid or GridButtonsWise || GridButtonsWiseChecking
		if (!buttonWorongValueExist)
		{
			// userInputGridButton
			GameObject SelectedGridFromButtonReference = buttonsOfWorongValueGridButton.transform.parent.parent.gameObject;
			// SelectedGridFromButtonReferenceButtons
			Button[] SelectedGridFromButtonReferenceButtons = SelectedGridFromButtonReference.GetComponentsInChildren<Button>();

			#region SelectedGridFromButtonReferenceButtons WiseChecking For Loop : for currentSelectedButtonGridButtons Comparisons 
			for (int SelectedGridFromButtonReferenceButtonIndex = 0; SelectedGridFromButtonReferenceButtonIndex < SelectedGridFromButtonReferenceButtons.Length; SelectedGridFromButtonReferenceButtonIndex++)
			{
				string GridButtonIndexGenerator = $"{SelectedGridFromButtonReferenceButtons[SelectedGridFromButtonReferenceButtonIndex].gameObject.name.Substring(0, 2)}";
				int ButtonReferenceGridButtonIndex = int.Parse(GridButtonIndexGenerator);

				if (ButtonReferences[ButtonReferenceGridButtonIndex].GetComponentInChildren<TMP_Text>().text == buttonWorongValue)
				{
					// Condition : To not execute below code for same selected userInputGridButton
					if (ButtonReferences[ButtonReferenceGridButtonIndex] != buttonsOfWorongValueGridButton)
					{
						buttonWorongValueExist = true;
						break;
					}

				}
			}
			#endregion
		}
		#endregion

		#region FINALLY : Change Color of buttonsOfWorongValueGridButton with Correct Color or Red Color on the basis of if buttonWorongValueExist or not, and we have checked this by all comparisons : 1) For Row 2) For Col 3) For Grid (Logics)
		if (!buttonWorongValueExist)
		{
			// Change Color of buttonsOfWorongValueGridButton with Correct Color of Text
			buttonsOfWorongValueGridButton.GetComponentInChildren<TMP_Text>().color = correctColorForGridMainDisplayButtonsForCorrectInput;

		}
		else
		{
			// Change Color of buttonsOfWorongValueGridButton with red Color of Text
			buttonsOfWorongValueGridButton.GetComponentInChildren<TMP_Text>().color = errorColorForGridMainDisplayButtonsForWrongInput;

		}
		#endregion

	}

	#endregion

	#region InformationButton Concept

	#region OnClick_InformationButton
	public void OnClick_InformationButton()
	{
		if (!IsInformationButtonActive)
		{
			IsInformationButtonActive = true;
			InformationButton.GetComponent<Image>().color = gridButtonSelectedColor;
		}
		else
		{

			IsInformationButtonActive = false;
			InformationButton.GetComponent<Image>().color = defaultColorForGridMainDisplayButtons;
		}
	}
	#endregion

	#endregion

	private bool randomValueExist = false;

	#region RandomInputAnalyzer
	void RandomInputAnalyzer(int value, Button _userInputGridButton)
	{
		int randomValue;
		string randomValueStr;

		randomValueExist = false;

		// Storing randomValue Value (Specific randomValue)
		randomValue = value;

		// randomValueStr
		randomValueStr = randomValue.ToString();

		// userInputGridButton
		userInputGridButton = _userInputGridButton;

		// Storing userInputGridButtonIndex For Comparisons Vertically, Horizontally or Grid Wise || _userInputGridButton's IndexValue (For: at which Specific Position that Button is exist IndexWise For Comparisons VHG Wise)
		userInputGridButtonIndex = _userInputGridButton.gameObject.name.Substring(0, 2);

		// Storing userInputGridButtonIndex In Integers For Comparisons Vertically and Horizontally by Splitting userInputGridButtonIndex in two Indexes like : 1) For Row 2) For Col 3) For Grid Maybe
		// RowIndex
		int RowIndex = int.Parse(userInputGridButtonIndex.Substring(0, 1));
		// ColIndex
		int ColIndex = int.Parse(userInputGridButtonIndex.Substring(1, 1));

		/*** There are 3 Logics for check userInputValueExist or not : 1) For Row 2) For Col 3) For Grid ***/

		#region IsNumberPossibleInRow : if randomValueNotExist then check next logic : 1) For Row or Horizontally
		if (!randomValueExist)
		{
			#region RowWiseChecking For Loop : for Horizontal Comparisons
			for (int i = 0; i < 9; i++)
			{
				string RowIndexGenerator = $"{RowIndex}{i}";
				int ButtonReferenceRowIndex = int.Parse(RowIndexGenerator);

				if (ButtonReferences[ButtonReferenceRowIndex].GetComponentInChildren<TMP_Text>().text == randomValueStr)
				{
					// Condition : To not execute below code for same selected userInputGridButton
					if (ButtonReferences[ButtonReferenceRowIndex] != userInputGridButton)
					{
						randomValueExist = true;
						break;
					}
				}
			}
			#endregion
		}
		#endregion

		#region IsNumberPossibleInColumn : if randomValueNotExist then check next logic : 2) For Column or Vertically
		if (!randomValueExist)
		{
			#region ColumnWiseChecking For Loop : for Vertical Comparisons 
			for (int i = 0; i < 9; i++)
			{
				string ColIndexGenerator = $"{i}{ColIndex}";
				int ButtonReferenceColIndex = int.Parse(ColIndexGenerator);

				if (ButtonReferences[ButtonReferenceColIndex].GetComponentInChildren<TMP_Text>().text == randomValueStr)
				{
					// Condition : To not execute below code for same selected userInputGridButton
					if (ButtonReferences[ButtonReferenceColIndex] != userInputGridButton)
					{
						randomValueExist = true;
						break;
					}

				}
			}
			#endregion
		}
		#endregion

		#region IsNumberPossibleInGrid : if randomValueNotExist then check next logic : 3) For Grid or GridButtonsWise || GridButtonsWiseChecking
		if (!randomValueExist)
		{
			// userInputGridButton
			GameObject SelectedGridFromButtonReference = userInputGridButton.transform.parent.parent.gameObject;
			// SelectedGridFromButtonReferenceButtons
			Button[] SelectedGridFromButtonReferenceButtons = SelectedGridFromButtonReference.GetComponentsInChildren<Button>();

			#region SelectedGridFromButtonReferenceButtons WiseChecking For Loop : for currentSelectedButtonGridButtons Comparisons 
			for (int SelectedGridFromButtonReferenceButtonIndex = 0; SelectedGridFromButtonReferenceButtonIndex < SelectedGridFromButtonReferenceButtons.Length; SelectedGridFromButtonReferenceButtonIndex++)
			{
				string GridButtonIndexGenerator = $"{SelectedGridFromButtonReferenceButtons[SelectedGridFromButtonReferenceButtonIndex].gameObject.name.Substring(0, 2)}";
				int ButtonReferenceGridButtonIndex = int.Parse(GridButtonIndexGenerator);

				if (ButtonReferences[ButtonReferenceGridButtonIndex].GetComponentInChildren<TMP_Text>().text == randomValueStr)
				{
					// Condition : To not execute below code for same selected userInputGridButton
					if (ButtonReferences[ButtonReferenceGridButtonIndex] != userInputGridButton)
					{
						randomValueExist = true;
						break;
					}

				}
			}
			#endregion
		}
		#endregion

		#region FINALLY : Putting Correct Final Value if randomValueNotExist then at that time we have checked all comparisons : 1) For Row 2) For Col 3) For Grid
		if (!randomValueExist)
		{
			// Putting UserInput In userInputGridButton but with correct Color of Text
			userInputGridButton.GetComponentInChildren<TMP_Text>().text = randomValueStr;
			userInputGridButton.GetComponentInChildren<TMP_Text>().color = Color.black;

		}
		else
		{
			// Putting UserInput In userInputGridButton but with red Color of Text
			userInputGridButton.GetComponentInChildren<TMP_Text>().text = randomValueStr;
			userInputGridButton.GetComponentInChildren<TMP_Text>().color = errorColorForGridMainDisplayButtonsForWrongInput;
		}

		#endregion
	}
	#endregion

	#region PuttingRandomValues
	public void PuttingRandomValues()
	{
		// selectedButtonToPutValue
		Button selectedButtonToPutValue;
		// GoToNextStatement
		bool GoToNextStatement = true;
		// FilledRandomValues
		int FilledRandomValues = 0;

		// Defining How Many Times Executing Below Code : That tells how many values we want in the game (Count of Values)
		// Variable is : LimitOfExistingValuesInGrid of SudokuObject Script (SudokuObject.LimitOfExistingValuesInGrid)
		while (FilledRandomValues < SudokuObject.LimitOfExistingValuesInGrid)
		{
			#region Iterating All Grids and putting values one at a time
			for (int GridIndex = 0; GridIndex < Grids.Length; GridIndex++)
			{
				// Getting Grid
				GameObject grid = Grids[GridIndex];

				Button[] GridButtons = grid.GetComponentsInChildren<Button>();

				List<Button> EmptyButtonsList = new List<Button>();

				// 1) Adding EmptyButtons in EmptyButtonsList
				for (int GridButtonIndex = 0; GridButtonIndex < GridButtons.Length; GridButtonIndex++)
				{
					if (string.IsNullOrEmpty(GridButtons[GridButtonIndex].GetComponentInChildren<TMP_Text>().text))
					{
						EmptyButtonsList.Add(GridButtons[GridButtonIndex]);
					}
				}

				// Internal Logic
				while (EmptyButtonsList != null)
				{
					int emptyButtonIndex = UnityEngine.Random.Range(0, EmptyButtonsList.Count);

					// 2) Select Random selectedButtonToPutValue
					selectedButtonToPutValue = EmptyButtonsList[emptyButtonIndex];

					// ReinitializingRandomList Concept
					List<int> randomList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

					while (randomList != null)
					{
						int index = UnityEngine.Random.Range(0, randomList.Count);
						int randomValue = randomList[index];

						// Reinitializing randomValueExist
						randomValueExist = false;

						// Storing selectedButtonToPutValueIndex For Comparisons Vertically, Horizontally or Grid Wise || selectedButtonToPutValueIndex's IndexValue (For: at which Specific Position that Button is exist IndexWise For Comparisons VHG Wise)
						selectedButtonToPutValueIndex = selectedButtonToPutValue.gameObject.name.Substring(0, 2);

						// Storing selectedButtonToPutValueIndex In Integers For Comparisons Vertically and Horizontally by Splitting selectedButtonToPutValueIndex in two Indexes like : 1) For Row 2) For Col 3) For Grid Maybe
						// RowIndex
						int RowIndex = int.Parse(selectedButtonToPutValueIndex.Substring(0, 1));
						// ColIndex
						int ColIndex = int.Parse(selectedButtonToPutValueIndex.Substring(1, 1));

						/*** There are 3 Logics for check randomValueExist or not : 1) For Row 2) For Col 3) For Grid ***/

						#region IsNumberPossibleInRow : if userInputValueNotExist then check next logic : 1) For Row or Horizontally
						if (!randomValueExist)
						{
							#region RowWiseChecking For Loop : for Horizontal Comparisons
							for (int i = 0; i < 9; i++)
							{
								string RowIndexGenerator = $"{RowIndex}{i}";
								int ButtonReferenceRowIndex = int.Parse(RowIndexGenerator);

								if (ButtonReferences[ButtonReferenceRowIndex].GetComponentInChildren<TMP_Text>().text == randomValue.ToString())
								{
									randomValueExist = true;
									break;
									#region extra
									/*// Condition : To not execute below code for same selected userInputGridButton
									if (ButtonReferences[ButtonReferenceRowIndex] != selectedButtonToPutValue)
									{
										randomValueExist = true;
										break;
									}*/
									#endregion
								}
							}
							#endregion
						}
						#endregion

						#region IsNumberPossibleInColumn : if userInputValueNotExist then check next logic : 2) For Column or Vertically
						if (!randomValueExist)
						{
							#region ColumnWiseChecking For Loop : for Vertical Comparisons 
							for (int i = 0; i < 9; i++)
							{
								string ColIndexGenerator = $"{i}{ColIndex}";
								int ButtonReferenceColIndex = int.Parse(ColIndexGenerator);

								if (ButtonReferences[ButtonReferenceColIndex].GetComponentInChildren<TMP_Text>().text == randomValue.ToString())
								{
									randomValueExist = true;
									break;
									#region extra
									/*// Condition : To not execute below code for same selected userInputGridButton
									if (ButtonReferences[ButtonReferenceColIndex] != selectedButtonToPutValue)
									{
										randomValueExist = true;
										break;
									}*/
									#endregion

								}
							}
							#endregion
						}
						#endregion

						#region IsNumberPossibleInGrid : if userInputValueNotExist then check next logic : 3) For Grid or GridButtonsWise || GridButtonsWiseChecking
						if (!randomValueExist)
						{
							// userInputGridButton
							GameObject SelectedGridFromButtonReference = selectedButtonToPutValue.transform.parent.parent.gameObject;
							// SelectedGridFromButtonReferenceButtons
							Button[] SelectedGridFromButtonReferenceButtons = SelectedGridFromButtonReference.GetComponentsInChildren<Button>();

							#region SelectedGridFromButtonReferenceButtons WiseChecking For Loop : for currentSelectedButtonGridButtons Comparisons 
							for (int SelectedGridFromButtonReferenceButtonIndex = 0; SelectedGridFromButtonReferenceButtonIndex < SelectedGridFromButtonReferenceButtons.Length; SelectedGridFromButtonReferenceButtonIndex++)
							{
								string GridButtonIndexGenerator = $"{SelectedGridFromButtonReferenceButtons[SelectedGridFromButtonReferenceButtonIndex].gameObject.name.Substring(0, 2)}";
								int ButtonReferenceGridButtonIndex = int.Parse(GridButtonIndexGenerator);

								if (ButtonReferences[ButtonReferenceGridButtonIndex].GetComponentInChildren<TMP_Text>().text == randomValue.ToString())
								{
									randomValueExist = true;
									break;
									#region extra
									/*// Condition : To not execute below code for same selected userInputGridButton
									if (ButtonReferences[ButtonReferenceGridButtonIndex] != selectedButtonToPutValue)
									{
										randomValueExist = true;
										break;
									}*/
									#endregion
								}
							}
							#endregion
						}
						#endregion

						#region FINALLY : Putting randomValue if randomValueNotExist then at that time we have checked all comparisons : 1) For Row 2) For Col 3) For Grid
						if (!randomValueExist)
						{
							// Putting randomValue In selectedButtonToPutValue
							selectedButtonToPutValue.GetComponentInChildren<TMP_Text>().text = randomValue.ToString();
							// Disabling Specific Button or Cell or Box (in Grid)
							selectedButtonToPutValue.GetComponent<GridButtonSelector>().IsInteractable = false;

							GoToNextStatement = true;
							break;

						}
						else
						{
							//if randomValueNotExist
							GoToNextStatement = false;
							randomList.Remove(randomValue);
						}
						#endregion

					}

					// GoToNextStatement or Not
					if (GoToNextStatement)
					{
						break;
					}
					else
					{
						continue;
					}

				}

			}
			#endregion

			// FilledRandomValues : To Fill Limited randomValues in All Grids (or How many cells or boxes we want to fill)
			FilledRandomValues++;
		}

	}
	#endregion

	#region InsertingRandomValues
	void InsertingRandomValues()
	{
		// randomSudokuObject
		randomSudokuObject = scriptsReferences.randomSudokuObjects.PickUpRandomSudokuObject();

		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				// dictionaryButtonIndexGenerator to get buttonReference
				string dictionaryButtonIndexGenerator = $"{i}{j}";
				// dictionaryButtonIndexGeneratorValueInInt
				int dictionaryButtonIndex = int.Parse(dictionaryButtonIndexGenerator);

				RandomInputAnalyzer(randomSudokuObject[i, j], ButtonReferences[dictionaryButtonIndex]);

			}
		}

	}
	#endregion

	#region InsertingValues
	void InsertingValues()
	{
		// randomSudokuObject
		randomSudokuObject = scriptsReferences.randomSudokuObjects.PickUpRandomSudokuObject();

		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				// dictionaryButtonIndexGenerator to get buttonReference
				string dictionaryButtonIndexGenerator = $"{i}{j}";
				// dictionaryButtonIndexGeneratorValueInInt
				int dictionaryButtonIndex = int.Parse(dictionaryButtonIndexGenerator);

				ButtonReferences[dictionaryButtonIndex].GetComponentInChildren<TMP_Text>().text = randomSudokuObject[i, j].ToString();

			}
		}

	}
	#endregion

	#region RemovingRandomValues
	void RemovingRandomValues()
	{
		// Getting randomSudokuObject
		//randomSudokuObject = scriptsReferences.randomSudokuObjects.PickUpRandomSudokuObject();

		// selectedButtonToEmptyValue
		Button selectedButtonToEmptyValue;

		// FilledRandomValues
		int EmptyRandomValues = 0;

		// Defining How Many Times Executing Below Code : That tells how many values we want in the game (Count of Values)
		// Variable is : LimitOfExistingValuesInGrid of SudokuObject Script (SudokuObject.LimitOfExistingValuesInGrid)
		while (EmptyRandomValues < SudokuObject.LimitOfExistingValuesInGrid)
		{
			#region Iterating All Grids and empty values one at a time
			for (int GridIndex = 0; GridIndex < Grids.Length; GridIndex++)
			{
				// Getting Grid
				GameObject grid = Grids[GridIndex];
				// Getting Buttons of grid
				Button[] GridButtons = grid.GetComponentsInChildren<Button>();
				// FilledButtonsList
				List<Button> FilledButtonsList = new List<Button>();

				#region Internal Logic
				// 1) Adding FilledButtons in FilledButtonsList
				for (int GridButtonIndex = 0; GridButtonIndex < GridButtons.Length; GridButtonIndex++)
				{
					if (!string.IsNullOrEmpty(GridButtons[GridButtonIndex].GetComponentInChildren<TMP_Text>().text))
					{
						FilledButtonsList.Add(GridButtons[GridButtonIndex]);
					}
				}
				int filledButtonIndex = UnityEngine.Random.Range(0, FilledButtonsList.Count);

				// 2) Select Random selectedButtonToEmptyValue
				selectedButtonToEmptyValue = FilledButtonsList[filledButtonIndex];

				// Finally selectedButtonToEmptyValue
				selectedButtonToEmptyValue.GetComponentInChildren<TMP_Text>().text = "";
				#endregion

			}
			#endregion

			// EmptyRandomValues : To Empty Limited randomValues in All Grids (or How many cells or boxes we want to empty)
			EmptyRandomValues++;
		}

	}
	#endregion

	#region AllFieldsFilledValidator
	private void AllFieldsFilledValidator()
	{
		Debug.Log("@");
		// For Loops : To iterating all ButtonReferences Dictionary or go through each cell in sudoku And To Check AllSudokuButtonsText || AllFieldsFilled Or Not
		for (int rows = 0; rows < 9; rows++)
		{
			for (int cols = 0; cols < 9; cols++)
			{
				// dictionaryButtonIndexGenerator
				string dictionaryButtonIndexGenerator = $"{rows}{cols}";
				// dictionaryButtonIndexGeneratorValueInInt
				int dictionaryButtonIndex = int.Parse(dictionaryButtonIndexGenerator);

				// Getting Button
				Button currentButton = ButtonReferences[dictionaryButtonIndex];
				// Getting TMP_Text Component Reference
				TMP_Text currentButtonText = currentButton.GetComponentInChildren<TMP_Text>();

				// Logic : Conditions To Check if AllSudokuButtonsText is empty or filled text value : On the basis of this we can represent Win Message
				if (currentButtonText.text != "")
				{
					AllFieldsFilled = true;
				}
				else
				{
					AllFieldsFilled = false;
					break;
				}
			}
			// To break outer for loop
			if (!AllFieldsFilled)
			{
				break;
			}
		}
		#region 
		/*// Logic : To returning true or false on the basis of AllFieldsFilled or Not
		if (AllFieldsFilled)
		{
			AllFieldsFilled = false;
		}
		else
		{
			AllFieldsFilled = false;
			return false;
		}*/
		#endregion
	}
	#endregion

	#region AllFieldsFilledValidator
	private bool AllFieldsFilledValueValidator()
	{
		bool AllFieldsValueCorrect = true;
		// For Loops : To iterating all ButtonReferences Dictionary or go through each cell in sudoku And To Check AllSudokuButtonsText || AllFieldsValueCorrect Or Not
		for (int rows = 0; rows < 9; rows++)
		{
			for (int cols = 0; cols < 9; cols++)
			{
				// dictionaryButtonIndexGenerator
				string dictionaryButtonIndexGenerator = $"{rows}{cols}";
				// dictionaryButtonIndexGeneratorValueInInt
				int dictionaryButtonIndex = int.Parse(dictionaryButtonIndexGenerator);

				// Getting Button
				Button currentButton = ButtonReferences[dictionaryButtonIndex];
				// Getting TMP_Text Component Reference
				TMP_Text currentButtonText = currentButton.GetComponentInChildren<TMP_Text>();

				// Logic : Conditions To Check if AllSudokuButtonsText is not empty or filled text value is Correct or Not : On the basis of this we can represent Win Message
				if (currentButtonText.color != errorColorForGridMainDisplayButtonsForWrongInput)
				{
					AllFieldsValueCorrect = true;
				}
				else
				{
					AllFieldsValueCorrect = false;
					break;
				}
			}
			// To break outer for loop
			if (!AllFieldsValueCorrect)
			{
				break;
			}
		}

		// Logic : To returning true or false on the basis of AllFieldsValueCorrect or Not
		if (AllFieldsValueCorrect)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	#endregion

	private void Update()
	{
		// if AllFieldsFilled, then Call AllFieldsFilledValueValidator() Method : To check AllFieldsFilledValueCorrect Or Not on the basis of this we represent user Win Message
		if (AllFieldsFilled)
		{
			// ReInitializing AllFieldsFilled : To stop this condition or code execution continuously And Run only one User gives Input or Pressed InputButtons Again (on the basis of AllFieldsFilled Value given by AllFieldsFilledValidator() Method)
			AllFieldsFilled = false;

			AllFieldsFilledOrValueCorrect = AllFieldsFilledValueValidator();

			// if AllFieldsFilledOrValueCorrect, Then show Win Message To User
			if (AllFieldsFilledOrValueCorrect)
			{
				Debug.Log(" *** You Win *** ");
                SceneManager.LoadScene("SudokuMainMenu");
            }
            else
			{
				Debug.Log(" !!! You Lose !!! ");
			}
		}
		
	}
}

