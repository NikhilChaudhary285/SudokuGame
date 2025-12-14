using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEditor.Tilemaps;

//using UnityEngine.UIElements;

public class ButtonReferencesGenerator : MonoBehaviour
{
	#region Lists of Rows
	List<int> r0 = new List<int>();
	List<int> r1 = new List<int>();
	List<int> r2 = new List<int>();
	List<int> r3 = new List<int>();
	List<int> r4 = new List<int>();
	List<int> r5 = new List<int>();
	List<int> r6 = new List<int>();
	List<int> r7 = new List<int>();
	List<int> r8 = new List<int>();
	#endregion

	#region Lists of Cols
	List<int> c0 = new List<int>();
	List<int> c1 = new List<int>();
	List<int> c2 = new List<int>();
	List<int> c3 = new List<int>();
	List<int> c4 = new List<int>();
	List<int> c5 = new List<int>();
	List<int> c6 = new List<int>();
	List<int> c7 = new List<int>();
	List<int> c8 = new List<int>();
	#endregion

	#region Lists of Grids
	List<int> g0 = new List<int>();
	List<int> g1 = new List<int>();
	List<int> g2 = new List<int>();
	List<int> g3 = new List<int>();
	List<int> g4 = new List<int>();
	List<int> g5 = new List<int>();
	List<int> g6 = new List<int>();
	List<int> g7 = new List<int>();
	List<int> g8 = new List<int>();
	#endregion

	#region usable after (Initializes Some Values)

	// Storing All Grids
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

	// GridsButtons
	Button[] GridMainPanelDisplayButtons;

	// ScriptsReferences Script
	[SerializeField]
	private ScriptsReferences scriptsReferences;

	#region ColorManagerForGridMainDisplayButtonsText;
	// defaultColorForGridMainDisplayButtonsText
	Color defaultColorForGridMainDisplayButtonsText;
	#endregion

	// ButtonReferences : Main Dictionary To store ButtonReferences With Index
	[HideInInspector]
	public Dictionary<int, Button> ButtonReferences = new Dictionary<int, Button>();

	#region randomSudokuObject Concept
	// After all mainpulations this varialbe value is changed and after, this randomSudokuObject have randomValues and some emptyValues also
	private int[,] randomSudokuObject;

	// Used To store solvedSudoku
	private int[,] finalSudokuObject;

	Button buttonReference;
	#endregion

	private void Awake()
	{
		#region Adding and Removing Some Values from SudokuObject
		/*// AddingSudokuObjects
		scriptsReferences.randomSudokuObjects.AddingSudokuObjects();

		// RemovingValues (from specific sudokuObject "randomSudokuObject")
		RemovingValues();*/
		#endregion

		// StoringListReferences of Buttons
		StoringListReferences();

		#region
		/*Rows = new List<List<int>>() { r0, r1, r2, r3, r4, r5, r6, r7, r8 };
		Columns = new List<List<int>>() { c0, c1, c2, c3, c4, c5, c6, c7, c8 };
		Grids = new List<List<int>>() { g0, g1, g2, g3, g4, g5, g6, g7, g8 };*/
		// ButtonReferencesIteratorWithIndexes
		//ButtonReferencesIteratorWithIndexes();
		#endregion	

	}

	// Start is called before the first frame update
	void Start()
	{
		#region usable after (1
		/*// For Only Getting Grids Count or Length
		Grids = GameObject.FindGameObjectsWithTag("Grid");

		// Gettings Each Grid MainCellButtons Count to fill random values in it
		EachGridMainCellButtons = Grids[0].GetComponentsInChildren<Button>();
		GridMainCellButtonsArraySize = EachGridMainCellButtons.Length;

		// ReinitializeListRandomList
		ReinitializeListRandomList(GridMainCellButtonsArraySize);

		// StoringGridReferences
		StoringGridReferences();

		// InitializingGridButtonsReferences
		InitializingGridButtonsReferences();*/
		#endregion

		// Creating SudokuObject
		//SudokuObject sudokuObject = new SudokuObject();
		// GenerateRandomNumbers
		//sudokuObject.GenerateRandomNumbers();

		// InsertingValues : Inserting randomSudokuObject Random Values with someEmptyValues also
		InsertingValues();

		#region ColorManagerForGridMainDisplayButtonsText
		// defaultColorForGridMainDisplayButtonsText
		ColorUtility.TryParseHtmlString("#323232", out defaultColorForGridMainDisplayButtonsText);
		#endregion

	}

