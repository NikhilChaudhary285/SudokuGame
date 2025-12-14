using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;

public class GridRandomNumbers : MonoBehaviour
{
    [SerializeField]
    private GameObject GridMainPanelDisplay;

    // Getting All Grids
    private GameObject[] Grids;

    // Getting MainCellButtons Count or to fill random values in it
    private Button[] MainCellButtons;

    // Each Grid MainCellButtons Count to full random values in it
    private Button[] EachGridMainCellButtons;

    // randomIndexList for storing values for grid and to give randomly
    List<int> randomIndexList;

    // currentUniqueFilledGridElementsValues
    List<int> currentUniqueFilledGridElementsValues;

    // GridReferences
    Dictionary<int, GameObject> GridReferences = new Dictionary<int, GameObject>();

    // GridsButtonsReferences 
    Dictionary<int, Button[]> GridsButtonsReferences = new Dictionary<int, Button[]>();

    // GridButtonsInitializes
    List<Button[]> GridButtonsInitializes = new List<Button[]>();

    // gridMainCells (To store all gridMainCells Values)
    int[] gridMainCells;

    // RandomValuesExistenceCountsDictionary
    Dictionary<int, int> RandomValuesExistenceCountsDictionary;

    // minimunExistenceCountsofRandomValuesList
    List<int> minimunExistenceCountsofRandomValuesList;

    #region Start Event Method
    // Start is called before the first frame update
    void Start()
    {
        // For Only Getting Grids Count or Length
        Grids = GameObject.FindGameObjectsWithTag("Grid");

        // Gettings Total MainCellButtons or to fill random values in it
        MainCellButtons = GridMainPanelDisplay.GetComponentsInChildren<Button>();

        // Gettings Each Grid MainCellButtons Count to fill random values in it
        EachGridMainCellButtons = Grids[0].GetComponentsInChildren<Button>();

        // gridMainCells (To store all gridMainCells Values)
        gridMainCells = new int[GridMainPanelDisplay.GetComponentsInChildren<Button>().Length];

        // StoringGridReferences
        StoringGridReferences();

        // InitializingGridButtonsReferences
        InitializingGridButtonsReferences();

        // StoringGridsButtonsReferences
        StoringGridsButtonsReferences();

        // GridValuesGenerator
        GridValuesGenerator();

        //RandomGeneratorAlgorithm();
    }
    #endregion

    #region StoringGridReferences
    void StoringGridReferences()
    {
        string baseName = "Grid";
        for (int index = 0; index < Grids.Length; index++)
        {
            GridReferences.Add(index, GameObject.Find($"{index + 1}{baseName}")); //Grid || Index

        }

        #region Iterate All Grids
        /*for (int i = 0; i < GridReferences.Count; i++)
        {
            Debug.Log($"{baseName}{i + 1}: {GridReferences[i]} \n");
        }*/
        #endregion

        #region JustPuttingValuesForTest
        /*int value = 1;
        for (int i = 0; i < GridReferences.Count; i++)
        {
            for (int j = 0; j < GridReferences[i].GetComponentsInChildren<Button>().Length; j++)
            {
                //Debug.Log($"Button{value}: {GridReferences[i].GetComponentsInChildren<Button>()[j].gameObject.name} \n");

                GridReferences[i].GetComponentsInChildren<Button>()[j].GetComponentInChildren<TMP_Text>().text = value.ToString();
                value++;
            }
        }*/
        #endregion

    }
    #endregion

    #region Main Logic
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

