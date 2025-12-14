using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlSettings : MonoBehaviour
{
    public Button BackButton;

    // Start is called before the first frame update
    void Start()
    {
        AddListeners();
    }

    private void AddListeners()
    {
        BackButton.onClick.AddListener(ClickOn_BackButton);
    }

    void ClickOn_BackButton()
    {
        SceneManager.LoadScene("SudokuMainMenu");
    }
}