	void StoringListReferences()
	{
		// buttonsGameObjectBaseName
		string buttonsGameObjectBaseName = "MainCell";
		// buttonsGameObject
		GameObject buttonsGameObject;
		// buttonReference
		Button buttonReference;

		for (int rows = 0; rows < 9; rows++)
		{
			for (int cols = 0; cols < 9; cols++)
			{
				// buttonsGameObjectName
				string buttonsGameObjectName = $"{rows}{cols}{buttonsGameObjectBaseName}";
				// buttonsGameObject
				buttonsGameObject = GameObject.Find(buttonsGameObjectName);
				// buttonReference
				buttonReference = buttonsGameObject.GetComponent<Button>();

				// dictionaryButtonIndexGenerator
				string dictionaryButtonIndexGenerator = $"{rows}{cols}"; 
				// dictionaryButtonIndexGeneratorValueInInt
				int dictionaryButtonIndexGeneratorValueInInt = int.Parse(dictionaryButtonIndexGenerator);

				ButtonReferences.Add(dictionaryButtonIndexGeneratorValueInInt, buttonReference);

			}
		}

	}

	#region RemovingValues
	void RemovingValues()
	{

		// Getting randomSudokuObject
		randomSudokuObject = scriptsReferences.randomSudokuObjects.PickUpRandomSudokuObject();

		// Setting finalSudokuObject : used To store solvedSudoku
		finalSudokuObject = randomSudokuObject;

		// RandomValues
		List<Tuple<int, int>> RandomValues = scriptsReferences.randomSudokuObjects.GetListOfRandomValues();

		// EmptyRandomValues
		int EmptyRandomValues = 81;

		// Defining How Many Times Executing Below Code : That tells how many values we want to remove in the game (Count of Values)
		// Variable is : LimitOfExistingValuesInGrid of SudokuObject Script (SudokuObject.LimitOfExistingValuesInGrid)
		while (EmptyRandomValues > SudokuObject.LimitOfExistingValuesInGrid)
		{
			// Tuple RandomValues Concept
			int randomIndex = UnityEngine.Random.Range(0, RandomValues.Count);
			Tuple<int, int> removeTuple = RandomValues[randomIndex]; // removeTuple : using for Represents index of randomSudokuObject positions values
			RandomValues.RemoveAt(randomIndex);

			// Removing randomValue with given TupleIndex and reInitializing Value to zero
			randomSudokuObject[removeTuple.Item1, removeTuple.Item2] = 0;

			EmptyRandomValues--;
		}
	}
	#endregion

	#region extraInsertingValues
	/*public void InsertingValues(int row, int column, Button buttonReference)
	{
		#region randomSudokuObject Concept
		// Inserting randomSudokuObject Random Values with someEmptyValues also
		//int valueForbuttonReferenceText = randomSudokuObject[row, column];
		#endregion

		int valueForbuttonReferenceText = randomSudokuObject[row, column];
		if (valueForbuttonReferenceText != 0)
		{
			buttonReference.GetComponent<GridButtonSelector>().IsInteractable = false;
			buttonReference.GetComponentInChildren<TMP_Text>().text = valueForbuttonReferenceText.ToString();
		}
		else
		{
			buttonReference.GetComponent<GridButtonSelector>().IsInteractable = true;
			buttonReference.GetComponentInChildren<TMP_Text>().text = "";
		}
	}*/
	#endregion