    #region Another StoringGridsButtonsReferences
    /*void StoringGridsButtonsReferences()
    {
        int iterations = 0;
        #region 1st GridSetup (with comparisons logic)
        //For1ButtonComparisons || Index: 0
        GridsButtonsReferences.Add(0, new Button[] { GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[3][0], GridButtonsInitializes[3][3], GridButtonsInitializes[3][6], GridButtonsInitializes[6][0], GridButtonsInitializes[6][3], GridButtonsInitializes[6][6] });
        //For2ButtonComparisons || Index: 1
        GridsButtonsReferences.Add(1, new Button[] { GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[3][1], GridButtonsInitializes[3][4], GridButtonsInitializes[3][7], GridButtonsInitializes[6][1], GridButtonsInitializes[6][4], GridButtonsInitializes[6][7] });
        //For3ButtonComparisons || Index: 2
        GridsButtonsReferences.Add(2, new Button[] { GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[3][2], GridButtonsInitializes[3][5], GridButtonsInitializes[3][8], GridButtonsInitializes[6][2], GridButtonsInitializes[6][5], GridButtonsInitializes[6][8] });
        //For4ButtonComparisons || Index: 3
        GridsButtonsReferences.Add(3, new Button[] { GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[3][0], GridButtonsInitializes[3][3], GridButtonsInitializes[3][6], GridButtonsInitializes[6][0], GridButtonsInitializes[6][3], GridButtonsInitializes[6][6] });
        //For5ButtonComparisons || Index: 4
        GridsButtonsReferences.Add(4, new Button[] { GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[3][1], GridButtonsInitializes[3][4], GridButtonsInitializes[3][7], GridButtonsInitializes[6][1], GridButtonsInitializes[6][4], GridButtonsInitializes[6][7] });
        //For6ButtonComparisons || Index: 5
        GridsButtonsReferences.Add(5, new Button[] { GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[3][2], GridButtonsInitializes[3][5], GridButtonsInitializes[3][8], GridButtonsInitializes[6][2], GridButtonsInitializes[6][5], GridButtonsInitializes[6][8] });
        //For7ButtonComparisons || Index: 6
        GridsButtonsReferences.Add(6, new Button[] { GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[3][0], GridButtonsInitializes[3][3], GridButtonsInitializes[3][6], GridButtonsInitializes[6][0], GridButtonsInitializes[6][3], GridButtonsInitializes[6][6] });
        //For8ButtonComparisons || Index: 7
        GridsButtonsReferences.Add(7, new Button[] { GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[3][1], GridButtonsInitializes[3][4], GridButtonsInitializes[3][7], GridButtonsInitializes[6][1], GridButtonsInitializes[6][4], GridButtonsInitializes[6][7] });
        //For9ButtonComparisons || Index: 8
        GridsButtonsReferences.Add(8, new Button[] { GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[3][2], GridButtonsInitializes[3][5], GridButtonsInitializes[3][8], GridButtonsInitializes[6][2], GridButtonsInitializes[6][5], GridButtonsInitializes[6][8] });
        #endregion

        #region 2nd GridSetup (with comparisons logic)
        //For10ButtonComparisons || Index: 9
        GridsButtonsReferences.Add(9, new Button[] { GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[4][0], GridButtonsInitializes[4][3], GridButtonsInitializes[4][6], GridButtonsInitializes[7][0], GridButtonsInitializes[7][3], GridButtonsInitializes[7][6] });
        //For11ButtonComparisons || Index: 10
        GridsButtonsReferences.Add(10, new Button[] { GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[4][1], GridButtonsInitializes[4][4], GridButtonsInitializes[4][7], GridButtonsInitializes[7][1], GridButtonsInitializes[7][4], GridButtonsInitializes[7][7] });
        //For12ButtonComparisons || Index: 11
        GridsButtonsReferences.Add(11, new Button[] { GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[1][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[4][2], GridButtonsInitializes[4][5], GridButtonsInitializes[4][8], GridButtonsInitializes[7][2], GridButtonsInitializes[7][5], GridButtonsInitializes[7][8] });
        //For13ButtonComparisons || Index: 12
        GridsButtonsReferences.Add(12, new Button[] { GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[4][0], GridButtonsInitializes[4][3], GridButtonsInitializes[4][6], GridButtonsInitializes[7][0], GridButtonsInitializes[7][3], GridButtonsInitializes[7][6] });
        //For14ButtonComparisons || Index: 13
        GridsButtonsReferences.Add(13, new Button[] { GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[4][1], GridButtonsInitializes[4][4], GridButtonsInitializes[4][7], GridButtonsInitializes[7][1], GridButtonsInitializes[7][4], GridButtonsInitializes[7][7] });
        //For15ButtonComparisons || Index: 14
        GridsButtonsReferences.Add(14, new Button[] { GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[4][2], GridButtonsInitializes[4][5], GridButtonsInitializes[4][8], GridButtonsInitializes[7][2], GridButtonsInitializes[7][5], GridButtonsInitializes[7][8] });
        //For16ButtonComparisons || Index: 15
        GridsButtonsReferences.Add(15, new Button[] { GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[4][0], GridButtonsInitializes[4][3], GridButtonsInitializes[4][6], GridButtonsInitializes[7][0], GridButtonsInitializes[7][3], GridButtonsInitializes[7][6] });
        //For17ButtonComparisons || Index: 16
        GridsButtonsReferences.Add(16, new Button[] { GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[4][1], GridButtonsInitializes[4][4], GridButtonsInitializes[4][7], GridButtonsInitializes[7][1], GridButtonsInitializes[7][4], GridButtonsInitializes[7][7] });
        //For18ButtonComparisons || Index: 17
        GridsButtonsReferences.Add(17, new Button[] { GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[4][2], GridButtonsInitializes[4][5], GridButtonsInitializes[4][8], GridButtonsInitializes[7][2], GridButtonsInitializes[7][5], GridButtonsInitializes[7][8] });
        #endregion

        #region 3rd GridSetup (with comparisons logic)
        //For19ButtonComparisons || Index: 18
        GridsButtonsReferences.Add(18, new Button[] { GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[5][0], GridButtonsInitializes[5][3], GridButtonsInitializes[5][6], GridButtonsInitializes[8][0], GridButtonsInitializes[8][3], GridButtonsInitializes[8][6] });
        //For20ButtonComparisons || Index: 19
        GridsButtonsReferences.Add(19, new Button[] { GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[5][1], GridButtonsInitializes[5][4], GridButtonsInitializes[5][7], GridButtonsInitializes[8][1], GridButtonsInitializes[8][4], GridButtonsInitializes[8][7] });
        //For21ButtonComparisons || Index: 20
        GridsButtonsReferences.Add(20, new Button[] { GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[5][2], GridButtonsInitializes[5][5], GridButtonsInitializes[5][8], GridButtonsInitializes[8][2], GridButtonsInitializes[8][5], GridButtonsInitializes[8][8] });
        //For22ButtonComparisons || Index: 21
        GridsButtonsReferences.Add(21, new Button[] { GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[5][0], GridButtonsInitializes[5][3], GridButtonsInitializes[5][6], GridButtonsInitializes[8][0], GridButtonsInitializes[8][3], GridButtonsInitializes[8][6] });
        //For23ButtonComparisons || Index: 22
        GridsButtonsReferences.Add(22, new Button[] { GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[5][1], GridButtonsInitializes[5][4], GridButtonsInitializes[5][7], GridButtonsInitializes[8][1], GridButtonsInitializes[8][4], GridButtonsInitializes[8][7] });
        //For24ButtonComparisons || Index: 23
        GridsButtonsReferences.Add(23, new Button[] { GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[5][2], GridButtonsInitializes[5][5], GridButtonsInitializes[5][8], GridButtonsInitializes[8][2], GridButtonsInitializes[8][5], GridButtonsInitializes[8][8] });
        //For25ButtonComparisons || Index: 24
        GridsButtonsReferences.Add(24, new Button[] { GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[5][0], GridButtonsInitializes[5][3], GridButtonsInitializes[5][6], GridButtonsInitializes[8][0], GridButtonsInitializes[8][3], GridButtonsInitializes[8][6] });
        //For26ButtonComparisons || Index: 25
        GridsButtonsReferences.Add(25, new Button[] { GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[5][1], GridButtonsInitializes[5][4], GridButtonsInitializes[5][7], GridButtonsInitializes[8][1], GridButtonsInitializes[8][4], GridButtonsInitializes[8][7] });
        //For27ButtonComparisons || Index: 26
        GridsButtonsReferences.Add(26, new Button[] { GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[5][2], GridButtonsInitializes[5][5], GridButtonsInitializes[5][8], GridButtonsInitializes[8][2], GridButtonsInitializes[8][5], GridButtonsInitializes[8][8] });
        #endregion

        #region 4th GridSetup (with comparisons logic)
        //For28ButtonComparisons || Index: 27
        GridsButtonsReferences.Add(27, new Button[] { GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[0][0], GridButtonsInitializes[0][3], GridButtonsInitializes[0][6], GridButtonsInitializes[6][0], GridButtonsInitializes[6][3], GridButtonsInitializes[6][6] });
        //For29ButtonComparisons || Index: 28
        GridsButtonsReferences.Add(28, new Button[] { GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[0][1], GridButtonsInitializes[0][4], GridButtonsInitializes[0][7], GridButtonsInitializes[6][1], GridButtonsInitializes[6][4], GridButtonsInitializes[6][7] });
        //For30ButtonComparisons || Index: 29
        GridsButtonsReferences.Add(29, new Button[] { GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[0][2], GridButtonsInitializes[0][5], GridButtonsInitializes[0][8], GridButtonsInitializes[6][2], GridButtonsInitializes[6][5], GridButtonsInitializes[6][8] });
        //For31ButtonComparisons || Index: 30
        GridsButtonsReferences.Add(30, new Button[] { GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[0][0], GridButtonsInitializes[0][3], GridButtonsInitializes[0][6], GridButtonsInitializes[6][0], GridButtonsInitializes[6][3], GridButtonsInitializes[6][6] });
        //For32ButtonComparisons || Index: 31
        GridsButtonsReferences.Add(31, new Button[] { GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[0][1], GridButtonsInitializes[0][4], GridButtonsInitializes[0][7], GridButtonsInitializes[6][1], GridButtonsInitializes[6][4], GridButtonsInitializes[6][7] });
        //For33ButtonComparisons || Index: 32
        GridsButtonsReferences.Add(32, new Button[] { GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[0][2], GridButtonsInitializes[0][5], GridButtonsInitializes[0][8], GridButtonsInitializes[6][2], GridButtonsInitializes[6][5], GridButtonsInitializes[6][8] });
        //For34ButtonComparisons || Index: 33
        GridsButtonsReferences.Add(33, new Button[] { GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[0][0], GridButtonsInitializes[0][3], GridButtonsInitializes[0][6], GridButtonsInitializes[6][0], GridButtonsInitializes[6][3], GridButtonsInitializes[6][6] });
        //For35ButtonComparisons || Index: 34
        GridsButtonsReferences.Add(34, new Button[] { GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[0][1], GridButtonsInitializes[0][4], GridButtonsInitializes[0][7], GridButtonsInitializes[6][1], GridButtonsInitializes[6][4], GridButtonsInitializes[6][7] });
        //For36ButtonComparisons || Index: 35
        GridsButtonsReferences.Add(35, new Button[] { GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[0][2], GridButtonsInitializes[0][5], GridButtonsInitializes[0][8], GridButtonsInitializes[6][2], GridButtonsInitializes[6][5], GridButtonsInitializes[6][8] });
        #endregion

        #region 5th GridSetup (with comparisons logic)
        //For37ButtonComparisons || Index: 36
        GridsButtonsReferences.Add(36, new Button[] { GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[1][0], GridButtonsInitializes[1][3], GridButtonsInitializes[1][6], GridButtonsInitializes[7][0], GridButtonsInitializes[7][3], GridButtonsInitializes[7][6] });
        //For38ButtonComparisons || Index: 37
        GridsButtonsReferences.Add(37, new Button[] { GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[1][1], GridButtonsInitializes[1][4], GridButtonsInitializes[1][7], GridButtonsInitializes[7][1], GridButtonsInitializes[7][4], GridButtonsInitializes[7][7] });
        //For39ButtonComparisons || Index: 38
        GridsButtonsReferences.Add(38, new Button[] { GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[1][2], GridButtonsInitializes[1][5], GridButtonsInitializes[1][8], GridButtonsInitializes[7][2], GridButtonsInitializes[7][5], GridButtonsInitializes[7][8] });
        //For40ButtonComparisons || Index: 39
        GridsButtonsReferences.Add(39, new Button[] { GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[1][0], GridButtonsInitializes[1][3], GridButtonsInitializes[1][6], GridButtonsInitializes[7][0], GridButtonsInitializes[7][3], GridButtonsInitializes[7][6] });
        //For41ButtonComparisons || Index: 40
        GridsButtonsReferences.Add(40, new Button[] { GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[1][1], GridButtonsInitializes[1][4], GridButtonsInitializes[1][7], GridButtonsInitializes[7][1], GridButtonsInitializes[7][4], GridButtonsInitializes[7][7] });
        //For42ButtonComparisons || Index: 41
        GridsButtonsReferences.Add(41, new Button[] { GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[1][2], GridButtonsInitializes[1][5], GridButtonsInitializes[1][8], GridButtonsInitializes[7][2], GridButtonsInitializes[7][5], GridButtonsInitializes[7][8] });
        //For43ButtonComparisons || Index: 42
        GridsButtonsReferences.Add(42, new Button[] { GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[1][0], GridButtonsInitializes[1][3], GridButtonsInitializes[1][6], GridButtonsInitializes[7][0], GridButtonsInitializes[7][3], GridButtonsInitializes[7][6] });
        //For44ButtonComparisons || Index: 43
        GridsButtonsReferences.Add(43, new Button[] { GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[1][1], GridButtonsInitializes[1][4], GridButtonsInitializes[1][7], GridButtonsInitializes[7][1], GridButtonsInitializes[7][4], GridButtonsInitializes[7][7] });
        //For45ButtonComparisons || Index: 44
        GridsButtonsReferences.Add(44, new Button[] { GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[1][2], GridButtonsInitializes[1][5], GridButtonsInitializes[1][8], GridButtonsInitializes[7][2], GridButtonsInitializes[7][5], GridButtonsInitializes[7][8] });
        #endregion

        #region 6th GridSetup (with comparisons logic)
        //For46ButtonComparisons || Index: 45
        GridsButtonsReferences.Add(45, new Button[] { GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[2][0], GridButtonsInitializes[2][3], GridButtonsInitializes[2][6], GridButtonsInitializes[8][0], GridButtonsInitializes[8][3], GridButtonsInitializes[8][6] });
        //For47ButtonComparisons || Index: 46
        GridsButtonsReferences.Add(46, new Button[] { GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[2][1], GridButtonsInitializes[2][4], GridButtonsInitializes[2][7], GridButtonsInitializes[8][1], GridButtonsInitializes[8][4], GridButtonsInitializes[8][7] });
        //For48ButtonComparisons || Index: 47
        GridsButtonsReferences.Add(47, new Button[] { GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[2][2], GridButtonsInitializes[2][5], GridButtonsInitializes[2][8], GridButtonsInitializes[8][2], GridButtonsInitializes[8][5], GridButtonsInitializes[8][8] });
        //For49ButtonComparisons || Index: 48
        GridsButtonsReferences.Add(48, new Button[] { GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[2][0], GridButtonsInitializes[2][3], GridButtonsInitializes[2][6], GridButtonsInitializes[8][0], GridButtonsInitializes[8][3], GridButtonsInitializes[8][6] });
        //For50ButtonComparisons || Index: 49
        GridsButtonsReferences.Add(49, new Button[] { GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[2][1], GridButtonsInitializes[2][4], GridButtonsInitializes[2][7], GridButtonsInitializes[8][1], GridButtonsInitializes[8][4], GridButtonsInitializes[8][7] });
        //For51ButtonComparisons || Index: 50
        GridsButtonsReferences.Add(50, new Button[] { GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[2][2], GridButtonsInitializes[2][5], GridButtonsInitializes[2][8], GridButtonsInitializes[8][2], GridButtonsInitializes[8][5], GridButtonsInitializes[8][8] });
        //For52ButtonComparisons || Index: 51
        GridsButtonsReferences.Add(51, new Button[] { GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[2][0], GridButtonsInitializes[2][3], GridButtonsInitializes[2][6], GridButtonsInitializes[8][0], GridButtonsInitializes[8][3], GridButtonsInitializes[8][6] });
        //For53ButtonComparisons || Index: 52
        GridsButtonsReferences.Add(52, new Button[] { GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[2][1], GridButtonsInitializes[2][4], GridButtonsInitializes[2][7], GridButtonsInitializes[8][1], GridButtonsInitializes[8][4], GridButtonsInitializes[8][7] });
        //For54ButtonComparisons || Index: 53
        GridsButtonsReferences.Add(53, new Button[] { GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[2][2], GridButtonsInitializes[2][5], GridButtonsInitializes[2][8], GridButtonsInitializes[8][2], GridButtonsInitializes[8][5], GridButtonsInitializes[8][8] });
        #endregion

        #region 7th GridSetup (with comparisons logic)
        //For55ButtonComparisons || Index: 54
        GridsButtonsReferences.Add(54, new Button[] { GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[0][0], GridButtonsInitializes[0][3], GridButtonsInitializes[0][6], GridButtonsInitializes[3][0], GridButtonsInitializes[3][3], GridButtonsInitializes[3][6] });
        //For56ButtonComparisons || Index: 55
        GridsButtonsReferences.Add(55, new Button[] { GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[0][1], GridButtonsInitializes[0][4], GridButtonsInitializes[0][7], GridButtonsInitializes[3][1], GridButtonsInitializes[3][4], GridButtonsInitializes[3][7] });
        //For57ButtonComparisons || Index: 56
        GridsButtonsReferences.Add(56, new Button[] { GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[0][2], GridButtonsInitializes[0][5], GridButtonsInitializes[0][8], GridButtonsInitializes[3][2], GridButtonsInitializes[3][5], GridButtonsInitializes[3][8] });
        //For58ButtonComparisons || Index: 57
        GridsButtonsReferences.Add(57, new Button[] { GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[0][0], GridButtonsInitializes[0][3], GridButtonsInitializes[0][6], GridButtonsInitializes[3][0], GridButtonsInitializes[3][3], GridButtonsInitializes[3][6] });
        //For59ButtonComparisons || Index: 58
        GridsButtonsReferences.Add(58, new Button[] { GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[0][1], GridButtonsInitializes[0][4], GridButtonsInitializes[0][7], GridButtonsInitializes[3][1], GridButtonsInitializes[3][4], GridButtonsInitializes[3][7] });
        //For60ButtonComparisons || Index: 59
        GridsButtonsReferences.Add(59, new Button[] { GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[0][2], GridButtonsInitializes[0][5], GridButtonsInitializes[0][8], GridButtonsInitializes[3][2], GridButtonsInitializes[3][5], GridButtonsInitializes[3][8] });
        //For61ButtonComparisons || Index: 60
        GridsButtonsReferences.Add(60, new Button[] { GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[0][0], GridButtonsInitializes[0][3], GridButtonsInitializes[0][6], GridButtonsInitializes[3][0], GridButtonsInitializes[3][3], GridButtonsInitializes[3][6] });
        //For62ButtonComparisons || Index: 61
        GridsButtonsReferences.Add(61, new Button[] { GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[0][1], GridButtonsInitializes[0][4], GridButtonsInitializes[0][7], GridButtonsInitializes[3][1], GridButtonsInitializes[3][4], GridButtonsInitializes[3][7] });
        //For63ButtonComparisons || Index: 62
        GridsButtonsReferences.Add(62, new Button[] { GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[0][2], GridButtonsInitializes[0][5], GridButtonsInitializes[0][8], GridButtonsInitializes[3][2], GridButtonsInitializes[3][5], GridButtonsInitializes[3][8] });
        #endregion

        #region 8th GridSetup (with comparisons logic)
        //For64ButtonComparisons || Index: 63
        GridsButtonsReferences.Add(63, new Button[] { GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[1][0], GridButtonsInitializes[1][3], GridButtonsInitializes[1][6], GridButtonsInitializes[4][0], GridButtonsInitializes[4][3], GridButtonsInitializes[4][6] });
        //For65ButtonComparisons || Index: 64
        GridsButtonsReferences.Add(64, new Button[] { GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[1][1], GridButtonsInitializes[1][4], GridButtonsInitializes[1][7], GridButtonsInitializes[4][1], GridButtonsInitializes[4][4], GridButtonsInitializes[4][7] });
        //For66ButtonComparisons || Index: 65
        GridsButtonsReferences.Add(65, new Button[] { GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[1][2], GridButtonsInitializes[1][5], GridButtonsInitializes[1][8], GridButtonsInitializes[4][2], GridButtonsInitializes[4][5], GridButtonsInitializes[4][8] });
        //For67ButtonComparisons || Index: 66
        GridsButtonsReferences.Add(66, new Button[] { GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[1][0], GridButtonsInitializes[1][3], GridButtonsInitializes[1][6], GridButtonsInitializes[4][0], GridButtonsInitializes[4][3], GridButtonsInitializes[4][6] });
        //For68ButtonComparisons || Index: 67
        GridsButtonsReferences.Add(67, new Button[] { GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[1][1], GridButtonsInitializes[1][4], GridButtonsInitializes[1][7], GridButtonsInitializes[4][1], GridButtonsInitializes[4][4], GridButtonsInitializes[4][7] });
        //For69ButtonComparisons || Index: 68
        GridsButtonsReferences.Add(68, new Button[] { GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[1][2], GridButtonsInitializes[1][5], GridButtonsInitializes[1][8], GridButtonsInitializes[4][2], GridButtonsInitializes[4][5], GridButtonsInitializes[4][8] });
        //For70ButtonComparisons || Index: 69
        GridsButtonsReferences.Add(69, new Button[] { GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[1][0], GridButtonsInitializes[1][3], GridButtonsInitializes[1][6], GridButtonsInitializes[4][0], GridButtonsInitializes[4][3], GridButtonsInitializes[4][6] });
        //For71ButtonComparisons || Index: 70
        GridsButtonsReferences.Add(70, new Button[] { GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[1][1], GridButtonsInitializes[1][4], GridButtonsInitializes[1][7], GridButtonsInitializes[4][1], GridButtonsInitializes[4][4], GridButtonsInitializes[4][7] });
        //For72ButtonComparisons || Index: 71
        GridsButtonsReferences.Add(71, new Button[] { GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[1][2], GridButtonsInitializes[1][5], GridButtonsInitializes[1][8], GridButtonsInitializes[4][2], GridButtonsInitializes[4][5], GridButtonsInitializes[4][8] });
        #endregion

        #region 9th GridSetup (with comparisons logic)
        //For73ButtonComparisons || Index: 72
        GridsButtonsReferences.Add(72, new Button[] { GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[2][0], GridButtonsInitializes[2][3], GridButtonsInitializes[2][6], GridButtonsInitializes[5][0], GridButtonsInitializes[5][3], GridButtonsInitializes[5][6] });
        //For74ButtonComparisons || Index: 73
        GridsButtonsReferences.Add(73, new Button[] { GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[2][1], GridButtonsInitializes[2][4], GridButtonsInitializes[2][7], GridButtonsInitializes[5][1], GridButtonsInitializes[5][4], GridButtonsInitializes[5][7] });
        //For75ButtonComparisons || Index: 74
        GridsButtonsReferences.Add(74, new Button[] { GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[2][2], GridButtonsInitializes[2][5], GridButtonsInitializes[2][8], GridButtonsInitializes[5][2], GridButtonsInitializes[5][5], GridButtonsInitializes[5][8] });
        //For76ButtonComparisons || Index: 75
        GridsButtonsReferences.Add(75, new Button[] { GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[2][0], GridButtonsInitializes[2][3], GridButtonsInitializes[2][6], GridButtonsInitializes[5][0], GridButtonsInitializes[5][3], GridButtonsInitializes[5][6] });
        //For77ButtonComparisons || Index: 76
        GridsButtonsReferences.Add(76, new Button[] { GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[2][1], GridButtonsInitializes[2][4], GridButtonsInitializes[2][7], GridButtonsInitializes[5][1], GridButtonsInitializes[5][4], GridButtonsInitializes[5][7] });
        //For78ButtonComparisons || Index: 77
        GridsButtonsReferences.Add(77, new Button[] { GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[2][2], GridButtonsInitializes[2][5], GridButtonsInitializes[2][8], GridButtonsInitializes[5][2], GridButtonsInitializes[5][5], GridButtonsInitializes[5][8] });
        //For79ButtonComparisons || Index: 78
        GridsButtonsReferences.Add(78, new Button[] { GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[2][0], GridButtonsInitializes[2][3], GridButtonsInitializes[2][6], GridButtonsInitializes[5][0], GridButtonsInitializes[5][3], GridButtonsInitializes[5][6] });
        //For80ButtonComparisons || Index: 79
        GridsButtonsReferences.Add(79, new Button[] { GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[2][1], GridButtonsInitializes[2][4], GridButtonsInitializes[2][7], GridButtonsInitializes[5][1], GridButtonsInitializes[5][4], GridButtonsInitializes[5][7] });
        //For81ButtonComparisons || Index: 80
        GridsButtonsReferences.Add(80, new Button[] { GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[2][2], GridButtonsInitializes[2][5], GridButtonsInitializes[2][8], GridButtonsInitializes[5][2], GridButtonsInitializes[5][5], GridButtonsInitializes[5][8] });
        #endregion

        *//*for (int i = 0; i < GridsButtonsReferences.Count; i++)
        {
            for (int j = 0; j < GridsButtonsReferences[i].Length; j++)
            {
                Debug.Log(GridsButtonsReferences[i]);
                Debug.Log(GridsButtonsReferences[i][j].gameObject.name);
                Debug.Log(GridsButtonsReferences[i][j].GetComponentInChildren<TMP_Text>().text);
                iterations++;
            }
        }
        Debug.Log(iterations);*//*
    }*/
    #endregion

