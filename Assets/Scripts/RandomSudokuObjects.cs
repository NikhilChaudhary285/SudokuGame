using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class RandomSudokuObjects : MonoBehaviour
{
    public List<int[,]> randomSudokoObjects = new List<int[,]>();

    public int[,] randomSudokuObject = null;

	#region randomSudokoObject1
	private int[,] randomSudokoObject1 = new int[,]
	{
		{5, 6, 8, 4, 2, 7, 9, 1, 3}, //0
        {1, 9, 7, 6, 8, 3, 2, 5, 4}, //1
        {3, 4, 2, 9, 1, 5, 6, 8, 7}, //2
        {6, 8, 5, 1, 3, 2, 4, 7, 9}, //3
        {7, 3, 4, 5, 9, 8, 1, 6, 2}, //4
        {2, 1, 9, 7, 6, 4, 5, 3, 8}, //5
        {8, 5, 1, 3, 4, 9, 7, 2, 6}, //6
        {9, 2, 6, 8, 7, 1, 3, 4, 5}, //7
        {4, 7, 3, 2, 5, 6, 8, 9, 1}, //8
    };
	#endregion

	#region randomSudokoObject2
	private int[,] randomSudokoObject2 = new int[,]
	{
		{5, 1, 3, 6, 2, 7, 9, 8, 4}, //0
        {6, 9, 4, 8, 1, 3, 2, 5, 7}, //1
        {8, 7, 2, 5, 9, 4, 6, 1, 3}, //2
        {2, 8, 1, 3, 6, 9, 7, 4, 5}, //3
        {4, 6, 9, 1, 7, 5, 8, 3, 2}, //4
        {7, 3, 5, 2, 4, 8, 1, 9, 6}, //5
        {9, 2, 6, 4, 5, 1, 3, 7, 8}, //6
        {1, 5, 8, 7, 3, 6, 4, 2, 9}, //7
        {3, 4, 7, 9, 8, 2, 5, 6, 1}, //8
    };
	#endregion

	#region randomSudokoObject3
	private int[,] randomSudokoObject3 = new int[,]
	{
		{8, 7, 1, 9, 6, 2, 3, 4, 5}, //0
        {3, 4, 9, 8, 1, 5, 7, 2, 6}, //1
        {2, 5, 6, 4, 3, 7, 8, 9, 1}, //2
        {1, 3, 2, 6, 5, 8, 4, 7, 9}, //3
        {5, 9, 8, 7, 4, 3, 1, 6, 2}, //4
        {7, 6, 4, 2, 9, 1, 5, 3, 8}, //5
        {4, 2, 7, 5, 8, 6, 9, 1, 3}, //6
        {6, 8, 3, 1, 7, 9, 2, 5, 4}, //7
        {9, 1, 5, 3, 2, 4, 6, 8, 7}, //8
    };
	#endregion

	#region randomSudokoObject4
	private int[,] randomSudokoObject4 = new int[,]
	{
		{9, 8, 4, 7, 6, 2, 5, 1, 3}, //0
        {2, 5, 7, 3, 8, 1, 6, 9, 4}, //1
        {6, 1, 3, 4, 5, 9, 8, 7, 2}, //2
        {1, 9, 6, 8, 2, 4, 7, 3, 5}, //3
        {7, 4, 5, 9, 3, 6, 2, 8, 1}, //4
        {8, 3, 2, 5, 1, 7, 4, 6, 9}, //5
        {4, 2, 9, 6, 7, 3, 1, 5, 8}, //6
        {3, 7, 8, 1, 4, 5, 9, 2, 6}, //7
        {5, 6, 1, 2, 9, 8, 3, 4, 7}, //8
    };
	#endregion

	#region randomSudokoObject5
	private int[,] randomSudokoObject5 = new int[,]
	{
		{7, 5, 9, 4, 1, 3, 2, 6, 8}, //0
        {1, 8, 6, 5, 2, 9, 3, 4, 7}, //1
        {2, 4, 3, 8, 7, 6, 1, 9, 5}, //2
        {5, 6, 1, 9, 4, 7, 8, 2, 3}, //3
        {3, 9, 2, 6, 8, 1, 5, 7, 4}, //4
        {8, 7, 4, 2, 3, 5, 6, 1, 9}, //5
        {9, 3, 8, 7, 6, 2, 4, 5, 1}, //6
        {4, 2, 5, 1, 9, 8, 7, 3, 6}, //7
        {6, 1, 7, 3, 5, 4, 9, 8, 2}, //8
    };
	#endregion

	// Calling this method in ButtonReferencesGenerator Script in Awake Method for specific reason
	public void AddingSudokuObjects()
    {
		// Adding randomSudokoObjects :-
		randomSudokoObjects.Add(randomSudokoObject1); //0
		randomSudokoObjects.Add(randomSudokoObject2); //1
		randomSudokoObjects.Add(randomSudokoObject3); //2
		randomSudokoObjects.Add(randomSudokoObject4); //3
		randomSudokoObjects.Add(randomSudokoObject5); //4
	}

	// PickUpRandomSudokuObject
	public int[,] PickUpRandomSudokuObject()
    {
        // pickupRandomSudokuObjectConcept
        int index = UnityEngine.Random.Range(0, randomSudokoObjects.Count);
		// randomSudokuObject
		randomSudokuObject = randomSudokoObjects[index];

		// Returning randomSudokuObject
		return randomSudokuObject;
	}

	public List<Tuple<int, int>> GetListOfRandomValues()
    {
        List<Tuple<int, int>> RandomValues = new List<Tuple<int, int>>();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                RandomValues.Add(new Tuple<int, int>(i, j));
            }
        }
        return RandomValues;
    }


}