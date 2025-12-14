using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainTesting : MonoBehaviour
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

	// Values : SudokuValues
	public int[,] Values;

	// randomList : To Pick Random Number
	List<int> randomList;

	// randomValue
	int randomValue = 0;

	// randomValueExist
	bool randomValueExist = false;

	// totalIterations
	int totalIterations = 0;

	// FindSolution Boolean
	//private bool FindSolution;

	//stopExecution or Not
	//bool stopExecution = false;

	// ReInitiailzingvaluesList
	List<int> values;

	// ScriptsReferences Script
	public ScriptsReferences scriptsReferences;

	// Start is called before the first frame update
	void Awake()
	{
		// FindSolution
		//FindSolution = false;

		// GetGridCells
		GetGridCells();

		// ReInitializingSudokuObjectValues or previously GeneratedExistingRandomNumbers in SudokuObject Values Array ( SudokuObject.Values[] )
		Values = new int[9, 9];

		// SudokuSolver
		SudokuSolver();

		// Calling scriptsReferences.buttonReferencesGenerator.InsertingValues() Method
		// scriptsReferences.buttonReferencesGenerator.InsertingValues();

		// IteratingSudokuValues
		IteratingSudokuValues();
	}

	#region isValidGrid
	bool isValidGrid(int[,] Values)
	{
		// Check if it is a valid solution
		#region validate all the rows
		for (int row = 0; row < 9; row++)
		{
			List<int> rowsList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			for (int column = 0; column < 9; column++)
			{
				int currentValue = Values[row, column];
				if (currentValue != 0)
				{
					if (rowsList.Contains(currentValue))
					{
						rowsList.Remove(currentValue);
					}
					else
					{
						// if any repeated number is existed in existed rows in sudoku, then return false, i.e. solution isn't valid or (try other number for valid solution or input)
						return false;
					}
				}

			}
		}
		#endregion

		#region validate all the cols
		for (int column = 0; column < 9; column++)
		{
			List<int> colsList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			for (int row = 0; row < 9; row++)
			{
				int currentValue = Values[row, column];
				if (currentValue != 0)
				{
					if (colsList.Contains(currentValue))
					{
						colsList.Remove(currentValue);
					}
					else
					{
						// if any repeated number is existed in existed cols in sudoku, then return false, i.e. solution isn't valid or (try other number for valid solution or input)
						return false;
					}
				}

			}
		}
		#endregion

		#region validate all the grids
		for (int GridIndex = 0; GridIndex < Grids.Count; GridIndex++)
		{
			List<Tuple<int, int>> currentGridCells = Grids[GridIndex];
			List<int> gridsList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			for (int currentGridCellIndex = 0; currentGridCellIndex < currentGridCells.Count; currentGridCellIndex++)
			{
				// currentGridCellRowIndex
				int currentGridCellRowIndex = currentGridCells[currentGridCellIndex].Item1;
				// currentGridCellColIndex
				int currentGridCellColIndex = currentGridCells[currentGridCellIndex].Item2;
				// currentValue
				int currentValue = Values[currentGridCellRowIndex, currentGridCellColIndex];
				if (currentValue != 0)
				{
					if (gridsList.Contains(currentValue))
					{
						gridsList.Remove(currentValue);
					}
					else
					{
						// if any repeated number is existed in existed grids in sudoku, then return false, i.e. solution isn't valid or (try other number for valid solution or input)
						return false;
					}
				}

			}
		}
		#endregion

		// if no one repeated number is existed in existed rows, cols and grids in sudoku, then return true, i.e. solution is valid or (this time currentNumber or input makes valid solution at present time)
		return true;
	}
	#endregion

	#region GetListOfNumbers
	List<int> GetListOfNumbers(int row, int column)
	{
		List<int> ListOfNumbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

		// Storing Current CellIndexes(Position)
		// RowIndex
		int RowIndex = row;
		// ColIndex
		int ColIndex = column;

		//*** There are 3 Logics for check userInputValueExist or not : 1) For Row 2) For Col 3) For Grid **

		#region Remove Digits used by same row 
		// RowWiseChecking For Loop : for Horizontal Comparisons, IsNumberExistedInRow : if currentValueExist then, remove it from ListOfNumbers and go to next logic : 1) For Column or Vertically
		for (int col = 0; col < 9; col++)
		{
			int currentValue = Values[RowIndex, col];
			if (ListOfNumbers.Contains(currentValue))
			{
				ListOfNumbers.Remove(currentValue);
			}
		}
		#endregion

		#region Remove Digits used by same column 
		// ColumnWiseChecking For Loop : for Vertical Comparisons, IsNumberExistedInColumn : if currentValueExist then, remove it from ListOfNumbers and go to next logic : 2) For Grid or GridWise
		for (int rows = 0; rows < 9; rows++)
		{
			int currentValue = Values[rows, ColIndex];
			if (ListOfNumbers.Contains(currentValue))
			{
				ListOfNumbers.Remove(currentValue);
			}
		}
		#endregion

		#region Remove Digits used by same Grid 
		// GetGridIndex
		int gridIndex = GetGridIndex(RowIndex, ColIndex);
		// Getting List of Tuple i.e. indexes of Current Grid (GridCells)
		List<Tuple<int, int>> GridCells = Grids[gridIndex];

		// GridWiseChecking For Loop : for GridWise Comparisons, IsNumberExistedInGrid : if currentValueExist then, remove it from ListOfNumbers, 
		// Finally at last we get ListOfNumbers which only have those numbers that doesn't exist in same row, column or grid according to current cell position(it's row and column wise position)
		for (int GridCellIndex = 0; GridCellIndex < GridCells.Count; GridCellIndex++)
		{
			//Debug.Log($"[{gridCells[gridCellIndex].Item1}][{gridCells[gridCellIndex].Item2}]");
			int GridCellRowIndex = GridCells[GridCellIndex].Item1; // Represent RowIndex of GridCell
			int GridCellColIndex = GridCells[GridCellIndex].Item2; // Represent ColIndex of GridCell

			int currentValue = Values[GridCellRowIndex, GridCellColIndex];
			if (ListOfNumbers.Contains(currentValue))
			{
				ListOfNumbers.Remove(currentValue);
			}
		}
		#endregion

		return ListOfNumbers;

	}
	#endregion

	#region SudokuSolver
	void SudokuSolver()
	{
		totalIterations++;
		bool TryToSolve = true;
		// Find the empty spots and take a guess, fill it if possible with below implemented logic
		for (int row = 0; row < 9; row++)
		{
			for (int column = 0; column < 9; column++)
			{
				if (Values[row, column] == 0)
				{
					// ReInitialize TryToSolve
					TryToSolve = false;

					// GetListOfNumbers || Candidates : To try fiil empty spot with any of number from these numbers if possible, try one by one
					List<int> listOfNumbers = GetListOfNumbers(row, column);
					for (int numberIndex = 0; numberIndex < listOfNumbers.Count; numberIndex++)
					{
						#region additional randomValue Concept
						int index = UnityEngine.Random.Range(0, listOfNumbers.Count);
						int randomValue = listOfNumbers[index];
						listOfNumbers.RemoveAt(index);

						Values[row, column] = randomValue;
						#endregion

						//Values[row, column] = listOfNumbers[numberIndex];
						// recurse on the modified grid
						bool isSolved = isValidGrid(Values);
						if (isSolved)
						{
							TryToSolve = true;
							break;
							// return true;
						}
						else
						{
							// Undo the wrong guess and start anew
							Values[row, column] = 0;
						}
					}

					// exhausted all ListOfNumbers || Candidates
					// but none solves the problem
					// return false;
					if (!TryToSolve)
					{
						// Call Method : Generate Another Sudoku, filled with some random Numbers and call this method again "SudokuSolver()", To fill empty spots again
						Debug.Log("Existed Sudoku Solution Is InCorrect :( And Try With Another Sudoku Soution Again");
						// ReInitialize Values Grid (1)
						Values = new int[9, 9];
						// Retrun Previous GenerateDiagonalRandomNumbers() And SudokuSolver() Method (2)
						// Then Call these below Methods Again (3)

						// GenerateDiagonalRandomNumbers();
						SudokuSolver();
						return;
					}
					else // fiiled empty spot with any of number from these numbers (ListOfNumbers || Candidates), so continue or ready for next empty spot to fill
					{
						continue;
					}

				}

			}
		}

		// no empty spot : All empty spots filled
		if (TryToSolve)
		{
			Debug.Log($"Existed Sudoku Solution Is Correct :) And TotalIterations To Generate This Correct Solution : {totalIterations}");
		}
		else
		{
			Debug.Log("Existed Sudoku Solution Is InCorrect :( ");
		}

	}
	#endregion

	#region GenerateDiagonalRandomNumbers
	public void GenerateDiagonalRandomNumbers()
	{
		randomValue = 0;

		for (int GridIndex = 0; GridIndex < Grids.Count; GridIndex++)
		{
			if (GridIndex == 1 || GridIndex == 2 || GridIndex == 3 || GridIndex == 5 || GridIndex == 6 || GridIndex == 7)
			{
				continue;

			}
			// Getting List of Tuple i.e. indexes of Current Grid (GridCells)
			List<Tuple<int, int>> currentGrid = Grids[GridIndex];

			#region GridWiseChecking For Loop : For GridWise Comparisons (GridCellsWise || GridCellsWiseChecking)
			for (int gridCellIndex = 0; gridCellIndex < currentGrid.Count; gridCellIndex++)
			{
				//Debug.Log(gridCellIndex);

				//Debug.Log($"[{currentGrid[gridCellIndex].Item1}][{currentGrid[gridCellIndex].Item2}]");
				int gridCellRowIndex = currentGrid[gridCellIndex].Item1; // Represent RowIndex of GridCell
				int gridCellColIndex = currentGrid[gridCellIndex].Item2; // Represent ColIndex of GridCell

				// Storing Current CellIndexes(Position)
				// RowIndex
				int RowIndex = gridCellRowIndex;
				// ColIndex
				int ColIndex = gridCellColIndex;

				//Debug.Log($"[{RowIndex}][{ColIndex}]");
				// ReInitializingRandomList
				ReInitializingRandomList();
				// for loop : running 9 times only to get 9 different random numbers and with this also removing one random number each time
				for (int r = 0; r < 9; r++)
				{
					int index = UnityEngine.Random.Range(0, randomList.Count);
					randomValue = randomList[index];
					randomList.RemoveAt(index);

					randomValueExist = false;

					//*** There are 3 Logics for check userInputValueExist or not : 1) For Row 2) For Col 3) For Grid **

					#region IsNumberPossibleInRow : if randomValueNotExist then check next logic : 1) For Row or Horizontally
					if (!randomValueExist)
					{
						#region RowWiseChecking For Loop : for Horizontal Comparisons
						for (int col = 0; col < 9; col++)
						{
							if (Values[RowIndex, col] == randomValue)
							{
								randomValueExist = true;
								break;
							}

						}
						#endregion
					}
					#endregion

					#region IsNumberPossibleInColumn : if randomValueNotExist then check next logic : 2) For Column or Vertically
					if (!randomValueExist)
					{
						#region ColumnWiseChecking For Loop : for Vertical Comparisons 
						for (int row = 0; row < 9; row++)
						{
							if (Values[row, ColIndex] == randomValue)
							{
								randomValueExist = true;
								break;
							}
						}
						#endregion
					}
					#endregion

					#region IsNumberPossibleInGrid : if randomValueNotExist then check next logic : 3) For Grid or GridCellsWise || GridCellsWiseChecking
					if (!randomValueExist)
					{
						// GetGridIndex
						int gridIndex = GetGridIndex(RowIndex, ColIndex);
						// Getting List of Tuple i.e. indexes of Current Grid (GridCells)
						List<Tuple<int, int>> GridCells = Grids[gridIndex];

						#region GridWiseChecking For Loop : For GridWise Comparisons (GridCellsWise || GridCellsWiseChecking)
						for (int GridCellIndex = 0; GridCellIndex < GridCells.Count; GridCellIndex++)
						{
							//Debug.Log($"[{gridCells[gridCellIndex].Item1}][{gridCells[gridCellIndex].Item2}]");
							int GridCellRowIndex = GridCells[GridCellIndex].Item1; // Represent RowIndex of GridCell
							int GridCellColIndex = GridCells[GridCellIndex].Item2; // Represent ColIndex of GridCell

							if (Values[GridCellRowIndex, GridCellColIndex] == randomValue)
							{
								randomValueExist = true;
								break;
							}
						}
						#endregion
					}
					#endregion

					// Finally : if randomValueNotExist In all Comparisons : 1) For Row (In RowWise), 2) For Column (In ColWise) 3) For Grid (In GridWise) || GridWiseElements, then simply put that value
					if (!randomValueExist)
					{
						Values[RowIndex, ColIndex] = randomValue;
						break;
					}
				}

				#endregion

			}

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

	#region Iterator
	void Iterator()
	{
		for (int GridIndex = 0; GridIndex < 1; GridIndex++)
		{
			List<Tuple<int, int>> currentGrid = Grids[GridIndex];
			for (int gridCellIndex = 0; gridCellIndex < currentGrid.Count; gridCellIndex++)
			{
				Debug.Log($"[{currentGrid[gridCellIndex].Item1}][{currentGrid[gridCellIndex].Item2}]");
			}
		}
	}
	#endregion

	#region ReInitializingvaluesList
	void ReInitializingvaluesList()
	{
		values = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
	}
	#endregion

	#region GetGridIndex
	private int GetGridIndex(int row, int column)
	{
		if (row < 3)
		{
			if (column < 3) { return 0; }
			else if (column < 6) { return 1; }
			else { return 2; }
		}
		else if (row < 6)
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

	#region ReInitializingRandomList
	void ReInitializingRandomList()
	{
		// ReInitializingRandomList : RandomNumber Concept
		randomList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
	}
	#endregion

	#region IteratingGridCellsPositions
	void IteratingGridCellsPositions()
	{
		for (int gridIndex = 0; gridIndex < Grids.Count; gridIndex++)
		{
			// Getting List of Tuple i.e. indexes of Current Grid (GridCells)
			List<Tuple<int, int>> grid = Grids[gridIndex];

			#region GridWiseChecking For Loop : For GridWise Comparisons (GridCellsWise || GridCellsWiseChecking)
			for (int gridCellIndex = 0; gridCellIndex < grid.Count; gridCellIndex++)
			{
				Debug.Log($"[{grid[gridCellIndex].Item1}][{grid[gridCellIndex].Item2}]");

			}
			#endregion
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
				rowValue += $"{Values[i, j]}, ";
			}
			Debug.Log(rowValue + "\n");
		}
	}
	#endregion

}