    void StoringGridsButtonsReferences()
    {
        #region 1st GridSetup (with comparisons logic)
        //For1ButtonComparisons || Index: 0
        GridsButtonsReferences.Add(0, new Button[] { GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[3][0], GridButtonsInitializes[3][3], GridButtonsInitializes[3][6], GridButtonsInitializes[6][0], GridButtonsInitializes[6][3], GridButtonsInitializes[6][6] });
        //For2ButtonComparisons || Index: 1
        GridsButtonsReferences.Add(1, new Button[] { GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[3][1], GridButtonsInitializes[3][4], GridButtonsInitializes[3][7], GridButtonsInitializes[6][1], GridButtonsInitializes[6][4], GridButtonsInitializes[6][7] });
        //For3ButtonComparisons || Index: 2
        GridsButtonsReferences.Add(2, new Button[] { GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[3][2], GridButtonsInitializes[3][5], GridButtonsInitializes[3][8], GridButtonsInitializes[6][2], GridButtonsInitializes[6][5], GridButtonsInitializes[6][8] });
        //For4ButtonComparisons || Index: 3
        GridsButtonsReferences.Add(3, new Button[] { GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[3][0], GridButtonsInitializes[3][3], GridButtonsInitializes[3][6], GridButtonsInitializes[6][0], GridButtonsInitializes[6][3], GridButtonsInitializes[6][6] });
        //For5ButtonComparisons || Index: 4
        GridsButtonsReferences.Add(4, new Button[] { GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[3][1], GridButtonsInitializes[3][4], GridButtonsInitializes[3][7], GridButtonsInitializes[6][1], GridButtonsInitializes[6][4], GridButtonsInitializes[6][7] });
        //For6ButtonComparisons || Index: 5
        GridsButtonsReferences.Add(5, new Button[] { GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[3][2], GridButtonsInitializes[3][5], GridButtonsInitializes[3][8], GridButtonsInitializes[6][2], GridButtonsInitializes[6][5], GridButtonsInitializes[6][8] });
        //For7ButtonComparisons || Index: 6
        GridsButtonsReferences.Add(6, new Button[] { GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[3][0], GridButtonsInitializes[3][3], GridButtonsInitializes[3][6], GridButtonsInitializes[6][0], GridButtonsInitializes[6][3], GridButtonsInitializes[6][6] });
        //For8ButtonComparisons || Index: 7
        GridsButtonsReferences.Add(7, new Button[] { GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[3][1], GridButtonsInitializes[3][4], GridButtonsInitializes[3][7], GridButtonsInitializes[6][1], GridButtonsInitializes[6][4], GridButtonsInitializes[6][7] });
        //For9ButtonComparisons || Index: 8
        GridsButtonsReferences.Add(8, new Button[] { GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[3][2], GridButtonsInitializes[3][5], GridButtonsInitializes[3][8], GridButtonsInitializes[6][2], GridButtonsInitializes[6][5], GridButtonsInitializes[6][8] });
        #endregion

        #region 2nd GridSetup (with comparisons logic)
        //For10ButtonComparisons || Index: 9
        GridsButtonsReferences.Add(9, new Button[] { GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[4][0], GridButtonsInitializes[4][3], GridButtonsInitializes[4][6], GridButtonsInitializes[7][0], GridButtonsInitializes[7][3], GridButtonsInitializes[7][6] });
        //For11ButtonComparisons || Index: 10
        GridsButtonsReferences.Add(10, new Button[] { GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[4][1], GridButtonsInitializes[4][4], GridButtonsInitializes[4][7], GridButtonsInitializes[7][1], GridButtonsInitializes[7][4], GridButtonsInitializes[7][7] });
        //For12ButtonComparisons || Index: 11
        GridsButtonsReferences.Add(11, new Button[] { GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[2][0], GridButtonsInitializes[2][1], GridButtonsInitializes[2][2], GridButtonsInitializes[4][2], GridButtonsInitializes[4][5], GridButtonsInitializes[4][8], GridButtonsInitializes[7][2], GridButtonsInitializes[7][5], GridButtonsInitializes[7][8] });
        //For13ButtonComparisons || Index: 12
        GridsButtonsReferences.Add(12, new Button[] { GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[4][0], GridButtonsInitializes[4][3], GridButtonsInitializes[4][6], GridButtonsInitializes[7][0], GridButtonsInitializes[7][3], GridButtonsInitializes[7][6] });
        //For14ButtonComparisons || Index: 13
        GridsButtonsReferences.Add(13, new Button[] { GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[4][1], GridButtonsInitializes[4][4], GridButtonsInitializes[4][7], GridButtonsInitializes[7][1], GridButtonsInitializes[7][4], GridButtonsInitializes[7][7] });
        //For15ButtonComparisons || Index: 14
        GridsButtonsReferences.Add(14, new Button[] { GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[2][3], GridButtonsInitializes[2][4], GridButtonsInitializes[2][5], GridButtonsInitializes[4][2], GridButtonsInitializes[4][5], GridButtonsInitializes[4][8], GridButtonsInitializes[7][2], GridButtonsInitializes[7][5], GridButtonsInitializes[7][8] });
        //For16ButtonComparisons || Index: 15
        GridsButtonsReferences.Add(15, new Button[] { GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[4][0], GridButtonsInitializes[4][3], GridButtonsInitializes[4][6], GridButtonsInitializes[7][0], GridButtonsInitializes[7][3], GridButtonsInitializes[7][6] });
        //For17ButtonComparisons || Index: 16
        GridsButtonsReferences.Add(16, new Button[] { GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[4][1], GridButtonsInitializes[4][4], GridButtonsInitializes[4][7], GridButtonsInitializes[7][1], GridButtonsInitializes[7][4], GridButtonsInitializes[7][7] });
        //For18ButtonComparisons || Index: 17
        GridsButtonsReferences.Add(17, new Button[] { GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[2][6], GridButtonsInitializes[2][7], GridButtonsInitializes[2][8], GridButtonsInitializes[4][2], GridButtonsInitializes[4][5], GridButtonsInitializes[4][8], GridButtonsInitializes[7][2], GridButtonsInitializes[7][5], GridButtonsInitializes[7][8] });
        #endregion

        #region 3rd GridSetup (with comparisons logic)
        //For19ButtonComparisons || Index: 18
        GridsButtonsReferences.Add(18, new Button[] { GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[5][0], GridButtonsInitializes[5][3], GridButtonsInitializes[5][6], GridButtonsInitializes[8][0], GridButtonsInitializes[8][3], GridButtonsInitializes[8][6] });
        //For20ButtonComparisons || Index: 19
        GridsButtonsReferences.Add(19, new Button[] { GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[5][1], GridButtonsInitializes[5][4], GridButtonsInitializes[5][7], GridButtonsInitializes[8][1], GridButtonsInitializes[8][4], GridButtonsInitializes[8][7] });
        //For21ButtonComparisons || Index: 20
        GridsButtonsReferences.Add(20, new Button[] { GridButtonsInitializes[0][0], GridButtonsInitializes[0][1], GridButtonsInitializes[0][2], GridButtonsInitializes[1][0], GridButtonsInitializes[1][1], GridButtonsInitializes[1][2], GridButtonsInitializes[5][2], GridButtonsInitializes[5][5], GridButtonsInitializes[5][8], GridButtonsInitializes[8][2], GridButtonsInitializes[8][5], GridButtonsInitializes[8][8] });
        //For22ButtonComparisons || Index: 21
        GridsButtonsReferences.Add(21, new Button[] { GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[5][0], GridButtonsInitializes[5][3], GridButtonsInitializes[5][6], GridButtonsInitializes[8][0], GridButtonsInitializes[8][3], GridButtonsInitializes[8][6] });
        //For23ButtonComparisons || Index: 22
        GridsButtonsReferences.Add(22, new Button[] { GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[5][1], GridButtonsInitializes[5][4], GridButtonsInitializes[5][7], GridButtonsInitializes[8][1], GridButtonsInitializes[8][4], GridButtonsInitializes[8][7] });
        //For24ButtonComparisons || Index: 23
        GridsButtonsReferences.Add(23, new Button[] { GridButtonsInitializes[0][3], GridButtonsInitializes[0][4], GridButtonsInitializes[0][5], GridButtonsInitializes[1][3], GridButtonsInitializes[1][4], GridButtonsInitializes[1][5], GridButtonsInitializes[5][2], GridButtonsInitializes[5][5], GridButtonsInitializes[5][8], GridButtonsInitializes[8][2], GridButtonsInitializes[8][5], GridButtonsInitializes[8][8] });
        //For25ButtonComparisons || Index: 24
        GridsButtonsReferences.Add(24, new Button[] { GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[5][0], GridButtonsInitializes[5][3], GridButtonsInitializes[5][6], GridButtonsInitializes[8][0], GridButtonsInitializes[8][3], GridButtonsInitializes[8][6] });
        //For26ButtonComparisons || Index: 25
        GridsButtonsReferences.Add(25, new Button[] { GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[5][1], GridButtonsInitializes[5][4], GridButtonsInitializes[5][7], GridButtonsInitializes[8][1], GridButtonsInitializes[8][4], GridButtonsInitializes[8][7] });
        //For27ButtonComparisons || Index: 26
        GridsButtonsReferences.Add(26, new Button[] { GridButtonsInitializes[0][6], GridButtonsInitializes[0][7], GridButtonsInitializes[0][8], GridButtonsInitializes[1][6], GridButtonsInitializes[1][7], GridButtonsInitializes[1][8], GridButtonsInitializes[5][2], GridButtonsInitializes[5][5], GridButtonsInitializes[5][8], GridButtonsInitializes[8][2], GridButtonsInitializes[8][5], GridButtonsInitializes[8][8] });
        #endregion

        #region 4th GridSetup (with comparisons logic)
        //For28ButtonComparisons || Index: 27
        GridsButtonsReferences.Add(27, new Button[] { GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[0][0], GridButtonsInitializes[0][3], GridButtonsInitializes[0][6], GridButtonsInitializes[6][0], GridButtonsInitializes[6][3], GridButtonsInitializes[6][6] });
        //For29ButtonComparisons || Index: 28
        GridsButtonsReferences.Add(28, new Button[] { GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[0][1], GridButtonsInitializes[0][4], GridButtonsInitializes[0][7], GridButtonsInitializes[6][1], GridButtonsInitializes[6][4], GridButtonsInitializes[6][7] });
        //For30ButtonComparisons || Index: 29
        GridsButtonsReferences.Add(29, new Button[] { GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[0][2], GridButtonsInitializes[0][5], GridButtonsInitializes[0][8], GridButtonsInitializes[6][2], GridButtonsInitializes[6][5], GridButtonsInitializes[6][8] });
        //For31ButtonComparisons || Index: 30
        GridsButtonsReferences.Add(30, new Button[] { GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[0][0], GridButtonsInitializes[0][3], GridButtonsInitializes[0][6], GridButtonsInitializes[6][0], GridButtonsInitializes[6][3], GridButtonsInitializes[6][6] });
        //For32ButtonComparisons || Index: 31
        GridsButtonsReferences.Add(31, new Button[] { GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[0][1], GridButtonsInitializes[0][4], GridButtonsInitializes[0][7], GridButtonsInitializes[6][1], GridButtonsInitializes[6][4], GridButtonsInitializes[6][7] });
        //For33ButtonComparisons || Index: 32
        GridsButtonsReferences.Add(32, new Button[] { GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[0][2], GridButtonsInitializes[0][5], GridButtonsInitializes[0][8], GridButtonsInitializes[6][2], GridButtonsInitializes[6][5], GridButtonsInitializes[6][8] });
        //For34ButtonComparisons || Index: 33
        GridsButtonsReferences.Add(33, new Button[] { GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[0][0], GridButtonsInitializes[0][3], GridButtonsInitializes[0][6], GridButtonsInitializes[6][0], GridButtonsInitializes[6][3], GridButtonsInitializes[6][6] });
        //For35ButtonComparisons || Index: 34
        GridsButtonsReferences.Add(34, new Button[] { GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[0][1], GridButtonsInitializes[0][4], GridButtonsInitializes[0][7], GridButtonsInitializes[6][1], GridButtonsInitializes[6][4], GridButtonsInitializes[6][7] });
        //For36ButtonComparisons || Index: 35
        GridsButtonsReferences.Add(35, new Button[] { GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[0][2], GridButtonsInitializes[0][5], GridButtonsInitializes[0][8], GridButtonsInitializes[6][2], GridButtonsInitializes[6][5], GridButtonsInitializes[6][8] });
        #endregion

        #region 5th GridSetup (with comparisons logic)
        //For37ButtonComparisons || Index: 36
        GridsButtonsReferences.Add(36, new Button[] { GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[1][0], GridButtonsInitializes[1][3], GridButtonsInitializes[1][6], GridButtonsInitializes[7][0], GridButtonsInitializes[7][3], GridButtonsInitializes[7][6] });
        //For38ButtonComparisons || Index: 37
        GridsButtonsReferences.Add(37, new Button[] { GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[1][1], GridButtonsInitializes[1][4], GridButtonsInitializes[1][7], GridButtonsInitializes[7][1], GridButtonsInitializes[7][4], GridButtonsInitializes[7][7] });
        //For39ButtonComparisons || Index: 38
        GridsButtonsReferences.Add(38, new Button[] { GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[5][0], GridButtonsInitializes[5][1], GridButtonsInitializes[5][2], GridButtonsInitializes[1][2], GridButtonsInitializes[1][5], GridButtonsInitializes[1][8], GridButtonsInitializes[7][2], GridButtonsInitializes[7][5], GridButtonsInitializes[7][8] });
        //For40ButtonComparisons || Index: 39
        GridsButtonsReferences.Add(39, new Button[] { GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[1][0], GridButtonsInitializes[1][3], GridButtonsInitializes[1][6], GridButtonsInitializes[7][0], GridButtonsInitializes[7][3], GridButtonsInitializes[7][6] });
        //For41ButtonComparisons || Index: 40
        GridsButtonsReferences.Add(40, new Button[] { GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[1][1], GridButtonsInitializes[1][4], GridButtonsInitializes[1][7], GridButtonsInitializes[7][1], GridButtonsInitializes[7][4], GridButtonsInitializes[7][7] });
        //For42ButtonComparisons || Index: 41
        GridsButtonsReferences.Add(41, new Button[] { GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[5][3], GridButtonsInitializes[5][4], GridButtonsInitializes[5][5], GridButtonsInitializes[1][2], GridButtonsInitializes[1][5], GridButtonsInitializes[1][8], GridButtonsInitializes[7][2], GridButtonsInitializes[7][5], GridButtonsInitializes[7][8] });
        //For43ButtonComparisons || Index: 42
        GridsButtonsReferences.Add(42, new Button[] { GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[1][0], GridButtonsInitializes[1][3], GridButtonsInitializes[1][6], GridButtonsInitializes[7][0], GridButtonsInitializes[7][3], GridButtonsInitializes[7][6] });
        //For44ButtonComparisons || Index: 43
        GridsButtonsReferences.Add(43, new Button[] { GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[1][1], GridButtonsInitializes[1][4], GridButtonsInitializes[1][7], GridButtonsInitializes[7][1], GridButtonsInitializes[7][4], GridButtonsInitializes[7][7] });
        //For45ButtonComparisons || Index: 44
        GridsButtonsReferences.Add(44, new Button[] { GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[5][6], GridButtonsInitializes[5][7], GridButtonsInitializes[5][8], GridButtonsInitializes[1][2], GridButtonsInitializes[1][5], GridButtonsInitializes[1][8], GridButtonsInitializes[7][2], GridButtonsInitializes[7][5], GridButtonsInitializes[7][8] });
        #endregion

        #region 6th GridSetup (with comparisons logic)
        //For46ButtonComparisons || Index: 45
        GridsButtonsReferences.Add(45, new Button[] { GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[2][0], GridButtonsInitializes[2][3], GridButtonsInitializes[2][6], GridButtonsInitializes[8][0], GridButtonsInitializes[8][3], GridButtonsInitializes[8][6] });
        //For47ButtonComparisons || Index: 46
        GridsButtonsReferences.Add(46, new Button[] { GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[2][1], GridButtonsInitializes[2][4], GridButtonsInitializes[2][7], GridButtonsInitializes[8][1], GridButtonsInitializes[8][4], GridButtonsInitializes[8][7] });
        //For48ButtonComparisons || Index: 47
        GridsButtonsReferences.Add(47, new Button[] { GridButtonsInitializes[3][0], GridButtonsInitializes[3][1], GridButtonsInitializes[3][2], GridButtonsInitializes[4][0], GridButtonsInitializes[4][1], GridButtonsInitializes[4][2], GridButtonsInitializes[2][2], GridButtonsInitializes[2][5], GridButtonsInitializes[2][8], GridButtonsInitializes[8][2], GridButtonsInitializes[8][5], GridButtonsInitializes[8][8] });
        //For49ButtonComparisons || Index: 48
        GridsButtonsReferences.Add(48, new Button[] { GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[2][0], GridButtonsInitializes[2][3], GridButtonsInitializes[2][6], GridButtonsInitializes[8][0], GridButtonsInitializes[8][3], GridButtonsInitializes[8][6] });
        //For50ButtonComparisons || Index: 49
        GridsButtonsReferences.Add(49, new Button[] { GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[2][1], GridButtonsInitializes[2][4], GridButtonsInitializes[2][7], GridButtonsInitializes[8][1], GridButtonsInitializes[8][4], GridButtonsInitializes[8][7] });
        //For51ButtonComparisons || Index: 50
        GridsButtonsReferences.Add(50, new Button[] { GridButtonsInitializes[3][3], GridButtonsInitializes[3][4], GridButtonsInitializes[3][5], GridButtonsInitializes[4][3], GridButtonsInitializes[4][4], GridButtonsInitializes[4][5], GridButtonsInitializes[2][2], GridButtonsInitializes[2][5], GridButtonsInitializes[2][8], GridButtonsInitializes[8][2], GridButtonsInitializes[8][5], GridButtonsInitializes[8][8] });
        //For52ButtonComparisons || Index: 51
        GridsButtonsReferences.Add(51, new Button[] { GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[2][0], GridButtonsInitializes[2][3], GridButtonsInitializes[2][6], GridButtonsInitializes[8][0], GridButtonsInitializes[8][3], GridButtonsInitializes[8][6] });
        //For53ButtonComparisons || Index: 52
        GridsButtonsReferences.Add(52, new Button[] { GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[2][1], GridButtonsInitializes[2][4], GridButtonsInitializes[2][7], GridButtonsInitializes[8][1], GridButtonsInitializes[8][4], GridButtonsInitializes[8][7] });
        //For54ButtonComparisons || Index: 53
        GridsButtonsReferences.Add(53, new Button[] { GridButtonsInitializes[3][6], GridButtonsInitializes[3][7], GridButtonsInitializes[3][8], GridButtonsInitializes[4][6], GridButtonsInitializes[4][7], GridButtonsInitializes[4][8], GridButtonsInitializes[2][2], GridButtonsInitializes[2][5], GridButtonsInitializes[2][8], GridButtonsInitializes[8][2], GridButtonsInitializes[8][5], GridButtonsInitializes[8][8] });
        #endregion

        #region 7th GridSetup (with comparisons logic)
        //For55ButtonComparisons || Index: 54
        GridsButtonsReferences.Add(54, new Button[] { GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[0][0], GridButtonsInitializes[0][3], GridButtonsInitializes[0][6], GridButtonsInitializes[3][0], GridButtonsInitializes[3][3], GridButtonsInitializes[3][6] });
        //For56ButtonComparisons || Index: 55
        GridsButtonsReferences.Add(55, new Button[] { GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[0][1], GridButtonsInitializes[0][4], GridButtonsInitializes[0][7], GridButtonsInitializes[3][1], GridButtonsInitializes[3][4], GridButtonsInitializes[3][7] });
        //For57ButtonComparisons || Index: 56
        GridsButtonsReferences.Add(56, new Button[] { GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[0][2], GridButtonsInitializes[0][5], GridButtonsInitializes[0][8], GridButtonsInitializes[3][2], GridButtonsInitializes[3][5], GridButtonsInitializes[3][8] });
        //For58ButtonComparisons || Index: 57
        GridsButtonsReferences.Add(57, new Button[] { GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[0][0], GridButtonsInitializes[0][3], GridButtonsInitializes[0][6], GridButtonsInitializes[3][0], GridButtonsInitializes[3][3], GridButtonsInitializes[3][6] });
        //For59ButtonComparisons || Index: 58
        GridsButtonsReferences.Add(58, new Button[] { GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[0][1], GridButtonsInitializes[0][4], GridButtonsInitializes[0][7], GridButtonsInitializes[3][1], GridButtonsInitializes[3][4], GridButtonsInitializes[3][7] });
        //For60ButtonComparisons || Index: 59
        GridsButtonsReferences.Add(59, new Button[] { GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[0][2], GridButtonsInitializes[0][5], GridButtonsInitializes[0][8], GridButtonsInitializes[3][2], GridButtonsInitializes[3][5], GridButtonsInitializes[3][8] });
        //For61ButtonComparisons || Index: 60
        GridsButtonsReferences.Add(60, new Button[] { GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[0][0], GridButtonsInitializes[0][3], GridButtonsInitializes[0][6], GridButtonsInitializes[3][0], GridButtonsInitializes[3][3], GridButtonsInitializes[3][6] });
        //For62ButtonComparisons || Index: 61
        GridsButtonsReferences.Add(61, new Button[] { GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[0][1], GridButtonsInitializes[0][4], GridButtonsInitializes[0][7], GridButtonsInitializes[3][1], GridButtonsInitializes[3][4], GridButtonsInitializes[3][7] });
        //For63ButtonComparisons || Index: 62
        GridsButtonsReferences.Add(62, new Button[] { GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[0][2], GridButtonsInitializes[0][5], GridButtonsInitializes[0][8], GridButtonsInitializes[3][2], GridButtonsInitializes[3][5], GridButtonsInitializes[3][8] });
        #endregion

        #region 8th GridSetup (with comparisons logic)
        //For64ButtonComparisons || Index: 63
        GridsButtonsReferences.Add(63, new Button[] { GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[1][0], GridButtonsInitializes[1][3], GridButtonsInitializes[1][6], GridButtonsInitializes[4][0], GridButtonsInitializes[4][3], GridButtonsInitializes[4][6] });
        //For65ButtonComparisons || Index: 64
        GridsButtonsReferences.Add(64, new Button[] { GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[1][1], GridButtonsInitializes[1][4], GridButtonsInitializes[1][7], GridButtonsInitializes[4][1], GridButtonsInitializes[4][4], GridButtonsInitializes[4][7] });
        //For66ButtonComparisons || Index: 65
        GridsButtonsReferences.Add(65, new Button[] { GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[8][0], GridButtonsInitializes[8][1], GridButtonsInitializes[8][2], GridButtonsInitializes[1][2], GridButtonsInitializes[1][5], GridButtonsInitializes[1][8], GridButtonsInitializes[4][2], GridButtonsInitializes[4][5], GridButtonsInitializes[4][8] });
        //For67ButtonComparisons || Index: 66
        GridsButtonsReferences.Add(66, new Button[] { GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[1][0], GridButtonsInitializes[1][3], GridButtonsInitializes[1][6], GridButtonsInitializes[4][0], GridButtonsInitializes[4][3], GridButtonsInitializes[4][6] });
        //For68ButtonComparisons || Index: 67
        GridsButtonsReferences.Add(67, new Button[] { GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[1][1], GridButtonsInitializes[1][4], GridButtonsInitializes[1][7], GridButtonsInitializes[4][1], GridButtonsInitializes[4][4], GridButtonsInitializes[4][7] });
        //For69ButtonComparisons || Index: 68
        GridsButtonsReferences.Add(68, new Button[] { GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[8][3], GridButtonsInitializes[8][4], GridButtonsInitializes[8][5], GridButtonsInitializes[1][2], GridButtonsInitializes[1][5], GridButtonsInitializes[1][8], GridButtonsInitializes[4][2], GridButtonsInitializes[4][5], GridButtonsInitializes[4][8] });
        //For70ButtonComparisons || Index: 69
        GridsButtonsReferences.Add(69, new Button[] { GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[1][0], GridButtonsInitializes[1][3], GridButtonsInitializes[1][6], GridButtonsInitializes[4][0], GridButtonsInitializes[4][3], GridButtonsInitializes[4][6] });
        //For71ButtonComparisons || Index: 70
        GridsButtonsReferences.Add(70, new Button[] { GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[1][1], GridButtonsInitializes[1][4], GridButtonsInitializes[1][7], GridButtonsInitializes[4][1], GridButtonsInitializes[4][4], GridButtonsInitializes[4][7] });
        //For72ButtonComparisons || Index: 71
        GridsButtonsReferences.Add(71, new Button[] { GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[8][6], GridButtonsInitializes[8][7], GridButtonsInitializes[8][8], GridButtonsInitializes[1][2], GridButtonsInitializes[1][5], GridButtonsInitializes[1][8], GridButtonsInitializes[4][2], GridButtonsInitializes[4][5], GridButtonsInitializes[4][8] });
        #endregion

        #region 9th GridSetup (with comparisons logic)
        //For73ButtonComparisons || Index: 72
        GridsButtonsReferences.Add(72, new Button[] { GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[2][0], GridButtonsInitializes[2][3], GridButtonsInitializes[2][6], GridButtonsInitializes[5][0], GridButtonsInitializes[5][3], GridButtonsInitializes[5][6] });
        //For74ButtonComparisons || Index: 73
        GridsButtonsReferences.Add(73, new Button[] { GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[2][1], GridButtonsInitializes[2][4], GridButtonsInitializes[2][7], GridButtonsInitializes[5][1], GridButtonsInitializes[5][4], GridButtonsInitializes[5][7] });
        //For75ButtonComparisons || Index: 74
        GridsButtonsReferences.Add(74, new Button[] { GridButtonsInitializes[6][0], GridButtonsInitializes[6][1], GridButtonsInitializes[6][2], GridButtonsInitializes[7][0], GridButtonsInitializes[7][1], GridButtonsInitializes[7][2], GridButtonsInitializes[2][2], GridButtonsInitializes[2][5], GridButtonsInitializes[2][8], GridButtonsInitializes[5][2], GridButtonsInitializes[5][5], GridButtonsInitializes[5][8] });
        //For76ButtonComparisons || Index: 75
        GridsButtonsReferences.Add(75, new Button[] { GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[2][0], GridButtonsInitializes[2][3], GridButtonsInitializes[2][6], GridButtonsInitializes[5][0], GridButtonsInitializes[5][3], GridButtonsInitializes[5][6] });
        //For77ButtonComparisons || Index: 76
        GridsButtonsReferences.Add(76, new Button[] { GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[2][1], GridButtonsInitializes[2][4], GridButtonsInitializes[2][7], GridButtonsInitializes[5][1], GridButtonsInitializes[5][4], GridButtonsInitializes[5][7] });
        //For78ButtonComparisons || Index: 77
        GridsButtonsReferences.Add(77, new Button[] { GridButtonsInitializes[6][3], GridButtonsInitializes[6][4], GridButtonsInitializes[6][5], GridButtonsInitializes[7][3], GridButtonsInitializes[7][4], GridButtonsInitializes[7][5], GridButtonsInitializes[2][2], GridButtonsInitializes[2][5], GridButtonsInitializes[2][8], GridButtonsInitializes[5][2], GridButtonsInitializes[5][5], GridButtonsInitializes[5][8] });
        //For79ButtonComparisons || Index: 78
        GridsButtonsReferences.Add(78, new Button[] { GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[2][0], GridButtonsInitializes[2][3], GridButtonsInitializes[2][6], GridButtonsInitializes[5][0], GridButtonsInitializes[5][3], GridButtonsInitializes[5][6] });
        //For80ButtonComparisons || Index: 79
        GridsButtonsReferences.Add(79, new Button[] { GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[2][1], GridButtonsInitializes[2][4], GridButtonsInitializes[2][7], GridButtonsInitializes[5][1], GridButtonsInitializes[5][4], GridButtonsInitializes[5][7] });
        //For81ButtonComparisons || Index: 80
        GridsButtonsReferences.Add(80, new Button[] { GridButtonsInitializes[6][6], GridButtonsInitializes[6][7], GridButtonsInitializes[6][8], GridButtonsInitializes[7][6], GridButtonsInitializes[7][7], GridButtonsInitializes[7][8], GridButtonsInitializes[2][2], GridButtonsInitializes[2][5], GridButtonsInitializes[2][8], GridButtonsInitializes[5][2], GridButtonsInitializes[5][5], GridButtonsInitializes[5][8] });
        #endregion

    }
    #endregion