	#region InsertingValues
	public void InsertingValues()
	{
		// randomSudokuObject
		randomSudokuObject = scriptsReferences.extraTesting.Values;

		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				// dictionaryButtonIndexGenerator to get buttonReference
				string dictionaryButtonIndexGenerator = $"{i}{j}";
				// dictionaryButtonIndexGeneratorValueInInt
				int dictionaryButtonIndex = int.Parse(dictionaryButtonIndexGenerator);
				// ButtonReference
				buttonReference = ButtonReferences[dictionaryButtonIndex];

				// valueForbuttonReferenceText : Inserting randomSudokuObject Random Values with someEmptyValues also
				int valueForbuttonReferenceText = randomSudokuObject[i, j];

				if (randomSudokuObject[i, j] != 0)
				{
					buttonReference.GetComponent<GridButtonSelector>().IsInteractable = false;
					buttonReference.GetComponentInChildren<TMP_Text>().text = valueForbuttonReferenceText.ToString();
				}
				else
				{
					buttonReference.GetComponent<GridButtonSelector>().IsInteractable = true;
					buttonReference.GetComponentInChildren<TMP_Text>().text = "";
				}

			}
		}
		
	}
	#endregion

	#region InsertingFinalSudokuObjectValues
	public void InsertingFinalSudokuObjectValues(int[,] _finalSudokuObjectValues)
	{
		// finalSudokuObject
		finalSudokuObject = _finalSudokuObjectValues;

		//Debug.Log("InsertingFinalSudokuObjectValues");
		//IteratingSudokuValues();

		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 9; j++)
			{
				// dictionaryButtonIndexGenerator to get buttonReference
				string dictionaryButtonIndexGenerator = $"{i}{j}";
				// dictionaryButtonIndexGeneratorValueInInt
				int dictionaryButtonIndex = int.Parse(dictionaryButtonIndexGenerator);
				// ButtonReference
				buttonReference = ButtonReferences[dictionaryButtonIndex];

				// valueForbuttonReferenceText : Inserting randomSudokuObject Random Values with someEmptyValues also
				int valueForbuttonReferenceText = finalSudokuObject[i, j];

				if (finalSudokuObject[i, j] != 0)
				{
					buttonReference.GetComponent<GridButtonSelector>().IsInteractable = false;
					buttonReference.GetComponentInChildren<TMP_Text>().text = valueForbuttonReferenceText.ToString();
					buttonReference.GetComponentInChildren<TMP_Text>().color = defaultColorForGridMainDisplayButtonsText;
				}
				else
				{
					buttonReference.GetComponent<GridButtonSelector>().IsInteractable = true;
					buttonReference.GetComponentInChildren<TMP_Text>().text = "";
				}

			}
		}

	}
	#endregion

	#region IteratingSudokuValues
	void IteratingSudokuValues()
	{
		for (int i = 0; i < 9; i++)
		{
			string rowValue = "";
			for (int j = 0; j < 9; j++)
			{
				rowValue += $"{finalSudokuObject[i, j]}, ";
			}
			Debug.Log(rowValue + "\n");
		}
	}
	#endregion
	#region ButtonReferencesIteratorWithIndexes : Iterating ButtonReferences With Indexes
	void ButtonReferencesIteratorWithIndexes()
	{
		foreach (KeyValuePair<int, Button> ButtonReferencesValue in ButtonReferences)
		{
			Debug.Log($"ButtonReferencesKey: {ButtonReferencesValue.Key} ButtonReferencesValue: {ButtonReferencesValue.Value.gameObject.name}");

		}

	}
	#endregion

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

	#region For Button Value Comparisons

	/*#region IsNumberPossibleInPosition : Checking IsNumberPossibleInPosition, if userInputValueExist or Not in all comparisons : 1) For Row 2) For Col 3) For Grid according to this we can return true or false 
	public bool IsNumberPossibleInPosition(string userInput, int RowIndex, int ColIndex, Button userInputGridButton)
	{
		#region Checking IsNumberPossibleInPosition : if userInputValueExist or Not in all comparisons : 1) For Row 2) For Col 3) For Grid according to this we can return true or false 
		// if userInputValueNotExist then at that time we have checked all comparisons : 1) For Row 2) For Col 3) For Grid and we find that userInputValueNotExist in all these comparisons positions,
		// So we can put userInput in Selcted userInputGridButton with correct Color of Text
		// 1) For Row 2) For Col :->
		if (IsNumberPossibleInRow(userInput, RowIndex, userInputGridButton) && IsNumberPossibleInColumn(userInput, ColIndex, userInputGridButton))
		{
			// 3) For Grid :->
			if (IsNumberPossibleInGrid(userInput, userInputGridButton))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			return false;
		}
		#endregion
	}
	#endregion

	#region IsNumberPossibleInRow : if userInputValueNotExist then check next logic : 1) For Row or Horizontally
	public bool IsNumberPossibleInRow(string userInput, int RowIndex, Button userInputGridButton)
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
					return false;
				}

			}
		}
		#endregion
		return true;
	}
	#endregion

	#region IsNumberPossibleInColumn : if userInputValueNotExist then check next logic : 2) For Column or Vertically
	public bool IsNumberPossibleInColumn(string userInput, int ColIndex, Button userInputGridButton)
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
					return false;
				}

			}
		}
		#endregion
		return true;
	}
	#endregion

	#region IsNumberPossibleInGrid : if userInputValueNotExist then check next logic : 3) For Grid or GridButtonsWise || GridButtonsWiseChecking
	public bool IsNumberPossibleInGrid(string userInput, Button userInputGridButton)
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
					return false;
				}

			}
		}
		#endregion
		return true;
	}
	#endregion*/

	#endregion


}
