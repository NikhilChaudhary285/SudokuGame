using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class GameSettings : MonoBehaviour
{
    public Button EasyButton;
    public Button MiddleButton;
    public Button HardButton;

    // Start is called before the first frame update
    void Start()
    {
        AddListeners();
		ReInitializingGameSettings();
	}

	private void ReInitializingGameSettings()
	{
		#region extra
		// ReInitializing GameSettings (Default) When Reload Or Going Back (mainMenu) in the Game
		/*SudokuObject.RemovingorCheckingExistingValuesInRow = false;
		SudokuObject.RemovingorCheckingExistingValuesInColumn = false;
		SudokuObject.RemovingorCheckingExistingValuesInGrid = false;

		SudokuObject.LimitOfExistingValuesInRoworColumnorGrid = 3;*/
		#endregion

	}
	private void AddListeners()
    {
        EasyButton.onClick.AddListener(ClickOn_EasyButton);
        MiddleButton.onClick.AddListener(ClickOn_MiddleButton);
        HardButton.onClick.AddListener(ClickOn_HardButton);
    }

    void ClickOn_EasyButton()
    {
		#region extra
		//SudokuObject.RemovingorCheckingExistingValuesInRow = true;
		//SudokuObject.RemovingorCheckingExistingValuesInColumn = true;
		//SudokuObject.RemovingorCheckingExistingValuesInGrid = true;

		//SudokuObject.LimitOfExistingValuesInRoworColumnorGrid = 3;
		#endregion

		ExtraTesting.LimitOfExistingValuesInGrid = 61;
		SceneManager.LoadScene("SudokuGame");
	}

	void ClickOn_MiddleButton()
	{
		#region extra
		//SudokuObject.RemovingorCheckingExistingValuesInRow = true;
		//SudokuObject.RemovingorCheckingExistingValuesInColumn = true;
		//SudokuObject.RemovingorCheckingExistingValuesInGrid = true;

		//SudokuObject.LimitOfExistingValuesInRoworColumnorGrid = 4;
		#endregion

		ExtraTesting.LimitOfExistingValuesInGrid = 51;
		SceneManager.LoadScene("SudokuGame");
	}

	void ClickOn_HardButton()
	{
		#region extra
		//SudokuObject.RemovingorCheckingExistingValuesInRow = true;
		//SudokuObject.RemovingorCheckingExistingValuesInColumn = true;
		//SudokuObject.RemovingorCheckingExistingValuesInGrid = true;

		//SudokuObject.LimitOfExistingValuesInRoworColumnorGrid = 5;
		#endregion

		ExtraTesting.LimitOfExistingValuesInGrid = 31;
		SceneManager.LoadScene("SudokuGame");
	}

}