    #region ReinitializeList (randomIndexList)
    void ReinitializeList(int GridMainCellButtonsArraySize)
    {
        randomIndexList = new List<int>();
        for (int i = 1; i < (GridMainCellButtonsArraySize + 1); i++)
        {
            randomIndexList.Add(i);
        }
    }
    #endregion

    #region ReinitializeList (currentUniqueFilledGridElementsValuesList)
    void ReinitializeUniqueFilledGridElementsValuesList()
    {
        currentUniqueFilledGridElementsValues = new List<int>();

    }
    #endregion

    #region ReinitializeRandomValuesExistenceCountsDictionary
    void ReinitializeRandomValuesExistenceCountsDictionary()
    {
        RandomValuesExistenceCountsDictionary = new Dictionary<int, int>();

    }
    #endregion

    #region ReinitializeList (minimunExistenceCountsofRandomValuesList)
    void ReinitializeMinimunExistenceCountsofRandomValuesList()
    {
        minimunExistenceCountsofRandomValuesList = new List<int>();

    }
    #endregion

    void GridValuesGenerator()
    {
        int GridMainCellButtonsArraySize = EachGridMainCellButtons.Length;
        int[] GridMainCellButtonsArray = new int[GridMainCellButtonsArraySize];

        // List Concept
        int randomIndex;
        int randomIndexValue;

        bool x = false;
        int TotalIterations = 0;

        //currentFilledGrids Holder
        int currentFilledGrids = 0;

        // CurrentSelectedGridIndex
        int GridIndex = 0;

        // currentSelectedGrid Holder
        GameObject currentSelectedGrid;

        // To store currentSelectedGridButtons
        Button[] currentSelectedGridButtons;

        // To store currentFilledGridElementsValues
        int currentFilledGridElementsValues = 0;

        // currentGridGridsReferences
        List<GameObject> currentGridGridsReferences = new List<GameObject>();

        int gridFilledMainCells = 0;

        // pickedUpButton
        Button pickedUpButton;

        while (currentFilledGrids < Grids.Length)
        {
            // Initialize or ReinitializeUniqueFilledGridElementsValuesList for Putting UniqueFilledGridElementsValues for current or next Grid
            ReinitializeUniqueFilledGridElementsValuesList();
            // Reinitialize currentSelectedGrid
            currentSelectedGrid = GridReferences[GridIndex];

            // Reinitialize currentSelectedGridButtons    
            currentSelectedGridButtons = currentSelectedGrid.GetComponentsInChildren<Button>();

            while (currentFilledGridElementsValues < currentSelectedGridButtons.Length)
            {
                #region Logic Here : -> 
                // ReinitializeList for GridMainCellButtons's values and putting values(1-9) || Resetting Some Values
                ReinitializeList(GridMainCellButtonsArraySize);

                for (int i = 0; i < GridsButtonsReferences[gridFilledMainCells].Length; i++)
                {
                    x = true;

                    //Debug.Log($"currentButtonName: {currentSelectedGridButtons[currentFilledGridElementsValues].gameObject.name} currentButtonComparisons: {GridsButtonsReferences[gridFilledMainCells][i].gameObject.name} currentButtonComparisonsValue: {GridsButtonsReferences[gridFilledMainCells][i].GetComponentInChildren<TMP_Text>().text}");
                    
                    // Comparisons with values Vertically and Horizontally Logic
                    string currentGridButtonStringValue = GridsButtonsReferences[gridFilledMainCells][i].GetComponentInChildren<TMP_Text>().text;
                    if (string.IsNullOrEmpty(currentGridButtonStringValue))
                    {
                        continue;
                    }
                    else
                    {
                        int gridsDuplicateValue = int.Parse(GridsButtonsReferences[gridFilledMainCells][i].GetComponentInChildren<TMP_Text>().text);
                        if (randomIndexList.Contains(gridsDuplicateValue))
                        {
                            randomIndexList.Remove(gridsDuplicateValue);

                        }
                    }

                }
                if (x)
                {
					// RemoveExistingGridValuesfromRandomIndexListLoop : Loop for remove already existing values in grid from randomIndexList
					for (int i = 0; i < currentUniqueFilledGridElementsValues.Count; i++)
                    {
                        if (randomIndexList.Contains(currentUniqueFilledGridElementsValues[i]))
                        {
                            randomIndexList.Remove(currentUniqueFilledGridElementsValues[i]);

                        }
                    }

                    #region Internal Logic Here
                    // pickedUpButton
                    pickedUpButton = currentSelectedGridButtons[currentFilledGridElementsValues];

                    // FindingPlacesForCurrentpickedUpValues
                    FindingPlacesForCurrentpickedUpValues(randomIndexList, pickedUpButton);

                    // minimunExistenceCountsofRandomValuesList: Storing or Reinitializes New minimunExistenceCountsofRandomValuesList which is returned by 'MinimunExistenceCountsofRandomValuesList()' Method
                    List<int> minimunExistenceCountsofRandomValuesList = MinimunExistenceCountsofRandomValuesList();

                    // Choosing RandomValue from minimunExistenceCountsofRandomValuesList
                    randomIndex = Random.Range(0, minimunExistenceCountsofRandomValuesList.Count);

                    // Storing FinalValue || FinalRandomValue to put at a specific Position or in specific Button
                    randomIndexValue = minimunExistenceCountsofRandomValuesList[randomIndex];
                    #endregion

                    // Finally putting specific value (randomIndexValue) suggested by "FindingPlacesForCurrentpickedUpValues()" Method at SpecificPosition or Place
                    currentSelectedGridButtons[currentFilledGridElementsValues].GetComponentInChildren<TMP_Text>().text = randomIndexValue.ToString();

                    //To remove oldUniqueFilledGridElementsValues and Suggesting newUniqueGridElementsValues To Fill
                    currentUniqueFilledGridElementsValues.Add(randomIndexValue);

                    gridMainCells[gridFilledMainCells] = randomIndexValue; //place in position (1   
                    currentFilledGridElementsValues++; //place in position (3
                    gridFilledMainCells++; //place in position (3
                }
                #endregion

                TotalIterations++;

            }

            // Resetting Some Values
            currentFilledGridElementsValues = 0;
            #region InsertingGridValues
            // InsertingGridValues(GridMainCellButtonsArray, currentSelectedGridButtons);
            #endregion
            currentFilledGrids++; // To eliminate loop after all grids are filled
            GridIndex++; // To select next grid

        }
        Debug.Log(TotalIterations);
        //JustIterateAllGridsElementsValues();
    }

