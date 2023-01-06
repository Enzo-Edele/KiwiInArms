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

    [SerializeField] Image bossHealthMask;
    float originalBossHealthSize;

    [SerializeField] Image playerHealthMask;
    float originalPlayerHealthSize;

    [SerializeField] GameObject playerHealthBar;
    [SerializeField] GameObject bossHealthBar;

    GameObject previousMenu;

    #endregion
    public static UIManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        originalBossHealthSize = bossHealthMask.rectTransform.rect.width;
        originalPlayerHealthSize = playerHealthMask.rectTransform.rect.width;
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
    public void DeactivateEndMenu()
    {
        endMenu.SetActive(false);
    }
    public void ActivateHealth()
    {
        playerHealthBar.SetActive(true);
        bossHealthBar.SetActive(true);
    }
    public void DeactivateHealth()
    {
        playerHealthBar.SetActive(false);
        bossHealthBar.SetActive(false);
    }
    #endregion

    public void UpdateHealth(float total, float actual)
    {
        bossHealthMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalBossHealthSize * (actual / total));
    }

    public void UpdatePlayerHealth(float total, float actual)
    {
        playerHealthMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalBossHealthSize * (actual / total));
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
