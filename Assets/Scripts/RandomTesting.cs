//
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RandomTesting : MonoBehaviour
{
	// ScriptReferences Script
	public ScriptsReferences scriptsReferences;

	// Lists of Tuples For Grid To Store Each Grid Cells || Cells Positions
	List<Tuple<int, int>> Grid1 = new List<Tuple<int, int>>();
	List<Tuple<int, int>> Grid2 = new List<Tuple<int, int>>();
	List<Tuple<int, int>> Grid3 = new List<Tuple<int, int>>();
	List<Tuple<int, int>> Grid4 = new List<Tuple<int, int>>();
	List<Tuple<int, int>> Grid5 = new List<Tuple<int, int>>();
	List<Tuple<int, int>> Grid6 = new List<Tuple<int, int>>();
	List<Tuple<int, int>> Grid7 = new List<Tuple<int, int>>();
	List<Tuple<int, int>> Grid8 = new List<Tuple<int, int>>();
	List<Tuple<int, int>> Grid9 = new List<Tuple<int, int>>();

	// List Of Lists of Grids
	List<List<Tuple<int, int>>> Grids = new List<List<Tuple<int, int>>>();

	// Values : SudokuValues
	public int[,] Values;

	// FindSolution Boolean
	private bool FindSolution;
	// randomList : To Pick Random Number
	List<int> randomList;

	// randomValueExist
	bool randomValueExist = false;
	
	// swapAValueExist
	bool swapAValueExist = false;
	
	// swapBValueExist
	bool swapBValueExist = false;

	// sudokuFieldIndex
	int sudokuFieldIndex = -1;

	//stopExecution or Not
	bool stopExecution = false;

	// EmptyFields
	List<Tuple<int, int>> EmptyFields = new List<Tuple<int, int>>();

	// Start is called before the first frame update
	void Awake()
    {
        // FindSolution
        FindSolution = false;	

		// GetGridCells
		GetGridCells();

		// IteratingValues
		//IteratingValues();

		// GenerateRandomNumbers : Calling GenerateRandomNumbers Method to generate random numbers
		GenerateDiagonalRandomNumbers();
		GenerateFilledRandomNumbers();
		//GenerateOtherRandomNumbers();
		//Iterator();
	}

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

	#region Loop Through ALL Grid Cells
	void AddingAllGridsCells()
	{
		for (int i = 0; i < Grids.Count; i++)
		{
			List<Tuple<int, int>> grid = Grids[i];

			Debug.Log(grid[0].Item1);
		}
	}
	#endregion

	// Storing Current CellIndexes (Position)
	// RowIndex
	int RowIndex;
	// ColIndex
	int ColIndex;
	int randomValue = 0;
	int forloopValue = 0;
	

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

	#region GenerateOtherRandomNumbers
	public void GenerateOtherRandomNumbers()
	{
		randomValue = 0;

		// For Loop SequenceCell Iterator : To Iterate All Postions To Fill It With Correct Number
		for (int mainRow = 0; mainRow < 9; mainRow++)
		{
			for (int mainCol = 0; mainCol < 9; mainCol++)
			{
				// Storing Current CellIndexes (Position)
				// RowIndex
				RowIndex = mainRow;
				// ColIndex
				ColIndex = mainCol;

				// GetGridIndex
				int GridIndex = GetGridIndex(RowIndex, ColIndex);
				if (GridIndex == 0 || GridIndex == 4 || GridIndex == 8)
				{
					continue;
				}

				// ReInitializingRandomList
				ReInitializingRandomList();
				// for loop : running 9 times only to get 9 different random numbers and with this also removing one random number each time
				for (int r = 0; r < 9; r++)
				{
					int index = UnityEngine.Random.Range(0, randomList.Count);
					randomValue = randomList[index];
					randomList.RemoveAt(index);

					randomValueExist = false;

					//*** There are 3 Logics for check userInputValueExist or not : 1) For Row 2) For Col 3) For Grid ***//

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
						List<Tuple<int, int>> gridCells = Grids[gridIndex];

						#region GridWiseChecking For Loop : For GridWise Comparisons (GridCellsWise || GridCellsWiseChecking)
						for (int gridCellIndex = 0; gridCellIndex < gridCells.Count; gridCellIndex++)
						{
							//Debug.Log($"[{gridCells[gridCellIndex].Item1}][{gridCells[gridCellIndex].Item2}]");
							int gridCellRowIndex = gridCells[gridCellIndex].Item1; // Represent RowIndex of GridCell
							int gridCellColIndex = gridCells[gridCellIndex].Item2; // Represent ColIndex of GridCell

							if (Values[gridCellRowIndex, gridCellColIndex] == randomValue)
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
					/*else
					{
						// Removing previously selected random number which isn't capable to insert at specific position and after that we can select from randomList in which this index's random value doesn't exist
						//totaliterantion++;
						SwapPositionsMachine(new Tuple<int, int>(RowIndex, ColIndex), randomValue);
					}*/

				}
				/*// if randomValueExist, then call SwapPositionsMachine() Method
				if (randomValueExist)
				{
					//Debug.Log($"[{RowIndex}][{ColIndex}]");
					forloopValue = 0;
					stopExecution = false;
					for (int i = 1; i < 10; i++)
					{
						if (!stopExecution)
						{
							//Debug.Log("ok");
							//sudokuFieldIndex = -1;
							forloopValue = i;
							//Debug.Log(forloopValue);
							SwapPositionsMachine(new Tuple<int, int>(RowIndex, ColIndex), forloopValue);
						}
						else
						{
							sudokuFieldIndex = -1;
							break;
						}
					}
				}
				sudokuFieldIndex = -1;*/
			}

		}

		//Debug.Log("totaliterations : " + totaliterantion);



	}
	#endregion

	#region GenerateDiagonalRandomNumbers
	public void GenerateDiagonalRandomNumbers()
	{
		//int totaliterantion = 0;

		// ReInitializingSudokuObjectValues or previously GeneratedExistingRandomNumbers in SudokuObject Values Array ( SudokuObject.Values[] )
		Values = new int[9, 9];

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
				//Debug.Log("totaliterations : " + totaliterantion);
		}


	}
	#endregion

	#region GenerateFilledRandomNumbers
	public void GenerateFilledRandomNumbers()
	{
		//int totaliterantion = 0;

		randomValue = 0;

		for (int GridIndex = 0; GridIndex < Grids.Count; GridIndex++)
		{
			if (GridIndex == 0 || GridIndex == 4 || GridIndex == 8)
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
					else
					{
						EmptyFields.Add(new Tuple<int, int>(RowIndex, ColIndex));
					}
				}
				#endregion

			}
			//Debug.Log("totaliterations : " + totaliterantion);
		}


	}
	#endregion

	void SwapWithGridCells()
	{
		for (int EmptyFieldIndex = 0; EmptyFieldIndex < EmptyFields.Count; EmptyFieldIndex++)
		{
			Tuple<int, int> currentCell = EmptyFields[EmptyFieldIndex];
			int RowIndex = currentCell.Item1;
			int ColIndex = currentCell.Item2;

			// GetGridIndex
			int gridIndex = GetGridIndex(RowIndex, ColIndex);
			// Getting List of Tuple i.e. indexes of Current Grid (GridCells)
			List<Tuple<int, int>> gridCells = Grids[gridIndex];


			List<int> values = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			#region GridWiseChecking For Loop : For GridWise Comparisons (GridCellsWise || GridCellsWiseChecking)
			for (int gridCellIndex = 0; gridCellIndex < gridCells.Count; gridCellIndex++)
			{
				//Debug.Log($"[{gridCells[gridCellIndex].Item1}][{gridCells[gridCellIndex].Item2}]");
				int gridCellRowIndex = gridCells[gridCellIndex].Item1; // Represent RowIndex of GridCell
				int gridCellColIndex = gridCells[gridCellIndex].Item2; // Represent ColIndex of GridCell

				values.Remove(Values[gridCellRowIndex, gridCellColIndex]);
				
			}
			#endregion
			// notPresentValue
			for (int valueIndex = 0; valueIndex < values.Count; valueIndex++)
			{
				int notPresentValue = values[valueIndex];

				SwapPositionsMachA(new Tuple<int, int>(RowIndex, ColIndex),notPresentValue);

			}

		}
	}


	#region SwapPositionsMachine
	void SwapPositionsMachA(Tuple<int, int> _swapA, int _notPresentValue)
	{
		// Storing _swapA field
		Tuple<int, int> swapA = _swapA;
		// Storing _swapB field
		Tuple<int, int> swapB;

		int RowIndex = swapA.Item1;
		int ColIndex = swapA.Item2;

		// GetGridIndex
		int gridIndex = GetGridIndex(RowIndex, ColIndex);
		// Getting List of Tuple i.e. indexes of Current Grid (GridCells)
		List<Tuple<int, int>> gridCells = Grids[gridIndex];

		// Storing LastRandomValue
		int notPresentValue = _notPresentValue;

		#region GridWiseChecking For Loop : For GridWise Comparisons (GridCellsWise || GridCellsWiseChecking)
		for (int gridCellIndex = 0; gridCellIndex < gridCells.Count; gridCellIndex++)
		{
			//Debug.Log($"[{gridCells[gridCellIndex].Item1}][{gridCells[gridCellIndex].Item2}]");
			int gridCellRowIndex = gridCells[gridCellIndex].Item1; // Represent RowIndex of GridCell
			int gridCellColIndex = gridCells[gridCellIndex].Item2; // Represent ColIndex of GridCell

			#region Comparisons Logic

			swapAValueExist = false;

			//*** There are 3 Logics for check userInputValueExist or not : 1) For Row 2) For Col 3) For Grid ***//

			#region IsNumberPossibleInRow : if randomValueNotExist then check next logic : 1) For Row or Horizontally
			if (!swapAValueExist)
			{
				#region RowWiseChecking For Loop : for Horizontal Comparisons
				for (int col = 0; col < 9; col++)
				{
					if (Values[gridCellRowIndex, col] == notPresentValue)
					{
						swapAValueExist = true;
						break;
					}

				}
				#endregion
			}
			#endregion

			#region IsNumberPossibleInColumn : if randomValueNotExist then check next logic : 2) For Column or Vertically
			if (!swapAValueExist)
			{
				#region ColumnWiseChecking For Loop : for Vertical Comparisons 
				for (int row = 0; row < 9; row++)
				{
					if (Values[row, gridCellColIndex] == notPresentValue)
					{
						swapAValueExist = true;
						break;
					}
				}
				#endregion
			}
			#endregion

			#region IsNumberPossibleInGrid : if randomValueNotExist then check next logic : 3) For Grid or GridCellsWise || GridCellsWiseChecking
			if (!swapAValueExist)
			{
				// GetGridIndex
				int GridIndex = GetGridIndex(gridCellRowIndex, gridCellColIndex);
				// Getting List of Tuple i.e. indexes of Current Grid (GridCells)
				List<Tuple<int, int>> GridCells = Grids[GridIndex];

				#region GridWiseChecking For Loop : For GridWise Comparisons (GridCellsWise || GridCellsWiseChecking)
				for (int GridCellIndex = 0; GridCellIndex < GridCells.Count; GridCellIndex++)
				{
					//Debug.Log($"[{gridCells[gridCellIndex].Item1}][{gridCells[gridCellIndex].Item2}]");
					int GridCellRowIndex = GridCells[GridCellIndex].Item1; // Represent RowIndex of GridCell
					int GridCellColIndex = GridCells[GridCellIndex].Item2; // Represent ColIndex of GridCell

					if (Values[GridCellRowIndex, GridCellColIndex] == notPresentValue)
					{
						swapAValueExist = true;
						break;
					}
				}
				#endregion
			}
			#endregion

			// Finally : if randomValueNotExist In all Comparisons : 1) For Row (In RowWise), 2) For Column (In ColWise) 3) For Grid (In GridWise) || GridWiseElements, then simply put that value
			if (!swapAValueExist)
			{
				// To Check : Can swapBRandomValue fit in swapA Position, if ok, then :)
				//SwapPositionsMachineC(swapA, swapB, swapBRandomValue, swapARandomValue);
			}
			else
			{
				// Call SwapPositionsMachine Again :  Because of forloopValue can't fit in swapB Position or swap isn't possible
				SwapPositionsMachine(new Tuple<int, int>(RowIndex, ColIndex), forloopValue); // Manage Indexings	

			}
			#endregion


		}
		#endregion



	}
	#endregion

	#region SwapPositionsMachine
	void SwapPositionsMachine(Tuple<int, int> swapA, int _forloopValue)
	{
		//Debug.Log(_forloopValue);
		sudokuFieldIndex++;
		Debug.Log(sudokuFieldIndex);
		if(sudokuFieldIndex > 80)
		{
			return;
		}
		//Debug.Log(sudokuFieldIndex);
		// GetFilledValues
		List<Tuple<int, int>> sudokuFields = GetSudokufields();
		// Storing currentSudokuField
		Tuple<int, int> swapB;
		// Storing LastRandomValue
		int forloopValue = _forloopValue;

		#region Iterating Each Cell Concept To Swap
		swapB = sudokuFields[sudokuFieldIndex];

		// To Check : Can forloopValue fit in swapB Position, if ok, then :)
		SwapPositionsMachineB(swapA, swapB, forloopValue);
		#endregion

	}
	#endregion

	#region SwapPositionsMachineB
	void SwapPositionsMachineB(Tuple<int, int> swapA, Tuple<int, int> swapB, int _forloopValue)
	{
		// Storing swapA Position
		// swapA RowIndex
		int swapARowIndex = swapA.Item1;
		// swapA ColIndex
		int swapAColIndex = swapA.Item2;

		// Storing swapB Position
		// swapB RowIndex
		int swapBRowIndex = swapB.Item1;
		// swapB ColIndex
		int swapBColIndex = swapB.Item2;

		// Storing swapARandomValue _forloopValue
		int swapARandomValue = _forloopValue;
		// Storing swapARandomValue _forloopValue
		int swapBRandomValue = Values[swapBRowIndex, swapBColIndex];

		#region Comparisons Logic

		swapAValueExist = false;

		//*** There are 3 Logics for check userInputValueExist or not : 1) For Row 2) For Col 3) For Grid ***//

		#region IsNumberPossibleInRow : if randomValueNotExist then check next logic : 1) For Row or Horizontally
		if (!swapAValueExist)
		{
			#region RowWiseChecking For Loop : for Horizontal Comparisons
			for (int col = 0; col < 9; col++)
			{
				if (Values[swapBRowIndex, col] == swapARandomValue)
				{
					swapAValueExist = true;
					break;
				}

			}
			#endregion
		}
		#endregion

		#region IsNumberPossibleInColumn : if randomValueNotExist then check next logic : 2) For Column or Vertically
		if (!swapAValueExist)
		{
			#region ColumnWiseChecking For Loop : for Vertical Comparisons 
			for (int row = 0; row < 9; row++)
			{
				if (Values[row, swapBColIndex] == swapARandomValue)
				{
					swapAValueExist = true;
					break;
				}
			}
			#endregion
		}
		#endregion

		#region IsNumberPossibleInGrid : if randomValueNotExist then check next logic : 3) For Grid or GridCellsWise || GridCellsWiseChecking
		if (!swapAValueExist)
		{
			// GetGridIndex
			int gridIndex = GetGridIndex(swapBRowIndex, swapBColIndex);
			// Getting List of Tuple i.e. indexes of Current Grid (GridCells)
			List<Tuple<int, int>> gridCells = Grids[gridIndex];

			#region GridWiseChecking For Loop : For GridWise Comparisons (GridCellsWise || GridCellsWiseChecking)
			for (int gridCellIndex = 0; gridCellIndex < gridCells.Count; gridCellIndex++)
			{
				//Debug.Log($"[{gridCells[gridCellIndex].Item1}][{gridCells[gridCellIndex].Item2}]");
				int gridCellRowIndex = gridCells[gridCellIndex].Item1; // Represent RowIndex of GridCell
				int gridCellColIndex = gridCells[gridCellIndex].Item2; // Represent ColIndex of GridCell

				if (Values[gridCellRowIndex, gridCellColIndex] == swapARandomValue)
				{
					swapAValueExist = true;
					break;
				}
			}
			#endregion
		}
		#endregion

		// Finally : if randomValueNotExist In all Comparisons : 1) For Row (In RowWise), 2) For Column (In ColWise) 3) For Grid (In GridWise) || GridWiseElements, then simply put that value
		if (!swapAValueExist)
		{
			// To Check : Can swapBRandomValue fit in swapA Position, if ok, then :)
			SwapPositionsMachineC(swapA, swapB, swapBRandomValue, swapARandomValue);
		}
		else
		{		
			// Call SwapPositionsMachine Again :  Because of forloopValue can't fit in swapB Position or swap isn't possible
			SwapPositionsMachine(new Tuple<int, int>(RowIndex, ColIndex), forloopValue); // Manage Indexings	
		
		}
		#endregion

	}
	#endregion

	#region SwapPositionsMachineB
	void SwapPositionsMachineC(Tuple<int, int> swapA, Tuple<int, int> swapB, int _swapBRandomValue, int _swapARandomValue)
	{
		// Storing swapA Position
		// swapA RowIndex
		int swapARowIndex = swapA.Item1;
		// swapA ColIndex
		int swapAColIndex = swapA.Item2;

		// Storing swapB Position
		// swapB RowIndex
		int swapBRowIndex = swapB.Item1;
		// swapB ColIndex
		int swapBColIndex = swapB.Item2;

		// swapB RandomValue
		int swapBRandomValue = _swapBRandomValue;
		// Storing swapARandomValue LastRandomValue
		int swapARandomValue = _swapARandomValue;

		#region Comparisons Logic

		swapBValueExist = false;

		//*** There are 3 Logics for check userInputValueExist or not : 1) For Row 2) For Col 3) For Grid ***//

		#region IsNumberPossibleInRow : if randomValueNotExist then check next logic : 1) For Row or Horizontally
		if (!swapBValueExist)
		{
			#region RowWiseChecking For Loop : for Horizontal Comparisons
			for (int col = 0; col < 9; col++)
			{
				if (Values[swapARowIndex, col] == swapBRandomValue)
				{
					if (swapBRandomValue != 0)
					{
						swapBValueExist = true;
						break;
					}
				}

			}
			#endregion
		}
		#endregion

		#region IsNumberPossibleInColumn : if randomValueNotExist then check next logic : 2) For Column or Vertically
		if (!swapBValueExist)
		{
			#region ColumnWiseChecking For Loop : for Vertical Comparisons 
			for (int row = 0; row < 9; row++)
			{
				if (Values[row, swapAColIndex] == swapBRandomValue)
				{
					if(swapBRandomValue != 0)
					{
						swapBValueExist = true;
						break;
					}
					
				}
			}
			#endregion
		}
		#endregion

		#region IsNumberPossibleInGrid : if randomValueNotExist then check next logic : 3) For Grid or GridCellsWise || GridCellsWiseChecking
		if (!swapBValueExist)
		{
			// GetGridIndex
			int gridIndex = GetGridIndex(swapARowIndex, swapAColIndex);
			// Getting List of Tuple i.e. indexes of Current Grid (GridCells)
			List<Tuple<int, int>> gridCells = Grids[gridIndex];

			#region GridWiseChecking For Loop : For GridWise Comparisons (GridCellsWise || GridCellsWiseChecking)
			for (int gridCellIndex = 0; gridCellIndex < gridCells.Count; gridCellIndex++)
			{
				//Debug.Log($"[{gridCells[gridCellIndex].Item1}][{gridCells[gridCellIndex].Item2}]");
				int gridCellRowIndex = gridCells[gridCellIndex].Item1; // Represent RowIndex of GridCell
				int gridCellColIndex = gridCells[gridCellIndex].Item2; // Represent ColIndex of GridCell

				if (Values[gridCellRowIndex, gridCellColIndex] == swapBRandomValue)
				{
					if (swapBRandomValue != 0)
					{
						swapBValueExist = true;
						break;
					}
				}
			}
			#endregion
		}
		#endregion

		// Finally : if randomValueNotExist In all Comparisons : 1) For Row (In RowWise), 2) For Column (In ColWise) 3) For Grid (In GridWise) || GridWiseElements, then simply put that value
		if (!swapBValueExist)
		{
			Debug.Log(5);
			stopExecution = true;
			
			// Finally Swap Positions With Values if swapA and swapB ok with swapping condition :)
			// swapA Position in SudokuValues
			Values[swapARowIndex, swapAColIndex] = swapBRandomValue;
			// swapB Position in SudokuValues
			Values[swapBRowIndex, swapBColIndex] = swapARandomValue;

		}
		else
		{
			
			// Call SwapPositionsMachine Again :  Because of swapBValue can't fit in swapA Position or swap isn't possible
			SwapPositionsMachine(new Tuple<int, int>(RowIndex, ColIndex), forloopValue); // Manage Indexing
			
				

		}
		#endregion

	}
	#endregion

	#region GetGridIndex
	private int GetGridIndex(int row, int column)
	{
		if(row < 3)
		{
			if(column < 3) { return 0; }
			else if(column < 6) { return 1; }
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

	#region GetSudokufields
	List<Tuple<int, int>> GetSudokufields()
	{
		// Storing filledValues Positions to Swap with any one from these positions to complete solve sudoku logics
		List<Tuple<int, int>> sudokufields = new List<Tuple<int, int>>();
		for (int row = 0; row < 9; row++)
		{
			for (int col = 0; col < 9; col++)
			{
				// GetGridIndex
				int GridIndex = GetGridIndex(row, col);
				if (GridIndex == 0 || GridIndex == 4 || GridIndex == 8)
				{
					continue;
				}
				sudokufields.Add(new Tuple<int, int>(row, col));
			}
		}
		return sudokufields;
	}
	#endregion

	#region GetFilledValues
	List<Tuple<int, int>> GetFilledValues()
	{
		// Storing filledValues Positions to Swap with any one from these positions to complete solve sudoku logics
		List<Tuple<int, int>> filledValues = new List<Tuple<int, int>>();
		for (int row = 0; row < 9; row++)
		{
			for (int col = 0; col < 9; col++)
			{
				filledValues.Add(new Tuple<int, int>(row,col));			
			}
		}
		return filledValues;
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

	#region GetRandomNumber
	private int GetRandomNumber()
    {		
        int index = UnityEngine.Random.Range(0, randomList.Count);
        int randomValue = randomList[index];
        randomList.RemoveAt(index);

        return randomValue;
	}
	#endregion

}
//