    #region FindingPlacesForCurrentpickedUpValues
    void FindingPlacesForCurrentpickedUpValues(List<int> pickedUpValues, Button pickedUpButton)
    {
        // ReinitializeRandomValuesExistenceCountsDictionary
        ReinitializeRandomValuesExistenceCountsDictionary();

        // Boolean for currentPickedUpRandomValueExistence
        bool currentPickedUpRandomValueExist = false;

        // pickedUpButtonReference
        Button pickedUpButtonReference = pickedUpButton;
        // pickedUpButtonGrid
        GameObject pickedUpButtonGrid = pickedUpButtonReference.transform.parent.parent.gameObject;
        // pickedUpButtonGridButtons
        Button[] pickedUpButtonGridButtons = pickedUpButtonGrid.GetComponentsInChildren<Button>();

        for (int pickedUpValuesIndex = 0; pickedUpValuesIndex < pickedUpValues.Count; pickedUpValuesIndex++)
        {
            // Holding currentPickedUpRandomValue
            int currentPickedUpRandomValue = pickedUpValues[pickedUpValuesIndex];

            for (int pickedUpButtonGridButtonsIndex = 0; pickedUpButtonGridButtonsIndex < pickedUpButtonGridButtons.Length; pickedUpButtonGridButtonsIndex++)
            {
                // Giving GridsButtonsReferencesCurrentButtonIndex : To suggesting AllGridsButtonsReferences for Comparisons with others
                int GridsButtonsReferencesCurrentButtonIndex = int.Parse(pickedUpButtonGridButtons[pickedUpButtonGridButtonsIndex].gameObject.name.Substring(0, 2));

                for (int GridsButtonsReferencesIndex = 0; GridsButtonsReferencesIndex < GridsButtonsReferences[GridsButtonsReferencesCurrentButtonIndex].Length; GridsButtonsReferencesIndex++)
                {
                    currentPickedUpRandomValueExist = false;

                    string currentGridButtonComparisonsButtonsStringValue = GridsButtonsReferences[GridsButtonsReferencesCurrentButtonIndex][GridsButtonsReferencesIndex].GetComponentInChildren<TMP_Text>().text;
                    if (string.IsNullOrEmpty(currentGridButtonComparisonsButtonsStringValue))
                    {
                        continue;
                    }
                    else
                    {
                        if (currentPickedUpRandomValue == int.Parse(GridsButtonsReferences[GridsButtonsReferencesCurrentButtonIndex][GridsButtonsReferencesIndex].GetComponentInChildren<TMP_Text>().text))
                        {
                            currentPickedUpRandomValueExist = true;
                            break;
                        }
                    }
                }
                if (!currentPickedUpRandomValueExist)
                {
                    RandomValuesExistenceCounts(currentPickedUpRandomValue);

                }
            }
        }

    }
    #endregion

