using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Declaration

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;

    [SerializeField] GameObject endMenu;

    [SerializeField] Image healthMask;
    float originalHealthSize;

    GameObject previousMenu;

    #endregion
    public static UIManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        originalHealthSize = healthMask.rectTransform.rect.width;
        //ActivateMainMenu();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            ActivatePauseMenu();
        }
    }

    #region Menus
    void ActivateMainMenu()
    {
        mainMenu.SetActive(true);
    }
    void DeactivateMainMenu()
    {
        mainMenu.SetActive(false);
    }
    void ActivatePauseMenu()
    {
        pauseMenu.SetActive(true);
    }
    void DeactivatePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
    public void ActivateEndMenu()
    {
        endMenu.SetActive(true);
    }
    void DeactivateEndMenu()
    {
        endMenu.SetActive(false);
    }
    #endregion

    public void UpdateHealth(float total, float actual)
    {
        healthMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalHealthSize * (actual / total));
    }

    public void ButtonActivateMenu(GameObject menu)
    {
        menu.SetActive(true);
        //set previous menu
    }
    public void ButtonDectivateMenu(GameObject menu)
    {
        menu.SetActive(false);
        //set previous menu
    }

    public void ButtonChangeSceneString(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
    public void ButtonChangeSceneInt(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void ButtonChangeSceneNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }
}
