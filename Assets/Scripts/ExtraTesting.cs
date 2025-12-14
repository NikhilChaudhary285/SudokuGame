using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ExtraTesting : MonoBehaviour
{
	#region Lists of Tuples For Grid To Store Each Grid Cells || Cells Positions
	List<Tuple<int, int>> Grid1 = new List<Tuple<int, int>>();
	List<Tuple<int, int>> Grid2 = new List<Tuple<int, int>>();
	List<Tuple<int, int>> Grid3 = new List<Tuple<int, int>>();
	List<Tuple<int, int>> Grid4 = new List<Tuple<int, int>>();
	List<Tuple<int, int>> Grid5 = new List<Tuple<int, int>>();
	List<Tuple<int, int>> Grid6 = new List<Tuple<int, int>>();
	List<Tuple<int, int>> Grid7 = new List<Tuple<int, int>>();
	List<Tuple<int, int>> Grid8 = new List<Tuple<int, int>>();
	List<Tuple<int, int>> Grid9 = new List<Tuple<int, int>>();
	#endregion

	#region List Of Lists of Grids
	List<List<Tuple<int, int>>> Grids = new List<List<Tuple<int, int>>>();
	#endregion

	// SudokuObject (SudokuObject Values)
	public int[,] Values;
	
	// SudokuObject (SudokuObject Values)
	public int[,] finalSudokuObjectValues = new int[9,9];

	// LimitOfExistingValuesInGrid (In SudokuGame)
	public static int LimitOfExistingValuesInGrid = 61;

	// totalValuesInGrid
	int currentValuesInGrid = 0;

	// totalIterations
	int totalIterations = 0;

	// gridsCells : Storing gridsCells (gridsCells Positions to remove Numbers from these positions randomly)
	List<Tuple<int, int>> gridsCellsList;

	// SudokuSolverButton
	public Button SudokuSolverButton;

	// ScriptsReferences Script
	public ScriptsReferences scriptsReferences;

	// Start is called before the first frame update
	void Awake()
	{
		// GetGridCells
		GetGridCells();

		// AddListeners
		//AddListeners();

		//Initialize Values
		Values = new int[9, 9];

		// totalIteraions
		totalIterations = 0;

		// currentValuesInGrid
		currentValuesInGrid = 81;

		// SudokuSolver : Main Method : To Generate Sudoku or Solve Sudoku Also
		SudokuSolver();

		// finalSudokuObjectValues
		StoringSudokuSolverFinalObjectValues(Values);

		// RemoveRandomNumbers
		RemoveRandomNumbers(LimitOfExistingValuesInGrid);

		// IteratingSudokuValues
		IteratingSudokuValues();

	}

	#region IsValidGrid
	bool IsValidGrid(int[,] Values)
	{
		// Validate All Rows, Columns and Grids
		#region Validate all the rows                                 
		for (int row = 0; row < 9; row++)
		{
			// rowsList
			List<int> rowsList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			for (int col = 0; col < 9; col++)
			{
				int currentValue = Values[row, col];
				if(currentValue != 0)
				{
					if(rowsList.Contains(currentValue))
					{
						rowsList.Remove(currentValue);
					}
					else
					{
						// return false, if any repeated number exist in any row
						return false;
					}
				}
			}
		}
		#endregion

		#region Validate all the cols                            
		for (int col = 0; col < 9; col++)
		{
			// colsList
			List<int> colsList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			for (int row = 0; row < 9; row++)
			{
				int currentValue = Values[row, col];
				if (currentValue != 0)
				{
					if (colsList.Contains(currentValue))
					{
						colsList.Remove(currentValue);
					}
					else
					{
						// return false, if any repeated number exist in any col
						return false;
					}
				}
			}
		}

		#endregion

		#region Validate all the grids
		for (int GridIndex = 0; GridIndex < Grids.Count; GridIndex++) 
		{ 
			// currentGrid
			List<Tuple<int, int>> gridCells = Grids[GridIndex];
			// gridsList
			List<int> gridsList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			for (int gridCellIndex = 0; gridCellIndex < gridCells.Count; gridCellIndex++)
			{
				// gridCell
				Tuple<int, int> gridCell = gridCells[gridCellIndex];
				// gridCellPosition (in gridCellRowIndex, gridCellColIndex)
				int gridCellRowIndex = gridCell.Item1;
				int gridCellColIndex = gridCell.Item2;

				int currentValue = Values[gridCellRowIndex, gridCellColIndex];
				if (currentValue != 0)
				{
					if (gridsList.Contains(currentValue))
					{
						gridsList.Remove(currentValue);
					}
					else
					{
						// return false, if any repeated number exist in any grid
						return false;
					}
				}

			}

		}
		#endregion

		// return true, if no one repeated number exist in any rows, cols or grids
		return true;

	}
	#endregion

	#region GetListOfNumbers
	List<int> GetListOfNumbers(int row, int column)
	{
		List<int> ListOfNumbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

		#region RowWiseChecking For Loop : for Horizontal Comparisons : Removing existed numbers from ListOfNumbers which are existed in same row
		for (int i = 0; i < 9; i++)
		{
			int currentValue = Values[row, i];
			if(currentValue != 0)
			{
				if(ListOfNumbers.Contains(currentValue))
				{
					ListOfNumbers.Remove(currentValue);
				}
			}

		}
		#endregion

		#region ColWiseChecking For Loop : for Vertical Comparisons : Removing existed numbers from ListOfNumbers which are existed in same column
		for (int i = 0; i < 9; i++)
		{
			int currentValue = Values[i, column];
			if (currentValue != 0)
			{
				if (ListOfNumbers.Contains(currentValue))
				{
					ListOfNumbers.Remove(currentValue);
				}
			}

		}
		#endregion

		#region For GridWise Comparisons : Removing existed numbers from ListOfNumbers which are existed in same grid
		// gridIndex
		int gridIndex = GetGridIndex(row, column);
		// gridCells
		List<Tuple<int, int>> gridCells = Grids[gridIndex];

		#region GridWiseChecking For Loop : for GridWise Comparisons : Removing existed numbers from ListOfNumbers which are existed in same grid
		for (int gridCellIndex = 0; gridCellIndex < gridCells.Count; gridCellIndex++)
		{
			// gridCell
			Tuple<int, int> gridCell = gridCells[gridCellIndex];
			// gridCellPosition (in gridCellRowIndex, gridCellColIndex)
			int gridCellRowIndex = gridCell.Item1;
			int gridCellColIndex = gridCell.Item2;

			// currentValue
			int currentValue = Values[gridCellRowIndex, gridCellColIndex];
			if (currentValue != 0)
			{
				if (ListOfNumbers.Contains(currentValue))
				{
					ListOfNumbers.Remove(currentValue);
				}
			}
		}
		#endregion

		#endregion

		return ListOfNumbers;
	}
	#endregion

	#region GetGridIndex
	int GetGridIndex(int row, int column)
	{
		if(row < 3)
		{
			if(column < 3) { return 0; }
			else if(column < 6) { return 1; }
			else { return 2; }
		}
		else if(row < 6) 
		{
			if (column < 3) { return 3; }
			else if (column < 6) { return 4; }
			else { return 5; }
		}
		else
		{
			if (column < 3) { return 6; }
			else if (column < 6) { return 7; }
			else { return 8; }
		}
	}
	#endregion

	#region SudokuSolver
	void SudokuSolver()
	{
		totalIterations++;

		// TryToSolve : To Control Flow of Recursions
		bool TryToSolve = false;

		for (int row = 0; row < 9; row++)
		{
			for (int column = 0; column < 9; column++)
			{
				if(Values[row, column] == 0)
				{
					// ReInitialize TryToSolve : To Call SudokuSolver Method Again Recursively, if no one random valid number is findout for currentCell
					TryToSolve = true;

					List<int> listOfNumbers = GetListOfNumbers(row, column);

                    while (listOfNumbers.Count != 0)
                    {
						#region randomValue Concept
						int index = UnityEngine.Random.Range(0, listOfNumbers.Count);
						int randomValue = listOfNumbers[index];
						listOfNumbers.RemoveAt(index);
						#endregion

						Values[row, column] = randomValue;
						// Validate Grid : Check isValidGrid after putting this number
						bool isSolved = IsValidGrid(Values);
						if(isSolved)
						{
							TryToSolve = false;
							break;
						}
						else
						{
							Values[row, column] = 0;
						}
					}

					// if any valid number was putted inside current cell and also the grid isValid with this, then ready for filling next cell
					if (!TryToSolve)
					{
						continue;
					}
					else // if no one valid number is found to put inside current cell, then start creating another solution
					{
						Debug.Log("This solution is not valid to fill current Cell with existed listOfNumbers :( So Try With Another New Solution");
						// 1) ReInitialize Values Array
						Values = new int[9, 9];
						// 2) Call SudokuSolver() Method Again Recursively
						SudokuSolver();
						// 3) return this method : i.e. return all "SudokuSolver()" Methods which is in stack, to not continue all recursion methods again
						return;

					}
				}
			}
		}

		// Finally : when no empty spots to fill
		if(!TryToSolve) 
		{
			Debug.Log($"This solution is valid with correctly filled random numbers in each cell :) And TotalIterations To Generate This Valid Solution : {totalIterations}");
		}

	}
	#endregion

	#region gridsCells : Storing gridsCells (gridsCells Positions to remove Numbers from these positions randomly)
	void StoreGridCellsPositions()
	{
		gridsCellsList = new List<Tuple<int, int>>();
		for (int row = 0; row < 9; row++)
		{
			for (int column = 0; column < 9; column++)
			{
				// Storing Each GridsCells Position
				gridsCellsList.Add(new Tuple<int, int>(row, column));
			}
		}
	}
	#endregion

	#region RemoveRandomNumbers
	void RemoveRandomNumbers(int LimitOfExistingValuesInGrid)
	{
		// StoreGridCellsPositions
		StoreGridCellsPositions(); // StoreGridCellsPositions List Name : gridsCellsList
		while (currentValuesInGrid > LimitOfExistingValuesInGrid)
		{
			// randomPosition Concept
			int index = UnityEngine.Random.Range(0, gridsCellsList.Count);
			int randomGridCellRowPosition = gridsCellsList[index].Item1;
			int randomGridCellColPosition = gridsCellsList[index].Item2;
			gridsCellsList.RemoveAt(index);

			if (Values[randomGridCellRowPosition, randomGridCellColPosition] != 0)
			{
				Values[randomGridCellRowPosition, randomGridCellColPosition] = 0;
			}
			currentValuesInGrid--;
		}
	}
	#endregion

	#region GetGridCells
	void GetGridCells()
	{
		#region For Loop : For Grid1, Grid2, Grid3
		for (int row = 0; row < 3; row++)
		{
			for (int col = 0; col < 3; col++) // Grid1
			{
				Grid1.Add(new Tuple<int, int>(row, col));
			}
			for (int col = 3; col < 6; col++) // Grid2
			{
				Grid2.Add(new Tuple<int, int>(row, col));
			}
			for (int col = 6; col < 9; col++) // Grid3
			{
				Grid3.Add(new Tuple<int, int>(row, col));
			}
		}
		#endregion

		// Adding Grids To List Of Grids (One by One)
		Grids.Add(Grid1);
		Grids.Add(Grid2);
		Grids.Add(Grid3);

		#region For Loop : For Grid4, Grid5, Grid6
		for (int row = 3; row < 6; row++)
		{
			for (int col = 0; col < 3; col++) // Grid1
			{
				Grid4.Add(new Tuple<int, int>(row, col));
			}
			for (int col = 3; col < 6; col++) // Grid2
			{
				Grid5.Add(new Tuple<int, int>(row, col));
			}
			for (int col = 6; col < 9; col++) // Grid3
			{
				Grid6.Add(new Tuple<int, int>(row, col));
			}
		}
		#endregion

		// Adding Grids To List Of Grids (One by One)
		Grids.Add(Grid4);
		Grids.Add(Grid5);
		Grids.Add(Grid6);

		#region For Loop : For Grid7, Grid8, Grid9
		for (int row = 6; row < 9; row++)
		{
			for (int col = 0; col < 3; col++) // Grid1
			{
				Grid7.Add(new Tuple<int, int>(row, col));
			}
			for (int col = 3; col < 6; col++) // Grid2
			{
				Grid8.Add(new Tuple<int, int>(row, col));
			}
			for (int col = 6; col < 9; col++) // Grid3
			{
				Grid9.Add(new Tuple<int, int>(row, col));
			}
		}
		#endregion

		// Adding Grids To List Of Grids (One by One)
		Grids.Add(Grid7);
		Grids.Add(Grid8);
		Grids.Add(Grid9);

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
				rowValue += $"{Values[i, j]}, ";
			}
			Debug.Log(rowValue + "\n");               
		}
	}
	#endregion

	#region SudokuSolver Concept

	#region StoringSudokuSolverFinalObjectValues
	void StoringSudokuSolverFinalObjectValues(int[,] Values)
	{
		for (int row = 0; row < 9; row++)
		{
			for (int column = 0; column < 9; column++)
			{
				finalSudokuObjectValues[row, column] = Values[row, column];
			}
		}
	}
	#endregion

	// Start is called before the first frame update
	void Start()
	{
		AddListeners();

	}
	private void AddListeners()
	{
		SudokuSolverButton.onClick.AddListener(ClickOn_SudokuSolverButton);
	}

	void ClickOn_SudokuSolverButton()
	{
		// InsertingFinalSudokuObjectValues
		scriptsReferences.buttonReferencesGenerator.InsertingFinalSudokuObjectValues(finalSudokuObjectValues);
		// ReInitializeAllGridButtonsGameObjectImageColorToDefault
		scriptsReferences.gridButtonSelector.ReInitializeAllGridButtonsGameObjectImageColorToDefault();
		// Implementing (userInputGridButton = null) because to solve issue like deselecting last element or cell of grid by user, To Not filling this cell again with other value because user click (solver or finish button)
		scriptsReferences.sudokoValidator.userInputGridButton = null;

	}
	#endregion

}