    #region RandomValuesExistenceCounts
    void RandomValuesExistenceCounts(int currentPickedUpRandomValue)
    {
        if (RandomValuesExistenceCountsDictionary.ContainsKey(currentPickedUpRandomValue))
        {
            int incrementRandomValueinRandomValuesExistenceCountsDictionary = RandomValuesExistenceCountsDictionary[currentPickedUpRandomValue];
            // incrementRandomValueCounts
            incrementRandomValueinRandomValuesExistenceCountsDictionary++;
            // ReInitialize With New incrementedRandomValueCounts
            RandomValuesExistenceCountsDictionary[currentPickedUpRandomValue] = incrementRandomValueinRandomValuesExistenceCountsDictionary;
        }
        else
        {
            RandomValuesExistenceCountsDictionary.Add(currentPickedUpRandomValue, 1);

        }
    }
    #endregion

    #region MinimunExistenceCountsofRandomValuesList
    List<int> MinimunExistenceCountsofRandomValuesList()
    {
        // ReinitializeMinimunExistenceCountsofRandomValuesList
        ReinitializeMinimunExistenceCountsofRandomValuesList();

        // minimunExistenceCountsofRandomValues
        int minimunExistenceCountsofRandomValues = RandomValuesExistenceCountsDictionary.Min(x => x.Value);
        Debug.Log($"{minimunExistenceCountsofRandomValues} \n");

        foreach (KeyValuePair<int, int> RandomValuesExistenceCountsDictionaryRandomValue in RandomValuesExistenceCountsDictionary)
        {
            if (RandomValuesExistenceCountsDictionaryRandomValue.Value == minimunExistenceCountsofRandomValues)
            {
                minimunExistenceCountsofRandomValuesList.Add(RandomValuesExistenceCountsDictionaryRandomValue.Key);
            }
        }

        return minimunExistenceCountsofRandomValuesList;
    }
    #endregion

