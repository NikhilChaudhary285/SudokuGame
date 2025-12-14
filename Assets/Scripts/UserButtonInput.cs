using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserButtonInput : MonoBehaviour
{
	// UserInputButton
	public Button UserInputButton;

	// SudokoValidator Script
	public SudokoValidator sudokoValidator;

	//Button userInputButton
	Button userInputButton;

	// Start is called before the first frame update
	void Start()
    {
        AddListeners();

	}
    void AddListeners()
    {
        UserInputButton.onClick.AddListener(GivingUserInput);
    }

	void GivingUserInput()
	{
		userInputButton = UserInputButton;
        sudokoValidator.TakingUserInput(userInputButton);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
