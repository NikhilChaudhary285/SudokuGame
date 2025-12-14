using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SudokuObject : MonoBehaviour
{
    public static int[,] Values = new int[9,9];

	// userInputValueExist
	private bool userInputValueExist = false;

	// RandomValuesList
	private List<int> randomList;

	#region extra
	// Default Values For ExistingValuesInRow, ExistingValuesInColumn, ExistingValuesInGrid
	int ExistingValuesInRow = 0;
	int ExistingValuesInColumn = 0;
	int ExistingValuesInGrid = 0;

	// GameSettings For CheckingExistingValuesInRow, CheckingExistingValuesInColumn, CheckingExistingValuesInGrid
	public static bool RemovingorCheckingExistingValuesInRow = false;
	public static bool RemovingorCheckingExistingValuesInColumn = false;
	public static bool RemovingorCheckingExistingValuesInGrid = false;

	// GameSettings For CheckingExistingValuesInRow, CheckingExistingValuesInColumn, CheckingExistingValuesInGrid
	public bool CheckingExistingValuesInRow = RemovingorCheckingExistingValuesInRow;
	public bool CheckingExistingValuesInColumn = RemovingorCheckingExistingValuesInColumn;
	public bool CheckingExistingValuesInGrid = RemovingorCheckingExistingValuesInGrid;

	// GameSettings For RemovingExistingValuesInRow, RemovingExistingValuesInColumn, RemovingExistingValuesInGrid
	public bool RemovingExistingValuesInRow = RemovingorCheckingExistingValuesInRow;
	public bool RemovingExistingValuesInColumn = RemovingorCheckingExistingValuesInColumn;
	public bool RemovingExistingValuesInGrid = RemovingorCheckingExistingValuesInGrid;

	public static int LimitOfExistingValuesInRoworColumnorGrid = 3;
	#endregion

	public static int LimitOfExistingValuesInGrid = 61;

	#region GetGridIndex
	public static int GetGridIndex(int row, int column)
    {
        if (row < 3)
        {
            if (column < 3) { return 1; }
            if (column < 6) { return 2; }
            else { return 3; }
        }
        else if(row < 6)
        {
			if (column < 3) { return 4; }
			if (column < 6) { return 5; }
			else { return 6; }
		}
		else
		{
			if (column < 3) { return 7; }
			if (column < 6) { return 8; }
			else { return 9; }
		}
	}
	#endregion

	#region GetGridGroup
	public static void GetGridGroup(int grid, out int startRow, out int startColumn)
	{
		startRow = 0; 
		startColumn = 0;
		switch (grid)
		{
			// To Represent All Grids Group by startRow & startColumn
			// For 1st Row Grids
			case 1:
				startRow = 0;
				startColumn = 0;
				break;
			case 2:
				startRow = 0;
				startColumn = 3;
				break;
			case 3:
				startRow = 0;
				startColumn = 6;
				break;
			// For 2nd Row Grids
			case 4:
				startRow = 3;
				startColumn = 0;
				break;
			case 5:
				startRow = 3;
				startColumn = 3;
				break;
			case 6:
				startRow = 3;
				startColumn = 6;
				break;
			// For 3rd Row Grids
			case 7:
				startRow = 6;
				startColumn = 0;
				break;

			case 8:
				startRow = 6;
				startColumn = 3;
				break;
			case 9:
				startRow = 6;
				startColumn = 6;
				break;

			default:
				break;
		}
	}
	#endregion

	#region GenerateRandomNumbers
	public void GenerateRandomNumbers()
	{
		// ReInitializingSudokuObjectValues or previously GeneratedExistingRandomNumbers in SudokuObject Values Array ( SudokuObject.Values[] )
		SudokuObject.Values = new int[9, 9];

		int randomValue = 0;
		for (int row = 0; row < 9; row++)
		{
			for (int column = 0; column < 9; column++)
			{
				randomList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
				// for loop : running 9 times only to get 9 different random numbers and with this also removing one random number each time
				for (int r = 1; r < 10; r++)
				{
					int index = Random.Range(0, randomList.Count);
					int value = randomList[index];
					randomList.RemoveAt(index);

					userInputValueExist = false;

					/*** There are 3 Logics for check userInputValueExist or not : 1) For Row 2) For Col 3) For Grid ***/

					#region IsNumberPossibleInRow : if userInputValueNotExist then check next logic : 1) For Row or Horizontally
					if (!userInputValueExist)
					{
						#region RowWiseChecking For Loop : for Horizontal Comparisons
						for (int i = 0; i < 9; i++)
						{
							if (SudokuObject.Values[row, i] == value)
							{
								userInputValueExist = true;
								break;
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
							if (SudokuObject.Values[i, column] == value)
							{
								userInputValueExist = true;
								break;
							}
						}
						#endregion
					}
					#endregion

					#region IsNumberPossibleInGrid : if userInputValueNotExist then check next logic : 3) For Grid or GridButtonsWise || GridButtonsWiseChecking
					if (!userInputValueExist)
					{
						// Getting GridIndex
						int GridIndex = SudokuObject.GetGridIndex(row, column);
						// Getting Specific GridGroup By GridIndex, StartRow & StartColumn
						GetGridGroup(GridIndex, out int startRow, out int startColumn);

						#region GridWiseChecking For Loop : for GridWise Comparisons || GridWiseElements
						for (int Row = startRow; Row < startRow + 3; Row++)
						{
							for (int Column = startColumn; Column < startColumn + 3; Column++)
							{
								if (SudokuObject.Values[Row, Column] == value)
								{
									userInputValueExist = true;
									break;
								}
							}
						}
						#endregion
					}
					#endregion

					// Finally : if userInputValueNotExist In all Comparisons : 1) For Row (In RowWise), 2) For Column (In ColWise) 3) For Grid (In GridWise) ||  GridWiseElements
					if (!userInputValueExist)
					{
						//randomValue = value;
						SudokuObject.Values[row, column] = value;
						break;
					}
					else
					{
						 // Removing previously selected random number which isn't capable to insert at specific position and after that we can select from randomList in which this index's random value doesn't exist

					}

					// Calling VHGWiseCheckingExistingValues : For To Implement How Many Limited Values we want in Sudoku
					/*VHGWiseCheckingExistingValues(row, column, CheckingExistingValuesInRow, CheckingExistingValuesInColumn, CheckingExistingValuesInGrid);
					if (ExistingValuesInRow < SudokuObject.LimitOfExistingValuesInRoworColumnorGrid && ExistingValuesInColumn < SudokuObject.LimitOfExistingValuesInRoworColumnorGrid && ExistingValuesInGrid < SudokuObject.LimitOfExistingValuesInRoworColumnorGrid)
					{
						// Calling VHGWiseRemovingExistingValues : For To Implement How Removing only one value if ExistingValuesInRow Crossing LimitOfExistingValuesInRoworColumnorGrid
						VHGWiseRemovingExistingValues(row, column, RemovingExistingValuesInRow, RemovingExistingValuesInColumn, RemovingExistingValuesInGrid);
					}*/

				}
			}
		}
	}
	#endregion

	#region VerticallyHorizontallyGridWiseCheckingExistingValues
	public void VHGWiseCheckingExistingValues(int row, int column, bool CheckingExistingValuesInRow, bool CheckingExistingValuesInColumn, bool CheckingExistingValuesInGrid)
	{
		// ReInitializing Values
		ExistingValuesInRow = 0;
		ExistingValuesInColumn = 0;
		ExistingValuesInGrid = 0;

		/*** There are 3 Logics for check ExistingValuesInRowColumnGrid : 1) For Row 2) For Col 3) For Grid ***/

		#region IsNumberPossibleInRow : if userInputValueNotExist then check next logic : 1) For Row or Horizontally
		if (CheckingExistingValuesInRow)
		{
			#region RowWiseChecking For Loop : for Horizontal Comparisons
			for (int i = 0; i < 9; i++)
			{
				if (SudokuObject.Values[row, i] != 0)
				{
					ExistingValuesInRow++;
				}

			}
			#endregion
		}
		#endregion

		#region IsNumberPossibleInColumn : if userInputValueNotExist then check next logic : 2) For Column or Vertically
		if (CheckingExistingValuesInColumn)
		{
			#region ColumnWiseChecking For Loop : for Vertical Comparisons 
			for (int i = 0; i < 9; i++)
			{
				if (SudokuObject.Values[i, column] != 0)
				{
					ExistingValuesInColumn++;
				}
			}
			#endregion
		}
		#endregion

		#region IsNumberPossibleInGrid : if userInputValueNotExist then check next logic : 3) For Grid or GridButtonsWise || GridButtonsWiseChecking
		if (CheckingExistingValuesInGrid)
		{
			// Getting GridIndex
			int GridIndex = SudokuObject.GetGridIndex(row, column);
			// Getting Specific GridGroup By GridIndex, StartRow & StartColumn
			GetGridGroup(GridIndex, out int startRow, out int startColumn);
	
			#region GridWiseChecking For Loop : for Checking ExistingValuesInGrid
			for (int Row = startRow; Row < startRow + 3; Row++)
			{
				for (int Column = startColumn; Column < startColumn + 3; Column++)
				{
					if (SudokuObject.Values[Row, Column] != 0)
					{
						ExistingValuesInGrid++;
					}
				}
			}
			#endregion
		}
		#endregion
	}
	#endregion

	#region VerticallyHorizontallyGridWiseRemovingExistingValues
	public void VHGWiseRemovingExistingValues(int row, int column, bool RemovingExistingValuesInRow, bool RemovingExistingValuesInColumn, bool RemovingExistingValuesInGrid)
	{
		/*** There are 3 Logics for check VHGWiseRemovingExistingValues : 1) For Row 2) For Col 3) For Grid ***/

		#region RemovingExistingValuesInRow : Removing One ExistingValueInRow : 1) For Row or Horizontally
		if (RemovingExistingValuesInRow)
		{
			bool valueRemoved = false;
			while(!valueRemoved)
			{
				#region RowWiseRemoving For Loop : for Horizontal Comparisons
				List<int> randomList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
				for (int i = 0; i < randomList.Count; i++)
				{
					if (SudokuObject.Values[row, randomList[i]] != 0 && 0 != Random.Range(0, 2))
					{
						valueRemoved = true;
						SudokuObject.Values[row, i] = 0;
						break;
					}
					else
					{
						if(SudokuObject.Values[row, randomList[i]] == 0)
						randomList.RemoveAt(i);
					}
					
				}
				#endregion
			}
		}
		#endregion

		#region RemovingExistingValuesInColumn : Removing One ExistingValueIncolumn : 2) For Column or Vertically
		if (RemovingExistingValuesInColumn)
		{
			bool valueRemoved = false;
			while (!valueRemoved)
			{
				#region ColumnWiseRemoving For Loop : for Horizontal Comparisons
				List<int> randomList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
				for (int i = 0; i < randomList.Count; i++)
				{
					if (SudokuObject.Values[randomList[i], column] != 0 && 0 != Random.Range(0, 2))
					{
						valueRemoved = true;
						SudokuObject.Values[randomList[i], column] = 0;
						break;
					}
					else
					{
						if (SudokuObject.Values[randomList[i], column] == 0)
							randomList.RemoveAt(i);
					}

				}
				#endregion
			}
		}
		#endregion

		#region RemovingExistingValuesInGrid : Removing One ExistingValueInGrid : 3) For Grid or GridWise || GridWiseRemoving
		if (RemovingExistingValuesInGrid)
		{
			// Getting GridIndex
			int GridIndex = SudokuObject.GetGridIndex(row, column);
			// Getting Specific GridGroup By GridIndex, StartRow & StartColumn
			GetGridGroup(GridIndex, out int startRow, out int startColumn);

			bool valueRemoved = false;
			while (!valueRemoved)
			{
				#region GridWiseRemoving For Loop : for GridWise Removing
				List<int> randomRowList = new List<int>() { startRow + 0, startRow + 1, startRow + 2, };
				List<int> randomColumnList = new List<int>() { startColumn + 0, startColumn + 1, startColumn + 2};
				#region GridWiseRemoving For Loop : for Removing ExistingValuesInGrid
				for (int i = startRow; i < startRow + 3; i++)
				{
					for (int j = startColumn; j < startColumn + 3; j++)
					{
						if (SudokuObject.Values[randomRowList[i], randomColumnList[j]] != 0 && 0 != Random.Range(0, 2))
						{
							valueRemoved = true;
							SudokuObject.Values[randomRowList[i], randomColumnList[j]] = 0;
							break;
						}
						else
						{
							if (SudokuObject.Values[randomRowList[i], randomColumnList[j]] == 0)
							{
								randomRowList.RemoveAt(i);
								randomColumnList.RemoveAt(j);
							}

						}
					}
					if(valueRemoved)
					{ break; }
				}
				#endregion
			}
			#endregion
		}
		#endregion

	}
	#endregion

	public void GivingListOfRandomValues()
	{
		//List<int> Group
	}


}