    #region InsertingGridValues
    void InsertingGridValues(int[] GridMainCellButtonsArray, Button[] currentSelectedGridButtons)
    {
        //To Skip Numbers Randomly (for to filled by user)
        int randomSkippedNumbersBool = 0;

        for (int i = 0; i < currentSelectedGridButtons.Length; i++)
        {
            #region skipped numbers randomly
            randomSkippedNumbersBool = Random.Range(0, 2);
            if (randomSkippedNumbersBool == 1)
            {
                currentSelectedGridButtons[i].GetComponentInChildren<TMP_Text>().text = GridMainCellButtonsArray[i].ToString();
            }
            else
            {
                currentSelectedGridButtons[i].GetComponentInChildren<TMP_Text>().text = "";
            }
            #endregion

        }
    }
    #endregion

    #region RandomGeneratorAlgorithm
    void RandomGeneratorAlgorithm()
    {
        // Gettings MainCellButtons to fill random values in it
        MainCellButtons = GridMainPanelDisplay.GetComponentsInChildren<Button>();
        // List Concept
        int randomIndex;
        int randomIndexValue;
        int currentelements = 0;
        int randomSkippedNumbersBool = 0;

        bool x = false;
        int TotalIterations = 0;

        int GridMainCellButtonsArraySize = EachGridMainCellButtons.Length;
        int[] GridMainCellButtonsArray = new int[GridMainCellButtonsArraySize];

        // Generating List To Pick Values Randomly from it
        randomIndexList = new List<int>();
        for (int i = 1; i < (GridMainCellButtonsArraySize + 1); i++)
        {
            randomIndexList.Add(i);
        }

        while (currentelements < GridMainCellButtonsArray.Length)
        {
            // List Random Index Concept
            randomIndex = Random.Range(0, randomIndexList.Count);
            randomIndexValue = randomIndexList[randomIndex];
            randomIndexList.RemoveAt(randomIndex);

            for (int j = 0; j < GridMainCellButtonsArray.Length; j++)
            {
                x = true;
                if (randomIndexValue == GridMainCellButtonsArray[j])
                {
                    x = false;
                    break;
                }
            }
            if (x)
            {
                GridMainCellButtonsArray[currentelements] = randomIndexValue;
                currentelements++;
            }
            TotalIterations++;
        }
        Debug.Log("TotalIterations: " + TotalIterations);
        for (int i = 0; i < MainCellButtons.Length; i++)
        {
            MainCellButtons[i].GetComponentInChildren<TMP_Text>().text = GridMainCellButtonsArray[i].ToString();

            #region skipped numbers randomly
            randomSkippedNumbersBool = Random.Range(0, 2);
            if (randomSkippedNumbersBool == 1)
            {
                MainCellButtons[i].GetComponentInChildren<TMP_Text>().text = GridMainCellButtonsArray[i].ToString();
            }
            else
            {
                MainCellButtons[i].GetComponentInChildren<TMP_Text>().text = "";
            }
            #endregion

        }
    }
    #endregion

    #region JustIterateAllGridsElementsValues
    void JustIterateAllGridsElementsValues()
    {
        for (int gridMainCellsIndex = 0; gridMainCellsIndex < gridMainCells.Length; gridMainCellsIndex++)
        {
            Debug.Log($"{gridMainCells[gridMainCellsIndex]}, ");
        }
    }
    #endregion

}
